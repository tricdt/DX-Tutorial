using DevExpress.Xpo;
using DevExpress.VideoRent.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using System.Globalization;

namespace DevExpress.VideoRent {
    public class RentsHistory {
        Random random = new Random();
        abstract class RandomGenerator {
            static Random random = new Random();
            public RandomGenerator() : base() { }
            protected Random Random { get { return random; } }
            public abstract int Next();
            public abstract int GetMax();
            public abstract int GetMin();
        }
        abstract class RandomGeneratorNoRepetitions : RandomGenerator {
            public abstract void ClearRepetitions();
        }
        class RandomGeneratorFiveState : RandomGenerator {
            int zero, one, two, three, four, min, max;
            public RandomGeneratorFiveState(int min, int zero, int one, int two, int three, int four)
                : base() {
                max = min + (four != 0 ? 4 : three != 0 ? 3 : two != 0 ? 2 : one != 0 ? 1 : 0);
                this.min = min;
                this.zero = zero;
                this.one = this.zero + one;
                this.two = this.one + two;
                this.three = this.two + three;
                this.four = this.three + four;
            }
            public override int Next() {
                int n = Random.Next(four);
                if(n < zero) return min;
                if(n < one) return min + 1;
                if(n < two) return min + 2;
                if(n < three) return min + 3;
                return min + 4;
            }
            public override int GetMax() { return max; }
            public override int GetMin() { return min; }
        }
        class RandomGeneratorFlat : RandomGenerator {
            int min;
            int max;
            public static RandomGeneratorFlat MinMax(int min, int max) {
                RandomGeneratorFlat ret = new RandomGeneratorFlat();
                ret.min = min;
                ret.max = max;
                return ret;
            }
            private RandomGeneratorFlat() : base() { }
            public RandomGeneratorFlat(int aver, int delta)
                : base() {
                min = aver - delta;
                max = aver + delta;
            }
            public override int Next() { return Random.Next(min, 1 + max); }
            public override int GetMax() { return max; }
            public override int GetMin() { return min; }
        }
        class RandomGeneratorGauss : RandomGenerator {
            double aver;
            double delta;
            int min;
            int max;
            public RandomGeneratorGauss(int min, int max)
                : base() {
                this.min = min;
                this.max = max;
                this.aver = (double)(min + max) / 2.0;
                this.delta = (double)(max - min) / 2.0;
            }
            public override int Next() {
                int ret = (int)(aver + delta * RandomNormal());
                if(ret < min) return min;
                if(ret > max) return max;
                return ret;
            }
            private double RandomNormal() {
                const double s = 0.449871;
                const double t = -0.386595;
                const double a = 0.19600;
                const double b = 0.25472;
                const double r1 = 0.27597;
                const double r2 = 0.27846;
                double u;
                double v;
                while(true) {
                    u = Random.NextDouble();
                    v = 1.7156 * (Random.NextDouble() - 0.5);
                    double x = u - s;
                    double y = Math.Abs(v) - t;
                    double q = Math.Sqrt(x) + y * (a * y - b * x);
                    if(q < r1) break;
                    if(q <= r2 && Math.Sqrt(v) < -4.0 * Math.Log(u) * Math.Sqrt(u)) break;
                }
                return v / u;
            }
            public override int GetMax() { return max; }
            public override int GetMin() { return min; }
        }
        abstract class RandomGeneratorSimpleNoRepetitions : RandomGeneratorNoRepetitions {
            Dictionary<int, bool> repetitions = new Dictionary<int, bool>();
            RandomGenerator generator;

