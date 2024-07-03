using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Controls
{
    public class TimeChangedEventArgs :RoutedEventArgs
    {
        public DateTime NewTime { get; set; }
        public TimeChangedEventArgs() { }
        public TimeChangedEventArgs(RoutedEvent routedEvent) : base(routedEvent) { }
        public TimeChangedEventArgs(RoutedEvent routedEvent, object source):base(routedEvent, source) { }

       
    }
}
