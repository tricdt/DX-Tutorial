Imports System.ComponentModel.DataAnnotations
Imports System.Windows

Namespace LayoutControlDemo

    Public Enum Gender
        Male
        Female
    End Enum

    Public Class SupportedDataTypesData

        Public Property IntProperty As Integer

        Public Property NullableIntProperty As Integer?

        Public Property DoubleProperty As Double

        Public Property NullableDoubleProperty As Double?

        Public Property BoolProperty As Boolean

        Public Property NullableBoolProperty As Boolean?

        Public Property CharProperty As Char

        Public Property NullableCharProperty As Char?

        Public Property EnumProperty As Gender

        Public Property NullableEnumProperty As Gender?

        Public Property StringProperty As String

        Public Property DateTimeProperty As Date

        Public Property NullableDateTimeProperty As Date?

        Public Property DecimalProperty As Decimal

        Public Property NullableDecimalProperty As Decimal?

        Public Property ComplexTypeProperty As Point

        <DataType(DataType.Currency)>
        Public Property CurrencyProperty As Decimal

        <DataType(DataType.MultilineText)>
        Public Property MultilineTextProperty As String

        <DataType(DataType.Password)>
        Public Property PasswordProperty As String

        <DataType(DataType.PhoneNumber)>
        Public Property PhoneNumberProperty As String
    End Class
End Namespace
