Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Layout

Namespace RichEditDemo

    Public Class HitTestPagePainter
        Inherits PagePainter

        Private _hitTestResult As RichEditHitTestResult

        Private _highlightOptions As Dictionary(Of LayoutType, RichEditPen)

        Private _defaultHighlightingPen As RichEditPen

        Public Sub New(ByVal hitTestResult As RichEditHitTestResult, ByVal highlightOptions As Dictionary(Of LayoutType, RichEditPen), ByVal defaultHighlightingPen As RichEditPen)
            _hitTestResult = hitTestResult
            _highlightOptions = highlightOptions
            _defaultHighlightingPen = defaultHighlightingPen
        End Sub

        Public Overrides Sub DrawPage(ByVal page As LayoutPage)
            MyBase.DrawPage(page)
            Dim currentHighLightPen As RichEditPen
            Dim layoutElement As LayoutElement
            While _hitTestResult IsNot Nothing
                layoutElement = _hitTestResult.LayoutElement
                If Not _highlightOptions.TryGetValue(layoutElement.Type, currentHighLightPen) Then currentHighLightPen = _defaultHighlightingPen
                If currentHighLightPen IsNot Nothing Then
                    If layoutElement.Type = LayoutType.FloatingPicture OrElse layoutElement.Type = LayoutType.TextBox Then
                        Dim pointToDraw As Point() = CType(layoutElement, LayoutFloatingObject).GetCoordinates()
                        Canvas.DrawLines(currentHighLightPen, pointToDraw)
                        Canvas.DrawLine(currentHighLightPen, pointToDraw(3), pointToDraw(0))
                    Else
                        Dim parentTextBox As LayoutTextBox = layoutElement.GetParentByType(Of LayoutTextBox)()
                        If parentTextBox IsNot Nothing Then
                            Using matrix As Matrix = parentTextBox.GetRotationMatrix()
                                Dim bounds As Rectangle = layoutElement.Bounds
                                Dim points As Point() = New Point() {New Point(bounds.X, bounds.Y), New Point(bounds.X + bounds.Width, bounds.Y), New Point(bounds.X + bounds.Width, bounds.Y + bounds.Height), New Point(bounds.X, bounds.Y + bounds.Height), New Point(bounds.X, bounds.Y)}
                                matrix.TransformPoints(points)
                                Canvas.DrawLines(currentHighLightPen, points)
                            End Using
                        Else
                            Canvas.DrawRectangle(currentHighLightPen, layoutElement.Bounds)
                        End If
                    End If
                End If

                _hitTestResult = _hitTestResult.Next
            End While
        End Sub
    End Class
End Namespace
