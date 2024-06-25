Imports DevExpress.Xpf.DemoBase

Namespace ControlsDemo

    <CodeFile("ViewModels/TileNavBaseViewModel.(cs)")>
    <CodeFile("ViewModels/TileNavPaneViewModel.(cs)")>
    <CodeFile("Modules/Views/TileNavDetailsView.xaml")>
    <CodeFile("Modules/Views/TileNavDetailsView.xaml.(cs)")>
    <CodeFile("Modules/Views/TileNavPaneHomeView.xaml")>
    <CodeFile("Modules/Views/TileNavPaneHomeView.xaml.(cs)")>
    Public Partial Class TileNavPaneModule
        Inherits ControlsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub HidePopupContent()
            tileNavPane.CloseFlyout()
            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
