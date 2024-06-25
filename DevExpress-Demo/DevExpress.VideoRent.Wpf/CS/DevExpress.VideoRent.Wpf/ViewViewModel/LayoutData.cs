using System;
using System.Windows;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.VideoRent.Wpf.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Xpo;
using System.Collections.Generic;

namespace DevExpress.VideoRent.Wpf {
    public class WpfLayoutData : LayoutData {
        public static readonly Theme DefaultApplicationTheme = Theme.Office2013;
        static WpfLayoutData() {
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
        }
        public static WpfLayoutData GetLayoutData() {
            XPCollection<WpfLayoutData> layoutDataCollection = LayoutManager.Current.GetLayoutData<WpfLayoutData>();
            if(layoutDataCollection.Count != 0) return layoutDataCollection[0];
            WpfLayoutData layoutData = new WpfLayoutData(VideoRentCurrentUser.GetCurrentUser(LayoutManager.Current.Session));
            LayoutManager.Current.LayoutData.Add(layoutData);
            return layoutData;
        }
        static void OnLayoutManagerBeforeSave(object sender, EventArgs e) {
            GetLayoutData().ApplicationThemeName = ApplicationThemeHelper.ApplicationThemeName;
        }
        static void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            ApplicationThemeHelper.ApplicationThemeName = GetLayoutData().ApplicationThemeName;
        }
        string applicationThemeName;
        bool saveDetailViewLayouts;
        bool saveGridControlsLayouts;
        bool saveDockLayoutManagersLayouts;
        bool saveAllLayouts;
        bool saveLayoutControlsLayouts;

