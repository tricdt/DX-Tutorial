Imports System.Globalization
Imports System.Threading
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class OrderCollectionView
        Inherits UserControl

        Public Sub New()
            Dim culture = New CultureInfo("en-us")
            Me.InitializeComponent()
            Thread.CurrentThread.CurrentCulture = culture
            Thread.CurrentThread.CurrentUICulture = culture
        End Sub
    End Class
End Namespace
