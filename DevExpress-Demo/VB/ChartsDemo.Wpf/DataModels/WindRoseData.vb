Imports System.Collections.Generic
Imports System.Xml.Serialization

Namespace ChartsDemo

    <XmlRoot("WindRoseData")>
    Public Class WindRoseData
        Inherits List(Of WindInfo)

        Public Shared Function Load() As WindRoseData
            Dim serializer As XmlSerializer = XmlSerializer.FromTypes({GetType(WindRoseData)})(0)
            Return CType(serializer.Deserialize(LoadFromResources("/Data/WindRoseData.xml")), WindRoseData)
        End Function
    End Class

    Public Class WindInfo

        Public Property Direction As String

        Public Property Speed As Integer
    End Class
End Namespace
