using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieGenreEditData : BindingAndDisposable {
        List<MovieGenre> list;

        public MovieGenreEditData() {
            List = ItemsSourceHelper.GenresList;
        }
        public string NullValueText { get { return EnumTitlesKeeper<MovieGenre>.GetTitle(MovieGenre.None); } }
        public List<MovieGenre> List {
            get { return list; }
            private set { SetValue<List<MovieGenre>>("List", ref list, value); }
        }
    }
    public class MovieRatingEditData : BindingAndDisposable {
        List<RatingItem> list;

        public MovieRatingEditData() {
#if SL
            ItemsSourceHelper.RequestRatingItems((result) => List = result);
#else
            List = ItemsSourceHelper.RatingsList;
#endif
        }
        public List<RatingItem> List {
            get { return list; }
            private set { SetValue<List<RatingItem>>("List", ref list, value); }
        }
    }
    public class MovieItemFormatEditData : BindingAndDisposable {
        List<FormatItem> list;

        public MovieItemFormatEditData() {
#if SL
            ItemsSourceHelper.RequestFormatItems((result) => List = result);
#else
            List = ItemsSourceHelper.FormatsList;
#endif
        }
        public List<FormatItem> List {
            get { return list; }
            private set { SetValue<List<FormatItem>>("List", ref list, value); }
        }
    }
    public class PersonGenderEditData : BindingAndDisposable {
        List<GenderItem> list;

        public PersonGenderEditData() {
#if SL
            ItemsSourceHelper.RequestGenderItems((result) => List = result);
#else
            List = ItemsSourceHelper.GendersList;
#endif
        }
        public List<GenderItem> List {
            get { return list; }
            private set { SetValue<List<GenderItem>>("List", ref list, value); }
        }
    }
    public class VideoRentObjectEditData<T> : BindingAndDisposable where T : VideoRentBaseObject {
        AllObjects<T> list;

        public VideoRentObjectEditData() { }
        public AllObjects<T> List {
            get { return list; }
            protected set { SetValue<AllObjects<T>>("List", ref list, value, true); }
        }
        protected override void DisposeManaged() {
            List = null;
            base.DisposeManaged();
        }
    }
    public class CountryEditData : VideoRentObjectEditData<Country> {
        public CountryEditData(Session session) {
            List = new AllObjects<Country>(session, null, new SortProperty("[Name]", SortingDirection.Ascending));
        }
    }
    public class MovieEditData : VideoRentObjectEditData<Movie> {
        public MovieEditData(Session session) {
            List = new AllObjects<Movie>(session, null, new SortProperty("[Title]", SortingDirection.Ascending));
        }
    }
    public class MovieArtistLineEditData : VideoRentObjectEditData<MovieArtistLine> {
        public MovieArtistLineEditData(Session session) {
            List = new AllObjects<MovieArtistLine>(session, null, new SortProperty("[Name]", SortingDirection.Ascending));
        }
    }
    public class CompanyTypeEditData : VideoRentObjectEditData<CompanyType> {
        public CompanyTypeEditData(Session session) {
            List = new AllObjects<CompanyType>(session, null, new SortProperty("[Name]", SortingDirection.Ascending));
        }
    }
    public class ArtistEditData : VideoRentObjectEditData<Artist> {
        public ArtistEditData(Session session) {
            List = new AllObjects<Artist>(session, null, new SortProperty("[FirstName]", SortingDirection.Ascending));
        }
    }
    public class CompanyEditData : VideoRentObjectEditData<Company> {
        public CompanyEditData(Session session) {
            List = new AllObjects<Company>(session, null, new SortProperty("[Name]", SortingDirection.Ascending));
        }
    }
    public class LanguageEditData : VideoRentObjectEditData<Language> {
        public LanguageEditData(Session session) {
            List = new AllObjects<Language>(session, null, new SortProperty("[Name]", SortingDirection.Ascending));
        }
    }
    public class MovieCategoryEditData : VideoRentObjectEditData<MovieCategory> {
        public MovieCategoryEditData(Session session) : this(session, null) { }
        public MovieCategoryEditData(Session session, Guid? excludedObjectOid) {
            MovieCategory excludedObject = excludedObjectOid == null ? null : SessionHelper.GetObjectByKey<MovieCategory>((Guid)excludedObjectOid, session);
            List = new AllObjects<MovieCategory>(excludedObject, session, null, new SortProperty("[Name]", SortingDirection.Ascending));
        }
    }
    public class DiscountLevelEditData : BindingAndDisposable {
        IList<EnumItem> list;

        public DiscountLevelEditData() {
            List = EnumTitlesKeeper<CustomerDiscountLevel>.GetItemsList<EnumItem>();
        }
        public IList<EnumItem> List {
            get { return list; }
            private set { SetValue<IList<EnumItem>>("List", ref list, value); }
        }
    }
    public class RentsPeriodEditData : BindingAndDisposable {
        IList<PeriodItem> list;

        public RentsPeriodEditData() {
            List = ItemsSourceHelper.PeriodsList;
        }
        public IList<PeriodItem> List {
            get { return list; }
            private set { SetValue<IList<PeriodItem>>("List", ref list, value); }
        }
    }
    public class ReceiptTypeItem {
        public ReceiptType Value { get; set; }
        public string Name { get; set; }
        public ImageSource Icon { get; set; }
#if SL
        public ReceiptTypeItem(ReceiptType value, ImageSource icon) {
            Value = value;
            Name = EnumTitlesKeeper<ReceiptType>.GetTitle(Value);
            Icon = icon;
        }
#else
        public ReceiptTypeItem(ReceiptType value) {
            Value = value;
            Name = EnumTitlesKeeper<ReceiptType>.GetTitle(Value);
            Icon = DevExpress.VideoRent.Resources.ImagesSourceHelper.Current.ReceiptTypes[(int)Value];
        }
#endif
    }
    public class ReceiptTypeEditData : BindingAndDisposable {
        static IList<ReceiptTypeItem> list;
        static ReceiptTypeEditData() {
            list = new List<ReceiptTypeItem>();
#if SL
            ImagesSourceHelper.Current.RequestReceiptTypeIcons((imagesList) =>
            {
                ReceiptType[] types = EnumHelper.GetValues<ReceiptType>();
                for(int i = 0; i < types.Length; ++i) {
                    list.Add(new ReceiptTypeItem(types[i], imagesList[i]));
                }
            });
#else
            foreach(ReceiptType recieptType in EnumHelper.GetValues<ReceiptType>()) {
                list.Add(new ReceiptTypeItem(recieptType));
            }
#endif
        }

        public IList<ReceiptTypeItem> List { get { return list; } }
    }
    public class ChartPaletteEditData : BindingAndDisposable {
        public IList<ChartPalette> List { get { return ChartPredefinedValuesProvider.PredefinedPalettes; } }
    }
    public class ChartMarker2DKindEditData : BindingAndDisposable {
        static List<ChartMarker2DKind> list;
        static ChartMarker2DKindEditData() {
            list = new List<ChartMarker2DKind>();
            list.Add(new ChartMarker2DKind(ConstStrings.Get("None"), null));
            list.AddRange(ChartPredefinedValuesProvider.PredefinedMarker2DKinds);
        }

        public IList<ChartMarker2DKind> List { get { return list; } }
    }
}
