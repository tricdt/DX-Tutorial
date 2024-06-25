using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.VideoRent.Resources.Helpers;
using DevExpress.VideoRent.Wpf.Helpers;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Accordion;
using System.Collections.ObjectModel;
using System.Collections;

namespace DevExpress.VideoRent.Wpf.ModulesBase {
    public class DemoModuleGroup : DependencyObject {
        #region Dependency Properties
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty ImageProperty;
        static DemoModuleGroup() {
            Type ownerType = typeof(DemoModuleGroup);
            TitleProperty = DependencyProperty.Register("Title", typeof(string), ownerType, new PropertyMetadata(string.Empty));
            ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public DemoModuleGroup(string title, string imageName) {
            Title = title;
        }
        public string Title { get { return (string)GetValue(TitleProperty); } private set { SetValue(TitleProperty, value); } }
        public ImageSource Image { get { return (ImageSource)GetValue(ImageProperty); } set { SetValue(ImageProperty, value); } }
    }
    public class DemoModuleCategory : DependencyObject {
        #region Dependency Properties
        public static readonly DependencyProperty GroupProperty;
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty SmallIconProperty;
        public static readonly DependencyProperty LargeIconProperty;
        static DemoModuleCategory() {
            Type ownerType = typeof(DemoModuleCategory);
            GroupProperty = DependencyProperty.Register("Group", typeof(DemoModuleGroup), ownerType, new PropertyMetadata(null));
            TitleProperty = DependencyProperty.Register("Title", typeof(string), ownerType, new PropertyMetadata(string.Empty));
            SmallIconProperty = DependencyProperty.Register("SmallIcon", typeof(ImageSource), ownerType, new PropertyMetadata(null));
            LargeIconProperty = DependencyProperty.Register("LargeIcon", typeof(ImageSource), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public DemoModuleCategory(DemoModuleGroup group, string title) {
            Group = group;
            Title = title;
        }
        public DemoModuleGroup Group { get { return (DemoModuleGroup)GetValue(GroupProperty); } private set { SetValue(GroupProperty, value); } }
        public string Title { get { return (string)GetValue(TitleProperty); } private set { SetValue(TitleProperty, value); } }
        public ImageSource SmallIcon { get { return (ImageSource)GetValue(SmallIconProperty); } set { SetValue(SmallIconProperty, value); } }
        public ImageSource LargeIcon { get { return (ImageSource)GetValue(LargeIconProperty); } set { SetValue(LargeIconProperty, value); } }
        public override string ToString() {
            return string.Concat(base.ToString(), string.Format("({0})", Title));
        }
    }
    public class AccordionGroup {
        public string Title { get; set; }
        public ImageSource LargeIcon { get; set; }
        public DemoModuleGroup DemoModuleGroup { get; private set; }

        public ObservableCollection<DemoModuleCategory> Categories { get; private set; }

        public AccordionGroup(DemoModuleGroup group, DemoModuleCategory[] categories, ImageSource icon) {
            DemoModuleGroup = group;
            Title = group.Title;
            Categories = new ObservableCollection<DemoModuleCategory>(categories);
            LargeIcon = icon;
        }
    }
    public class DemoModule : CustomShowUserControl, IDisposable {
        static StringIdGenerator idGenerator = new StringIdGenerator();
        bool barItemsPrefixAdded = false;
        string id;

        public DemoModule() {
            id = idGenerator.Get();
        }
        ~DemoModule() { Dispose(false); }
        public bool Disposed { get { return id == null; } }
        public string Id { get { return id; } }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void AddPrefixToBarItems() {
            if(barItemsPrefixAdded) return;
            barItemsPrefixAdded = true;
            Grid grid = Content as Grid;
            if(grid == null) return;
            BarManager barManager = (BarManager)grid.Children[0];
            RibbonControl ribbonControl = (RibbonControl)barManager.Child;
            foreach(BarItem item in barManager.Items) {
                item.Name = string.Format("{0}_{1}", Id, item.Name);
                RibbonManager.SetFocusingElement(item, this);
                if(!(item is BarSubItem)) continue;
                foreach(BarItemLink link in ((BarSubItem)item).ItemLinks) {
                    link.BarItemName = string.Format("{0}_{1}", Id, link.BarItemName);
                }
            }
            foreach(RibbonPageCategoryBase rpcb in ribbonControl.ActualCategories) {
                foreach(RibbonPage page in rpcb.Pages) {
                    page.Name = string.Format("{0}_{1}", Id, page.Name);
                    foreach(RibbonPageGroup group in page.Groups) {
                        foreach(BarItemLinkBase itemLinkBase in group.ItemLinks) {
                            AddPrefixToBarItemLink(itemLinkBase);
                        }
                    }
                }
            }
            foreach(BarItemLinkBase itemLinkBase in ribbonControl.PageHeaderItemLinks)
                AddPrefixToBarItemLink(itemLinkBase);
        }
        internal object Root { get; set; }
        internal BarManager BarManager { get; set; }
        internal RibbonControl RibbonControl { get; set; }
        internal RibbonPage Bar { get; set; }
        internal EventHandler BarIsSelectedChanged { get; set; }
        internal List<BarItemLinkBase> PageHeaderItemLinks { get; set; }
        protected virtual void DisposeManaged() { }
        protected virtual void DisposeUnmanaged() {
            idGenerator.Release(id);
            id = null;
        }
        void AddPrefixToBarItemLink(BarItemLinkBase itemLinkBase) {
            BarItemLink itemLink = itemLinkBase as BarItemLink;
            if(itemLink == null) return;
            itemLink.BarItemName = string.Format("{0}_{1}", Id, itemLink.BarItemName);
        }
        void Dispose(bool disposing) {
            if(Disposed) return;
            if(disposing)
                DisposeManaged();
            DisposeUnmanaged();
        }
        #region Commands
        class DemoModuleSelectCategoryCommand : ICommand {
            DemoModule demoModule;

            public DemoModuleSelectCategoryCommand(DemoModule demoModule) {
                this.demoModule = demoModule;
            }
            public event EventHandler CanExecuteChanged { add { } remove { } }
            public bool CanExecute(object parameter) { return true; }
            public void Execute(object parameter) {
                DemoModulesControl.Current.SelectDemoModuleCategory(((ClassicShowType)demoModule.ShowMethodType).Category);
            }
        }
        DemoModuleSelectCategoryCommand selectCategoryCommand;
        public ICommand SelectCategoryCommand {
            get {
                if(selectCategoryCommand == null)
                    selectCategoryCommand = new DemoModuleSelectCategoryCommand(this);
                return selectCategoryCommand;
            }
        }
        #endregion
    }
    public class DemoModulesControl : Control {
        public static DemoModulesControl Current { get; set; }
        #region Dependency Properties
        public static readonly DependencyProperty BarManagerProperty;
        public static readonly DependencyProperty StatusBarManagerProperty;
        public static readonly DependencyProperty DefaultToolbarItemsProperty;
        public static readonly DependencyProperty DefaultPageProperty;
        static DemoModulesControl() {
            Type ownerType = typeof(DemoModulesControl);
            BarManagerProperty = DependencyProperty.Register("BarManager", typeof(BarManager), ownerType, new PropertyMetadata(null));
            StatusBarManagerProperty = DependencyProperty.Register("StatusBarManager", typeof(BarManager), ownerType, new PropertyMetadata(null));
            DefaultToolbarItemsProperty = DependencyProperty.Register("DefaultToolbarItems", typeof(List<BarItem>), ownerType, new PropertyMetadata(null));
            DefaultPageProperty = DependencyProperty.Register("DefaultPage", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback((d, e) => ((DemoModulesControl)d).OnDefaultPageChanged(e))));
        }
        #endregion
        AccordionControl accordion;
        ContentPresenter demoModulePresenter;
        Dictionary<string, RibbonPageCategoryBase> subcategories;
        Dictionary<DemoModuleCategory, List<DemoModule>> demoModules;
        ObservableCollection<AccordionGroup> accordionGroups;
        RibbonManager ribbonManager;
        DemoModule currentDemoModuleControl = null;

        public DemoModulesControl() {
            DefaultStyleKey = typeof(DemoModulesControl);
            DefaultToolbarItems = new List<BarItem>();
            subcategories = new Dictionary<string, RibbonPageCategoryBase>();
            demoModules = new Dictionary<DemoModuleCategory, List<DemoModule>>();
        }
        public BarManager BarManager { get { return (BarManager)GetValue(BarManagerProperty); } set { SetValue(BarManagerProperty, value); } }
        public BarManager StatusBarManager { get { return (BarManager)GetValue(StatusBarManagerProperty); } set { SetValue(StatusBarManagerProperty, value); } }
        public List<BarItem> DefaultToolbarItems { get { return (List<BarItem>)GetValue(DefaultToolbarItemsProperty); } set { SetValue(DefaultToolbarItemsProperty, value); } }
        public object DefaultPage { get { return (object)GetValue(DefaultPageProperty); } set { SetValue(DefaultPageProperty, value); } }
        public override void EndInit() {
            this.ribbonManager = new RibbonManager(BarManager, Ribbon);
            this.ribbonManager.AddItemsToToolbar(DefaultToolbarItems);
            base.EndInit();
        }
        public void SaveLayoutToStream(Stream stream) {
            byte[] bytes = Encoding.UTF8.GetBytes(this.ribbonManager.GetToolbarAsString());
            stream.Write(bytes, 0, bytes.Length);
        }
        public void RestoreLayoutFromStream(Stream stream) {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            string toolbarString = Encoding.UTF8.GetString(bytes);
            this.ribbonManager.SetToolbarAsString(toolbarString);
        }
        public void AddDemoModule(DemoModule demoModule) {
            PrepareDemoModuleBar(demoModule);
            PrepareDemoModuleRoot(demoModule);
            DepPropertyHelper.SubscribeToChanged(demoModule.Bar, RibbonPage.IsSelectedProperty, demoModule.BarIsSelectedChanged);
            AddDemoModuleBar(demoModule);
            List<DemoModule> demoModulesList;
            if(!demoModules.TryGetValue(((ClassicShowType)demoModule.ShowMethodType).Category, out demoModulesList)) {
                demoModulesList = new List<DemoModule>();
                demoModules.Add(((ClassicShowType)demoModule.ShowMethodType).Category, demoModulesList);
                UpdateAccordion();
            }
            demoModulesList.Add(demoModule);
        }
        public void UpdateAccordion() {
            var accordionGroups = new ObservableCollection<AccordionGroup>();
            demoModules.Keys.GroupBy(x => x.Group).ToList()
                .ForEach(group => accordionGroups.Add(new AccordionGroup(group.Key, group.ToArray(), group.Key.Image)));
            this.accordionGroups = accordionGroups;
            accordion.ItemsSource = this.accordionGroups;
            Dispatcher.BeginInvoke((Action)(() => { accordion.SelectedRootItem = this.accordionGroups.FirstOrDefault(); }));
        }
        public void RemoveDemoModule(DemoModule demoModule) {
            DepPropertyHelper.UnsubscribeFromChanged(demoModule.Bar, RibbonPage.IsSelectedProperty, demoModule.BarIsSelectedChanged);
            List<DemoModule> demoModulesList = demoModules[((ClassicShowType)demoModule.ShowMethodType).Category];
            demoModulesList.Remove(demoModule);
            if(demoModulesList.Count == 0) {
                demoModules.Remove(((ClassicShowType)demoModule.ShowMethodType).Category);
                UpdateAccordion();
            }
            RemoveDemoModuleBar(demoModule);
        }
        public void SelectDemoModule(DemoModule demoModule) {
            if(demoModule == null) {
                if(currentDemoModuleControl != null) {
                    RemoveDemoModulePageHeaderItemLinks(currentDemoModuleControl);
                    currentDemoModuleControl = null;
                }
                demoModulePresenter.Content = DefaultPage;
                OnAccordionItemSelected(accordion, new AccordionSelectedItemChangedEventArgs(accordion, accordion.SelectedItem, null));
                accordion.SelectedItem = null;
            } else {
                var category = ((ClassicShowType)demoModule.ShowMethodType).Category;
                accordion.SelectedItem = category;
                Ribbon.SelectedPage = demoModule.Bar;
            }
        }
        public void SelectDemoModuleCategory(DemoModuleCategory category) {
            List<DemoModule> categoryDemoModules;
            if(!demoModules.TryGetValue(category, out categoryDemoModules)) {
                accordion.SelectedItem = null;
                accordion.SelectedRootItem = null;
                return;
            }
            accordion.SelectedItem = category;
            Ribbon.SelectedPage = categoryDemoModules[0].Bar;
        }
        public void SelectLastDemoModuleInCategory(DemoModuleCategory category) {
            List<DemoModule> categoryDemoModules;
            if(!demoModules.TryGetValue(category, out categoryDemoModules)) {
                SelectDemoModuleCategory(category);
                return;
            }
            SelectDemoModule(categoryDemoModules[categoryDemoModules.Count - 1]);
        }
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            bool ctrlIsPressed = (Keyboard.Modifiers & ModifierKeys.Control) != 0;
            bool altIsPressed = (Keyboard.Modifiers & ModifierKeys.Alt) != 0;
            switch(e.Key) {
                case Key.F1:
                    SelectDemoModule(null);
                    break;
                case Key.D:
                    if(ctrlIsPressed && altIsPressed)
                        GC.Collect();
                    break;

            }
        }
        RibbonControl Ribbon { get { return (RibbonControl)BarManager.Child; } }
        DemoModuleCategory CurrentCategory { get { return accordion.SelectedItem == null ? null : (DemoModuleCategory)accordion.SelectedItem; } }

