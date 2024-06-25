Imports System.Windows
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Partial Class TimeSpanDataAggregationTab
        Inherits TabItemModule

        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(TimeSpanDataAggregationTab), Nothing)

        Private ReadOnly Property Diagram As XYDiagram2D
            Get
                Return TryCast(chart.Diagram, XYDiagram2D)
            End Get
        End Property

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
            lbeAggregationFunctions.FilterCriteria = CriteriaOperator.Parse("Not ([Id] in ('None', 'Financial', 'Histogram', 'Sum', 'Count', 'Custom'))")
        End Sub

        Friend Shared Function ConvertCelsiusToFahrenheit(ByVal value As Double) As Double
            Return value * 1.8 + 32
        End Function

        Private Sub chart_AxisScaleChanged(ByVal sender As Object, ByVal e As AxisScaleChangedEventArgs)
            Dim axisX As Axis2D = Diagram.AxisX
            Dim axisY As Axis2D = Diagram.AxisY
            If e.Axis Is axisX Then
                Dim args As TimeSpanScaleChangedEventArgs = TryCast(e, TimeSpanScaleChangedEventArgs)
                If args IsNot Nothing AndAlso args.MeasureUnitChange.IsChanged Then Title = "Measure Unit: " & args.MeasureUnitChange.NewValue
            ElseIf e.Axis Is axisY Then
                Diagram.SecondaryAxesY(0).WholeRange.SetMinMaxValues(ConvertCelsiusToFahrenheit(axisY.ActualVisualRange.ActualMinValueInternal), ConvertCelsiusToFahrenheit(axisY.ActualVisualRange.ActualMaxValueInternal))
            End If
        End Sub

        Private Sub Chart_CustomDrawCrosshair(ByVal sender As Object, ByVal e As CustomDrawCrosshairEventArgs)
            If e.CrosshairElementGroups.Count > 0 AndAlso e.CrosshairElementGroups(0).CrosshairElements.Count > 0 Then
                For Each element As CrosshairElement In e.CrosshairElementGroups(0).CrosshairElements
                    If element.LabelElement IsNot Nothing AndAlso element.SeriesPoint IsNot Nothing Then element.LabelElement.Text += String.Format(", {0:0}°F", ConvertCelsiusToFahrenheit(element.SeriesPoint.Value))
                Next
            End If
        End Sub
    End Class
End Namespace
