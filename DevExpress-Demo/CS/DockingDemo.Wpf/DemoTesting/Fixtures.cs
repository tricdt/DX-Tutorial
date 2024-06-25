using System;
using System.Windows;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Bars;

namespace DockingDemo.Tests {
    #region DockingDemoModulesAccessor
    public class DockingDemoModulesAccessor : DemoModulesAccessor<DockingDemoModule> {
        public DockingDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        DockLayoutManager manager;
        public DockLayoutManager DockLayoutManager {
            get {
                if(manager == null)
                    manager = LayoutHelper.FindElement(DemoModule, Criteria) as DockLayoutManager;
                return manager;
            }
        }
        bool Criteria(FrameworkElement element) {
            return element is DevExpress.Xpf.Docking.DockLayoutManager;
        }
        public void ResetDockLayoutManager() {
            manager = null;
        }
        
        public PanelGroups RowColumnLayoutModule { get { return DemoModule as PanelGroups; } }
        public FloatingPanels FloatPanelsModule { get { return DemoModule as FloatingPanels; } }
        public Serialization SerializationModule { get { return DemoModule as Serialization; } }
        public MVVMSerialization MvvmSerializationModule { get { return DemoModule as MVVMSerialization; } }
        public DocumentGroups DocumentGroupsModule { get { return DemoModule as DocumentGroups; } }
        public DockLayout DockLayoutModule { get { return DemoModule as DockLayout; } }
    }
    #endregion DockingDemoModulesAccessor
    public abstract class BaseDockingDemoTestingFixture : BaseDemoTestingFixture {
        readonly DockingDemoModulesAccessor modulesAccessor;
        public BaseDockingDemoTestingFixture() {
            modulesAccessor = new DockingDemoModulesAccessor(this);
        }
        public DockLayoutManager DockLayoutManager { get { return modulesAccessor.DockLayoutManager; } }
        protected void ResetDockLayoutManager() {
            modulesAccessor.ResetDockLayoutManager();
        }
        
        public PanelGroups RowColumnLayoutModule { get { return modulesAccessor.RowColumnLayoutModule; } }
        public Serialization SerializationModule { get { return modulesAccessor.SerializationModule; } }
        public MVVMSerialization MvvmSerializationModule { get { return modulesAccessor.MvvmSerializationModule; } }
        public FloatingPanels FloatPanelsModule { get { return modulesAccessor.FloatPanelsModule; } }
        public DocumentGroups DocumentGroupsModule { get { return modulesAccessor.DocumentGroupsModule; } }
        public DockLayout DockLayout { get { return modulesAccessor.DockLayoutModule; } }
    }

    public class DockingCheckAllDemosFixture : CheckAllDemosFixture {
        protected override void CreateSwitchAllThemesActions() {
            base.CreateSwitchAllThemesActions();
        }
    }
    
    public class CheckDemoOptionsFixture : BaseDockingDemoTestingFixture {
        #region Initialization
        protected override void CreateActions() {
            base.CreateActions();
            
            CheckSerializationModule();
            CheckMvvmSerializationModule();
            CheckDocumentGroupsModule();
            CheckRowColumnLayoutModule();
            CreateSetCurrentDemoActions(null, false);
        }
        void CheckRowColumnLayoutModule() {
            AddLoadModuleActions(typeof(PanelGroups));
            Dispatch(RowColumnLayoutTest);
        }
        void CheckSerializationModule() {
            AddLoadModuleActions(typeof(Serialization));
            Dispatch(SerializationTest);
        }
        void CheckMvvmSerializationModule() {
            AddLoadModuleActions(typeof(MVVMSerialization));
            Dispatch(MVVMSerializationTest);
        }
        void CheckDocumentGroupsModule() {
            AddLoadModuleActions(typeof(DocumentGroups));
            Dispatch(DocumentGroupsTest);
        }
        void CheckIDEWorkspacesModule() {
            AddLoadModuleActions(typeof(DockLayout));
            Dispatch(IDEWorkspacesTest);
        }
        #endregion Initialization
        void IDEWorkspacesTest() {
            
            DocumentPanel document1 = DockLayout.DemoDockContainer.GetItem("document1") as DocumentPanel;
            Assert.IsNotNull(document1);
            FloatGroup floatGroup = DockLayout.DemoDockContainer.DockController.Float(document1);
            UpdateLayoutAndDoEvents();
            DockLayout.DemoDockContainer.DockController.Hide(floatGroup);
            UpdateLayoutAndDoEvents();
            DockLayout.DemoDockContainer.DockController.Close(floatGroup);
            UpdateLayoutAndDoEvents();
            WorkspaceManager.GetWorkspaceManager(DockLayout.DemoDockContainer).ApplyWorkspace("workspace2");
            UpdateLayoutAndDoEvents();
            return;
        }
        void RowColumnLayoutTest() {
            ResetDockLayoutManager();

            Assert.AreEqual(3, DockLayoutManager.LayoutRoot.Items.Count);
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation == System.Windows.Controls.Orientation.Horizontal);
            Assert.IsTrue(RowColumnLayoutModule.allowSplittersCheck.IsChecked.Value);
            Assert.AreEqual(0, RowColumnLayoutModule.orientationListBox.SelectedIndex);
            
