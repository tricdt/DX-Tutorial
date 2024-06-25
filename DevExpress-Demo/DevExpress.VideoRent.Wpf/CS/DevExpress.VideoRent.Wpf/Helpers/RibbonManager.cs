using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;
using DevExpress.VideoRent.Resources.Helpers;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class RibbonManager {
        class DefaultAction {
            Type type;
            object content;
            ImageSource glyph;
            ImageSource largeGlyph;

            public DefaultAction(BarItem barItem) {
                this.type = barItem.GetType();
                this.content = barItem.Content;
                this.glyph = barItem.Glyph;
                this.largeGlyph = barItem.LargeGlyph;
            }
            public static bool operator !=(DefaultAction action1, DefaultAction action2) {
                return !(action1 == action2);
            }
            public static bool operator ==(DefaultAction action1, DefaultAction action2) {
                bool action1IsNull = (object)action1 == null;
                bool action2IsNull = (object)action2 == null;
                if(action1IsNull && action2IsNull) return true;
                if(action1IsNull || action2IsNull) return false;
                return AreEqual(action1, action2);
            }
            public override bool Equals(object obj) {
                DefaultAction defaultAction = obj as DefaultAction;
                if(defaultAction == null) return false;
                return AreEqual(this, defaultAction);
            }
            public override int GetHashCode() {
                int hash = 0;
                hash += this.type.GetHashCode();
                hash += this.content == null ? 0 : this.content.GetHashCode();
                hash += ImageSourceHelper.ImageSourceToString(this.glyph).GetHashCode();
                hash += ImageSourceHelper.ImageSourceToString(this.largeGlyph).GetHashCode();
                return hash;
            }
            static bool AreEqual(DefaultAction action1, DefaultAction action2) {
                if(action1.type != action2.type) return false;
                if(!object.Equals(action1.content, action2.content)) return false;
                if(ImageSourceHelper.ImageSourceToString(action1.glyph) != ImageSourceHelper.ImageSourceToString(action2.glyph)) return false;
                if(ImageSourceHelper.ImageSourceToString(action1.largeGlyph) != ImageSourceHelper.ImageSourceToString(action2.largeGlyph)) return false;
                return true;
            }
        }
        class BarItemEqualityComparer : IEqualityComparer<BarItem> {
            static StringIdGenerator namesGenerator = new StringIdGenerator();

            public BarItem CopyBarItem(BarItem barItem, out ICollection<BarItemLinkBase> barItemLinks) {
                BarItem copiedBarItem = (BarItem)Activator.CreateInstance(barItem.GetType());
                copiedBarItem.Name = "CopiedBarItem" + namesGenerator.Get();
                copiedBarItem.Content = barItem.Content;
                copiedBarItem.ContentTemplate = barItem.ContentTemplate;
                copiedBarItem.Glyph = barItem.Glyph;
                copiedBarItem.LargeGlyph = barItem.LargeGlyph;
                copiedBarItem.Hint = barItem.Hint;
                copiedBarItem.GlyphAlignment = barItem.GlyphAlignment;
                RibbonManager.SetAction(copiedBarItem, RibbonManager.GetAction(barItem));
                CopyBarItemEditSettings(barItem, copiedBarItem);
                CopyBarItemPopupControl(barItem, copiedBarItem);
                barItemLinks = CopyBarItemItemLinks(barItem, copiedBarItem);
                return copiedBarItem;
            }
            public string BarItemToString(BarItem barItem) {
                string type = ReflectionHelper.ObjectToString(barItem.GetType());
                string content = ReflectionHelper.ObjectToString(barItem.Content);
                string glyph = ImageSourceHelper.ImageSourceToString(barItem.Glyph);
                string largeGlyph = ImageSourceHelper.ImageSourceToString(barItem.LargeGlyph);
                string hint = ReflectionHelper.ObjectToString(barItem.Hint);
                string glyphAlignment = ReflectionHelper.ObjectToString(barItem.GlyphAlignment);
                string action = ReflectionHelper.ObjectToString(RibbonManager.GetAction(barItem));
                return ReflectionHelper.CombineStrings(type, content, glyph, largeGlyph, hint, glyphAlignment, action);
            }
            public BarItem BarItemFromString(string s) {
                string[] parts = ReflectionHelper.SplitString(s);
                if(parts.Length != 7) return null;
                Type type = (Type)ReflectionHelper.ObjectFromString(parts[0]);
                if(type == null) return null;
                BarItem barItem = (BarItem)Activator.CreateInstance(type);
                barItem.Content = ReflectionHelper.ObjectFromString(parts[1]);
                barItem.Glyph = ImageSourceHelper.ImageSourceFromString(parts[2]);
                barItem.LargeGlyph = ImageSourceHelper.ImageSourceFromString(parts[3]);
                barItem.Hint = ReflectionHelper.ObjectFromString(parts[4]);
                barItem.GlyphAlignment = (Dock)ReflectionHelper.ObjectFromString(parts[5]);
                RibbonManager.SetAction(barItem, ReflectionHelper.ObjectFromString(parts[6]));
                return barItem;
            }
            public bool Equals(BarItem x, BarItem y) {
                return object.Equals(RibbonManager.GetActualAction(x), RibbonManager.GetActualAction(y));
            }
            public int GetHashCode(BarItem obj) {
                return RibbonManager.GetActualAction(obj).GetHashCode();
            }
            ICollection<BarItemLinkBase> CopyBarItemItemLinks(BarItem barItem, BarItem copiedBarItem) {
                BarSubItem barSubItem = barItem as BarSubItem;
                if(barSubItem == null) return null;
                BarSubItem copiedBarSubItem = (BarSubItem)copiedBarItem;
                foreach(BarItemLinkBase barItemLinkBase in barSubItem.ItemLinks) {
                    BarItemLinkBase copiedBarItemLinkBase = (BarItemLinkBase)Activator.CreateInstance(barItemLinkBase.GetType());
                    copiedBarSubItem.ItemLinks.Add(copiedBarItemLinkBase);
                    BarItemLink barItemLink = barItemLinkBase as BarItemLink;
                    if(barItemLink != null) {
                        BarItemLink copiedBarItemLink = (BarItemLink)copiedBarItemLinkBase;
                        copiedBarItemLink.BarItemName = barItemLink.BarItemName;
                    }
                }
                return copiedBarSubItem.ItemLinks;
            }
            void CopyBarItemEditSettings(BarItem barItem, BarItem copiedBarItem) {
                BarEditItem barEditItem = barItem as BarEditItem;
                if(barEditItem == null) return;
                BarEditItem copiedBarEditItem = (BarEditItem)copiedBarItem;
                copiedBarEditItem.EditSettings = barEditItem.EditSettings;
                copiedBarEditItem.EditWidth = barEditItem.EditWidth;
                copiedBarEditItem.EditHeight = barEditItem.EditHeight;
            }

            void CopyBarItemPopupControl(BarItem barItem, BarItem copiedBarItem) {
                BarSplitButtonItem barSplitButtonItem = barItem as BarSplitButtonItem;
                if(barSplitButtonItem == null) return;
                BarSplitButtonItem copiedBarEditItem = (BarSplitButtonItem)copiedBarItem;
                copiedBarEditItem.PopupControl = barSplitButtonItem.PopupControl;
                copiedBarEditItem.ActAsDropDown = barSplitButtonItem.ActAsDropDown;
            }
        }
        #region Dependency Properties
        public static readonly DependencyProperty FocusingElementProperty;
        public static readonly DependencyProperty ActionProperty;
        static readonly DependencyProperty DefaultActionProperty;
        static RibbonManager() {
            Type ownerType = typeof(RibbonManager);
            FocusingElementProperty = DependencyProperty.RegisterAttached("FocusingElement", typeof(UIElement), ownerType, new PropertyMetadata(null));
            ActionProperty = DependencyProperty.RegisterAttached("Action", typeof(object), ownerType, new PropertyMetadata(null));
            DefaultActionProperty = DependencyProperty.RegisterAttached("DefaultAction", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion
        static BarItemEqualityComparer barItemEqualityComparer = new BarItemEqualityComparer();

        static object GetDefaultAction(BarItem barItem) { return barItem.GetValue(DefaultActionProperty); }
        static void SetDefaultAction(BarItem barItem, object action) { barItem.SetValue(DefaultActionProperty, action); }
        static object GetActualAction(BarItem barItem) {
            object action = GetAction(barItem);
            if(action != null) return action;
            action = GetDefaultAction(barItem);
            if(action == null) {
                action = new DefaultAction(barItem);
                SetDefaultAction(barItem, action);
            }
            return action;
        }
        public static UIElement GetFocusingElement(BarItem barItem) { return (UIElement)barItem.GetValue(FocusingElementProperty); }
        public static void SetFocusingElement(BarItem barItem, UIElement element) { barItem.SetValue(FocusingElementProperty, element); }
        public static object GetAction(BarItem barItem) { return barItem.GetValue(ActionProperty); }
        public static void SetAction(BarItem barItem, object action) { barItem.SetValue(ActionProperty, action); }

        BarManager barManager;
        RibbonControl ribbonControl;
        Dictionary<string, BarItem> barItemsByName = new Dictionary<string, BarItem>();
        Dictionary<BarItem, BarItem> sharedBarItems = new Dictionary<BarItem, BarItem>(barItemEqualityComparer);
        Dictionary<RibbonPage, ICollection<BarItem>> dynamicPages = new Dictionary<RibbonPage, ICollection<BarItem>>();
        RibbonPage activeDynamicPage = null;

        public RibbonManager(BarManager barManager, RibbonControl ribbonControl) {
            this.barManager = barManager;
            this.ribbonControl = ribbonControl;
            this.ribbonControl.SelectedPageChanged += OnRibbonControlSelectedPageChanged;
        }
        public void Merge(RibbonPageCategoryBase category, BarManager barManager, RibbonControl ribbonControl) {
            IList<BarItem> items = DetachItems(barManager);
            RibbonPage page = DetachPage(ribbonControl);
            AddBarItems(items);
            foreach(RibbonPageGroup group in page.Groups) {
                CorrectBarItemLinks(group.ItemLinks);
            }
            CorrectBarItemLinks(ribbonControl.PageHeaderItemLinks);
            this.dynamicPages.Add(page, items);
            category.Pages.Add(page);
        }
        public void Unmerge(RibbonPage page) {
            page.PageCategory.Pages.Remove(page);
            foreach(RibbonPageGroup group in new List<RibbonPageGroup>(page.Groups)) {
                foreach(BarItemLinkBase link in new List<BarItemLinkBase>(group.Links)) {
                    group.Links.Remove(link);
                }
                page.Groups.Remove(group);
                BindingOperations.ClearBinding(page, RibbonPage.DataContextProperty);
                page.DataContext = null;
            }
            ICollection<BarItem> pageItems = this.dynamicPages[page];
            this.dynamicPages.Remove(page);
            RemoveBarItems(pageItems);
        }
        public string GetToolbarAsString() {
            List<string> itemsStrings = new List<string>();
            foreach(BarItemLinkBase itemLinkBase in this.ribbonControl.ToolbarItemLinks) {
                BarItemLink itemLink = itemLinkBase as BarItemLink;
                if(itemLink == null) continue;
                string name = null;
                string properties = null;
                BarItem sharedBarItem;
                if(itemLink.Item != null && this.sharedBarItems.TryGetValue(itemLink.Item, out sharedBarItem)) {
                    properties = barItemEqualityComparer.BarItemToString(sharedBarItem);
                } else {
                    name = itemLink.BarItemName;
                }
                itemsStrings.Add(ReflectionHelper.CombineStrings(ReflectionHelper.ObjectToString(name), ReflectionHelper.ObjectToString(properties)));
            }
            return ReflectionHelper.CombineStrings(itemsStrings.ToArray());
        }
        public void SetToolbarAsString(string toolbar) {
            this.ribbonControl.ToolbarItemLinks.Clear();
            string[] itemStrings = ReflectionHelper.SplitString(toolbar);
            foreach(string itemString in itemStrings) {
                string[] parts = ReflectionHelper.SplitString(itemString);
                if(parts.Length != 2) continue;
                string name = (string)ReflectionHelper.ObjectFromString(parts[0]);
                string properties = (string)ReflectionHelper.ObjectFromString(parts[1]);
                BarItem barItemForLink;
                if(name != null) {
                    barItemForLink = this.barManager.Items[name];
                } else {
                    BarItem barItem = barItemEqualityComparer.BarItemFromString(properties);
                    barItemForLink = barItem == null ? null : CreateSharedBarItem(barItem);
                }
                if(barItemForLink != null)
                    this.ribbonControl.ToolbarItemLinks.Add(barItemForLink.CreateLink());
            }
        }
        public void AddItemsToToolbar(ICollection<BarItem> toolbarItems) {
            AddBarItems(toolbarItems);
            foreach(BarItem toolbarItem in toolbarItems) {
                BarItem sharedBarItem = GetSharedBarItem(toolbarItem);
                this.ribbonControl.ToolbarItemLinks.Add(sharedBarItem.CreateLink());
            }
        }
        IList<BarItem> DetachItems(BarManager bm) {
            List<BarItem> items = new List<BarItem>(bm.Items);
            foreach(BarItem item in items)
                bm.Items.Remove(item);
            return items;
        }
        RibbonPage DetachPage(RibbonControl rc) {
            RibbonPage page = rc.ActualCategories[0].Pages[0];
            page.PageCategory.Pages.Remove(page);
            return page;
        }
        BarItem CreateSharedBarItem(BarItem barItem) {
            BarItem sharedBarItem;
            if(!this.sharedBarItems.TryGetValue(barItem, out sharedBarItem)) {
                ICollection<BarItemLinkBase> sharedBarItemLinks;
                sharedBarItem = barItemEqualityComparer.CopyBarItem(barItem, out sharedBarItemLinks);
                sharedBarItem.DataContextChanged += (s, e) => { sharedBarItem.DataContext = null; };
                sharedBarItem.IsEnabled = false;
                sharedBarItem.ItemClick += OnSharedBarItemItemClick;
                sharedBarItem.ItemDoubleClick += OnSharedBarItemItemClick;
                CorrectBarItemLinks(sharedBarItemLinks);
                this.barManager.Items.Add(sharedBarItem);
                this.sharedBarItems.Add(sharedBarItem, sharedBarItem);
            }
            return sharedBarItem;
        }
        void OnRibbonControlSelectedPageChanged(object sender, RibbonPropertyChangedEventArgs e) {
            RibbonPage oldPage = (RibbonPage)e.OldValue;
            RibbonPage newPage = (RibbonPage)e.NewValue;
            if(newPage != null && !this.dynamicPages.ContainsKey(newPage)) {
                if(activeDynamicPage == null)
                    activeDynamicPage = oldPage;
                return;
            }
            if(newPage != activeDynamicPage) {
                SetPageIsActive(activeDynamicPage, false);
                activeDynamicPage = newPage;
                SetPageIsActive(activeDynamicPage, true);
            }
        }
        void SetPageIsActive(RibbonPage page, bool isActive) {
            if(page == null) return;
            ICollection<BarItem> barItems;
            if(!this.dynamicPages.TryGetValue(page, out barItems)) return;
            foreach(BarItem barItem in barItems)
                SetBarItemIsActive(barItem, isActive);
        }
        BarItemLinkBase GetCorrectedBarItemLink(BarItemLinkBase barItemLinkBase) {
            BarItemLink barItemLink = barItemLinkBase as BarItemLink;
            if(barItemLink == null) return barItemLinkBase;
            string barItemName = barItemLink.BarItemName;
            BarItem barItem;
            if(!this.barItemsByName.TryGetValue(barItemName, out barItem)) return null;
            BarItemLink correctedBarItemLink = GetSharedBarItem(barItem).CreateLink();
            LinkBarItemLinkToBarItemName(correctedBarItemLink, barItemName);
            return correctedBarItemLink;
        }
        void SetBarItemIsActive(BarItem barItem, bool isActive) {
            BarItem sharedBarItem = GetSharedBarItem(barItem);
            SetActiveBarItem(sharedBarItem, isActive ? barItem : null);
        }
        void LinkBarItemToSharedBarItem(BarItem barItem, BarItem sharedBarItem) {
            barItem.Tag = sharedBarItem;
        }
        BarItem GetSharedBarItem(BarItem barItem) {
            return (BarItem)barItem.Tag;
        }
        void LinkBarItemLinkToBarItemName(BarItemLink barItemLink, string barItemName) {
            barItemLink.Tag = barItemName;
        }
        string GetBarItemName(BarItemLink barItemLink) {
            return (string)barItemLink.Tag;
        }
        void AddBarItems(ICollection<BarItem> barItems) {
            foreach(BarItem barItem in barItems) {
                this.barManager.Items.Add(barItem);
                bool nameIsEmpty = string.IsNullOrEmpty(barItem.Name);
                if(!nameIsEmpty && this.barItemsByName.ContainsKey(barItem.Name)) continue;
                BarItem sharedBarItem = CreateSharedBarItem(barItem);
                if(!nameIsEmpty)
                    this.barItemsByName.Add(barItem.Name, barItem);
                LinkBarItemToSharedBarItem(barItem, sharedBarItem);
            }
        }
        void RemoveBarItems(ICollection<BarItem> barItems) {
            foreach(BarItem barItem in barItems) {
                LinkBarItemToSharedBarItem(barItem, null);
                this.barItemsByName.Remove(barItem.Name);
                this.barManager.Items.Remove(barItem);
            }
        }
        void CorrectBarItemLinks(ICollection<BarItemLinkBase> barItemLinks) {
            if(barItemLinks == null) return;
            List<BarItemLinkBase> sourceBarItemLinks = new List<BarItemLinkBase>(barItemLinks);
            barItemLinks.Clear();
            foreach(BarItemLinkBase barItemLinkBase in sourceBarItemLinks) {
                BarItemLinkBase correctedLink = GetCorrectedBarItemLink(barItemLinkBase);
                if(correctedLink != null)
                    barItemLinks.Add(correctedLink);
            }
        }
        void SetActiveBarItem(BarItem sharedBarItem, BarItem activeBarItem) {
            sharedBarItem.Tag = activeBarItem;
            BarCheckItem sharedBarCheckItem = sharedBarItem as BarCheckItem;
            BarEditItem sharedBarEditItem = sharedBarItem as BarEditItem;
            if(activeBarItem == null) {
                sharedBarItem.IsEnabled = false;
                sharedBarItem.Content = sharedBarItem.Content;
                sharedBarItem.Glyph = sharedBarItem.Glyph;
                sharedBarItem.LargeGlyph = sharedBarItem.LargeGlyph;
                sharedBarItem.Hint = sharedBarItem.Hint;
                sharedBarItem.GlyphAlignment = sharedBarItem.GlyphAlignment;
                if(sharedBarEditItem != null) {
                    object editValue = sharedBarEditItem.EditValue;
                    BindingOperations.ClearBinding(sharedBarEditItem, BarEditItem.EditValueProperty);
                    sharedBarEditItem.EditValue = editValue;
                }
                if(sharedBarCheckItem != null)
                    DepPropertyHelper.UnbindHard(sharedBarCheckItem, BarCheckItem.IsCheckedProperty);
            } else {
                sharedBarItem.SetBinding(BarItem.IsEnabledProperty, new Binding("IsEnabled") { Source = activeBarItem, Mode = BindingMode.OneWay });
                sharedBarItem.SetBinding(BarItem.ContentProperty, new Binding("Content") { Source = activeBarItem, Mode = BindingMode.OneWay });
                sharedBarItem.SetBinding(BarItem.GlyphProperty, new Binding("Glyph") { Source = activeBarItem, Mode = BindingMode.OneWay });
                sharedBarItem.SetBinding(BarItem.LargeGlyphProperty, new Binding("LargeGlyph") { Source = activeBarItem, Mode = BindingMode.OneWay });
                sharedBarItem.SetBinding(BarItem.HintProperty, new Binding("Hint") { Source = activeBarItem, Mode = BindingMode.OneWay });
                sharedBarItem.SetBinding(BarItem.GlyphAlignmentProperty, new Binding("GlyphAlignment") { Source = activeBarItem, Mode = BindingMode.OneWay });
                if(sharedBarItem is BarEditItem)
                    sharedBarItem.SetBinding(BarEditItem.EditValueProperty, new Binding("EditValue") { Source = activeBarItem, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                if(sharedBarCheckItem != null)
                    DepPropertyHelper.BindHard(sharedBarCheckItem, BarCheckItem.IsCheckedProperty, activeBarItem, BarCheckItem.IsCheckedProperty);
            }
        }
        BarItem GetActiveBarItem(BarItem sharedBarItem) {
            return (BarItem)sharedBarItem.Tag;
        }
        void OnSharedBarItemItemClick(object sender, ItemClickEventArgs e) {
            BarItem sharedBarItem = (BarItem)sender;
            BarItem activeBarItem = PrepareToClickAndGetActiveItem(sharedBarItem);
            if(activeBarItem != null)
                activeBarItem.PerformClick();
        }
        BarItem PrepareToClickAndGetActiveItem(BarItem sharedBarItem) {
            BarItem activeBarItem = GetActiveBarItem(sharedBarItem);
            if(activeBarItem == null) return null;
            UIElement focusingElement = GetFocusingElement(activeBarItem);
            if(focusingElement != null)
                focusingElement.Focus();
            return activeBarItem;
        }
    }
    public static class ImageSourceHelper {
        class EmbeddedResourceUri {
            public EmbeddedResourceUri(Assembly assembly, string name, bool isNameFull) {
                Assembly = assembly;
                Name = name;
                IsNameFull = isNameFull;
            }
            public Assembly Assembly { get; private set; }
            public string Name { get; private set; }
            public bool IsNameFull { get; private set; }
            public override bool Equals(object obj) {
                EmbeddedResourceUri uri = obj as EmbeddedResourceUri;
                if(uri == null) return false;
                if(!object.Equals(Assembly, uri.Assembly)) return false;
                if(!object.Equals(Name, uri.Name)) return false;
                if(!object.Equals(IsNameFull, uri.IsNameFull)) return false;
                return true;
            }
            public override int GetHashCode() {
                int hash = 0;
                hash += Assembly == null ? 0 : Assembly.GetHashCode();
                hash += Name == null ? 0 : Name.GetHashCode();
                hash += IsNameFull.GetHashCode();
                return hash;
            }
        }
        #region Dependency Properties
        static readonly DependencyProperty SourceUriProperty;
        static ImageSourceHelper() {
            Type ownerType = typeof(ImageSourceHelper);
            SourceUriProperty = DependencyProperty.RegisterAttached("SourceUri", typeof(EmbeddedResourceUri), ownerType, new PropertyMetadata(null));
        }
        #endregion
        static EmbeddedResourceUri GetSourceUri(ImageSource imageSource) { return (EmbeddedResourceUri)imageSource.GetValue(SourceUriProperty); }
        static void SetSourceUri(ImageSource imageSource, EmbeddedResourceUri uri) { imageSource.SetValue(SourceUriProperty, uri); }
        static Dictionary<EmbeddedResourceUri, ImageSource> imageSources = new Dictionary<EmbeddedResourceUri, ImageSource>();

        public static ImageSource CreateImageFromEmbeddedResource(Assembly assembly, string name, bool isNameFull) {
            EmbeddedResourceUri uri = new EmbeddedResourceUri(assembly, name, isNameFull);
            ImageSource imageSource;
            if(imageSources.TryGetValue(uri, out imageSource)) return imageSource;
            Stream stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, name, isNameFull);
            imageSource = CreateImageSourceFromStream(stream);
            SetSourceUri(imageSource, uri);
            imageSources.Add(uri, imageSource);
            return imageSource;
        }
        public static string ImageSourceToString(ImageSource imageSource) {
            BitmapImage bitmapImage = imageSource as BitmapImage;
            if(bitmapImage == null) return string.Empty;
            if(bitmapImage.UriSource == null) return EmbeddedResourceImageSourceToString(imageSource);
            return string.Format(bitmapImage.UriSource.IsAbsoluteUri ? "a{0}" : "r{0}", bitmapImage.UriSource);
        }
        public static ImageSource ImageSourceFromString(string s) {
            if(string.IsNullOrEmpty(s)) return null;
            try {
                if(s[0] == 'e') return EmbeddedResourceImageSourceFromString(s);
                bool absolute = s[0] == 'a';
                string uriString = s.Substring(1);
                return new BitmapImage(new Uri(uriString, absolute ? UriKind.Absolute : UriKind.Relative));
            } catch {
                return null;
            }
        }
        static string EmbeddedResourceImageSourceToString(ImageSource imageSource) {
            EmbeddedResourceUri uri = GetSourceUri(imageSource);
            if(uri == null) return string.Empty;
            return string.Format("e{0}\\/{1}\\/{2}", uri.Assembly.FullName, uri.Name, uri.IsNameFull);
        }
        static ImageSource EmbeddedResourceImageSourceFromString(string s) {
            string[] parts = s.Substring(1).Split(new string[] { "\\/" }, StringSplitOptions.None);
            if(parts.Length != 3) return null;
            Assembly assembly = AssemblyHelper.GetAssembly(parts[0]);
            if(assembly == null) return null;
            return CreateImageFromEmbeddedResource(assembly, parts[1], bool.Parse(parts[2]));
        }
        static ImageSource CreateImageSourceFromStream(Stream stream) {
            if(stream == null) return null;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = stream;
            bi.EndInit();
            return bi;
        }
    }
}