            public RandomGeneratorSimpleNoRepetitions(RandomGenerator generator) {
                this.generator = generator;
            }
            public override void ClearRepetitions() { repetitions.Clear(); }
            public override int Next() {
                int ret = generator.Next();
                int begin = ret;
                bool cycle = false;
                while(repetitions.ContainsKey(ret)) {
                    if(++ret > generator.GetMax()) ret = generator.GetMin();
                    if(ret == begin) {
                        cycle = true;
                        break;
                    }
                }
                if(!cycle) repetitions.Add(ret, true);
                return ret;
            }
            public override int GetMax() { return generator.GetMax(); }
            public override int GetMin() { return generator.GetMin(); }
            protected RandomGenerator Generator { get { return generator; } }
        }
        class RandomGeneratorFlatNoRepetitions : RandomGeneratorSimpleNoRepetitions {
            public RandomGeneratorFlatNoRepetitions(int aver, int delta)
                : base(new RandomGeneratorFlat(aver, delta)) {
            }
            private RandomGeneratorFlatNoRepetitions(bool minMax, int min, int max)
                : base(RandomGeneratorFlat.MinMax(min, max)) {
            }
            public static RandomGeneratorFlatNoRepetitions MinMax(int min, int max) { return new RandomGeneratorFlatNoRepetitions(true, min, max); }
        }
        class RandomGeneratorGaussNoRepetitions : RandomGeneratorSimpleNoRepetitions {
            public RandomGeneratorGaussNoRepetitions(int min, int max)
                : base(new RandomGeneratorGauss(min, max)) {
            }
        }
        class MovieItemData {
            public MovieItemData(MovieData movie, MovieItemFormat format) {
                Movie = movie;
                Format = format;
            }
            public MovieItem DBItem;
            public MovieItemFormat Format;
            public MovieData Movie;
            public void Return() {
                Movie.ReturnItem(this);
            }
        }
        class MovieData {
            Random random = new Random();

