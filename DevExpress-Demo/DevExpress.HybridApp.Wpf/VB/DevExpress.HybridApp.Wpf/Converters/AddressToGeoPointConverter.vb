Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV

    Public Class AddressToGeoPointConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim address = TryCast(value, Address)
            If address Is Nothing Then Return value
            Return New GeoPoint(address.Latitude, address.Longitude)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
