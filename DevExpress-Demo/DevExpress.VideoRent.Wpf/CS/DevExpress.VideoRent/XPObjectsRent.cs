using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

#if SL
using PrivateAttribute = System.ObsoleteAttribute;
using DevExpress.Xpf.Drawing;
#endif

namespace DevExpress.VideoRent {
    public interface IRentItem {
        MovieItem RentItem { get; }
        MovieItem SellItem { get; }
    }
    public class MovieInFormat : IRentItem {
        Movie movie;
        MovieItemFormat format;
        public MovieInFormat(Movie movie, MovieItemFormat format)
            : base() {
            this.movie = movie;
            this.format = format;
        }
        public Movie Movie { get { return movie; } }
        public MovieItemFormat Format { get { return format; } }
        public MovieItem RentItem {
            get {
                XPCollection<MovieItem> items = new XPCollection<MovieItem>(Movie.Items, CriteriaOperator.Parse("Status = ? and Format = ?", MovieItemStatus.Active, Format));
                if(items.Count == 0) return null;
                return items[0];
            }
        }
        public MovieItem SellItem {
            get {
                XPCollection<MovieItem> items = new XPCollection<MovieItem>(Movie.Items,
                    CriteriaOperator.Parse("Status = ? and AvailableForSell = ? and Format = ?", MovieItemStatus.Active, true, Format));
                if(items.Count == 0) return null;
                return items[0];
            }
        }
    }
    public class RentInfo {
        IRentItem item;
        int days;
        public RentInfo(IRentItem item, int days)
            : base() {
            this.item = item;
            this.days = days;
        }
        public RentInfo(IRentItem item) : this(item, 0) { }
        public RentInfo(Movie movie, MovieItemFormat format, int days) : this(new MovieInFormat(movie, format), days) { }
        public RentInfo(Movie movie, MovieItemFormat format) : this(new MovieInFormat(movie, format)) { }
        public IRentItem Item { get { return item; } }
        public int Days { get { return days; } }
    }
    public class MovieItem : VideoRentBaseObject, IRentItem {
        Movie movie;
        MovieItemStatus status;
        MovieItemFormat format;
        decimal sellingPrice;
        bool availableForSellMark;
        string location;

