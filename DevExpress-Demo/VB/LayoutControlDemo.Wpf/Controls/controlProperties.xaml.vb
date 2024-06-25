Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    Public Partial Class controlProperties
        Inherits UserControl

#Region "Dependency Properties"
        Public Shared ReadOnly LayoutControlProperty As DependencyProperty = DependencyProperty.Register("LayoutControl", GetType(LayoutControlBase), GetType(controlProperties), New PropertyMetadata(New PropertyChangedCallback(AddressOf LayoutControlDemo.controlProperties.OnLayoutControlChanged)))

        Public Shared ReadOnly LayoutControlPropertiesProperty As DependencyProperty = DependencyProperty.Register("LayoutControlProperties", GetType(FrameworkElement), GetType(controlProperties), Nothing)

        Public Shared ReadOnly ItemPropertiesProperty As DependencyProperty = DependencyProperty.Register("ItemProperties", GetType(FrameworkElement), GetType(controlProperties), Nothing)

        Public Shared ReadOnly ItemPropertiesHeaderTemplateProperty As DependencyProperty = DependencyProperty.Register("ItemPropertiesHeaderTemplate", GetType(DataTemplate), GetType(controlProperties), Nothing)

        Public Shared ReadOnly SelectedItemProperty As DependencyProperty = DependencyProperty.Register("SelectedItem", GetType(Object), GetType(controlProperties), New PropertyMetadata(New PropertyChangedCallback(AddressOf LayoutControlDemo.controlProperties.OnSelectedItemChanged)))

        Private Shared Sub OnLayoutControlChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, controlProperties).OnLayoutControlChanged()
        End Sub

        Private Shared Sub OnSelectedItemChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, controlProperties).OnSelectedItemChanged(e.OldValue, e.NewValue)
        End Sub

#End Region  ' Dependency Properties
        Public Sub New()
            InitializeComponent()
        End Sub

        Public Property LayoutControl As LayoutControlBase
            Get
                Return CType(GetValue(LayoutControlProperty), LayoutControlBase)
            End Get

            Set(ByVal value As LayoutControlBase)
                SetValue(LayoutControlProperty, value)
            End Set
        End Property

        Public Property LayoutControlProperties As FrameworkElement
            Get
                Return CType(GetValue(LayoutControlPropertiesProperty), FrameworkElement)
            End Get

            Set(ByVal value As FrameworkElement)
                SetValue(LayoutControlPropertiesProperty, value)
            End Set
        End Property

        Public Property ItemProperties As FrameworkElement
            Get
                Return CType(GetValue(ItemPropertiesProperty), FrameworkElement)
            End Get

            Set(ByVal value As FrameworkElement)
                SetValue(ItemPropertiesProperty, value)
            End Set
        End Property

        Public Property ItemPropertiesHeaderTemplate As DataTemplate
            Get
                Return CType(GetValue(ItemPropertiesHeaderTemplateProperty), DataTemplate)
            End Get

            Set(ByVal value As DataTemplate)
                SetValue(ItemPropertiesHeaderTemplateProperty, value)
            End Set
        End Property

        Public Property SelectedItem As Object
            Get
                Return CObj(GetValue(SelectedItemProperty))
            End Get

            Set(ByVal value As Object)
                SetValue(SelectedItemProperty, value)
            End Set
        End Property

        Private Function GetSelectedTileBackgroundMask() As Brush
            Dim gradientStops = New GradientStopCollection()
            gradientStops.Add(New GradientStop With {.Offset = 0, .Color = Color.FromArgb(70, 0, 0, 0)})
            gradientStops.Add(New GradientStop With {.Offset = 0.4, .Color = Color.FromArgb(0, 0, 0, 0)})
            gradientStops.Add(New GradientStop With {.Offset = 0.6, .Color = Color.FromArgb(0, 0, 0, 0)})
            gradientStops.Add(New GradientStop With {.Offset = 1, .Color = Color.FromArgb(70, 0, 0, 0)})
            Return New LinearGradientBrush(gradientStops, 0)
        End Function

        Private Sub OnInitLayoutControl(ByVal layoutControl As LayoutControlBase)
            For Each child As FrameworkElement In layoutControl.GetLogicalChildren(False)
                If TypeOf child Is SampleLayoutItem Then
                    AddHandler CType(child, SampleLayoutItem).IsSelectedChanged, AddressOf OnItemIsSelectedChanged
                ElseIf TypeOf child Is Tile Then
                    AddHandler child.MouseLeftButtonDown, AddressOf OnTileMouseLeftButtonDown
                ElseIf TypeOf child Is LayoutControlBase Then
                    OnInitLayoutControl(CType(child, LayoutControlBase))
                End If
            Next
        End Sub

        Private Sub OnItemIsSelectedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim layoutItem = CType(sender, SampleLayoutItem)
            If layoutItem.IsSelected Then SelectedItem = layoutItem
        End Sub

        Private Sub OnLayoutControlChanged()
            OnInitLayoutControl(LayoutControl)
        End Sub

        Private Sub OnSelectedItemChanged(ByVal oldValue As Object, ByVal newVaue As Object)
            If TypeOf oldValue Is SampleLayoutItem Then CType(oldValue, SampleLayoutItem).IsSelected = False
            If TypeOf oldValue Is Tile Then
                Dim tile = CType(oldValue, Tile)
                tile.SetValue(Tile.CalculatedBackgroundProperty, SelectedTileCalculatedBackground)
                SelectedTileCalculatedBackground = Nothing
                RemoveHandler tile.Loaded, AddressOf OnSelectedTileLoaded
            End If

            If TypeOf SelectedItem Is Tile Then
                Dim tile = CType(SelectedItem, Tile)
                SelectedTileCalculatedBackground = tile.CalculatedBackground
                UpdateSelectedTileBackgroundMask()
                AddHandler tile.Loaded, AddressOf OnSelectedTileLoaded
            End If
        End Sub

        Private Sub OnSelectedTileLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not LayoutControl.Controller.IsDragAndDrop Then UpdateSelectedTileBackgroundMask()
        End Sub

        Private Sub OnTileMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            SelectedItem = sender
        End Sub

        Private Sub UpdateSelectedTileBackgroundMask()
            CType(SelectedItem, Tile).SetValue(Tile.CalculatedBackgroundProperty, GetSelectedTileBackgroundMask())
        End Sub

        Private Property SelectedTileCalculatedBackground As Brush
    End Class

    Public Class ObjectToStringConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(String.IsNullOrEmpty(CStr(value)), Nothing, value)
        End Function
    End Class

    Public Class ObjectToTypeNameConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then
                Return value.GetType().Name
            Else
                Return Nothing
            End If
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ItemToHeaderConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then
                If TypeOf value Is SampleLayoutItem Then
                    Return CType(value, SampleLayoutItem).Caption
                ElseIf TypeOf value Is Tile Then
                    Return TryCast(CType(value, Tile).Header, String)
                Else
                    Return value.GetType().Name
                End If
            Else
                Return Nothing
            End If
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