            public MovieData(Movie dbMovie)
                : base() {
                DBMovie = dbMovie;
                for(int formatIndex = 0; formatIndex < formats.Length; ++formatIndex)
                    ItemsWithFormat[formatIndex] = new LinkedList<MovieItemData>();
                for(int statusIndex = 0; statusIndex < statuses.Length; ++statusIndex) {
                    ItemsWithStatus[statusIndex] = new LinkedList<MovieItemData>();
                    for(int formatIndex = 0; formatIndex < formats.Length; ++formatIndex)
                        ItemsWithStatusAndFormat[statusIndex, formatIndex] = new LinkedList<MovieItemData>();
                }
            }
            public Movie DBMovie;
            public RandomGenerator LostItemsCountGenerator;
            public RandomGenerator DamageItemsCountGenerator;
            public LinkedList<MovieItemData> Items = new LinkedList<MovieItemData>();
            public LinkedList<MovieItemData>[] ItemsWithFormat = new LinkedList<MovieItemData>[formats.Length];
            public LinkedList<MovieItemData>[] ItemsWithStatus = new LinkedList<MovieItemData>[statuses.Length];
            public LinkedList<MovieItemData>[,] ItemsWithStatusAndFormat = new LinkedList<MovieItemData>[statuses.Length, formats.Length];
            public MovieItemData RentItem(MovieItemFormat format, bool isSell) {
                MovieItemStatus status = isSell ? MovieItemStatus.Sold : MovieItemStatus.Rented;
                MovieItemData item = GetActiveItem(format);
                ChangeItemStatus(item, MovieItemStatus.Active, status);
                return item;
            }
            public void ReturnItem(MovieItemData item) {
                ChangeItemStatus(item, MovieItemStatus.Rented, MovieItemStatus.Active);
            }
            public MovieItemData LostItem() {
                MovieItemFormat format = formats[random.Next(formats.Length)];
                MovieItemData item = GetActiveItem(format);
                ChangeItemStatus(item, MovieItemStatus.Active, MovieItemStatus.Lost);
                return item;
            }
            public MovieItemData DamageItem() {
                MovieItemFormat format = formats[random.Next(formats.Length)];
                MovieItemData item = GetActiveItem(format);
                ChangeItemStatus(item, MovieItemStatus.Active, MovieItemStatus.Damaged);
                return item;
            }
            MovieItemData GetActiveItem(MovieItemFormat format) {
                LinkedList<MovieItemData> activeItemsWithFormat = ItemsWithStatusAndFormat[statusesIndexes[MovieItemStatus.Active], formatsIndexes[format]];
                MovieItemData item;
                if(activeItemsWithFormat.Count != 0) return activeItemsWithFormat.First.Value;
                item = new MovieItemData(this, format);
                Items.AddLast(item);
                ItemsWithFormat[formatsIndexes[format]].AddLast(item);
                ItemsWithStatus[statusesIndexes[MovieItemStatus.Active]].AddLast(item);
                activeItemsWithFormat.AddLast(item);
                return item;
            }
            void ChangeItemStatus(MovieItemData item, MovieItemStatus oldStatus, MovieItemStatus newStatus) {
                ItemsWithStatus[statusesIndexes[oldStatus]].Remove(item);
                ItemsWithStatusAndFormat[statusesIndexes[oldStatus], formatsIndexes[item.Format]].Remove(item);
                ItemsWithStatus[statusesIndexes[newStatus]].AddLast(item);
                ItemsWithStatusAndFormat[statusesIndexes[newStatus], formatsIndexes[item.Format]].AddLast(item);
            }
        }
        class RentData {
            public Rent DBRent;
            public MovieData Movie;
            public MovieItemFormat Format;
            public MovieItemData Item;
            public int Days;
        }
        class Point {
            public Point(int day, int time)
                : base() {
                Day = day;
                Time = time;
            }
            public int Day;
            public int Time;
            public List<RentData> Rents = new List<RentData>();
            public List<RentData> Returns = new List<RentData>();
            public List<RentData> Sells = new List<RentData>();
        }
        class LostDamageItemAction {
            public LostDamageItemAction(int day, int time, MovieData movie, bool damage)
                : base() {
                Day = day;
                Time = time;
                Movie = movie;
                Damage = damage;
            }
            public int Day;
            public int Time;
            public bool Damage = false;
            public MovieData Movie;
            public MovieItemData Item;
        }
        class CustomerData {
            public Customer DBCustomer;
            public RandomGenerator InterPointsTermGenerator;
            public RandomGenerator MoviesForRentCountGenerator;
            public RandomGenerator OverdueDaysCountGenerator;
            public RandomGenerator OverdueDaysCountLastDayGenerator;
            public RandomGenerator MovieFormatGenerator;
            public RandomGenerator SellsCountGenerator;
            public RandomGenerator SellsInReceiptCountGenerator;
            public RandomGenerator ReceiptTimeGenerator;
            public int LastDayRentsCount = 2;
            public List<Point> Points = new List<Point>();
            public CustomerData(Customer dbCustomer)
                : base() {
                DBCustomer = dbCustomer;
            }
            public Point GetPoint(int day) {
                foreach(Point point in Points) {
                    if(point.Day == day) return point;
                }
                return null;
            }
        }
        Dictionary<MovieItemFormat, decimal> sellingPrices = new Dictionary<MovieItemFormat, decimal>();
        UnitOfWork session;
        CustomerData[] customers;
        MovieData[] movies;
        int daysCount;
        RandomGeneratorNoRepetitions movieGenerator;
        RandomGenerator lostDamageTimeGenerator;
        List<LostDamageItemAction> lostDamageItemActions = new List<LostDamageItemAction>();
        Dictionary<int, int> moviesMap = new Dictionary<int, int>();
        static MovieItemFormat[] formats = EnumHelper.GetValues<MovieItemFormat>();
        static MovieItemStatus[] statuses = EnumHelper.GetValues<MovieItemStatus>();
        static Dictionary<MovieItemFormat, int> formatsIndexes;
        static Dictionary<MovieItemStatus, int> statusesIndexes;
        RandomGenerator[] addedItemsCountGenerators;
        IBackgroundWorker backgroundWorker;
        IExceptionProcesser exceptionProcesser;

