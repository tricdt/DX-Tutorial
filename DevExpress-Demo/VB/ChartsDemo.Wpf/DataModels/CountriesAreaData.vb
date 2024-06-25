Imports System.Collections.Generic
Imports System.Xml.Serialization

Namespace ChartsDemo

    <XmlRoot("Countries")>
    Public Class CountriesAreaData
        Inherits List(Of Country)

        Private Shared Function CompareCountriesByArea(ByVal country1 As Country, ByVal country2 As Country) As Integer
            Return country2.Area.CompareTo(country1.Area)
        End Function

        Public Shared Function Load() As CountriesAreaData
            Dim serializer As XmlSerializer = XmlSerializer.FromTypes({GetType(CountriesAreaData)})(0)
            Dim data As CountriesAreaData = CType(serializer.Deserialize(LoadFromResources("/Data/Countries.xml")), CountriesAreaData)
            data.Sort(New System.Comparison(Of Country)(AddressOf CompareCountriesByArea))
            Return data
        End Function
    End Class

    Public Class Country

        Public Property Area As Double

        Public Property Name As String

        Public Property OfficialName As String
    End Class
End Namespace
