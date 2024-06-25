#if DebugTest
using System;
using System.Collections.Generic;
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
    public class TestDataLayer : SimpleDataLayer {
        private int selectCount = 0, updateCount = 0;
        List<string> selectsLog = new List<string>();
        List<string> updatesLog = new List<string>();
        bool isLogging = false;

        public TestDataLayer(XPDictionary dict, IDataStore provider) : base(dict, provider) { }
        public override SelectedData SelectData(params SelectStatement[] selects) {
            if(isLogging) {
                selectCount += selects.Length;
                foreach(SelectStatement statement in selects) {
                    selectsLog.Add(statement.ToString());
                }
            }
            return base.SelectData(selects);
        }
        public override ModificationResult ModifyData(params ModificationStatement[] dmlStatements) {
            if(isLogging) {
                updateCount += dmlStatements.Length;
                foreach(ModificationStatement statement in dmlStatements) {
                    updatesLog.Add(statement.ToString());
                }
            }
            return base.ModifyData(dmlStatements);
        }
        public int SelectCount {
            get { return selectCount; }
            set { selectCount = value; }
        }
        public int UpdateCount {
            get { return updateCount; }
            set { updateCount = value; }
        }
        public void StartLogging() {
            selectsLog = new List<string>();
            updatesLog = new List<string>();
            selectCount = 0;
            updateCount = 0;
            isLogging = true;
        }
        public void StopLogging() {
            isLogging = false;
        }
        public string[] SelectsLog {
            get { return selectsLog.ToArray(); }
        }
        public string[] UpdatesLog {
            get { return updatesLog.ToArray(); }
        }
    }
#if SL
    public class XPOObjectsBaseTests : SilverlightTest {
#else
    public class XPOObjectsBaseTests {
#endif
        static ReflectionDictionary dict;
        UnitOfWork session;
        [TestInitialize]
        public virtual void Init() {
            VideoRent.Helpers.VideoRentDateTime.RealTime = false;
            if(dict == null) {
                dict = new ReflectionDictionary();
            }
            XpoDefault.DataLayer = new TestDataLayer(dict, DataStoreHelper.CreateInMemoryDataStore());
            XpoDefault.Session = null;
            this.session = new UnitOfWork();
            CreateData(Session);
        }
        [TestCleanup]
        public virtual void ShutDown() {
            this.session.Disconnect();
            this.session.Dispose();
            this.session = null;
            XpoDefault.DataLayer.Dispose();
            XpoDefault.DataLayer = null;
        }
        protected Artist JamesCameron { get { return GetArtist(Session, "Cameron"); } }
        protected Artist UweBoll { get { return GetArtist(Session, "Boll"); } }
        protected UnitOfWork Session { get { return session; } }
        protected UnitOfWork GetSession() { return Session; }
        protected Customer Andrew { get { return GetCustomer(Session, "Andrew"); } }
        protected Customer Alex { get { return GetCustomer(Session, "Alex"); } }
        protected Customer Anton { get { return GetCustomer(Session, "Anton"); } }
        protected Movie Avatar { get { return GetMovie(Session, "Avatar"); } }
        protected Movie Postal { get { return GetMovie(Session, "Postal"); } }
        protected Movie Cube { get { return GetMovie(Session, "Cube"); } }
        protected Movie Nirvana { get { return GetMovie(Session, "Nirvana"); } }
        protected Company FoxCompany { get { return GetCompany(Session, "Fox"); } }
        protected Customer GetCustomer(Session session, string firstName) {
            return session.FindObject<Customer>(CriteriaOperator.Parse("FirstName = ?", firstName));
        }
        protected Movie GetMovie(Session session, string title) {
            return session.FindObject<Movie>(CriteriaOperator.Parse("Title = ?", title));
        }
        protected Artist GetArtist(Session session, string name) {
            return session.FindObject<Artist>(CriteriaOperator.Parse("LastName = ?", name));
        }
        protected Company GetCompany(UnitOfWork Session, string name) {
            return session.FindObject<Company>(CriteriaOperator.Parse("Name = ?", name));
        }
        protected virtual void CreateData(UnitOfWork session) {
            VideoRentCurrentUser.Login(session, ReferenceData.AdministratorString);
            CreateMovies(session);
            session.CommitChanges();
            CreateCustomers(session);
            session.CommitChanges();
            CreateArtists(session);
            session.CommitChanges();
            CreateCountries(session);
            session.CommitChanges();
            CreateCompanies(session);
            session.CommitChanges();
            FillMoviesProperties(session);
            session.CommitChanges();
        }
        protected virtual void CreateCountries(UnitOfWork session) {
            CreateCountry("USA", "us");
            CreateCountry("Russia", "ru");
        }
        protected void CreateCountry(string name, string acronym) {
            Country country = new Country(Session);
            country.Name = name;
            country.Acronym = acronym;
        }
        protected virtual void CreateCompanies(UnitOfWork session) {
            CompanyType distributor = session.FindObject<CompanyType>(CriteriaOperator.Parse("Name = ?", "Distributors"));
            Company fox = CreateCompany(session, "Fox", distributor);
            Avatar.AddCompany(fox);
        }
        protected Company CreateCompany(UnitOfWork session, string name, CompanyType type) {
            Company company = new Company(session);
            company.Name = name;
            company.Type = type;
            return company;
        }
        protected Artist CreateArtist(UnitOfWork session, string firstName, string lastName) {
            return new Artist(session, firstName, lastName);
        }
        protected virtual void CreateArtists(UnitOfWork session) {
            Artist artist = null;
            MovieArtistLine line = MovieArtistLine.GetDirector(session);
            artist = CreateArtist(session, "James", "Cameron");
            Avatar.AddArtist(artist, line);
            artist = CreateArtist(session, "Uwe", "Boll");
            Postal.AddArtist(artist, MovieArtistLine.GetDirector(session));
        }

        protected virtual void CreateCustomers(UnitOfWork session) {
            CreateCustomer(session, "Andrew", "Telnov");
            CreateCustomer(session, "Alex", "Abidarov");
            CreateCustomer(session, "Anton", "Abanin");
        }

        protected Customer CreateCustomer(UnitOfWork session, string firstName, string lastName) {
            return new Customer(session, firstName, lastName);
        }
        protected virtual void CreateMovies(UnitOfWork session) {
            CreateMovie(session, "Avatar", 1);
            CreateMovie(session, "Postal", 2);
            CreateMovie(session, "Cube", 3);
            CreateMovie(session, "Nirvana", 4);
        }
        protected virtual void FillMoviesProperties(UnitOfWork session) {
            Movie movie = Avatar;
            movie.AspectRatio = "1.78 : 1";
            movie.Awards = "Nominated";
            movie.CountriesAsString = "us";
            movie.Genre = MovieGenre.Action | MovieGenre.Adventure | MovieGenre.Fantasy;
            movie.Language = session.FindObject<Language>(CriteriaOperator.Parse("Name = ?", "English"));
            movie.Plot = "Avatar Plot";
            movie.ReleaseDate = new DateTime(2009, 12, 17);
            movie.RunTime = new TimeSpan(0, 162, 0);
            movie.Tagline = "Avatar Tagline";
            movie.WebSite = "http://www.avatarmovie.com/";
        }
        void CreateMovie(UnitOfWork session, string title, int quanity) {
            Movie movie = new Movie(session, title);
            for(int i = 0; i < quanity; i++) {
                movie.AddItem(MovieItemFormat.DVD, 100);
            }
        }
    }
}
#endif
