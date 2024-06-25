Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Editors.Helpers

Namespace PropertyGridDemo.Tests

    Public Class PropertyGridCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class

    Public Class PropertyGridDemoModulesAccessor
        Inherits DemoModulesAccessor(Of PropertyGridDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BasePropertyGridDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As PropertyGridDemoModulesAccessor

        Protected Property ModuleAccessor As PropertyGridDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As PropertyGridDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As PropertyGridDemoModulesAccessor
    End Class
End Namespace
