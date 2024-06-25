Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports DevExpress.Xpf.Editors.Helpers
Imports DevExpress.Xpf.Core

Namespace MVVMDemo.Tests

    Public Class MVVMCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return theme IsNot Theme.HybridApp
        End Function
    End Class

    Public Class MVVMDemoModulesAccessor
        Inherits DemoModulesAccessor(Of MVVMDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseMVVMDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As MVVMDemoModulesAccessor

        Protected Property ModuleAccessor As MVVMDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As MVVMDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As MVVMDemoModulesAccessor
    End Class
End Namespace
