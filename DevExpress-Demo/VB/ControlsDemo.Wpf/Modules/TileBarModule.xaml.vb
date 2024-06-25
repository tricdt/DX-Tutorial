Imports DevExpress.Xpf.DemoBase

Namespace ControlsDemo

    <CodeFile("ViewModels/TileNavBaseViewModel.(cs)")>
    <CodeFile("ViewModels/TileBarViewModel.(cs)")>
    <CodeFile("Modules/Views/TileNavDetailsView.xaml")>
    <CodeFile("Modules/Views/TileNavDetailsView.xaml.(cs)")>
    <CodeFile("Modules/Views/TileBarHomeView.xaml")>
    <CodeFile("Modules/Views/TileBarHomeView.xaml.(cs)")>
    <CodeFile("Services/TileBarService.(cs)")>
    Public Partial Class TileBarModule
        Inherits ControlsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub HidePopupContent()
            tileBar.CloseFlyout()
            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
