namespace GridDemo.Filtering {
	public partial class AutoFilterRow : GridDemoModule {
		public AutoFilterRow() {
			InitializeComponent();
			grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataList(1000);
		}
	}
}
