using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpf.Editors;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class VisualBackgroundWorker : ProgressBarEdit, IBackgroundWorker {
        BackgroundWorker worker = new BackgroundWorker();
        int reportedProgress;
        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event ProgressChangedEventHandler ProgressChanged;
        public VisualBackgroundWorker() {
            InitializeBackgroundWorker();
            reportedProgress = (int)this.Value;
        }
        public bool CancellationPending { get { return worker.CancellationPending; } }
        public bool IsBusy { get { return worker.IsBusy; } }
        [DefaultValue(true)]
        public bool WorkerReportsProgress {
            get { return worker.WorkerReportsProgress; }
            set { worker.WorkerReportsProgress = value; }
        }
        [DefaultValue(false)]
        public bool WorkerSupportsCancellation {
            get { return worker.WorkerSupportsCancellation; }
            set { worker.WorkerSupportsCancellation = value; }
        }
        public void CancelAsync() { worker.CancelAsync(); }
        public void ReportProgress(int percentProgress) {
            if(percentProgress <= reportedProgress) return;
            reportedProgress = percentProgress;
            worker.ReportProgress(reportedProgress);
        }
        public void ReportProgress(int percentProgress, object userState) {
            if(percentProgress <= reportedProgress) return;
            reportedProgress = percentProgress;
            worker.ReportProgress(reportedProgress, userState);
        }
        public void RunWorkerAsync() {
            this.Value = reportedProgress = (int)this.Minimum;
            worker.RunWorkerAsync();
        }
        public void RunWorkerAsync(object argument) {
            this.Value = reportedProgress = (int)this.Minimum;
            worker.RunWorkerAsync(argument);
        }
        public int ReportedProgress { get { return reportedProgress; } }
        void InitializeBackgroundWorker() {
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
        }
        void worker_DoWork(object sender, DoWorkEventArgs e) {
            if(DoWork != null) DoWork(this, e);
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.Value = reportedProgress = (int)this.Maximum;
            if(RunWorkerCompleted != null) RunWorkerCompleted(this, e);
        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            this.Value = e.ProgressPercentage;
            if(ProgressChanged != null) ProgressChanged(this, e);
        }
    }
}
