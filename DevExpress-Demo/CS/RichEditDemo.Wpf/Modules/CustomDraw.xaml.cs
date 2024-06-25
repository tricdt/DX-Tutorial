using System;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Collections.Generic;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Layout;
using System.Windows.Input;
using System.Windows;

namespace RichEditDemo {
    public partial class CustomDraw : RichEditDemoModule {
        List<FixedRange> _searchResult = new List<FixedRange>();
        int _currentItemIndex = -1;

        public CustomDraw() {
            InitializeComponent();
        }

        int CurrentItemIndex {
            get { return _currentItemIndex; }
            set {
                if (value < 0 || value >= SearchResult.Count)
                    return;
                _currentItemIndex = value;
                FixedRange fixedRange = SearchResult[_currentItemIndex];
                DocumentRange range = RichEditControl.Document.CreateRange(fixedRange.Start, fixedRange.Length);
                RichEditControl.Document.ChangeActiveDocument(RichEditControl.Document);
                RichEditControl.Document.Selection = range;
                RichEditControl.ScrollToCaret();
                UpdateSearchOptionsUI();
                RichEditControl.Refresh();
            }
        }
        List<FixedRange> SearchResult { get { return _searchResult; } }
        bool IsSelectionInMainDocument { get { return !richEdit.IsSelectionInTextBox && !richEdit.IsSelectionInHeaderOrFooter && !richEdit.IsSelectionInComment; } }

        void RichEditControl_Loaded(object sender, RoutedEventArgs e) {
            richEdit.ReplaceService<IRichEditCommandFactoryService>(new CustomsRichEditCommandFactoryService(richEdit, richEdit.GetService<IRichEditCommandFactoryService>(), this.searchTextBox));
            FindText();
        }
        void RichEditControl_BeforePagePaint(object sender, BeforePagePaintEventArgs e) {
            if (e.CanvasOwnerType == CanvasOwnerType.Printer || SearchResult.Count == 0)
                return;
            FixedRange currentItem = SearchResult[CurrentItemIndex];
            List<FixedRange> visibleSearchResult = GetVisibleRanges(SearchResult, e.Page.MainContentRange);
            e.Painter = new CustomDrawPagePainter(richEdit, visibleSearchResult, currentItem);
        }
        List<FixedRange> GetVisibleRanges(List<FixedRange> ranges, FixedRange visibleRange) {
            List<FixedRange> result = new List<FixedRange>();
            int visibleRangeEnd = visibleRange.Start + visibleRange.Length;
            foreach (FixedRange range in ranges) {
                if (visibleRangeEnd < range.Start)
                    break;
                if (visibleRange.Contains(range))
                    result.Add(range);
            }
            return result;
        }
        void RichEditControl_ContentChanged(object sender, EventArgs e) {
            SearchResult.Clear();
            RichEditControl.Refresh();
        }
        void CancelSearchButtonInfo_Click(object sender, RoutedEventArgs e) {
            this.searchTextBox.Text = string.Empty;
        }
        void ButtonDownInfo_Click(object sender, RoutedEventArgs e) {
            CurrentItemIndex++;
        }
        void ButtonUpInfo_Click(object sender, RoutedEventArgs e) {
            CurrentItemIndex--;
        }
        void searchTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter)
                FindText();
            else if (e.Key == Key.Escape) {
                this.searchTextBox.Text = string.Empty;
                RichEditControl.Focus();
            }
        }
        void UpdateSearchOptionsUI() {
            this.navigationButtonEdit.Visibility = !string.IsNullOrEmpty(this.searchTextBox.Text) && SearchResult.Count > 0 ? Visibility.Visible : Visibility.Hidden;
            foreach (ButtonInfo button in this.searchTextBox.Buttons)
                button.Visibility = !string.IsNullOrEmpty(this.searchTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
            if (string.IsNullOrEmpty(this.searchTextBox.Text))
                this.labelResultCount.Text = String.Empty;
            else if (SearchResult.Count == 0)
                this.labelResultCount.Text = "No matches";
            else
                this.labelResultCount.Text = String.Format("{0} of {1} matches", CurrentItemIndex + 1, SearchResult.Count);
        }
        void FindText() {
            SearchResult.Clear();
            string textToSearch = this.searchTextBox.Text;
            if (IsSelectionInMainDocument && !String.IsNullOrEmpty(textToSearch)) {
                SearchOptions options = GetSearchOptions();
                DocumentRange[] ranges = RichEditControl.Document.FindAll(textToSearch, options);
                for (int i = 0; i < ranges.Length; i++)
                    SearchResult.Add(new FixedRange(ranges[i].Start.ToInt(), ranges[i].Length));
            }

            CurrentItemIndex = 0;
            UpdateSearchOptionsUI();
            RichEditControl.Refresh();
        }
        SearchOptions GetSearchOptions() {
            SearchOptions result = SearchOptions.None;
            if (this.edtMatchCase.IsChecked.Value)
                result |= SearchOptions.CaseSensitive;
            if (this.edtFindWholeWordsOnly.IsChecked.Value)
                result |= SearchOptions.WholeWord;
            return result;
        }
        void SearchOptions_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            FindText();
        }
    }
}
