Imports System.Collections.Generic
Imports System.Linq

Namespace DevExpress.Diagram.Demos

    Public Module RelationshipsData

        Public Function GetEmployees() As Employee()
            Return FilteredEmployees.Take(9).ToArray()
        End Function

        Public Function GetRelationships(ByVal employees As Employee()) As RelationshipInfo()
            Dim index As Integer = 0
            Dim relations = New List(Of RelationshipInfo)()
            For i As Integer = 0 To employees.Length - 1
                For j As Integer = i + 1 To employees.Length - 1
                    If index Mod 4 <= 1 Then relations.Add(New RelationshipInfo(employees(i), employees(j), CType(index Mod 4, EmployeeRelationship)))
                    index += 1
                Next
            Next

            Return relations.ToArray()
        End Function

        Public Function GetEmployeeRelationships(ByVal employee As Employee, ByVal relationships As IEnumerable(Of RelationshipInfo), ByVal relationshipKind As EmployeeRelationship) As Employee()
            Return relationships.Where(Function(x) x.Relationship = relationshipKind).[Select](Function(x) GetRelationship(employee, x)).Where(Function(x) x IsNot Nothing).ToArray()
        End Function

        Private Function GetRelationship(ByVal employee As Employee, ByVal x As RelationshipInfo) As Employee
            If x.Source Is employee Then Return x.Target
            If x.Target Is employee Then Return x.Source
            Return Nothing
        End Function
    End Module

    Public Class RelationshipInfo

        Private _Source As Employee, _Target As Employee, _Relationship As EmployeeRelationship

        Public Sub New(ByVal source As Employee, ByVal target As Employee, ByVal relationship As EmployeeRelationship)
            Me.Source = source
            Me.Target = target
            Me.Relationship = relationship
        End Sub

        Public Property Source As Employee
            Get
                Return _Source
            End Get

            Private Set(ByVal value As Employee)
                _Source = value
            End Set
        End Property

        Public Property Target As Employee
            Get
                Return _Target
            End Get

            Private Set(ByVal value As Employee)
                _Target = value
            End Set
        End Property

        Public Property Relationship As EmployeeRelationship
            Get
                Return _Relationship
            End Get

            Private Set(ByVal value As EmployeeRelationship)
                _Relationship = value
            End Set
        End Property
    End Class

    Public Enum EmployeeRelationship
        KnowEachOther
        Friends
    End Enum
End Namespace
