Imports DevExpress.Xpf.Charts
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace DevExpress.SalesDemo.Wpf.View

    Public Partial Class PerformanceAreaChartView
        Inherits UserControl

        Public Shared ReadOnly DateBorderMarginProperty As DependencyProperty = DependencyProperty.Register("DateBorderMargin", GetType(Thickness), GetType(PerformanceAreaChartView), New PropertyMetadata(New Thickness()))

        Public Shared ReadOnly SalesVolumesMarginProperty As DependencyProperty = DependencyProperty.Register("SalesVolumesMargin", GetType(Thickness), GetType(PerformanceAreaChartView), New PropertyMetadata(New Thickness()))

        Public Shared ReadOnly AreaAndSalesVolumesBrushProperty As DependencyProperty = DependencyProperty.Register("AreaAndSalesVolumesBrush", GetType(SolidColorBrush), GetType(PerformanceAreaChartView), New PropertyMetadata(Brushes.Red))

        Public Shared ReadOnly ButtonsGridMarginProperty As DependencyProperty = DependencyProperty.Register("ButtonsGridMargin", GetType(Thickness), GetType(PerformanceAreaChartView), New PropertyMetadata(New Thickness(0)))

        Public Shared ReadOnly AxisXMinorCountProperty As DependencyProperty = DependencyProperty.Register("AxisXMinorCount", GetType(Integer), GetType(PerformanceAreaChartView), New PropertyMetadata(1))

        Public Shared ReadOnly AxisXGridSpacingProperty As DependencyProperty = DependencyProperty.Register("AxisXGridSpacing", GetType(Double), GetType(PerformanceAreaChartView), New PropertyMetadata(1R))

        Public Shared ReadOnly AreaSeriesCrosshairLabelPatternProperty As DependencyProperty = DependencyProperty.Register("AreaSeriesCrosshairLabelPattern", GetType(String), GetType(PerformanceAreaChartView), New PropertyMetadata(""))

        Public Shared ReadOnly DateTimeGridAlignmentProperty As DependencyProperty = DependencyProperty.Register("DateTimeGridAlignment", GetType(DateTimeGridAlignment), GetType(PerformanceAreaChartView), New PropertyMetadata(DateTimeGridAlignment.Day))

        Public Shared ReadOnly DateTimeMeasureUnitProperty As DependencyProperty = DependencyProperty.Register("DateTimeMeasureUnit", GetType(DateTimeMeasureUnit), GetType(PerformanceAreaChartView), New PropertyMetadata(DateTimeMeasureUnit.Hour))

        Public Property DateBorderMargin As Thickness
            Get
                Return CType(GetValue(DateBorderMarginProperty), Thickness)
            End Get

            Set(ByVal value As Thickness)
                SetValue(DateBorderMarginProperty, value)
            End Set
        End Property

        Public Property SalesVolumesMargin As Thickness
            Get
                Return CType(GetValue(SalesVolumesMarginProperty), Thickness)
            End Get

            Set(ByVal value As Thickness)
                SetValue(DateBorderMarginProperty, value)
            End Set
        End Property

        Public Property AreaAndSalesVolumesBrush As SolidColorBrush
            Get
                Return CType(GetValue(AreaAndSalesVolumesBrushProperty), SolidColorBrush)
            End Get

            Set(ByVal value As SolidColorBrush)
                SetValue(AreaAndSalesVolumesBrushProperty, value)
            End Set
        End Property

        Public Property ButtonsGridMargin As Thickness
            Get
                Return CType(GetValue(ButtonsGridMarginProperty), Thickness)
            End Get

            Set(ByVal value As Thickness)
                SetValue(ButtonsGridMarginProperty, value)
            End Set
        End Property

        Public Property AxisXMinorCount As Integer
            Get
                Return CInt(GetValue(AxisXMinorCountProperty))
            End Get

            Set(ByVal value As Integer)
                SetValue(AxisXMinorCountProperty, value)
            End Set
        End Property

        Public Property AxisXGridSpacing As Double
            Get
                Return CDbl(GetValue(AxisXGridSpacingProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(AxisXGridSpacingProperty, value)
            End Set
        End Property

        Public Property AreaSeriesCrosshairLabelPattern As String
            Get
                Return CStr(GetValue(AreaSeriesCrosshairLabelPatternProperty))
            End Get

            Set(ByVal value As String)
                SetValue(AreaSeriesCrosshairLabelPatternProperty, value)
            End Set
        End Property

        Public Property DateTimeGridAlignment As DateTimeGridAlignment
            Get
                Return CType(GetValue(DateTimeGridAlignmentProperty), DateTimeGridAlignment)
            End Get

            Set(ByVal value As DateTimeGridAlignment)
                SetValue(DateTimeGridAlignmentProperty, value)
            End Set
        End Property

        Public Property DateTimeMeasureUnit As DateTimeMeasureUnit
            Get
                Return CType(GetValue(DateTimeMeasureUnitProperty), DateTimeMeasureUnit)
            End Get

            Set(ByVal value As DateTimeMeasureUnit)
                SetValue(DateTimeMeasureUnitProperty, value)
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
