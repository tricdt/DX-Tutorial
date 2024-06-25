Imports System
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.Native

Namespace ChartsDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
            InitializeComponent()
            DataContext = ViewModelSource.Create(Function() New MainViewModel())
        End Sub
    End Class

    Public Class MainViewModel
        Inherits ViewModelBase

        Public Sub New()
        End Sub
    End Class

    Public Class ChartDemoControl
        Inherits DemoBaseControl

        Protected Overrides Sub AppearDemoModule(ByVal result As [Option](Of Either(Of Exception, DemoModule)))
            MyBase.AppearDemoModule(result)
            If TypeOf CurrentDemoModule Is ChartsDemoModule Then CType(CurrentDemoModule, ChartsDemoModule).OnStartModule()
        End Sub

        Public Overrides Sub LoadModule(ByVal moduleDescription As DemoModuleDescription)
            If TypeOf CurrentDemoModule Is ChartsDemoModule Then CType(CurrentDemoModule, ChartsDemoModule).OnStopModule()
            MyBase.LoadModule(moduleDescription)
        End Sub
    End Class
End Namespace
