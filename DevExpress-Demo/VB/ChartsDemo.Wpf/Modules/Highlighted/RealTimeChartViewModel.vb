Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    <POCOViewModel>
    Public Class RealTimeChartViewModel
        Implements IChartDemoViewModel

        Private _Panes As List(Of ChartsDemo.MeasurableQuantityPaneViewModel), _SensorDataSeries As List(Of ChartsDemo.SensorDataSeriesViewModel)

        Public Shared Function Create() As RealTimeChartViewModel
            Return ViewModelSource.Create(Function() New RealTimeChartViewModel())
        End Function

        Const MaxVisualRangeLengthMilliseconds As Double = 50000R

        Private ReadOnly timer As DispatcherTimer = ChartsDemoModule.CreateTimer()

        Private ReadOnly generator As SensorDataGenerator

        Public ReadOnly Property DataSource As ObservableCollection(Of SensorIndicationItem)
            Get
                Return generator.DataSource
            End Get
        End Property

        Public Overridable Property FPS As String

        Public Overridable Property Palette As Palette

        Public Overridable Property SensorSet1Brush As SolidColorBrush

        Public Overridable Property SensorSet2Brush As SolidColorBrush

        Public Property Panes As List(Of MeasurableQuantityPaneViewModel)
            Get
                Return _Panes
            End Get

            Private Set(ByVal value As List(Of MeasurableQuantityPaneViewModel))
                _Panes = value
            End Set
        End Property

        Public Property SensorDataSeries As List(Of SensorDataSeriesViewModel)
            Get
                Return _SensorDataSeries
            End Get

            Private Set(ByVal value As List(Of SensorDataSeriesViewModel))
                _SensorDataSeries = value
            End Set
        End Property

        Private framesCount As Integer = 0

        Private lastFpsUpdate As Date = Date.Now

        Protected Sub New()
            generator = New SensorDataGenerator()
            generator.GenerateInitialData()
            generator.Start()
            AddHandler timer.Tick, AddressOf Timer_Tick
            timer.Interval = New TimeSpan(0, 0, 0, 0, 15)
            AddHandler CompositionTarget.Rendering, AddressOf CompositionTarget_Rendering
            Panes = New List(Of MeasurableQuantityPaneViewModel) From {New MeasurableQuantityPaneViewModel("Temperature", False), New MeasurableQuantityPaneViewModel("Pressure", False), New MeasurableQuantityPaneViewModel("Power", False), New MeasurableQuantityPaneViewModel("Intensity", True)}
            SensorDataSeries = New List(Of SensorDataSeriesViewModel) From {SensorDataSeriesViewModel.Create("SensorIndication1", Panes(0)), SensorDataSeriesViewModel.Create("SensorIndication2", Panes(0)), SensorDataSeriesViewModel.Create("SensorIndication3", Panes(1)), SensorDataSeriesViewModel.Create("SensorIndication4", Panes(1)), SensorDataSeriesViewModel.Create("SensorIndication5", Panes(2)), SensorDataSeriesViewModel.Create("SensorIndication6", Panes(2)), SensorDataSeriesViewModel.Create("SensorIndication7", Panes(3)), SensorDataSeriesViewModel.Create("SensorIndication8", Panes(3))}
            Palette = New OfficePalette()
        End Sub

        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            UpdateDataSource()
            If(Date.Now - lastFpsUpdate).TotalSeconds >= 1 Then
                FPS = "FPS: " & framesCount.ToString()
                lastFpsUpdate = Date.Now
                framesCount = 0
            End If
        End Sub

        Private Sub CompositionTarget_Rendering(ByVal sender As Object, ByVal e As EventArgs)
            framesCount += 1
        End Sub

        Protected Sub OnPaletteChanged()
            If Palette Is Nothing Then Return
            SensorSet1Brush = New SolidColorBrush(Palette(0))
            SensorSet2Brush = New SolidColorBrush(Palette(1))
            SensorSet1Brush.Freeze()
            SensorSet2Brush.Freeze()
            For i As Integer = 0 To SensorDataSeries.Count - 1
                If i Mod 2 = 0 Then
                    SensorDataSeries(i).Brush = SensorSet1Brush
                Else
                    SensorDataSeries(i).Brush = SensorSet2Brush
                End If
            Next
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
            RemoveHandler CompositionTarget.Rendering, AddressOf CompositionTarget_Rendering
        End Sub

        Public Sub LimitZoom(ByVal e As XYDiagram2DBeforeZoomEventArgs)
            If Not(TypeOf e.Axis Is AxisX2D) Then Return
            If e.NewRange.Max - e.NewRange.Min > MaxVisualRangeLengthMilliseconds Then e.Cancel = True
        End Sub

        Public Sub UpdateDataSource() Implements IChartDemoViewModel.UpdateDataSource
            If generator IsNot Nothing Then generator.UpdateDataSource()
        End Sub
    End Class

    Public Class MeasurableQuantityPaneViewModel

        Private _Name As String, _AxisXVisible As Boolean

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property AxisXVisible As Boolean
            Get
                Return _AxisXVisible
            End Get

            Private Set(ByVal value As Boolean)
                _AxisXVisible = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal scrollBarVisible As Boolean)
            Me.Name = name
            AxisXVisible = scrollBarVisible
        End Sub
    End Class

    Public Class SensorDataSeriesViewModel

        Private _ValueDataMember As String, _Pane As MeasurableQuantityPaneViewModel

        Public Shared Function Create(ByVal valueDataMember As String, ByVal pane As MeasurableQuantityPaneViewModel) As SensorDataSeriesViewModel
            Return ViewModelSource.Create(Function() New SensorDataSeriesViewModel(valueDataMember, pane))
        End Function

        Public Overridable Property Brush As SolidColorBrush

        Public Property ValueDataMember As String
            Get
                Return _ValueDataMember
            End Get

            Private Set(ByVal value As String)
                _ValueDataMember = value
            End Set
        End Property

        Public Property Pane As MeasurableQuantityPaneViewModel
            Get
                Return _Pane
            End Get

            Private Set(ByVal value As MeasurableQuantityPaneViewModel)
                _Pane = value
            End Set
        End Property

        Protected Sub New(ByVal valueDataMember As String, ByVal pane As MeasurableQuantityPaneViewModel)
            Me.ValueDataMember = valueDataMember
            Me.Pane = pane
        End Sub
    End Class
End Namespace