        void PrepareDemoModuleBar(DemoModule demoModule) {
            Grid grid = (Grid)demoModule.Content;
            demoModule.BarManager = (BarManager)grid.Children[0];
            demoModule.RibbonControl = (RibbonControl)demoModule.BarManager.Child;
            grid.Children.Remove(demoModule.BarManager);
            demoModule.Bar = demoModule.RibbonControl.ActualCategories[0].Pages[0];
            demoModule.BarIsSelectedChanged = (s, e) => RaiseDemoModuleBarIsSelectedChanged(demoModule);
            demoModule.PageHeaderItemLinks = new List<BarItemLinkBase>(demoModule.RibbonControl.PageHeaderItemLinks);
            foreach(BarItemLinkBase link in demoModule.PageHeaderItemLinks)
                demoModule.RibbonControl.PageHeaderItemLinks.Remove(link);
        }
        void PrepareDemoModuleRoot(DemoModule demoModuleControl) {
            DockLayoutManager manager = ((Grid)demoModuleControl.Content).Children[0] as DockLayoutManager;
            if(manager != null) {
                demoModuleControl.Root = demoModuleControl;
                return;
            }
            manager = new DockLayoutManager() { Background = new SolidColorBrush(Colors.Transparent) };
            manager.LayoutRoot = new LayoutGroup() { Orientation = Orientation.Horizontal, Background = new SolidColorBrush(Colors.Transparent) };
            LayoutPanel demoModulePanel = new LayoutPanel() { ShowCaption = false, ShowBorder = false, Background = new SolidColorBrush(Colors.Transparent) };
            demoModulePanel.Content = demoModuleControl;
            manager.LayoutRoot.Add(demoModulePanel);
            demoModuleControl.Root = manager;
        }
        void AddDemoModuleBar(DemoModule demoModule) {
            bool isVisible = CurrentCategory == ((ClassicShowType)demoModule.ShowMethodType).Category;
            RibbonPageCategoryBase rpcb;
            if(subcategories.ContainsKey(((ClassicShowType)demoModule.ShowMethodType).Subcategory)) {
                rpcb = subcategories[((ClassicShowType)demoModule.ShowMethodType).Subcategory];
            } else {
                rpcb = new RibbonPageCategory() { Caption = ((ClassicShowType)demoModule.ShowMethodType).Subcategory };
                Ribbon.Categories.Add(rpcb);
                subcategories.Add(((ClassicShowType)demoModule.ShowMethodType).Subcategory, rpcb);
            }
            rpcb.IsVisible = isVisible;
            demoModule.Bar.IsVisible = isVisible;
            this.ribbonManager.Merge(rpcb, demoModule.BarManager, demoModule.RibbonControl);
        }
        void RemoveDemoModuleBar(DemoModule demoModule) {
            RemoveDemoModulePageHeaderItemLinks(demoModule);
            RibbonPageCategoryBase rpcb = subcategories[((ClassicShowType)demoModule.ShowMethodType).Subcategory];
            this.ribbonManager.Unmerge(demoModule.Bar);
            if(rpcb.Pages.Count == 0) {
                Ribbon.Categories.Remove(subcategories[((ClassicShowType)demoModule.ShowMethodType).Subcategory]);
                subcategories.Remove(((ClassicShowType)demoModule.ShowMethodType).Subcategory);
            } else {
                bool isVisible = false;
                foreach(RibbonPage page in rpcb.Pages) {
                    if(page.IsVisible) {
                        isVisible = true;
                        break;
                    }
                }
                subcategories[((ClassicShowType)demoModule.ShowMethodType).Subcategory].IsVisible = isVisible;
            }
        }
        void RaiseDemoModuleBarIsSelectedChanged(DemoModule demoModuleControl) {
            if(!demoModuleControl.Bar.IsSelected) {
                currentDemoModuleControl = demoModuleControl;
                return;
            }
            if(demoModuleControl != currentDemoModuleControl) {
                if(currentDemoModuleControl != null)
                    RemoveDemoModulePageHeaderItemLinks(currentDemoModuleControl);
                currentDemoModuleControl = demoModuleControl;
                SetDemoModulePresenterContent(demoModuleControl.Root);
                AddDemoModulePageHeaderItemLinks(currentDemoModuleControl);
            }
        }
        void SetDemoModulePresenterContent(object content) {
            MouseHelper.WaitIdle();
            demoModulePresenter.Content = content;
        }
        void AddDemoModulePageHeaderItemLinks(DemoModule demoModuleControl) {
            foreach(BarItemLinkBase itemLinkBase in demoModuleControl.PageHeaderItemLinks)
                Ribbon.PageHeaderItemLinks.Add(itemLinkBase);
        }
        void RemoveDemoModulePageHeaderItemLinks(DemoModule demoModuleControl) {
            foreach(BarItemLinkBase itemLinkBase in demoModuleControl.PageHeaderItemLinks)
                Ribbon.PageHeaderItemLinks.Remove(itemLinkBase);
        }

