Imports System
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace SpreadsheetDemo.Tests

    Public Class SpreadsheetCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
#If Not DXCORE3
            If moduleType Is GetType(BindingToDataSource) Then Return False
#End If
            Return MyBase.CanRunModule(moduleType)
        End Function
    End Class

    Public Class SpreadsheetDemoModulesAccessor
        Inherits DemoModulesAccessor(Of SpreadsheetDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseSpreadsheetDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As SpreadsheetDemoModulesAccessor

        Protected Property ModuleAccessor As SpreadsheetDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As SpreadsheetDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As SpreadsheetDemoModulesAccessor
    End Class
End Namespace
