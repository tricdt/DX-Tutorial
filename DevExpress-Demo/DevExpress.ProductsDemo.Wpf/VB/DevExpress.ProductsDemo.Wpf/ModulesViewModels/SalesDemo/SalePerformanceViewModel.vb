Imports DevExpress.Xpf.Charts
Imports DevExpress.Mvvm
Imports System
Imports System.Windows.Input
Imports DevExpress.SalesDemo.Model

Namespace ProductsDemo

    Public Enum SalesPerformanceViewMode
        Daily
        Monthly
    End Enum

    Public Class SalesPerformanceViewModel
        Inherits ViewModelBase

        Private modeField As SalesPerformanceViewMode

        Private selectedDateField As Date = Date.Now

        Private chartDataSourceField As Object

        Private selectedDateStringField As String

        Private argumentDataMemberField As String

        Private valueDataMemberField As String

        Private secondButtonTextField As String

        Private thirdButtonTextField As String

        Private firstSalesVolumeHeaderField As String

        Private secondSalesVolumeHeaderField As String

        Private thirdSalesVolumeHeaderField As String

        Private firstSalesVolumeField As String

        Private secondSalesVolumeField As String

        Private thirdSalesVolumeField As String

        Private axisXLabelFormatStringField As String

        Private areaSeriesCrosshairLabelPatternField As String

        Private areaSeriesVisibleField As Boolean

        Private barSeriesVisibleField As Boolean

        Private chartSideMarginsEnabledField As Boolean

        Private dateTimeMeasureUnitField As DateTimeMeasureUnit

        Private dateTimeGridAlignmentField As DateTimeGridAlignment

        Private gridSpacing As Double

        Private axisXMinorCountField As Integer

        Private timeForwardCommandField As ICommand

        Private timeBackwardCommandField As ICommand

        Private setCurrentTimePeriodCommandField As ICommand

        Private setLastTimePeriodCommandField As ICommand

        Public Property Mode As SalesPerformanceViewMode
            Get
                Return modeField
            End Get

            Set(ByVal value As SalesPerformanceViewMode)
                SetProperty(modeField, value, "Mode", New Action(AddressOf OnModePropertyChanged))
            End Set
        End Property

        Public Property SelectedDate As Date
            Get
                Return selectedDateField
            End Get

            Set(ByVal value As Date)
                SetProperty(selectedDateField, value, "SelectedDate", New Action(AddressOf OnSelectedDatePropertyChanged))
            End Set
        End Property

        Public Property SelectedDateString As String
            Get
                Return selectedDateStringField
            End Get

            Private Set(ByVal value As String)
                SetProperty(selectedDateStringField, value, "SelectedDateString")
            End Set
        End Property

        Public Property ChartDataSource As Object
            Get
                Return chartDataSourceField
            End Get

            Set(ByVal value As Object)
                SetProperty(chartDataSourceField, value, "ChartDataSource")
            End Set
        End Property

        Public Property ArgumentDataMember As String
            Get
                Return argumentDataMemberField
            End Get

            Set(ByVal value As String)
                SetProperty(argumentDataMemberField, value, "ArgumentDataMember")
            End Set
        End Property

        Public Property ValueDataMember As String
            Get
                Return valueDataMemberField
            End Get

            Set(ByVal value As String)
                SetProperty(valueDataMemberField, value, "ValueDataMember")
            End Set
        End Property

        Public Property SecondButtonText As String
            Get
                Return secondButtonTextField
            End Get

            Private Set(ByVal value As String)
                SetProperty(secondButtonTextField, value, "SecondButtonText")
            End Set
        End Property

        Public Property ThirdButtonText As String
            Get
                Return thirdButtonTextField
            End Get

            Private Set(ByVal value As String)
                SetProperty(thirdButtonTextField, value, "ThirdButtonText")
            End Set
        End Property

        Public Property FirstSalesVolumeHeader As String
            Get
                Return firstSalesVolumeHeaderField
            End Get

            Private Set(ByVal value As String)
                SetProperty(firstSalesVolumeHeaderField, value, "FirstSalesVolumeHeader")
            End Set
        End Property

        Public Property SecondSalesVolumeHeader As String
            Get
                Return secondSalesVolumeHeaderField
            End Get

            Private Set(ByVal value As String)
                SetProperty(secondSalesVolumeHeaderField, value, "SecondSalesVolumeHeader")
            End Set
        End Property

        Public Property ThirdSalesVolumeHeader As String
            Get
                Return thirdSalesVolumeHeaderField
            End Get

            Private Set(ByVal value As String)
                SetProperty(thirdSalesVolumeHeaderField, value, "ThirdSalesVolumeHeader")
            End Set
        End Property

        Public Property FirstSalesVolume As String
            Get
                Return firstSalesVolumeField
            End Get

            Set(ByVal value As String)
                SetProperty(firstSalesVolumeField, value, "FirstSalesVolume")
            End Set
        End Property

        Public Property SecondSalesVolume As String
            Get
                Return secondSalesVolumeField
            End Get

            Set(ByVal value As String)
                SetProperty(secondSalesVolumeField, value, "SecondSalesVolume")
            End Set
        End Property

        Public Property ThirdSalesVolume As String
            Get
                Return thirdSalesVolumeField
            End Get

            Set(ByVal value As String)
                SetProperty(thirdSalesVolumeField, value, "ThirdSalesVolume")
            End Set
        End Property

        Public Property AxisXLabelFormatString As String
            Get
                Return axisXLabelFormatStringField
            End Get

            Set(ByVal value As String)
                SetProperty(axisXLabelFormatStringField, value, "AxisXLabelFormatString")
            End Set
        End Property

        Public Property AreaSeriesCrosshairLabelPattern As String
            Get
                Return areaSeriesCrosshairLabelPatternField
            End Get

            Set(ByVal value As String)
                SetProperty(areaSeriesCrosshairLabelPatternField, value, "AreaSeriesCrosshairLabelPattern")
            End Set
        End Property

        Public Property AreaSeriesVisible As Boolean
            Get
                Return areaSeriesVisibleField
            End Get

            Set(ByVal value As Boolean)
                SetProperty(areaSeriesVisibleField, value, "AreaSeriesVisible", New Action(AddressOf OnSeriesVisibilityChanged))
            End Set
        End Property

        Public Property BarSeriesVisible As Boolean
            Get
                Return barSeriesVisibleField
            End Get

            Set(ByVal value As Boolean)
                SetProperty(barSeriesVisibleField, value, "BarSeriesVisible", New Action(AddressOf OnSeriesVisibilityChanged))
            End Set
        End Property

        Public Property ChartSideMarginsEnabled As Boolean
            Get
                Return chartSideMarginsEnabledField
            End Get

            Set(ByVal value As Boolean)
                SetProperty(chartSideMarginsEnabledField, value, "ChartSideMarginsEnabled")
            End Set
        End Property

        Public Property DateTimeMeasureUnit As DateTimeMeasureUnit
            Get
                Return dateTimeMeasureUnitField
            End Get

            Set(ByVal value As DateTimeMeasureUnit)
                SetProperty(dateTimeMeasureUnitField, value, "DateTimeMeasureUnit")
            End Set
        End Property

        Public Property DateTimeGridAlignment As DateTimeGridAlignment
            Get
                Return dateTimeGridAlignmentField
            End Get

            Set(ByVal value As DateTimeGridAlignment)
                SetProperty(dateTimeGridAlignmentField, value, "DateTimeGridAlignment")
            End Set
        End Property

        Public Property AxisXGridSpacing As Double
            Get
                Return gridSpacing
            End Get

            Set(ByVal value As Double)
                SetProperty(gridSpacing, value, "AxisXGridSpacing")
            End Set
        End Property

        Public Property AxisXMinorCount As Integer
            Get
                Return axisXMinorCountField
            End Get

            Set(ByVal value As Integer)
                SetProperty(axisXMinorCountField, value, "AxisXMinorCount")
            End Set
        End Property

        Public Property TimeForwardCommand As ICommand
            Get
                Return timeForwardCommandField
            End Get

            Private Set(ByVal value As ICommand)
                SetProperty(timeForwardCommandField, value, "TimeForwardCommand")
            End Set
        End Property

        Public Property TimeBackwardCommand As ICommand
            Get
                Return timeBackwardCommandField
            End Get

            Private Set(ByVal value As ICommand)
                SetProperty(timeBackwardCommandField, value, "TimeBackwardCommand")
            End Set
        End Property

        Public Property SetCurrentTimePeriodCommand As ICommand
            Get
                Return setCurrentTimePeriodCommandField
            End Get

            Set(ByVal value As ICommand)
                SetProperty(setCurrentTimePeriodCommandField, value, "SetCurrentTimePeriodCommand")
            End Set
        End Property

        Public Property SetLastTimePeriodCommand As ICommand
            Get
                Return setLastTimePeriodCommandField
            End Get

            Set(ByVal value As ICommand)
                SetProperty(setLastTimePeriodCommandField, value, "SetLastTimePeriodCommand")
            End Set
        End Property

        Public Sub New()
            OnModePropertyChanged()
            TimeForwardCommand = New DelegateCommand(AddressOf OnTimeForwardCommandExecuted, AddressOf CanExecuteTimeForward)
            TimeBackwardCommand = New DelegateCommand(AddressOf OnTimeBackwardCommandExecuted)
            SetCurrentTimePeriodCommand = New DelegateCommand(AddressOf OnSetCurrentTimePeriodCommandExecuted)
            SetLastTimePeriodCommand = New DelegateCommand(AddressOf OnSetLastTimePeriodCommandExecuted)
        End Sub

        Private Sub OnModePropertyChanged()
            SecondButtonText = If(Mode = SalesPerformanceViewMode.Daily, "Yesterday", "Last Month")
            ThirdButtonText = If(Mode = SalesPerformanceViewMode.Daily, "Today", "This Month")
            FirstSalesVolumeHeader = If(Mode = SalesPerformanceViewMode.Daily, "TODAY", "THIS MONTH")
            SecondSalesVolumeHeader = If(Mode = SalesPerformanceViewMode.Daily, "YESTERDAY", "LAST MONTH")
            ThirdSalesVolumeHeader = If(Mode = SalesPerformanceViewMode.Daily, "LAST WEEK", "YTD")
            OnSelectedDatePropertyChanged()
        End Sub

        Private Sub OnSelectedDatePropertyChanged()
            Dim fomatString As String = If(Mode = SalesPerformanceViewMode.Daily, "d", "MMMM, yyyy")
            SelectedDateString = SelectedDate.ToString(fomatString)
        End Sub

        Private Sub OnSeriesVisibilityChanged()
            If AreaSeriesVisible AndAlso Not BarSeriesVisible Then
                ChartSideMarginsEnabled = False
            Else
                ChartSideMarginsEnabled = True
            End If
        End Sub

        Private Function IsCurrentMonth(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return now.Year = [date].Year AndAlso now.Month = [date].Month
        End Function

        Private Function IsToday(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return now.Year = [date].Year AndAlso now.Month = [date].Month AndAlso now.Day = [date].Day
        End Function

        Private Function CanExecuteTimeForward() As Boolean
            If Mode = SalesPerformanceViewMode.Daily Then
                Return Not IsToday(SelectedDate)
            Else
                Return Not IsCurrentMonth(SelectedDate)
            End If
        End Function

        Private Sub OnTimeForwardCommandExecuted()
            If Mode = SalesPerformanceViewMode.Daily Then
                SelectedDate = SelectedDate.AddDays(1)
            Else
                SelectedDate = SelectedDate.AddMonths(1)
            End If
        End Sub

        Private Sub OnTimeBackwardCommandExecuted()
            If Mode = SalesPerformanceViewMode.Daily Then
                SelectedDate = SelectedDate.AddDays(-1)
            Else
                SelectedDate = SelectedDate.AddMonths(-1)
            End If
        End Sub

        Private Sub OnSetCurrentTimePeriodCommandExecuted()
            SelectedDate = Date.Now
        End Sub

        Private Sub OnSetLastTimePeriodCommandExecuted()
            If Mode = SalesPerformanceViewMode.Daily Then
                SelectedDate = Date.Now.AddDays(-1)
            Else
                SelectedDate = Date.Now.AddMonths(-1)
            End If
        End Sub

        Protected Overrides Sub OnInitializeInDesignMode()
            MyBase.OnInitializeInDesignMode()
            OnModePropertyChanged()
            SelectedDate = Date.Now
            Dim dataProvider As IDataProvider = New SampleDataProvider()
            FirstSalesVolume = "1.1M"
            SecondSalesVolume = "2.2M"
            ThirdSalesVolume = "12.3M"
            ValueDataMember = "TotalCost"
            ArgumentDataMember = "StartOfPeriod"
            ChartDataSource = dataProvider.GetSales(Date.Today.AddMonths(-1), Date.Today, GroupingPeriod.Day)
            AreaSeriesVisible = True
        End Sub
    End Class
End Namespace
