Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiThreadDataProcessingViewModel.(cs)")>
    Public Partial Class MultiThreadDataProcessing
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler ModuleUnloaded, Sub(s, e)
                grid.ItemsSource = Nothing
                pLinqInstantSource.Dispose()
            End Sub
        End Sub
    End Class
End Namespace
