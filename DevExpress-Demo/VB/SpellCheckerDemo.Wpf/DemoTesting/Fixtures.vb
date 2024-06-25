Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace SpellCheckerDemo.Tests

    Public Class SpellCheckerDemoModuleAccessor
        Inherits DemoModulesAccessor(Of SpellCheckerDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseSpellCheckerTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As SpellCheckerDemoModuleAccessor

        Public Sub New()
            modulesAccessor = New SpellCheckerDemoModuleAccessor(Me)
        End Sub
    End Class

    Public Class SpellCheckerCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class
End Namespace
