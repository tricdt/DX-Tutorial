namespace ChartsDemo {
    public partial class Bubble3DTab : TabItemModule {
        public Bubble3DTab() {
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
