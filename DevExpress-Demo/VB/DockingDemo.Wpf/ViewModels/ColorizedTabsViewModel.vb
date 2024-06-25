Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.POCO
Imports DockingDemo.Helpers

Namespace DockingDemo.ViewModels

    Public Class ColorizedTabsViewModel

        Public Shared Function Create() As ColorizedTabsViewModel
            Return ViewModelSource.Create(Function() New ColorizedTabsViewModel())
        End Function

        Protected Sub New()
            If IsInDesignMode() Then Return
            InitDataSource()
        End Sub

        Public Overridable Property Employees As List(Of EmployeeWrapper)

        Private Sub InitDataSource()
            Employees = NWindContext.Create().Employees.ToList().[Select](Function(x) EmployeeWrapper.Create(x)).ToList()
            Dim i As Integer = 0
            For Each employee In Employees
                employee.BackgroundColor = ColorPalette.GetColor(System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1))
            Next
        End Sub
    End Class

    Public Class EmployeeWrapper

        Private _Employee As DevExpress.DemoData.Models.Employee

        Public Shared Function Create(ByVal employee As DevExpress.DemoData.Models.Employee) As EmployeeWrapper
            Return ViewModelSource.Create(Function() New EmployeeWrapper(employee))
        End Function

        Protected Sub New(ByVal employee As DevExpress.DemoData.Models.Employee)
            Me.Employee = employee
        End Sub

        Public Property Employee As DevExpress.DemoData.Models.Employee
            Get
                Return _Employee
            End Get

            Private Set(ByVal value As DevExpress.DemoData.Models.Employee)
                _Employee = value
            End Set
        End Property

        Public Overridable Property BackgroundColor As Color
    End Class
End Namespace
