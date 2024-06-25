using DevExpress.Xpf.Charts;
using System.Windows.Input;

namespace ChartsDemo {
    public partial class Pie3DTab : TabItemModule {
        public Pie3DTab() {
            InitializeComponent();
        }

        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = this.chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.SeriesPoint != null && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Released)
                e.Cursor = Cursors.Hand;
        }
        bool isAnimationCompleted = false;
        public override bool IsAnimationCompleted {
            get {
                return isAnimationCompleted;
            }
        }
        private void Storyboard_Completed(object sender, System.EventArgs e) {
            isAnimationCompleted = true;
        }
    }
}