            Assert.IsTrue(DockLayoutManager.LayoutRoot.IsSplittersEnabled);
            EditorsActions.ToggleCheckEdit(RowColumnLayoutModule.allowSplittersCheck);
            UpdateLayoutAndDoEvents();
            Assert.IsFalse(DockLayoutManager.LayoutRoot.IsSplittersEnabled);

            EditorsActions.ToggleCheckEdit(RowColumnLayoutModule.allowSplittersCheck);
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(DockLayoutManager.LayoutRoot.IsSplittersEnabled);

            RowColumnLayoutModule.orientationListBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(1, RowColumnLayoutModule.orientationListBox.SelectedIndex);
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation == System.Windows.Controls.Orientation.Vertical);

            RowColumnLayoutModule.orientationListBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(0, RowColumnLayoutModule.orientationListBox.SelectedIndex);
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation == System.Windows.Controls.Orientation.Horizontal);
        }
        void SerializationTest() {
            ResetDockLayoutManager();

            LayoutGroup root = DockLayoutManager.LayoutRoot;

            Assert.AreEqual(3, root.Items.Count);
            Assert.IsTrue(root.Items[0] is LayoutGroup);
            Assert.IsTrue(root.Items[1] is LayoutPanel);
            Assert.IsTrue(root.Items[2] is LayoutGroup);

            LayoutPanel panel1 = ((LayoutGroup)root.Items[0]).Items[0] as LayoutPanel;
            LayoutPanel panel2 = ((LayoutGroup)root.Items[0]).Items[1] as LayoutPanel;
            LayoutPanel panel3 = ((LayoutGroup)root.Items[2]).Items[0] as LayoutPanel;
            LayoutPanel panel4 = ((LayoutGroup)root.Items[2]).Items[1] as LayoutPanel;

            Assert.IsTrue(SerializationModule.serializeButton.IsEnabled);
            Assert.IsFalse(SerializationModule.deserializeButton.IsEnabled);
            Assert.AreEqual(SerializationModule.layoutSampleName.Items.Count, 4);
            Assert.AreEqual(SerializationModule.layoutSampleName.SelectedIndex, 0);

            SerializationModule.layoutSampleName.SelectedIndex = 1;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            SerializationModule.layoutSampleName.SelectedIndex = 2;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            SerializationModule.layoutSampleName.SelectedIndex = 3;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            SerializationModule.layoutSampleName.SelectedIndex = 0;
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();

            UIAutomationActions.ClickButton(SerializationModule.serializeButton);
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(SerializationModule.deserializeButton.IsEnabled);
            Assert.IsFalse(panel1.IsAutoHidden);
            DockLayoutManager.DockController.Hide(panel1);
            Assert.IsTrue(panel1.IsAutoHidden);
            UIAutomationActions.ClickButton(SerializationModule.deserializeButton);
            UpdateLayoutAndDoEvents();
            Assert.IsFalse(panel1.IsAutoHidden);
        }
        void MVVMSerializationTest() {
            ResetDockLayoutManager();

            LayoutGroup root = DockLayoutManager.LayoutRoot;

            Assert.AreEqual(1, root.Items.Count);
            Assert.IsTrue(root.Items[0] is LayoutGroup);

            Assert.AreEqual(5, ((LayoutGroup)root.Items[0]).Items.Count);

            LayoutPanel panel1 = ((LayoutGroup)root.Items[0]).Items[0] as LayoutPanel;
            LayoutPanel panel2 = ((LayoutGroup)root.Items[0]).Items[1] as LayoutPanel;
            LayoutPanel panel3 = ((LayoutGroup)root.Items[0]).Items[2] as LayoutPanel;
            LayoutPanel panel4 = ((LayoutGroup)root.Items[0]).Items[3] as LayoutPanel;
            LayoutPanel panel5 = ((LayoutGroup)root.Items[0]).Items[3] as LayoutPanel;

            Assert.IsNotNull(panel1);
            Assert.IsNotNull(panel2);
            Assert.IsNotNull(panel3);
            Assert.IsNotNull(panel4);
            Assert.IsNotNull(panel5);

            Assert.IsTrue(MvvmSerializationModule.serializeButton.IsEnabled, "1");
            Assert.IsFalse(MvvmSerializationModule.deserializeButton.IsEnabled, "2");
            Assert.AreEqual(MvvmSerializationModule.layoutSampleName.Items.Count, 3, "3");
            Assert.AreEqual(MvvmSerializationModule.layoutSampleName.SelectedIndex, 0, "4");

            for(int i = 0; i < 5; i++) { 
                MouseActions.ClickElement(MvvmSerializationModule.serializeButton);
                System.Windows.Input.CommandManager.InvalidateRequerySuggested();
                UpdateLayoutAndDoEvents();
                if(MvvmSerializationModule.deserializeButton.IsEnabled) break;
            }
            Assert.IsTrue(MvvmSerializationModule.deserializeButton.IsEnabled, "5");


            MvvmSerializationModule.layoutSampleName.SelectedIndex = 1;
            UIAutomationActions.ClickButton(MvvmSerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            MvvmSerializationModule.layoutSampleName.SelectedIndex = 2;
            UIAutomationActions.ClickButton(MvvmSerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();
            MvvmSerializationModule.layoutSampleName.SelectedIndex = 0;
            UIAutomationActions.ClickButton(MvvmSerializationModule.loadSampleLayoutButton);
            UpdateLayoutAndDoEvents();

            MouseActions.ClickElement(MvvmSerializationModule.deserializeButton);
            UpdateLayoutAndDoEvents();

            root = DockLayoutManager.LayoutRoot;
            Assert.AreEqual(1, root.Items.Count, "6");
            Assert.IsTrue(root.Items[0] is LayoutGroup, "7");
            Assert.AreEqual(5, ((LayoutGroup)root.Items[0]).Items.Count, "8");

            panel1 = ((LayoutGroup)root.Items[0]).Items[0] as LayoutPanel;
            panel2 = ((LayoutGroup)root.Items[0]).Items[1] as LayoutPanel;
            panel3 = ((LayoutGroup)root.Items[0]).Items[2] as LayoutPanel;
            panel4 = ((LayoutGroup)root.Items[0]).Items[3] as LayoutPanel;
            panel5 = ((LayoutGroup)root.Items[0]).Items[3] as LayoutPanel;

            Assert.IsNotNull(panel1, "9");
            Assert.IsNotNull(panel2, "10");
            Assert.IsNotNull(panel3, "11");
            Assert.IsNotNull(panel4, "12");
            Assert.IsNotNull(panel5, "13");
        }
        void DocumentGroupsTest() {
            ResetDockLayoutManager();
            Assert.AreEqual(DockLayoutManager.LayoutRoot.Items[0], DocumentGroupsModule.documentContainer);

            DocumentGroup dGroup = DocumentGroupsModule.documentContainer;

            Assert.IsFalse(DocumentGroupsModule.fixedHeader.IsEnabled);
            Assert.IsFalse(DocumentGroupsModule.fixedHeader.IsChecked.Value);

            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.Default);
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InTabControlHeader);
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 2;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InAllTabPageHeaders);
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 3;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InActiveTabPageHeader);
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 4;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader);
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 5;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InActiveTabPageAndTabControlHeader);
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 6;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.NoWhere);
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.Default);

            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Horizontal);
            DocumentGroupsModule.headerOrientationComboBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Vertical);
            DocumentGroupsModule.headerOrientationComboBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Horizontal);

            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Default);
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Left);
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 2;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Top);
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 3;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Right);
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 4;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Bottom);
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Default);

            Assert.IsFalse(DocumentGroupsModule.headersAutoFill.IsChecked.Value);
            Assert.IsFalse(dGroup.TabHeadersAutoFill);
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll);
            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 1;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Trim);
            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 2;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll);
            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 3;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.MultiLine);
            Assert.IsFalse(DocumentGroupsModule.headersAutoFill.IsChecked.Value);
            Assert.IsFalse(dGroup.TabHeadersAutoFill);
            Assert.IsTrue(DocumentGroupsModule.fixedHeader.IsEnabled);

            DocumentGroupsModule.headersAutoFill.IsChecked = true;
            Assert.IsTrue(dGroup.TabHeadersAutoFill);
            UpdateLayoutAndDoEvents();

            DocumentGroupsModule.fixedHeader.IsChecked = true;
            Assert.IsTrue(dGroup.FixedMultiLineTabHeaders);
            UpdateLayoutAndDoEvents();

            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 0;
            UpdateLayoutAndDoEvents();
            Assert.IsTrue(DocumentGroupsModule.headersAutoFill.IsChecked.Value);
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll);
            Assert.IsTrue(dGroup.TabHeadersAutoFill);
        }
    }
    
    
}
