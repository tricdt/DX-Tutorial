Imports DevExpress.Xpf.DemoBase

Namespace RibbonDemo

    <CodeFiles("Modules/MVVMRibbon/Views/MVVMRibbon.xaml;
                 Modules/MVVMRibbon/Views/MVVMRibbon.xaml.(cs);
                 Modules/MVVMRibbon/ViewModels/MVVMRibbonCommands.(cs);
                 Modules/MVVMRibbon/ViewModels/MVVMRibbonViewModel.(cs)")>
    Public Partial Class MVVMRibbon
        Inherits RibbonDemoModule

        Public Sub New()
            InitializeComponent()
            Ribbon = ribbonControl
        End Sub
    End Class
End Namespace
