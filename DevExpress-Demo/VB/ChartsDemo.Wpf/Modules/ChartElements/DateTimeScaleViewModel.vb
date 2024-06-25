Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class DateTimeScaleViewModel

        Private _MayFirst1 As DateTime, _MayFirst2 As DateTime, _MayFirst3 As DateTime, _JanuaryFirst1 As DateTime, _JanuaryFirst2 As DateTime, _JanuaryFirst3 As DateTime, _GridAlignments As ObservableCollection(Of DevExpress.Xpf.Charts.DateTimeGridAlignment), _AggregateFunctions As ObservableCollection(Of DevExpress.Xpf.Charts.AggregateFunction), _DataSource As List(Of ChartsDemo.FinancialDataPoint), _Series As List(Of ChartsDemo.DateTimeScaleSeriesViewModel)

        Public Shared Function Create() As DateTimeScaleViewModel
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New ChartsDemo.DateTimeScaleViewModel())
        End Function

        Private ReadOnly Property PriceSeries As DateTimeScaleSeriesViewModel
            Get
                Return Me.Series(0)
            End Get
        End Property

        Private ReadOnly Property VolumeSeries As DateTimeScaleSeriesViewModel
            Get
                Return Me.Series(1)
            End Get
        End Property

        Public Overridable Property ScaleMode As ScaleMode

        Public Overridable Property MeasureUnit As ArgumentMeasureUnit

        Public Overridable Property SelectedSeries As DateTimeScaleSeriesViewModel

        Public Overridable Property MeasureUnitMultiplier As Integer

        Public Overridable Property SelectedAggregateFunction As AggregateFunction

        Public Overridable Property ScaleOptionsEnabled As Boolean

        Public Overridable Property AutoGrid As Boolean

        Public Overridable Property WorkTimeOnly As Boolean

        Public Overridable Property WorkTimeOptionsEnabled As Boolean

        Public Overridable Property WorkdaysOptionsEnabled As Boolean

        Public Overridable Property ExcludeWeekends As Boolean

        Public Overridable Property ExcludeHolidays As Boolean

        Public Overridable Property GridOptionsEnabled As Boolean

        Public Overridable Property GridAlignment As DateTimeGridAlignment

        Public Overridable Property GridSpacing As Integer

        Public Overridable Property GridOffset As Integer

        Public Overridable Property MinorTickmarksCount As Integer

        Public Overridable Property StartWorkHour As Integer

        Public Overridable Property EndWorkHour As Integer

        Public Overridable Property Title As String

        Public Overridable Property VisualRangeMin As System.DateTime?

        Public Overridable Property VisualRangeMax As System.DateTime?

        Public Property MayFirst1 As DateTime
            Get
                Return _MayFirst1
            End Get

            Private Set(ByVal value As DateTime)
                _MayFirst1 = value
            End Set
        End Property

        Public Property MayFirst2 As DateTime
            Get
                Return _MayFirst2
            End Get

            Private Set(ByVal value As DateTime)
                _MayFirst2 = value
            End Set
        End Property

        Public Property MayFirst3 As DateTime
            Get
                Return _MayFirst3
            End Get

            Private Set(ByVal value As DateTime)
                _MayFirst3 = value
            End Set
        End Property

        Public Property JanuaryFirst1 As DateTime
            Get
                Return _JanuaryFirst1
            End Get

            Private Set(ByVal value As DateTime)
                _JanuaryFirst1 = value
            End Set
        End Property

        Public Property JanuaryFirst2 As DateTime
            Get
                Return _JanuaryFirst2
            End Get

            Private Set(ByVal value As DateTime)
                _JanuaryFirst2 = value
            End Set
        End Property

        Public Property JanuaryFirst3 As DateTime
            Get
                Return _JanuaryFirst3
            End Get

            Private Set(ByVal value As DateTime)
                _JanuaryFirst3 = value
            End Set
        End Property

        Public Property GridAlignments As ObservableCollection(Of DevExpress.Xpf.Charts.DateTimeGridAlignment)
            Get
                Return _GridAlignments
            End Get

            Private Set(ByVal value As ObservableCollection(Of DevExpress.Xpf.Charts.DateTimeGridAlignment))
                _GridAlignments = value
            End Set
        End Property

        Public Property AggregateFunctions As ObservableCollection(Of DevExpress.Xpf.Charts.AggregateFunction)
            Get
                Return _AggregateFunctions
            End Get

            Private Set(ByVal value As ObservableCollection(Of DevExpress.Xpf.Charts.AggregateFunction))
                _AggregateFunctions = value
            End Set
        End Property

        Public Property DataSource As List(Of ChartsDemo.FinancialDataPoint)
            Get
                Return _DataSource
            End Get

            Private Set(ByVal value As List(Of ChartsDemo.FinancialDataPoint))
                _DataSource = value
            End Set
        End Property

        Public Property Series As List(Of ChartsDemo.DateTimeScaleSeriesViewModel)
            Get
                Return _Series
            End Get

            Private Set(ByVal value As List(Of ChartsDemo.DateTimeScaleSeriesViewModel))
                _Series = value
            End Set
        End Property

        Protected Sub New()
            Me.DataSource = New ChartsDemo.FinancialDataWithBreaksGenerator().Generate()
            Me.Series = New System.Collections.Generic.List(Of ChartsDemo.DateTimeScaleSeriesViewModel) From {ChartsDemo.DateTimeScaleSeriesViewModel.Create(ChartsDemo.SeriesView.Price), ChartsDemo.DateTimeScaleSeriesViewModel.Create(ChartsDemo.SeriesView.Volume)}
            Me.AggregateFunctions = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Charts.AggregateFunction) From {DevExpress.Xpf.Charts.AggregateFunction.Financial}
            Me.GridAlignments = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Charts.DateTimeGridAlignment)()
            Me.SelectedSeries = Me.PriceSeries
            Me.ScaleMode = DevExpress.Xpf.Charts.ScaleMode.Manual
            Me.MeasureUnit = ChartsDemo.ArgumentMeasureUnit.Month
            Me.ScaleOptionsEnabled = True
            Me.MeasureUnitMultiplier = 1
            Me.GridSpacing = 1
            Me.GridOffset = 0
            Me.MinorTickmarksCount = 3
            Dim firstYear As Integer = Me.DataSource(CInt((0))).DateTimeStamp.Year
            Me.JanuaryFirst1 = New System.DateTime(firstYear, 1, 1)
            Me.JanuaryFirst2 = New System.DateTime(firstYear + 1, 1, 1)
            Me.JanuaryFirst3 = New System.DateTime(firstYear + 2, 1, 1)
            Me.MayFirst1 = New System.DateTime(firstYear, 5, 1)
            Me.MayFirst2 = New System.DateTime(firstYear + 1, 5, 1)
            Me.MayFirst3 = New System.DateTime(firstYear + 2, 5, 1)
            Me.StartWorkHour = 8
            Me.EndWorkHour = 18
            Me.ExcludeHolidays = True
            Me.ExcludeWeekends = True
        End Sub

        Private Sub SetAggregateFunctions()
            Select Case Me.SelectedSeries.SeriesView
                Case ChartsDemo.SeriesView.Price
                    Me.PriceSeries.Visible = True
                    Me.VolumeSeries.Visible = False
                    Me.AggregateFunctions.Clear()
                    Me.AggregateFunctions.Add(DevExpress.Xpf.Charts.AggregateFunction.Financial)
                    Me.SelectedAggregateFunction = DevExpress.Xpf.Charts.AggregateFunction.Financial
                Case ChartsDemo.SeriesView.Volume
                    Me.PriceSeries.Visible = False
                    Me.VolumeSeries.Visible = True
                    Me.AggregateFunctions.Clear()
                    Me.AggregateFunctions.Add(DevExpress.Xpf.Charts.AggregateFunction.Sum)
                    Me.AggregateFunctions.Add(DevExpress.Xpf.Charts.AggregateFunction.Average)
                    Me.AggregateFunctions.Add(DevExpress.Xpf.Charts.AggregateFunction.Minimum)
                    Me.AggregateFunctions.Add(DevExpress.Xpf.Charts.AggregateFunction.Maximum)
                    Me.SelectedAggregateFunction = DevExpress.Xpf.Charts.AggregateFunction.Sum
                Case Else
                    Throw New System.ComponentModel.InvalidEnumArgumentException(String.Format("The {0} enum value is unknown", Me.SelectedSeries.SeriesView))
            End Select
        End Sub

        Private Sub ChangeTitle()
            If Me.SelectedSeries Is Me.PriceSeries Then
                Me.Title = "Price by " & Me.MeasureUnit
            Else
                Me.Title = "Sales Volume by " & Me.MeasureUnit
            End If
        End Sub

        Protected Sub OnSelectedSeriesChanged()
            Select Case Me.SelectedSeries.SeriesView
                Case ChartsDemo.SeriesView.Price
                    Me.PriceSeries.Visible = True
                    Me.VolumeSeries.Visible = False
                Case ChartsDemo.SeriesView.Volume
                    Me.PriceSeries.Visible = False
                    Me.VolumeSeries.Visible = True
                Case Else
                    Throw New System.ComponentModel.InvalidEnumArgumentException(String.Format("The {0} enum value is unknown", Me.SelectedSeries.SeriesView))
            End Select

            Me.SetAggregateFunctions()
            Me.ChangeTitle()
        End Sub

        Protected Sub OnScaleModeChanged()
            Select Case Me.ScaleMode
                Case DevExpress.Xpf.Charts.ScaleMode.Continuous
                    Me.AggregateFunctions.Clear()
                    Me.AggregateFunctions.Add(DevExpress.Xpf.Charts.AggregateFunction.None)
                    Me.SelectedAggregateFunction = DevExpress.Xpf.Charts.AggregateFunction.None
                    Me.ScaleOptionsEnabled = False
                    Me.GridOptionsEnabled = True
                Case DevExpress.Xpf.Charts.ScaleMode.Automatic
                    Me.ScaleOptionsEnabled = False
                    Me.SetAggregateFunctions()
                    Me.AutoGrid = True
                    Me.GridOptionsEnabled = False
                Case DevExpress.Xpf.Charts.ScaleMode.Manual
                    Me.ScaleOptionsEnabled = True
                    Me.SetAggregateFunctions()
                    Me.GridOptionsEnabled = True
                Case Else
                    Throw New System.ComponentModel.InvalidEnumArgumentException(String.Format("The {0} enum value is unknown", Me.ScaleMode))
            End Select
        End Sub

        Protected Sub OnMeasureUnitChanged()
            Me.GridAlignments.Clear()
            Dim measureUnitEnumValue As Integer = CInt(Me.MeasureUnit)
            For i As Integer = measureUnitEnumValue To 9 - 1
                Me.GridAlignments.Add(CType(i, DevExpress.Xpf.Charts.DateTimeGridAlignment))
            Next

            Dim gridAlignmentEnumValue As Integer = If(measureUnitEnumValue + 1 > 8, 8, measureUnitEnumValue + 1)
            Me.GridAlignment = CType(gridAlignmentEnumValue, DevExpress.Xpf.Charts.DateTimeGridAlignment)
            Me.VisualRangeMax = Nothing
            Me.VisualRangeMin = Nothing
            Me.VisualRangeMax = System.Linq.Enumerable.Last(Of ChartsDemo.FinancialDataPoint)(Me.DataSource).DateTimeStamp
            Select Case Me.MeasureUnit
                Case ChartsDemo.ArgumentMeasureUnit.Hour
                    Me.WorkdaysOptionsEnabled = True
                    Me.WorkTimeOptionsEnabled = True
                Case ChartsDemo.ArgumentMeasureUnit.Day
                    Me.WorkdaysOptionsEnabled = True
                    Me.WorkTimeOptionsEnabled = False
                Case ChartsDemo.ArgumentMeasureUnit.Week, ChartsDemo.ArgumentMeasureUnit.Month, ChartsDemo.ArgumentMeasureUnit.Quarter, ChartsDemo.ArgumentMeasureUnit.Year
                    Me.WorkdaysOptionsEnabled = False
                    Me.WorkTimeOptionsEnabled = False
            End Select

            If Not Me.ScaleMode.Equals(DevExpress.Xpf.Charts.ScaleMode.Automatic) Then
                Select Case Me.MeasureUnit
                    Case ChartsDemo.ArgumentMeasureUnit.Hour
                        Me.VisualRangeMin = Me.VisualRangeMax.Value.AddDays(-50)
                    Case ChartsDemo.ArgumentMeasureUnit.Day
                        Me.VisualRangeMin = Me.VisualRangeMax.Value.AddDays(-50)
                    Case ChartsDemo.ArgumentMeasureUnit.Week
                        Me.VisualRangeMin = Me.VisualRangeMax.Value.AddDays(-50 * 7)
                    Case ChartsDemo.ArgumentMeasureUnit.Month
                        Me.VisualRangeMin = Me.VisualRangeMax.Value.AddMonths(-50)
                    Case ChartsDemo.ArgumentMeasureUnit.Quarter, ChartsDemo.ArgumentMeasureUnit.Year
                        Me.VisualRangeMin = Me.VisualRangeMax.Value.AddYears(-3)
                End Select
            End If
        End Sub

        Public Sub OnScaleChanged(ByVal e As DevExpress.Xpf.Charts.AxisScaleChangedEventArgs)
            If Not(TypeOf e.Axis Is DevExpress.Xpf.Charts.AxisX2D) Then Return
            Dim args As DevExpress.Xpf.Charts.DateTimeScaleChangedEventArgs = TryCast(e, DevExpress.Xpf.Charts.DateTimeScaleChangedEventArgs)
            If args Is Nothing Then Return
            If args.MeasureUnitChange.IsChanged Then Me.MeasureUnit = CType(args.MeasureUnitChange.NewValue, ChartsDemo.ArgumentMeasureUnit)
            If args.GridAlignmentChange.IsChanged Then Me.GridAlignment = args.GridAlignmentChange.NewValue
            If args.GridOffsetChange.IsChanged Then Me.GridOffset = CInt(args.GridOffsetChange.NewValue)
            If args.GridSpacingChange.IsChanged Then Me.GridSpacing = CInt(args.GridSpacingChange.NewValue)
            Me.ChangeTitle()
        End Sub
    End Class

    Public Class DateTimeScaleSeriesViewModel

        Public Shared Function Create(ByVal view As ChartsDemo.SeriesView) As DateTimeScaleSeriesViewModel
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New ChartsDemo.DateTimeScaleSeriesViewModel(view))
        End Function

        Private ReadOnly view As ChartsDemo.SeriesView

        Public ReadOnly Property SeriesView As SeriesView
            Get
                Return Me.view
            End Get
        End Property

        Public Overridable Property Visible As Boolean

        Protected Sub New(ByVal view As ChartsDemo.SeriesView)
            Me.view = view
        End Sub

        Public Overrides Function ToString() As String
            Return Me.view.ToString()
        End Function
    End Class

    Public Enum SeriesView
        Price
        Volume
    End Enum

    Public Enum ArgumentMeasureUnit
        Millisecond = 0
        Hour = 3
        Day = 4
        Week = 5
        Month = 6
        Quarter = 7
        Year = 8
    End Enum
End Namespace
