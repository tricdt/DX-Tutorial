Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Editors.Helpers

Namespace EditorsDemo.Tests

    Public Class EditorsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class

    Public Class EditorsDemoModulesAccessor
        Inherits DemoModulesAccessor(Of EditorsDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseEditorsDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As EditorsDemoModulesAccessor

        Protected Property ModuleAccessor As EditorsDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As EditorsDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As EditorsDemoModulesAccessor
    End Class
End Namespace
