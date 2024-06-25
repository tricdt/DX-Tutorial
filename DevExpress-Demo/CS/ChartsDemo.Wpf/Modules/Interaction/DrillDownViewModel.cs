using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.POCO;

namespace ChartsDemo {
    public class DrillDownViewModel {
        public static DrillDownViewModel Create() {
            return ViewModelSource.Create(() => new DrillDownViewModel());
        }

        public List<DevAVBranch> HierarchicalData {
            get;
            protected set;
        }
        public virtual bool DiagramRotated {
            get;
            protected set;
        }
        public virtual bool ShowValuesZeroLevel {
            get;
            protected set;
        }
        public virtual double SideMargins {
            get;
            protected set;
        }
        public virtual bool ToolTipEnabled {
            get;
            protected set;
        }
        public virtual bool CrosshairEnabled {
            get;
            protected set;
        }
        public virtual string AxisYTextPattern {
            get;
            protected set;
        }

        protected DrillDownViewModel() {
            DevAVSales sales = new DevAVSales();
            HierarchicalData = sales.GetHierarchicalSalesData();
        }

        public void OnDrillDownStateChanged(List<object> breadcrumbPath) {
            object last = breadcrumbPath.Last();
            if (last == null) {
                DiagramRotated = true;
                ShowValuesZeroLevel = true;
                SideMargins = 0.5;
                CrosshairEnabled = false;
                ToolTipEnabled = true;
                AxisYTextPattern = "${V:0,,.##}M";
            }
            else if (last is DevAVBranch) {
                DiagramRotated = false;
                ShowValuesZeroLevel = true;
                SideMargins = 0.5;
                CrosshairEnabled = true;
                ToolTipEnabled = false;
                AxisYTextPattern = "${V:0,.##}K";
            }
            else if (last is DevAVProductCategory) {
                DiagramRotated = false;
                ShowValuesZeroLevel = true;
                SideMargins = 0;
                CrosshairEnabled = true;
                ToolTipEnabled = false;
                AxisYTextPattern = "${V:0,.##}K";
            }
            else if (last is DevAVProduct) {
                DiagramRotated = false;
                ShowValuesZeroLevel = false;
                SideMargins = 0;
                CrosshairEnabled = true;
                ToolTipEnabled = false;
                AxisYTextPattern = "${V:0.##}";
            }
        }
    }
}
