Imports System.Net
Imports System.Windows
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core

Namespace DevExpress.DevAV

    Public Partial Class MainWindow
        Inherits ThemedWindow

        Public Sub New()
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12
            App.Test(Me)
            Me.InitializeComponent()
            If Height > SystemParameters.VirtualScreenHeight OrElse Width > SystemParameters.VirtualScreenWidth Then WindowState = WindowState.Maximized
            Utils.About.UAlgo.Default.DoEventObject(Utils.About.UAlgo.kDemo, Utils.About.UAlgo.pWPF, Me)
            Call EventManager.RegisterClassHandler(GetType(BarItem), BarItem.ItemClickEvent, New ItemClickEventHandler(AddressOf OnBarItemClick), True)
            ThemeManager.AddThemeChangingHandler(Me, Sub(s, e) Log(String.Format("OutlookInspiredApp: Change Theme: {0}", ApplicationThemeHelper.ApplicationThemeName)))
        End Sub

        Private Sub MainWindowLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Left < 0 OrElse Top < 0 Then WindowState = WindowState.Maximized
        End Sub

        Private Sub OnBarItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim barItem = CType(sender, BarItem)
            Dim content = If(barItem.Content, barItem.CustomizationContent)
            If content IsNot Nothing Then Call Log(String.Format("OutlookInspiredApp: BarItemClick: {0}", content.ToString()))
        End Sub
    End Class
End Namespace
