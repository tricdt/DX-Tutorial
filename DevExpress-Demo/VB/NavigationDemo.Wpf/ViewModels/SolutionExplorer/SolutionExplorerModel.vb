Imports System.Collections.Generic

Namespace NavigationDemo

    Public Enum TypeNode
        File
        [Class]
        [Property]
        [Event]
        Field
        Method
        [Enum]
        EnumElement
    End Enum

    Public Class SolutionNode

        Public Property TypeNode As TypeNode

        Public Property Name As String

        Public Property TypeName As String

        Public Property FileName As String

        Private _children As List(Of SolutionNode)

        Public ReadOnly Property Children As IEnumerable(Of SolutionNode)
            Get
                Return _children
            End Get
        End Property

        Public ReadOnly Property DisplayName As String
            Get
                Return If(String.IsNullOrEmpty(TypeName), Name, String.Format("{0} : {1}", Name, TypeName))
            End Get
        End Property

        Public Property SearchString As String

        Public Property SearchName As String

        Public Sub AddChildren(ByVal child As SolutionNode)
            If _children Is Nothing Then _children = New List(Of SolutionNode)()
            _children.Add(child)
        End Sub
    End Class
End Namespace
