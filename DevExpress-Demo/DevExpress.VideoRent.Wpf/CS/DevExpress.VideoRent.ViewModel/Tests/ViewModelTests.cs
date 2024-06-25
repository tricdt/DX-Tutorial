#if DebugTest
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.Tests;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
#if SL
using DevExpress.Xpf.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
using TestInitializeAttribute = NUnit.Framework.SetUpAttribute;
using TestCleanupAttribute = NUnit.Framework.TearDownAttribute;
using NUnit.Framework;
#endif

namespace DevExpress.VideoRent.ViewModel.Tests {
    public class ViewModelTests : XPOObjectsBaseTests {
        public override void Init() {
            base.Init();
            RegisterViews();
            RegisterModules();
        }
        protected TestMessageBoxView TestMessageBoxView { get; private set; }
        protected TestOpenFileDialogView TestOpenFileDialogView { get; private set; }
        protected TestChartPredefinedValuesProviderView TestChartPredefinedValuesProviderView { get; private set; }
        protected TestMouseView TestMouseView { get; private set; }
        void RegisterModules() {
            ModulesManager.Reset();
            Registration.Register();
        }
        void RegisterViews() {
            MessageBox.View = TestMessageBoxView = new TestMessageBoxView();
            OpenFileDialog.View = TestOpenFileDialogView = new TestOpenFileDialogView();
            ChartPredefinedValuesProvider.View = TestChartPredefinedValuesProviderView = new TestChartPredefinedValuesProviderView();
            Mouse.View = TestMouseView = new TestMouseView();
            if(ViewsManager.Current != null) return;
            TestViewsManager viewsManager = new TestViewsManager();
            viewsManager.RegisterView(typeof(MovieEdit), typeof(MovieEditView));
            viewsManager.RegisterView(typeof(MovieAddArtistEdit), typeof(MovieAddArtistEditView));
            viewsManager.RegisterView(typeof(MoviesEdit), typeof(MoviesEditView));
            viewsManager.RegisterView(typeof(MoviePicturesEdit), typeof(MoviePicturesEditView));
            viewsManager.RegisterView(typeof(MovieDetail), typeof(MovieDetailView));
            viewsManager.RegisterView(typeof(MoviesList), typeof(MoviesListView));
            viewsManager.RegisterView(typeof(MovieCategoryPriceEdit), typeof(MovieCategoryPriceEditView));
            viewsManager.RegisterView(typeof(MovieCategoryEdit), typeof(MovieCategoryEditView));
            viewsManager.RegisterView(typeof(MovieCategoriesEdit), typeof(MovieCategoriesEditView));
            viewsManager.RegisterView(typeof(MovieCategoryDetail), typeof(MovieCategoryDetailView));
            viewsManager.RegisterView(typeof(MovieCategoriesList), typeof(MovieCategoriesListView));
            viewsManager.RegisterView(typeof(ArtistEdit), typeof(ArtistEditView));
            viewsManager.RegisterView(typeof(ArtistsEdit), typeof(ArtistsEditView));
            viewsManager.RegisterView(typeof(ArtistPicturesEdit), typeof(ArtistPicturesEditView));
            viewsManager.RegisterView(typeof(ArtistDetail), typeof(ArtistDetailView));
            viewsManager.RegisterView(typeof(ArtistsList), typeof(ArtistsListView));
            viewsManager.RegisterView(typeof(CompanyEdit), typeof(CompanyEditView));
            viewsManager.RegisterView(typeof(CompaniesEdit), typeof(CompaniesEditView));
            viewsManager.RegisterView(typeof(CompanyMoviesEdit), typeof(CompanyMoviesEditView));
            viewsManager.RegisterView(typeof(CompanyDetail), typeof(CompanyDetailView));
            viewsManager.RegisterView(typeof(CompaniesList), typeof(CompaniesListView));
            viewsManager.RegisterView(typeof(CustomersEdit), typeof(CustomersEditView));
            viewsManager.RegisterView(typeof(CustomersList), typeof(CustomersListView));
            viewsManager.RegisterView(typeof(CustomerEdit), typeof(CustomerEditView));
            viewsManager.RegisterView(typeof(CustomerDetail), typeof(CustomerDetailView));
            viewsManager.RegisterView(typeof(CurrentCustomerRentsEdit), typeof(CurrentCustomerRentsEditView));
            viewsManager.RegisterView(typeof(CurrentCustomerRentsDetail), typeof(CurrentCustomerRentsDetailView));
        }
    }
    [TestClass]
    public class EditableObjectTests : ViewModelTests {
        string message;

