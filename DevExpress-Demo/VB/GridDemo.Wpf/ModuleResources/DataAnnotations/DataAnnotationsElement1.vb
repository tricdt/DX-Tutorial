Imports System.ComponentModel.DataAnnotations
Imports System.Windows

Namespace GridDemo

    Public Class DataAnnotationsElement1

        <Display(Name:="Int", GroupName:="Numeric Types")>
        Public Property IntProperty As Integer

        <Display(Name:="Nullable Int", GroupName:="Numeric Types")>
        Public Property NullableIntProperty As Integer?

        <Display(Name:="Double", GroupName:="Numeric Types")>
        Public Property DoubleProperty As Double

        <Display(Name:="Nullable Double", GroupName:="Numeric Types")>
        Public Property NullableDoubleProperty As Double?

        <Display(Name:="Bool", GroupName:="Bool Types")>
        Public Property BoolProperty As Boolean

        <Display(Name:="Nullable Bool", GroupName:="Bool Types")>
        Public Property NullableBoolProperty As Boolean?

        <Display(Name:="Char", GroupName:="Char And String Types")>
        Public Property CharProperty As Char

        <Display(Name:="Nullable Char", GroupName:="Char And String Types")>
        Public Property NullableCharProperty As Char?

        <Display(Name:="Enum", GroupName:="Enum Types")>
        Public Property EnumProperty As Gender

        <Display(Name:="Nullable Enum", GroupName:="Enum Types")>
        Public Property NullableEnumProperty As Gender?

        <Display(Name:="String", GroupName:="Char And String Types")>
        Public Property StringProperty As String

        <Display(Name:="DateTime", GroupName:="Date Types")>
        Public Property DateTimeProperty As Date

        <Display(Name:="Nullable DateTime", GroupName:="Date Types")>
        Public Property NullableDateTimeProperty As Date?

        <Display(Name:="Decimal", GroupName:="Numeric Types")>
        Public Property DecimalProperty As Decimal

        <Display(Name:="Nullable Decimal", GroupName:="Numeric Types")>
        Public Property NullableDecimalProperty As Decimal?

        <Display(Name:="Point", GroupName:="Complex Types")>
        Public Property ComplexTypeProperty As Point

        <Display(Name:="Currency", GroupName:="Numeric Types"), DataType(DataType.Currency)>
        Public Property CurrencyProperty As Decimal

        <Display(Name:="Multiline Text", GroupName:="Char And String Types"), DataType(DataType.MultilineText)>
        Public Property MultilineTextProperty As String

        <Display(Name:="Phone Number", GroupName:="Char And String Types"), DataType(DataType.PhoneNumber)>
        Public Property PhoneNumberProperty As String
    End Class
End Namespace