        public RentsHistory(int daysCount, UnitOfWork session, IBackgroundWorker backgroundWorker, IExceptionProcesser exceptionProcesser) {
            if(formatsIndexes == null) {
                formatsIndexes = new Dictionary<MovieItemFormat, int>();
                for(int i = 0; i < formats.Length; ++i)
                    formatsIndexes.Add(formats[i], i);
            }
            if(statusesIndexes == null) {
                statusesIndexes = new Dictionary<MovieItemStatus, int>();
                for(int i = 0; i < statuses.Length; ++i)
                    statusesIndexes.Add(statuses[i], i);
            }
            addedItemsCountGenerators = new RandomGenerator[formats.Length];
            this.exceptionProcesser = exceptionProcesser;
            this.session = session;
            this.daysCount = daysCount;
            this.backgroundWorker = backgroundWorker;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(Generate);
            this.backgroundWorker.RunWorkerAsync(System.Threading.Thread.CurrentThread.CurrentUICulture);
        }
        void Generate(object sender, DoWorkEventArgs e) {
            System.Threading.Thread.CurrentThread.CurrentUICulture = (CultureInfo)e.Argument;
            SetDefaultCategoryPrices();
            CreateCutPriceCategory();
            CreateNewReleasesCategory();
            GenerateAddedItemsCounts();
            IDGenerator.EnableFastGeneration(this.session);
            GenerateMoviesData();
            GenerateCustomersData();
            GeneratePoints();
            GenerateRentsAndSells();
            GenerateReturns();
            GenerateLostDamageItemActions();
            GenerateItems();
            WriteHistoryToDB();
            IDGenerator.DisableFastGeneration();
            SessionHelper.CommitSession(this.session, exceptionProcesser);
        }
        void SetDefaultCategoryPrices() {
            MovieCategory category = MovieCategory.GetDefaultCategory(session);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(0, 1.5M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(1, 2.25M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(2, 2.0M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(3, 1.75M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(0, 3.0M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(1, 4.0M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(2, 3.75M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(3, 3.5M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(4, 3.25M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(0, 1.0M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(1, 1.5M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(2, 1.25M);
            sellingPrices.Add(MovieItemFormat.DVD, 12.5M);
            sellingPrices.Add(MovieItemFormat.BlueRay, 22.5M);
            sellingPrices.Add(MovieItemFormat.VideoCD, 7.5M);
        }
        void CreateCutPriceCategory() {
            MovieCategory category = new MovieCategory(session, Properties.Resources.CutPriceMovieCategoryName);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(0, 0.7M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(1, 1.0M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(2, 0.9M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(3, 0.8M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(0, 1.5M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(1, 2.0M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(2, 1.9M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(3, 1.8M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(4, 1.7M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(0, 0.5M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(1, 0.7M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(2, 0.6M);
        }
        void CreateNewReleasesCategory() {
            MovieCategory category = new MovieCategory(session, Properties.Resources.NewReleasesPriceMovieCategoryName);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(0, 2.5M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(1, 3.75M);
            category.GetPrice(MovieItemFormat.DVD).SetDaysRentPrice(2, 3.0M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(0, 4.25M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(1, 5.25M);
            category.GetPrice(MovieItemFormat.BlueRay).SetDaysRentPrice(2, 4.75M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(0, 1.25M);
            category.GetPrice(MovieItemFormat.VideoCD).SetDaysRentPrice(1, 1.75M);
        }
        void GenerateAddedItemsCounts() {
            addedItemsCountGenerators[formatsIndexes[MovieItemFormat.DVD]] = RandomGeneratorFlat.MinMax(2, 4);
            addedItemsCountGenerators[formatsIndexes[MovieItemFormat.BlueRay]] = RandomGeneratorFlat.MinMax(2, 4);
            addedItemsCountGenerators[formatsIndexes[MovieItemFormat.VideoCD]] = RandomGeneratorFlat.MinMax(1, 2);
        }
        void GenerateMoviesData() {
            XPCollection<Movie> dbMovies = new XPCollection<Movie>(session);
            movies = new MovieData[dbMovies.Count];
            RandomGeneratorNoRepetitions mappedMovieGenerator = RandomGeneratorFlatNoRepetitions.MinMax(0, movies.Length - 1);
            for(int i = 0; i < movies.Length; ++i) {
                movies[i] = new MovieData(dbMovies[i]);
                movies[i].LostItemsCountGenerator = RandomGeneratorFlat.MinMax(0, 5);
                movies[i].DamageItemsCountGenerator = RandomGeneratorFlat.MinMax(0, 5);
                moviesMap.Add(i, mappedMovieGenerator.Next());
            }
            movieGenerator = new RandomGeneratorGaussNoRepetitions(0, movies.Length - 1);
            lostDamageTimeGenerator = RandomGeneratorFlat.MinMax(9 * 3600 + 1800, 18 * 3600 + 1800);
        }
        void GenerateCustomersData() {
            XPCollection<Customer> dbCustomers = new XPCollection<Customer>(session);
            customers = new CustomerData[dbCustomers.Count];
            for(int i = 0; i < customers.Length; ++i)
                customers[i] = new CustomerData(dbCustomers[i]);
            RandomGenerator averInterPointsTermGenerator = new RandomGeneratorFlat(4, 0);
            foreach(CustomerData customer in customers) {
                if(customer.DBCustomer.FirstName == "Essie") {
                    customer.LastDayRentsCount = 4;
                    customer.OverdueDaysCountLastDayGenerator = new RandomGeneratorFiveState(-1, 4, 3, 2, 2, 1);
                } else
                    customer.OverdueDaysCountLastDayGenerator = new RandomGeneratorFiveState(-1, 2, 6, 4, 3, 0);
                if(customer.DBCustomer.Photo == null) {
                    customer.SellsCountGenerator = RandomGeneratorFlat.MinMax(3, 7);
                    customer.MovieFormatGenerator = new RandomGeneratorFiveState(0, 50, 10, 40, 0, 0);
                }
                else {
                    customer.SellsCountGenerator = RandomGeneratorFlat.MinMax(10, 15);
                    customer.MovieFormatGenerator = new RandomGeneratorFiveState(0, 60, 30, 10, 0, 0);
                }
                customer.InterPointsTermGenerator = new RandomGeneratorFlat(averInterPointsTermGenerator.Next(), 3);
                customer.MoviesForRentCountGenerator = new RandomGeneratorFiveState(0, 70, 5, 20, 5, 0);
                customer.OverdueDaysCountGenerator = new RandomGeneratorFiveState(-2, 1, 2, 18, 2, 0);
                
                customer.SellsInReceiptCountGenerator = RandomGeneratorFlat.MinMax(1, 2);
                customer.ReceiptTimeGenerator = RandomGeneratorFlat.MinMax(9 * 3600 + 1800, 18 * 3600 + 1800);
            }
        }
        void GeneratePoints() {
            foreach(CustomerData customer in customers) {
                int lastDay;
                int day = 0;
                while(true) {
                    lastDay = day;
                    day += customer.InterPointsTermGenerator.Next();
                    if(day >= daysCount) break;
                    customer.Points.Add(new Point(day, customer.ReceiptTimeGenerator.Next()));
                }
                if(daysCount - lastDay > customer.InterPointsTermGenerator.GetMax()) {
                    lastDay += customer.InterPointsTermGenerator.Next() % (daysCount - lastDay);
                    customer.Points.Add(new Point(lastDay, customer.ReceiptTimeGenerator.Next()));
                }
            }
        }
        void GenerateLostDamageItemActions() {
            foreach(MovieData movie in movies) {
                int lostItemsCount = movie.LostItemsCountGenerator.Next();
                for(int i = 0; i < lostItemsCount; ++i) {
                    int day = random.Next(daysCount);
                    lostDamageItemActions.Add(new LostDamageItemAction(day, lostDamageTimeGenerator.Next(), movie, false));
                }
                int damageItemsCount = movie.DamageItemsCountGenerator.Next();
                for(int i = 0; i < damageItemsCount; ++i) {
                    int day = random.Next(daysCount);
                    lostDamageItemActions.Add(new LostDamageItemAction(day, lostDamageTimeGenerator.Next(), movie, true));
                }
            }
        }
        void GenerateSell(CustomerData customer, Point point) {
            int sellsInReceiptCount = customer.SellsInReceiptCountGenerator.Next();
            for(int k = 0; k < sellsInReceiptCount; ++k) {
                RentData sell = new RentData();
                sell.Movie = movies[moviesMap[movieGenerator.Next()]];
                sell.Format = formats[customer.MovieFormatGenerator.Next()];
                point.Sells.Add(sell);
            }
        }
        void GenerateRentsAndSells() {
            foreach(CustomerData customer in customers) {
                int sellsCount = customer.SellsCountGenerator.Next();
                Dictionary<Point, bool> sellPoints = new Dictionary<Point, bool>();
                for(int i = 0; i < sellsCount; ++i) {
                    Point point = customer.Points[random.Next(customer.Points.Count - 1)];
                    if(!sellPoints.ContainsKey(point)) sellPoints.Add(point, true);
                }
                for(int pointIndex = 0; pointIndex < customer.Points.Count - 1; ++pointIndex) {
                    movieGenerator.ClearRepetitions();
                    Point point = customer.Points[pointIndex];
                    Point nextPoint = customer.Points[pointIndex + 1];
                    int rentsCount = customer.MoviesForRentCountGenerator.Next();
                    for(int i = 0; i < rentsCount; ++i) {
                        RentData rent = new RentData();
                        rent.Movie = movies[moviesMap[movieGenerator.Next()]];
                        rent.Format = formats[customer.MovieFormatGenerator.Next()];
                        rent.Days = nextPoint.Day - point.Day - customer.OverdueDaysCountGenerator.Next();
                        point.Rents.Add(rent);
                    }
                    if(sellPoints.ContainsKey(point)) GenerateSell(customer, point);
                }
                Point lastPoint = customer.Points[customer.Points.Count - 1];
                movieGenerator.ClearRepetitions();
                int lastDayRentsCount = customer.LastDayRentsCount + customer.MoviesForRentCountGenerator.Next();
                for(int i = 0; i < lastDayRentsCount; ++i) {
                    RentData rent = new RentData();
                    rent.Days = daysCount - lastPoint.Day - customer.OverdueDaysCountLastDayGenerator.Next();
                    if(rent.Days < 1) rent.Days = 1;
                    rent.Movie = movies[moviesMap[movieGenerator.Next()]];
                    rent.Format = formats[customer.MovieFormatGenerator.Next()];
                    lastPoint.Rents.Add(rent);
                }
                if(sellPoints.ContainsKey(lastPoint)) GenerateSell(customer, lastPoint);
            }
        }
        void GenerateReturns() {
            foreach(CustomerData customer in customers) {
                for(int pointIndex = 1; pointIndex < customer.Points.Count; ++pointIndex) {
                    Point point = customer.Points[pointIndex];
                    Point prevPoint = customer.Points[pointIndex - 1];
                    foreach(RentData rent in prevPoint.Rents)
                        point.Returns.Add(rent);
                }
            }
        }
        void GenerateItems() {
            for(int day = 0; day < daysCount; ++day) {
                foreach(CustomerData customer in customers) {
                    foreach(Point point in customer.Points) {
                        if(point.Day != day) continue;
                        foreach(RentData rent in point.Rents)
                            rent.Item = rent.Movie.RentItem(rent.Format, false);
                        foreach(RentData rent in point.Returns)
                            rent.Item.Return();
                        foreach(RentData sell in point.Sells)
                            sell.Item = sell.Movie.RentItem(sell.Format, true);
                    }
                }
                foreach(LostDamageItemAction action in lostDamageItemActions) {
                    if(action.Day != day) continue;
                    if(action.Damage) action.Item = action.Movie.DamageItem();
                    else action.Item = action.Movie.LostItem();
                }
            }
        }
        void WriteHistoryToDB() {
            bool realTime = VideoRentDateTime.RealTime;
            VideoRentDateTime.RealTime = false;
            backgroundWorker.ReportProgress(10);
            WriteItemsToDB();
            double percentProgress = backgroundWorker.ReportedProgress;
            double percentPerDay = (70.0 - percentProgress) / daysCount;
            VideoRentDateTime.AddDays(-daysCount - 1);
            for(int day = 0; day < daysCount; ++day) {
                VideoRentDateTime.AddDays(1);
                foreach(CustomerData customer in customers) {
                    Point point = customer.GetPoint(day);
                    if(point == null) continue;
                    VideoRentDateTime.SetTimeOfDay(point.Time);
                    List<RentInfo> rentsInfo = new List<RentInfo>();
                    foreach(RentData rent in point.Rents)
                        rentsInfo.Add(new RentInfo(rent.Item.DBItem, rent.Days));
                    Receipt receipt = customer.DBCustomer.DoRent(rentsInfo);
                    foreach(RentData rent in point.Rents) {
                        foreach(Rent dbRent in receipt.Rents) {
                            if(dbRent.Item != rent.Item.DBItem) continue;
                            rent.DBRent = dbRent;
                            break;
                        }
                    }
                    List<Rent> returns = new List<Rent>();
                    foreach(RentData rent in point.Returns)
                        returns.Add(rent.DBRent);
                    customer.DBCustomer.ReturnRents(returns);
                    List<RentInfo> sellsInfo = new List<RentInfo>();
                    foreach(RentData sell in point.Sells)
                        sellsInfo.Add(new RentInfo(sell.Item.DBItem));
                    customer.DBCustomer.Buy(sellsInfo);
                }
                foreach(LostDamageItemAction action in lostDamageItemActions) {
                    if(action.Day != day) continue;
                    VideoRentDateTime.SetTimeOfDay(action.Time);
                    if(action.Damage) action.Item.DBItem.Status = MovieItemStatus.Damaged;
                    else action.Item.DBItem.Status = MovieItemStatus.Lost;
                }
                percentProgress += percentPerDay;
                backgroundWorker.ReportProgress((int)percentProgress);
            }
            backgroundWorker.ReportProgress(70);
            WriteAddedItems();
            SessionHelper.CommitInBackground(session, exceptionProcesser, backgroundWorker, 100);
            VideoRentDateTime.RealTime = realTime;
        }
        void WriteItemsToDB() {
            double percentProgress = backgroundWorker.ReportedProgress;
            double percentPerMovie = (20.0 - percentProgress) / movies.Length;
            for(int i = 0; i < movies.Length; ++i) {
                MovieData movie = movies[i];
                foreach(MovieItemData item in movie.Items)
                    item.DBItem = movie.DBMovie.AddItem(item.Format, sellingPrices[item.Format]);
                percentProgress += percentPerMovie;
                backgroundWorker.ReportProgress((int)percentProgress);
            }
            backgroundWorker.ReportProgress(20);
        }
        void WriteAddedItems() {
            XPCollection<Movie> dbMovies = new XPCollection<Movie>(session);
            double percentProgress = backgroundWorker.ReportedProgress;
            double percentPerMovie = (75.0 - percentProgress) / dbMovies.Count;
            foreach(Movie dbMovie in dbMovies) {
                for(int formatIndex = 0; formatIndex < formats.Length; ++formatIndex) {
                    if(addedItemsCountGenerators[formatIndex] == null) continue;
                    int addedItemsCount = addedItemsCountGenerators[formatIndex].Next();
                    for(int i = 0; i < addedItemsCount; ++i)
                        dbMovie.AddItem(formats[formatIndex], sellingPrices[formats[formatIndex]]);
                }
                percentProgress += percentPerMovie;
                backgroundWorker.ReportProgress((int)percentProgress);
            }
            backgroundWorker.ReportProgress(75);
        }
    }
}
