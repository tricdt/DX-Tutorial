using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace DevExpress.VideoRent.ViewModel {
    public enum GridView { TableView, CardView }
    public class GridUIOptions : VideoRentBaseObject {
        bool allowPerPixelScrolling;
        bool allowCascadeUpdate;
        bool allowEditing;
        bool showTheAutoFilterRow;
        bool showVerticalLines;
        bool showIndicator;

        public GridUIOptions(Session session) : base(session) { }
        public override void AfterConstruction() {
            base.AfterConstruction();
            AllowPerPixelScrolling = true;
            AllowCascadeUpdate = true;
            AllowEditing = false;
            ShowTheAutoFilterRow = true;
            ShowVerticalLines = false;
        }
        public bool AllowPerPixelScrolling {
            get { return allowPerPixelScrolling; }
            set { SetPropertyValue<bool>("AllowPerPixelScrolling", ref allowPerPixelScrolling, value); }
        }
        public bool AllowCascadeUpdate {
            get { return allowCascadeUpdate; }
            set { SetPropertyValue<bool>("AllowCascadeUpdate", ref allowCascadeUpdate, value); }
        }
        public bool AllowEditing {
            get { return allowEditing; }
            set { SetPropertyValue<bool>("AllowEditing", ref allowEditing, value); }
        }
        public bool ShowTheAutoFilterRow {
            get { return showTheAutoFilterRow; }
            set { SetPropertyValue<bool>("ShowTheAutoFilterRow", ref showTheAutoFilterRow, value); }
        }
        public bool ShowVerticalLines {
            get { return showVerticalLines; }
            set { SetPropertyValue<bool>("ShowVerticalLines", ref showVerticalLines, value); }
        }
        public bool ShowIndicator {
            get { return showIndicator; }
            set { SetPropertyValue<bool>("ShowIndicator", ref showIndicator, value); }
        }
    }
    public class ViewModelLayoutData : LayoutData {
        public static ViewModelLayoutData GetLayoutData() {
            XPCollection<ViewModelLayoutData> layoutDataCollection = LayoutManager.Current.GetLayoutData<ViewModelLayoutData>();
            if(layoutDataCollection.Count != 0) return layoutDataCollection[0];
            ViewModelLayoutData layoutData = new ViewModelLayoutData(VideoRentCurrentUser.GetCurrentUser(LayoutManager.Current.Session));
            LayoutManager.Current.LayoutData.Add(layoutData);
            return layoutData;
        }

        Guid? currentCustomerOid;
        GridView moviesEditGridView;
        GridUIOptions moviesEditGridUIOptions;
        GridUIOptions artistsEditGridUIOptions;
        GridUIOptions customersEditGridUIOptions;
        GridUIOptions companiesEditGridUIOptions;
        bool currentCustomerRentsEditShowViewCaption;
        GridUIOptions currentCustomerRentsEditGridUIOptions;
        string movieCategoriesEditChartPaletteName;
        int movieCategoriesEditChartMarkerSize;
        double movieCategoriesEditChartMarkerAngle;
        string movieCategoriesEditChartMarkerKindName;

        public ViewModelLayoutData(Session session) : base(session) { }
        public ViewModelLayoutData(Employee employee)
            : this(employee.Session) {
            Employee = employee;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            CurrentCustomerOid = RetrieveDefaultCustomerOid();
            MoviesEditGridView = GridView.TableView;
            MoviesEditGridUIOptions = new GridUIOptions(Session);
            ArtistsEditGridUIOptions = new GridUIOptions(Session);
            CompaniesEditGridUIOptions = new GridUIOptions(Session);
            CustomersEditGridUIOptions = new GridUIOptions(Session);
            CurrentCustomerRentsEditShowViewCaption = true;
            CurrentCustomerRentsEditGridUIOptions = new GridUIOptions(Session);
            CurrentCustomerRentsEditGridUIOptions.ShowVerticalLines = true;
            MovieCategoriesEditChartPaletteName = "Office";
            MovieCategoriesEditChartMarkerSize = 15;
            MovieCategoriesEditChartMarkerAngle = 70.0;
            MovieCategoriesEditChartMarkerKindName = "Cross";
        }
        public Guid? CurrentCustomerOid {
            get { return currentCustomerOid; }
            set { SetPropertyValue<Guid?>("CurrentCustomerOid", ref currentCustomerOid, value); }
        }
        public GridView MoviesEditGridView {
            get { return moviesEditGridView; }
            set { SetPropertyValue<GridView>("MoviesEditGridView", ref moviesEditGridView, value); }
        }
        public GridUIOptions MoviesEditGridUIOptions {
            get { return moviesEditGridUIOptions; }
            set { SetPropertyValue<GridUIOptions>("MoviesEditGridUIOptions", ref moviesEditGridUIOptions, value); }
        }
        public GridUIOptions ArtistsEditGridUIOptions {
            get { return artistsEditGridUIOptions; }
            set { SetPropertyValue<GridUIOptions>("ArtistsEditGridUIOptions", ref artistsEditGridUIOptions, value); }
        }
        public GridUIOptions CustomersEditGridUIOptions {
            get { return customersEditGridUIOptions; }
            set { SetPropertyValue<GridUIOptions>("CustomersEditGridUIOptions", ref customersEditGridUIOptions, value); }
        }
        public GridUIOptions CompaniesEditGridUIOptions {
            get { return companiesEditGridUIOptions; }
            set { SetPropertyValue<GridUIOptions>("CompaniesEditGridUIOptions", ref companiesEditGridUIOptions, value); }
        }
        public bool CurrentCustomerRentsEditShowViewCaption {
            get { return currentCustomerRentsEditShowViewCaption; }
            set { SetPropertyValue<bool>("CurrentCustomerRentsEditShowViewCaption", ref currentCustomerRentsEditShowViewCaption, value); }
        }
        public GridUIOptions CurrentCustomerRentsEditGridUIOptions {
            get { return currentCustomerRentsEditGridUIOptions; }
            set { SetPropertyValue<GridUIOptions>("CurrentCustomerRentsEditGridUIOptions", ref currentCustomerRentsEditGridUIOptions, value); }
        }
        public string MovieCategoriesEditChartPaletteName {
            get { return movieCategoriesEditChartPaletteName; }
            set { SetPropertyValue<string>("MovieCategoriesEditChartPaletteName", ref movieCategoriesEditChartPaletteName, value); }
        }
        public double MovieCategoriesEditChartMarkerAngle {
            get { return movieCategoriesEditChartMarkerAngle; }
            set { SetPropertyValue<double>("MovieCategoriesEditChartMarkerAngle", ref movieCategoriesEditChartMarkerAngle, value); }
        }
        public int MovieCategoriesEditChartMarkerSize {
            get { return movieCategoriesEditChartMarkerSize; }
            set { SetPropertyValue<int>("MovieCategoriesEditChartMarkerSize", ref movieCategoriesEditChartMarkerSize, value); }
        }
        public string MovieCategoriesEditChartMarkerKindName {
            get { return movieCategoriesEditChartMarkerKindName; }
            set { SetPropertyValue<string>("MovieCategoriesEditChartMarkerKindName", ref movieCategoriesEditChartMarkerKindName, value); }
        }
        public Guid RetrieveDefaultCustomerOid() {
            Customer defaultCustomer = Session.FindObject<Customer>(CriteriaOperator.Parse("[FirstName] = 'Essie'"));
            if(defaultCustomer == null)
                defaultCustomer = Session.FindObject<Customer>(null);
            return defaultCustomer == null ? Guid.Empty : defaultCustomer.Oid;
        }
    }
}
