using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.VisualData;

namespace SchedulingDemo.ViewModels {
    public class CustomTimeRulerWindowViewModel : TimeRulerWindowViewModel {
        readonly TimeRuler timeRuler;
        public CustomTimeRulerWindowViewModel(TimeRuler timeRuler, TimeZoneInfo defaultTimeZone) : base(timeRuler, defaultTimeZone) {
            this.timeRuler = timeRuler;
            ShowMinutes = this.timeRuler.ShowMinutes;
            AlwaysShowTimeDesignator = this.timeRuler.AlwaysShowTimeDesignator;
        }

        public bool ShowMinutes { get { return GetProperty(() => ShowMinutes); } set { SetProperty(() => ShowMinutes, value); } }
        public bool AlwaysShowTimeDesignator { get { return GetProperty(() => AlwaysShowTimeDesignator); } set { SetProperty(() => AlwaysShowTimeDesignator, value); } }
        protected override void Save() {
            base.Save();
            this.timeRuler.ShowMinutes = ShowMinutes;
            this.timeRuler.AlwaysShowTimeDesignator = AlwaysShowTimeDesignator;
        }

    }
}
