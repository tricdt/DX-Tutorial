Imports DevExpress.SalesDemo.Model
Imports System.Windows.Media.Imaging

Namespace DevExpress.SalesDemo.Wpf.Helpers

    Public Module ResourceImageHelper

        Public Function GetResourceImage(ByVal image As String) As BitmapImage
            Dim res As BitmapImage = New BitmapImage()
            res.BeginInit()
            res.StreamSource = ModelAssemblyHelper.GetResourceStream(image)
            res.EndInit()
            res.Freeze()
            Return res
        End Function
    End Module
End Namespace
