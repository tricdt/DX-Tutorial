using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    [MetadataType(typeof(MaskedInputMetadata))]
    public class MaskedInputData_FluentAPI {
        #region Numeric masks
        public decimal CurrencyProperty { get; set; }
        public double PercentProperty { get; set; }
        public double FixedPointProperty { get; set; }
        public double NumberProperty { get; set; }
        public double Decimal4DigitsProperty { get; set; }

        public double CustomNumericMaskPropery1 { get; set; }
        public double CustomNumericMaskPropery2 { get; set; }
        #endregion

        #region DateTime masks
        public DateTime ShortDateProperty { get; set; }
        public DateTime LongDateProperty { get; set; }
        public DateTime ShortTimeProperty { get; set; }
        public DateTime LongTimeProperty { get; set; }
        public DateTime FullDateShortTimeProperty { get; set; }
        public DateTime FullDateLongTimeProperty { get; set; }
        public DateTime MonthDayProperty { get; set; }
        public DateTime YearMonthProperty { get; set; }
        public DateTime SortableDateTimeProperty { get; set; }

        public DateTime CustomDateTimeMaskPropery1 { get; set; }
        public DateTime CustomDateTimeMaskPropery2 { get; set; }
        #endregion

        #region RegEx masks
        public string PhoneProperty { get; set; }
        public string SocialSequrityProperty { get; set; }
        public string LongZipCodeProperty { get; set; }
        public string ShortZipCodeProperty { get; set; }
        #endregion
    }

    public static class MaskedInputMetadata {
        public static void BuildMetadata(MetadataBuilder<MaskedInputData_FluentAPI> builder) {
            #region Numeric masks
            builder.Property(x => x.CurrencyProperty)
                .DisplayName("Currency")
                .NumericMask(PredefinedMasks.Numeric.Currency);
            builder.Property(x => x.PercentProperty)
                .DisplayName("Percents")
                .NumericMask(PredefinedMasks.Numeric.Percent);
            builder.Property(x => x.FixedPointProperty)
                .DisplayName("Number")
                .NumericMask(PredefinedMasks.Numeric.FixedPoint);
            builder.Property(x => x.NumberProperty)
                .DisplayName("Number (with delimiters)")
                .NumericMask(PredefinedMasks.Numeric.Number);
            builder.Property(x => x.Decimal4DigitsProperty)
                .DisplayName("Decimal (4 digits)")
                .NumericMask(PredefinedMasks.Numeric.Decimal + "4");

            builder.Property(x => x.CustomNumericMaskPropery1)
                .DisplayName("4 digits")
                .Description(CustomNumericMaskPropery1Description)
                .NumericMask("#,##0.00");  
            builder.Property(x => x.CustomNumericMaskPropery2)
                .DisplayName("Negative alternative")
                .Description(CustomNumericMaskPropery2Description)
                .NumericMask("#,##0.00;<<#,##0.00>>");  

             
            #endregion

            #region DateTime masks
            builder.Property(x => x.ShortDateProperty)
                .DisplayName("Short date")
                .DateTimeMask(PredefinedMasks.DateTime.ShortDate);
            builder.Property(x => x.LongDateProperty)
                .DisplayName("Long date")
                .DateTimeMask(PredefinedMasks.DateTime.LongDate);
            builder.Property(x => x.ShortTimeProperty)
                .DisplayName("Short time")
                .DateTimeMask(PredefinedMasks.DateTime.ShortTime);
            builder.Property(x => x.LongTimeProperty)
                .DisplayName("Long time")
                .DateTimeMask(PredefinedMasks.DateTime.LongTime);
            builder.Property(x => x.FullDateShortTimeProperty)
                .DisplayName("Full date, short time")
                .DateTimeMask(PredefinedMasks.DateTime.FullDateShortTime);
            builder.Property(x => x.FullDateLongTimeProperty)
                .DisplayName("Full date, long time")
                .DateTimeMask(PredefinedMasks.DateTime.FullDateLongTime);
            builder.Property(x => x.MonthDayProperty)
                .DisplayName("Month day")
                .DateTimeMask(PredefinedMasks.DateTime.MonthDay);
            builder.Property(x => x.YearMonthProperty)
                .DisplayName("Year month")
                .DateTimeMask(PredefinedMasks.DateTime.YearMonth);
            builder.Property(x => x.SortableDateTimeProperty)
                .DisplayName("Sortable date time")
                .DateTimeMask(PredefinedMasks.DateTime.SortableDateTime);

            builder.Property(x => x.CustomDateTimeMaskPropery1)
                .DisplayName("Year first")
                .Description(CustomDateTimeMaskPropery1Description)
                .DateTimeMask("yyyy-MM-dd");
            builder.Property(x => x.CustomDateTimeMaskPropery2)
                .DisplayName("Abbreviated months names")
                .Description(CustomDateTimeMaskPropery2Description)
                .DateTimeMask("yyyy-MMM-dd dddd");  

            #endregion

            #region RegEx masks
            builder.Property(x => x.PhoneProperty)
                .DisplayName("Phone")
                .RegExMask(@"((\+\d|8)?\(\d{3}\))?\d{3}-\d\d-\d\d");
            builder.Property(x => x.SocialSequrityProperty)
                .DisplayName("Social sequrity")
                .RegExMask(@"\d{3}-\d{2}-\d{4}");
            builder.Property(x => x.LongZipCodeProperty)
                .DisplayName("Long zip code")
                .RegExMask(@"\d{5}(-\d{1,4})?");
            builder.Property(x => x.ShortZipCodeProperty)
                .DisplayName("Short zip code")
                .RegExMask(@"\d{5}");
            #endregion

            #region Layout
            builder.TableLayout()
                    .GroupContainer("Numeric masks")
                        .Group("Standard")
                            .ContainsProperty(x => x.CurrencyProperty)
                            .ContainsProperty(x => x.PercentProperty)
                            .ContainsProperty(x => x.FixedPointProperty)
                            .ContainsProperty(x => x.NumberProperty)
                            .ContainsProperty(x => x.Decimal4DigitsProperty)
                        .EndGroup()
                        .Group("Custom")
                            .ContainsProperty(x => x.CustomNumericMaskPropery1)
                            .ContainsProperty(x => x.CustomNumericMaskPropery2)
                        .EndGroup()
                    .EndGroupContainer()
                    .GroupContainer("DateTime masks")
                        .Group("Standard")
                            .ContainsProperty(x => x.ShortDateProperty)
                            .ContainsProperty(x => x.LongDateProperty)
                            .ContainsProperty(x => x.ShortTimeProperty)
                            .ContainsProperty(x => x.LongTimeProperty)
                            .ContainsProperty(x => x.FullDateShortTimeProperty)
                            .ContainsProperty(x => x.FullDateLongTimeProperty)
                            .ContainsProperty(x => x.MonthDayProperty)
                            .ContainsProperty(x => x.YearMonthProperty)
                            .ContainsProperty(x => x.SortableDateTimeProperty)
                        .EndGroup()
                        .Group("Custom")
                            .ContainsProperty(x => x.CustomDateTimeMaskPropery1)
                            .ContainsProperty(x => x.CustomDateTimeMaskPropery2)
                        .EndGroup()
                    .EndGroupContainer()
                    .Group("RegEx masks")
                        .ContainsProperty(x => x.PhoneProperty)
                        .ContainsProperty(x => x.SocialSequrityProperty)
                        .ContainsProperty(x => x.LongZipCodeProperty)
                        .ContainsProperty(x => x.ShortZipCodeProperty)
                    .EndGroup();

	        #endregion
        }
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
