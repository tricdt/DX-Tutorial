using DevExpress.Xpf.PivotGrid;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using DependencyPropertyManager = System.Windows.DependencyProperty;

namespace ProductsDemo.Modules {
	public partial class PivotGridModule : UserControl {

		static PivotGridModule() {
			Type ownerType = typeof(PivotGridModule);
			PivotGridControlProperty = DependencyPropertyManager.Register("PivotGridControl", typeof(PivotGridModule),
				ownerType, new PropertyMetadata(null, OnPivotGridControlChanged));
		}

		public static void OnPivotGridControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if(e.NewValue == null)
				return;
			((PivotGridModule)d).Pivot = new WeakReference(e.NewValue);
		}

		WeakReference Pivot = null;
		public static readonly DependencyProperty PivotGridControlProperty;

		protected virtual bool NeedChangeEditorsTheme { get { return false; } }

		public PivotGridControl PivotGridControl {
			get { return (PivotGridControl)GetValue(PivotGridControlProperty); }
			set { SetValue(PivotGridControlProperty, value); }
		}

		public PivotGridModule() {
			InitializeComponent();
		}

		void OnLoaded(object sender, RoutedEventArgs e) {
			pivotGrid.BestFit();
		}
	}

	public class IntToKMConverter : MarkupExtension, IValueConverter {

		public override object ProvideValue(IServiceProvider serviceProvider) {
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			string s = value as string;
			if(s == null)
				return value;
			int result = default(int);
			if(int.TryParse(s, out result) && result > 0)
				return Convert(result);
			return value;
		}

		object Convert(int result) {
			if(result > 1000000000)
				return (result / 1000000000).ToString() + "B";
			else if(result > 1000000)
				return (result / 1000000).ToString() + "M";
			else if(result > 10000)
				return (result / 1000).ToString() + "K";
			else
				return result.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
