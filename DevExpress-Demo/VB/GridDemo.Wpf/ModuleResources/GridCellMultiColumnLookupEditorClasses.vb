Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Windows.Data

Namespace GridDemo

    Public Class CustomerDetailsConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim rowData As RowData = TryCast(value, RowData)
            If rowData Is Nothing Then Return Nothing
            Dim row As Customer = CType(rowData.Row, Customer)
            Return String.Format("{0}{1}, {2}, {3}" & Microsoft.VisualBasic.Constants.vbCrLf & "{4}, {5}", row.Region, row.Country, row.City, row.PostalCode, row.Address, row.Phone)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class
End Namespace
