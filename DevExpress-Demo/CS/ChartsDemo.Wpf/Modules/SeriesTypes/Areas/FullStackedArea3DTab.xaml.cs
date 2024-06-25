namespace ChartsDemo {
    public partial class FullStackedArea3DTab : TabItemModule {
        public FullStackedArea3DTab() {
            InitializeComponent();
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
