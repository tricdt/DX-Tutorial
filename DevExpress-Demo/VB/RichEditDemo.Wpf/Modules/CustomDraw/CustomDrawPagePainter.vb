Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native

Namespace RichEditDemo

    Public Class CustomDrawPagePainter
        Inherits PagePainter

        Private _rangesForHighlight As List(Of FixedRange)

        Private _richEditControl As RichEditControl

        Private _rangeForRowHighlight As FixedRange

        Public Sub New(ByVal richEdit As RichEditControl, ByVal rangesForHighlight As List(Of FixedRange), ByVal rangeForRowHighlight As FixedRange)
            _richEditControl = richEdit
            _rangesForHighlight = rangesForHighlight
            _rangeForRowHighlight = rangeForRowHighlight
        End Sub

        Private Property IsHighlightingAllowed As Boolean

        Public Overrides Sub DrawPageArea(ByVal pageArea As LayoutPageArea)
            IsHighlightingAllowed = True
            MyBase.DrawPageArea(pageArea)
            IsHighlightingAllowed = False
        End Sub

        Public Overrides Sub DrawRow(ByVal row As LayoutRow)
            If IsHighlightingAllowed AndAlso row.Range.Intersect(_rangeForRowHighlight) Then
                Dim pen As RichEditPen = New RichEditPen(Colors.Blue, Canvas.ConvertToDrawingLayoutUnits(2, _richEditControl.LayoutUnit))
                Canvas.DrawRectangle(pen, row.Bounds)
            End If

            MyBase.DrawRow(row)
        End Sub

        Public Overrides Sub DrawPlainTextBox(ByVal plainTextBox As PlainTextBox)
            HighlightElement(plainTextBox)
            MyBase.DrawPlainTextBox(plainTextBox)
        End Sub

        Public Overrides Sub DrawPageNumberBox(ByVal pageNumberBox As PlainTextBox)
            HighlightElement(pageNumberBox)
            MyBase.DrawPageNumberBox(pageNumberBox)
        End Sub

        Public Overrides Sub DrawSpaceBox(ByVal spaceBox As PlainTextBox)
            HighlightElement(spaceBox)
            MyBase.DrawSpaceBox(spaceBox)
        End Sub

        Private Sub HighlightElement(ByVal element As PlainTextBox)
            If Not IsHighlightingAllowed Then Return
            Dim range As FixedRange = _rangesForHighlight.FirstOrDefault(Function(r) element.Range.Intersect(r))
            If range Is Nothing Then Return
            Dim brush As RichEditBrush = New RichEditBrush(Colors.Yellow)
            If range.Equals(element.Range) Then
                Canvas.FillRectangle(brush, element.Bounds)
            Else
                Dim characterBoxes As CharacterBoxCollection = _richEditControl.DocumentLayout.Split(element)
                Dim firstBox As CharacterBox = characterBoxes(0)
                Dim lastBox As CharacterBox = characterBoxes(characterBoxes.Count - 1)
                For Each box As CharacterBox In characterBoxes
                    If box.Range.Start = range.Start Then firstBox = box
                    If box.Range.Start + box.Range.Length = range.Start + range.Length Then lastBox = box
                Next

                Canvas.FillRectangle(brush, System.Drawing.Rectangle.FromLTRB(firstBox.Bounds.X, firstBox.Bounds.Y, lastBox.Bounds.Right, lastBox.Bounds.Bottom))
            End If
        End Sub
    End Class
End Namespace
