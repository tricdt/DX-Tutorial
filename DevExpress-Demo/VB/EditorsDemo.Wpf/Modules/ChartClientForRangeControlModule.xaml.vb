Imports System
Imports System.Windows
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts.RangeControlClient
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class ChartClientForRangeControlModule
        Inherits EditorsDemoModule

        Const dataCount As Integer = 100

        Public Property ChartClientModel As ChartClientModel

        Public Sub New()
            InitializeComponent()
            lbDTGridAlignment.SelectedIndex = 0
            lbTSGridAlignment.SelectedIndex = 0
            ChartClientModel = New ChartClientModel()
            ChartClientModel.NumericItemsSource = GenerateNumericData()
            ChartClientModel.DateTimeItemsSource = GenerateDateTimeData()
            ChartClientModel.TimeSpanItemsSource = GenerateTimeSpanData()
            DataContext = Me
        End Sub

        Private Function GenerateNumericData() As Double()
            Dim data As Double() = New Double(99) {}
            Dim rand As Random = New Random()
            Dim value As Double = 0
            For i As Integer = 0 To dataCount - 1
                value +=(rand.NextDouble() - 0.5)
                data(i) = value
            Next

            Return data
        End Function

        Private Function GenerateDateTimeData() As List(Of DataSourceItem)
            Dim data As List(Of DataSourceItem) = New List(Of DataSourceItem)()
            Dim now As Date = Date.Now.Date
            Dim rand As Random = New Random()
            Dim value As Double = 0
            For i As Integer = 0 To dataCount - 1
                now = now.AddDays(1)
                value +=(rand.NextDouble() - 0.5)
                data.Add(New DataSourceItem() With {.Argument = now, .Value = value + Math.Sin(i * 0.6)})
            Next

            Return data
        End Function

        Private Function GenerateTimeSpanData() As List(Of DataSourceItem)
            Dim data As List(Of DataSourceItem) = New List(Of DataSourceItem)()
            Dim rand As Random = New Random()
            Dim value As Double = 0
            For i As Integer = 0 To dataCount - 1
                value +=(rand.NextDouble() - 0.5)
                data.Add(New DataSourceItem() With {.Argument = TimeSpan.FromMinutes(i * 30), .Value = value + Math.Sin(i * 0.6)})
            Next

            Return data
        End Function
    End Class

    Public Class ChartClientModel
        Inherits FrameworkElement

        Public Shared ReadOnly NumericItemsSourceProperty As DependencyProperty = DependencyProperty.Register("NumericItemsSource", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly DateTimeItemsSourceProperty As DependencyProperty = DependencyProperty.Register("DateTimeItemsSource", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly TimeSpanItemsSourceProperty As DependencyProperty = DependencyProperty.Register("TimeSpanItemsSource", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly MinimumDateTimeGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MinimumDateTimeGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(1.0))

        Public Shared ReadOnly MiddleDateTimeGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MiddleDateTimeGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(3.0))

        Public Shared ReadOnly MaximumDateTimeGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MaximumDateTimeGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(5.0))

        Public Shared ReadOnly MinimumTimeSpanGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MinimumTimeSpanGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(1.0))

        Public Shared ReadOnly MiddleTimeSpanGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MiddleTimeSpanGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(3.0))

        Public Shared ReadOnly MaximumTimeSpanGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MaximumTimeSpanGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(5.0))

        Public Shared ReadOnly DateTimeGridAlignmentProperty As DependencyProperty

        Public Shared ReadOnly TimeSpanGridAlignmentProperty As DependencyProperty

        Public Shared ReadOnly DateTimeGridSpacingVisibilityProperty As DependencyProperty = DependencyProperty.Register("DateTimeGridSpacingVisibility", GetType(Visibility), GetType(ChartClientModel), New PropertyMetadata(Visibility.Collapsed))

        Public Shared ReadOnly TimeSpanGridSpacingVisibilityProperty As DependencyProperty = DependencyProperty.Register("TimeSpanGridSpacingVisibility", GetType(Visibility), GetType(ChartClientModel), New PropertyMetadata(Visibility.Collapsed))

        Protected Shared Sub DateTimeGridAlignmentChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim model As ChartClientModel = TryCast(d, ChartClientModel)
            If model IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
                Dim gridAlignment As DateTimeGridAlignment = CType((CType(e.NewValue, ListBoxEditItem).Tag), DateTimeGridAlignment)
                model.DateTimeGridSpacingVisibility = If(gridAlignment.Equals(DateTimeGridAlignment.Auto), Visibility.Collapsed, Visibility.Visible)
                model.UpdateGridSpacing(gridAlignment)
            End If
        End Sub

        Protected Shared Sub TimeSpanGridAlignmentChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim model As ChartClientModel = TryCast(d, ChartClientModel)
            If model IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
                Dim gridAlignment As TimeSpanGridAlignment = CType((CType(e.NewValue, ListBoxEditItem).Tag), TimeSpanGridAlignment)
                model.TimeSpanGridSpacingVisibility = If(gridAlignment.Equals(TimeSpanGridAlignment.Auto), Visibility.Collapsed, Visibility.Visible)
                model.UpdateGridSpacing(gridAlignment)
            End If
        End Sub

        Shared Sub New()
            DateTimeGridAlignmentProperty = DependencyProperty.Register("DateTimeGridAlignment", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(DateTimeGridAlignment.Day, AddressOf DateTimeGridAlignmentChanged))
            TimeSpanGridAlignmentProperty = DependencyProperty.Register("TimeSpanGridAlignment", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(TimeSpanGridAlignment.Hour, AddressOf TimeSpanGridAlignmentChanged))
        End Sub

        Public Property MinimumDateTimeGridSpacing As Double
            Get
                Return CDbl(GetValue(MinimumDateTimeGridSpacingProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MinimumDateTimeGridSpacingProperty, value)
            End Set
        End Property

        Public Property MiddleDateTimeGridSpacing As Double
            Get
                Return CDbl(GetValue(MiddleDateTimeGridSpacingProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MiddleDateTimeGridSpacingProperty, value)
            End Set
        End Property

        Public Property MaximumDateTimeGridSpacing As Double
            Get
                Return CDbl(GetValue(MaximumDateTimeGridSpacingProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MaximumDateTimeGridSpacingProperty, value)
            End Set
        End Property

        Public Property MinimumTimeSpanGridSpacing As Double
            Get
                Return CDbl(GetValue(MinimumTimeSpanGridSpacingProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MinimumTimeSpanGridSpacingProperty, value)
            End Set
        End Property

        Public Property MiddleTimeSpanGridSpacing As Double
            Get
                Return CDbl(GetValue(MiddleTimeSpanGridSpacingProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MiddleTimeSpanGridSpacingProperty, value)
            End Set
        End Property

        Public Property MaximumTimeSpanGridSpacing As Double
            Get
                Return CDbl(GetValue(MaximumTimeSpanGridSpacingProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MaximumTimeSpanGridSpacingProperty, value)
            End Set
        End Property

        Public Property NumericItemsSource As Object
            Get
                Return GetValue(NumericItemsSourceProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(NumericItemsSourceProperty, value)
            End Set
        End Property

        Public Property DateTimeItemsSource As Object
            Get
                Return GetValue(DateTimeItemsSourceProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(DateTimeItemsSourceProperty, value)
            End Set
        End Property

        Public Property TimeSpanItemsSource As Object
            Get
                Return GetValue(TimeSpanItemsSourceProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(TimeSpanItemsSourceProperty, value)
            End Set
        End Property

        Public Property DateTimeGridSpacingVisibility As Visibility
            Get
                Return CType(GetValue(DateTimeGridSpacingVisibilityProperty), Visibility)
            End Get

            Set(ByVal value As Visibility)
                SetValue(DateTimeGridSpacingVisibilityProperty, value)
            End Set
        End Property

        Public Property TimeSpanGridSpacingVisibility As Visibility
            Get
                Return CType(GetValue(TimeSpanGridSpacingVisibilityProperty), Visibility)
            End Get

            Set(ByVal value As Visibility)
                SetValue(TimeSpanGridSpacingVisibilityProperty, value)
            End Set
        End Property

        Private Sub UpdateGridSpacing(ByVal gridAlignment As DateTimeGridAlignment)
            MinimumDateTimeGridSpacing = GetMinimumGridSpacing(gridAlignment)
            MaximumDateTimeGridSpacing = GetMaximumGridSpacing(gridAlignment)
            MiddleDateTimeGridSpacing =(MinimumDateTimeGridSpacing + MaximumDateTimeGridSpacing) / 2
        End Sub

        Private Sub UpdateGridSpacing(ByVal gridAlignment As TimeSpanGridAlignment)
            MinimumTimeSpanGridSpacing = GetMinimumGridSpacing(gridAlignment)
            MaximumTimeSpanGridSpacing = GetMaximumGridSpacing(gridAlignment)
            MiddleTimeSpanGridSpacing =(MinimumTimeSpanGridSpacing + MaximumTimeSpanGridSpacing) / 2
        End Sub

        Private Function GetMaximumGridSpacing(ByVal gridAlignment As DateTimeGridAlignment) As Double
            Select Case gridAlignment
                Case DateTimeGridAlignment.Day
                    Return 15
                Case DateTimeGridAlignment.Month
                    Return 3
                Case DateTimeGridAlignment.Week
                    Return 6
                Case Else
                    Return 5
            End Select
        End Function

        Private Function GetMinimumGridSpacing(ByVal gridAlignment As DateTimeGridAlignment) As Double
            Select Case gridAlignment
                Case DateTimeGridAlignment.Day
                    Return 5
                Case DateTimeGridAlignment.Month
                    Return 1
                Case DateTimeGridAlignment.Week
                    Return 2
                Case Else
                    Return 1
            End Select
        End Function

        Private Function GetMaximumGridSpacing(ByVal gridAlignment As TimeSpanGridAlignment) As Double
            Select Case gridAlignment
                Case TimeSpanGridAlignment.Hour
                    Return 12
                Case TimeSpanGridAlignment.Minute
                    Return 720
                Case Else
                    Return 1
            End Select
        End Function

        Private Function GetMinimumGridSpacing(ByVal gridAlignment As TimeSpanGridAlignment) As Double
            Select Case gridAlignment
                Case TimeSpanGridAlignment.Hour
                    Return 4
                Case TimeSpanGridAlignment.Minute
                    Return 240
                Case Else
                    Return 1
            End Select
        End Function
    End Class

    Public Class DataSourceItem

        Public Property Argument As Object

        Public Property Value As Object
    End Class

    Public Enum ChartViewType
        Area
        Bar
        Line
    End Enum

    Public Class ChartViewTypeConverter
        Implements IValueConverter

        Private Function Parse(ByVal type As String) As RangeControlClientView
            If Equals(type, "Area") Then Return New RangeControlClientAreaView()
            If Equals(type, "Bar") Then Return New RangeControlClientBarView()
            If Equals(type, "Line") Then Return New RangeControlClientLineView()
            Return Nothing
        End Function

        Private Function Parse(ByVal type As ChartViewType) As RangeControlClientView
            If type = ChartViewType.Area Then Return New RangeControlClientAreaView()
            If type = ChartViewType.Bar Then Return New RangeControlClientBarView()
            If type = ChartViewType.Line Then Return New RangeControlClientLineView()
            Return Nothing
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String Then Return Parse(TryCast(value, String))
            If TypeOf value Is ChartViewType Then Return Parse(CType(value, ChartViewType))
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf parameter Is RangeControlClientAreaView Then Return ChartViewType.Area
            If TypeOf parameter Is RangeControlClientBarView Then Return ChartViewType.Bar
            If TypeOf parameter Is RangeControlClientLineView Then Return ChartViewType.Line
            Return String.Empty
        End Function
    End Class
End Namespace
