using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LayoutControlDemo {
    public class MaskedInputData {
        #region Numeric masks
        [NumericMask(Mask = PredefinedMasks.Numeric.Currency)]
        [Display(Name = "Currency (Culture-specific)", GroupName = NumericStandardGroup)]
        public decimal CurrencyProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.Numeric.Currency, Culture = "fr-FR")]
        [Display(Name = "Currency (EURO)", GroupName = NumericStandardGroup)]
        public decimal EuroCurrencyProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.Numeric.Percent)]
        [Display(Name = "Percents", GroupName = NumericStandardGroup)]
        public double PercentProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.Numeric.PercentFractional)]
        [Display(Name = "Percents (multiplied by 100)", GroupName = NumericStandardGroup)]
        public double PercentMultBy100Property { get; set; }

        [NumericMask(Mask = PredefinedMasks.Numeric.FixedPoint)]
        [Display(Name = "Number", GroupName = NumericStandardGroup)]
        public double FixedPointProperty { get; set; } 

        [NumericMask(Mask = PredefinedMasks.Numeric.Number)]
        [Display(Name = "Number (with delimiters)", GroupName = NumericStandardGroup)]
        public double NumberProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.Numeric.Decimal + "4")]
        [Display(Name = "Decimal (4 digits)", GroupName = NumericStandardGroup)]
        public double Decimal4DigitsProperty { get; set; }

        [NumericMask(Mask = "#,##0.00")]
        [Display(Name = "4 digits", GroupName = NumericCustomGroup, Description = CustomNumericMaskPropery1Description)]
        public double CustomNumericMaskPropery1 { get; set; }

        const string NegativeAlternativeMask = "#,##0.00;<<#,##0.00>>";
        [NumericMask(Mask = NegativeAlternativeMask)]
        [Display(Name = "Negative alternative", GroupName = NumericCustomGroup, Description = CustomNumericMaskPropery2Description)]
        public double CustomNumericMaskPropery2 { get; set; }
        #endregion

        #region DateTime masks
        [NumericMask(Mask = PredefinedMasks.DateTime.ShortDate)]
        [Display(Name = "Short date", GroupName = DateTimeStandardGroup)]
        public DateTime ShortDateProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.LongDate)]
        [Display(Name = "Long date", GroupName = DateTimeStandardGroup)]
        public DateTime LongDateProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.ShortTime)]
        [Display(Name = "Short time", GroupName = DateTimeStandardGroup)]
        public DateTime ShortTimeProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.LongTime)]
        [Display(Name = "Long time", GroupName = DateTimeStandardGroup)]
        public DateTime LongTimeProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.FullDateShortTime)]
        [Display(Name = "Full date, short time", GroupName = DateTimeStandardGroup)]
        public DateTime FullDateShortTimeProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.FullDateLongTime)]
        [Display(Name = "Full date, long time", GroupName = DateTimeStandardGroup)]
        public DateTime FullDateLongTimeProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.GeneralDateShortTime)]
        [Display(Name = "General date, short time", GroupName = DateTimeStandardGroup)]
        public DateTime GeneralDateShortTimeProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.GeneralDateLongTime)]
        [Display(Name = "General date, long time", GroupName = DateTimeStandardGroup)]
        public DateTime GeneralDateLongTimeProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.MonthDay)]
        [Display(Name = "Month day", GroupName = DateTimeStandardGroup)]
        public DateTime MonthDayProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.YearMonth)]
        [Display(Name = "Year month", GroupName = DateTimeStandardGroup)]
        public DateTime YearMonthProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.SortableDateTime)]
        [Display(Name = "Sortable date time", GroupName = DateTimeStandardGroup)]
        public DateTime SortableDateTimeProperty { get; set; }

        [NumericMask(Mask = PredefinedMasks.DateTime.UniversalSortableDateTime)]
        [Display(Name = "Universal sortable date time", GroupName = DateTimeStandardGroup)]
        public DateTime UniversalSortableDateTimeProperty { get; set; }

        [NumericMask(Mask = "yyyy-MM-dd")]
        [Display(Name = "Year first", GroupName = DateTimeCustomGroup, Description = CustomDateTimeMaskPropery1Description)]
        public DateTime CustomDateTimeMaskPropery1 { get; set; }

        [NumericMask(Mask = "yyyy-MMM-dd dddd")]
        [Display(Name = "Abbreviated months names", GroupName = DateTimeCustomGroup, Description = CustomDateTimeMaskPropery2Description)]
        public DateTime CustomDateTimeMaskPropery2 { get; set; }
        #endregion

        #region RegEx masks
        const string PhoneMask = @"((\+\d|8)?\(\d{3}\))?\d{3}-\d\d-\d\d";
        [RegExMask(Mask = PhoneMask)]
        [Display(Name = "Phone", GroupName = RegExGroup)]
        public string PhoneProperty { get; set; }

        const string SocialSequrityMask = @"\d{3}-\d{2}-\d{4}";
        [RegExMask(Mask = SocialSequrityMask)]
        [Display(Name = "Social sequrity", GroupName = RegExGroup)]
        public string SocialSequrityProperty { get; set; }

        const string LongZipCodeMask = @"\d{5}(-\d{1,4})?";
        [RegExMask(Mask = LongZipCodeMask)]
        [Display(Name = "Long zip code", GroupName = RegExGroup)]
        public string LongZipCodeProperty { get; set; }

        const string ShortZipCodeMask = @"\d{5}";
        [RegExMask(Mask = ShortZipCodeMask)]
        [Display(Name = "Short zip code", GroupName = RegExGroup)]
        public string ShortZipCodeProperty { get; set; }
        #endregion

        const string NumericStandardGroup = "{Tabs}/Numeric masks/Standard";
        const string NumericCustomGroup = "{Tabs}/Numeric masks/Custom";
        const string DateTimeStandardGroup = "{Tabs}/DateTime masks/Standard";
        const string DateTimeCustomGroup = "{Tabs}/DateTime masks/Custom";
        const string RegExGroup = "{Tabs}/RegEx masks";

        const string CustomNumericMaskPropery1Description = 
            "A mask for entering a real number that has a maximum of 4 digits to the left of the decimal point: #,##0.00";
        const string CustomNumericMaskPropery2Description = 
            "Negative values will be enclosed with double angle brackets: #,##0.00;<<#,##0.00>>";
        const string CustomDateTimeMaskPropery1Description =
            "The mask that requires the year to be entered at the first position, the month at the second and the day at the third position.\r\n" +
            "The '-' characters are used as the date separators: yyyy-MM-dd";
        const string CustomDateTimeMaskPropery2Description =
            "The months are edited using their abbreviated names.\r\n" + 
            "In addition, the name of the day is displayed to the right of the value: yyyy-MMM-dd dddd\r\n" + 
            "The name of the day cannot be edited.";
    }
}
