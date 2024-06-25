using System;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.VideoRent.Helpers;
using System.Collections.Generic;
using System.Reflection;

#if SL
using DevExpress.Xpf.Drawing;
#endif

namespace DevExpress.VideoRent {
    [Flags]
    public enum MovieGenre {
        None = 0, Action = 0x1, Adventure = 0x2, Animation = 0x4, Biography = 0x8, Comedy = 0x10, Crime = 0x20,
        Documentary = 0x40, Drama = 0x80, Family = 0x100, Fantasy = 0x200, History = 0x400, Horror = 0x800,
        Music = 0x1000, Musical = 0x2000, Mystery = 0x4000, Romance = 0x8000, SciFi = 0x10000, Sport = 0x20000,
        Thriller = 0x40000, War = 0x80000, Western = 0x100000
    }
    public enum MovieRating { G = 0, PG = 1, PG13 = 2, R = 3, NC17 = 4 }
    public enum MovieItemStatus { Active, Rented, Sold, Damaged, Lost }
    public enum MovieItemFormat { DVD = 1, BlueRay = 2, VideoCD = 3 }
    public enum EmployeeRole { Administrator, Operator }
    public enum PersonGender { Male, Female }
    public enum ReceiptType { Rentals, Purchases, LateFees }
    public enum CustomerDiscountLevel { FirstTime, Basic, Occasional, Active, Prodigious }
    public enum ActiveRentType { None = 0, Overdue = 1, Today = 2, Active = 3 }
    public class ReferenceData {
        public const int ShelvesCount = 12;
        public const string DirectorString = "Director";
        public const string AdministratorString = "Administrator";
        public static string[] CompanyTypes = new string[] {
            "Production", "Distributor", "Special Effects", "Others" };
        public static string[] MovieLanguages = new string[] {
            "English", "French", "Spanish", "Russian", "Arabic", "Chinese", "German", "Japanese", "Portuguese", "Somali", "Italian" };
        public static decimal[] CustomerDiscount = new decimal[] { 0, 0.03M, 0.05M, 0.1M, 0.2M };
        public static decimal[] CustomerDiscountLevel = new decimal[] { 0, 300, 600, 900, 1200 };
        public static decimal GetDiscountLevelPayment(CustomerDiscountLevel level) {
            return CustomerDiscountLevel[(int)level];
        }
        public static decimal GetDiscount(CustomerDiscountLevel level) {
            return CustomerDiscount[(int)level];
        }
    }
    public class ReferenceImages {
        static Image person = null, person_small = null, movie = null;
        public static Image UnknownPerson {
            get {
                if(person == null)
                    person = ResourceImageHelperCore.CreateImageFromResources("DevExpress.VideoRent.Images.Unknown-user.png", typeof(Customer).Assembly);
                return person;
            }
        }
        public static Image UnknownPerson_Small {
            get {
                if(person_small == null)
                    person_small = ResourceImageHelperCore.CreateImageFromResources("DevExpress.VideoRent.Images.Unknown-user-small.png", typeof(Customer).Assembly);
                return person_small;
            }
        }
        public static Image UnknownMovie {
            get {
                if(movie == null)
                    movie = ResourceImageHelperCore.CreateImageFromResources("DevExpress.VideoRent.Images.Unknown-movie.png", typeof(Movie).Assembly);
                return movie;
            }
        }
    }
    public static class EnumTitlesRegistrator {
        static bool alreadyRegistered;
        public static void TryRegisterTitles() {
            if(alreadyRegistered) return;
            alreadyRegistered = true;
            EnumTitlesKeeper<MovieItemFormat>.RegisterTitle(MovieItemFormat.DVD, Properties.Resources.DVD);
            EnumTitlesKeeper<MovieItemFormat>.RegisterTitle(MovieItemFormat.BlueRay, Properties.Resources.BlueRay);
            EnumTitlesKeeper<MovieItemFormat>.RegisterTitle(MovieItemFormat.VideoCD, Properties.Resources.VideoCD);
            EnumTitlesKeeper<MovieItemFormat>.RegisterTitle(null, Properties.Resources.AllFormats);
            EnumTitlesKeeper<MovieRating>.RegisterTitle(MovieRating.PG13, "PG-13");
            EnumTitlesKeeper<MovieRating>.RegisterTitle(MovieRating.NC17, "NC-17");
            EnumTitlesKeeper<PersonGender>.RegisterTitle(PersonGender.Female, Properties.Resources.Female);
            EnumTitlesKeeper<PersonGender>.RegisterTitle(PersonGender.Male, Properties.Resources.Male);
            EnumTitlesKeeper<CustomerDiscountLevel>.RegisterTitle(CustomerDiscountLevel.FirstTime, Properties.Resources.RenterFirstTime);
            EnumTitlesKeeper<CustomerDiscountLevel>.RegisterTitle(CustomerDiscountLevel.Basic, Properties.Resources.RenterBasic);
            EnumTitlesKeeper<CustomerDiscountLevel>.RegisterTitle(CustomerDiscountLevel.Occasional, Properties.Resources.RenterOccasional);
            EnumTitlesKeeper<CustomerDiscountLevel>.RegisterTitle(CustomerDiscountLevel.Active, Properties.Resources.RenterActive);
            EnumTitlesKeeper<CustomerDiscountLevel>.RegisterTitle(CustomerDiscountLevel.Prodigious, Properties.Resources.RenterProdigious);
            EnumTitlesKeeper<ReceiptType>.RegisterTitle(ReceiptType.Rentals, Properties.Resources.Rentals);
            EnumTitlesKeeper<ReceiptType>.RegisterTitle(ReceiptType.Purchases, Properties.Resources.Purchases);
            EnumTitlesKeeper<ReceiptType>.RegisterTitle(ReceiptType.LateFees, Properties.Resources.LateFees);
            EnumTitlesKeeper<MovieItemStatus>.RegisterTitle(MovieItemStatus.Active, Properties.Resources.Active);
            EnumTitlesKeeper<MovieItemStatus>.RegisterTitle(MovieItemStatus.Damaged, Properties.Resources.Damaged);
            EnumTitlesKeeper<MovieItemStatus>.RegisterTitle(MovieItemStatus.Lost, Properties.Resources.Lost);
            EnumTitlesKeeper<MovieItemStatus>.RegisterTitle(MovieItemStatus.Rented, Properties.Resources.Rented);
            EnumTitlesKeeper<MovieItemStatus>.RegisterTitle(MovieItemStatus.Sold, Properties.Resources.Sold);
            EnumTitlesKeeper<ActiveRentType>.RegisterTitle(ActiveRentType.None, Properties.Resources.None);
            EnumTitlesKeeper<ActiveRentType>.RegisterTitle(ActiveRentType.Today, Properties.Resources.Today);
            EnumTitlesKeeper<ActiveRentType>.RegisterTitle(ActiveRentType.Overdue, Properties.Resources.Overdue);
            EnumTitlesKeeper<ActiveRentType>.RegisterTitle(ActiveRentType.Active, Properties.Resources.ActiveRent);
        }
    }
    public static class EnumTitlesKeeper<T> {
        static object nullValue = new object();
        static Dictionary<object, string> titles = new Dictionary<object, string>();
        public static void RegisterTitle(object id, string value) {
            if(id == null) {
                id = nullValue;
            }
            if(!titles.ContainsKey(id)) {
                titles.Add(id, value);
            }
        }
        public static List<TI> GetItemsList<TI>() {
            List<TI> listToReturn = new List<TI>();
            if(titles != null) {
                ConstructorInfo constructor = typeof(TI).GetConstructor(new Type[] { typeof(T), typeof(string) });
                if(constructor == null) {
                    constructor = typeof(TI).GetConstructor(new Type[] { typeof(object), typeof(string) });
                }
                if(constructor != null) {
                    foreach(T item in EnumHelper.GetValues<T>()) {
                        listToReturn.Add((TI)constructor.Invoke(new object[] { item, GetTitle(item) }));
                    }
                }
            }
            return listToReturn;
        }
        public static string GetTitle(object value) {
            EnumTitlesRegistrator.TryRegisterTitles();
            if(value == null) {
                value = nullValue;
            }
            string title;
            if(!titles.TryGetValue(value, out title)) {
                title = value.ToString();
            }
            return title;
        }
    }
}
