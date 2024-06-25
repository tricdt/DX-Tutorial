Imports System.Collections.Generic
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class DailyWeatherViewModel

        Const vostokStationName As String = "Vostok Station"

        Const deathValleyName As String = "Death Valley, NV"

        Private ReadOnly weatherField As List(Of WeatherItem)

        Public ReadOnly Property Weather As List(Of WeatherItem)
            Get
                Return weatherField
            End Get
        End Property

        Public Sub New()
            Dim data As ExtremeWeatherData = New ExtremeWeatherData()
            Dim valleyData As List(Of WeatherRecord) = data.LoadDeathValleyData()
            Dim vostokData As List(Of WeatherRecord) = data.LoadVostokStationData()
            Dim palette As Palette = New OfficePalette()
            weatherField = New List(Of WeatherItem)() From {New WeatherItem(valleyData, False, palette(1), deathValleyName), New WeatherItem(valleyData, True, palette(1), deathValleyName), New WeatherItem(vostokData, False, palette(0), vostokStationName), New WeatherItem(vostokData, True, palette(0), vostokStationName)}
        End Sub

        Public Overridable ReadOnly Property ChartAnimationService As IChartAnimationService
            Get
                Return Nothing
            End Get
        End Property

        Public Sub OnModuleLoaded()
            If ChartAnimationService IsNot Nothing Then ChartAnimationService.Animate()
        End Sub
    End Class

    Public Class WeatherItem
        Inherits BindableBase

        Private _Data As List(Of ChartsDemo.WeatherRecord), _IsAverageWeather As Boolean, _Color As Color, _Name As String

        Public Property AverageLineThickness As Integer
            Get
                Return GetProperty(Function() Me.AverageLineThickness)
            End Get

            Set(ByVal value As Integer)
                SetProperty(Function() AverageLineThickness, value)
            End Set
        End Property

        Public Property Data As List(Of WeatherRecord)
            Get
                Return _Data
            End Get

            Private Set(ByVal value As List(Of WeatherRecord))
                _Data = value
            End Set
        End Property

        Public Property IsAverageWeather As Boolean
            Get
                Return _IsAverageWeather
            End Get

            Private Set(ByVal value As Boolean)
                _IsAverageWeather = value
            End Set
        End Property

        Public Property Color As Color
            Get
                Return _Color
            End Get

            Private Set(ByVal value As Color)
                _Color = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Sub New(ByVal data As List(Of WeatherRecord), ByVal isAverageWeather As Boolean, ByVal color As Color, ByVal name As String)
            Me.Data = data
            Me.IsAverageWeather = isAverageWeather
            Me.Color = color
            Me.Name = name
            AverageLineThickness = 2
        End Sub
    End Class
End Namespace
