Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Bars
Imports ControlsDemo.GalleryDemo
Imports System.Windows.Media.Effects
Imports DevExpress.Xpf.Core
Imports DevExpress.Data.Extensions

Namespace ControlsDemo

    Public Partial Class GalleryPhotoViewer
        Inherits ControlsDemo.ControlsDemoModule

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Property SlideMode As Boolean

        Private Sub GalleryMenu_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.GalleryItemEventArgs)
            Me.gallery.ScrollToGroupByIndex(Me.galleryMenu.Gallery.Groups(CInt((0))).Items.IndexOf(e.Item))
        End Sub

        Private Sub ControlPanel_CommandClick(ByVal sender As Object, ByVal e As ControlsDemo.GalleryDemo.ControlPanelEventArgs)
            Select Case e.Command
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.ZoomValueChanged
                    Me.imageViewer.ScaleCenter(Me.controlPanel.ZoomValue / 100)
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.RotateLeft
                    Me.RotateCounterclockwise()
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.RotateRight
                    Me.RotateClockwise()
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.ZoomToOriginalSize
                    Me.controlPanel.SetAndAnimateZoomValue(100)
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.VerSize
                    Me.controlPanel.SetAndAnimateZoomValue(Me.imageViewer.VerticalFitScale * 100)
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.HorSize
                    Me.controlPanel.SetAndAnimateZoomValue(Me.imageViewer.HorizontalFitScale * 100)
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.AutoSize
                    Me.controlPanel.SetAndAnimateZoomValue(Me.imageViewer.GetBestFitScale() * 100)
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.[Next]
                    Me.ShowItemInImageVewer(Me.GetNextItem(CType(Me.imageViewer.Tag, DevExpress.Xpf.Bars.GalleryItem)))
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.Prior
                    Me.ShowItemInImageVewer(Me.GetPriorItem(CType(Me.imageViewer.Tag, DevExpress.Xpf.Bars.GalleryItem)))
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.Play
                    Me.slideViewer.Visibility = System.Windows.Visibility.Visible
                    Me.slideViewer.NextSlideImageSource = CType(Me.gallery.Gallery.Groups(CInt((0))).Items(CInt((0))).Caption, ControlsDemo.PhotoInfo).Source
                    Me.slideViewer.Tag = Me.gallery.Gallery.Groups(CInt((0))).Items(0)
                    Me.SlideMode = True
                    Me.slideViewer.Play()
                Case ControlsDemo.GalleryDemo.ControlPanelCommand.Print
                    Me.PrintCurrentImage()
            End Select
        End Sub

        Private Sub RotateClockwise()
            Select Case Me.imageViewer.Rotation
                Case System.Windows.Media.Imaging.Rotation.Rotate0
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate90
                Case System.Windows.Media.Imaging.Rotation.Rotate90
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate180
                Case System.Windows.Media.Imaging.Rotation.Rotate180
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate270
                Case System.Windows.Media.Imaging.Rotation.Rotate270
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate0
            End Select
        End Sub

        Private Sub RotateCounterclockwise()
            Select Case Me.imageViewer.Rotation
                Case System.Windows.Media.Imaging.Rotation.Rotate0
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate270
                Case System.Windows.Media.Imaging.Rotation.Rotate90
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate0
                Case System.Windows.Media.Imaging.Rotation.Rotate180
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate90
                Case System.Windows.Media.Imaging.Rotation.Rotate270
                    Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate180
            End Select
        End Sub

        Private Function GetNextItem(ByVal item As DevExpress.Xpf.Bars.GalleryItem) As GalleryItem
            Return Me.GetItem(item, True)
        End Function

        Private Function GetPriorItem(ByVal item As DevExpress.Xpf.Bars.GalleryItem) As GalleryItem
            Return Me.GetItem(item, False)
        End Function

        Private Function GetItem(ByVal item As DevExpress.Xpf.Bars.GalleryItem, ByVal [next] As Boolean) As GalleryItem
            If item Is Nothing Then Return Nothing
            Dim coeff = If([next], 1, -1)
            Dim itemIndex = item.Group.Items.IndexOf(item) + coeff
            If item.Group.Items.IsValidIndex(itemIndex) Then Return item.Group.Items(itemIndex)
            Dim groups = item.Group.Gallery.Groups
            Dim groupIndex =(groups.IndexOf(item.Group) + coeff + groups.Count) Mod groups.Count
            Dim group = groups(groupIndex)
            Return If([next], group.Items.First(), group.Items.Last())
        End Function

        Private Sub bntCloseImageViewer_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.CloseImageViewPopup()
        End Sub

        Private Sub CloseImageViewPopup()
            Me.mainView.Effect = Nothing
            Me.imageViewPopup.Visibility = System.Windows.Visibility.Collapsed
        End Sub

        Private Sub Gallery_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.GalleryItemEventArgs)
            Me.OpenImageViewPopup(e.Item)
        End Sub

        Private Sub OpenImageViewPopup(ByVal item As DevExpress.Xpf.Bars.GalleryItem)
            Me.ShowItemInImageVewer(item)
            Me.mainView.Effect = New System.Windows.Media.Effects.BlurEffect() With {.Radius = 3}
            Me.imageViewPopup.Visibility = System.Windows.Visibility.Visible
        End Sub

        Private Sub ShowItemInImageVewer(ByVal item As DevExpress.Xpf.Bars.GalleryItem)
            Me.imageViewerTitle.Text = CType(item.Caption, ControlsDemo.PhotoInfo).Caption
            Me.imageViewer.ImageSource = CType(CType(item.Caption, ControlsDemo.PhotoInfo).Source, System.Windows.Media.Imaging.BitmapSource)
            Me.imageViewer.Tag = item
            Me.imageViewer.Rotation = System.Windows.Media.Imaging.Rotation.Rotate0
            AddHandler Me.imageViewer.LayoutUpdated, New System.EventHandler(AddressOf Me.imageViewer_LayoutUpdated)
            Me.FitImageInViewport()
        End Sub

        Private Sub imageViewer_LayoutUpdated(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.FitImageInViewport()
            RemoveHandler Me.imageViewer.LayoutUpdated, New System.EventHandler(AddressOf Me.imageViewer_LayoutUpdated)
        End Sub

        Private Sub FitImageInViewport()
            If Me.imageViewer.ImageSource Is Nothing Then
                Me.controlPanel.ZoomValue = 100
                Return
            End If

            Dim scaleWidth As Double = System.Math.Min(1.0, (Me.imageViewer.Viewport.ActualWidth - 20) / Me.imageViewer.ImageSource.PixelWidth)
            Dim scaleHeight As Double = System.Math.Min(1.0, (Me.imageViewer.Viewport.ActualHeight - 20) / Me.imageViewer.ImageSource.PixelHeight)
            Me.controlPanel.ZoomValue = 100 * System.Math.Min(scaleWidth, scaleHeight)
        End Sub

        Private Sub imageViewer_MouseWheelZoom(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.controlPanel.ZoomValue = Me.imageViewer.Scale * 100
        End Sub

        Private Sub slideViewer_BeforeCurrentSlideLoading(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim item As DevExpress.Xpf.Bars.GalleryItem = Me.GetNextItem(CType(Me.slideViewer.Tag, DevExpress.Xpf.Bars.GalleryItem))
            Me.slideViewer.Tag = item
            Me.slideViewer.NextSlideImageSource = CType(item.Caption, ControlsDemo.PhotoInfo).Source
        End Sub

        Protected Overridable Sub OnMouseDownCore()
            If Me.SlideMode Then
                Me.SlideMode = False
                Me.slideViewer.[Stop]()
                Me.slideViewer.Visibility = System.Windows.Visibility.Collapsed
            End If
        End Sub

        Protected Overrides Sub OnMouseLeftButtonDown(ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Me.OnMouseDownCore()
            MyBase.OnMouseLeftButtonDown(e)
        End Sub

        Protected Overrides Sub OnMouseRightButtonDown(ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Me.OnMouseDownCore()
            MyBase.OnMouseRightButtonDown(e)
        End Sub

        Public Sub PrintCurrentImage()
            Dim printDialog As System.Windows.Controls.PrintDialog = New System.Windows.Controls.PrintDialog()
            If printDialog.ShowDialog() = True Then
                Dim photoInfo As ControlsDemo.PhotoInfo = CType(CType(Me.imageViewer.Tag, DevExpress.Xpf.Bars.GalleryItem).Caption, ControlsDemo.PhotoInfo)
                printDialog.PrintVisual(New System.Windows.Controls.Image() With {.Source = photoInfo.Source}, photoInfo.Caption)
            End If
        End Sub

        Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Input.KeyEventArgs)
            MyBase.OnKeyDown(e)
            If e.Key = System.Windows.Input.Key.Escape Then
                If Me.SlideMode Then
                    Me.SlideMode = False
                    Me.slideViewer.[Stop]()
                    Me.slideViewer.Visibility = System.Windows.Visibility.Collapsed
                    Return
                End If

                If Me.imageViewPopup.Visibility = System.Windows.Visibility.Visible Then
                    Me.CloseImageViewPopup()
                End If
            End If
        End Sub
    End Class

    Public Class PhotoInfo

        Public Property Source As ImageSource

        Public Property Caption As String

        Public Property SizeInfo As String

        Public Property ViewSize As Size

        Public Sub New()
            Me.ViewSize = New System.Windows.Size(Double.NaN, Double.NaN)
        End Sub
    End Class
End Namespace
