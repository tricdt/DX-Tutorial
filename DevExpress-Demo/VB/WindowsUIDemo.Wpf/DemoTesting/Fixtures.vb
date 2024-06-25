Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports System.Linq

Namespace WindowsUIDemo.Tests

    Public Class WindowsUICheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Private skipMemoryLeaksCheckModules As Type() = New Type() {}

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function
    End Class
End Namespace
