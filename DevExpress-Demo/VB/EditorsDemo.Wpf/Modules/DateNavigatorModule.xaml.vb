Imports System
Imports System.Collections.Generic
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.DateNavigator

Namespace EditorsDemo

    Public Partial Class DateNavigatorModule
        Inherits EditorsDemoModule

        Private disableWeekend As Boolean

        Private highlightUSHolidays As Boolean

        Public Sub New()
            InitializeComponent()
            AddHandler navigator.RequestCellState, AddressOf NavigatorOnRequestCellState
            styleSettingsComboBox.ItemsSource = New List(Of StyleSettingsViewModel)() From {New StyleSettingsViewModel() With {.Name = "Outlook", .Value = New DateNavigatorOutlookStyleSettings()}, New StyleSettingsViewModel() With {.Name = "Classic", .Value = New DateNavigatorStyleSettings()}}
            styleSettingsComboBox.SelectedIndex = 0
        End Sub

        Private Sub NavigatorOnRequestCellState(ByVal sender As Object, ByVal args As DateNavigatorRequestCellStateEventArgs)
            If disableWeekend AndAlso (args.DateTime.DayOfWeek = DayOfWeek.Saturday OrElse args.DateTime.DayOfWeek = DayOfWeek.Sunday) Then args.CellState = args.CellState Or DateNavigatorCellState.IsDisabled
            If highlightUSHolidays AndAlso IsUSHoliday(args.DateTime) Then args.CellState = args.CellState Or DateNavigatorCellState.IsHoliday
        End Sub

        Private Sub OnDisabledWeekendEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            disableWeekend = CBool(e.NewValue)
            navigator.RefreshCellStates()
        End Sub

        Private Sub OnHighlightUSHolidaysEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            highlightUSHolidays = CBool(e.NewValue)
            navigator.RefreshCellStates()
        End Sub

        Private Sub OnStyleSettingsEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            navigator.StyleSettings = CType(e.NewValue, DateNavigatorStyleSettingsBase)
        End Sub

        Private Function IsUSHoliday(ByVal [date] As Date) As Boolean
            Dim nthWeekDay As Integer = CInt((Math.Ceiling(CDbl([date].Day) / 7.0R)))
            Dim dayName As DayOfWeek = [date].DayOfWeek
            Dim isThursday As Boolean = dayName = DayOfWeek.Thursday
            Dim isFriday As Boolean = dayName = DayOfWeek.Friday
            Dim isMonday As Boolean = dayName = DayOfWeek.Monday
            Dim isWeekend As Boolean = dayName = DayOfWeek.Saturday OrElse dayName = DayOfWeek.Sunday
            If [date].Month = 12 AndAlso [date].Day = 31 AndAlso isFriday OrElse [date].Month = 1 AndAlso [date].Day = 1 AndAlso Not isWeekend OrElse [date].Month = 1 AndAlso [date].Day = 2 AndAlso isMonday Then Return True
            If [date].Month = 1 AndAlso isMonday AndAlso nthWeekDay = 3 Then Return True
            If [date].Month = 2 AndAlso isMonday AndAlso nthWeekDay = 3 Then Return True
            If [date].Month = 5 AndAlso isMonday AndAlso [date].AddDays(7).Month = 6 Then Return True
            If [date].Month = 7 AndAlso [date].Day = 3 AndAlso isFriday OrElse [date].Month = 7 AndAlso [date].Day = 4 AndAlso Not isWeekend OrElse [date].Month = 7 AndAlso [date].Day = 5 AndAlso isMonday Then Return True
            If [date].Month = 9 AndAlso isMonday AndAlso nthWeekDay = 1 Then Return True
            If [date].Month = 10 AndAlso isMonday AndAlso nthWeekDay = 2 Then Return True
            If [date].Month = 11 AndAlso [date].Day = 10 AndAlso isFriday OrElse [date].Month = 11 AndAlso [date].Day = 11 AndAlso Not isWeekend OrElse [date].Month = 11 AndAlso [date].Day = 12 AndAlso isMonday Then Return True
            If [date].Month = 11 AndAlso isThursday AndAlso nthWeekDay = 4 Then Return True
            If [date].Month = 12 AndAlso [date].Day = 24 AndAlso isFriday OrElse [date].Month = 12 AndAlso [date].Day = 25 AndAlso Not isWeekend OrElse [date].Month = 12 AndAlso [date].Day = 26 AndAlso isMonday Then Return True
            Return False
        End Function
    End Class

    Public Class StyleSettingsViewModel

        Public Property Name As String

        Public Property Value As DateNavigatorStyleSettingsBase
    End Class
End Namespace
