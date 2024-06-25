Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase

Namespace RibbonDemo

    <CodeFiles("Modules/RibbonWindow/Views/PainterWindow.xaml;
                 Modules/RibbonWindow/Views/PainterWindow.xaml.(cs);
                 Modules/RibbonWindow/ViewModels/PainterWindowViewModel.(cs);
                 Modules/RibbonWindow/Services/CloseWindowService.(cs);
                 Modules/RibbonWindow/Views/RibbonWindow.xaml;
                 Modules/RibbonWindow/Views/RibbonWindow.xaml.(cs)")>
    Public Partial Class RibbonWindow
        Inherits RibbonDemoModule

        Public Sub New()
            DevExpress.Xpf.Bars.BarManager.CheckBarItemNames = False
            InitializeComponent()
        End Sub

        Private Sub ShowPainterWindow()
            Dim Window As PainterWindow = New PainterWindow()
            Window.Width = ActualWidth
            Window.Height = ActualHeight
            Dim pos As Point = PointToScreen(New Point())
            Window.Left = pos.X
            Window.Top = pos.Y
            Window.Show()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowPainterWindow()
        End Sub
    End Class
End Namespace
