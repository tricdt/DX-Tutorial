Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.Xpf.RichEdit
Imports DrawingPoint = System.Drawing.Point
Imports System.Text

Namespace RichEditDemo

    Public Partial Class HitTesting
        Inherits RichEditDemoModule

        Private _highlightOptions As Dictionary(Of LayoutType, RichEditPen) = New Dictionary(Of LayoutType, RichEditPen)()

        Public Sub New()
            InitializeComponent()
            SpecifyHighlightOptions()
        End Sub

        Private Property CurrentPageIndex As Integer

        Private Property HitTestResult As RichEditHitTestResult

        Private Property MousePosition As Point

        Private ReadOnly Property HighlightOptions As Dictionary(Of LayoutType, RichEditPen)
            Get
                Return _highlightOptions
            End Get
        End Property

        Private Sub RichEditControl_BeforePagePaint(ByVal sender As Object, ByVal e As BeforePagePaintEventArgs)
            If e.CanvasOwnerType = CanvasOwnerType.Control AndAlso e.Page.Index = CurrentPageIndex AndAlso HitTestResult IsNot Nothing Then
                Dim defaultHighlightingPen As RichEditPen = If(highlightOther.IsChecked = True, New RichEditPen(otherHigtlightColor.Color, 3), Nothing)
                e.Painter = New HitTestPagePainter(RichEditHitTestResult.Reverse(HitTestResult), HighlightOptions, defaultHighlightingPen)
            End If
        End Sub

        Private Sub HitTest(ByVal point As DrawingPoint)
            Dim pageLayoutPosition As PageLayoutPosition = RichEditControl.ActiveView.GetDocumentLayoutPosition(point)
            If pageLayoutPosition Is Nothing Then
                HitTestResult = Nothing
                Return
            End If

            CurrentPageIndex = pageLayoutPosition.PageIndex
            Dim position As DrawingPoint = pageLayoutPosition.Position
            Dim page As LayoutPage = RichEditControl.DocumentLayout.GetPage(CurrentPageIndex)
            Dim hitTestManager As HitTestManager = New HitTestManager(richEdit.DocumentLayout)
            Dim searchOption As HitTestSearchOption = If(exactHit.IsChecked.Value, HitTestSearchOption.Exact, HitTestSearchOption.Nearest)
            Select Case CType(cbScope.EditValue, ScopeType)
                Case ScopeType.Page
                    HitTestResult = hitTestManager.HitTest(page, position, searchOption)
                Case ScopeType.MainPageArea
                    HitTestResult = hitTestManager.HitTest(page.PageAreas(0), position, searchOption)
                Case ScopeType.HeaderPageArea
                    If page.Header IsNot Nothing Then HitTestResult = hitTestManager.HitTest(page.Header, position, searchOption)
                Case ScopeType.FooterPageArea
                    If page.Footer IsNot Nothing Then HitTestResult = hitTestManager.HitTest(page.Footer, position, searchOption)
            End Select
        End Sub

        Private Sub HighlightPotionsChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SpecifyHighlightOptions()
        End Sub

        Private Sub SpecifyHighlightOptions()
            HighlightOptions.Clear()
            HighlightOptions.Add(LayoutType.Page, If(highlightPage.IsChecked = True, New RichEditPen(pageHigtlightColor.Color, 3), Nothing))
            HighlightOptions.Add(LayoutType.PageArea, If(highlightPageArea.IsChecked = True, New RichEditPen(pageAreaHigtlightColor.Color, 3), Nothing))
            HighlightOptions.Add(LayoutType.Column, If(highlightColumn.IsChecked = True, New RichEditPen(columnHigtlightColor.Color, 3), Nothing))
            HighlightOptions.Add(LayoutType.Row, If(highlightRow.IsChecked = True, New RichEditPen(rowHigtlightColor.Color, 3), Nothing))
            HighlightOptions.Add(LayoutType.PlainTextBox, If(highlightBox.IsChecked = True, New RichEditPen(boxHigtlightColor.Color, 3), Nothing))
            HighlightOptions.Add(LayoutType.CharacterBox, If(highlightCharacterBox.IsChecked = True, New RichEditPen(characterBoxHigtlightColor.Color, 3), Nothing))
        End Sub

        Private Sub RichEditControl_MouseMove(ByVal sender As Object, ByVal e As Input.MouseEventArgs)
            Dim position As Point = e.GetPosition(richEdit)
            If MousePosition <> position Then
                HitTest(New DrawingPoint(CInt(position.X), CInt(position.Y)))
                RichEditControl.Refresh()
                MousePosition = position
            End If
        End Sub

        Private Function Concat(ByVal c As Char, ByVal count As Integer) As String
            Dim builder As StringBuilder = New StringBuilder(count)
            builder.Append(c, count)
            Return builder.ToString()
        End Function

        Private Sub RichEditControl_MouseLeftButtonUp(ByVal sender As Object, ByVal e As Input.MouseButtonEventArgs)
            lbResult.Items.Clear()
            Dim reversedResult As RichEditHitTestResult = RichEditHitTestResult.Reverse(HitTestResult)
            Dim i As Integer = 0
            While reversedResult IsNot Nothing
                Dim item As String = String.Format("{0}- {1}", Concat(" "c, i * 2), reversedResult.LayoutElement.Type)
                lbResult.Items.Add(item)
                reversedResult = reversedResult.Next
                i += 1
            End While
        End Sub
    End Class
End Namespace
