Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.DemoBase

Namespace RibbonDemo

    <CodeFiles("Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml;
                 Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml.(cs);
                 Modules/RibbonMerging/Views/PaintUserControl.xaml;
                 Modules/RibbonMerging/Views/PaintUserControl.xaml.(cs);
                 Modules/RibbonMerging/Views/TextUserControl.xaml;
                 Modules/RibbonMerging/Views/TextUserControl.xaml.(cs);                 
                 Modules/RibbonMerging/ViewModels/PaintPanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/TextPanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/PanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/RibbonMergingViewModel.(cs);")>
    Public Partial Class RibbonMergingUserControl
        Inherits RibbonDemoModule

        Public Sub New()
            InitializeComponent()
            Ribbon = mainRibbon
        End Sub

        Protected Overrides Sub Hide()
            mainRibbon.CloseApplicationMenu()
            MyBase.Hide()
        End Sub
    End Class
End Namespace
