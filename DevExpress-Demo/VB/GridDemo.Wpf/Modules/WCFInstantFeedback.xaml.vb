Imports System

Namespace GridDemo

    Public Partial Class WCFInstantFeedback
        Inherits GridDemoModule

        Public Shared ReadOnly ServiceUri As Uri = New Uri("http://demos.devexpress.com/Services/WcfLinqSC/WcfSCService.svc/")

        Public Shared ReadOnly Property DataServiceContext As WcfSCService.SCEntities
            Get
                Return New WcfSCService.SCEntities(ServiceUri)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler ModuleUnloaded, Sub(s, e)
                grid.ItemsSource = Nothing
                wcfInstantSource.Dispose()
            End Sub
        End Sub
    End Class
End Namespace
