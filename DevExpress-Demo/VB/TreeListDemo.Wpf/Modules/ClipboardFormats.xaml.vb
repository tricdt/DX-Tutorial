Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows

Namespace TreeListDemo

    Public Partial Class ClipboardFormats
        Inherits TreeListDemoModule

        Protected Overrides ReadOnly Property ShowBorder As Boolean
            Get
                Return True
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            view.SelectCells(1, treeList.Columns.First(), 15, treeList.Columns.Last())
            CopyAndPasteClipboardData()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CopyAndPasteClipboardData()
        End Sub

        Private Sub CopyAndPasteClipboardData()
            view.CopySelectedCellsToClipboard()
            PasteClipboardData()
        End Sub

        Private Sub PasteClipboardData()
            PasteHTMLFormat()
            PasteXLSFormat()
            PasteRTFFormat()
        End Sub

        Private Sub PasteRTFFormat()
            richEditControl.Document.RtfText = String.Empty
            richEditControl.Document.Text = String.Empty
            Dim rtfText As String
            Try
                rtfText = Clipboard.GetText(TextDataFormat.Rtf)
            Catch
                rtfText = Nothing
            End Try

            richEditControl.Document.AppendRtfText(rtfText)
        End Sub

        Private Sub PasteXLSFormat()
            spreadsheetControl.ActiveWorksheet.Clear(spreadsheetControl.ActiveWorksheet.GetDataRange())
            Dim stream As Stream
            Try
                stream = TryCast(Clipboard.GetDataObject().GetData("Biff8"), Stream)
            Catch
                stream = Nothing
            End Try

            spreadsheetControl.LoadDocument(stream, DevExpress.Spreadsheet.DocumentFormat.Xls)
            spreadsheetControl.ActiveWorksheet.DefaultColumnWidthInPixels = CInt(spreadsheetControl.ActualWidth) \ treeList.Columns.Count
        End Sub

        Private Sub PasteHTMLFormat()
            Dim html As String
            Try
                html = Clipboard.GetText(TextDataFormat.Html)
            Catch
                html = Nothing
            End Try

            If String.IsNullOrEmpty(html) Then
                webBrowser.NavigateToString(" ")
            Else
                html = html.Remove(0, html.Substring(0, html.IndexOf("<html", StringComparison.OrdinalIgnoreCase)).Length)
                webBrowser.NavigateToString(html)
            End If
        End Sub

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            webBrowser.Visibility = Visibility.Visible
        End Sub

        Protected Overrides Sub HidePopupContent()
            webBrowser.Visibility = Visibility.Collapsed
            MyBase.HidePopupContent()
        End Sub

        Protected Overrides Sub Clear()
            MyBase.Clear()
            webBrowser.Dispose()
        End Sub
    End Class
End Namespace