        [TestMethod]
        public void TryDeleteDenyObject_CheckInfoMessage() {
            Assert.IsNotNull(Anton.DoRent(new RentInfo(Avatar, MovieItemFormat.DVD)));
            SessionHelper.CommitSession(Session, null);
            Assert.IsFalse(Avatar.AllowDelete);
            MoviesList moviesList = (MoviesList)ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false);
            moviesList.MoviesEdit.CurrentRecord = SessionHelper.GetObject<Movie>(Avatar, moviesList.MoviesEdit.VRObjectsEditObject.VideoRentObjects.Session);
            TestMessageBoxView.ShowDelegate = MessageBoxShow;
            Assert.IsFalse(moviesList.MoviesEdit.DeleteCurrentRecord());
            Assert.AreEqual(ConstStrings.Get("ObjectCanNotBeDeleted"), message);
            TestMessageBoxView.ShowDelegate = null;
            moviesList.Dispose();
        }
        MessageBoxResult MessageBoxShow(string message, string title, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult) {
            this.message = message;
            return MessageBoxResult.OK;
        }
    }
    [TestClass]
    public class LayoutManagerTests : ViewModelTests {
        [TestMethod]
        public void SaveLayoutData() {
            Assert.IsTrue(LayoutManager.Current.Login(ReferenceData.AdministratorString, string.Empty, Session));
            TestLayoutData1.GetLayoutData().Data1 = 1;
            TestLayoutData2.GetLayoutData().Data2 = 2;
            LayoutManager.Current.Logout();
            Assert.IsTrue(LayoutManager.Current.Login(ReferenceData.AdministratorString, string.Empty, Session));
            Assert.AreEqual(1, TestLayoutData1.GetLayoutData().Data1);
            Assert.AreEqual(2, TestLayoutData2.GetLayoutData().Data2);
            LayoutManager.Current.Logout();
        }
    }
    public class TestLayoutData1 : LayoutData {
        public static TestLayoutData1 GetLayoutData() {
            XPCollection<TestLayoutData1> layoutDataCollection = LayoutManager.Current.GetLayoutData<TestLayoutData1>();
            if(layoutDataCollection.Count != 0) return layoutDataCollection[0];
            TestLayoutData1 layoutData = new TestLayoutData1(VideoRentCurrentUser.GetCurrentUser(LayoutManager.Current.Session));
            LayoutManager.Current.LayoutData.Add(layoutData);
            return layoutData;
        }
        public TestLayoutData1(Session session) : base(session) { }
        public TestLayoutData1(Employee employee)
            : base(employee.Session) {
            Employee = employee;
        }
        public int Data1 {
            get { return GetPropertyValue<int>("Data1"); }
            set { SetPropertyValue<int>("Data1", value); }
        }
    }
    public class TestLayoutData2 : LayoutData {
        public static TestLayoutData2 GetLayoutData() {
            XPCollection<TestLayoutData2> layoutDataCollection = LayoutManager.Current.GetLayoutData<TestLayoutData2>();
            if(layoutDataCollection.Count != 0) return layoutDataCollection[0];
            TestLayoutData2 layoutData = new TestLayoutData2(VideoRentCurrentUser.GetCurrentUser(LayoutManager.Current.Session));
            LayoutManager.Current.LayoutData.Add(layoutData);
            return layoutData;
        }
        public TestLayoutData2(Session session) : base(session) { }
        public TestLayoutData2(Employee employee)
            : base(employee.Session) {
            Employee = employee;
        }
        public int Data2 {
            get { return GetPropertyValue<int>("Data2"); }
            set { SetPropertyValue<int>("Data2", value); }
        }
    }
    public delegate MessageBoxResult MessageBoxShowDelegate(string message, string title, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult);
    public class TestMessageBoxView : IMessageBoxView {
        public MessageBoxShowDelegate ShowDelegate { get; set; }
        public MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult) {
            if(ShowDelegate == null) return defaultResult;
            return ShowDelegate(message, title, button, image, defaultResult);
        }
    }
    public delegate Stream OpenFileDelegate();
    public delegate Image OpenImageDelegate();
    public class TestOpenFileDialogView : IOpenFileDialogView {
        public OpenFileDelegate OpenFileDelegate { get; set; }
        public OpenImageDelegate OpenImageDelegate { get; set; }
        public Stream OpenFile() {
            return OpenFileDelegate == null ? null : OpenFileDelegate();
        }
        public Image OpenImage() {
            return OpenImageDelegate == null ? null : OpenImageDelegate();
        }
    }
    public class TestChartPredefinedValuesProviderView : IChartPredefinedValuesProviderView {
        static List<ChartPalette> predefinedPalettes;
        static List<ChartMarker2DKind> predefinedMarker2DKinds;
        static TestChartPredefinedValuesProviderView() {
            predefinedPalettes = new List<ChartPalette>() { new ChartPalette("DXCharts", null), new ChartPalette("Office", null) };
            predefinedMarker2DKinds = new List<ChartMarker2DKind>() { new ChartMarker2DKind("Triangle", null), new ChartMarker2DKind("Dollar", null) };
        }

        public IList<ChartPalette> PredefinedPalettes { get { return predefinedPalettes; } }
        public IList<ChartMarker2DKind> PredefinedMarker2DKinds { get { return predefinedMarker2DKinds; } }
    }
    public class TestMouseView : IMouseView {
        CursorType cursorType = CursorType.Arrow;

        public CursorType CursorType {
            get { return cursorType; }
            set { cursorType = value; }
        }
        public void WaitIdle() { }
    }
    public class TestEditableObject : EditableObject {
        Guid oid;

        public TestEditableObject(Guid oid)
            : base(null) {
            this.oid = oid;
        }
        public override object Key { get { return oid; } }
        public override bool Dirty { get { return false; } }
        protected override void SaveOverride() { }
        protected override void ReloadBegin() { }
        protected override void ReloadEnd() { }
    }
    public abstract class TestView {
        ViewModelModule module;

        public TestView(ViewModelModule module) {
            this.module = module;
        }
        public ViewModelModule Module { get { return module; } }
    }
    public class TestViewsManager : ViewsManager {
        public TestViewsManager() {
            ViewsManager.Current = this;
        }
        Dictionary<Type, Type> viewsTypes = new Dictionary<Type, Type>();
        public void RegisterView(Type viewModelModuleType, Type viewType) {
            viewsTypes.Add(viewModelModuleType, viewType);
        }
        public override object CreateView(ViewModelModule module) {
            Type viewModelModuleType = module.GetType();
            Type viewType = viewsTypes[viewModelModuleType];
            return viewType.GetConstructor(new Type[] { viewModelModuleType }).Invoke(new object[] { module });
        }
    }
    public class MovieEditView : TestView {
        public MovieEditView(MovieEdit movieEdit) : base(movieEdit) { }
        public new MovieEdit Module { get { return (MovieEdit)base.Module; } }
    }
    public class MovieAddArtistEditView : TestView {
        public MovieAddArtistEditView(MovieAddArtistEdit movieAddArtistEdit) : base(movieAddArtistEdit) { }
        public new MovieAddArtistEdit Module { get { return (MovieAddArtistEdit)base.Module; } }
    }
    public class MoviesEditView : TestView {
        public MoviesEditView(MoviesEdit moviesEdit) : base(moviesEdit) { }
        public new MoviesEdit Module { get { return (MoviesEdit)base.Module; } }
    }
    public class MoviePicturesEditView : TestView {
        public MoviePicturesEditView(MoviePicturesEdit moviePicturesEdit) : base(moviePicturesEdit) { }
        public new MoviePicturesEdit Module { get { return (MoviePicturesEdit)base.Module; } }
    }
    public class MovieDetailView : TestView {
        public static MovieDetailView LastCreatedView = null;
        public MovieDetailView(MovieDetail movieDetail)
            : base(movieDetail) {
            LastCreatedView = this;
        }
        public new MovieDetail Module { get { return (MovieDetail)base.Module; } }
    }
    public class MoviesListView : TestView {
        public static MoviesListView LastCreatedView = null;
        public MoviesListView(MoviesList moviesList)
            : base(moviesList) {
            LastCreatedView = this;
        }
        public new MoviesList Module { get { return (MoviesList)base.Module; } }
    }
    public class ArtistEditView : TestView {
        public ArtistEditView(ArtistEdit artistEdit) : base(artistEdit) { }
        public new ArtistEdit Module { get { return (ArtistEdit)base.Module; } }
    }
    public class ArtistAddMovieEditView : TestView {
        public ArtistAddMovieEditView(ArtistEdit artistEdit) : base(artistEdit) { }
        public new ArtistAddMovieEdit Module { get { return (ArtistAddMovieEdit)base.Module; } }
    }
    public class ArtistsEditView : TestView {
        public ArtistsEditView(ArtistsEdit artistsEdit) : base(artistsEdit) { }
        public new ArtistsEdit Module { get { return (ArtistsEdit)base.Module; } }
    }
    public class ArtistPicturesEditView : TestView {
        public ArtistPicturesEditView(ArtistPicturesEdit artistPicturesEdit) : base(artistPicturesEdit) { }
        public new ArtistPicturesEdit Module { get { return (ArtistPicturesEdit)base.Module; } }
    }
    public class ArtistDetailView : TestView {
        public static ArtistDetailView LastCreatedView = null;
        public ArtistDetailView(ArtistDetail artistDetail)
            : base(artistDetail) {
            LastCreatedView = this;
        }
        public new ArtistDetail Module { get { return (ArtistDetail)base.Module; } }
    }
    public class ArtistsListView : TestView {
        public static ArtistsListView LastCreatedView = null;

        public ArtistsListView(ArtistsList moviesList)
            : base(moviesList) {
            LastCreatedView = this;
        }
        public new ArtistsList Module { get { return (ArtistsList)base.Module; } }
    }
    public class CompanyEditView : TestView {
        public CompanyEditView(CompanyEdit artistEdit) : base(artistEdit) { }
        public new CompanyEdit Module { get { return (CompanyEdit)base.Module; } }
    }
    public class CompaniesEditView : TestView {
        public CompaniesEditView(CompaniesEdit artistsEdit) : base(artistsEdit) { }
        public new CompaniesEdit Module { get { return (CompaniesEdit)base.Module; } }
    }
    public class CompanyMoviesEditView : TestView {
        public CompanyMoviesEditView(CompanyMoviesEdit edit) : base(edit) { }
        public new CompanyMoviesEdit Module { get { return (CompanyMoviesEdit)base.Module; } }
    }
    public class CompanyDetailView : TestView {
        public static CompanyDetailView LastCreatedView = null;
        public CompanyDetailView(CompanyDetail artistDetail)
            : base(artistDetail) {
            LastCreatedView = this;
        }
        public new CompanyDetail Module { get { return (CompanyDetail)base.Module; } }
    }
    public class CompaniesListView : TestView {
        public static CompaniesListView LastCreatedView = null;

        public CompaniesListView(CompaniesList moviesList)
            : base(moviesList) {
            LastCreatedView = this;
        }
        public new CompaniesList Module { get { return (CompaniesList)base.Module; } }
    }
    public class MovieCategoryPriceEditView : TestView {
        public MovieCategoryPriceEditView(MovieCategoryPriceEdit movieCategoryPriceEdit) : base(movieCategoryPriceEdit) { }
        public new MovieCategoryPriceEdit Module { get { return (MovieCategoryPriceEdit)base.Module; } }
    }
    public class MovieCategoryEditView : TestView {
        public MovieCategoryEditView(MovieCategoryEdit movieCategoryEdit) : base(movieCategoryEdit) { }
        public new MovieCategoryEdit Module { get { return (MovieCategoryEdit)base.Module; } }
    }
    public class MovieCategoriesEditView : TestView {
        public MovieCategoriesEditView(MovieCategoriesEdit movieCategoriesEdit) : base(movieCategoriesEdit) { }
        public new MovieCategoriesEdit Module { get { return (MovieCategoriesEdit)base.Module; } }
    }
    public class MovieCategoryDetailView : TestView {
        public static MovieCategoryDetailView LastCreatedView = null;
        public MovieCategoryDetailView(MovieCategoryDetail movieDetail)
            : base(movieDetail) {
            LastCreatedView = this;
        }
        public new MovieCategoryDetail Module { get { return (MovieCategoryDetail)base.Module; } }
    }
    public class MovieCategoriesListView : TestView {
        public static MovieCategoriesListView LastCreatedView = null;
        public MovieCategoriesListView(MovieCategoriesList moviesList)
            : base(moviesList) {
            LastCreatedView = this;
        }
        public new MovieCategoriesList Module { get { return (MovieCategoriesList)base.Module; } }
    }
    public class CustomersEditView : TestView {
        public CustomersEditView(CustomersEdit customersEdit) : base(customersEdit) { }
        public new CustomersEdit Module { get { return (CustomersEdit)base.Module; } }
    }
    public class CustomersListView : TestView {
        public static CustomersListView LastCreatedView = null;
        public CustomersListView(CustomersList customersList)
            : base(customersList) {
            LastCreatedView = this;
        }
        public new CustomersList Module { get { return (CustomersList)base.Module; } }
    }
    public class CustomerEditView : TestView {
        public static CustomerEditView LastCreatedView = null;
        public CustomerEditView(CustomerEdit customerEdit)
            : base(customerEdit) {
            LastCreatedView = this;
        }
        public new CustomerEdit Module { get { return (CustomerEdit)base.Module; } }
    }
    public class CustomerDetailView : TestView {
        public static CustomerDetailView LastCreatedView = null;
        public CustomerDetailView(CustomerDetail customerDetail)
            : base(customerDetail) {
            LastCreatedView = this;
        }
        public new CustomerDetail Module { get { return (CustomerDetail)base.Module; } }
    }
    public class CurrentCustomerRentsEditView : TestView {
        public CurrentCustomerRentsEditView(CurrentCustomerRentsEdit currentCustomerRentsEdit) : base(currentCustomerRentsEdit) { }
        public new CurrentCustomerRentsEdit Module { get { return (CurrentCustomerRentsEdit)base.Module; } }
    }
    public class CurrentCustomerRentsDetailView : TestView {
        public static CurrentCustomerRentsDetailView LastCreatedView = null;
        public CurrentCustomerRentsDetailView(CurrentCustomerRentsDetail customersList)
            : base(customersList) {
            LastCreatedView = this;
        }
        public new CurrentCustomerRentsDetail Module { get { return (CurrentCustomerRentsDetail)base.Module; } }
    }
}
#endif
