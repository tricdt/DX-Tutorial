Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows.Threading
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class MvvmFinancialChartingViewModel
        Implements IChartDemoViewModel

        Private _StockData As ObservableCollection(Of ChartsDemo.FinancialDataPoint), _AllIndicators As List(Of ChartsDemo.IndicatorItem), _SeparatePaneIndicators As List(Of ChartsDemo.IndicatorItem), _SideMarginsValue As Double

        Public Shared Function Create() As MvvmFinancialChartingViewModel
            Return ViewModelSource.Create(Function() New MvvmFinancialChartingViewModel())
        End Function

        Const InitialPointsOnScreen As Integer = 60

        Const MaxPointsOnScreen As Integer = 90

        Private ReadOnly timer As DispatcherTimer = ChartsDemoModule.CreateTimer()

        Private ReadOnly generator As RealTimeFinancialDataGenerator

        Public Property StockData As ObservableCollection(Of FinancialDataPoint)
            Get
                Return _StockData
            End Get

            Private Set(ByVal value As ObservableCollection(Of FinancialDataPoint))
                _StockData = value
            End Set
        End Property

        Public Property AllIndicators As List(Of IndicatorItem)
            Get
                Return _AllIndicators
            End Get

            Private Set(ByVal value As List(Of IndicatorItem))
                _AllIndicators = value
            End Set
        End Property

        Public Property SeparatePaneIndicators As List(Of IndicatorItem)
            Get
                Return _SeparatePaneIndicators
            End Get

            Private Set(ByVal value As List(Of IndicatorItem))
                _SeparatePaneIndicators = value
            End Set
        End Property

        Public Property SideMarginsValue As Double
            Get
                Return _SideMarginsValue
            End Get

            Private Set(ByVal value As Double)
                _SideMarginsValue = value
            End Set
        End Property

        Public Overridable Property VisualXRangeMin As Date

        Public Overridable Property VisualXRangeMax As Date

        Public Overridable Property AxisXMeasureUnit As DateTimeMeasureUnit

        Public Overridable Property AxisXMeasureUnitMultiplier As Integer

        Public Overridable Property CurrentPrice As Double

        Public Overridable Property PriceAxisYSideMargin As Double

        Public Overridable Property VolumeAxisYSideMargin As Double

        Public Overridable Property AxisXLabelPattern As String

        Protected Sub New()
            generator = New RealTimeFinancialDataGenerator()
            generator.ReadInitialData()
            StockData = generator.DataSource
            AddHandler timer.Tick, AddressOf Timer_Tick
            timer.Interval = New TimeSpan(0, 0, 0, 0, 41)
            AllIndicators = New List(Of IndicatorItem)() From {IndicatorItem.Create(IndicatorType.BollingerBands, "Bollinger Bands", "BB (20, 2)")}
            Dim cciLevels As List(Of Double) = New List(Of Double)() From {-100, 100}
            Dim wrLevels As List(Of Double) = New List(Of Double)() From {-80, -20}
            SeparatePaneIndicators = New List(Of IndicatorItem)() From {IndicatorItem.Create(IndicatorType.CommodityChannelIndex, "Commodity Channel Index", "CCI (14)", cciLevels, -150, 150), IndicatorItem.Create(IndicatorType.WilliamsR, "Williams %R", "%R (14)", wrLevels, -100, 0)}
            AllIndicators.AddRange(SeparatePaneIndicators)
            SideMarginsValue = 2
            SetInitialState()
        End Sub

        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            UpdateDataSource()
        End Sub

        Private Sub UpdateAxisXVisibility()
            Dim lastVisibleIndicatorFound As Boolean = False
            For i As Integer = SeparatePaneIndicators.Count - 1 To -1 + 1 Step -1
                Dim indicatorItem As IndicatorItem = SeparatePaneIndicators(i)
                If Not lastVisibleIndicatorFound Then
                    indicatorItem.ShowXAxis = True
                    lastVisibleIndicatorFound = True
                Else
                    indicatorItem.ShowXAxis = False
                End If
            Next
        End Sub

        Private Sub UpdateVisualRangeAndSideMargins(ByVal unit As DateTimeMeasureUnit, ByVal multiplier As Integer)
            Dim priceAxisYSideMargin As Double = 0
            Dim volumeAxisYSideMargin As Double = 0
            Dim visualXRangeMax As Date = New DateTime()
            Dim visualXRangeMin As Date = New DateTime()
            Dim axisXLabelPattern As String = ""
            Select Case unit
                Case DateTimeMeasureUnit.Minute
                    priceAxisYSideMargin = 0.03 * multiplier
                    volumeAxisYSideMargin = 500 * multiplier
                    visualXRangeMax = Enumerable.Last(StockData).DateTimeStamp.AddMinutes(Math.Max(5, multiplier) * SideMarginsValue)
                    visualXRangeMin = visualXRangeMax.AddMinutes(-InitialPointsOnScreen * Math.Max(5, multiplier))
                    axisXLabelPattern = "{A:g}"
                Case DateTimeMeasureUnit.Hour
                    priceAxisYSideMargin = 1.8
                    volumeAxisYSideMargin = 30000
                    visualXRangeMax = Enumerable.Last(StockData).DateTimeStamp.AddHours(multiplier * SideMarginsValue)
                    visualXRangeMin = visualXRangeMax.AddHours(-InitialPointsOnScreen * multiplier)
                    axisXLabelPattern = "{A:d}"
            End Select

            Me.PriceAxisYSideMargin = priceAxisYSideMargin
            Me.VolumeAxisYSideMargin = volumeAxisYSideMargin
            Me.VisualXRangeMax = visualXRangeMax
            Me.VisualXRangeMin = visualXRangeMin
            Me.AxisXLabelPattern = axisXLabelPattern
        End Sub

        Public Sub SetInitialState()
            VisualXRangeMax = New DateTime()
            VisualXRangeMin = New DateTime()
            AxisXMeasureUnit = DateTimeMeasureUnit.Minute
            AxisXMeasureUnitMultiplier = 5
            generator.Start()
            UpdateAxisXVisibility()
        End Sub

        Public Sub CheckVisualRangeOnZoom(ByVal e As XYDiagram2DBeforeZoomEventArgs)
            If Not(TypeOf e.Axis Is AxisX2D) Then Return
            If e.NewRange.Max - e.NewRange.Min > MaxPointsOnScreen Then e.Cancel = True
        End Sub

        Public Sub PauseTimer() Implements IChartDemoViewModel.PauseTimer
            timer.Stop()
        End Sub

        Public Sub ResumeTimer() Implements IChartDemoViewModel.ResumeTimer
            timer.Start()
        End Sub

        Public Sub Unload() Implements IChartDemoViewModel.Unload
            timer.Stop()
            generator.Stop()
            RemoveHandler timer.Tick, AddressOf Timer_Tick
        End Sub

        Public Sub UpdateDataSource() Implements IChartDemoViewModel.UpdateDataSource
            If generator IsNot Nothing Then generator.UpdateDataSource()
            CurrentPrice = Enumerable.Last(generator.DataSource).Close
        End Sub

        Protected Sub OnAxisXMeasureUnitChanged()
            UpdateVisualRangeAndSideMargins(AxisXMeasureUnit, AxisXMeasureUnitMultiplier)
        End Sub

        Protected Sub OnAxisXMeasureUnitMultiplierChanged()
            UpdateVisualRangeAndSideMargins(AxisXMeasureUnit, AxisXMeasureUnitMultiplier)
        End Sub
    End Class

    Public Class IndicatorItem

        Private _Type As IndicatorType, _Name As String, _LegendText As String, _SignalLineValues As List(Of Double), _YRangeMin As Double?, _YRangeMax As Double?

        Public Property Type As IndicatorType
            Get
                Return _Type
            End Get

            Private Set(ByVal value As IndicatorType)
                _Type = value
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

        Public Property LegendText As String
            Get
                Return _LegendText
            End Get

            Private Set(ByVal value As String)
                _LegendText = value
            End Set
        End Property

        Public Overridable Property ShowXAxis As Boolean

        Public Property SignalLineValues As List(Of Double)
            Get
                Return _SignalLineValues
            End Get

            Private Set(ByVal value As List(Of Double))
                _SignalLineValues = value
            End Set
        End Property

        Public Property YRangeMin As Double?
            Get
                Return _YRangeMin
            End Get

            Private Set(ByVal value As Double?)
                _YRangeMin = value
            End Set
        End Property

        Public Property YRangeMax As Double?
            Get
                Return _YRangeMax
            End Get

            Private Set(ByVal value As Double?)
                _YRangeMax = value
            End Set
        End Property

        Protected Sub New(ByVal indicatorType As IndicatorType, ByVal indicatorName As String, ByVal indicatorLegendText As String, ByVal signalLines As List(Of Double), ByVal yAxisRangeMin As Double?, ByVal yAxisRangeMax As Double?)
            Type = indicatorType
            Name = indicatorName
            LegendText = indicatorLegendText
            If signalLines Is Nothing Then
                SignalLineValues = New List(Of Double)()
            Else
                SignalLineValues = signalLines
            End If

            YRangeMin = yAxisRangeMin
            YRangeMax = yAxisRangeMax
        End Sub

        Public Shared Function Create(ByVal indicatorType As IndicatorType, ByVal indicatorName As String, ByVal indicatorLegendText As String, ByVal Optional signalLines As List(Of Double) = Nothing, ByVal Optional yAxisRangeMin As Double? = Nothing, ByVal Optional yAxisRangeMax As Double? = Nothing) As IndicatorItem
            Return ViewModelSource.Create(Function() New IndicatorItem(indicatorType, indicatorName, indicatorLegendText, signalLines, yAxisRangeMin, yAxisRangeMax))
        End Function
    End Class

    Public Enum IndicatorType
        CommodityChannelIndex
        WilliamsR
        BollingerBands
    End Enum
End Namespace
