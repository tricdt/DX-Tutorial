using System;
using System.Windows.Controls;
using DevExpress.Xpf.Gantt;

namespace GanttDemo.Examples {
    public partial class CustomizeTimescale : UserControl {
        public CustomizeTimescale() {
            InitializeComponent();
        }

        #region !
        static readonly TimescaleRuler _90MinutesRuler = new TimescaleRuler(TimescaleUnit.Minute, displayFormat: "H:mm", count: 90);
        static readonly TimescaleRuler _1weekRuler = new TimescaleRuler(TimescaleUnit.Week, formatProvider: new CustomDateTimeRangeFormatter());

        void GanttView_RequestTimeScales(object sender, RequestTimescaleRulersEventArgs e) {
            if(e.Zoom >= TimeSpan.FromMinutes(1) && e.Zoom < TimeSpan.FromMinutes(3))
                e.TimescaleRulers[2] = _90MinutesRuler;
            else if(e.Zoom >= TimeSpan.FromMinutes(30) && e.Zoom < TimeSpan.FromHours(2))
                e.TimescaleRulers[1] = _1weekRuler;
        }
        #endregion
    }
}
