Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    <MetadataType(GetType(MaskedInputMetadata))>
    Public Class MaskedInputData_FluentAPI

#Region "Numeric masks"
        Public Property CurrencyProperty As Decimal

        Public Property PercentProperty As Double

        Public Property FixedPointProperty As Double

        Public Property NumberProperty As Double

        Public Property Decimal4DigitsProperty As Double

        Public Property CustomNumericMaskPropery1 As Double

        Public Property CustomNumericMaskPropery2 As Double

#End Region
#Region "DateTime masks"
        Public Property ShortDateProperty As Date

        Public Property LongDateProperty As Date

        Public Property ShortTimeProperty As Date

        Public Property LongTimeProperty As Date

        Public Property FullDateShortTimeProperty As Date

        Public Property FullDateLongTimeProperty As Date

        Public Property MonthDayProperty As Date

        Public Property YearMonthProperty As Date

        Public Property SortableDateTimeProperty As Date

        Public Property CustomDateTimeMaskPropery1 As Date

        Public Property CustomDateTimeMaskPropery2 As Date

#End Region
#Region "RegEx masks"
        Public Property PhoneProperty As String

        Public Property SocialSequrityProperty As String

        Public Property LongZipCodeProperty As String

        Public Property ShortZipCodeProperty As String
#End Region
    End Class

    Public Module MaskedInputMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of MaskedInputData_FluentAPI))
#Region "Numeric masks"
            builder.Property(Function(x) x.CurrencyProperty).DisplayName("Currency").NumericMask(PredefinedMasks.Numeric.Currency)
            builder.Property(Function(x) x.PercentProperty).DisplayName("Percents").NumericMask(PredefinedMasks.Numeric.Percent)
            builder.Property(Function(x) x.FixedPointProperty).DisplayName("Number").NumericMask(PredefinedMasks.Numeric.FixedPoint)
            builder.Property(Function(x) x.NumberProperty).DisplayName("Number (with delimiters)").NumericMask(PredefinedMasks.Numeric.Number)
            builder.Property(Function(x) x.Decimal4DigitsProperty).DisplayName("Decimal (4 digits)").NumericMask(PredefinedMasks.Numeric.Decimal & "4")
            builder.Property(Function(x) x.CustomNumericMaskPropery1).DisplayName("4 digits").Description(CustomNumericMaskPropery1Description).NumericMask("#,##0.00")
            builder.Property(Function(x) x.CustomNumericMaskPropery2).DisplayName("Negative alternative").Description(CustomNumericMaskPropery2Description).NumericMask("#,##0.00;<<#,##0.00>>")
#End Region
#Region "DateTime masks"
            builder.Property(Function(x) x.ShortDateProperty).DisplayName("Short date").DateTimeMask(PredefinedMasks.DateTime.ShortDate)
            builder.Property(Function(x) x.LongDateProperty).DisplayName("Long date").DateTimeMask(PredefinedMasks.DateTime.LongDate)
            builder.Property(Function(x) x.ShortTimeProperty).DisplayName("Short time").DateTimeMask(PredefinedMasks.DateTime.ShortTime)
            builder.Property(Function(x) x.LongTimeProperty).DisplayName("Long time").DateTimeMask(PredefinedMasks.DateTime.LongTime)
            builder.Property(Function(x) x.FullDateShortTimeProperty).DisplayName("Full date, short time").DateTimeMask(PredefinedMasks.DateTime.FullDateShortTime)
            builder.Property(Function(x) x.FullDateLongTimeProperty).DisplayName("Full date, long time").DateTimeMask(PredefinedMasks.DateTime.FullDateLongTime)
            builder.Property(Function(x) x.MonthDayProperty).DisplayName("Month day").DateTimeMask(PredefinedMasks.DateTime.MonthDay)
            builder.Property(Function(x) x.YearMonthProperty).DisplayName("Year month").DateTimeMask(PredefinedMasks.DateTime.YearMonth)
            builder.Property(Function(x) x.SortableDateTimeProperty).DisplayName("Sortable date time").DateTimeMask(PredefinedMasks.DateTime.SortableDateTime)
            builder.Property(Function(x) x.CustomDateTimeMaskPropery1).DisplayName("Year first").Description(CustomDateTimeMaskPropery1Description).DateTimeMask("yyyy-MM-dd")
            builder.Property(Function(x) x.CustomDateTimeMaskPropery2).DisplayName("Abbreviated months names").Description(CustomDateTimeMaskPropery2Description).DateTimeMask("yyyy-MMM-dd dddd")
