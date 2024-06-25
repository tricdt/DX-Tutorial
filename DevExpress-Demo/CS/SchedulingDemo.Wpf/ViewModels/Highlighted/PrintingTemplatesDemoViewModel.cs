using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Scheduling;

namespace SchedulingDemo.ViewModels {
    [POCOViewModel(ImplementIDataErrorInfo = true), MetadataType(typeof(PrintingTemplatesDemoViewModel.PrintingDemoViewModelMetadata))]
    public class PrintingTemplatesDemoViewModel {
        public class PrintingDemoViewModelMetadata : IMetadataProvider<PrintingTemplatesDemoViewModel> {
            void IMetadataProvider<PrintingTemplatesDemoViewModel>.BuildMetadata(MetadataBuilder<PrintingTemplatesDemoViewModel> builder) {
                builder.Property(x => x.PrintStart)
                    .MatchesInstanceRule((v, x) => v < x.PrintEnd);
                builder.Property(x => x.PrintEnd)
                    .MatchesInstanceRule((v, x) => v > x.PrintStart);
            }
        }

        public virtual DateTime Start { get; set; }
        public IEnumerable<WorkCalendar> Calendars { get { return WorkData.Calendars; } }
        public IEnumerable<WorkAppointment> Appointments { get { return WorkData.Appointments; } }
        
        public virtual ISchedulerReportService SchedulerReportService { get { return null; } }
        public virtual IDispatcherService DispatcherService { get { return null; } }
        public virtual bool IsPreviewPageVisible { get; set; }
        public virtual ReportTemplate ReportTemplate { get; set; }
        public virtual bool PrintVisibleInterval { get; set; }
        public virtual bool PrintVisibleResources { get; set; }
        public virtual DateTime PrintStart { get; set; }
        public virtual DateTime PrintEnd { get; set; }
        public DateTimeRange PrintInterval { get { return PrintVisibleInterval ? DateTimeRange.Empty : new DateTimeRange(PrintStart, PrintEnd); } }
        public IEnumerable<object> PrintResources { get { return PrintVisibleResources ? null : WorkData.Calendars.Where(x => (bool)x.Tag).Cast<object>(); } }
        public virtual object Report { get; set; }

        public virtual bool DailyReportShowCalendar { get; set; }
        public virtual bool DailyReportShowInterval { get; set; }
        public virtual int DailyReportDaysPerPage { get; set; }
        public virtual int DailyReportResourcesPerPage { get; set; }
        public virtual bool WeeklyReportShowCalendar { get; set; }
        public virtual bool WeeklyReportShowInterval { get; set; }
        public virtual int WeeklyReportResourcesPerPage { get; set; }
        public virtual bool MonthlyReportShowCalendar { get; set; }
        public virtual bool MonthlyReportShowInterval { get; set; }
        public virtual int MonthlyReportResourcesPerPage { get; set; }
        public virtual bool TimelineReportShowCalendar { get; set; }
        public virtual bool TimelineReportShowInterval { get; set; }
        public virtual int TimelineReportResourcesPerPage { get; set; }
        public virtual int TimelineReportIntervalsPerPage { get; set; }
        public virtual bool TrifoldReportShowCalendar { get; set; }

        public PrintingTemplatesDemoViewModel() {
            Start = WorkData.TodayStart;
            PrintVisibleInterval = true;
            PrintVisibleResources = true;
            PrintStart = Start;
            PrintEnd = Start.AddDays(1);
            ReportTemplate = ReportTemplate.DailyStyle;
            WorkData.Calendars.ToList()
                .ForEach(x => {
                    x.IsVisible = false;
                    x.Tag = false;
                });
            WorkData.CalendarMark.IsVisible = true;
            WorkData.CalendarMark.Tag = true;

            DailyReportShowCalendar = true;
            DailyReportShowInterval = true;
            DailyReportDaysPerPage = 0;
            DailyReportResourcesPerPage = 0;
            WeeklyReportShowCalendar = true;
            WeeklyReportShowInterval = true;
            WeeklyReportResourcesPerPage = 0;
            MonthlyReportShowCalendar = true;
            MonthlyReportShowInterval = true;
            MonthlyReportResourcesPerPage = 0;
            TimelineReportShowCalendar = true;
            TimelineReportShowInterval = true;
            TimelineReportResourcesPerPage = 0;
            TimelineReportIntervalsPerPage = 0;
            TrifoldReportShowCalendar = false;

            ((INotifyPropertyChanged)this).PropertyChanged += OnPropertyChanged;
        }
        public void OnLoaded() {
            IsPreviewPageVisible = true;
        }
        public void PreviewInNewWindow() {
            SchedulerReportService.ShowPrintPreview(ReportTemplate);
        }
        public void UpdateReport() {
            if (SchedulerReportService == null) return;
            this.RaisePropertyChanged(x => x.PrintInterval);
            this.RaisePropertyChanged(x => x.PrintResources);
            DispatcherService.BeginInvoke(new Action(() => 
                Report = SchedulerReportService.CreateReport(ReportTemplate)));
        }
        protected void OnIsPreviewPageVisibleChanged() {
            if(IsPreviewPageVisible)
                UpdateReport();
        }
        void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == BindableBase.GetPropertyName(() => DailyReportShowCalendar)
                || e.PropertyName == BindableBase.GetPropertyName(() => DailyReportShowInterval)
                || e.PropertyName == BindableBase.GetPropertyName(() => DailyReportDaysPerPage)
                || e.PropertyName == BindableBase.GetPropertyName(() => DailyReportResourcesPerPage)
                || e.PropertyName == BindableBase.GetPropertyName(() => WeeklyReportShowCalendar)
                || e.PropertyName == BindableBase.GetPropertyName(() => WeeklyReportShowInterval)
                || e.PropertyName == BindableBase.GetPropertyName(() => WeeklyReportResourcesPerPage)
                || e.PropertyName == BindableBase.GetPropertyName(() => MonthlyReportShowCalendar)
                || e.PropertyName == BindableBase.GetPropertyName(() => MonthlyReportShowInterval)
                || e.PropertyName == BindableBase.GetPropertyName(() => MonthlyReportResourcesPerPage)
                || e.PropertyName == BindableBase.GetPropertyName(() => TimelineReportShowCalendar)
                || e.PropertyName == BindableBase.GetPropertyName(() => TimelineReportShowInterval)
                || e.PropertyName == BindableBase.GetPropertyName(() => TimelineReportResourcesPerPage)
                || e.PropertyName == BindableBase.GetPropertyName(() => TimelineReportIntervalsPerPage)
                || e.PropertyName == BindableBase.GetPropertyName(() => TrifoldReportShowCalendar)

                || e.PropertyName == BindableBase.GetPropertyName(() => PrintStart)
                || e.PropertyName == BindableBase.GetPropertyName(() => PrintEnd)
                || e.PropertyName == BindableBase.GetPropertyName(() => PrintVisibleInterval)
                || e.PropertyName == BindableBase.GetPropertyName(() => PrintVisibleResources)
                || e.PropertyName == BindableBase.GetPropertyName(() => ReportTemplate))
                UpdateReport();
        }
    }
}
