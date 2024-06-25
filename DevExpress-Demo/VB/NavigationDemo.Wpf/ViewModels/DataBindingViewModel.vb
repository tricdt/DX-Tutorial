Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports NavigationDemo.Utils

Namespace NavigationDemo

    Public Class DataBindingViewModel

        Private _Departments As ReadOnlyCollection(Of NavigationDemo.EmployeeDepartment)

        Public Property Departments As ReadOnlyCollection(Of EmployeeDepartment)
            Get
                Return _Departments
            End Get

            Private Set(ByVal value As ReadOnlyCollection(Of EmployeeDepartment))
                _Departments = value
            End Set
        End Property

        Public Overridable Property SelectedItem As Object

        Public Sub New()
            Dim departments = EmployeesWithPhotoData.DataSource.GroupBy(Function(x) x.GroupName).[Select](Function(x) CreateEmployeeDepartment(x.Key, x.Take(10).ToArray())).ToArray()
            Me.Departments = New ReadOnlyCollection(Of EmployeeDepartment)(departments)
            SelectedItem = Me.Departments(1).Employees(0)
        End Sub

        Private Shared Function CreateEmployeeDepartment(ByVal name As String, ByVal employees As IList(Of Employee)) As EmployeeDepartment
            Dim image = GetEmployeeDepartmentImage(name)
            Return New EmployeeDepartment(name, image, employees)
        End Function
    End Class

    Public Class EmployeeDepartment

        Private _Name As String, _SvgImage As Uri, _Employees As ReadOnlyCollection(Of DevExpress.Xpf.DemoBase.DataClasses.Employee)

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property SvgImage As Uri
            Get
                Return _SvgImage
            End Get

            Private Set(ByVal value As Uri)
                _SvgImage = value
            End Set
        End Property

        Public Property Employees As ReadOnlyCollection(Of Employee)
            Get
                Return _Employees
            End Get

            Private Set(ByVal value As ReadOnlyCollection(Of Employee))
                _Employees = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal svgImage As Uri, ByVal employees As IList(Of Employee))
            Me.Name = name
            Me.SvgImage = svgImage
            Me.Employees = New ReadOnlyCollection(Of Employee)(employees)
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
