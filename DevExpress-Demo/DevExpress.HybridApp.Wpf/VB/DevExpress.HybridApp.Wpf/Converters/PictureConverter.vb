Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.DevAV

Namespace DevExpress.DevAV

    Public Class PictureConverter
        Implements System.Windows.Data.IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim picture As DevExpress.DevAV.Picture = TryCast(value, DevExpress.DevAV.Picture)
            Return If(picture Is Nothing, Nothing, picture.Data)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Dim data As Byte() = TryCast(value, Byte())
            Return If(data Is Nothing, Nothing, New DevExpress.DevAV.Picture() With {.Data = data})
        End Function
    End Class
End Namespace
