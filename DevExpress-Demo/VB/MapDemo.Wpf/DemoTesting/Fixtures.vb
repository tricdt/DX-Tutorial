Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports System

Namespace MapDemo.Tests

    Public Class MapCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return False
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return False
        End Function
    End Class
End Namespace
