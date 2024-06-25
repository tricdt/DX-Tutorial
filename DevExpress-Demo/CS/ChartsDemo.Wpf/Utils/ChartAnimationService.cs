using System;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {

    public interface IChartAnimationService {
        void Animate();
    }


    public class ChartAnimationService : ServiceBase, IChartAnimationService {
        ChartControl Chart { get { return (ChartControl)AssociatedObject; } }
        public void Animate() {
            Chart.Dispatcher.BeginInvoke(new Action(Chart.Animate));
        }
    }

}
