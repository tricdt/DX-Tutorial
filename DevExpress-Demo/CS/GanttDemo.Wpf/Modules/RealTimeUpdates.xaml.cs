using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Gantt;

namespace GanttDemo {
    [CodeFile("Utils/CSVLoader.cs")]
    [CodeFile("ViewModels/PageLoadingViewModel.cs")]
    public partial class RealTimeUpdates : GanttDemoModule {
        public RealTimeUpdates() {
            InitializeComponent();
        }

        void RequestGanttTimescaleRulers(object sender, RequestTimescaleRulersEventArgs e) {
            e.TimescaleRulers[0] = new TimescaleRuler(TimescaleUnit.Minute, "hh:mm");
            e.TimescaleRulers[1] = new TimescaleRuler(TimescaleUnit.Second, "s 'sec'", 1);
            e.TimescaleRulers[2] = new TimescaleRuler(TimescaleUnit.Millisecond, "fff", 250);
        }
    }
}
