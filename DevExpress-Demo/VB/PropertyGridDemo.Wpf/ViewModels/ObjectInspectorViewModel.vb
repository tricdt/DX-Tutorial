Imports System.Windows
Imports System.Windows.Media

Namespace PropertyGridDemo

    Public Class ObjectInspectorViewModel

        Public Overridable Property Data As SimpleEditingData

        Public Sub New()
            Data = New SimpleEditingData() With {.IntProperty = 123, .NullableIntProperty = 4567, .DoubleProperty = 12345.12345, .NullableDoubleProperty = 6789.6789, .BoolProperty = True, .CharProperty = "Y"c, .NullableCharProperty = "N"c, .NullableEnumProperty = Gender.Female, .StringProperty = "text", .DateTimeProperty = Date.Today, .NullableDateTimeProperty = Date.Today.AddDays(1), .DecimalProperty = 12345.12345D, .NullableDecimalProperty = 789.789D, .PointProperty = New Point(123.45, 678.9), .CurrencyProperty = 1234567.89D, .MultilineTextProperty = "first line of text" & Microsoft.VisualBasic.Constants.vbCrLf & "second line of text" & Microsoft.VisualBasic.Constants.vbCrLf & "third line of text", .PasswordProperty = "password", .PhoneNumberProperty = "1234567890", .ColorProperty = Colors.Red}
        End Sub
    End Class
End Namespace
