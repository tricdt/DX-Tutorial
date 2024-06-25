using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

namespace DevExpress.VideoRent.ViewModel {
    public class ItemsSourceHelperBase {
        static List<MovieGenre> genresList;
        static List<PeriodItem> periodsList;
        static List<int> predefinedPeriods;
        public static List<MovieGenre> GenresList {
            get {
                if(genresList != null) return genresList;
                genresList = GenerateMovieGenresSource();
                return genresList;
            }
            set { genresList = value; }
        }
        public static List<PeriodItem> PeriodsList {
            get {
                if(periodsList != null) return periodsList;
                periodsList = GeneratePeriodsList();
                return periodsList;
            }
            set { periodsList = value; }
        }
        public static List<int> PredefinedPeriods {
            get {
                if(predefinedPeriods == null) {
                    predefinedPeriods = new List<int>(new int[] { 0, 1, 3, 6, 12, 24, 60, 120 });
                }
                return predefinedPeriods;
            }
        }
        static List<MovieGenre> GenerateMovieGenresSource() {
            List<MovieGenre> list = new List<MovieGenre>();
            MovieGenre[] genreValues = EnumHelper.GetValues<MovieGenre>();
            foreach(MovieGenre flag in genreValues) {
                if((int)flag != 0) list.Add(flag);
            }
            return list.Count == 0 ? null : list;
        }
        static List<PeriodItem> GeneratePeriodsList() {
            List<PeriodItem> list = new List<PeriodItem>();
            list.Add(new PeriodItem(ConstStrings.Get("PeriodCustom"), PredefinedPeriods[0]));
            list.Add(new PeriodItem(ConstStrings.Get("PeriodMonth"), PredefinedPeriods[1]));
            list.Add(new PeriodItem(ConstStrings.Get("PeriodThreeMonths"), PredefinedPeriods[2]));
            list.Add(new PeriodItem(ConstStrings.Get("PeriodSixMonths"), PredefinedPeriods[3]));
            list.Add(new PeriodItem(ConstStrings.Get("PeriodYear"), PredefinedPeriods[4]));
            list.Add(new PeriodItem(ConstStrings.Get("PeriodTwoYears"), PredefinedPeriods[5]));
            list.Add(new PeriodItem(ConstStrings.Get("PeriodFiveYears"), PredefinedPeriods[6]));
            list.Add(new PeriodItem(ConstStrings.Get("PeriodTenYears"), PredefinedPeriods[7]));
            return list;
        }
    }
    public class GenderItem {
        public ImageSource Icon { get; set; }
        public PersonGender Value { get; set; }
        public GenderItem(ImageSource icon, PersonGender gender) {
            Value = gender;
            Icon = icon;
        }
    }
    public class FormatItem {
        public ImageSource Icon { get; set; }
        public MovieItemFormat Value { get; set; }
        public FormatItem(ImageSource icon, MovieItemFormat format) {
            Value = format;
            Icon = icon;
        }
    }
    public class RatingItem {
        public ImageSource Icon { get; set; }
        public ImageSource LargeIcon { get; set; }
        public MovieRating Rating { get; set; }
        public RatingItem(ImageSource icon, ImageSource largeIcon, MovieRating rating) {
            Rating = rating;
            LargeIcon = largeIcon;
            Icon = icon;
        }
    }
    public class FormatsChartSourceItem {
        public string PointName { get; set; }
        public int PointValue { get; set; }
        public FormatsChartSourceItem(string name, int value) {
            PointName = name;
            PointValue = value;
        }
    }
    public class RentalsChartSourceItem {
        public string FormatName { get; set; }
        public Dictionary<string, int> Points { get; set; }
        public RentalsChartSourceItem(string name, Dictionary<string, int> value) {
            FormatName = name;
            Points = value;
        }
    }
    public class PeriodItem {
        public string PeriodName { get; set; }
        public int Period { get; set; }
        public PeriodItem(string name, int value) {
            PeriodName = name;
            Period = value;
        }
    }
    public class NewNameGenerator<T> : Disposable where T : XPBaseObject {
        string categoryUniqueNamePattern;
        int currentPatternIndex;
        string currentPattern;
        Session session;
        XPCollection<T> allObjects;
        string nameField = "Name";
        XPClassInfo classInfo;
        XPMemberInfo memberInfo;
        bool needToDisposeObjects = true;

        public NewNameGenerator() {
            string categoryNamePattern = ConstStrings.Get(string.Format("New{0}", typeof(T).Name));
            categoryUniqueNamePattern = categoryNamePattern + " ({0})";
            currentPattern = categoryNamePattern;
            currentPatternIndex = 0;
        }
        public NewNameGenerator(Session session)
            : this() {
            this.session = session == null ? new Session(Session.DefaultSession.DataLayer) : session;
        }
        public NewNameGenerator(Session session, string nameField)
            : this(session) {
            this.nameField = nameField;
        }
        public NewNameGenerator(XPCollection<T> allObjects)
            : this() {
            this.allObjects = allObjects;
            this.session = allObjects.Session;
            needToDisposeObjects = false;
        }
        public NewNameGenerator(XPCollection<T> allObjects, string nameField)
            : this(allObjects) {
            this.nameField = nameField;
        }
        public string RetrieveNewName() {
            if(!CheckStates()) return string.Format(categoryUniqueNamePattern, DateTime.Now.ToString());
            foreach(T item in allObjects) {
                if(memberInfo.GetValue(item) is string && (string)memberInfo.GetValue(item) == currentPattern) {
                    UpdateNamePattern();
                    return RetrieveNewName();
                }
            }
            return currentPattern;
        }
        protected override void DisposeManaged() {
            if(allObjects != null && needToDisposeObjects)
                allObjects.Dispose();
            allObjects = null;
            base.DisposeManaged();
        }
        protected override void DisposeUnmanaged() {
            session = null;
            base.DisposeUnmanaged();
        }
        bool CheckStates() {
            if(classInfo == null) {
                classInfo = session.GetClassInfo<T>();
            }
            if(memberInfo == null) {
                memberInfo = classInfo.GetMember(nameField);
            }
            if(memberInfo == null) return false;
            if(allObjects == null) allObjects = new XPCollection<T>(session, null, new SortProperty(string.Format("[{0}]", nameField), SortingDirection.Ascending));
            return true;
        }
        void UpdateNamePattern() {
            currentPattern = string.Format(categoryUniqueNamePattern, ++currentPatternIndex);
        }
    }
}
