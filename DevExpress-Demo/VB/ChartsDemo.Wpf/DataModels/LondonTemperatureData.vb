Imports System.Collections.Generic
Imports System.Xml.Serialization

Namespace ChartsDemo

    <XmlRoot("TemperatureData")>
    Public Class TemperatureData
        Inherits List(Of TemperaturesInfo)

        Public Shared Function Load() As TemperatureData
            Dim serializer As XmlSerializer = XmlSerializer.FromTypes({GetType(TemperatureData)})(0)
            Return CType(serializer.Deserialize(LoadFromResources("/Data/LondonTemperature.xml")), TemperatureData)
        End Function
    End Class

    Public Class TemperaturesInfo

        Public Property Description As String

        Public Property Items As List(Of TemperatureInfo)
    End Class

    Public Class TemperatureInfo

        Public Property [Date] As Date

        Public Property Temperature As Integer
    End Class
End Namespace