        void OnAccordionItemSelected(object sender, AccordionSelectedItemChangedEventArgs e) {
            if(e.OldItem != null) {
                DemoModuleCategory prevCategory = (DemoModuleCategory)e.OldItem;
                List<DemoModule> modules;
                if(demoModules.TryGetValue(prevCategory, out modules)) {
                    foreach(DemoModule demoModule in modules) {
                        demoModule.Bar.IsVisible = false;
                        subcategories[((ClassicShowType)demoModule.ShowMethodType).Subcategory].IsVisible = false;
                    }
                }
            }
            if(e.NewItem == null) {
                Ribbon.SelectedPage = Ribbon.SelectedPage.PageCategory.Pages[0];
                currentDemoModuleControl = null;
                return;
            }
            DemoModuleCategory newCategory = (DemoModuleCategory)e.NewItem;
            foreach(DemoModule demoModule in demoModules[newCategory]) {
                subcategories[((ClassicShowType)demoModule.ShowMethodType).Subcategory].IsVisible = true;
                demoModule.Bar.IsVisible = true;
            }
            Ribbon.SelectedPage = demoModules[newCategory][0].Bar;
        }
        void OnDefaultPageChanged(DependencyPropertyChangedEventArgs e) {
            if(e.NewValue != null)
                SelectDemoModule(null);
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            subcategories.Add(string.Empty, Ribbon.ActualCategories[0]);
            if(accordion != null)
                accordion.SelectedItemChanged -= OnAccordionItemSelected;
            accordion = (AccordionControl)GetTemplateChild("accordion");
            accordion.SelectedItemChanged += OnAccordionItemSelected;
            demoModulePresenter = (ContentPresenter)GetTemplateChild("DemoModulePresenter");
        }
    }
}
