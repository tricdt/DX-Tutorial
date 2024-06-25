Imports DevExpress.Spreadsheet
Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows

Namespace GridDemo

    Public Partial Class ClipboardFormats
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler richEditControl.ActiveViewChanged, AddressOf OnActiveViewChanged
            tableView.SelectCells(5, grid.Columns.First(), 15, grid.Columns.Last())
            tableView.CopySelectedCellsToClipboard()
            PasteClipboardData()
        End Sub

        Private Sub OnActiveViewChanged(ByVal sender As Object, ByVal e As EventArgs)
            richEditControl.ActiveView.AdjustColorsToSkins = True
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            tableView.CopySelectedCellsToClipboard()
            PasteClipboardData()
        End Sub

        Friend Sub PasteClipboardData()
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

            spreadsheetControl.LoadDocument(stream, DocumentFormat.Xls)
            spreadsheetControl.ActiveWorksheet.DefaultColumnWidthInPixels = CInt(spreadsheetControl.ActualWidth) \ grid.Columns.Count
        End Sub

        Private Sub PasteHTMLFormat()
            Dim html As String
            Try
                html = Clipboard.GetText(TextDataFormat.Html)
            Catch
                html = Nothing
            End Try

            Try
                If String.IsNullOrEmpty(html) Then
                    webBrowser.NavigateToString(" ")
                Else
                    html = html.Remove(0, html.Substring(0, html.IndexOf("<html", StringComparison.OrdinalIgnoreCase)).Length)
                    webBrowser.NavigateToString(html)
                End If
            Catch
            End Try
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
