Imports System
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports System.Collections.Generic
Imports DevExpress.XtraRichEdit.Services
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Layout
Imports System.Windows.Input
Imports System.Windows

Namespace RichEditDemo

    Public Partial Class CustomDraw
        Inherits RichEditDemoModule

        Private _searchResult As List(Of FixedRange) = New List(Of FixedRange)()

        Private _currentItemIndex As Integer = -1

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Property CurrentItemIndex As Integer
            Get
                Return _currentItemIndex
            End Get

            Set(ByVal value As Integer)
                If value < 0 OrElse value >= SearchResult.Count Then Return
                _currentItemIndex = value
                Dim fixedRange As FixedRange = SearchResult(_currentItemIndex)
                Dim range As DocumentRange = RichEditControl.Document.CreateRange(fixedRange.Start, fixedRange.Length)
                RichEditControl.Document.ChangeActiveDocument(RichEditControl.Document)
                RichEditControl.Document.Selection = range
                RichEditControl.ScrollToCaret()
                UpdateSearchOptionsUI()
                RichEditControl.Refresh()
            End Set
        End Property

        Private ReadOnly Property SearchResult As List(Of FixedRange)
            Get
                Return _searchResult
            End Get
        End Property

        Private ReadOnly Property IsSelectionInMainDocument As Boolean
            Get
                Return Not richEdit.IsSelectionInTextBox AndAlso Not richEdit.IsSelectionInHeaderOrFooter AndAlso Not richEdit.IsSelectionInComment
            End Get
        End Property

        Private Sub RichEditControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            richEdit.ReplaceService(Of IRichEditCommandFactoryService)(New CustomsRichEditCommandFactoryService(richEdit, richEdit.GetService(Of IRichEditCommandFactoryService)(), searchTextBox))
            FindText()
        End Sub

        Private Sub RichEditControl_BeforePagePaint(ByVal sender As Object, ByVal e As BeforePagePaintEventArgs)
            If e.CanvasOwnerType = CanvasOwnerType.Printer OrElse SearchResult.Count = 0 Then Return
            Dim currentItem As FixedRange = SearchResult(CurrentItemIndex)
            Dim visibleSearchResult As List(Of FixedRange) = GetVisibleRanges(SearchResult, e.Page.MainContentRange)
            e.Painter = New CustomDrawPagePainter(richEdit, visibleSearchResult, currentItem)
        End Sub

        Private Function GetVisibleRanges(ByVal ranges As List(Of FixedRange), ByVal visibleRange As FixedRange) As List(Of FixedRange)
            Dim result As List(Of FixedRange) = New List(Of FixedRange)()
            Dim visibleRangeEnd As Integer = visibleRange.Start + visibleRange.Length
            For Each range As FixedRange In ranges
                If visibleRangeEnd < range.Start Then Exit For
                If visibleRange.Contains(range) Then result.Add(range)
            Next

            Return result
        End Function

        Private Sub RichEditControl_ContentChanged(ByVal sender As Object, ByVal e As EventArgs)
            SearchResult.Clear()
            RichEditControl.Refresh()
        End Sub

        Private Sub CancelSearchButtonInfo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            searchTextBox.Text = String.Empty
        End Sub

        Private Sub ButtonDownInfo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CurrentItemIndex += 1
        End Sub

        Private Sub ButtonUpInfo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CurrentItemIndex -= 1
        End Sub

        Private Sub searchTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If e.Key = Key.Enter Then
                FindText()
            ElseIf e.Key = Key.Escape Then
                searchTextBox.Text = String.Empty
                RichEditControl.Focus()
            End If
        End Sub

        Private Sub UpdateSearchOptionsUI()
            navigationButtonEdit.Visibility = If(Not String.IsNullOrEmpty(searchTextBox.Text) AndAlso SearchResult.Count > 0, Visibility.Visible, Visibility.Hidden)
            For Each button As ButtonInfo In searchTextBox.Buttons
                button.Visibility = If(Not String.IsNullOrEmpty(searchTextBox.Text), Visibility.Visible, Visibility.Hidden)
            Next

            If String.IsNullOrEmpty(searchTextBox.Text) Then
                labelResultCount.Text = String.Empty
            ElseIf SearchResult.Count = 0 Then
                labelResultCount.Text = "No matches"
            Else
                labelResultCount.Text = String.Format("{0} of {1} matches", CurrentItemIndex + 1, SearchResult.Count)
            End If
        End Sub

        Private Sub FindText()
            SearchResult.Clear()
            Dim textToSearch As String = searchTextBox.Text
            If IsSelectionInMainDocument AndAlso Not String.IsNullOrEmpty(textToSearch) Then
                Dim options As SearchOptions = GetSearchOptions()
                Dim ranges As DocumentRange() = RichEditControl.Document.FindAll(textToSearch, options)
                For i As Integer = 0 To ranges.Length - 1
                    SearchResult.Add(New FixedRange(ranges(i).Start.ToInt(), ranges(i).Length))
                Next
            End If

            CurrentItemIndex = 0
            UpdateSearchOptionsUI()
            RichEditControl.Refresh()
        End Sub

        Private Function GetSearchOptions() As SearchOptions
            Dim result As SearchOptions = SearchOptions.None
            If edtMatchCase.IsChecked.Value Then result = result Or SearchOptions.CaseSensitive
            If edtFindWholeWordsOnly.IsChecked.Value Then result = result Or SearchOptions.WholeWord
            Return result
        End Function

        Private Sub SearchOptions_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            FindText()
        End Sub
    End Class
End Namespace
