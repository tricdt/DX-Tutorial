Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace LayoutControlDemo

    Public Class MaskedInputData

#Region "Numeric masks"
        <NumericMask(Mask:=PredefinedMasks.Numeric.Currency)>
        <Display(Name:="Currency (Culture-specific)", GroupName:=NumericStandardGroup)>
        Public Property CurrencyProperty As Decimal

        <NumericMask(Mask:=PredefinedMasks.Numeric.Currency, Culture:="fr-FR")>
        <Display(Name:="Currency (EURO)", GroupName:=NumericStandardGroup)>
        Public Property EuroCurrencyProperty As Decimal

        <NumericMask(Mask:=PredefinedMasks.Numeric.Percent)>
        <Display(Name:="Percents", GroupName:=NumericStandardGroup)>
        Public Property PercentProperty As Double

        <NumericMask(Mask:=PredefinedMasks.Numeric.PercentFractional)>
        <Display(Name:="Percents (multiplied by 100)", GroupName:=NumericStandardGroup)>
        Public Property PercentMultBy100Property As Double

        <NumericMask(Mask:=PredefinedMasks.Numeric.FixedPoint)>
        <Display(Name:="Number", GroupName:=NumericStandardGroup)>
        Public Property FixedPointProperty As Double

        <NumericMask(Mask:=PredefinedMasks.Numeric.Number)>
        <Display(Name:="Number (with delimiters)", GroupName:=NumericStandardGroup)>
        Public Property NumberProperty As Double

        <NumericMask(Mask:=PredefinedMasks.Numeric.Decimal & "4")>
        <Display(Name:="Decimal (4 digits)", GroupName:=NumericStandardGroup)>
        Public Property Decimal4DigitsProperty As Double

        <NumericMask(Mask:="#,##0.00")>
        <Display(Name:="4 digits", GroupName:=NumericCustomGroup, Description:=CustomNumericMaskPropery1Description)>
        Public Property CustomNumericMaskPropery1 As Double

        Const NegativeAlternativeMask As String = "#,##0.00;<<#,##0.00>>"

        <NumericMask(Mask:=NegativeAlternativeMask)>
        <Display(Name:="Negative alternative", GroupName:=NumericCustomGroup, Description:=CustomNumericMaskPropery2Description)>
        Public Property CustomNumericMaskPropery2 As Double

#End Region
#Region "DateTime masks"
        <NumericMask(Mask:=PredefinedMasks.DateTime.ShortDate)>
        <Display(Name:="Short date", GroupName:=DateTimeStandardGroup)>
        Public Property ShortDateProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.LongDate)>
        <Display(Name:="Long date", GroupName:=DateTimeStandardGroup)>
        Public Property LongDateProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.ShortTime)>
        <Display(Name:="Short time", GroupName:=DateTimeStandardGroup)>
        Public Property ShortTimeProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.LongTime)>
        <Display(Name:="Long time", GroupName:=DateTimeStandardGroup)>
        Public Property LongTimeProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.FullDateShortTime)>
        <Display(Name:="Full date, short time", GroupName:=DateTimeStandardGroup)>
        Public Property FullDateShortTimeProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.FullDateLongTime)>
        <Display(Name:="Full date, long time", GroupName:=DateTimeStandardGroup)>
        Public Property FullDateLongTimeProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.GeneralDateShortTime)>
        <Display(Name:="General date, short time", GroupName:=DateTimeStandardGroup)>
        Public Property GeneralDateShortTimeProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.GeneralDateLongTime)>
        <Display(Name:="General date, long time", GroupName:=DateTimeStandardGroup)>
        Public Property GeneralDateLongTimeProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.MonthDay)>
        <Display(Name:="Month day", GroupName:=DateTimeStandardGroup)>
        Public Property MonthDayProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.YearMonth)>
        <Display(Name:="Year month", GroupName:=DateTimeStandardGroup)>
        Public Property YearMonthProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.SortableDateTime)>
        <Display(Name:="Sortable date time", GroupName:=DateTimeStandardGroup)>
        Public Property SortableDateTimeProperty As Date

        <NumericMask(Mask:=PredefinedMasks.DateTime.UniversalSortableDateTime)>
        <Display(Name:="Universal sortable date time", GroupName:=DateTimeStandardGroup)>
        Public Property UniversalSortableDateTimeProperty As Date

        <NumericMask(Mask:="yyyy-MM-dd")>
        <Display(Name:="Year first", GroupName:=DateTimeCustomGroup, Description:=CustomDateTimeMaskPropery1Description)>
        Public Property CustomDateTimeMaskPropery1 As Date

        <NumericMask(Mask:="yyyy-MMM-dd dddd")>
        <Display(Name:="Abbreviated months names", GroupName:=DateTimeCustomGroup, Description:=CustomDateTimeMaskPropery2Description)>
        Public Property CustomDateTimeMaskPropery2 As Date

#End Region
#Region "RegEx masks"
        Const PhoneMask As String = "((\+\d|8)?\(\d{3}\))?\d{3}-\d\d-\d\d"

        <RegExMask(Mask:=PhoneMask)>
        <Display(Name:="Phone", GroupName:=RegExGroup)>
        Public Property PhoneProperty As String

        Const SocialSequrityMask As String = "\d{3}-\d{2}-\d{4}"

        <RegExMask(Mask:=SocialSequrityMask)>
        <Display(Name:="Social sequrity", GroupName:=RegExGroup)>
        Public Property SocialSequrityProperty As String

        Const LongZipCodeMask As String = "\d{5}(-\d{1,4})?"

        <RegExMask(Mask:=LongZipCodeMask)>
        <Display(Name:="Long zip code", GroupName:=RegExGroup)>
        Public Property LongZipCodeProperty As String

        Const ShortZipCodeMask As String = "\d{5}"

        <RegExMask(Mask:=ShortZipCodeMask)>
        <Display(Name:="Short zip code", GroupName:=RegExGroup)>
        Public Property ShortZipCodeProperty As String

#End Region
        Const NumericStandardGroup As String = "{Tabs}/Numeric masks/Standard"

        Const NumericCustomGroup As String = "{Tabs}/Numeric masks/Custom"

        Const DateTimeStandardGroup As String = "{Tabs}/DateTime masks/Standard"

        Const DateTimeCustomGroup As String = "{Tabs}/DateTime masks/Custom"

        Const RegExGroup As String = "{Tabs}/RegEx masks"

        Const CustomNumericMaskPropery1Description As String = "A mask for entering a real number that has a maximum of 4 digits to the left of the decimal point: #,##0.00"

        Const CustomNumericMaskPropery2Description As String = "Negative values will be enclosed with double angle brackets: #,##0.00;<<#,##0.00>>"

        Const CustomDateTimeMaskPropery1Description As String = "The mask that requires the year to be entered at the first position, the month at the second and the day at the third position." & Microsoft.VisualBasic.Constants.vbCrLf & "The '-' characters are used as the date separators: yyyy-MM-dd"

        Const CustomDateTimeMaskPropery2Description As String = "The months are edited using their abbreviated names." & Microsoft.VisualBasic.Constants.vbCrLf & "In addition, the name of the day is displayed to the right of the value: yyyy-MMM-dd dddd" & Microsoft.VisualBasic.Constants.vbCrLf & "The name of the day cannot be edited."
    End Class
End Namespace
