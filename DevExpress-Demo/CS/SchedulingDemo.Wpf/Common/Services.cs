using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Native;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraScheduler.Reporting;
using System.Windows;

namespace SchedulingDemo {
    public interface ISchedulerReportService {
        ISchedulerReport CreateReport(ReportTemplate template);
        void ShowPrintPreview(ReportTemplate template);
    }
    public class SchedulerReportService : ServiceBase, ISchedulerReportService {
        public static readonly DependencyProperty SchedulerProperty =
            DependencyProperty.Register("Scheduler", typeof(SchedulerControl), typeof(SchedulerReportService), new PropertyMetadata(null));
        public SchedulerControl Scheduler { get { return (SchedulerControl)GetValue(SchedulerProperty); } set { SetValue(SchedulerProperty, value); } }
       
        ISchedulerReport ISchedulerReportService.CreateReport(ReportTemplate template) {
            var res = Scheduler.CreateReport(template);
            res.CreateDocument();
            return res;
        }
        void ISchedulerReportService.ShowPrintPreview(ReportTemplate template) {
            Scheduler.ShowPrintPreview(template);
        }
    }
}
