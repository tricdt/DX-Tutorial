#if DebugTest
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using DevExpress.VideoRent.Resources.Helpers;
using DevExpress.VideoRent.Wpf.Helpers;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpo;
using NUnit.Framework;

namespace DevExpress.VideoRent.Wpf.Tests {
    [TestFixture]
    public class HelpersTests {
        [Test]
        public void DependencyPropertyHelper_SubscribeToChanged() {
            bool eventRaised = false;
            TheDependencyObject theDependencyObject = new TheDependencyObject();
            EventHandler changed = delegate(object sender, EventArgs e)
            {
                eventRaised = true;
            };
            DepPropertyHelper.SubscribeToChanged(theDependencyObject, TheDependencyObject.IntValueProperty, changed);
            theDependencyObject.IntValue = 1;
            Assert.IsTrue(eventRaised);
            DepPropertyHelper.UnsubscribeFromChanged(theDependencyObject, TheDependencyObject.IntValueProperty, changed);
            eventRaised = false;
            theDependencyObject.IntValue = 2;
            Assert.IsFalse(eventRaised);
        }
        [Test]
        public void BindingListAttacher_MoveItemInOwner() {
            bool resetIsRaised = false;
            bool removeIsRaised = false;
            ObservableCollection<object> bindingList = new ObservableCollection<object> { 1, 2, 3, 4 };
            bindingList.CollectionChanged += (s, e) =>
            {
                switch(e.Action) {
                    case NotifyCollectionChangedAction.Reset: resetIsRaised = true; break;
                    case NotifyCollectionChangedAction.Remove: removeIsRaised = true; break;
                }
            };
            TestAttachedBindingListOwner listOwner = new TestAttachedBindingListOwner();
            BindingListAttacher bla = new BindingListAttacher(listOwner);
            bla.BindingList = bindingList;
            bla.BindingListAllowItemMoving = false;
            Assert.AreEqual("ResetList", listOwner.Log);
            resetIsRaised = false;
            removeIsRaised = false;
            listOwner.MoveItem(0, 2);
            Assert.IsFalse(resetIsRaised);
            Assert.IsFalse(removeIsRaised);
        }
        [Test]
        public void BindingList_AutoidentXPCollection() {
            TestAttachedBindingListOwner listOwner = new TestAttachedBindingListOwner();
            BindingListAttacher bla = new BindingListAttacher(listOwner);
            bla.BindingList = new ObservableCollection<object>();
            Assert.IsTrue(bla.BindingListAllowItemMoving);
            bla.BindingList = new XPCollection();
            Assert.IsFalse(bla.BindingListAllowItemMoving);
        }
        [Test]
        public void ReflectionHelper_CombineStrings_SplitStrings() {
            string[] s = new string[] { "aaa", "x", "x", "\\", "\\\\", "\\x", "_x_", "_\\x_" };
            string combined = ReflectionHelper.CombineStrings(s);
            string[] splited = ReflectionHelper.SplitString(combined);
            Assert.AreEqual(s.Length, splited.Length);
            for(int i = 0; i < s.Length; ++i)
                Assert.AreEqual(s[i], splited[i]);
            Assert.AreEqual(0, ReflectionHelper.SplitString(string.Empty).Length);
        }
        [Test]
        public void FocusedRowRestoringHelper_RestoreRow_AfterDataSourceChanged() {
            string[] s = new string[] { "aaa", "x", "x", "\\", "\\\\", "\\x", "_x_", "_\\x_" };
            GridControl grid = new GridControl() { View = new TableView() };
            FocusedRowRestoringHelper.SetItemsSourceInterface(grid, new List<string>(s));
            grid.SelectedItem = s[3];
            FocusedRowRestoringHelper.SetItemsSourceInterface(grid, new List<string>(s));
            Assert.AreEqual(s[3], grid.SelectedItem);
        }
        [Test]
        public void FocusedRowRestoringHelper_RestoreFirstRow_AfterDataSourceChanged() {
            string[] s = new string[] { "aaa", "x", "x", "\\", "\\\\", "\\x", "_x_", "_\\x_" };
            GridControl grid = new GridControl() { View = new TableView() };
            FocusedRowRestoringHelper.SetItemsSourceInterface(grid, new List<string>(s));
            grid.SelectedItem = s[3];
            grid.SelectedItem = s[0];
            FocusedRowRestoringHelper.SetItemsSourceInterface(grid, new List<string>(s));
            Assert.AreEqual(s[0], grid.SelectedItem);
        }
        [Test]
        public void FocusedRowRestoringHelper_RestoreRow_AfterViewChanged() {
            string[] s = new string[] { "aaa", "x", "x", "\\", "\\\\", "\\x", "_x_", "_\\x_" };
            GridControl grid = new GridControl() { View = new TableView() };
            FocusedRowRestoringHelper.SetItemsSourceInterface(grid, new List<string>(s));
            grid.View.MoveNextRow();
            grid.View.MoveNextRow();
            FocusedRowRestoringHelper.SetViewInterface(grid,new CardView());
            Assert.AreEqual(grid.SelectedItem, s[1]);
        }
        [Test]
        public void FocusedRowRestoringHelper_RestoreRow_AfterDataSourceAndViewChanged() {
            string[] s = new string[] { "aaa", "x", "x", "\\", "\\\\", "\\x", "_x_", "_\\x_" };
            GridControl grid = new GridControl() { View = new TableView() };
            FocusedRowRestoringHelper.SetItemsSourceInterface(grid, new List<string>(s));
            grid.View.MoveNextRow();
            grid.View.MoveNextRow();
            FocusedRowRestoringHelper.SetItemsSourceInterface(grid, new List<string>(s));
            FocusedRowRestoringHelper.SetViewInterface(grid, new CardView());
            Assert.AreEqual(grid.SelectedItem, s[1]);
        }
    }
    public class TheDependencyObjectBase : DependencyObject {
        public static readonly DependencyProperty IntValueProperty;
        static TheDependencyObjectBase() {
            Type ownerType = typeof(TheDependencyObjectBase);
            IntValueProperty = DependencyProperty.Register("IntValue", typeof(int), ownerType, new PropertyMetadata(0));
        }
        public int IntValue {
            get { return (int)GetValue(IntValueProperty); }
            set { SetValue(IntValueProperty, value); }
        }
    }
    public class TheDependencyObject : TheDependencyObjectBase { }
    public class IntData {
        public int Data { get; set; }
    }
    public class TestAttachedBindingListOwner : IAttachedBindingListOwner {
        List<object> list = new List<object>();
        public TestAttachedBindingListOwner() {
            Log = string.Empty;
        }
        public string Log { get; private set; }
        public IList List { get { return list; } }
        public event NotifyCollectionChangedEventHandler ListChanged;
        public void ClearLog() {
            Log = string.Empty;
        }
        public void AddItem(int newIndex, object item) {
            Log += string.Format("AddItem {0} _{1}_", newIndex, item);
            list.Insert(newIndex, item);
            RaiseListChanged();
        }
        public void RemoveItem(int oldIndex) {
            Log += string.Format("RemoveItem {0}", oldIndex);
            list.RemoveAt(oldIndex);
            RaiseListChanged();
        }
        public void MoveItem(int oldIndex, int newIndex) {
            Log += string.Format("MoveItem {0} {1}", oldIndex, newIndex);
            object item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
            RaiseListChanged();
        }
        public void ResetList(IList newList) {
            Log += string.Format("ResetList");
            list.Clear();
            foreach(object item in newList)
                list.Add(item);
            RaiseListChanged();
        }
        void RaiseListChanged() {
            if(ListChanged != null)
                ListChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
    [TestFixture]
    public class RibbonManagerTests {
        Window window;
        BarManager barManager;
        RibbonControl ribbonControl;
        RibbonManager ribbonManager;
        RibbonDefaultPageCategory defaultCategory;

        [SetUp]
        public void Init() {
            CreateWndow();
            CreateRibbon();
            CreateRibbonManager();
        }
        [Test]
        public void AddDynamicPages_PerformClick() {
            BarItem barItem1, barItem2;
            RibbonPage page1, page2;
            RibbonPageGroup group1, group2;
            CreateDynamicPage("FirstItem", "1", out barItem1, out page1, out group1);
            CreateDynamicPage("SecondItem", "1", out barItem2, out page2, out group2);
            bool barItem1Clicked = false;
            bool barItem2Clicked = false;
            barItem1.ItemClick += (s, e) => { barItem1Clicked = true; };
            barItem2.ItemClick += (s, e) => { barItem2Clicked = true; };
            BarItemLink barItemLink1 = (BarItemLink)group1.ItemLinks[0];
            BarItemLink barItemLink2 = (BarItemLink)group2.ItemLinks[0];
            Assert.IsNotNull(barItemLink1.Item);
            Assert.AreEqual(barItemLink1.Item, barItemLink2.Item);
            this.ribbonControl.SelectedPage = page1;
            barItemLink1.Item.PerformClick();
            Assert.IsTrue(barItem1Clicked);
            Assert.IsFalse(barItem2Clicked);
            barItem1Clicked = false;
            this.ribbonControl.SelectedPage = page2;
            barItemLink2.Item.PerformClick();
            Assert.IsFalse(barItem1Clicked);
            Assert.IsTrue(barItem2Clicked);
        }
        [Test]
        public void SaveAndRestoreToolbar() {
            BarItem barItem1, barItem2;
            RibbonPage page1, page2;
            RibbonPageGroup group1, group2;
            CreateDynamicPage("FirstItem", 1, out barItem1, out page1, out group1);
            CreateDynamicPage("SecondItem", 2, out barItem2, out page2, out group2);
            this.ribbonControl.ToolbarItemLinks.Add(((BarItemLink)this.defaultCategory.Pages[0].Groups[0].ItemLinks[0]).Item.CreateLink());
            this.ribbonControl.ToolbarItemLinks.Add(((BarItemLink)this.defaultCategory.Pages[1].Groups[0].ItemLinks[0]).Item.CreateLink());
            this.ribbonControl.ToolbarItemLinks.Add(((BarItemLink)this.defaultCategory.Pages[2].Groups[0].ItemLinks[0]).Item.CreateLink());
            string toolbar = this.ribbonManager.GetToolbarAsString();
            CreateRibbon();
            CreateRibbonManager();
            CreateDynamicPage("FirstItem", 2, out barItem1, out page1, out group1);
            CreateDynamicPage("SecondItem", 1, out barItem2, out page2, out group2);
            this.ribbonManager.SetToolbarAsString(toolbar);
            Assert.AreEqual(3, this.ribbonControl.ToolbarItemLinks.Count);
            Assert.AreEqual("StaticItem", ((BarItemLink)this.ribbonControl.ToolbarItemLinks[0]).Item.Content);
            Assert.AreEqual(1, ((BarItemLink)this.ribbonControl.ToolbarItemLinks[1]).Item.Content);
            Assert.AreEqual(2, ((BarItemLink)this.ribbonControl.ToolbarItemLinks[2]).Item.Content);
        }
        void CreateDynamicPage(string name, object content, out BarItem barItem, out RibbonPage page, out RibbonPageGroup group) {
            BarManager barManager = new BarManager();
            RibbonControl ribbonControl = new RibbonControl();
            RibbonDefaultPageCategory pageCategory = new RibbonDefaultPageCategory();
            barManager.Child = ribbonControl;
            ribbonControl.Categories.Add(pageCategory);
            barItem = new BarButtonItem() { Name = name, Content = content };
            page = new RibbonPage();
            group = new RibbonPageGroup();
            barManager.Items.Add(barItem);
            group.ItemLinks.Add(barItem.CreateLink());
            page.Groups.Add(group);
            pageCategory.Pages.Add(page);
            this.ribbonManager.Merge(this.defaultCategory, barManager, ribbonControl);
        }
        void CreateRibbon() {
            this.barManager = new BarManager();
            BarButtonItem staticItem = new BarButtonItem() { Name = "StaticItem", Content = "StaticItem" };
            this.barManager.Items.Add(staticItem);
            this.ribbonControl = new RibbonControl();
            this.defaultCategory = new RibbonDefaultPageCategory();
            RibbonPage staticPage = new RibbonPage() { Caption = "StaticPage" };
            RibbonPageGroup staticItemLinkGroup = new RibbonPageGroup() { Caption = "StaticItemLinkGroup" };
            staticItemLinkGroup.ItemLinks.Add(staticItem.CreateLink());
            staticPage.Groups.Add(staticItemLinkGroup);
            this.defaultCategory.Pages.Add(staticPage);
            this.ribbonControl.Categories.Add(defaultCategory);
            this.barManager.Child = ribbonControl;
            this.window.Content = barManager;
        }
        void CreateWndow() {
            this.window = new Window();
            this.window.Show();
        }
        void CreateRibbonManager() {
            this.ribbonManager = new RibbonManager(this.barManager, this.ribbonControl);
        }
    }
}
#endif
