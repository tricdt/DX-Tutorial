Imports System
Imports System.Threading
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace NavigationDemo.Tests

    Public Class AccordionCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return theme IsNot Theme.HybridApp AndAlso Not Equals(theme.Name, "Base")
        End Function

        Protected Overrides Sub CreateCheckOptionsAction()
            MyBase.CreateCheckOptionsAction()
            If DemoBaseTesting.CurrentDemoModule.GetType() Is GetType(NavigationPaneDemoModule) Then Thread.Sleep(4000)
        End Sub
    End Class

    Public Class AccordionDemoModulesAccessor
        Inherits DemoModulesAccessor(Of AccordionDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseAccordionDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As AccordionDemoModulesAccessor

        Protected Property ModuleAccessor As AccordionDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As AccordionDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As AccordionDemoModulesAccessor
    End Class
End Namespace
