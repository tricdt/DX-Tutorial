Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports DevExpress.Xpf.Editors.Helpers
Imports DevExpress.Xpf.Core

Namespace DialogsDemo.Tests

    Public Class DialogsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return theme IsNot Theme.HybridApp
        End Function
    End Class

    Public Class DialogsDemoModulesAccessor
        Inherits DemoModulesAccessor(Of DialogsDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseDialogsDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As DialogsDemoModulesAccessor

        Protected Property ModuleAccessor As DialogsDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As DialogsDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As DialogsDemoModulesAccessor
    End Class
End Namespace
