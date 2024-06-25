using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.VideoRent.Wpf.Helpers;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.XtraReports;
using CursorType = DevExpress.VideoRent.ViewModel.ViewModelBase.CursorType;
using MessageBoxButton = DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBoxButton;
using MessageBoxImage = DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBoxImage;
using MessageBoxResult = DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBoxResult;

namespace DevExpress.VideoRent.Wpf {
    public class MessageBoxView : IMessageBoxView {
        public MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage image, MessageBoxResult defaultResult) {
            return TranslateResultBack(DXMessageBox.Show(null, message, title, TranslateButton(button), TranslateImage(image), TranslateResult(defaultResult)));
        }
        System.Windows.MessageBoxResult TranslateResult(MessageBoxResult result) {
            switch(result) {
            case MessageBoxResult.OK: return System.Windows.MessageBoxResult.OK;
            case MessageBoxResult.Cancel: return System.Windows.MessageBoxResult.Cancel;
            case MessageBoxResult.Yes: return System.Windows.MessageBoxResult.Yes;
            case MessageBoxResult.No: return System.Windows.MessageBoxResult.No;
            default: return System.Windows.MessageBoxResult.None;
            }
        }
        MessageBoxResult TranslateResultBack(System.Windows.MessageBoxResult result) {
            switch(result) {
            case System.Windows.MessageBoxResult.OK: return MessageBoxResult.OK;
            case System.Windows.MessageBoxResult.Cancel: return MessageBoxResult.Cancel;
            case System.Windows.MessageBoxResult.Yes: return MessageBoxResult.Yes;
            case System.Windows.MessageBoxResult.No: return MessageBoxResult.No;
            default: return MessageBoxResult.None;
            }
        }
        System.Windows.MessageBoxButton TranslateButton(MessageBoxButton button) {
            switch(button) {
            case MessageBoxButton.YesNo: return System.Windows.MessageBoxButton.YesNo;
            case MessageBoxButton.YesNoCancel: return System.Windows.MessageBoxButton.YesNoCancel;
            case MessageBoxButton.OKCancel: return System.Windows.MessageBoxButton.OKCancel;
            default: return System.Windows.MessageBoxButton.OK;
            }
        }
        System.Windows.MessageBoxImage TranslateImage(MessageBoxImage image) {
            switch(image) {
            case MessageBoxImage.Asterisk: return System.Windows.MessageBoxImage.Asterisk;
            case MessageBoxImage.Error: return System.Windows.MessageBoxImage.Error;
            case MessageBoxImage.Question: return System.Windows.MessageBoxImage.Question;
            case MessageBoxImage.Warning: return System.Windows.MessageBoxImage.Warning;
            default: return System.Windows.MessageBoxImage.None;
            }
        }
    }
    public class OpenFileDialogView : IOpenFileDialogView {
        public Stream OpenFile() {
#if !SL
            if(BrowserInteropHelper.IsBrowserHosted)
                return null;
#endif
            return LoadFileCore();
        }
        Stream LoadFileCore() {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if(dlg.ShowDialog() == true) {
#if !SL
                return dlg.OpenFile();
#else
                return dlg.File.OpenRead()
#endif
            }
            return null;
        }

        public System.Drawing.Image OpenImage() {
#if !SL
            if(BrowserInteropHelper.IsBrowserHosted)
                return null;
#endif
            return LoadImageCore();
        }
        static System.Drawing.Image LoadImageCore() {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = EditorLocalizer.GetString(EditorStringId.ImageEdit_OpenFileFilter);
            if(dlg.ShowDialog() == true) {
#if !SL
                Stream stream = dlg.OpenFile();
#else
                Stream stream = dlg.File.OpenRead()
#endif
                return System.Drawing.Image.FromStream(stream);
            }
            return null;
        }
    }
    public class ChartPredefinedValuesProviderView : IChartPredefinedValuesProviderView {
        static IList<ChartPalette> predefinedPalettes;
        static IList<ChartMarker2DKind> predefinedMarker2DKinds;

        public IList<ChartPalette> PredefinedPalettes {
            get {
                if(predefinedPalettes == null)
                    predefinedPalettes = GetPredefinedPalettesCore();
                return predefinedPalettes;
            }
        }
        public IList<ChartMarker2DKind> PredefinedMarker2DKinds {
            get {
                if(predefinedMarker2DKinds == null)
                    predefinedMarker2DKinds = GetPredefinedMarker2DKindsCore();
                return predefinedMarker2DKinds;
            }
        }
        static IList<ChartPalette> GetPredefinedPalettesCore() {
            List<ChartPalette> palettes = new List<ChartPalette>();
            foreach(PaletteKind item in Palette.GetPredefinedKinds()) {
                Palette palette = Activator.CreateInstance(item.Type) as Palette;
                if(palette == null) continue;
                palettes.Add(new ChartPalette(item.Name, palette));
            }
            return palettes;
        }
        static IList<ChartMarker2DKind> GetPredefinedMarker2DKindsCore() {
            List<ChartMarker2DKind> markers = new List<ChartMarker2DKind>();
            foreach(Marker2DKind item in Marker2DModel.GetPredefinedKinds()) {
                markers.Add(new ChartMarker2DKind(item.Name, null));
            }
            return markers;
        }
    }
    public class ReportDialogView : IReportDialogView {
        public void ShowPrintDialog(IReport report) {
            DevExpress.Xpf.Printing.PrintHelper.ShowPrintPreviewDialog(Application.Current.MainWindow, report);
        }
    }
    public class MouseView : IMouseView {
        public CursorType CursorType {
            get { return TranslateCursorTypeBack(System.Windows.Input.Mouse.OverrideCursor); }
            set { System.Windows.Input.Mouse.OverrideCursor = TranslateCursorType(value); }
        }
        public void WaitIdle() {
            MouseHelper.WaitIdle();
        }
        Cursor TranslateCursorType(CursorType cursorType) {
            switch(cursorType) {
                case CursorType.Wait: return Cursors.Wait;
                case CursorType.Hand: return Cursors.Hand;
                case CursorType.Cross: return Cursors.Cross;
                default: return null;
            }
        }
        CursorType TranslateCursorTypeBack(Cursor cursorType) {
            if(cursorType == Cursors.Wait) return CursorType.Wait;
            if(cursorType == Cursors.Hand) return CursorType.Hand;
            if(cursorType == Cursors.Cross) return CursorType.Cross;
            return CursorType.Arrow;
        }
    }
}
