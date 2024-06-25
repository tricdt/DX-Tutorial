Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports System.Linq
Imports System.Windows.Controls

Namespace BarsDemo.Tests

    Public Class BarsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Private skipMemoryLeaksCheckModules As Type() = New Type() {}

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function
    End Class

    Public Class BarsDemoModulesAccessor
        Inherits DemoModulesAccessor(Of BarsDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub

        Public ReadOnly Property Manager As BarManager
            Get
                Return DemoModule.Manager
            End Get
        End Property
    End Class

    Public MustInherit Class BaseBarsDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As BarsDemoModulesAccessor

        Public Sub New()
            modulesAccessor = New BarsDemoModulesAccessor(Me)
        End Sub

        Public ReadOnly Property Manager As BarManager
            Get
                Return modulesAccessor.Manager
            End Get
        End Property
    End Class

    Public Class CheckDemoOptionsFixture
        Inherits BaseBarsDemoTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(ItemProperties))
        End Sub
    End Class
End Namespace
