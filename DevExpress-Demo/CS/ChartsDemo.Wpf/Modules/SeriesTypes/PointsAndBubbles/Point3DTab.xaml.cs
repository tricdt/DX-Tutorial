namespace ChartsDemo {
    public partial class Point3DTab : TabItemModule {
        public Point3DTab() {
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
