Imports System.Windows
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Partial Class NumericDataAggregationTab
        Inherits TabItemModule

        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(NumericDataAggregationTab), Nothing)

        Public Property Title As String
            Get
                Return CStr(GetValue(TitleProperty))
            End Get

            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            lbeAggregationFunctions.FilterCriteria = CriteriaOperator.Parse("Not ([Id] in ('None', 'Financial', 'Histogram'))")
        End Sub

        Private Sub chart_AxisScaleChanged(ByVal sender As Object, ByVal e As AxisScaleChangedEventArgs)
            Dim axisX As AxisBase = CType(chart.Diagram, XYDiagram2D).AxisX
            If e.Axis IsNot axisX Then Return
            Dim numericArgs As NumericScaleChangedEventArgs = TryCast(e, NumericScaleChangedEventArgs)
            If numericArgs Is Nothing Then Return
            If numericArgs.MeasureUnitChange.IsChanged Then
                Title = "Measure Unit: " & numericArgs.MeasureUnitChange.NewValue
            End If
        End Sub
    End Class
End Namespace
