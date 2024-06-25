Imports System.Collections.Generic
Imports System.Xml.Serialization

Namespace ChartsDemo

    <XmlRoot("Comments")>
    Public Class CommentsData
        Inherits List(Of CommentsInfo)

        Public Shared Function Load() As CommentsData
            Dim serializer As XmlSerializer = XmlSerializer.FromTypes({GetType(CommentsData)})(0)
            Return CType(serializer.Deserialize(LoadFromResources("/Data/CommentsData.xml")), CommentsData)
        End Function
    End Class

    Public Class CommentsInfo

        Public Property Category As String

        Public Property Items As List(Of CommentInfo)
    End Class

    Public Class CommentInfo

        Public Property [Date] As Date

        Public Property CommentCount As Integer
    End Class
End Namespace
