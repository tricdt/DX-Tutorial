Imports DevExpress.Mvvm
Imports DevExpress.Diagram.Demos
Imports DevExpress.Mvvm.DataAnnotations

Namespace DiagramDemo

    Public Class RelationshipDiagramViewModel
        Inherits ViewModelBase

        Private _Employees As DevExpress.Diagram.Demos.Employee(), _Relationships As DevExpress.Diagram.Demos.RelationshipInfo()

        Public Property Employees As Employee()
            Get
                Return _Employees
            End Get

            Private Set(ByVal value As Employee())
                _Employees = value
            End Set
        End Property

        Public Property Relationships As RelationshipInfo()
            Get
                Return _Relationships
            End Get

            Private Set(ByVal value As RelationshipInfo())
                _Relationships = value
            End Set
        End Property

        <BindableProperty>
        Public Overridable Property FriendsCollection As Employee()

        <BindableProperty>
        Public Overridable Property KnownPersonsCollection As Employee()

        Public Overridable Property SelectedEmployee As Employee

        Public Sub New(ByVal employees As Employee())
            Me.Employees = GetEmployees()
            Relationships = GetRelationships(Me.Employees)
        End Sub

        Protected Sub OnSelectedEmployeeChanged()
            FriendsCollection = GetEmployeeRelationships(SelectedEmployee, Relationships, EmployeeRelationship.Friends)
            KnownPersonsCollection = GetEmployeeRelationships(SelectedEmployee, Relationships, EmployeeRelationship.KnowEachOther)
        End Sub
    End Class
End Namespace
