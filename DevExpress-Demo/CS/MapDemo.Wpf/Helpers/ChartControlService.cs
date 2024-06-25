using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapDemo {
    public interface IChartControlService {
        void Animate();
        void UpdateData();
    }
    public class ChartControlService : ServiceBase, IChartControlService {
        public ChartControl ChartControl { get { return (ChartControl)AssociatedObject; } }
        public void UpdateData() { 
            ChartControl.UpdateData();
        }
        public void Animate() {
            ChartControl.Animate();
        }
    }
}
