#if DebugTest
using System;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

#if SL
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
using TestInitializeAttribute = NUnit.Framework.SetUpAttribute;
using TestCleanupAttribute = NUnit.Framework.TearDownAttribute;
using NUnit.Framework;
#endif

namespace DevExpress.VideoRent.Tests {
    [TestClass]
    public class XPOObjectsTests : XPOObjectsBaseTests {
        [TestMethod]
        public void GeneratedId() {
            Assert.AreEqual(1, Avatar.MovieId);
            Assert.AreEqual(3, Anton.CustomerId);
            Customer lastCustomer = new Customer(Session);
            Session.CommitChanges();
            int lastId = lastCustomer.CustomerId;
            Customer customerWithId = new Customer(Session, lastId + 1, string.Empty, string.Empty, string.Empty);
            Customer customer = new Customer(Session);
            Session.CommitChanges();
            Assert.AreEqual(customerWithId.CustomerId + 1, customer.CustomerId);
        }
        [TestMethod]
        public void GenerateCustomerId() {
            using(NestedUnitOfWork nuow = Session.BeginNestedUnitOfWork()) {
                new Customer(nuow, "First", "Customer");
                nuow.CommitChanges();
            }
            Session.CommitChanges();
            int firstId = Session.FindObject<Customer>(CriteriaOperator.Parse("FullName = ?", "First Customer")).CustomerId;
            using(NestedUnitOfWork nuow = Session.BeginNestedUnitOfWork()) {
                new Customer(nuow, "Second", "Customer");
                nuow.CommitChanges();
            }
            Session.CommitChanges();
            int secondId = Session.FindObject<Customer>(CriteriaOperator.Parse("FullName = ?", "Second Customer")).CustomerId;
            Assert.AreEqual(1, secondId - firstId);
        }
        [TestMethod]
        public void CreatedByUserAndCreatedAt() {
            Assert.IsNotNull(Avatar.CreatedBy);
            Assert.IsNotNull(Avatar.CreatedAt);
        }
        [TestMethod]
        public void AddMovieItem_AvailableForRent() {
            Movie movie = new Movie(Session);
            Assert.IsFalse(movie.IsAvailableForRent);
            movie.AddItem();
            Assert.IsTrue(movie.IsAvailableForRent);
        }
        [TestMethod]
        public void CheckDefaultCategory() {
            Assert.IsNotNull(MovieCategory.GetDefaultCategory(Session));
        }
        [TestMethod]
        public void RentMovieByCustomer_AvailableForRent() {
            Movie movie = new Movie(Session);
            movie.AddItem(MovieItemFormat.DVD, 10);
            Assert.IsTrue(movie.IsAvailableForRent);
            Customer customer = new Customer(Session);
            Rent rent = customer.DoRent(new RentInfo(movie)).Rents[0];
            Assert.IsNotNull(rent);
            Assert.AreEqual(1, rent.Days);
            Assert.AreEqual(customer, rent.Customer);
            Assert.AreEqual(movie.Items[0], rent.Item);
            Assert.IsFalse(movie.IsAvailableForRent);
            Assert.IsNull(customer.DoRent(new RentInfo(movie)));
        }
        [TestMethod]
        public void Rent_And_ReturnMovie() {
            Rent rent = Andrew.DoRent(new RentInfo(Avatar)).Rents[0];
            Assert.IsFalse(Avatar.IsAvailableForRent);
            Assert.IsTrue(rent.Active);
            rent.Return();
            Assert.IsFalse(rent.Active);
            Assert.IsTrue(Avatar.IsAvailableForRent);
        }
        [TestMethod]
        public void RentOn_And_DateReturned() {
            VideoRentDateTime.AddDays(-2);
            Rent rent = Andrew.DoRent(new RentInfo(Avatar)).Rents[0];
            Assert.IsNull(rent.ReturnedOn);
            VideoRentDateTime.AddDays(2);
            rent.Return();
            Assert.AreEqual(VideoRentDateTime.Now.Date, rent.ReturnedOn.Value.Date);
            Assert.AreEqual(VideoRentDateTime.Now.AddDays(-2).Date, rent.RentedOn.Date);
        }
        [TestMethod]
        public void Directors() {
            Assert.AreEqual("James Cameron", Avatar.Directors);
            Artist director = new Artist(Session, "Test", "Director");
            Avatar.AddArtist(director, MovieArtistLine.GetDirector(Session));
            Session.CommitChanges();
            Assert.AreEqual("James Cameron, Test Director", Avatar.Directors);
        }
        [TestMethod]
        public void RentStatus() {
            Rent rent = Andrew.DoRent(new RentInfo(Avatar)).Rents[0];
            MovieItem avatarItem = rent.Item;
            Assert.IsTrue(rent.Active);
            Assert.AreEqual(MovieItemStatus.Rented, avatarItem.Status);
            rent.Return();
            Assert.IsFalse(rent.Active);
            Assert.AreEqual(MovieItemStatus.Active, avatarItem.Status);
            Rent newRent = Anton.DoRent(new RentInfo(avatarItem)).Rents[0];
            Assert.IsTrue(newRent.Active);
            Assert.AreEqual(MovieItemStatus.Rented, avatarItem.Status);
            Assert.IsFalse(rent.Active);
        }
        [TestMethod]
        public void CreatingAndDeletingPrices() {
            MovieCategory category = new MovieCategory(Session);
            foreach(MovieItemFormat format in EnumHelper.GetValues<MovieItemFormat>()) {
                MovieCategoryPrice price = category.GetPrice(MovieItemFormat.DVD);
                Assert.IsNotNull(price);
            }
            XPCollection<MovieCategoryPrice> prices = category.Prices;
            category.Delete();
            foreach(MovieCategoryPrice price in prices)
                Assert.IsTrue(price.IsDeleted);
        }
        [TestMethod]
        public void CreatingMovieAndCheckDefaultCategory() {
            Movie movie = new Movie(Session);
            Assert.IsNotNull(movie.Category);
            Assert.AreEqual(movie.Category, MovieCategory.GetDefaultCategory(Session));
        }
        [TestMethod]
        public void SaveMovieWithNullCategory() {
            Movie movie = new Movie(Session, "new film");
            movie.Category = null;
            Session.CommitChanges();
            Assert.AreEqual(MovieCategory.GetDefaultCategory(Session), movie.Category);
        }
        [TestMethod]
        public void MovieItemCreateFewItems() {
            int itemsCount = new XPCollection<MovieItem>(Session).Count;
            MovieItem.CreateFewItems(Avatar.Items[0], 0);
            Session.CommitChanges();
            int newItemsCount = new XPCollection<MovieItem>(Session).Count;
            Assert.AreEqual(itemsCount, newItemsCount);
            MovieItem.CreateFewItems(Avatar.Items[0], 3);
            Session.CommitChanges();
            newItemsCount = new XPCollection<MovieItem>(Session).Count;
            Assert.AreEqual(itemsCount + 3, newItemsCount);
        }
        [TestMethod]
        public void MovieItemCountInfo() {
            MovieItem.CreateFewItems(Avatar.Items[0], 100);
            Session.CommitChanges();
            MovieItem.CountInfo countInfo = new MovieItem.CountInfo(Avatar.Items);
            Assert.AreEqual(Avatar.Items.Count, countInfo.Total);
        }
        [TestMethod]
        public void MovieItemLastRentedOn() {
            VideoRentDateTime.AddDays(10);
            DateTime rentDate = VideoRentDateTime.Now;
            Rent rent = Anton.DoRent(new RentInfo(Postal)).Rents[0];
            MovieItem item = rent.Item;
            VideoRentDateTime.AddDays(2);
            rent.Return();
            Assert.AreEqual(rentDate, item.LastRentedOn);
        }
        [TestMethod]
        public void RentMovies_CalcPayment() {
            Receipt receipt = Alex.Buy(new RentInfo[] { new RentInfo(Avatar), new RentInfo(Cube) });
            Assert.AreEqual(2, receipt.Rents.Count);
            Assert.AreEqual(MovieItemStatus.Sold, receipt.Rents[0].Item.Status);
            Assert.AreEqual(MovieItemStatus.Sold, receipt.Rents[1].Item.Status);
            decimal payment = receipt.Rents[0].Item.SellingPrice + receipt.Rents[1].Item.SellingPrice;
            Assert.AreEqual(payment, receipt.Payment);
        }
        [TestMethod]
        public void ReturnMovie() {
            Rent rent = Alex.DoRent(new RentInfo(Avatar, 1)).Rents[0];
            VideoRentDateTime.AddDays(3);
            Receipt overdueReceipt = Alex.ReturnRent(rent);
            Assert.IsNotNull(overdueReceipt);
            Assert.AreNotEqual(0, overdueReceipt.Payment);
        }
        [TestMethod]
        public void MovieItemAsRentItem() {
            MovieItem item = Avatar.GetActiveItems()[0];
            Assert.AreEqual(item, item.RentItem);
            item.Status = MovieItemStatus.Rented;
            Assert.IsNull(item.RentItem);
        }
        [TestMethod]
        public void CustomerIsDebter() {
            Receipt antonReceipt = Anton.DoRent(new RentInfo(Postal, 1));
            VideoRentDateTime.AddDays(5);
            Andrew.DoRent(new RentInfo(Avatar, 3));
            Session.CommitChanges();
            Assert.IsTrue(Anton.IsDebter);
            Assert.IsFalse(Andrew.IsDebter);
            Anton.ReturnRent(antonReceipt.Rents[0]);
            Session.CommitChanges();
            Assert.IsFalse(Anton.IsDebter);
        }
        [TestMethod]
        public void MovieAddItem_CheckPrice() {
            Assert.AreEqual(100, Avatar.AddItem(MovieItemFormat.DVD, 100).SellingPrice);
        }
        [TestMethod]
        public void DeleteMovieWithNullCategory() {
            Avatar.Category = null;
            Avatar.Delete();
            Session.CommitChanges();
            MovieCategory category = MovieCategory.GetDefaultCategory(Session);
            XPCollection<Movie> movies = new XPCollection<Movie>(category.Movies);
            foreach(Movie movie in movies) {
                movie.Awards = "1";
            }
            Session.CommitChanges();
        }
        [TestMethod]
        public void DeleteMovieWithNullTitle() {
            Avatar.Title = null;
            Avatar.Delete();
            Session.CommitChanges();
        }
        [TestMethod]
        public void TryDeleteItemWasInRent() {
            MovieItem item = Avatar.AddItem();
            Receipt receipt = Andrew.DoRent(new RentInfo(item));
            VideoRentDateTime.AddDays(3);
            Andrew.ReturnRents(receipt.Rents);
            SessionHelper.CommitSession(Session, null);
            if(item.AllowDelete)
                item.Delete();
            SessionHelper.CommitSession(Session, null);
        }
        [TestMethod]
        public void DeleteMovieItemOnRemove() {
            MovieItem item = Avatar.AddItem();
            SessionHelper.CommitSession(Session, null);
            Avatar.Items.Remove(item);
            Assert.IsTrue(item.IsDeleted);
        }
        [TestMethod]
        public void ChangeArtistMovie() {
            XPCollection<MovieArtist> changedObjects = new XPCollection<MovieArtist>(Session, CriteriaOperator.Parse("Movie = ? AND Artist = ?", Cube, JamesCameron));
            Assert.AreEqual(0, changedObjects.Count);
            XPCollection<MovieArtist> col = new XPCollection<MovieArtist>(Session, CriteriaOperator.Parse("Movie = ? AND Artist = ?", Avatar, JamesCameron));
            MovieArtist ma = col[0];
            ma.Movie = Cube;
            SessionHelper.CommitSession(Session, null);
            changedObjects = new XPCollection<MovieArtist>(Session, CriteriaOperator.Parse("Movie = ? AND Artist = ?", Cube, JamesCameron));
            Assert.AreEqual(1, changedObjects.Count);
        }
    }
    [TestClass]
    public class PriceTests : XPOObjectsBaseTests {
        [TestMethod]
        public void MovieItem_CalcOnOrderPrice() {
            MovieCategory newFilms = new MovieCategory(Session);
            MovieCategoryPrice newFilmsOnDVD = newFilms.GetPrice(MovieItemFormat.DVD);
            newFilmsOnDVD.LateRentPrice = 4;
            newFilmsOnDVD.Days1RentPrice = 2;
            newFilmsOnDVD.Days2RentPrice = 1;
            Avatar.Category = newFilms;
            MovieItem avatarItem = Avatar.Items[0];
            decimal onOrderPrice = avatarItem.CalcOnOrderPrice(9);
            Assert.AreEqual(7 * 1 + (9 - 7) * 4, onOrderPrice);
            onOrderPrice = avatarItem.CalcOnOrderPrice(1);
            Assert.AreEqual(1 * 2, onOrderPrice);
        }
        [TestMethod]
        public void MovieItem_CalcRentMaxDaysCount() {
            MovieCategory newFilms = new MovieCategory(Session);
            MovieCategoryPrice newFilmsOnDVD = newFilms.GetPrice(MovieItemFormat.DVD);
            newFilmsOnDVD.LateRentPrice = 1;
            newFilmsOnDVD.Days1RentPrice = 2;
            newFilmsOnDVD.Days2RentPrice = 1;
            Avatar.Category = newFilms;
            MovieItem avatarItem = Avatar.Items[0];
            avatarItem.SellingPrice = 2;
            Assert.AreEqual(0, avatarItem.CalcRentMaxDaysCount());
            avatarItem.SellingPrice = 4;
            Assert.AreEqual(3, avatarItem.CalcRentMaxDaysCount());
            avatarItem.SellingPrice = 10;
            Assert.AreEqual(9, avatarItem.CalcRentMaxDaysCount());
            avatarItem.SellingPrice = 10.5m;
            Assert.AreEqual(10, avatarItem.CalcRentMaxDaysCount());
            avatarItem.AvailableForSell = false;
            Assert.AreEqual(-1, avatarItem.CalcRentMaxDaysCount());
        }
    }
    [TestClass]
    public class XPOCatalogTests : XPOObjectsBaseTests {
        [TestMethod]
        public void ArtistLine_FindDirector() {
            MovieArtistLine line = MovieArtistLine.GetDirector(Session);
            Assert.IsNotNull(line);
        }
        [TestMethod]
        public void FindDirectorForMovie() {
            Assert.AreEqual(Avatar.Director.Artist.FirstName, "James");
            Assert.AreEqual(Postal.Director.Artist.LastName, "Boll");
        }
        [TestMethod]
        public void CountriesAsString() {
            Assert.AreEqual("us", Avatar.CountriesAsString);
            Avatar.CountriesAsString = "us, ru";
            Assert.AreEqual("ru, us", Avatar.CountriesAsString);
        }
        [TestMethod]
        public void AllowDelete() {
            Movie movie = new Movie(Session, "1");
            MovieItem item = movie.AddItem(MovieItemFormat.DVD, 10);
            Andrew.DoRent(new RentInfo(item));
            Session.CommitChanges();
            Assert.IsFalse(movie.AllowDelete);
        }
    }
    [TestClass]
    public class XPOMetaDataHelperTests : XPOObjectsBaseTests {
        static ReflectionDictionary dict;
        UnitOfWork copy;
        public override void Init() {
            base.Init();
            if(dict == null) dict = new ReflectionDictionary();
            copy = new UnitOfWork(new TestDataLayer(dict, DataStoreHelper.CreateInMemoryDataStore()));
        }
        [TestMethod]
        public void CopySession() {
            Postal.CountriesAsString = "ru, us";
            Session.CommitChanges();
            IDGenerator.DisableGeneration(copy);
            SessionHelper.CopySession(Session, copy, Session.Dictionary.CollectClassInfos(typeof(VideoRentBaseObject).Assembly), null, 0);
            copy.CommitChanges();
            IDGenerator.EnableGeneration();
            Movie copyPostal = copy.FindObject<Movie>(CriteriaOperator.Parse("Title = ?", "Postal"));
            Assert.AreEqual("Postal", copyPostal.Title);
            Assert.AreEqual("ru, us", copyPostal.CountriesAsString);
            Assert.IsNotNull(MovieCategory.GetDefaultCategory(copy));
            Assert.AreEqual(MovieCategory.GetDefaultCategory(copy), copyPostal.Category);
            Customer copyAnton = copy.FindObject<Customer>(CriteriaOperator.Parse("FirstName = ? and LastName = ?", "Anton", "Abanin"));
            Assert.AreEqual(3, copyAnton.CustomerId);
        }
        [TestMethod]
        public void SessionHelper_GetObjectByKey() {
            Movie movie = new Movie(Session);
            NestedUnitOfWork nested = Session.BeginNestedUnitOfWork();
            Assert.IsNotNull(SessionHelper.GetObjectByKey<Movie>(movie.Oid, nested));
            nested.CommitChanges();
        }
    }
    [TestClass]
    public class RentsHistoryTests : XPOObjectsBaseTests {
#if !SL
        volatile bool flag = false;
#endif
        bool rentsCountAssert = false;
        bool receiptsCountAssert = false;
        bool rentsIdsAssert = false;
        bool receiptsIdsAssert = false;
        bool rentsDatesAssert = false;
        [TestMethod]
        public void GenerateIDFast() {
            IDGenerator.EnableFastGeneration(Session);
            Movie movie5 = new Movie(Session, "movie");
            Session.CommitChanges();
            Assert.AreEqual(5, movie5.MovieId);
            IDGenerator.DisableFastGeneration();
            Session.CommitChanges();
            Movie movie6 = new Movie(Session, "movie");
            Session.CommitChanges();
            Assert.AreEqual(6, movie6.MovieId);
        }
#if SL
        [Asynchronous]
#endif
        [TestMethod]
        public void Generate() {
            StandartBackgroundWorker backgroundWorker = new StandartBackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
#if SL
            backgroundWorker.RunWorkerCompleted += (s, e) =>
            {
                Assert.IsTrue(rentsCountAssert);
                Assert.IsTrue(receiptsCountAssert);
                Assert.IsTrue(rentsIdsAssert);
                Assert.IsTrue(receiptsIdsAssert);
                Assert.IsTrue(rentsDatesAssert);
                EnqueueTestComplete();
            };
#endif
            new RentsHistory(30, Session, backgroundWorker, null);
#if!SL
            while(!flag)
                ;
            Assert.IsTrue(rentsCountAssert);
            Assert.IsTrue(receiptsCountAssert);
            Assert.IsTrue(rentsIdsAssert);
            Assert.IsTrue(receiptsIdsAssert);
            Assert.IsTrue(rentsDatesAssert);
#endif
        }
        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            XPCollection<Rent> rents = new XPCollection<Rent>(Session);
            XPCollection<Receipt> receipts = new XPCollection<Receipt>(Session);
            rentsIdsAssert = true;
            for(int i = 1; i <= rents.Count; ++i) {
                if(Session.FindObject<Rent>(CriteriaOperator.Parse("RentId = ?", i)) == null) {
                    rentsIdsAssert = false;
                    break;
                }
            }
            receiptsIdsAssert = true;
            for(int i = 1; i <= receipts.Count; ++i) {
                if(Session.FindObject<Receipt>(CriteriaOperator.Parse("ReceiptId = ?", i)) == null) {
                    receiptsIdsAssert = false;
                    break;
                }
            }
            rentsDatesAssert = true;
            foreach(Rent rent in rents) {
                if(rent.ReturnedOn != null && rent.RentedOn.Date == ((DateTime)rent.ReturnedOn).Date) {
                    rentsDatesAssert = false;
                    break;
                }
            }
            rentsCountAssert = rents.Count != 0;
            receiptsCountAssert = receipts.Count != 0;
#if !SL
            flag = true;
#endif
        }
    }
}
#endif