        public MovieItem(Session session) : base(session) { }
        public MovieItem(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public MovieItem(Movie movie)
            : this(movie.Session) {
            Movie = movie;
        }
        public MovieItem(Movie movie, MovieItemFormat format)
            : this(movie) {
            Format = format;
        }
        public MovieItem(Movie movie, MovieItemFormat format, decimal sellingPrice)
            : this(movie, format) {
            AvailableForSell = true;
            SellingPrice = sellingPrice;
        }
        internal MovieItem(Movie movie, MovieItemFormat format, decimal sellingPrice, string location)
            : this(movie, format, sellingPrice) {
            Location = location;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            Status = MovieItemStatus.Active;
            Format = MovieItemFormat.DVD;
            AvailableForSell = false;
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("MovieItemId"), Browsable(false), Private("", true)]
        public int _MovieItemId {
            get { return GetPropertyValue<int>("_MovieItemId"); }
            set { SetPropertyValue<int>("_MovieItemId", value); }
        }
        [PersistentAlias("_MovieItemId")]
        public int MovieItemId {
            get { return GetPropertyValue<int>("_MovieItemId"); }
            protected set { SetPropertyValue<int>("_MovieItemId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int MovieItemId {
            get { return GetPropertyValue<int>("MovieItemId"); }
            protected set { SetPropertyValue<int>("MovieItemId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "MovieItem"; } }
        public MovieItem RentItem {
            get {
                if(Status == MovieItemStatus.Active) return this;
                return null;
            }
        }
        public MovieItem SellItem {
            get {
                if(AvailableForSell && Status == MovieItemStatus.Active) return this;
                return null;
            }
        }
        [Association("Movie-Items"), DeleteIfNull]
        public Movie Movie {
            get { return movie; }
            set { SetPropertyValue<Movie>("Movie", ref movie, value); }
        }
        public string MovieTitle { get { return Movie.MovieTitle; } }
        public MovieGenre MovieGenre { get { return Movie.Genre; } }
        public string MovieLanguage { get { return Movie.Language.Name; } }
        public Image MoviePhoto { get { return Movie.Photo; } }
        public MovieRating MovieRating { get { return Movie.Rating; } }
        public string MoviePlot { get { return Movie.Plot; } }

        [Association("Item-Rents")]
        public XPCollection<Rent> Rents {
            get { return GetCollection<Rent>("Rents"); }
        }
        public MovieItemStatus Status {
            get { return status; }
            set { SetPropertyValue<MovieItemStatus>("Status", ref status, value); }
        }
        public MovieItemFormat Format {
            get { return format; }
            set { SetPropertyValue<MovieItemFormat>("Format", ref format, value); }
        }
        public decimal SellingPrice {
            get { return sellingPrice; }
            set { SetPropertyValue<decimal>("SellingPrice", ref sellingPrice, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Location {
            get { return location; }
            set { SetPropertyValue<string>("Location", ref location, value); }
        }
#if SL
        [Persistent("AvailableForSellMark"), Browsable(false), Private("", true)]
        public bool _AvailableForSellMark {
            get { return availableForSellMark; }
            set { SetPropertyValue<bool>("_AvailableForSellMark", ref availableForSellMark, value); }
        }
        [PersistentAlias("_AvailableForSellMark")]
        protected bool AvailableForSellMark {
            get { return availableForSellMark; }
            set { SetPropertyValue<bool>("_AvailableForSellMark", ref availableForSellMark, value); }
        }
#else
        [Persistent]
        protected bool AvailableForSellMark {
            get { return availableForSellMark; }
            set { SetPropertyValue<bool>("AvailableForSellMark", ref availableForSellMark, value); }
        }
#endif
        [NonPersistent]
        public bool AvailableForSell {
            get { return AvailableForSellMark && Status != MovieItemStatus.Sold; }
            set { AvailableForSellMark = value; }
        }
        public DateTime? LastRentedOn {
            get {
                if(Rents.Count == 0) return null;
                XPCollection<Rent> sortedRents = new XPCollection<Rent>(Rents);
                sortedRents.Sorting.Clear();
                sortedRents.Sorting.Add(new SortProperty("RentedOn", SortingDirection.Descending));
                return sortedRents[0].RentedOn;
            }
        }
        public bool WasInRent { get { return Rents.Count != 0; } }
        MovieCategoryPrice GetCategoryPrice() {
            return Movie.Category.GetPrice(Format);
        }
        public int CalcRentMaxDaysCount() { // return value (-1) stand for infinity
            if(!AvailableForSell) return -1;
            return GetCategoryPrice().CalcRentDaysCountForLessPrice(SellingPrice);
        }
        public decimal CalcOnOrderPrice(int days) {
            return GetCategoryPrice().CalcOnOrderPrice(days);
        }
        public static XPCollection<MovieItem> CreateFewItems(MovieItem pattern, int count) {
            XPCollection<MovieItem> ret = new XPCollection<MovieItem>(pattern.Session, false);
            for(int i = 0; i < count; ++i) {
                MovieItem item = new MovieItem(pattern.Movie, pattern.Format);
                item.AvailableForSell = pattern.AvailableForSell;
                item.SellingPrice = pattern.SellingPrice;
                ret.Add(item);
            }
            return ret;
        }
        public class CountInfo {
            MovieItemFormat[] formats;
            MovieItemStatus[] statuses;
            DateTime? lastRentedOn;
            int[,] withFormatAndStatus;
            int[] withFormatForSell;
            int[] withStatus;
            int[] withFormat;
            int forSell;
            int total;
            public CountInfo()
                : base() {
                formats = EnumHelper.GetValues<MovieItemFormat>();
                statuses = EnumHelper.GetValues<MovieItemStatus>();
                lastRentedOn = null;
                withFormatAndStatus = new int[formats.Length, statuses.Length];
                withFormatForSell = new int[formats.Length];
                withStatus = new int[statuses.Length];
                withFormat = new int[formats.Length];
            }
            public CountInfo(XPCollection<MovieItem> items)
                : this() {
                foreach(MovieItem item in items) {
                    if(item.LastRentedOn != null && (this.lastRentedOn == null || this.lastRentedOn.Value < this.lastRentedOn.Value)) this.lastRentedOn = item.LastRentedOn;
                    int formatIndex = FormatIndex(item.Format);
                    ++this.withFormatAndStatus[formatIndex, StatusIndex(item.Status)];
                    if(item.AvailableForSell) ++this.withFormatForSell[formatIndex];
                }
                total = 0;
                for(int statusIndex = 0; statusIndex < statuses.Length; ++statusIndex) {
                    withStatus[statusIndex] = 0;
                    for(int formatIndex = 0; formatIndex < formats.Length; ++formatIndex)
                        withStatus[statusIndex] += withFormatAndStatus[formatIndex, statusIndex];
                    total += withStatus[statusIndex];
                }
                forSell = 0;
                for(int formatIndex = 0; formatIndex < formats.Length; ++formatIndex) {
                    forSell += withFormatForSell[formatIndex];
                    withFormat[formatIndex] = 0;
                    for(int statusIndex = 0; statusIndex < statuses.Length; ++statusIndex)
                        withFormat[formatIndex] += withFormatAndStatus[formatIndex, statusIndex];
                }
            }
            public MovieItemStatus[] Statuses { get { return statuses; } }
            public DateTime? LastRentedOn { get { return lastRentedOn; } }
            public int[,] WithFormatAndStatus { get { return withFormatAndStatus; } }
            public int[] WithFormatForSell { get { return withFormatForSell; } }
            public int[] WithStatus { get { return withStatus; } }
            public int[] WithFormat { get { return withFormat; } }
            public int ForSell { get { return forSell; } }
            public int Total { get { return total; } }
            public int FormatIndex(MovieItemFormat format) {
                int ret = -1;
                for(int index = 0; index < formats.Length; ++index) {
                    if(formats[index] != format) continue;
                    ret = index;
                    break;
                }
                return ret;
            }
            public int StatusIndex(MovieItemStatus status) {
                int ret = -1;
                for(int index = 0; index < statuses.Length; ++index) {
                    if(statuses[index] != status) continue;
                    ret = index;
                    break;
                }
                return ret;
            }
        }
        public override bool AllowDelete { get { return base.AllowDelete && Status != MovieItemStatus.Rented && Status != MovieItemStatus.Sold && !WasInRent; } }
    }
    public class MovieCategory : VideoRentBaseObject {
        public static MovieCategory GetDefaultCategory(Session session) {
            return session.FindObject<MovieCategory>(CriteriaOperator.Parse("IsDefault = ?", true));
        }

        string name;
        bool isDefault;

        public MovieCategory(Session session) : base(session) { }
        public MovieCategory(Session session, string name)
            : base(session) {
            Name = name;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            IsDefault = false;
            foreach(MovieItemFormat format in EnumHelper.GetValues<MovieItemFormat>()) {
                new MovieCategoryPrice(this, format);
            }
        }
        protected override void OnDeleting() {
            base.OnDeleting();
            MovieCategory defaultCategory = MovieCategory.GetDefaultCategory(Session);
            foreach(Movie movie in new List<Movie>(Movies))
                movie.Category = defaultCategory;
        }
        [Indexed(Unique = true)]
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }
        [Association("Category-Movies")]
        public XPCollection<Movie> Movies { get { return GetCollection<Movie>("Movies"); } }
        [Association("Category-Prices"), Aggregated]
        public XPCollection<MovieCategoryPrice> Prices { get { return GetCollection<MovieCategoryPrice>("Prices"); } }
        [Browsable(false)]
        public bool IsDefault {
            get { return isDefault; }
            set { SetPropertyValue<bool>("IsDefault", ref isDefault, value); }
        }
        public MovieCategoryPrice GetPrice(MovieItemFormat format) {
            MovieCategoryPrice ret = null;
            Prices.Filter = CriteriaOperator.Parse("Format = ?", format);
            if(Prices.Count != 0) ret = Prices[0];
            Prices.Filter = null;
            return ret;
        }
#if !SL
        public List<ChartPriceData> GetPrices() {
            List<ChartPriceData> ret = new List<ChartPriceData>();
            foreach(MovieCategoryPrice price in Prices) {
                for(int i = 1; i < 8; i++)
                    ret.Add(new ChartPriceData(price.Format, i, GetMovieCategoryPriceDay(price, i)));
            }
            return ret;
        }
#endif
        public override bool AllowDelete { get { return base.AllowDelete && !IsDefault; } }
        public override string ToString() { return Name; }
        static decimal GetMovieCategoryPriceDay(MovieCategoryPrice price, int day) {
            decimal ret = price.GetDaysRentPrice(day);
            if(ret == 0) return price.GetDaysRentPrice(0);
            return ret;
        }
    }
    public class MovieCategoryPrice : VideoRentBaseObject {
        static public int TermsRentCount = 7;
        MovieItemFormat format;
        MovieCategory category;
        int defaultRentDays;
        decimal[] daysRentPrice = new decimal[TermsRentCount + 1]; // daysRentPrice[0] - Late fee per day

        public MovieCategoryPrice(Session session) : base(session) { }
        public MovieCategoryPrice(MovieCategory category, MovieItemFormat format)
            : this(category.Session) {
            Category = category;
            Format = format;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            Format = MovieItemFormat.DVD;
            DefaultRentDays = 1;
            LateRentPrice = 1;
        }
        public int DefaultRentDays {
            get { return defaultRentDays; }
            set { SetPropertyValue<int>("DefaultRentDays", ref defaultRentDays, value); }
        }
        public decimal DefaultPrice { get { return CalcOnOrderPrice(DefaultRentDays) / DefaultRentDays; } }
        [Association("Category-Prices"), Indexed("Format", Unique = true), DeleteIfNull]
        public MovieCategory Category {
            get { return category; }
            set { SetPropertyValue<MovieCategory>("Category", ref category, value); }
        }
        public MovieItemFormat Format {
            get { return format; }
            set { SetPropertyValue<MovieItemFormat>("Format", ref format, value); }
        }
        public decimal LateRentPrice { get { return GetDaysRentPrice(0); } set { SetDaysRentPrice(0, value); } }
        public decimal Days1RentPrice { get { return GetDaysRentPrice(1); } set { SetDaysRentPrice(1, value); } }
        public decimal Days2RentPrice { get { return GetDaysRentPrice(2); } set { SetDaysRentPrice(2, value); } }
        public decimal Days3RentPrice { get { return GetDaysRentPrice(3); } set { SetDaysRentPrice(3, value); } }
        public decimal Days4RentPrice { get { return GetDaysRentPrice(4); } set { SetDaysRentPrice(4, value); } }
        public decimal Days5RentPrice { get { return GetDaysRentPrice(5); } set { SetDaysRentPrice(5, value); } }
        public decimal Days6RentPrice { get { return GetDaysRentPrice(6); } set { SetDaysRentPrice(6, value); } }
        public decimal Days7RentPrice { get { return GetDaysRentPrice(7); } set { SetDaysRentPrice(7, value); } }
        public decimal GetDaysRentPrice(int index) { return daysRentPrice[index]; }
        public void SetDaysRentPrice(int index, decimal value) {
            if(index == 0) SetPropertyValue<decimal>("LateRentPrice", ref daysRentPrice[0], value);
            else SetPropertyValue<decimal>(string.Format("Days{0}RentPrice", index), ref daysRentPrice[index], value);
        }
        public decimal CalcOnOrderPrice(int days) {
            if(days < 1) throw new ArgumentOutOfRangeException("days");
            int normalDays = days > TermsRentCount ? TermsRentCount : days;
            int priceDays = normalDays;
            while(GetDaysRentPrice(priceDays) == 0 && priceDays > 0) --priceDays;
            decimal onOrderPrice = normalDays * GetDaysRentPrice(priceDays);
            if(days > TermsRentCount) onOrderPrice += (days - TermsRentCount) * LateRentPrice;
            return onOrderPrice;
        }
        public int CalcRentDaysCountForLessPrice(decimal price) {
            decimal priceNormalDays = CalcOnOrderPrice(TermsRentCount);
            if(price > priceNormalDays) return TermsRentCount + (int)MathHelper.Ceiling((price - priceNormalDays) / LateRentPrice) - 1;
            int daysCount = TermsRentCount;
            while(daysCount > 0 && CalcOnOrderPrice(daysCount) >= price) --daysCount;
            return daysCount;
        }
    }
    public class Rent : VideoRentBaseObject {
        MovieItem item;
        int days;
        DateTime? returnedOn;
        Receipt receipt;
        Receipt receiptOverdue;
        decimal payment;
        decimal overduePayment;

        public Rent(Session session) : base(session) { }
        public Rent(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public Rent(Receipt receipt, MovieItem item) : this(receipt, item, 0) { }
        public Rent(Receipt receipt, MovieItem item, int days)
            : this(receipt.Session) {
            if(receipt == null) throw new ArgumentNullException("Receipt");
            if(item == null) throw new ArgumentNullException("Item");
            if(item.Status != MovieItemStatus.Active) throw new ArgumentException("Item is not active");
            Receipt = receipt;
            Item = item;
            if(receipt.Type == ReceiptType.Purchases) {
                Item.Status = MovieItemStatus.Sold;
                Item.AvailableForSell = false;
                Days = 0;
            } else {
                Item.Status = MovieItemStatus.Rented;
                Days = days != 0 ? days : item.Movie.Category.GetPrice(item.Format).DefaultRentDays;
            }
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            Days = 1;
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("RentId"), Browsable(false), Private("", true)]
        public int _RentId {
            get { return GetPropertyValue<int>("_RentId"); }
            set { SetPropertyValue<int>("_RentId", value); }
        }
        [PersistentAlias("_RentId")]
        public int RentId {
            get { return GetPropertyValue<int>("_RentId"); }
            protected set { SetPropertyValue<int>("_RentId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int RentId {
            get { return GetPropertyValue<int>("RentId"); }
            protected set { SetPropertyValue<int>("RentId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "Rent"; } }
#if SL
        [Association("Item-Rents")]
        [Persistent("Item"), Browsable(false), Private("", true)]
        public MovieItem _Item {
            get { return item; }
            set { SetPropertyValue<MovieItem>("_Item", ref item, value); }
        }
        [PersistentAlias("_Item")]
        public MovieItem Item {
            get { return item; }
            protected set { SetPropertyValue<MovieItem>("_Item", ref item, value); }
        }
#else
        [Persistent, Association("Item-Rents")]
        public MovieItem Item {
            get { return item; }
            protected set { SetPropertyValue<MovieItem>("Item", ref item, value); }
        }
#endif
        public int Days {
            get { return days; }
            set { SetPropertyValue<int>("Days", ref days, value); }
        }
        public DateTime? ReturnedOn {
            get { return returnedOn; }
            set { SetPropertyValue<DateTime?>("ReturnedOn", ref returnedOn, value); }
        }
        [Indexed(Unique = false)]
        public DateTime ExpectedOn { get { return ReturnedOn.HasValue ? ReturnedOn.Value : ClearedReturnedOn; } }
        public DateTime ClearedReturnedOn { get { return RentedOn.AddDays(Days); } }
        public DateTime ClearedReturnedOnDate { get { return ClearedReturnedOn.Date; } }
        public int Overdue {
            get {
                DateTime date = ReturnedOn.HasValue ? ReturnedOn.Value : DevExpress.VideoRent.Helpers.VideoRentDateTime.Now;
                return (int)(((double)date.Ticks - RentedOn.Ticks) / TimeSpan.TicksPerDay - Days);
            }
        }
        public int ActiveType {
            get {
                if(!Active) return (int)ActiveRentType.None;
                if(Overdue > 0) return (int)ActiveRentType.Overdue;
                if(Overdue == 0) return (int)ActiveRentType.Today;
                return (int)ActiveRentType.Active;
            }
        }
        public bool IsOverdue { get { return Overdue > 0; } }
        public bool Active { get { return ReturnedOn == null && Item.Status == MovieItemStatus.Rented; } }
        public Customer Customer { get { return Receipt.Customer; } }
        [Indexed(Unique = false), PersistentAlias("Receipt.Date")]
        public DateTime RentedOn { get { return Receipt.Date; } }
        public decimal Payment {
            get { return payment; }
            set { SetPropertyValue<decimal>("Payment", ref payment, value); }
        }
        public decimal OverduePayment {
            get { return overduePayment; }
            set { SetPropertyValue<decimal>("OverduePayment", ref overduePayment, value); }
        }
        public string MovieTitle { get { return Item.MovieTitle; } }
        public MovieGenre MovieGenre { get { return Item.MovieGenre; } }
        public string MovieLanguage { get { return Item.MovieLanguage; } }
        public Image MoviePhoto { get { return Item.MoviePhoto; } }
        public int MovieRating { get { return (int)Item.MovieRating; } }
        public string MoviePlot { get { return Item.MoviePlot; } }
        public Movie Movie { get { return Item.Movie; } }

        public MovieItemFormat ItemFormat {
            get {
                if(Item == null) return MovieItemFormat.DVD;
                return Item.Format;
            }
        }
        public string ItemFormatCaption { get { return EnumTitlesKeeper<MovieItemFormat>.GetTitle(ItemFormat); } }
        public string MovieInfo { get { return string.Format("{0} - {1}", MovieTitle, EnumTitlesKeeper<MovieItemFormat>.GetTitle(ItemFormat)); } }
#if SL
        [Association("Receipt-Rents")]
        [Persistent("Receipt"), Browsable(false), Private("", true)]
        public Receipt _Receipt {
            get { return receipt; }
            set { SetPropertyValue<Receipt>("_Receipt", ref receipt, value); }
        }
        [PersistentAlias("_Receipt")]
        public Receipt Receipt {
            get { return receipt; }
            protected set { SetPropertyValue<Receipt>("_Receipt", ref receipt, value); }
        }
#else
        [Persistent, Association("Receipt-Rents")]
        public Receipt Receipt {
            get { return receipt; }
            protected set { SetPropertyValue<Receipt>("Receipt", ref receipt, value); }
        }
#endif
        public ReceiptType ReceiptType { get { return Receipt.Type; } }
        [Association("ReceiptOverdue-Rents")]
        public Receipt ReceiptOverdue {
            get { return receiptOverdue; }
            set { SetPropertyValue<Receipt>("ReceiptOverdue", ref receiptOverdue, value); }
        }
        public void Return() {
            if(Item.Status != MovieItemStatus.Rented) throw new Exception("Item is not in rent");
            ReturnedOn = DevExpress.VideoRent.Helpers.VideoRentDateTime.Now;
            Item.Status = MovieItemStatus.Active;
        }
        public decimal CalcPayment() {
            if(Receipt.Type == ReceiptType.Purchases) Payment = Item.SellingPrice;
            else Payment = Item.CalcOnOrderPrice(Days);
            return Payment;
        }
        public decimal CalcOverduePayment() {
            OverduePayment = Item.CalcOnOrderPrice(Days + Overdue) - Payment;
            return OverduePayment;
        }
    }
    public class Receipt : VideoRentBaseObject {
        Customer customer;
        DateTime date;
        ReceiptType type;
        decimal payment;
        decimal discount;
        bool closed;

        public Receipt(Session session) : base(session) { }
        public Receipt(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public Receipt(Customer customer, ReceiptType type)
            : base(customer.Session) {
            if(customer == null) throw new ArgumentNullException("Customer");
            Customer = customer;
            Type = type;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            Date = VideoRent.Helpers.VideoRentDateTime.Now;
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("ReceiptId"), Browsable(false), Private("", true)]
        public int _ReceiptId {
            get { return GetPropertyValue<int>("_ReceiptId"); }
            protected set { SetPropertyValue<int>("_ReceiptId", value); }
        }
        [PersistentAlias("_ReceiptId")]
        public int ReceiptId {
            get { return GetPropertyValue<int>("_ReceiptId"); }
            protected set { SetPropertyValue<int>("_ReceiptId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int ReceiptId {
            get { return GetPropertyValue<int>("ReceiptId"); }
            protected set { SetPropertyValue<int>("ReceiptId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "Receipt"; } }
#if SL
        [Association("Customer-Receipts")]
        [Persistent("Customer"), Browsable(false), Private("", true)]
        public Customer _Customer {
            get { return customer; }
            set { SetPropertyValue<Customer>("_Customer", ref customer, value); }
        }
        [PersistentAlias("_Customer")]
        public Customer Customer {
            get { return customer; }
            protected set { SetPropertyValue<Customer>("_Customer", ref customer, value); }
        }
#else
        [Persistent, Association("Customer-Receipts")]
        public Customer Customer {
            get { return customer; }
            protected set { SetPropertyValue<Customer>("Customer", ref customer, value); }
        }
#endif
        [Indexed(Unique = false)]
        public DateTime Date {
            get { return date; }
            set { SetPropertyValue<DateTime>("Date", ref date, value); }
        }
#if SL
        [Persistent("Type"), Browsable(false), Private("", true)]
        public ReceiptType _Type {
            get { return type; }
            protected set { SetPropertyValue<ReceiptType>("_Type", ref type, value); }
        }
        [PersistentAlias("_Type")]
        public ReceiptType Type {
            get { return type; }
            protected set { SetPropertyValue<ReceiptType>("_Type", ref type, value); }
        }
#else
        [Persistent]
        public ReceiptType Type {
            get { return type; }
            protected set { SetPropertyValue<ReceiptType>("Type", ref type, value); }
        }
#endif
        public decimal Payment {
            get { return payment; }
            set { SetPropertyValue<decimal>("Payment", ref payment, value); }
        }
        public decimal Discount {
            get { return discount; }
            set { SetPropertyValue<decimal>("Discount", ref discount, value); }
        }
#if SL
        [Persistent("Closed"), Browsable(false), Private("", true)]
        public bool _Closed {
            get { return closed; }
            set { SetPropertyValue<bool>("Closed", ref closed, value); }
        }
        [PersistentAlias("_Closed")]
        public bool Closed {
            get { return closed; }
            protected set { SetPropertyValue<bool>("_Closed", ref closed, value); }
        }
#else
        [Persistent]
        public bool Closed {
            get { return closed; }
            protected set { SetPropertyValue<bool>("Closed", ref closed, value); }
        }
#endif
        [Association("Receipt-Rents")]
        public XPCollection<Rent> Rents { get { return GetCollection<Rent>("Rents"); } } //Type = Rent or Sale
        [Association("ReceiptOverdue-Rents")]
        public XPCollection<Rent> OverdueRents { get { return GetCollection<Rent>("OverdueRents"); } } //Type = Overdue
        public void CalcPayment() {
            if(Type == ReceiptType.LateFees) CalcOverduePayment();
            else CalcOrdinarPayment();
            Customer.CalcDiscount();
        }
        void CalcOrdinarPayment() {
            decimal payment = 0;
            foreach(Rent rent in Rents) {
                payment += rent.CalcPayment();
            }
            CalcDiscount(payment);
        }
        void CalcOverduePayment() {
            decimal payment = 0;
            foreach(Rent rent in OverdueRents) {
                payment += rent.CalcOverduePayment();
            }
            CalcDiscount(payment);
        }
        void CalcDiscount(decimal payment) {
            Discount = Math.Round(payment * Customer.Discount * 100) / 100;
            Payment = payment - Discount;
        }
    }
    public class ChartPriceData {
        MovieItemFormat format;
        int day;
        decimal price;
        public ChartPriceData(MovieItemFormat format, int day, decimal price) {
            this.format = format;
            this.day = day;
            this.price = price;
        }
        public string Type { get { return EnumTitlesKeeper<MovieItemFormat>.GetTitle(format); } }
        public decimal Price { get { return price; } }
        public int Day { get { return day; } }
    }
}
