using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public partial class ButtonEditModule : EditorsDemoModule {
        Queue<string> logQueue = new Queue<string>();
        const int maxLogEntriesCount = 42;
        #region Sample 2
        List<string> controlList = new List<string>(new string[] { 
            "CheckEdit", 
            "ProgressBarEdit",
            "TrackBarEdit",
            "ListBoxEdit",
            "TextEdit", 
            "ButtonEdit", 
            "ComboBoxEdit", 
            "SpinEdit", 
            "MemoEdit",
            "DateEdit"
        });
        int selectedIndex = 0;
        int SelectedIndex {
            get { return selectedIndex; }
            set { UpdateSelectedIndex(value); }
        }
        void UpdateSelectedIndex(int value) {
            selectedIndex = Math.Min(Math.Max(0, value), controlList.Count - 1);
            Update();
        }
        void Update() {
            UpdateValue();
            UpdateButtons();
        }
        void UpdateValue() {
            editor2.EditValue = controlList[SelectedIndex];
        }
        void UpdateButtons() {
            editor2.Buttons[0].IsEnabled = SelectedIndex > 0;
            editor2.Buttons[1].IsEnabled = SelectedIndex < controlList.Count - 1;
        }
        void LeftButtonClick(object sender, RoutedEventArgs e) {
            SelectedIndex--;
        }
        void RightButtonClick(object sender, RoutedEventArgs e) {
            SelectedIndex++;
        }
        #endregion
        public ButtonEditModule() {
            InitializeComponent();
            DataContext = CreateButtonsSource();
            SubscribeButtons();
            Update();
        }

        List<ButtonsViewModel> CreateButtonsSource() {
            return new List<ButtonsViewModel>(){
                new ButtonsViewModel(){ToolTip = "Button 3", GlyphKind = GlyphKind.Redo, Index = 3},
                new ButtonsViewModel(){ToolTip = "Button 1", GlyphKind = GlyphKind.Apply},
                new ButtonsViewModel(){ToolTip = "Button 4", GlyphKind = GlyphKind.Cancel, Index = 4},
                new ButtonsViewModel(){ToolTip = "Button 2", GlyphKind = GlyphKind.Undo},
                new ButtonsViewModel(){ToolTip = "Button 5", GlyphKind = GlyphKind.Left, IsLeft=true},
            };
        }
        void SubscribeButtons() {
            SubscribeButtonEdit(editor1);
            SubscribeButtonEdit(editor2);
            SubscribeButtonEdit(editor3);
        }
        void SubscribeButtonEdit(ButtonEdit edit) {
            foreach(ButtonInfoBase info in edit.Buttons)
                ((CommonButtonInfo)info).Click += OnButtonClick;
        }
        void OnButtonClick(object sender, RoutedEventArgs e) {
            var buttonInfo = sender as CommonButtonInfo;
            string senderName = buttonInfo != null ? buttonInfo.Name : ((CommonButtonInfo)((FrameworkElement)sender).DataContext).Name;
            AddToLog("ButtonClick: " + senderName + "\n");
        }
        void AddToLog(string meassage) {
            EnqueueLogMessage(meassage);
            RepopulateLog();
            ScrollLogToEnd();
        }
        void EnqueueLogMessage(string text) {
            logQueue.Enqueue(text);
            if(logQueue.Count > maxLogEntriesCount)
                logQueue.Dequeue();
        }
        void RepopulateLog() {
            log.Clear();
            StringBuilder builder = new StringBuilder();
            foreach(string logText in logQueue)
                builder.Append(logText);
            log.Text = builder.ToString();
        }
        bool IsScrollViewer(FrameworkElement element) {
            return element is ScrollViewer;
        }
        void ScrollLogToEnd() {
            ScrollViewer sv = (ScrollViewer)LayoutHelper.FindElement(log, IsScrollViewer);
            if (sv == null)
                return;
            sv.ScrollToVerticalOffset(sv.ScrollableHeight);
        }
        void ClearButtonClick(object sender, RoutedEventArgs e) {
            logQueue.Clear();
            log.Clear();
        }
    }
    [POCOViewModel]
    public class ButtonsViewModel : ViewModelBase {
        public GlyphKind GlyphKind { get; set; }
        public string ToolTip { get; set; }
        public int Index { get; set; }
        public bool IsLeft { get; set; }
    }
}
