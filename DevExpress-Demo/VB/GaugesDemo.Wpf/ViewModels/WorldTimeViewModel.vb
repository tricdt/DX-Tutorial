Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Threading
Imports DevExpress.Mvvm

Namespace GaugesDemo

    Public Class WorldTimeViewModel

        Private _RomanLabels As IEnumerable(Of GaugesDemo.HourLabelData)

        Public Property RomanLabels As IEnumerable(Of HourLabelData)
            Get
                Return _RomanLabels
            End Get

            Private Set(ByVal value As IEnumerable(Of HourLabelData))
                _RomanLabels = value
            End Set
        End Property

        Public Overridable Property ShowLabels As Boolean

        Public Overridable Property ShowCustomLabels As Boolean

        Public Overridable Property ShowRegularLabels As Boolean

        Public Overridable Property NewYorkTime As Date

        Public Overridable Property LondonTime As Date

        Public Overridable Property MoscowTime As Date

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Protected Sub New()
            RomanLabels = Enumerable.Range(1, 12).[Select](Function(x) New HourLabelData(x)).ToArray()
            ShowLabels = True
            UpdateTime()
            timer.Interval = TimeSpan.FromSeconds(1)
        End Sub

        Protected Sub OnShowLabelsChanged()
            UpdateLabelsVisiblity()
        End Sub

        Protected Sub OnShowCustomLabelsChanged()
            UpdateLabelsVisiblity()
        End Sub

        Private Sub UpdateLabelsVisiblity()
            For Each labelData As HourLabelData In RomanLabels
                labelData.Visible = ShowLabels AndAlso ShowCustomLabels
            Next

            ShowRegularLabels = ShowLabels AndAlso Not ShowCustomLabels
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateTime()
        End Sub

        Private Function ConvertToTimeZone(ByVal utcTime As Date, ByVal timeZone As String) As Date
            Return TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone))
        End Function

        Private Sub UpdateTime()
            Dim nowUtc As Date = Date.UtcNow
            NewYorkTime = ConvertToTimeZone(nowUtc, "Eastern Standard Time")
            LondonTime = ConvertToTimeZone(nowUtc, "GMT Standard Time")
            MoscowTime = ConvertToTimeZone(nowUtc, "Russian Standard Time")
        End Sub

        Public Sub Start()
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Start()
        End Sub

        Public Sub [Stop]()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf OnTimedEvent
        End Sub
    End Class

    Public Class HourLabelData
        Inherits BindableBase

        Private _Hour As Integer, _RomanHour As String

        Public Property Hour As Integer
            Get
                Return _Hour
            End Get

            Private Set(ByVal value As Integer)
                _Hour = value
            End Set
        End Property

        Public Property RomanHour As String
            Get
                Return _RomanHour
            End Get

            Private Set(ByVal value As String)
                _RomanHour = value
            End Set
        End Property

        Public Property Visible As Boolean
            Get
                Return GetProperty(Function() Me.Visible)
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Function() Visible, value)
            End Set
        End Property

        Public Sub New(ByVal hour As Integer)
            Me.Hour = hour
            RomanHour = romanHours(hour - 1)
        End Sub

        Private Shared romanHours As String() = New String() {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII"}
    End Class

    Public MustInherit Class DateTimeToUnitConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Protected MustOverride Function ConvertCore(ByVal dateTime As Date) As Double

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Date Then Return ConvertCore(CDate(value))
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class DateTimeToSecondConverter
        Inherits DateTimeToUnitConverter

        Protected Overrides Function ConvertCore(ByVal dateTime As Date) As Double
            Return CDbl(dateTime.Second) / 60.0 * 12.0
        End Function
    End Class

    Public Class DateTimeToMinuteConverter
        Inherits DateTimeToUnitConverter

        Protected Overrides Function ConvertCore(ByVal dateTime As Date) As Double
            Return CDbl(dateTime.Minute) / 60.0 * 12.0
        End Function
    End Class

    Public Class DateTimeToHourConverter
        Inherits DateTimeToUnitConverter

        Protected Overrides Function ConvertCore(ByVal dateTime As Date) As Double
            Return dateTime.Hour Mod 12
        End Function
    End Class
End Namespace
