namespace ChartsDemo {
    public partial class ManhattanBarTab : TabItemModule {
        public ManhattanBarTab() {
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
