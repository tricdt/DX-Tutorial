Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization
Imports System.Runtime.CompilerServices

Namespace DialogsDemo.Helpers

    <XmlRoot("Filters")>
    Public Class Filters
        Inherits List(Of Filter)

    End Class

    Public Class Filter

        Public Property Description As String

        Public Property Expression As String

        Public ReadOnly Property FilterString As String
            Get
                Return String.Join(FiltersHelper.FilterSeparator, Description, Expression)
            End Get
        End Property

        Public Property IsEnable As Boolean
    End Class

    Public Module FiltersHelper

        Public Const FilterSeparator As String = "|"

        Public ReadOnly PhotoFilters As Filters

        Sub New()
            Using stream = DialogsDemoHelper.GetDataStream("PhotoFilters.xml")
                Dim serializer = New XmlSerializer(GetType(Filters))
                PhotoFilters = CType(serializer.Deserialize(stream), Filters)
            End Using
        End Sub

        <Extension()>
        Public Function GetFilterString(ByVal filters As Filters) As String
            Return filters.Where(Function(x) x.IsEnable).Aggregate(String.Empty, Function(a, x) String.Join(If(String.IsNullOrEmpty(a), String.Empty, FiltersHelper.FilterSeparator), a, x.FilterString))
        End Function
    End Module
End Namespace
