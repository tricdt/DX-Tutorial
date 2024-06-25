using System;
using System.ComponentModel;

namespace DevExpress.VideoRent.Helpers {
    public interface IBackgroundWorker {
        event DoWorkEventHandler DoWork;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
        event ProgressChangedEventHandler ProgressChanged;
        bool CancellationPending { get; }
        bool IsBusy { get; }
        bool WorkerReportsProgress { get; set; }
        bool WorkerSupportsCancellation { get; set; }
        void CancelAsync();
        void ReportProgress(int percentProgress);
        void ReportProgress(int percentProgress, object userState);
        void RunWorkerAsync();
        void RunWorkerAsync(object argument);
        int ReportedProgress { get; }
    }
    public class StandartBackgroundWorker : IBackgroundWorker {
        BackgroundWorker worker = new BackgroundWorker();
        int reportedProgress = 0;
        public event DoWorkEventHandler DoWork;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event ProgressChangedEventHandler ProgressChanged;
        public StandartBackgroundWorker()
            : base() {
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
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
        public void RunWorkerAsync() { worker.RunWorkerAsync(); }
        public void RunWorkerAsync(object argument) { worker.RunWorkerAsync(argument); }
        void worker_DoWork(object sender, DoWorkEventArgs e) {
            if(DoWork != null) DoWork(this, e);
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if(RunWorkerCompleted != null) RunWorkerCompleted(this, e);
        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if(ProgressChanged != null) ProgressChanged(this, e);
        }
        public int ReportedProgress { get { return reportedProgress; } }
    }
}
