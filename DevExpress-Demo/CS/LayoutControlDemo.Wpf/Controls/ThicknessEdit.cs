using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;

namespace LayoutControlDemo {
    [TemplatePart(Name = ThicknessMemberEdit.EditorElementName, Type = typeof(ComboBoxEdit))]
    public class ThicknessMemberEdit : Control {
        #region Dependency Properties

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(Thickness), typeof(ThicknessMemberEdit),
                new PropertyMetadata(new PropertyChangedCallback(OnThicknessChanged)));

        static void OnThicknessChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((ThicknessMemberEdit)o).OnThicknessChanged();
        }

        #endregion Dependency Properties

        private Side _Side;

        public ThicknessMemberEdit() {
            DefaultStyleKey = typeof(ThicknessMemberEdit);
        }

        public Side Side {
            get { return _Side; }
            set {
                if (Side.Equals(value))
                    return;
                _Side = value;
                UpdateSelectedItem();
            }
        }
        public Thickness Thickness {
            get { return (Thickness)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        #region Template

        internal const string EditorElementName = "EditorElement";

        public override void OnApplyTemplate() {
            if (EditorElement != null)
                EditorElement.SelectedIndexChanged -= OnEditorElementSelectedIndexChanged;
            base.OnApplyTemplate();
            EditorElement = GetTemplateChild(EditorElementName) as ComboBoxEdit;
            if (EditorElement != null)
                EditorElement.SelectedIndexChanged += OnEditorElementSelectedIndexChanged;
            UpdateSelectedItem();
        }

        protected ComboBoxEdit EditorElement { get; private set; }

        private void OnEditorElementSelectedIndexChanged(object sender, RoutedEventArgs e) {
            OnSelectionChanged();
        }

        #endregion Template

        protected virtual void OnSelectionChanged() {
            UpdateThickness();
        }
        protected virtual void OnThicknessChanged() {
            UpdateSelectedItem();
        }
        protected void UpdateSelectedItem() {
            if (EditorElement != null)
                EditorElement.SelectedItem = Thickness.GetValue(Side);
        }
        protected void UpdateThickness() {
            if (EditorElement != null)
                Thickness = Thickness.ChangeValue(Side, (double)EditorElement.SelectedItem);
        }
    }

    public class ThicknessEdit : Control {
        #region Dependency Properties

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Thickness), typeof(ThicknessEdit), null);

        #endregion Dependency Properties

        public ThicknessEdit() {
            DefaultStyleKey = typeof(ThicknessEdit);
        }

        public Thickness Value {
            get { return (Thickness)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}
