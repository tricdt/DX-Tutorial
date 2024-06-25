Imports System
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo

    <CodeFile("Modules/AppearanceCustomization/AdvancedCustomization.xaml"), CodeFile("Modules/AppearanceCustomization/AdvancedCustomization.xaml.(cs)"), CodeFile("DataModels/RealEstateData.(cs)"), NoAutogeneratedCodeFiles>
    Public Partial Class AdvancedCustomizationDemo
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub chart_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler CType(sender, ChartControl).BoundDataChanged, AddressOf chart_BoundDataChanged
            Dim firstYear As Integer = Integer.Parse(chart.Diagram.Series(0).Points(0).Argument)
            For i As Integer = firstYear To firstYear + 3 - 1
                Dim found As SeriesPoint = Nothing
                Dim maxIncome As Double = 0.0
                For Each series As Series In chart.Diagram.Series
                    Dim arg As String = i.ToString()
                    Dim point As SeriesPoint = series.Points.Where(Function(sp) Equals(sp.Argument, arg)).First()
                    If point.Value > maxIncome Then
                        found = point
                        maxIncome = point.Value
                    End If
                Next

                chart.Annotations.Add(New Annotation() With {.LabelMode = True, .Content = found.Tag, .AnchorPoint = New SeriesPointAnchorPoint() With {.SeriesPoint = found}})
            Next
        End Sub
    End Class

    Friend Enum Months
        Jan
        Feb
        Mar
        Apr
        May
        Jun
        Jul
        Aug
        Sep
        Oct
        Nov
        Dec
    End Enum

    Friend Class EmployeeNameToPhotoConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Dim name As String = TryCast(value, String)
            If Equals(name, Nothing) Then Return value
            Dim image As BitmapImage = New BitmapImage(New Uri("../../Images/Employees/" & name & ".png", UriKind.Relative))
            Return image
        End Function
    End Class

    Friend Class AxisValueToLabelTextConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value.GetType() IsNot GetType(Double) Then Return value
            Dim doubleValue As Double = CDbl(value)
            If doubleValue < 1000 Then
                Return String.Format("${0:0.}", value)
            ElseIf doubleValue < 1000000 Then
                Return String.Format("${0:0,.}K", value)
            ElseIf doubleValue < 1000000000 Then
                Return String.Format("${0:0,,.#}M", value)
            End If

            Return value.ToString()
        End Function
    End Class

    Friend Class AxisValueToLabelFontSizeConverter
        Inherits ForwardOnlyValueConverter

        Private Function CalculateFontSize(ByVal ratio As Double, ByVal minFontSize As Integer, ByVal maxFontSize As Integer) As Single
            Return CSng(minFontSize + (maxFontSize - minFontSize) * ratio)
        End Function

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Dim item As AxisLabelItem = TryCast(value, AxisLabelItem)
            If item Is Nothing OrElse item.Value.GetType() IsNot GetType(Double) Then Return value
            Dim doubleValue As Double = CDbl(item.Value)
            Dim axisY As AxisY2D = CType(item.Label.Parent, AxisY2D)
            Dim ratio As Double = doubleValue / (CDbl(axisY.ActualWholeRange.ActualMaxValue) + axisY.ActualWholeRange.ActualSideMarginsValue)
            Return CalculateFontSize(ratio, 12, 22)
        End Function
    End Class

    Friend Class AxisValueToLabelForegroundConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Dim item As AxisLabelItem = TryCast(value, AxisLabelItem)
            If item Is Nothing OrElse item.Value.GetType() IsNot GetType(Double) Then Return value
            Dim doubleValue As Double = CDbl(item.Value)
            Dim axisY As AxisY2D = CType(item.Label.Parent, AxisY2D)
            Dim ratio As Double = doubleValue / (CDbl(axisY.ActualWholeRange.ActualMaxValue) + axisY.ActualWholeRange.ActualSideMarginsValue)
            Dim color As Color = InterpolateColors(Color.FromArgb(255, 170, 42, 0), Colors.Green, ratio)
            Dim brush As SolidColorBrush = New SolidColorBrush(color)
            brush.Freeze()
            Return brush
        End Function
    End Class

    Friend Class BrushToSolidColorBrushConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Return value
        End Function
    End Class

    Friend Class CustomBarModelPanel
        Inherits Panel

        Public Shared ReadOnly SidesProperty As DependencyProperty = DependencyProperty.RegisterAttached("Sides", GetType(SolidSidesExtended), GetType(CustomBarModelPanel), New PropertyMetadata(SolidSidesExtended.Front))

        Public Shared Sub SetSides(ByVal element As UIElement, ByVal value As SolidSides)
            element.SetValue(SidesProperty, value)
        End Sub

        Public Shared Function GetSides(ByVal element As UIElement) As SolidSidesExtended
            Return CType(element.GetValue(SidesProperty), SolidSidesExtended)
        End Function

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            For Each child As UIElement In Children
                Select Case GetSides(child)
                    Case SolidSidesExtended.Top, SolidSidesExtended.Bottom
                        child.Measure(New Size(availableSize.Width, Double.PositiveInfinity))
                    Case SolidSidesExtended.Right, SolidSidesExtended.Left
                        child.Measure(New Size(Double.PositiveInfinity, availableSize.Height))
                    Case SolidSidesExtended.FrontWindow
                        Dim width As Double = Math.Floor(availableSize.Width / 5R) * 5R
                        Dim height As Double = Math.Floor(availableSize.Height / 9R) * 9R
                        height = If(height < 0, 0, height)
                        child.Measure(New Size(width, height))
                    Case Else
                        child.Measure(availableSize)
                End Select
            Next

            Dim constraint As Size = New Size()
            constraint.Width = If(Double.IsInfinity(availableSize.Width), 0, availableSize.Width)
            constraint.Height = If(Double.IsInfinity(availableSize.Height), 0, availableSize.Height)
            Return constraint
        End Function

        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            For Each child As UIElement In Children
                Select Case GetSides(child)
                    Case SolidSidesExtended.Top
                        child.Arrange(New Rect(New Point(0, 0), New Size(finalSize.Width, child.DesiredSize.Height)))
                    Case SolidSidesExtended.Bottom
                        child.Arrange(New Rect(New Point(0, finalSize.Height - child.DesiredSize.Height), New Size(finalSize.Width, child.DesiredSize.Height)))
                    Case SolidSidesExtended.Left
                        child.Arrange(New Rect(New Point(0, 0), New Size(child.DesiredSize.Width, finalSize.Height)))
                    Case SolidSidesExtended.Right
                        child.Arrange(New Rect(New Point(finalSize.Width - child.DesiredSize.Width, 0), New Size(child.DesiredSize.Width, finalSize.Height)))
                    Case SolidSidesExtended.FrontWindow
                        Dim width As Double = Math.Floor(finalSize.Width / 5R) * 5R
                        Dim height As Double = Math.Floor(finalSize.Height / 9R) * 9R
                        Dim x As Double = finalSize.Width / 2R - width / 2R
                        Dim y As Double = finalSize.Height / 2R - height / 2R
                        child.Arrange(New Rect(New Point(x, y), New Size(width, height)))
                    Case Else
                        child.Arrange(New Rect(New Point(0, 0), finalSize))
                End Select
            Next

            Return finalSize
        End Function
    End Class

    Friend Enum SolidSidesExtended
        Left = 0
        Top = 1
        Right = 2
        Bottom = 3
        Front = 4
        FrontWindow = 5
    End Enum
End Namespace
