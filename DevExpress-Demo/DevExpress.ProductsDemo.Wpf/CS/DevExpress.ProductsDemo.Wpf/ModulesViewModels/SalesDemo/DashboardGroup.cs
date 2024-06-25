using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ProductsDemo {
    [ContentProperty("Content")]
    public class DashboardGroup : Control {
        #region Dependency Properties
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(DashboardGroup), new PropertyMetadata(null));
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(DashboardGroup), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(DashboardGroup), new PropertyMetadata(new Thickness(0)));
        #endregion

        public object Content {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public string HeaderText {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }
        public Thickness HeaderMargin {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        public DashboardGroup() {
            DefaultStyleKey = typeof(DashboardGroup);
        }
    }
}
