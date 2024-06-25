Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations
Imports System.Windows

Namespace LayoutControlDemo

    <MetadataType(GetType(SupportedDataTypesMetadata))>
    Public Class SupportedDataTypesData_FluentAPI

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

        Public Property CurrencyProperty As Decimal

        Public Property MultilineTextProperty As String

        Public Property PasswordProperty As String

        Public Property PhoneNumberProperty As String
    End Class

    Public Module SupportedDataTypesMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of SupportedDataTypesData_FluentAPI))
            builder.[Property](Function(x) x.CurrencyProperty).CurrencyDataType()
            builder.[Property](Function(x) x.MultilineTextProperty).MultilineTextDataType()
            builder.[Property](Function(x) x.PhoneNumberProperty).PhoneNumberDataType()
            builder.[Property](Function(x) x.PasswordProperty).PasswordDataType()
        End Sub
    End Module
End Namespace
