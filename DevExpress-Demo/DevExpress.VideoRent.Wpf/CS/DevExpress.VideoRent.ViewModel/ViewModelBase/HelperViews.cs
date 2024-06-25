using System;
using System.Collections.Generic;
using System.IO;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.XtraReports;
#if SL
using DevExpress.Xpf.Drawing;
#else
using System.Drawing;
#endif


namespace DevExpress.VideoRent.ViewModel.ViewModelBase {
    public enum MessageBoxResult { None, OK, Cancel, Yes, No }
    public enum MessageBoxButton { OK, OKCancel, YesNoCancel, YesNo }
    public enum MessageBoxImage { None, Error, Question, Warning, Asterisk }
    public interface IMessageBoxView {
        MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult);
    }
    public static class MessageBox {
        public static IMessageBoxView View { get; set; }
        public static MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage image) {
            return Show(message, title, button, image, MessageBoxResult.None);
        }
        public static MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult) {
            if(View == null) return defaultResult;
            return View.Show(message, title, button, image, defaultResult);
        }
    }
    public class ExceptionProcesser : IExceptionProcesser {
        public static ExceptionProcesser Current { get; private set; }
        static ExceptionProcesser() {
            Current = new ExceptionProcesser();
        }
        public void Process(Exception e) {
            MessageBox.Show(e.Message, ConstStrings.Get("Warning"), MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    public interface IOpenFileDialogView {
        Stream OpenFile();
        Image OpenImage();
    }
    public static class OpenFileDialog {
        public static IOpenFileDialogView View { get; set; }
        public static Stream OpenFile() {
            return View.OpenFile();
        }
        public static Image OpenImage() {
            return View.OpenImage();
        }
    }
    public class ChartPalette {
        public ChartPalette(string name, object tag) {
            Name = name;
            Tag = tag;
        }
        public string Name { get; private set; }
        public object Tag { get; private set; }
    }
    public class ChartMarker2DKind {
        public ChartMarker2DKind(string name, object tag) {
            Name = name;
            Tag = tag;
        }
        public string Name { get; private set; }
        public object Tag { get; private set; }
    }
    public interface IChartPredefinedValuesProviderView {
        IList<ChartPalette> PredefinedPalettes { get; }
        IList<ChartMarker2DKind> PredefinedMarker2DKinds { get; }
    }
    public static class ChartPredefinedValuesProvider {
        public static IChartPredefinedValuesProviderView View { get; set; }
        public static IList<ChartPalette> PredefinedPalettes { get { return View.PredefinedPalettes; } }
        public static IList<ChartMarker2DKind> PredefinedMarker2DKinds { get { return View.PredefinedMarker2DKinds; } }
    }
#if !SL //TODO  
    public interface IReportDialogView {
        void ShowPrintDialog(IReport report);
    }
    public static class ReportDialog {
        public static IReportDialogView View { get; set; }
        public static void ShowPrintDialog(IReport report) { View.ShowPrintDialog(report); }
    }
#endif
    public enum CursorType { Arrow, Cross, Hand, Wait }
    public interface IMouseView {
        CursorType CursorType { get; set; }
        void WaitIdle();
    }
    public static class Mouse {
        public static IMouseView View { get; set; }
        public static CursorType CursorType {
            get { return View.CursorType; }
            set { View.CursorType = value; }
        }
        public static void WaitIdle() { View.WaitIdle(); }
    }
}
