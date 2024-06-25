namespace GridDemo {
    public partial class EditEntireRow : GridDemoModule {
        public EditEntireRow() {
            InitializeComponent();          
        }        
        protected override void HidePopupContent() {
            view.CancelRowChanges();
            base.HidePopupContent();
        }
        protected override void ShowPopupContent() {
            view.CancelRowChanges();
            base.ShowPopupContent();            
        }
    }
}
