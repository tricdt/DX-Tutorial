Namespace GridDemo

#If Not DXCORE3
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/EntityFrameworkInstantFeedbackModeViewModel.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/OutlookDataContext.(cs)")>
    Public Partial Class EntityFrameworkInstantFeedbackMode
        Inherits GridDemoModule

#Else
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/EntityFrameworkInstantFeedbackModeViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/OutlookDataContextNetCore.(cs)")]
    public partial class EntityFrameworkInstantFeedbackMode : GridDemoModule {
#End If
        Public Sub New()
            InitializeComponent()
            AddHandler ModuleUnloaded, Sub(s, e)
                grid.ItemsSource = Nothing
                instantFeedbackDataSource.Dispose()
            End Sub
        End Sub
    End Class
End Namespace
