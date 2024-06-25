using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel;
using DevExpress.VideoRent.Wpf.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using MessageBox = DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBox;
using MessageBoxButton = DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBoxButton;
using MessageBoxImage = DevExpress.VideoRent.ViewModel.ViewModelBase.MessageBoxImage;

namespace DevExpress.VideoRent.Wpf {
    public partial class CreateDatabaseWindow : ThemedWindow {
        DBConnectData dbConnectData;
        public CreateDatabaseWindow(DBConnectData dbConnectData) {
            InitializeComponent();
            this.dbConnectData = dbConnectData;
            this.Loaded += (s, e) => Start(this, EventArgs.Empty);
        }
        public event EventHandler Start;
        public void BeginWork() {
            this.Cursor = Cursors.Wait;
        }
        public void EndWork(bool complete) {
            if(!complete) {
                CreateDbWorker.Value = CreateDbWorker.Minimum;
                GenerateRentsHistoryWorker.Value = GenerateRentsHistoryWorker.Minimum;
            }
            this.Cursor = Cursors.Arrow;
        }
    }
    public class CreateInitialDbDialog : ICreateInitialDbDialog {
        CreateDatabaseWindow wnd;
        public void Show(DBConnectData dbConnectData) { wnd = new CreateDatabaseWindow(dbConnectData); }
        public void ShowDialog() { wnd.ShowDialog(); }
        public void Close() {
            wnd.Close();
            wnd = null;
        }
        public IBackgroundWorker CreateDbWorker { get { return wnd.CreateDbWorker; } }
        public IBackgroundWorker GenerateRentsHistoryWorker { get { return wnd.GenerateRentsHistoryWorker; } }
        public event EventHandler Start {
            add { wnd.Start += value; }
            remove { wnd.Start -= value; }
        }
        public void BeginWork() { wnd.BeginWork(); }
        public void EndWork(bool complete) { wnd.EndWork(complete); }
        public void ShowUnableToOpenMessage(bool createNew) {
            ThreadHelper.DoInThread(wnd.Dispatcher, () => MessageBox.Show(createNew ? ConstStrings.Get("UnableCreateDBMessage") : ConstStrings.Get("UnableOpenDBMessage"), ConstStrings.Get("Error"), MessageBoxButton.OK, MessageBoxImage.Error));
        }
    }
}
