using System.Windows.Media;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class LogarithmicScaleViewModel {
        public static LogarithmicScaleViewModel Create() {
            return ViewModelSource.Create(() => new LogarithmicScaleViewModel());
        }

        public virtual Palette Palette {
            get;
            set;
        }
        public virtual SolidColorBrush Headphones190dBSPLBrush {
            get;
            protected set;
        }
        public virtual SolidColorBrush Headphones1100dBSPLBrush {
            get;
            protected set;
        }
        public virtual SolidColorBrush Headphones290dBSPLBrush {
            get;
            protected set;
        }
        public virtual SolidColorBrush Headphones2100dBSPLBrush {
            get;
            protected set;
        }

        protected LogarithmicScaleViewModel() {
            Palette = new OfficePalette();
        }

        protected void OnPaletteChanged() {
            if (Palette == null)
                return;
            Headphones190dBSPLBrush = new SolidColorBrush(Palette[0]);
            Headphones1100dBSPLBrush = new SolidColorBrush(ColorUtils.ChangeColorLuminance(Palette[0], 0.8f));
            Headphones290dBSPLBrush = new SolidColorBrush(Palette[1]);
            Headphones2100dBSPLBrush = new SolidColorBrush(ColorUtils.ChangeColorLuminance(Palette[1], 0.8f));
            Headphones190dBSPLBrush.Freeze();
            Headphones1100dBSPLBrush.Freeze();
            Headphones290dBSPLBrush.Freeze();
            Headphones2100dBSPLBrush.Freeze();
        }
    }
}
