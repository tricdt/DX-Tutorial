using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Collections.Generic;
using DevExpress.Xpf.Editors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.Xpf.Editors.Validation;
using System.Windows.Threading;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public abstract class BindingInfoBase : FrameworkElement {
        #region Dependency Properties
        public static readonly DependencyProperty TopElementProperty;
        static BindingInfoBase() {
            Type ownerType = typeof(BindingInfoBase);
            TopElementProperty = DependencyProperty.Register("TopElement", typeof(FrameworkElement), ownerType, new PropertyMetadata(null,
                (d, e) => ((BindingInfoBase)d).RaiseTopElementChanged(e)));
        }
        #endregion
        List<string> elementNames = new List<string>();

        public BindingInfoBase() { }
        internal FrameworkElement TopElement { get { return (FrameworkElement)GetValue(TopElementProperty); } set { SetValue(TopElementProperty, value); } }
        protected List<string> ElementNames { get { return elementNames; } }
        protected abstract void Activate(int elementNameIndex, DependencyObject element);
        void RaiseTopElementChanged(DependencyPropertyChangedEventArgs e) {
            FrameworkElement oldValue = (FrameworkElement)e.OldValue;
            FrameworkElement newValue = (FrameworkElement)e.NewValue;
            if(oldValue != null)
                oldValue.Initialized -= OnTopElementInitialized;
            if(newValue != null)
                newValue.Initialized += OnTopElementInitialized;
        }
        void OnTopElementInitialized(object sender, EventArgs e) {
            FrameworkElement topElement = (FrameworkElement)sender;
            for(int i = 0; i < ElementNames.Count; ++i) {
                if(string.IsNullOrEmpty(ElementNames[i])) continue;
                DependencyObject element = FindName(topElement, ElementNames[i]);
                if(element != null) {
                    ShareDataContext(element as FrameworkElement, FrameworkElement.DataContextProperty);
                    ShareDataContext(element as FrameworkContentElement, FrameworkContentElement.DataContextProperty);
                    Activate(i, element);
                } else {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(string.Format("BindingInfoBase: Element with Name=\'{0}\' not resolved", ElementNames[i]));
#endif
                }
            }
        }
        void ShareDataContext(DependencyObject element, DependencyProperty dataContextProperty) {
            if(element == null) return;
            BindingOperations.SetBinding(element, dataContextProperty, new Binding("DataContext") { Source = this });
        }
        static DependencyObject FindName(FrameworkElement topElement, string name) {
            int d = name.IndexOf(':');
            if(d < 0) return topElement.FindName(name) as DependencyObject;
            return name.Remove(d) == "top" ? topElement : topElement.Resources[name.Substring(d + 1)] as DependencyObject;
        }
    }
    [ContentProperty("Items")]
    public class BindingsInfoCollection : FrameworkElement {
        #region Dependency Properties
        public static readonly DependencyProperty ItemsProperty;
        public static readonly DependencyProperty TopElementProperty;
        public static readonly DependencyProperty IsValidProperty;
        public static readonly DependencyProperty DoValidateSignalSlotProperty;
        static BindingsInfoCollection() {
            Type ownerType = typeof(BindingsInfoCollection);
            ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<BindingInfoBase>), ownerType, new PropertyMetadata(null));
            TopElementProperty = DependencyProperty.Register("TopElement", typeof(FrameworkElement), ownerType, new PropertyMetadata(null));
            IsValidProperty = DependencyProperty.Register("IsValid", typeof(bool), ownerType, new PropertyMetadata(true));
            DoValidateSignalSlotProperty = DependencyProperty.Register("DoValidateSignalSlot", typeof(bool), ownerType, new PropertyMetadata(false,
                (d, e) => ((BindingsInfoCollection)d).RaiseDoValidateSignalSlotChanged(e)));
        }
        #endregion
        List<ValidationInfo> validations = new List<ValidationInfo>();

        public BindingsInfoCollection() {
            ObservableCollection<BindingInfoBase> items = new ObservableCollection<BindingInfoBase>();
            items.CollectionChanged += OnItemsCollectionChanged;
            Items = items;
        }
        public ObservableCollection<BindingInfoBase> Items { get { return (ObservableCollection<BindingInfoBase>)GetValue(ItemsProperty); } private set { SetValue(ItemsProperty, value); } }
        public bool IsValid { get { return (bool)GetValue(IsValidProperty); } set { SetValue(IsValidProperty, value); } }
        public bool DoValidateSignalSlot { get { return (bool)GetValue(DoValidateSignalSlotProperty); } set { SetValue(DoValidateSignalSlotProperty, value); } }
        public bool DoValidate() {
            bool isValid = true;
            foreach(ValidationInfo validationInfo in this.validations) {
                if(!validationInfo.DoValidate())
                    isValid = false;
            }
            IsValid = isValid;
            return isValid;
        }
        internal FrameworkElement TopElement { get { return (FrameworkElement)GetValue(TopElementProperty); } set { SetValue(TopElementProperty, value); } }
        void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            this.validations.Clear();
            foreach(BindingInfoBase bindingInfo in Items) {
                bindingInfo.SetBinding(BindingInfoBase.DataContextProperty, new Binding("DataContext") { Source = this });
                bindingInfo.SetBinding(BindingInfoBase.TopElementProperty, new Binding("TopElement") { Source = this });
                ValidationInfo validation = bindingInfo as ValidationInfo;
                if(validation != null)
                    this.validations.Add(validation);
            }
        }
        void RaiseDoValidateSignalSlotChanged(DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) return;
            DoValidate();
        }
    }
    public class DataBindingsHelper : DependencyObject {
        public static readonly DependencyProperty BindingsProperty;
        static DataBindingsHelper() {
            Type ownerType = typeof(DataBindingsHelper);
            BindingsProperty = DependencyProperty.RegisterAttached("Bindings", typeof(BindingsInfoCollection), ownerType, new PropertyMetadata(null, BindingsChanged));
        }
        public static BindingsInfoCollection GetBindings(FrameworkElement target) { return (BindingsInfoCollection)target.GetValue(BindingsProperty); }
        public static void SetBindings(FrameworkElement target, BindingsInfoCollection bindings) { target.SetValue(BindingsProperty, bindings); }
        static void BindingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FrameworkElement target = (FrameworkElement)d;
            BindingsInfoCollection bindings = (BindingsInfoCollection)e.NewValue;
            bindings.TopElement = target;
        }
    }
    public class BindingInfo : BindingInfoBase {
        public BindingInfo() {
            ElementNames.Add(string.Empty);
        }
        public string ElementName { get { return ElementNames[0]; } set { ElementNames[0] = value; } }
        public DependencyProperty Property { get; set; }
        public BindingBase Value { get; set; }
        protected override void Activate(int elementNameIndex, DependencyObject element) {
            BindingOperations.SetBinding(element, Property, Value);
        }
    }
    public class ValueInfo : BindingInfoBase {
        public ValueInfo() {
            ElementNames.Add(string.Empty);
        }
        public string ElementName { get { return ElementNames[0]; } set { ElementNames[0] = value; } }
        public DependencyProperty Property { get; set; }
        public object Value { get; set; }
        protected override void Activate(int elementNameIndex, DependencyObject element) {
            element.SetValue(Property, Value);
        }
    }
    public interface IValidationRule {
        void Validate(object sender, ValidationEventArgs e);
    }
    public class ValidationInfo : BindingInfoBase {
        const int EditsMaxCount = 4;
        BaseEdit[] edits = new BaseEdit[EditsMaxCount];
        bool isValid = true;
        object errorContent = null;
        ErrorType errorType = ErrorType.None;
        bool editsCleaning = false;
        bool editsUpdating = false;

        public ValidationInfo() {
            for(int i = EditsMaxCount; --i >= 0; )
                ElementNames.Add(string.Empty);
        }
        public string EditName0 { get { return ElementNames[0]; } set { ElementNames[0] = value; } }
        public string EditName1 { get { return ElementNames[1]; } set { ElementNames[1] = value; } }
        public string EditName2 { get { return ElementNames[2]; } set { ElementNames[2] = value; } }
        public string EditName3 { get { return ElementNames[3]; } set { ElementNames[3] = value; } }
        public IValidationRule Rule { get; set; }
        public bool DoValidate() {
            CleanEdits();
            bool isValid = false;
            object errorContent = null;
            ErrorType errorType = ErrorType.None;
            foreach(BaseEdit edit in this.edits) {
                if(edit == null) continue;
                ValidationEventArgs vea = new ValidationEventArgs(BaseEdit.ValidateEvent, edit, edit.EditValue, null);
                Rule.Validate(edit, vea);
                if(vea.IsValid) {
                    isValid = true;
                    break;
                } else {
                    errorContent = vea.ErrorContent;
                    errorType = vea.ErrorType;
                }
            }
            SetError(isValid, errorContent, errorType);
            UpdateEdits();
            return this.isValid;
        }
        protected override void Activate(int elementNameIndex, DependencyObject element) {
            BaseEdit edit = element as BaseEdit;
            this.edits[elementNameIndex] = edit;
            if(edit == null) return;
            edit.InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
            edit.Validate += OnEditValidate;
        }
        void CleanEdits() {
            this.editsCleaning = true;
            foreach(BaseEdit edit in this.edits) {
                if(edit == null || edit.ValidationError == null) continue;
                edit.DoValidate();
            }
            this.editsCleaning = false;
        }
        void UpdateEdits() {
            this.editsUpdating = true;
            foreach(BaseEdit edit in this.edits) {
                if(edit == null) continue;
                edit.DoValidate();
            }
            this.editsUpdating = false;
        }
        void SetError(bool isValid, object errorContent, ErrorType errorType) {
            this.errorContent = errorContent;
            this.errorType = errorType;
            this.isValid = isValid;
        }
        void WriteError(ref ValidationEventArgs e) {
            if(!this.isValid) {
                e.ErrorContent = this.errorContent;
                e.ErrorType = this.errorType;
                e.IsValid = false;
            }
        }
        void OnEditValidate(object sender, ValidationEventArgs e) {
            if(this.editsCleaning) return;
            BaseEdit validateEdit = (BaseEdit)sender;
            if(this.editsUpdating || object.Equals(e.Value, validateEdit.EditValue)) {
                WriteError(ref e);
            } else {
                Dispatcher.BeginInvoke((Action)(() => DoValidate()), DispatcherPriority.Render);
            }
        }
    }
}
