Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System

Namespace SankeyDemo.Tests

    Public Class SankeyCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return False
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return False
        End Function
    End Class
End Namespace