        public WpfLayoutData(Session session) : base(session) { }
        public WpfLayoutData(Employee employee)
            : this(employee.Session) {
            Employee = employee;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            ApplicationThemeName = DefaultApplicationTheme.Name;
            SaveAllLayouts = true;
            SaveDetailViewLayouts = true;
            SaveGridControlsLayouts = true;
            SaveDockLayoutManagersLayouts = true;
            SaveLayoutControlsLayouts = true;
        }
        public string ApplicationThemeName {
            get { return applicationThemeName; }
            set { SetPropertyValue<string>("ApplicationThemeName", ref applicationThemeName, value); }
        }
        public bool SaveAllLayouts {
            get { return saveAllLayouts; }
            set { SetPropertyValue<bool>("SaveAllLayouts", ref saveAllLayouts, value); }
        }
        public bool SaveDetailViewLayouts {
            get { return saveDetailViewLayouts; }
            set { SetPropertyValue<bool>("SaveDetailViewLayouts", ref saveDetailViewLayouts, value); }
        }
        public bool SaveGridControlsLayouts {
            get { return saveGridControlsLayouts; }
            set { SetPropertyValue<bool>("SaveGridControlsLayouts", ref saveGridControlsLayouts, value); }
        }
        public bool SaveDockLayoutManagersLayouts {
            get { return saveDockLayoutManagersLayouts; }
            set { SetPropertyValue<bool>("SaveDockLayoutManagersLayouts", ref saveDockLayoutManagersLayouts, value); }
        }
        public bool SaveLayoutControlsLayouts {
            get { return saveLayoutControlsLayouts; }
            set { SetPropertyValue<bool>("SaveLayoutControlsLayouts", ref saveLayoutControlsLayouts, value); }
        }
    }
    public class ElementLayoutData : LayoutData {
        public static ElementLayoutData GetLayoutData(string folderName) {
            XPCollection<ElementLayoutData> layoutDataCollection = LayoutManager.Current.GetLayoutData<ElementLayoutData>();
            layoutDataCollection.Filter = CriteriaOperator.Parse("FolderName = ?", folderName);
            if(layoutDataCollection.Count != 0) return layoutDataCollection[0];
            ElementLayoutData layoutData = new ElementLayoutData(VideoRentCurrentUser.GetCurrentUser(LayoutManager.Current.Session), folderName);
            LayoutManager.Current.LayoutData.Add(layoutData);
            return layoutData;
        }
        string folderName;
        byte[] data;

        public ElementLayoutData(Session session) : base(session) { }
        public ElementLayoutData(Employee employee, string folderName)
            : this(employee.Session) {
            Employee = employee;
            FolderName = folderName;
        }
        [Size(SizeAttribute.Unlimited)]
        //[Indexed("Employee", Name="EmployeeFolderName", Unique=true)] it is not supported by XPO
        public string FolderName {
            get { return folderName; }
            set { SetPropertyValue<string>("FolderName", ref folderName, value); }
        }
        public byte[] Data {
            get { return data; }
            set { SetPropertyValue<byte[]>("Data", ref data, value); }
        }
    }
    public class ElementLayoutDataStore : DisposableFrameworkElement, ILayoutDataStore {
        class SaveLoadHandlers {
            public SaveLoadHandlers(SaveLoadLayoutDataEventHandler saveHandler, SaveLoadLayoutDataEventHandler loadHandler) {
                SaveHandler = saveHandler;
                LoadHandler = loadHandler;
            }
            public SaveLoadLayoutDataEventHandler SaveHandler { get; private set; }
            public SaveLoadLayoutDataEventHandler LoadHandler { get; private set; }
            public override bool Equals(object obj) {
                SaveLoadHandlers handlers = obj as SaveLoadHandlers;
                if(handlers == null) return false;
                return object.Equals(SaveHandler, handlers.SaveHandler) && object.Equals(LoadHandler, handlers.LoadHandler);
            }
            public override int GetHashCode() {
                return SaveHandler.GetHashCode() + LoadHandler.GetHashCode();
            }
            public static bool operator ==(SaveLoadHandlers h1, SaveLoadHandlers h2) {
                bool h1IsNull = (object)h1 == null;
                bool h2IsNull = (object)h2 == null;
                if(h1IsNull && h2IsNull) return true;
                if(h1IsNull || h2IsNull) return false;
                return h1.SaveHandler == h2.SaveHandler && h1.LoadHandler == h2.LoadHandler;
            }
            public static bool operator !=(SaveLoadHandlers h1, SaveLoadHandlers h2) {
                return !(h1 == h2);
            }
        }
        List<SaveLoadHandlers> saveLoadHandlers = new List<SaveLoadHandlers>();

        public void Subscribe(SaveLoadLayoutDataEventHandler onAfterLoad, SaveLoadLayoutDataEventHandler onBeforeSave) {
            saveLoadHandlers.Add(new SaveLoadHandlers(onBeforeSave, onAfterLoad));
            LayoutManager.Current.Subscribe(onAfterLoad, onBeforeSave);
        }
        public void Unsubscribe(SaveLoadLayoutDataEventHandler onAfterLoad, SaveLoadLayoutDataEventHandler onBeforeSave) {
            LayoutManager.Current.Unsubscribe(onAfterLoad, onBeforeSave);
            saveLoadHandlers.Remove(new SaveLoadHandlers(onBeforeSave, onAfterLoad));
        }
        public void WriteLayoutData(string folderName, byte[] data, bool clearing) {
            if(clearing || !AllowSaveOrRestore(folderName)) return;
            ElementLayoutData.GetLayoutData(folderName).Data = data;
        }
        public byte[] ReadLayoutData(string folderName, bool clearing) {
            if(!clearing && !AllowSaveOrRestore(folderName)) return null;
            return ElementLayoutData.GetLayoutData(folderName).Data;
        }
        protected override void DisposeManaged() {
            foreach(SaveLoadHandlers handlers in saveLoadHandlers)
                LayoutManager.Current.Unsubscribe(handlers.LoadHandler, handlers.SaveHandler);
            saveLoadHandlers.Clear();
            base.DisposeManaged();
        }
        static bool AllowSaveOrRestore(string folderName) {
            if(!SaveLayoutOptionsProvider.Current.SaveAllLayouts) return false;
            if(!SaveLayoutOptionsProvider.Current.SaveDetailViewLayouts && IsFolderOfControlType(folderName, typeof(ThemedWindow))) return false;
            if(!SaveLayoutOptionsProvider.Current.SaveGridControlsLayouts && IsFolderOfControlType(folderName, typeof(GridControl))) return false;
            if(!SaveLayoutOptionsProvider.Current.SaveDockLayoutManagersLayouts && IsFolderOfControlType(folderName, typeof(DockLayoutManager))) return false;
            if(!SaveLayoutOptionsProvider.Current.SaveLayoutControlsLayouts && IsFolderOfControlType(folderName, typeof(LayoutControl))) return false;
            return true;
        }
        static bool IsFolderOfControlType(string folderName, Type controlType) {
            return folderName.IndexOf(controlType.Name + ":") == 0;
        }
    }
    public class SaveLayoutOptionsProvider : BindingAndDisposable {
        static SaveLayoutOptionsProvider current;
        public static SaveLayoutOptionsProvider Current {
            get {
                if(current == null)
                    current = new SaveLayoutOptionsProvider();
                return current;
            }
        }
        bool saveAllLayouts;
        bool saveDetailViewLayouts;
        bool saveGridControlsLayouts;
        bool saveDockLayoutManagersLayouts;
        bool saveLayoutControlsLayouts;

        public SaveLayoutOptionsProvider() {
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
        }
        public bool SaveAllLayouts {
            get { return saveAllLayouts; }
            set { SetValue<bool>("SaveAllLayouts", ref saveAllLayouts, value); }
        }
        public bool SaveDetailViewLayouts {
            get { return saveDetailViewLayouts; }
            set { SetValue<bool>("SaveDetailViewLayouts", ref saveDetailViewLayouts, value); }
        }
        public bool SaveGridControlsLayouts {
            get { return saveGridControlsLayouts; }
            set { SetValue<bool>("SaveGridControlsLayouts", ref saveGridControlsLayouts, value); }
        }
        public bool SaveDockLayoutManagersLayouts {
            get { return saveDockLayoutManagersLayouts; }
            set { SetValue<bool>("SaveDockLayoutManagersLayouts", ref saveDockLayoutManagersLayouts, value); }
        }
        public bool SaveLayoutControlsLayouts {
            get { return saveLayoutControlsLayouts; }
            set { SetValue<bool>("SaveLayoutControlsLayouts", ref saveLayoutControlsLayouts, value); }
        }
        protected override void DisposeManaged() {
            LayoutManager.Current.Unsubscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
            base.DisposeManaged();
        }
        void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            WpfLayoutData layoutData = WpfLayoutData.GetLayoutData();
            SaveAllLayouts = layoutData.SaveAllLayouts;
            SaveDetailViewLayouts = layoutData.SaveDetailViewLayouts;
            SaveGridControlsLayouts = layoutData.SaveGridControlsLayouts;
            SaveDockLayoutManagersLayouts = layoutData.SaveDockLayoutManagersLayouts;
            SaveLayoutControlsLayouts = layoutData.SaveLayoutControlsLayouts;
        }
        void OnLayoutManagerBeforeSave(object sender, EventArgs e) {
            WpfLayoutData layoutData = WpfLayoutData.GetLayoutData();
            layoutData.SaveAllLayouts = SaveAllLayouts;
            layoutData.SaveDetailViewLayouts = SaveDetailViewLayouts;
            layoutData.SaveGridControlsLayouts = SaveGridControlsLayouts;
            layoutData.SaveDockLayoutManagersLayouts = SaveDockLayoutManagersLayouts;
            layoutData.SaveLayoutControlsLayouts = SaveLayoutControlsLayouts;
        }
    }
}
