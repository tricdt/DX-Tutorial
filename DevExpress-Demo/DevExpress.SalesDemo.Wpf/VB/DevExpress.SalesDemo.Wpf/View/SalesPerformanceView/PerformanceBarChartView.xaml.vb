Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace DevExpress.SalesDemo.Wpf.View

    Public Partial Class PerformanceBarChartView
        Inherits UserControl

        Public Shared ReadOnly DateBorderMarginProperty As DependencyProperty = DependencyProperty.Register("DateBorderMargin", GetType(Thickness), GetType(PerformanceBarChartView), New PropertyMetadata(New Thickness()))

        Public Shared ReadOnly SalesVolumesMarginProperty As DependencyProperty = DependencyProperty.Register("SalesVolumesMargin", GetType(Thickness), GetType(PerformanceBarChartView), New PropertyMetadata(New Thickness()))

        Public Shared ReadOnly AreaAndSalesVolumesBrushProperty As DependencyProperty = DependencyProperty.Register("AreaAndSalesVolumesBrush", GetType(SolidColorBrush), GetType(PerformanceBarChartView), New PropertyMetadata(Brushes.Red))

        Public Shared ReadOnly ButtonsGridMarginProperty As DependencyProperty = DependencyProperty.Register("ButtonsGridMargin", GetType(Thickness), GetType(PerformanceBarChartView), New PropertyMetadata(New Thickness(0)))

        Public Shared ReadOnly AxisXMinorCountProperty As DependencyProperty = DependencyProperty.Register("AxisXMinorCount", GetType(Integer), GetType(PerformanceBarChartView), New PropertyMetadata(1))

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

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
