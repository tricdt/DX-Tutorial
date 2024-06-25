Imports System
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace PdfViewerDemo.Tests

    Public Class PdfViewerCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return moduleType IsNot GetType(MainDemoModule) OrElse theme IsNot Theme.HybridApp
        End Function

        Protected Overrides Function AllowSwitchToCodeTab(ByVal moduleType As Type) As Boolean
            Return False
        End Function
    End Class

    Public Class PdfViewerDemoModulesAccessor
        Inherits DemoModulesAccessor(Of PdfViewerDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BasePdfViewerDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As PdfViewerDemoModulesAccessor

        Protected Property ModuleAccessor As PdfViewerDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As PdfViewerDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As PdfViewerDemoModulesAccessor
    End Class
End Namespace
