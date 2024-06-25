Imports System
Imports DevExpress.Mvvm
Imports System.Linq
Imports DevExpress.Diagram.Demos

Namespace DiagramDemo

    Public Class OrgChartViewModel
        Inherits ViewModelBase

        Private _Templates As DiagramDemo.EmployeeTemplate(), _Employees As DevExpress.Diagram.Demos.Employee()

        Public Event SelectedTemplateChanged As EventHandler

        Public Property Templates As EmployeeTemplate()
            Get
                Return _Templates
            End Get

            Private Set(ByVal value As EmployeeTemplate())
                _Templates = value
            End Set
        End Property

        Public Overridable Property SelectedTemplate As EmployeeTemplate

        Public Property Employees As Employee()
            Get
                Return _Employees
            End Get

            Private Set(ByVal value As Employee())
                _Employees = value
            End Set
        End Property

        Public Overridable Property SelectedEmployee As Employee

        Public Sub New(ByVal employees As Employee(), ByVal templates As EmployeeTemplate())
            Me.Employees = employees
            Me.Templates = templates
            SelectedTemplate = Me.Templates.FirstOrDefault()
        End Sub

        Protected Sub OnSelectedTemplateChanged()
            RaiseEvent SelectedTemplateChanged(Me, EventArgs.Empty)
        End Sub
    End Class

    Public Class EmployeeTemplate

        Private _Name As String, _Image As Object

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Image As Object
            Get
                Return _Image
            End Get

            Private Set(ByVal value As Object)
                _Image = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal image As Object)
            Me.Name = name
            Me.Image = image
        End Sub
    End Class
End Namespace
