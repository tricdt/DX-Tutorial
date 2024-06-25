using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Mvvm;
using DevExpress.Office.Services;
using DevExpress.Xpf.Spreadsheet.Services;
using DevExpress.Xpf.Core;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Xpf.RichEdit;

namespace RichEditDemo {
    public partial class FirstLook : RichEditDemoModule {
        static readonly DependencyProperty PageCountProperty = DependencyProperty.Register("PageCount", typeof(int), typeof(FirstLook), new PropertyMetadata(1));
        static readonly DependencyProperty ActivePageNumberProperty = DependencyProperty.Register("ActivePageNumber", typeof(int), typeof(FirstLook), new PropertyMetadata(1));
        static readonly DependencyProperty WordCountProperty = DependencyProperty.Register("WordCount", typeof(int), typeof(FirstLook), new PropertyMetadata(0));
        static readonly DependencyProperty ActiveViewZoomProperty = DependencyProperty.Register("ActiveViewZoom", typeof(float), typeof(FirstLook), new PropertyMetadata(100f, OnActiveViewZoomChanged));
        static void OnActiveViewZoomChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            FirstLook firstLook = (FirstLook)obj;
            firstLook.RichEditControl.ActiveView.ZoomFactor = (float)e.NewValue / 100;
        }

        DispatcherTimer documentStatisticsTimer;
        bool includeTextBoxes;

        public FirstLook() {
            OfficeCharts.Instance.ActivateWpfCharts();

            InitializeComponent();

            this.richEdit.DocumentLayout.DocumentFormatted += DocumentLayout_DocumentFormatted;
            this.richEdit.HyphenationDictionaries.Add(new OpenOfficeHyphenationDictionary(DemoUtils.GetRelativePath("hyph_en_US.dic"), new System.Globalization.CultureInfo("en-US")));
            this.documentStatisticsTimer = new DispatcherTimer();
            this.documentStatisticsTimer.Tick += DocumentStatisticsTimer_Tick;
            this.documentStatisticsTimer.Start();
        }

        public int PageCount {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }
        public int ActivePageNumber {
            get { return (int)GetValue(ActivePageNumberProperty); }
            set { SetValue(ActivePageNumberProperty, value); }
        }
        public int WordCount {
            get { return (int)GetValue(WordCountProperty); }
            set { SetValue(WordCountProperty, value); }
        }
        public float ActiveViewZoom {
            get { return (float)GetValue(ActiveViewZoomProperty); }
            set { SetValue(ActiveViewZoomProperty, value); }
        }

        void DocumentLayout_DocumentFormatted(object sender, EventArgs e) {
            this.richEdit.Dispatcher.BeginInvoke(new Action(() => {
                PageCount = this.richEdit.DocumentLayout.GetPageCount();
            }));
        }
        void RichEditControl_InvalidFormatException(object sender, DevExpress.XtraRichEdit.RichEditInvalidFormatExceptionEventArgs e) {
            string currentFileName = ((RichEditControl)sender).Options.DocumentSaveOptions.CurrentFileName;
            Dispatcher.BeginInvoke(new Action(() => {
                ThemedMessageBox.Show(
                    "Error",
                    string.Format("Cannot open the file '{0}' because the file format or file extension is not valid.\n" +
                    "Verify that file has not been corrupted and that the file extension matches the format of the file.", currentFileName ?? string.Empty),
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }));
        }
        void RichEditControl_DocumentClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (RichEditControl.Modified) {
                string currentFileName = RichEditControl.Options.DocumentSaveOptions.CurrentFileName;
                string message = !string.IsNullOrEmpty(currentFileName) ?
                    string.Format("Do you want to save the changes you made for '{0}'?", currentFileName) : "Do you want to save the changes?";
                MessageBoxResult result = ThemedMessageBox.Show("Warning", message, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                    RichEditControl.SaveDocument();
                e.Cancel = result == MessageBoxResult.Cancel;
            }
        }
        void RichEditControl_VisiblePagesChanged(object sender, EventArgs e) {
            ActivePageNumber = RichEditControl.ActiveView.GetVisiblePageLayoutInfos()[0].PageIndex + 1;
        }
        void RichEditControl_SelectionChanged(object sender, EventArgs e) {
            RangedLayoutElement element = richEdit.DocumentLayout.GetElement<RangedLayoutElement>(RichEditControl.Document.CaretPosition);
            if (element != null)
                ActivePageNumber = RichEditControl.DocumentLayout.GetPageIndex(element) + 1;
        }
        void RichEditControl_ContentChanged(object sender, EventArgs e) {
            this.documentStatisticsTimer.Start();
        }
        void DocumentStatisticsTimer_Tick(object sender, EventArgs e) {
            this.documentStatisticsTimer.Stop();
            Dispatcher.BeginInvoke(new Action(() => {
                if (RichEditControl.Document != null) {
                    StaticsticsVisitor visitor = CalculateDocumentStatistics(this.includeTextBoxes);
                    WordCount = visitor.WordCount;
                }
            }));
        }
        void DocumentStatisticsBarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            var viewModel = StatisticsViewModel.Create(this.includeTextBoxes, includeTextBoxes => {
                StaticsticsVisitor visitor = CalculateDocumentStatistics(includeTextBoxes);
                return new StatisticsData(visitor.NoSpacesCharacterCount, visitor.WithSpacesCharacterCount, visitor.WordCount, visitor.ParagraphCount);
            });
            IDialogService dialog = dialogService;
            dialog.ShowDialog(MessageButton.OK, "Document Statistics", viewModel);
            if (this.includeTextBoxes != viewModel.IncludeTextboxes) {
                this.includeTextBoxes = viewModel.IncludeTextboxes;
                StaticsticsVisitor visitor = CalculateDocumentStatistics(this.includeTextBoxes);
                WordCount = visitor.WordCount;
            }
        }
        StaticsticsVisitor CalculateDocumentStatistics(bool includeTextBoxes) {
            DocumentIterator iterator = new DocumentIterator(RichEditControl.Document, true);
            StaticsticsVisitor visitor = new StaticsticsVisitor(includeTextBoxes);
            while (iterator.MoveNext())
                iterator.Current.Accept(visitor);
            return visitor;
        }
        void RichEditControl_ZoomChanged(object sender, EventArgs e) {
            ActiveViewZoom = RichEditControl.ActiveView.ZoomFactor * 100;
        }
    }
}
