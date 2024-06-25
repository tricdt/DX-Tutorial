using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Controls;



namespace GridDemo {
    public class DemoColumnChooserControl : Control {
        public static readonly DependencyProperty ViewProperty;

        static DemoColumnChooserControl() {
            ViewProperty = DependencyProperty.Register("View", typeof(GridViewBase), typeof(DemoColumnChooserControl), new PropertyMetadata(null));
        }
        public DemoColumnChooserControl() {
            DefaultStyleKey = typeof(DemoColumnChooserControl);
        }

        public GridViewBase View {
            get { return (GridViewBase)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        internal ColumnChooserControl ColunmChooserControl { get; private set; }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            ColunmChooserControl = (ColumnChooserControl)GetTemplateChild("PART_ColumnChooserControl");
        }
    }
    public class DemoColumnChooser : IColumnChooser, IColumnChooserFactory {
        readonly DemoColumnChooserControl columnChooserControl;
        public DemoColumnChooser(DemoColumnChooserControl columnChooserControl) {
            this.columnChooserControl = columnChooserControl;
        }
        #region IColumnChooser Members
        void IColumnChooser.Show() { }
        void IColumnChooser.Hide() { }
        void IColumnChooser.ApplyState(IColumnChooserState state) { }
        void IColumnChooser.SaveState(IColumnChooserState state) { }
        void IColumnChooser.Destroy() { }
        UIElement IColumnChooser.TopContainer { get { return columnChooserControl.ColunmChooserControl; } }
        #endregion

        #region IColumnChooserFactory Members
        IColumnChooser IColumnChooserFactory.Create(Control owner) {
            return this;
        }
        #endregion
    }
}
