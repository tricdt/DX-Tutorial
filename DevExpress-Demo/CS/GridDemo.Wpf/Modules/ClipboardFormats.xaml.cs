using DevExpress.Spreadsheet;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace GridDemo {
    public partial class ClipboardFormats : GridDemoModule {
        public ClipboardFormats() {
            InitializeComponent();
            richEditControl.ActiveViewChanged += OnActiveViewChanged;
            tableView.SelectCells(5, grid.Columns.First(), 15, grid.Columns.Last());
            tableView.CopySelectedCellsToClipboard();
            PasteClipboardData();
        }
        private void OnActiveViewChanged(object sender, EventArgs e) {
            richEditControl.ActiveView.AdjustColorsToSkins = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            tableView.CopySelectedCellsToClipboard();
            PasteClipboardData();
        }
        internal void PasteClipboardData() {
            PasteHTMLFormat();
            PasteXLSFormat();
            PasteRTFFormat();
        }
        void PasteRTFFormat() {
            richEditControl.Document.RtfText = string.Empty;
            richEditControl.Document.Text = string.Empty;
            string rtfText;
            try {
                rtfText = Clipboard.GetText(TextDataFormat.Rtf);
            } catch {
                rtfText = null;
            }
            richEditControl.Document.AppendRtfText(rtfText);    
        }
        void PasteXLSFormat() {
            spreadsheetControl.ActiveWorksheet.Clear(spreadsheetControl.ActiveWorksheet.GetDataRange());
            Stream stream;
            try {
                stream = Clipboard.GetDataObject().GetData("Biff8") as Stream;
            } catch {
                stream = null;
            }
            spreadsheetControl.LoadDocument(stream, DevExpress.Spreadsheet.DocumentFormat.Xls);      
            spreadsheetControl.ActiveWorksheet.DefaultColumnWidthInPixels = (int)spreadsheetControl.ActualWidth / grid.Columns.Count;
        }
        void PasteHTMLFormat() {
            string html;
            try {
                html = Clipboard.GetText(TextDataFormat.Html);
            } catch {
                html = null;
            }
            try {
                if(string.IsNullOrEmpty(html))
                    webBrowser.NavigateToString(" ");
                else {
                    html = html.Remove(0, html.Substring(0, html.IndexOf("<html", StringComparison.OrdinalIgnoreCase)).Length);
                    webBrowser.NavigateToString(html);
                }
            } catch { }
        }
        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            webBrowser.Visibility = Visibility.Visible;
        }
        protected override void HidePopupContent() {
            webBrowser.Visibility = Visibility.Collapsed;
            base.HidePopupContent();
        }
        protected override void Clear() {
            base.Clear();
            webBrowser.Dispose();
        }

    }  
}
