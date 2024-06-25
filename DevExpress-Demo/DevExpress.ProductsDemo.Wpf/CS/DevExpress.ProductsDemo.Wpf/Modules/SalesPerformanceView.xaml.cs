using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductsDemo.Modules {
    
    
    
    public partial class SalesPerformanceView : UserControl {
        public SalesPerformanceView() {
            InitializeComponent();
        }
        #region Dependency properties
        public static readonly DependencyProperty DateBorderMarginProperty =
            DependencyProperty.Register("DateBorderMargin", typeof(Thickness), typeof(SalesPerformanceView),
            new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty SalesVolumesMarginProperty =
            DependencyProperty.Register("SalesVolumesMargin", typeof(Thickness), typeof(SalesPerformanceView),
            new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty AreaAndSalesVolumesBrushProperty =
            DependencyProperty.Register("AreaAndSalesVolumesBrush", typeof(SolidColorBrush), typeof(SalesPerformanceView),
            new PropertyMetadata(Brushes.Red));
        public static readonly DependencyProperty ButtonsGridMarginProperty =
            DependencyProperty.Register("ButtonsGridMargin", typeof(Thickness), typeof(SalesPerformanceView),
            new PropertyMetadata(new Thickness(0)));
        #endregion

        public Thickness DateBorderMargin {
            get { return (Thickness)GetValue(DateBorderMarginProperty); }
            set { SetValue(DateBorderMarginProperty, value); }
        }
        public Thickness SalesVolumesMargin {
            get { return (Thickness)GetValue(SalesVolumesMarginProperty); }
            set { SetValue(DateBorderMarginProperty, value); }
        }
        public SolidColorBrush AreaAndSalesVolumesBrush {
            get { return (SolidColorBrush)GetValue(AreaAndSalesVolumesBrushProperty); }
            set { SetValue(AreaAndSalesVolumesBrushProperty, value); }
        }
        public Thickness ButtonsGridMargin {
            get { return (Thickness)GetValue(ButtonsGridMarginProperty); }
            set { SetValue(ButtonsGridMarginProperty, value); }
        }
    }
}
