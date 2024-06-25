Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.LayoutControl
Imports System.Threading
Imports System.Globalization

Namespace DevExpress.DevAV.Views

    Public Partial Class CustomerView
        Inherits UserControl

        Public Sub New()
            Dim culture = New CultureInfo("en-us")
            Me.InitializeComponent()
            Thread.CurrentThread.CurrentCulture = culture
            Thread.CurrentThread.CurrentUICulture = culture
        End Sub

        Private Sub CityGroupSizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            CType(sender, LayoutGroup).ItemLabelsAlignment = If(e.NewSize.Width < 360, LayoutItemLabelsAlignment.Local, LayoutItemLabelsAlignment.Default)
        End Sub
    End Class
End Namespace
