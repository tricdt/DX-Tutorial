Imports System.Collections

Namespace GridDemo

    Public Class CollectionInfo

        Private _Collection As IEnumerable, _Description As String

        Public Sub New(ByVal collection As IEnumerable, ByVal description As String)
            Me.Collection = collection
            Me.Description = description
        End Sub

        Public Property Collection As IEnumerable
            Get
                Return _Collection
            End Get

            Private Set(ByVal value As IEnumerable)
                _Collection = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _Description
            End Get

            Private Set(ByVal value As String)
                _Description = value
            End Set
        End Property
    End Class
End Namespace
