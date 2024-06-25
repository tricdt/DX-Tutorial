Imports System
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace SchedulingDemo.Tests

    Public Class SchedulingCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            If moduleType Is GetType(OnDemandDataLoading) Then Return False
            Return MyBase.CanRunModule(moduleType)
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            If moduleType Is GetType(PrintingTemplatesDemoModule) Then Return False
            Return True
        End Function
    End Class
End Namespace
