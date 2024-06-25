Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Dialogs
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Class MapEditorViewModel
        Inherits BindableBase

        Private ReadOnly itemIndexes As Dictionary(Of Type, Long) = New Dictionary(Of Type, Long)()

        Private activeItemsField As ObservableCollection(Of MapItem)

        Private fillColorField As Color

        Private strokeColorField As Color

        Public Property ActiveItems As ObservableCollection(Of MapItem)
            Get
                Return activeItemsField
            End Get

            Set(ByVal value As ObservableCollection(Of MapItem))
                activeItemsField = value
                OnActiveItemsChanged()
            End Set
        End Property

        Public Property FillColor As Color
            Get
                Return fillColorField
            End Get

            Set(ByVal value As Color)
                If fillColorField = value Then Return
                fillColorField = value
                OnColorValueChanged()
            End Set
        End Property

        Public Property StrokeColor As Color
            Get
                Return strokeColorField
            End Get

            Set(ByVal value As Color)
                If strokeColorField = value Then Return
                strokeColorField = value
                OnColorValueChanged()
            End Set
        End Property

        Public Sub New()
            SetDefaultColor()
        End Sub

        Private Function GenerateName(ByVal item As MapItem) As String
            Dim itemType As Type = item.GetType()
            If Not itemIndexes.ContainsKey(itemType) Then itemIndexes(itemType) = 0
            itemIndexes(itemType) += 1
            Return String.Format("{0} {1}", itemType.Name, itemIndexes(itemType))
        End Function

        Private Sub SetDefaultColor()
            fillColorField = Color.FromArgb(142, 0, 176, 80)
            strokeColorField = Colors.White
            RaisePropertyChanged("FillColor")
            RaisePropertyChanged("StrokeColor")
        End Sub

        Private Sub OnColorValueChanged()
            If ActiveItems IsNot Nothing Then
                For Each item As MapItem In ActiveItems
                    UpdateColor(item)
                Next
            End If
        End Sub

        Private Sub OnActiveItemsChanged()
            Dim shape As MapShapeBase = If(ActiveItems IsNot Nothing AndAlso ActiveItems.Count = 1, TryCast(ActiveItems(0), MapShapeBase), Nothing)
            If shape IsNot Nothing Then
                fillColorField = CType(shape.Fill, SolidColorBrush).Color
                strokeColorField = CType(shape.Stroke, SolidColorBrush).Color
                RaisePropertyChanged("FillColor")
                RaisePropertyChanged("StrokeColor")
            Else
                SetDefaultColor()
            End If
        End Sub

        Private Sub UpdateColor(ByVal item As MapItem)
            Dim shape As MapShapeBase = TryCast(item, MapShapeBase)
            If shape IsNot Nothing Then
                shape.Fill = New SolidColorBrush(FillColor)
                shape.Stroke = New SolidColorBrush(StrokeColor)
            End If
        End Sub

        Public Sub OnShapesLoaded(ByVal items As List(Of MapItem))
            items(0).EnableHighlighting = False
            items(0).IsHitTestVisible = False
            ActiveItems = New ObservableCollection(Of MapItem)() From {items(50)}
        End Sub

        Public Sub OnMapItemCreating(ByVal item As MapItem)
            item.Attributes.Add(New MapItemAttribute() With {.Name = "name", .Value = GenerateName(item)})
            UpdateColor(item)
        End Sub

        Public Sub Export(ByVal layer As VectorLayerBase)
            Using dialog = New DXSaveFileDialog()
                dialog.Filter = "KML files|*.kml"
                dialog.CreatePrompt = True
                dialog.OverwritePrompt = True
                If dialog.ShowDialog().Value Then
                    layer.ExportToKml(dialog.FileName)
                    ThemedMessageBox.Show("Info", String.Format("Items successfully exported to {0} file", dialog.FileName), MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End Using
        End Sub
    End Class

    Public Class SelectedItemToPerimeterConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim shape As MapShape = TryCast(value, MapShape)
            Return If(shape IsNot Nothing, Math.Round(GeoUtils.CalculateStrokeLength(shape), 3) & " m", String.Empty)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class SelectedItemToAreaConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim shape As MapShape = TryCast(value, MapShape)
            Return If(shape IsNot Nothing, Math.Round(GeoUtils.CalculateArea(shape), 3) & " m²", String.Empty)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class SelectedItemToDiameterConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim dot As MapDot = TryCast(value, MapDot)
            Return If(dot IsNot Nothing, CInt(dot.Size) & " dip", String.Empty)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class FontHeightToRectangleSizeConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return CInt((CDbl(value) / 2))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class EditorModeToToolTipEnabledConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value Is Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class ItemToolTipTemplateConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Property ShapeTemplate As DataTemplate

        Public Property DotTemplate As DataTemplate

        Public Property PinTemplate As DataTemplate

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is MapDot Then Return DotTemplate
            Return If(TypeOf value Is MapShape, ShapeTemplate, PinTemplate)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
