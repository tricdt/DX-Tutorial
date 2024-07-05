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

namespace MyDX_Demo.Controls
{
    /// <summary>
    /// Interaction logic for ControllableClock.xaml
    /// </summary>
    public partial class ControllableClock : UserControl
    {


        public Clock Clock
        {
            get { return (Clock)GetValue(ClockProperty); }
            set { SetValue(ClockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClockProperty =
            DependencyProperty.Register("Clock", typeof(Clock), typeof(ControllableClock), new PropertyMetadata(null));


        public ControllableClock()
        {
            InitializeComponent();
        }
    }
}