#End Region
#Region "RegEx masks"
            builder.Property(Function(x) x.PhoneProperty).DisplayName("Phone").RegExMask("((\+\d|8)?\(\d{3}\))?\d{3}-\d\d-\d\d")
            builder.Property(Function(x) x.SocialSequrityProperty).DisplayName("Social sequrity").RegExMask("\d{3}-\d{2}-\d{4}")
            builder.Property(Function(x) x.LongZipCodeProperty).DisplayName("Long zip code").RegExMask("\d{5}(-\d{1,4})?")
            builder.Property(Function(x) x.ShortZipCodeProperty).DisplayName("Short zip code").RegExMask("\d{5}")
#End Region
#Region "Layout"
            builder.TableLayout().GroupContainer("Numeric masks").Group("Standard").ContainsProperty(Function(x) x.CurrencyProperty).ContainsProperty(Function(x) x.PercentProperty).ContainsProperty(Function(x) x.FixedPointProperty).ContainsProperty(Function(x) x.NumberProperty).ContainsProperty(Function(x) x.Decimal4DigitsProperty).EndGroup().Group("Custom").ContainsProperty(Function(x) x.CustomNumericMaskPropery1).ContainsProperty(Function(x) x.CustomNumericMaskPropery2).EndGroup().EndGroupContainer().GroupContainer("DateTime masks").Group("Standard").ContainsProperty(Function(x) x.ShortDateProperty).ContainsProperty(Function(x) x.LongDateProperty).ContainsProperty(Function(x) x.ShortTimeProperty).ContainsProperty(Function(x) x.LongTimeProperty).ContainsProperty(Function(x) x.FullDateShortTimeProperty).ContainsProperty(Function(x) x.FullDateLongTimeProperty).ContainsProperty(Function(x) x.MonthDayProperty).ContainsProperty(Function(x) x.YearMonthProperty).ContainsProperty(Function(x) x.SortableDateTimeProperty).EndGroup().Group("Custom").ContainsProperty(Function(x) x.CustomDateTimeMaskPropery1).ContainsProperty(Function(x) x.CustomDateTimeMaskPropery2).EndGroup().EndGroupContainer().Group("RegEx masks").ContainsProperty(Function(x) x.PhoneProperty).ContainsProperty(Function(x) x.SocialSequrityProperty).ContainsProperty(Function(x) x.LongZipCodeProperty).ContainsProperty(Function(x) x.ShortZipCodeProperty).EndGroup()
#End Region
        End Sub

        Const CustomNumericMaskPropery1Description As String = "A mask for entering a real number that has a maximum of 4 digits to the left of the decimal point: #,##0.00"

        Const CustomNumericMaskPropery2Description As String = "Negative values will be enclosed with double angle brackets: #,##0.00;<<#,##0.00>>"

        Const CustomDateTimeMaskPropery1Description As String = "The mask that requires the year to be entered at the first position, the month at the second and the day at the third position." & Microsoft.VisualBasic.Constants.vbCrLf & "The '-' characters are used as the date separators: yyyy-MM-dd"

        Const CustomDateTimeMaskPropery2Description As String = "The months are edited using their abbreviated names." & Microsoft.VisualBasic.Constants.vbCrLf & "In addition, the name of the day is displayed to the right of the value: yyyy-MMM-dd dddd" & Microsoft.VisualBasic.Constants.vbCrLf & "The name of the day cannot be edited."
    End Module
End Namespace
