Imports System.Collections.Generic
Imports System.Globalization
Imports System.Xml.Linq

Namespace ChartsDemo

    Friend Class ExtremeWeatherData

        Private Function Load(ByVal fileName As String) As List(Of WeatherRecord)
            Dim items As List(Of WeatherRecord) = New List(Of WeatherRecord)()
            Dim weatherDocument As XDocument = LoadXmlFromResources("/Data/DailyWeather/" & fileName)
            For Each element As XElement In weatherDocument.Root.Elements("Weather")
                items.Add(WeatherRecord.Load(element))
            Next

            Return items
        End Function

        Public Function LoadDeathValleyData() As List(Of WeatherRecord)
            Return Load("DeathValley.xml")
        End Function

        Public Function LoadVostokStationData() As List(Of WeatherRecord)
            Return Load("VostokStation.xml")
        End Function
    End Class

    Public Class WeatherRecord

        Private _MinValue As Double, _MaxValue As Double, _AvgValue As Double

        Public Shared Function Load(ByVal element As XElement) As WeatherRecord
            Dim culture As CultureInfo = CultureInfo.InvariantCulture
            Dim _date As Date = Date.Parse(element.Attribute("Date").Value, culture)
            Dim min As Double = Double.Parse(element.Attribute("Min").Value, culture)
            Dim max As Double = Double.Parse(element.Attribute("Max").Value, culture)
            Dim avg As Double = Double.Parse(element.Attribute("Avg").Value, culture)
            Return New WeatherRecord(_date, max, avg, min)
        End Function

        Private dateField As Date

        Public Property MinValue As Double
            Get
                Return _MinValue
            End Get

            Private Set(ByVal value As Double)
                _MinValue = value
            End Set
        End Property

        Public Property MaxValue As Double
            Get
                Return _MaxValue
            End Get

            Private Set(ByVal value As Double)
                _MaxValue = value
            End Set
        End Property

        Public Property AvgValue As Double
            Get
                Return _AvgValue
            End Get

            Private Set(ByVal value As Double)
                _AvgValue = value
            End Set
        End Property

        Public Property [Date] As Date
            Get
                Return dateField
            End Get

            Private Set(ByVal value As Date)
                dateField = value
            End Set
        End Property

        Private Sub New(ByVal _date As Date, ByVal maxValue As Double, ByVal avgValue As Double, ByVal minValue As Double)
            [Date] = _date
            Me.MaxValue = maxValue
            Me.AvgValue = avgValue
            Me.MinValue = minValue
        End Sub
    End Class
End Namespace
