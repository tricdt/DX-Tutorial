Imports System.Collections.Generic
Imports System.IO
Imports System.Xml.Serialization

Namespace MapDemo

    Public Class EarthquakeViewModel

        <XmlElement("yr")>
        Public Property Year As Integer

        <XmlElement("mon")>
        Public Property Month As Integer

        <XmlElement("day")>
        Public Property Day As Integer

        <XmlElement("hr")>
        Public Property Hour As Integer

        <XmlElement("min")>
        Public Property Minute As Integer

        <XmlElement("sec")>
        Public Property Second As Double

        <XmlElement("glat")>
        Public Property Latitude As Double

        <XmlElement("glon")>
        Public Property Longitude As Double

        <XmlElement("dep")>
        Public Property Depth As Double

        <XmlElement("mag")>
        Public Property Magnitude As Double
    End Class

    <XmlRoot("Data")>
    Public Class EarthquakesData

        Private Shared instanceField As EarthquakesData

        Public Shared ReadOnly Property DataItems As List(Of EarthquakeViewModel)
            Get
                Return Instance.Items
            End Get
        End Property

        Public Shared ReadOnly Property Instance As EarthquakesData
            Get
                Return If(instanceField, Function()
                    instanceField = CreateInstance()
                    Return instanceField
                End Function())
            End Get
        End Property

        Private Shared Function CreateInstance() As EarthquakesData
            Dim serializer As XmlSerializer = New XmlSerializer(GetType(EarthquakesData))
            Dim documentStream As Stream = LoadStreamFromResources("/Data/Earthquakes.xml")
            Return CType(serializer.Deserialize(documentStream), EarthquakesData)
        End Function

        <XmlElement("Row")>
        Public Property Items As List(Of EarthquakeViewModel)

        Private Sub New()
        End Sub
    End Class
End Namespace
