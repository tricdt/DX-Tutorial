Imports DevExpress.Xpf.Charts.Sankey
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media

Namespace SankeyDemo

    Public Class ContinentColorizer
        Inherits SankeyColorizerBase

        Private continentColorPairsField As Dictionary(Of String, Color) = New Dictionary(Of String, Color)()

        Private continentCountriesPairsField As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))()

        Public Property ContinentColorPairs As Dictionary(Of String, Color)
            Get
                Return continentColorPairsField
            End Get

            Set(ByVal value As Dictionary(Of String, Color))
                continentColorPairsField = value
            End Set
        End Property

        Public Property ContinentCountriesPairs As Dictionary(Of String, List(Of String))
            Get
                Return continentCountriesPairsField
            End Get

            Set(ByVal value As Dictionary(Of String, List(Of String)))
                continentCountriesPairsField = value
            End Set
        End Property

        Public Overrides Function GetLinkSourceColor(ByVal link As SankeyLink) As Color
            Dim country = link.SourceNode.Tag.ToString()
            Dim continent As String = GetContinentByCountry(country)
            If Not Equals(continent, "") AndAlso continentColorPairsField.ContainsKey(continent) Then Return continentColorPairsField(continent)
            Return New Color()
        End Function

        Public Overrides Function GetLinkTargetColor(ByVal link As SankeyLink) As Color
            Dim country = link.TargetNode.Tag.ToString()
            Dim continent As String = GetContinentByCountry(country)
            If Not Equals(continent, "") AndAlso continentColorPairsField.ContainsKey(continent) Then Return continentColorPairsField(continent)
            Return New Color()
        End Function

        Public Overrides Function GetNodeColor(ByVal info As SankeyNode) As Color
            Dim country = info.Tag.ToString()
            Dim continent As String = GetContinentByCountry(country)
            If Not Equals(continent, "") AndAlso continentColorPairsField.ContainsKey(continent) Then Return continentColorPairsField(continent)
            Return New Color()
        End Function

        Private Function GetContinentByCountry(ByVal country As String) As String
            If continentColorPairsField.ContainsKey(country) Then Return country
            For Each continentCountryPairs In continentCountriesPairsField
                If continentCountryPairs.Value.Contains(country) Then Return continentCountryPairs.Key
            Next

            Return ""
        End Function
    End Class

    Friend Class GradientColorizer
        Inherits SankeyColorizerBase

        Public Shared Function Create(ByVal dataSource As List(Of Export)) As GradientColorizer
            Dim colorizer As GradientColorizer = New GradientColorizer()
            If dataSource.Count <> 0 Then
                Dim exportCountryValues As Dictionary(Of String, Double) = New Dictionary(Of String, Double)()
                Dim importCountryValues As Dictionary(Of String, Double) = New Dictionary(Of String, Double)()
                For Each export In dataSource
                    If Not exportCountryValues.ContainsKey(export.Exporter) Then exportCountryValues.Add(export.Exporter, 0)
                    exportCountryValues(export.Exporter) += export.Sum
                    If Not importCountryValues.ContainsKey(export.Importer) Then importCountryValues.Add(export.Importer, 0)
                    importCountryValues(export.Importer) += export.Sum
                Next

                Dim values = exportCountryValues.Values.ToList()
                values.AddRange(importCountryValues.Values)
                colorizer.MinValue = values.Min()
                colorizer.MaxValue = values.Max()
            End If

            Return colorizer
        End Function

        Public Property MinValue As Double

        Public Property MaxValue As Double

        Private Sub New()
        End Sub

        Private Function GetGradientColor(ByVal percent As Double) As Color
            percent = If(Double.IsNaN(percent), 1, percent)
            Dim color1 As Color = Color.FromArgb(255, 96, 181, 204)
            Dim color2 As Color = Color.FromArgb(255, 230, 108, 125)
            Dim resultRed As Double = color1.R * (1.0 - percent) + color2.R * percent
            Dim resultGreen As Double = color1.G * (1.0 - percent) + color2.G * percent
            Dim resultBlue As Double = color1.B * (1.0 - percent) + color2.B * percent
            Return Color.FromRgb(CByte(resultRed), CByte(resultGreen), CByte(resultBlue))
        End Function

        Public Overrides Function GetLinkSourceColor(ByVal link As SankeyLink) As Color
            Return GetGradientColor((link.SourceNode.TotalWeight - MinValue) / (MaxValue - MinValue))
        End Function

        Public Overrides Function GetLinkTargetColor(ByVal link As SankeyLink) As Color
            Return GetGradientColor((link.TargetNode.TotalWeight - MinValue) / (MaxValue - MinValue))
        End Function

        Public Overrides Function GetNodeColor(ByVal node As SankeyNode) As Color
            Return GetGradientColor((node.TotalWeight - MinValue) / (MaxValue - MinValue))
        End Function
    End Class

    Public Module ContinentCountries

        Public Function GetContinentCountriesPairs_ColorizerDemo() As Dictionary(Of String, List(Of String))
            Dim continentCountriesPairs As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))()
            continentCountriesPairs.Add("North America", New List(Of String)() From {"United States", "Canada", "Mexico"})
            continentCountriesPairs.Add("Asia", New List(Of String)() From {"China", "Japan", "South Korea"})
            continentCountriesPairs.Add("South America", New List(Of String)() From {"Brazil", "Argentina"})
            continentCountriesPairs.Add("Australia", New List(Of String)() From {"Australia"})
            continentCountriesPairs.Add("Europe", New List(Of String)() From {"Netherlands", "Germany", "United Kingdom", "Italy", "France", "Spain", "United Kingdom"})
            Return continentCountriesPairs
        End Function

        Public Function GetContinentColorPairs_ColorizerDemo() As Dictionary(Of String, Color)
            Dim continentColorPairs = New Dictionary(Of String, Color)()
            continentColorPairs.Add("Asia", Color.FromRgb(245, 86, 74))
            continentColorPairs.Add("North America", Color.FromRgb(29, 178, 245))
            continentColorPairs.Add("South America", Color.FromRgb(151, 201, 92))
            continentColorPairs.Add("Australia", Color.FromRgb(198, 144, 83))
            continentColorPairs.Add("Europe", Color.FromRgb(255, 199, 32))
            Return continentColorPairs
        End Function

        Public Function GetContinentCountriesPairs_InteractionDemo() As Dictionary(Of String, List(Of String))
            Dim continentCountriesPairs As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))()
            continentCountriesPairs.Add("North America", New List(Of String)() From {"United States", "Canada", "Mexico"})
            continentCountriesPairs.Add("South and Central America", New List(Of String)() From {"Brazil", "Argentina"})
            continentCountriesPairs.Add("Australasia", New List(Of String)() From {"Australia"})
            continentCountriesPairs.Add("Europe", New List(Of String)() From {"Netherlands", "Germany", "United Kingdom", "Italy", "France", "Spain", "United Kingdom"})
            continentCountriesPairs.Add("Asia Pacific", New List(Of String)() From {"China", "Japan", "South Korea", "India", "Singapore", "Other Asia Pacific"})
            continentCountriesPairs.Add("Middle East", New List(Of String)() From {"Saudi Arabia", "UAE", "United Arab Emirates", "Kuwait", "Iraq"})
            continentCountriesPairs.Add("CIS", New List(Of String)() From {"Russia", "Other CIS"})
            Return continentCountriesPairs
        End Function

        Public Function GetContinentColorPairs_InteractionDemo() As Dictionary(Of String, Color)
            Dim continentColorPairs = New Dictionary(Of String, Color)()
            continentColorPairs = New Dictionary(Of String, Color)()
            continentColorPairs.Add("North America", Color.FromRgb(29, 178, 245))
            continentColorPairs.Add("South and Central America", Color.FromRgb(151, 201, 92))
            continentColorPairs.Add("Europe", Color.FromRgb(255, 199, 32))
            continentColorPairs.Add("CIS", Color.FromRgb(44, 115, 255))
            continentColorPairs.Add("Middle East", Color.FromRgb(186, 85, 211))
            continentColorPairs.Add("West Africa", Color.FromRgb(255, 126, 32))
            continentColorPairs.Add("North Africa", Color.FromRgb(51, 204, 170))
            continentColorPairs.Add("Africa", Color.FromRgb(103, 113, 220))
            continentColorPairs.Add("Australasia", Color.FromRgb(198, 144, 83))
            continentColorPairs.Add("Asia Pacific", Color.FromRgb(245, 86, 74))
            Return continentColorPairs
        End Function
    End Module
End Namespace
