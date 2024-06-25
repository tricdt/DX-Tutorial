using System.Windows;

namespace MapDemo {
    public class HotelLabel : VisibleControl {
        public static readonly DependencyProperty HotelInfoProperty = DependencyProperty.Register("HotelInfo",
            typeof(HotelInfo), typeof(HotelLabel), new PropertyMetadata(null));

        public HotelInfo HotelInfo {
            get { return (HotelInfo)GetValue(HotelInfoProperty); }
            set { SetValue(HotelInfoProperty, value); }
        }

        public HotelLabel() {
            DefaultStyleKey = typeof(HotelLabel);
        }
    }
}
