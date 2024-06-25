using DevExpress.Xpf.DemoBase;
using System.Windows.Data;
using System.Windows.Media.Animation;
namespace WindowsUIDemo {
    [CodeFile("Modules/Views/NavigatorView.xaml")]
    [CodeFile("Modules/Views/DashboardView.xaml")]
    [CodeFile("Modules/Views/LoanCalculatorView.xaml")]
    [CodeFile("Modules/Views/MortgageRatesView.xaml")]
    [CodeFile("Modules/Views/ResearchView.xaml")]
    [CodeFile("Modules/Views/StatisticsView.xaml")]
    [CodeFile("Modules/Views/SystemInformationView.xaml")]
    [CodeFile("Modules/Views/UserManagementView.xaml")]
    [CodeFile("Modules/Views/ZillowView.xaml")]
    public partial class FrameNavigation : WindowsUIDemoModule {
        public FrameNavigation() {
            InitializeComponent();
        }
    }

    public class FrameAnimationSelector : DevExpress.Xpf.WindowsUI.AnimationSelector {
        private Storyboard _BackStoryboard;
        private Storyboard _ForwardStoryboard;
        public Storyboard ForwardStoryboard {
            get { return _ForwardStoryboard; }
            set { _ForwardStoryboard = value; }
        }
        public Storyboard BackStoryboard {
            get { return _BackStoryboard; }
            set { _BackStoryboard = value; }
        }
        protected override Storyboard SelectStoryboard(DevExpress.Xpf.WindowsUI.FrameAnimation animation) {
            return animation.Direction == DevExpress.Xpf.WindowsUI.AnimationDirection.Forward ? ForwardStoryboard : BackStoryboard;
        }
    }
}
