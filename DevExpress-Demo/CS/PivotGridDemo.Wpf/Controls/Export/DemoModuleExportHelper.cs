using DevExpress.Export;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PdfViewer;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IDialogService = DevExpress.Mvvm.IDialogService;
using System.Windows.Threading;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.PivotGrid.Printing;

namespace PivotGridDemo {

    public static class DemoModuleWYSIWYGExportHelper {
        public static void DoExport(PivotGridControl pivot, ExportFormat format) {
            switch(format) {
                case ExportFormat.Pdf:
                case ExportFormat.Rtf:
                case ExportFormat.Docx:
                    pivot.PrintLayoutMode = PrintLayoutMode.MultiplePagesLayout;
                    break;
                default:
                    pivot.PrintLayoutMode = PrintLayoutMode.SinglePageLayout;
                    break;
            }
            switch(format) {
                case ExportFormat.Xls:
                    Export(pivot, link => new Action<Stream, XlsExportOptions>(link.ExportToXls));
                    break;
                case ExportFormat.Xlsx:
                    Export(pivot, link => new Action<Stream, XlsxExportOptions>(link.ExportToXlsx));
                    break;
                case ExportFormat.Pdf:
                    Export(pivot, link => new Action<Stream, PdfExportOptions>(link.ExportToPdf));
                    break;
                case ExportFormat.Htm:
                    Export(pivot, link => new Action<Stream, HtmlExportOptions>(link.ExportToHtml));
                    break;
                case ExportFormat.Mht:
                    Export(pivot, link => new Action<Stream, MhtExportOptions>(link.ExportToMht));
                    break;
                case ExportFormat.Rtf:
                    Export(pivot, link => new Action<Stream, RtfExportOptions>(link.ExportToRtf));
                    break;
                case ExportFormat.Txt:
                    Export(pivot, link => new Action<Stream, TextExportOptions>(link.ExportToText));
                    break;
                case ExportFormat.Image:
                    Export(pivot, link => new Action<Stream, ImageExportOptions>(link.ExportToImage));
                    break;
                case ExportFormat.Xps:
                    Export(pivot, link => new Action<Stream, XpsExportOptions>(link.ExportToXps));
                    break;
                case ExportFormat.Docx:
                    Export(pivot, link => new Action<Stream, DocxExportOptions>(link.ExportToDocx));
                    break;
            }
        }
        static void OnAfterBuildPages(object sender, EventArgs e) {
            DXSplashScreen.Close();
        }
        static void UnsubscribeProgressEvents(PrintableControlLink link, EventHandler onExportProgress) {
            link.PrintingSystem.ProgressReflector.PositionChanged -= onExportProgress;
            link.PrintingSystem.AfterBuildPages -= OnAfterBuildPages;
        }
        static void SubscribeProgressEvents(PrintableControlLink link, EventHandler onExportProgress) {
            link.PrintingSystem.ProgressReflector.PositionChanged += onExportProgress;
            link.PrintingSystem.AfterBuildPages += OnAfterBuildPages;
        }
        static void Export<T>(PivotGridControl pivot, Func<PrintableControlLink, Action<Stream, T>> getExportToStreamMethod) where T : ExportOptionsBase, new() {
            var link = new PrintableControlLink(pivot);
            EventHandler onExportProgress = (o, e) => ExportHelper.ExportProgress(new ProgressChangedEventArgs(link.PrintingSystem.ProgressReflector.Position, null));
            using(MemoryStream stream = new MemoryStream()) {
                ExportHelper.ExportCore(getExportToStreamMethod(link), stream,
                options => SubscribeProgressEvents(link, onExportProgress),
                options => UnsubscribeProgressEvents(link, onExportProgress));
            }
        }
    }
    public static class DemoModuleExportHelper {
        public static void ExportToXlsx(PivotGridControl pivot) {
            Export(new Action<Stream, XlsxExportOptionsEx>(pivot.ExportToXlsx));
        }
        public static void ExportToXls(PivotGridControl pivot) {
            Export(new Action<Stream, XlsExportOptionsEx>(pivot.ExportToXls));
        }
        public static void ExportToCsv(PivotGridControl pivot) {
            Export(new Action<Stream, CsvExportOptionsEx>(pivot.ExportToCsv));
        }
        static void Export<T>(Action<Stream, T> exportToStreamMethod) where T : ExportOptionsBase, new() {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action<Action<Stream, T>>(ExportCore), DispatcherPriority.ContextIdle, exportToStreamMethod);
        }
        static void ExportCore<T>(Action<Stream, T> exportToStreamMethod) where T : ExportOptionsBase, new() {
            using(MemoryStream stream = new MemoryStream()) {
                ExportHelper.ExportCore(exportToStreamMethod, stream, SubscribeProgressEvents, UnsubscribeProgressEvents);
            }
        }
        static void UnsubscribeProgressEvents<T>(T options) where T : ExportOptionsBase {
            ((IDataAwareExportOptions)options).ExportProgress -= ExportHelper.ExportProgress;
        }
        static void SubscribeProgressEvents<T>(T options) where T : ExportOptionsBase {
            ((IDataAwareExportOptions)options).ExportProgress += ExportHelper.ExportProgress;
        }
    }
    static class ExportHelper {
        const string OpenInInternalViewer = "Open with DevExpress Control";
        const string OpenInExternalViewer = "Open with Default App";
        const string OpenExportedFile = "Open Exported File";
        const string OpenExportedFileDescription = "You can view the exported file in your system default\napplication or in a DevExpress WPF {0} control.";

        static Dictionary<ViewerType, string> InternalViewerDisplayTexts = new Dictionary<ViewerType, string>() {
            { ViewerType.Spreadsheet, "Spreadsheet" },
            { ViewerType.RichEdit, "RichEdit" },
            { ViewerType.PDFViewer, "PDF Viewer" },
        };

        public static void ExportCore<T>(Action<Stream, T> exportToStream, Stream stream, Action<T> subscribeProgress, Action<T> unsubscribeProgress)
            where T : ExportOptionsBase, new() {
            if(stream == null)
                return;
            try {
                DXSplashScreen.Show<ExportWaitIndicator>();
                var options = new T();
                subscribeProgress(options);
                try {
                    exportToStream(stream, options);
                } finally {
                    unsubscribeProgress(options);
                    if(DXSplashScreen.IsActive)
                        DXSplashScreen.Close();
                }

                stream.Seek(0, SeekOrigin.Begin);

                ViewerType viewerType = GetInternalViewerType(options, IsLargeFile(stream));

                if(viewerType == ViewerType.External) {
                    if(ShouldOpenExportedFile())
                        OpenExternalViewer(stream, options);
                    return;
                }

                switch(GetExportType(viewerType)) {
                    case ViewerType.External:
                        OpenExternalViewer(stream, options);
                        return;
                    case ViewerType.None:
                        return;
                    default:
                        OpenInternalViewer(stream, options, viewerType);
                        return;
                }
            } catch(Exception e) {
                DXMessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static ViewerType GetExportType(ViewerType viewerType) {
            List<UICommand> dialogCommands = new List<UICommand>();
            ViewerType result = ViewerType.None;
            dialogCommands.Add(new UICommand(0, OpenInInternalViewer, DelegateCommandFactory.Create(() => result = viewerType), true, false));
            dialogCommands.Add(new UICommand(1, OpenInExternalViewer, DelegateCommandFactory.Create(() => result = ViewerType.External), true, false));
            DXDialogWindow d = new DXDialogWindow() {
                ResizeMode = ResizeMode.NoResize,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.SingleBorderWindow,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
                Title = OpenExportedFile
            };
            d.CommandsSource = dialogCommands;
            d.Content = new TextBlock() { Text = String.Format(OpenExportedFileDescription, InternalViewerDisplayTexts[viewerType]), Margin = new Thickness(24, 15, 24, 16), HorizontalAlignment = HorizontalAlignment.Center };
            d.ShowDialogWindow();
            return result;
        }

        static bool IsLargeFile(Stream stream) {
            return stream.Length > (long)300000;
        }

        static void OpenExternalViewer<T>(Stream stream, T options) where T : ExportOptionsBase {
            string fullFilePath = SaveToFile(stream, options);
            ProcessLaunchHelper.StartConfirmed(fullFilePath);
        }

        static ViewerType GetInternalViewerType(ExportOptionsBase options, bool isLargeFile) {
            switch(options.GetFormat()) {
                case ExportFormat.Csv:
                case ExportFormat.Xls:
                case ExportFormat.Xlsx:
                    return isLargeFile ? ViewerType.External : ViewerType.Spreadsheet;
                case ExportFormat.Rtf:
                    return isLargeFile ? ViewerType.External : ViewerType.RichEdit;
                case ExportFormat.Pdf:
                    return ViewerType.PDFViewer;
                case ExportFormat.Htm:
                case ExportFormat.Mht:
                default:
                    return ViewerType.External;
            }
        }

        static void OpenInternalViewer<T>(Stream stream, T options, ViewerType vieweType) where T : ExportOptionsBase {
            switch(vieweType) {
                case ViewerType.Spreadsheet:
                    OpenInternalViewerWindow(SpreadSheetLoadDocument, () => new SpreadsheetControl(), stream, options);
                    return;
                case ViewerType.PDFViewer:
                    OpenInternalViewerWindow(PdfViewerLoadDocument, () => new PdfViewerControl(), stream, options);
                    return;
                case ViewerType.RichEdit:
                    OpenInternalViewerWindow(RichEditLoadDocument, () => new RichEditControl(), stream, options);
                    return;
            }
        }
        static void OpenInternalViewerWindow<T1, T2>(Action<T2, Stream, T1> loadDocumentAction, Func<T2> createViewer, Stream stream, T1 options) where T1 : ExportOptionsBase where T2 : FrameworkElement {
            DXSplashScreen.Show<OpenViewerWaitIndicator>();
            var viewerWindow = new ThemedWindow() { Title = "Document", Owner = Application.Current.MainWindow, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            T2 viewerControl = createViewer();
            viewerWindow.Content = viewerControl;
            loadDocumentAction(viewerControl, stream, options);
            viewerWindow.Loaded += ViewerWindow_Loaded1;
            viewerWindow.ShowDialog();
        }

        static DevExpress.Spreadsheet.DocumentFormat GetSpreadsheetDocumentFormat(ExportOptionsBase options) {
            switch(options.GetFormat()) {
                case ExportFormat.Csv: return DevExpress.Spreadsheet.DocumentFormat.Csv;
                case ExportFormat.Xls: return DevExpress.Spreadsheet.DocumentFormat.Xls;
                case ExportFormat.Xlsx: return DevExpress.Spreadsheet.DocumentFormat.Xlsx;
                default: return DevExpress.Spreadsheet.DocumentFormat.Undefined;
            }
        }
        static DevExpress.XtraRichEdit.DocumentFormat GetRichEditDocumentFormat(ExportOptionsBase options) {
            switch(options.GetFormat()) {
                case ExportFormat.Rtf: return DevExpress.XtraRichEdit.DocumentFormat.Rtf;
                case ExportFormat.Htm: return DevExpress.XtraRichEdit.DocumentFormat.Html;
                case ExportFormat.Mht: return DevExpress.XtraRichEdit.DocumentFormat.Mht;
                default: return DevExpress.XtraRichEdit.DocumentFormat.Undefined;
            }
        }

        static void ViewerWindow_Loaded1(object sender, RoutedEventArgs e) {
            FrameworkElement element = (FrameworkElement)sender;
            element.Loaded -= ViewerWindow_Loaded1;
            if(DXSplashScreen.IsActive)
                DXSplashScreen.Close();
        }

        static void RichEditLoadDocument<T>(RichEditControl richEditControl, Stream stream, T options) where T : ExportOptionsBase {
            richEditControl.CommandBarStyle = DevExpress.Xpf.RichEdit.CommandBarStyle.Ribbon;
            richEditControl.LoadDocument(stream, GetRichEditDocumentFormat(options));
        }
        static void SpreadSheetLoadDocument<T>(SpreadsheetControl spreadSheetControl, Stream stream, T options) where T : ExportOptionsBase {
            spreadSheetControl.CommandBarStyle = DevExpress.Xpf.Spreadsheet.CommandBarStyle.Ribbon;
            spreadSheetControl.LoadDocument(stream, GetSpreadsheetDocumentFormat(options));
        }
        static void PdfViewerLoadDocument<T>(PdfViewerControl pdfViewerControl, Stream stream, T options) where T : ExportOptionsBase {
            pdfViewerControl.DocumentSource = stream;
        }

        static string SaveToFile<T>(Stream stream, T options) where T : ExportOptionsBase {
            string tempFileName = Path.ChangeExtension(Path.GetTempFileName(), options.GetFileExtension());
            using(FileStream fileStream = new FileStream(tempFileName, FileMode.Create)) {
                stream.CopyTo(fileStream);
            }
            return tempFileName;
        }

        public static void ExportProgress(ProgressChangedEventArgs ea) {
            if(!DXSplashScreen.IsActive)
                return;
            DXSplashScreen.Progress(ea.ProgressPercentage);
        }
        public static string GetFileName(ExportOptionsBase options) {
            return GetFileName(ExportOptionsControllerBase.GetControllerByOptions(options));
        }
        static string GetFileName(ExportOptionsControllerBase controller) {
            SaveFileDialog dlg = CreateSaveFileDialog(controller);
            if(dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.FileName))
                return PathExtensionHelper.GetValidExtension(dlg.FileName, controller.FileExtensions[0], controller.FileExtensions);
            else
                return string.Empty;
        }
        static SaveFileDialog CreateSaveFileDialog(ExportOptionsControllerBase controller) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title);
            dlg.ValidateNames = true;
            dlg.FileName = PrintPreviewOptions.DefaultFileNameDefault;
            dlg.Filter = controller.Filter;
            return dlg;
        }
        static bool ShouldOpenExportedFile() {
            return DXMessageBox.Show(
                PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestion),
                PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestionCaption),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
    public class PrintingIconImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string rawIconName = value as string;
            if(rawIconName == null)
                return null;
            string iconName = Regex.Replace(rawIconName, "[^a-zA-Z]", string.Empty);
            string iconPath = "Images/BarItems/" + iconName + "_32x32.png";
            return new PrintingResourceImageExtension() { ResourceName = iconPath }.ProvideValue(null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class InternalViewerViewModel {
        public InternalViewerViewModel(Stream stream, ExportOptionsBase options) {
            Stream = stream;
            Options = options;
        }

        public Stream Stream { get; private set; }
        public ExportOptionsBase Options { get; private set; }

        public void StopSplashScreen() {
            if(DXSplashScreen.IsActive)
                DXSplashScreen.Close();
        }
    }

    public enum ViewerType { None, External, Spreadsheet, RichEdit, PDFViewer }
}
