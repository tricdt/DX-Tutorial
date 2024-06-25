using DevExpress.Xpf.DemoBase.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace DialogsDemo {
    public abstract class BaseValueConverter : MarkupExtension, IValueConverter {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

    public class CountryToFlagImageConverterExtension : BaseValueConverter {                
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            return GetFlagImageByCountryName((string)value);
        }

        public static byte[] GetFlagImageByCountryName(string countryName) {            
            var flag = CountriesData.DataSource.Find(x => x.Name == countryName).Flag;
            return  flag;                           
        }
    }

    public class GenderToImageConverterExtension : BaseValueConverter {
        public static List<string> images = new List<string> { "m", "f" };
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            return GetImagePathByGender((string)value);
        }

        public static string GetImagePathByGender(string genderName) {
            genderName = genderName.ToLowerInvariant();
            if(genderName == "m")
                return "pack://application:,,,/DialogsDemo;component/Images/ThemedWindow/Gender/Male.svg";
            if(genderName == "f")
                return "pack://application:,,,/DialogsDemo;component/Images/ThemedWindow/Gender/Female.svg";
            return genderName;
        }        
    }

    public class GenderToFullStringConverterExtension : BaseValueConverter {
        public static List<string> images = new List<string> { "m", "f" };
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            return GetFullStringByGender((string)value);
        }

        public static string GetFullStringByGender(string genderName) {
            genderName = genderName.ToLowerInvariant();
            if(genderName == "m")
                return "Male";
            if(genderName == "f")
                return "Female";
            return genderName;
        }        
    }

    public class StatusToFullStringConverterExtension : BaseValueConverter {
        public static List<string> images = new List<string> { "s", "m" };
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return null;
            return GetFullStringByGender((string)value);
        }

        public static string GetFullStringByGender(string genderName) {
            genderName = genderName.ToLowerInvariant();
            if(genderName == "s")
                return "Single";
            if(genderName == "m")
                return "Married";
            return genderName;
        }
    }
}
