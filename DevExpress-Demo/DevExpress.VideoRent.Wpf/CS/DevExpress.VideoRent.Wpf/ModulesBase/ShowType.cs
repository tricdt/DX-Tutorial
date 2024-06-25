using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.VideoRent.Wpf.Helpers;
using System.Windows.Data;
using System.Windows.Threading;

namespace DevExpress.VideoRent.Wpf.ModulesBase {
    public abstract class ShowTypeBase : FrameworkElement {
        public virtual void Apply(FrameworkElement content) { }
    }
    public class ShowType : ShowTypeBase {
        #region Dependency Properties
        public static readonly DependencyProperty CloseSignalSlotProperty;
        static ShowType() {
            Type ownerType = typeof(ShowType);
            CloseSignalSlotProperty = DependencyProperty.Register("CloseSignalSlot", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((ShowType)d).RaiseCloseSignalSlotChanged(e)));
        }
        public bool CloseSignalSlot { get { return (bool)GetValue(CloseSignalSlotProperty); } set { SetValue(CloseSignalSlotProperty, value); } }
        public virtual void Close() { }
        #endregion
        void RaiseCloseSignalSlotChanged(DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) return;
            Close();
        }
    }
    public interface ISupportCustomShow {
        ShowTypeBase ShowMethodType { get; set; }
        void Show();
    }
    public class PopupShowType : ShowType {
        #region Dependency Properties
        public static readonly DependencyProperty TitleProperty;
        static PopupShowType() {
            Type ownerType = typeof(PopupShowType);
            TitleProperty = DependencyProperty.Register("Title", typeof(string), ownerType, new PropertyMetadata(string.Empty));
        }
        #endregion
        bool alreadyClosing;

        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }
        public event EventHandler Closing;
        public override void Apply(FrameworkElement content) {
            base.Apply(content);
            alreadyClosing = false;
            PopupWindow = new PopupWindow() { Content = content };
            BindingOperations.SetBinding(PopupWindow, PopupWindow.TitleProperty, new Binding("Title") { Source = this, Mode = BindingMode.OneWay });
            PopupWindow.Closing += OnContainerClosing;
            Dispatcher.BeginInvoke((Action)(() => PopupWindow.ShowDialog()), DispatcherPriority.Render);
        }
        public override void Close() {
            alreadyClosing = true;
            if(PopupWindow != null)
                PopupWindow.Close();
        }
        protected PopupWindow PopupWindow { get; set; }
        protected virtual void OnContainerClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            if(!alreadyClosing) {
                Dispatcher.BeginInvoke((Action)RaiseClosing, System.Windows.Threading.DispatcherPriority.Render);
                e.Cancel = true;
            }
        }
        protected void RaiseClosing() {
            if(Closing != null)
                Closing(this, EventArgs.Empty);
        }
    }
    public class ClassicShowType : ShowType {
        #region Dependency Properties
        public static readonly DependencyProperty CategoryProperty;
        public static readonly DependencyProperty SubcategoryProperty;
        public static readonly DependencyProperty SelectProperty;
        public static readonly DependencyProperty SelectSignalSlotProperty;
        static ClassicShowType() {
            Type ownerType = typeof(ClassicShowType);
            CategoryProperty = DependencyProperty.Register("Category", typeof(DemoModuleCategory), ownerType, new PropertyMetadata(null, (d, e) => ((ClassicShowType)d).RaiseCategoryChanged(e)));
            SubcategoryProperty = DependencyProperty.Register("Subcategory", typeof(string), ownerType, new PropertyMetadata(string.Empty));
            SelectProperty = DependencyProperty.Register("Select", typeof(bool), ownerType, new PropertyMetadata(false));
            SelectSignalSlotProperty = DependencyProperty.Register("SelectSignalSlot", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((ClassicShowType)d).RaiseSelectSignalSlotChanged()));
        }
        #endregion
        DemoModule demoModule;

        public DemoModuleCategory Category { get { return (DemoModuleCategory)GetValue(CategoryProperty); } set { SetValue(CategoryProperty, value); } }
        public string Subcategory { get { return (string)GetValue(SubcategoryProperty); } set { SetValue(SubcategoryProperty, value); } }
        public bool Select { get { return (bool)GetValue(SelectProperty); } set { SetValue(SelectProperty, value); } }
        public bool SelectSignalSlot { get { return (bool)GetValue(SelectSignalSlotProperty); } set { SetValue(SelectSignalSlotProperty, value); } }
        public override void Apply(FrameworkElement content) {
            base.Apply(content);
            DemoModule module = content as DemoModule;
            if(DemoModulesControl.Current == null) return;
            module.Height = double.NaN;
            module.Width = double.NaN;
            demoModule = module;
            if(demoModule == null) return;
            demoModule.AddPrefixToBarItems();
            if(Category != null)
                Show();
        }
        public override void Close() {
            if(demoModule == null) return;
            DemoModulesControl.Current.RemoveDemoModule(demoModule);
        }
        void Show() {
            DemoModulesControl.Current.AddDemoModule(demoModule);
            if(Select) DemoModulesControl.Current.SelectDemoModule(demoModule);
        }
        void RaiseSelectSignalSlotChanged() {
            if(SelectSignalSlot || demoModule == null) return;
            DemoModulesControl.Current.SelectDemoModule(demoModule);
        }
        void RaiseCategoryChanged(DependencyPropertyChangedEventArgs e) {
            if(e.NewValue != null && demoModule != null)
                Show();
        }
    }
    public class CustomShowUserControl : UserControl, ISupportCustomShow {
        #region Dependency Properties
        public static readonly DependencyProperty ShowMethodTypeProperty;
        static CustomShowUserControl() {
            Type ownerType = typeof(CustomShowUserControl);
            ShowMethodTypeProperty = DependencyProperty.Register("ShowMethodType", typeof(ShowTypeBase), ownerType, new PropertyMetadata(null, (d, e) => ((CustomShowUserControl)d).RaiseShowTypeChangedChanged()));
        }
        #endregion
        bool useDelayedShow = false;

        public ShowTypeBase ShowMethodType { get { return (ShowTypeBase)GetValue(ShowMethodTypeProperty); } set { SetValue(ShowMethodTypeProperty, value); } }
        public void Show() {
            if(ShowMethodType != null) ShowMethodType.Apply(this);
            else useDelayedShow = true;
        }
        void RaiseShowTypeChangedChanged() {
            if(useDelayedShow)
                Show();
        }
    }
}
