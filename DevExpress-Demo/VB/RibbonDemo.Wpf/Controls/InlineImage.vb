Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation

Namespace RibbonDemo

    Public Class InlineImage
        Inherits Panel

        Protected ReadOnly Property DefaultSize As Size
            Get
                Return New Size(100, 100)
            End Get
        End Property

        Private Property Shapes As Dictionary(Of InlineImageBorderType, String)

        Public Property PathData As String

        Private inlineImageViewModelCore As InlineImageViewModel

        Public Property InlineImageViewModel As InlineImageViewModel
            Get
                Return inlineImageViewModelCore
            End Get

            Protected Set(ByVal value As InlineImageViewModel)
                inlineImageViewModelCore = value
                AddHandler CType(inlineImageViewModelCore, INotifyPropertyChanged).PropertyChanged, AddressOf OnInlineImageViewModelPropertyChanged
                UpdateImage()
            End Set
        End Property

        Private Sub OnInlineImageViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            UpdateImage()
        End Sub

        Public Sub New(ByVal viewModel As InlineImageViewModel)
            Shapes = New Dictionary(Of InlineImageBorderType, String)()
            Shapes.Add(InlineImageBorderType.None, "M 0,0 L100,0 L100,100  L0,100 Z")
            Shapes.Add(InlineImageBorderType.Rectangle, "M 0,0 L100,0 L100,100  L0,100 Z")
            Shapes.Add(InlineImageBorderType.Circle, "M0,0 A50,50 0 0 0 0,100 A50,50 0 0 0 0,0")
            Shapes.Add(InlineImageBorderType.Triangle, "M0,2L1,0L2,2Z")
            Shapes.Add(InlineImageBorderType.Star, "M0,-10L2.9,-4.04L9.5,-3L4.25,1.5L5.8,8L0,5L-5.8,8L-4.25,1.5L-9.5,-3L-2.9,-4.04Z")
            Shapes.Add(InlineImageBorderType.LeftArrow, "M0,0L10,5L10,3L30,3L30,-3L10,-3L10,-5Z")
            Shapes.Add(InlineImageBorderType.RightArrow, "M0,0L-10,5L-10,3L-30,3L-30,-3L-10,-3L-10,-5Z")
            Shapes.Add(InlineImageBorderType.UpArrow, "M0,0L5,10L3,10L3,30L-3,30L-3,10L-5,10Z")
            Shapes.Add(InlineImageBorderType.DownArrow, "M0,0L5,-10L3,-10L3,-30L-3,-30L-3,-10L-5,-10Z")
            InlineImageViewModel = viewModel
        End Sub

        Private Function GetPath(ByVal size As Size) As Shapes.Path
            Dim image As BitmapImage = New BitmapImage(New Uri(InlineImageViewModel.ImageSource, UriKind.RelativeOrAbsolute))
            image.BaseUri = BaseUriHelper.GetBaseUri(Me)
            Dim brush As ImageBrush = New ImageBrush() With {.ImageSource = image}
            Dim path As Shapes.Path = New Shapes.Path()
            path.Fill = brush
            path.Stroke = New SolidColorBrush(InlineImageViewModel.Color)
            path.StrokeThickness = If(InlineImageViewModel.HasBorder, InlineImageViewModel.BorderWeight, 0)
            path.Stretch = Stretch.Uniform
            path.Width = size.Width
            path.Height = size.Height
            PathData = Shapes(InlineImageViewModel.ShapeType)
            path.SetBinding(Windows.Shapes.Path.DataProperty, New Binding("PathData") With {.Source = Me, .Mode = BindingMode.OneWay})
            Return path
        End Function

        Protected Overrides Sub OnRender(ByVal dc As DrawingContext)
            MyBase.OnRender(dc)
            Dim dv As DrawingVisual = New DrawingVisual()
            Dim vb As VisualBrush = New VisualBrush(GetPath(RenderSize))
            dc.DrawRectangle(vb, Nothing, New Rect(New Point(), RenderSize))
        End Sub

        Private Sub UpdateImage()
            Dim size As Size = New Size(DefaultSize.Width * InlineImageViewModel.Scale, DefaultSize.Height * InlineImageViewModel.Scale)
            Width = size.Width
            Height = size.Height
            InvalidateVisual()
        End Sub
    End Class
End Namespace
