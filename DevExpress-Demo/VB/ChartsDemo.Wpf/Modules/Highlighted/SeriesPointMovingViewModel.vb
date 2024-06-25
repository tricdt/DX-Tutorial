Imports System.Collections.Generic
Imports System.Windows.Input
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class SeriesPointMovingViewModel

        Public Shared Function Create() As SeriesPointMovingViewModel
            Return ViewModelSource.Create(Function() New SeriesPointMovingViewModel())
        End Function

        Const MinValue As Double = 0

        Const MaxRetailPriceValue As Double = 500

        Const MaxValue As Double = 1000 * MaxRetailPriceValue

        Private legendsField As SeriePointMovingLegendViewModel()

        Private panesField As SeriePointMovingPaneViewModel() = New SeriePointMovingPaneViewModel() {SeriePointMovingPaneViewModel.Create(), SeriePointMovingPaneViewModel.Create()}

        Private axesYField As SeriePointMovingAxisYViewModel() = New SeriePointMovingAxisYViewModel() {SeriePointMovingAxisYViewModel.Create("Retail Price (USD)", "{V}", False, True, AxisAlignment.Near), SeriePointMovingAxisYViewModel.Create("Income (USD)", "{V:0,}M", True, False, AxisAlignment.Far), SeriePointMovingAxisYViewModel.Create("Units", "{V:0,}K", True, True, AxisAlignment.Near)}

        Private currentSeriesItem As SeriesPointMovingSeriesViewModel = Nothing

        Private currentSeriesPointItem As DraggableItem = Nothing

        Private currentChartControl As ChartControl = Nothing

        Private ReadOnly Property SetCursorService As ISetCursorService
            Get
                Return GetService(Of ISetCursorService)()
            End Get
        End Property

        Public Overridable ReadOnly Property Legends As SeriePointMovingLegendViewModel()
            Get
                Return legendsField
            End Get
        End Property

        Public Overridable ReadOnly Property Panes As SeriePointMovingPaneViewModel()
            Get
                Return panesField
            End Get
        End Property

        Public Overridable ReadOnly Property AxesY As SeriePointMovingAxisYViewModel()
            Get
                Return axesYField
            End Get
        End Property

        Public Overridable Property SeriesItems As List(Of SeriesPointMovingSeriesViewModel)

        Protected Sub New()
            CreateLegends()
            CreateSeriesItems()
            UpdateTotalIncome()
        End Sub

        Private Sub CreateLegends()
            legendsField = New SeriePointMovingLegendViewModel() {SeriePointMovingLegendViewModel.Create(panesField(0), HorizontalPosition.RightOutside, VerticalPosition.Top, Nothing), SeriePointMovingLegendViewModel.Create(panesField(1), HorizontalPosition.RightOutside, VerticalPosition.Top, Nothing), SeriePointMovingLegendViewModel.Create(panesField(1), HorizontalPosition.Center, VerticalPosition.BottomOutside, New List(Of SeriesPointMovingCustomLegendItemViewModel)() From {SeriesPointMovingCustomLegendItemViewModel.Create("Total income: ")})}
        End Sub

        Private Sub CreateSeriesItems()
            Dim dataModel As DraggableDataModel = DraggableDataModel.CreateModel()
            SeriesItems = New List(Of SeriesPointMovingSeriesViewModel)(5)
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Income", dataModel, legendsField(0), panesField(0), axesYField(1)))
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Retail Price", dataModel, legendsField(0), panesField(0), axesYField(0)))
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Stock", dataModel, legendsField(1), panesField(1), axesYField(2)))
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Demand", dataModel, legendsField(1), panesField(1), axesYField(2)))
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Production", dataModel, legendsField(1), panesField(1), axesYField(2)))
        End Sub

        Private Sub SetNewPointValue(ByVal newValue As Double)
            If newValue < MinValue OrElse newValue > MaxRetailPriceValue AndAlso Equals(currentSeriesItem.Name, "Retail Price") OrElse newValue > MaxValue Then Return
            If newValue = 0 Then Return
            Select Case currentSeriesItem.Name
                Case "Production"
                    SeriesItems(0).DraggableItems.UpdateProduction(currentSeriesPointItem, newValue)
                Case "Demand"
                    SeriesItems(0).DraggableItems.UpdateDemand(currentSeriesPointItem, newValue)
                Case "Retail Price"
                    SeriesItems(0).DraggableItems.UpdateCost(currentSeriesPointItem, newValue)
            End Select

            UpdateTotalIncome()
        End Sub

        Private Sub SetHighlight(ByVal highlighted As Boolean)
            currentSeriesPointItem.IsHighlighted = highlighted
            currentSeriesItem.IsHighlighted = highlighted
        End Sub

        Private Sub UpdateTotalIncome()
            legendsField(2).CustomItems(0).Text = String.Format("Total income: ${0}K", SeriesItems(0).DraggableItems.TotalIncome.ToString("N0"))
        End Sub

        Public Sub OnMouseDown(ByVal mouseDownData As MouseDownData)
            If mouseDownData IsNot Nothing AndAlso Not Equals(mouseDownData.Series.Name, "Income") Then
                currentSeriesItem = mouseDownData.Series
                currentSeriesPointItem = mouseDownData.CurrentItem
                currentChartControl = mouseDownData.Chart
                SetHighlight(True)
                currentChartControl.CaptureMouse()
            End If
        End Sub

        Public Sub OnMouseMove(ByVal moveData As MouseMoveData)
            If moveData Is Nothing Then Return
            If moveData.IsOnDraggablePoint Then
                SetCursorService.SetCursor(Cursors.SizeNS)
            ElseIf currentSeriesPointItem Is Nothing Then
                SetCursorService.SetCursor(Cursors.Arrow)
            End If

            If currentSeriesPointItem IsNot Nothing AndAlso currentSeriesItem.Pane Is moveData.Pane Then SetNewPointValue(moveData.DiagramNumericalValue)
        End Sub

        Public Sub OnMouseUp()
            If currentSeriesItem IsNot Nothing Then
                SetHighlight(False)
                currentSeriesItem = Nothing
                currentSeriesPointItem = Nothing
                currentChartControl.ReleaseMouseCapture()
                currentChartControl = Nothing
            End If
        End Sub
    End Class

    Public Class SeriesPointMovingSeriesViewModel

        Public Shared Function Create(ByVal name As String, ByVal draggableItems As DraggableDataModel, ByVal legend As SeriePointMovingLegendViewModel, ByVal pane As SeriePointMovingPaneViewModel, ByVal axisY As SeriePointMovingAxisYViewModel) As SeriesPointMovingSeriesViewModel
            Return ViewModelSource.Create(Function() New SeriesPointMovingSeriesViewModel(name, draggableItems, legend, pane, axisY))
        End Function

        Public Overridable Property Name As String

        Public Overridable Property Legend As SeriePointMovingLegendViewModel

        Public Overridable Property Pane As SeriePointMovingPaneViewModel

        Public Overridable Property AxisY As SeriePointMovingAxisYViewModel

        Public Overridable Property DraggableItems As DraggableDataModel

        Public Overridable Property IsHighlighted As Boolean

        Public Sub New(ByVal name As String, ByVal draggableItems As DraggableDataModel, ByVal legend As SeriePointMovingLegendViewModel, ByVal pane As SeriePointMovingPaneViewModel, ByVal axisY As SeriePointMovingAxisYViewModel)
            Me.Name = name
            Me.Legend = legend
            Me.Pane = pane
            Me.AxisY = axisY
            Me.DraggableItems = draggableItems
            IsHighlighted = False
        End Sub
    End Class

    Public Class SeriePointMovingLegendViewModel

        Public Shared Function Create(ByVal dockTarget As SeriePointMovingPaneViewModel, ByVal horizontalAlignment As HorizontalPosition, ByVal verticalAlignment As VerticalPosition, ByVal customItems As List(Of SeriesPointMovingCustomLegendItemViewModel)) As SeriePointMovingLegendViewModel
            Return ViewModelSource.Create(Function() New SeriePointMovingLegendViewModel(dockTarget, horizontalAlignment, verticalAlignment, customItems))
        End Function

        Public Overridable Property DockTarget As SeriePointMovingPaneViewModel

        Public Overridable Property HorizontalPosition As HorizontalPosition

        Public Overridable Property VerticalPosition As VerticalPosition

        Public Overridable Property CustomItems As List(Of SeriesPointMovingCustomLegendItemViewModel)

        Protected Sub New(ByVal dockTarget As SeriePointMovingPaneViewModel, ByVal horizontalAlignment As HorizontalPosition, ByVal verticalAlignment As VerticalPosition, ByVal customItems As List(Of SeriesPointMovingCustomLegendItemViewModel))
            Me.DockTarget = dockTarget
            HorizontalPosition = horizontalAlignment
            VerticalPosition = verticalAlignment
            Me.CustomItems = customItems
        End Sub
    End Class

    Public Class SeriesPointMovingCustomLegendItemViewModel

        Public Shared Function Create(ByVal text As String) As SeriesPointMovingCustomLegendItemViewModel
            Return ViewModelSource.Create(Function() New SeriesPointMovingCustomLegendItemViewModel(text))
        End Function

        Public Overridable Property Text As String

        Protected Sub New(ByVal text As String)
            Me.Text = text
        End Sub
    End Class

    Public Class SeriePointMovingPaneViewModel

        Public Shared Function Create() As SeriePointMovingPaneViewModel
            Return ViewModelSource.Create(Function() New SeriePointMovingPaneViewModel())
        End Function
    End Class

    Public Class SeriePointMovingAxisYViewModel

        Public Shared Function Create(ByVal title As String, ByVal textPattern As String, ByVal alwaysShowZeroLevel As Boolean, ByVal gridLinesVisible As Boolean, ByVal alignment As AxisAlignment) As SeriePointMovingAxisYViewModel
            Return ViewModelSource.Create(Function() New SeriePointMovingAxisYViewModel(title, textPattern, alwaysShowZeroLevel, gridLinesVisible, alignment))
        End Function

        Public Overridable Property Title As String

        Public Overridable Property TextPattern As String

        Public Overridable Property AlwaysShowZeroLevel As Boolean

        Public Overridable Property GridLinesVisible As Boolean

        Public Overridable Property Alignment As AxisAlignment

        Protected Sub New(ByVal title As String, ByVal textPattern As String, ByVal alwaysShowZeroLevel As Boolean, ByVal gridLinesVisible As Boolean, ByVal alignment As AxisAlignment)
            Me.Title = title
            Me.TextPattern = textPattern
            Me.AlwaysShowZeroLevel = alwaysShowZeroLevel
            Me.GridLinesVisible = gridLinesVisible
            Me.Alignment = alignment
        End Sub
    End Class

    Public Class SeriesPointLabelViewModel
    End Class

    Public Interface ISetCursorService

        Sub SetCursor(ByVal cursor As Cursor)

    End Interface

    Public Class SetCursorService
        Inherits ServiceBase
        Implements ISetCursorService

        Public Sub SetCursor(ByVal cursor As Cursor) Implements ISetCursorService.SetCursor
            CType(AssociatedObject, SeriesPointMovingDemo).Cursor = cursor
        End Sub
    End Class
End Namespace
