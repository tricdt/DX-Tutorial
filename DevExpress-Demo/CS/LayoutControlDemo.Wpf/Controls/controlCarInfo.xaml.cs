using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;

namespace LayoutControlDemo {
    public partial class controlCarInfo : UserControl {
        private FrameworkElement _Owner;

        public controlCarInfo() : this(false, false, false) { }
        public controlCarInfo(bool showBackground, bool showBorder, bool showDetails) {
            InitializeComponent();
            if (!showBackground)
                LayoutRoot.Background = new SolidColorBrush(Colors.Transparent);
            if (!showBorder)
                layoutBase.Padding = new Thickness(0);
            if (!showDetails)
                foreach (FrameworkElement element in layoutBase.GetVisibleChildren())
                    if ((string)element.Tag == "DetailInfo")
                        element.SetVisible(false);
        }

        public Point ContentOffset { get { return layoutBase.MapPoint(layoutBase.ContentBounds.Location(), this); } }
        public FrameworkElement Owner {
            get { return _Owner; }
            set {
                if (Owner == value)
                    return;
                _Owner = value;
                LayoutRoot.IsHitTestVisible = Owner == null;
            }
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters) {
            if (Owner == null)
                return base.HitTestCore(hitTestParameters);
            else
                if (Owner.IsInVisualTree() && Owner.Contains(this.MapPoint(hitTestParameters.HitPoint, null)))
                    return new PointHitTestResult(Owner, TranslatePoint(hitTestParameters.HitPoint, Owner));
                else
                    return null;
        }
    }
}
