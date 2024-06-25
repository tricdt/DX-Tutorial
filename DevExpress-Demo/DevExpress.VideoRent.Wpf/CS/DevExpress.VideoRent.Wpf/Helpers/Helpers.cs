using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Xpo;
using System.Collections;
using DevExpress.VideoRent.Resources.Helpers;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public abstract class DisposableFrameworkElement : FrameworkElement, IDisposable {
        #region Dependency Properties
        public static readonly DependencyProperty DisposeSignalSlotProperty;
        static DisposableFrameworkElement() {
            Type ownerType = typeof(DisposableFrameworkElement);
            DisposeSignalSlotProperty = DependencyProperty.Register("DisposeSignalSlot", typeof(bool), ownerType, new PropertyMetadata(false,
                (d, e) => ((DisposableFrameworkElement)d).RaiseDisposeSignalSlotChanged(e)));
        }
        #endregion
        bool disposed = false;

        ~DisposableFrameworkElement() { Dispose(false); }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public bool Disposed { get { return disposed; } }
        public event EventHandler AfterDispose;
        protected virtual void DisposeManaged() { }
        protected virtual void DisposeUnmanaged() { }
        void Dispose(bool disposing) {
            if(Disposed) return;
            disposed = true;
            if(disposing)
                DisposeManaged();
            DisposeUnmanaged();
            if(disposing)
                RaiseAfterDispose();
        }
        void RaiseAfterDispose() {
            if(AfterDispose != null)
                AfterDispose(this, EventArgs.Empty);
            AfterDispose = null;
        }
        void RaiseDisposeSignalSlotChanged(DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) return;
            Dispose();
        }
    }
    public static class ResourceDictionaryLoader {
        static Dictionary<Uri, ResourceDictionary> dictionaries = new Dictionary<Uri, ResourceDictionary>();

        public static ResourceDictionary Load(Assembly assembly, string path) {
            Uri uri = AssemblyHelper.GetResourceUri(assembly, path);
            ResourceDictionary rd;
            if(!dictionaries.TryGetValue(uri, out rd)) {
                rd = new ResourceDictionary() { Source = uri };
                dictionaries.Add(uri, rd);
            }
            return rd;
        }
    }
    public static class LayoutControlExtensions {
        public static void FlipLayout(this LayoutGroup group) {
            UIElement[] groupChildren = new UIElement[group.Children.Count];
            group.Children.CopyTo(groupChildren, 0);
            group.Children.Clear();
            for(int i = groupChildren.Length - 1; i >= 0; --i) {
                group.Children.Add(groupChildren[i]);
            }
        }
    }
    public static class ReflectionHelper {
        const string NullString = "<Null>";

        public static string ObjectToString(object obj) {
            if(obj == null) return NullString;
            Type objAsType = obj as Type;
            if(objAsType != null) {
                return CombineStrings(objAsType.Assembly.FullName, objAsType.FullName);
            } else {
                Type type = obj.GetType();
                return CombineStrings(type.Assembly.FullName, type.FullName, obj.ToString());
            }
        }
        public static object ObjectFromString(string s) {
            if(s == NullString) return null;
            string[] parts = SplitString(s);
            if(parts.Length != 2 && parts.Length != 3) return null;
            Assembly assembly = AssemblyHelper.GetAssembly(parts[0]);
            if(assembly == null) return null;
            Type type = assembly.GetType(parts[1]);
            if(type == null) return null;
            if(parts.Length == 2) return type;
            return ParseValue(parts[2], type);
        }
        public static string CombineStrings(params string[] parts) {
            string s = string.Empty;
            foreach(string part in parts) {
                s += "_x_" + part.ToString().Replace("\\", "\\\\").Replace("x", "\\x");
            }
            return s.Length == 0 ? string.Empty : s.Substring(3);
        }
        public static string[] SplitString(string s) {
            if(string.IsNullOrEmpty(s)) return new string[] { };
            string[] parts = s.Split(new string[] { "_x_" }, StringSplitOptions.None);
            string[] sourceParts = new string[parts.Length];
            for(int i = 0; i < parts.Length; ++i) {
                string part = parts[i];
                string sourcePart = string.Empty;
                bool bs = false;
                for(int c = 0; c < part.Length; ++c) {
                    if(part[c] == '\\') {
                        if(bs) {
                            bs = false;
                            sourcePart += "\\";
                        } else {
                            bs = true;
                        }
                    } else {
                        bs = false;
                        sourcePart += part[c];
                    }
                }
                sourceParts[i] = sourcePart;
            }
            return sourceParts;
        }
        static object ParseValue(string valueStringPresentation, Type valueType) {
            if(valueType == typeof(string)) return valueStringPresentation;
            if(valueType.BaseType == typeof(Enum)) return Enum.Parse(valueType, valueStringPresentation);
            MethodInfo parseMethod = valueType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
            return parseMethod.Invoke(null, new object[] { valueStringPresentation });
        }
    }
    public class StringIdGenerator {
        int nextFree = 0;
        SortedSet<int> releases = new SortedSet<int>();
        AutoResetEvent releasesUnlock = new AutoResetEvent(true); // mutex, protect nextFree and releases
        public string Get() {
            releasesUnlock.WaitOne();
            int ret;
            if(releases.Count != 0) {
                ret = releases.Min;
                releases.Remove(ret);
            } else {
                ret = nextFree++;
            }
            releasesUnlock.Set();
            return string.Format("_{0}", ret);
        }
        public void Release(string value) {
            releasesUnlock.WaitOne();
            int ret = int.Parse(value.Substring(1));
            releases.Add(ret);
            while(releases.Contains(nextFree - 1)) {
                --nextFree;
                releases.Remove(nextFree);
            }
            releasesUnlock.Set();
        }
    }
    public class DataSource : DependencyObject {
        #region Dependency Properties
        public static readonly DependencyProperty DataObjectProperty;
        static DataSource() {
            Type ownerType = typeof(DataSource);
            DataObjectProperty = DependencyProperty.Register("DataObject", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public object DataObject { get { return GetValue(DataObjectProperty); } set { SetValue(DataObjectProperty, value); } }
    }
    public class ImageSourceProvider : DependencyObject {
        #region Dependency Properties
        public static readonly DependencyProperty ImageSourceProperty;
        static ImageSourceProvider() {
            Type ownerType = typeof(ImageSourceProvider);
            ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public ImageSource ImageSource { get { return (ImageSource)GetValue(ImageSourceProperty); } set { SetValue(ImageSourceProperty, value); } }
    }
    public class ImageCollectionImageSource : ImageSourceProvider, ISupportInitialize {
        #region Dependency Properties
        public static readonly DependencyProperty ImageCollectionProperty;
        static ImageCollectionImageSource() {
            Type ownerType = typeof(ImageCollectionImageSource);
            ImageCollectionProperty = DependencyProperty.Register("ImageCollection", typeof(List<ImageSource>), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public List<ImageSource> ImageCollection { get { return (List<ImageSource>)GetValue(ImageCollectionProperty); } set { SetValue(ImageCollectionProperty, value); } }
        public int ImageIndex { get; set; }
        public virtual void BeginInit() { }
        public virtual void EndInit() {
            ImageSource = ImageCollection == null ? null : ImageCollection[ImageIndex];
        }
    }
    public class TabbedGroupSignalHelper {
        #region Dependency Properties
        public static readonly DependencyProperty SelectIndexSignalSlotProperty;
        public static readonly DependencyProperty IndexToSelectProperty;
        static TabbedGroupSignalHelper() {
            Type ownerType = typeof(TabbedGroupSignalHelper);
            SelectIndexSignalSlotProperty = DependencyProperty.RegisterAttached("SelectIndexSignalSlot", typeof(bool), ownerType, new PropertyMetadata(false, RaiseSelectIndexSignalSlotChanged));
            IndexToSelectProperty = DependencyProperty.RegisterAttached("IndexToSelect", typeof(int), ownerType, new PropertyMetadata(0));
        }
        #endregion
        public static bool GetSelectIndexSignalSlot(DependencyObject d) { return (bool)d.GetValue(SelectIndexSignalSlotProperty); }
        public static void SetSelectIndexSignalSlot(DependencyObject d, bool value) { d.SetValue(SelectIndexSignalSlotProperty, value); }
        public static int GetIndexToSelect(DependencyObject d) { return (int)d.GetValue(IndexToSelectProperty); }
        public static void SetIndexToSelect(DependencyObject d, int value) { d.SetValue(IndexToSelectProperty, value); }
        static void RaiseSelectIndexSignalSlotChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) return;
            LayoutGroup tabsGroup = d as LayoutGroup;
            if(tabsGroup == null) return;
            tabsGroup.SelectedTabIndex = GetIndexToSelect(d);
        }
    }
    public class LayoutControlSignalHelper {
        #region Dependency Properties
        public static readonly DependencyProperty FlipLayoutSignalSlotProperty;
        static LayoutControlSignalHelper() {
            Type ownerType = typeof(LayoutControlSignalHelper);
            FlipLayoutSignalSlotProperty = DependencyProperty.RegisterAttached("FlipLayoutSignalSlot", typeof(bool), ownerType, new PropertyMetadata(false, RaiseFlipLayoutSignalSlotChanged));
        }
        #endregion
        public static bool GetFlipLayoutSignalSlot(DependencyObject d) { return (bool)d.GetValue(FlipLayoutSignalSlotProperty); }
        public static void SetFlipLayoutSignalSlot(DependencyObject d, bool value) { d.SetValue(FlipLayoutSignalSlotProperty, value); }
        static void RaiseFlipLayoutSignalSlotChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) return;
            LayoutControl control = d as LayoutControl;
            if(control == null) return;
            ((LayoutGroup)control.Root).FlipLayout();
        }
    }
    public class GridControlSignalHelper {
        #region Dependency Properties
        public static readonly DependencyProperty RefreshDataSignalSlotProperty;
        static GridControlSignalHelper() {
            Type ownerType = typeof(GridControlSignalHelper);
            RefreshDataSignalSlotProperty = DependencyProperty.RegisterAttached("RefreshDataSignalSlot", typeof(bool), ownerType, new PropertyMetadata(false, RaiseRefreshDataSignalSlotChanged));
        }
        #endregion
        public static bool GetRefreshDataSignalSlot(DependencyObject d) { return (bool)d.GetValue(RefreshDataSignalSlotProperty); }
        public static void SetRefreshDataSignalSlot(DependencyObject d, bool value) { d.SetValue(RefreshDataSignalSlotProperty, value); }
        static void RaiseRefreshDataSignalSlotChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) return;
            GridControl control = d as GridControl;
            if(control != null)
                control.RefreshData();
        }
    }
    public class ImmediateCellValueSavingHelper {
        #region Dependency Properties
        public static readonly DependencyProperty FieldNameProperty;
        static readonly DependencyProperty FieldNamesProperty;
        static ImmediateCellValueSavingHelper() {
            Type ownerType = typeof(ImmediateCellValueSavingHelper);
            FieldNameProperty = DependencyProperty.RegisterAttached("FieldName", typeof(string), ownerType, new PropertyMetadata(null, RaiseFieldNameChanged));
            FieldNamesProperty = DependencyProperty.RegisterAttached("FieldNames", typeof(List<string>), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public static string GetFieldName(GridViewBase d) { return (string)d.GetValue(FieldNameProperty); }
        public static void SetFieldName(GridViewBase d, string value) { d.SetValue(FieldNameProperty, value); }
        static List<string> GetFieldNames(GridViewBase d) { return (List<string>)d.GetValue(FieldNamesProperty); }
        static void SetFieldNames(GridViewBase d, List<string> value) { d.SetValue(FieldNamesProperty, value); }
        static void RaiseFieldNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            GridViewBase view = (GridViewBase)d;
            string fieldName = (string)e.NewValue;
            view.CellValueChanging -= OnCellValueChanged;
            if(fieldName == null) return;
            List<string> fields = fieldName.Length == 0 ? new List<string>() : new List<string>(fieldName.Split(','));
            SetFieldNames(view, fields);
            view.CellValueChanging += OnCellValueChanged;
        }
        static void OnCellValueChanged(object sender, CellValueChangedEventArgs e) {
            GridViewBase view = (GridViewBase)sender;
            List<string> fieldNames = GetFieldNames(view);
            if(fieldNames.Count == 0 || fieldNames.Contains(e.Column.FieldName)) {
                view.CommitEditing();
            }
        }
    }
    public class FocusedRowRestoringHelper : DependencyObject {
        public static readonly DependencyProperty ItemsSourceInterfaceProperty;
        public static readonly DependencyProperty ViewInterfaceProperty;
        public static readonly DependencyProperty NavigationStyleInterfaceProperty;
        static readonly DependencyProperty ItemsSourceProperty;
        static readonly DependencyProperty FocusedRowProperty;
        static readonly DependencyProperty ViewProperty;
        static readonly DependencyProperty NavigationStyleProperty;
        static Dictionary<DependencyProperty, DependencyProperty> interfaces;
        static Dictionary<DependencyProperty, PropertyInfo> reflections;
        static FocusedRowRestoringHelper() {
            Type ownerType = typeof(FocusedRowRestoringHelper);
            ItemsSourceProperty = DependencyProperty.RegisterAttached("ItemsSource", typeof(object), ownerType, new PropertyMetadata(null, OnPropertyNeedsToBeReloadedChangedChanged));
            ItemsSourceInterfaceProperty = DependencyProperty.RegisterAttached("ItemsSourceInterface", typeof(object), ownerType, new PropertyMetadata(null, OnInterfacePropertyChanged<GridControl>));
            FocusedRowProperty = DependencyProperty.RegisterAttached("FocusedRow", typeof(object), ownerType, new PropertyMetadata(null));
            ViewProperty = DependencyProperty.RegisterAttached("View", typeof(object), ownerType, new PropertyMetadata(null, OnSimplePropertyChanged));
            ViewInterfaceProperty = DependencyProperty.RegisterAttached("ViewInterface", typeof(object), ownerType, new PropertyMetadata(null, OnInterfacePropertyChanged<GridControl>));
            NavigationStyleProperty = DependencyProperty.RegisterAttached("NavigationStyle", typeof(object), ownerType, new PropertyMetadata(null, OnSimplePropertyChanged));
            NavigationStyleInterfaceProperty = DependencyProperty.RegisterAttached("NavigationStyleInterface", typeof(object), ownerType, new PropertyMetadata(null, OnInterfacePropertyChanged<GridViewBase>));
            interfaces = new Dictionary<DependencyProperty, DependencyProperty>();
            interfaces.Add(ItemsSourceInterfaceProperty, ItemsSourceProperty);
            interfaces.Add(NavigationStyleInterfaceProperty, NavigationStyleProperty);
            interfaces.Add(ViewInterfaceProperty, ViewProperty);
            reflections = new Dictionary<DependencyProperty, PropertyInfo>();
        }
        public static void SetItemsSourceInterface(DependencyObject element, object value) { element.SetValue(ItemsSourceInterfaceProperty, value); }
        public static object GetItemsSourceInterface(DependencyObject element) { return (object)element.GetValue(ItemsSourceInterfaceProperty); }
        public static void SetViewInterface(DependencyObject element, object value) { element.SetValue(ViewInterfaceProperty, value); }
        public static object GetViewInterface(DependencyObject element) { return (object)element.GetValue(ViewInterfaceProperty); }
        public static void SetNavigationStyleInterface(DependencyObject element, object value) { element.SetValue(NavigationStyleInterfaceProperty, value); }
        public static object GetNavigationStyleInterface(DependencyObject element) { return (object)element.GetValue(NavigationStyleInterfaceProperty); }
        static void SetItemsSource(DependencyObject element, object value) { element.SetValue(ItemsSourceProperty, value); }
        static object GetItemsSource(DependencyObject element) { return (object)element.GetValue(ItemsSourceProperty); }
        static void SetFocusedRow(DependencyObject element, object value) { element.SetValue(FocusedRowProperty, value); }
        static object GetFocusedRow(DependencyObject element) { return (object)element.GetValue(FocusedRowProperty); }
        static void SetView(DependencyObject element, object value) { element.SetValue(ViewProperty, value); }
        static object GetView(DependencyObject element) { return (object)element.GetValue(ViewProperty); }
        static void SetNavigationStyle(DependencyObject element, object value) { element.SetValue(NavigationStyleProperty, value); }
        static object GetNavigationStyle(DependencyObject element) { return (object)element.GetValue(NavigationStyleProperty); }
        static void OnInterfacePropertyChanged<T>(DependencyObject d, DependencyPropertyChangedEventArgs e) where T : DependencyObject {
            T neededTypeObject = d as T;
            if(neededTypeObject == null) return;
            UpdateFocusedRowValue(neededTypeObject);
            if(neededTypeObject.GetValue(interfaces[e.Property]) == null) {
                DependencyProperty property = interfaces[e.Property];
                BindingOperations.SetBinding(neededTypeObject, property, new Binding(property.Name) { Source = neededTypeObject });
            }
            PropertyInfo gridPropertyInfo;
            DependencyProperty propertyBytInterface = interfaces[e.Property];
            if(!reflections.TryGetValue(propertyBytInterface, out gridPropertyInfo)) {
                gridPropertyInfo = d.GetType().GetProperty(propertyBytInterface.Name);
                reflections.Add(propertyBytInterface, gridPropertyInfo);
            }
            if(gridPropertyInfo != null) {
                gridPropertyInfo.SetValue(neededTypeObject, e.NewValue, null);
            }
        }
        static void OnSimplePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            RestoreFocusedRow(d as GridControl);
            RestoreFocusedRow(d as GridViewBase);
        }
        static void OnPropertyNeedsToBeReloadedChangedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            GridControl grid = d as GridControl;
            if(grid == null) return;
            RestoreFocusedRow((GridViewBase)grid.View, true);
        }
        static void RestoreFocusedRow(GridViewBase view) {
            RestoreFocusedRow(view, false);
        }
        static void RestoreFocusedRow(GridControl grid) {
            if(grid == null) return;
            RestoreFocusedRow((GridViewBase)grid.View, false);
        }
        static void RestoreFocusedRow(GridViewBase view, bool needRefreshValue) {
            if(view == null || view.Grid == null || view.Grid.CurrentItem == GetFocusedRow(view.Grid)) return;
            object focusedRowValue = GetFocusedRow(view.Grid);
            if(needRefreshValue && focusedRowValue is XPBaseObject && view.Grid.ItemsSource is XPBaseCollection) {
                focusedRowValue = SessionHelper.GetObject(focusedRowValue, ((XPBaseCollection)view.Grid.ItemsSource).Session);
            }
            view.Grid.CurrentItem = focusedRowValue;
            SetFocusedRow(view.Grid, focusedRowValue);
        }
        static void SetViewFocusedRow(DependencyObject d, GridViewBase view) {
            if(view != null && view.Grid != null) {
                SetFocusedRow(d, view.Grid.CurrentItem);
            }
        }
        static void UpdateFocusedRowValue(DependencyObject d) {
            try {
                PropertyInfo viewInfo = GetPropertyInfo(d, GridControl.ViewProperty);
                SetViewFocusedRow(d, viewInfo.GetValue(d, null) as GridViewBase);
            } catch(Exception) {
                GridViewBase view = d as GridViewBase;
                SetViewFocusedRow(view.Grid, view);
            }
        }
        static PropertyInfo GetPropertyInfo(object d, DependencyProperty property) {
            PropertyInfo viewInfo;
            if(!reflections.TryGetValue(property, out viewInfo)) {
                viewInfo = d.GetType().GetProperty(property.Name);
                reflections.Add(property, viewInfo);
            }
            return viewInfo;
        }
    }
    public static class GridControlExtensions {
        public static int GetRowListIndexByRow(this GridControl gridControl, object row) {
            return ((IList)gridControl.ItemsSource).IndexOf(row);
        }
    }
    public static class MouseHelper {
        public static void WaitIdle() {
            Mouse.OverrideCursor = Cursors.Wait;
            Application.Current.Dispatcher.BeginInvoke((Action)(() => { Mouse.OverrideCursor = null; }), DispatcherPriority.ApplicationIdle);
        }
    }
}
