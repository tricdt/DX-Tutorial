using DevExpress.Mvvm.UI;
using DevExpress.SalesDemo.Model;
using DevExpress.SalesDemo.Wpf.Converters;
using DevExpress.Xpf.Charts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.SalesDemo.Wpf.Controls {
    public class VolumeLabelControl : ItemsControl {
        public static readonly DependencyProperty PaletteProperty =
            DependencyProperty.Register("Palette", typeof(CustomPalette), typeof(VolumeLabelControl), 
            new PropertyMetadata(null));
        public static readonly DependencyProperty LabelForegroundProperty =
            DependencyProperty.Register("LabelForeground", typeof(Brush), typeof(VolumeLabelControl), 
            new PropertyMetadata(null));
        public CustomPalette Palette {
            get { return (CustomPalette)GetValue(PaletteProperty); }
            set { SetValue(PaletteProperty, value); }
        }
        public Brush LabelForeground {
            get { return (Brush)GetValue(LabelForegroundProperty); }
            set { SetValue(LabelForegroundProperty, value); }
        }

        public VolumeLabelControl() {
            DefaultStyleKey = typeof(VolumeLabelControl);
        }
        public int IndexOf(object item) {
            if(item is VolumeLabelItem) {
                return ItemContainerGenerator.IndexFromContainer((VolumeLabelItem)item);
            }
            return Items.IndexOf(item);
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            if(element is VolumeLabelItem)
                ((VolumeLabelItem)element).Initialize(this);
        }
        protected override DependencyObject GetContainerForItemOverride() {
            return new VolumeLabelItem();
        }
    }
    public class VolumeLabelItem : HeaderedContentControl {
        public const string ElementName_PART_Label = "PART_Label";
        VolumeLabelControl Owner;
        PaletteToBrushConverter PaletteToBrushConverter;
        public VolumeLabelItem() {
            DefaultStyleKey = typeof(VolumeLabelItem);
            PaletteToBrushConverter = new PaletteToBrushConverter();
        }
        public void Initialize(VolumeLabelControl owner) {
            Owner = owner;
            PaletteToBrushConverter.Palette = Owner.Palette;
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            if(Owner == null) return;
            TextBlock labelElement = (TextBlock)GetTemplateChild(ElementName_PART_Label);
            if(Owner.LabelForeground != null)
                labelElement.Foreground = Owner.LabelForeground;
            else if(Owner.Palette != null)
                labelElement.Foreground = (Brush)PaletteToBrushConverter.Convert(Owner.IndexOf(this), typeof(Brush), null, null);
            else
                labelElement.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
