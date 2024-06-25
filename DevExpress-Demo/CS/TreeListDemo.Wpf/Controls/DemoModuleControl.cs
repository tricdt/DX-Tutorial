using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;

namespace TreeListDemo {
    public class TreeListDemoModule : DemoModule {
        static readonly DependencyPropertyKey TreeListControlPropertyKey;
        public static readonly DependencyProperty TreeListControlProperty;
        static TreeListDemoModule() {
            TreeListControlPropertyKey = DependencyProperty.RegisterReadOnly("TreeListControl", typeof(TreeListControl), typeof(TreeListDemoModule), new PropertyMetadata(null));
            TreeListControlProperty = TreeListControlPropertyKey.DependencyProperty;
        }

        public TreeListControl TreeListControl {
            get { return (TreeListControl)GetValue(TreeListControlProperty); }
            protected set { SetValue(TreeListControlPropertyKey, value); }
        }
        protected override void HidePopupContent() {
            if(TreeListControl != null)
                TreeListControl.View.HideColumnChooser();
            base.HidePopupContent();
        }
        protected virtual bool ShowBorder { get { return false; } }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            if(TreeListControl == null) {
                TreeListControl = Content as TreeListControl ?? LayoutTreeHelper.GetVisualChildren((DependencyObject)Content).OfType<TreeListControl>().FirstOrDefault(); ;
            }
            if(OptionsDataContext == null)
                OptionsDataContext = TreeListControl;
            if(TreeListControl != null)
                TreeListControl.ShowBorder = ShowBorder;
        }
    }

    public class PrintTreeListDemoModule : TreeListDemoModule {
        DXTabControl dxTabControlCore;
        public PrintTreeListDemoModule() {
            ShowPrintPreview = new DelegateCommand<object>(p => { OnShowPrintPreview((string)p); });
            ShowPrintPreviewInNewTab = new DelegateCommand<object>(p => { OnShowPrintPreviewInNewTab((string)p); });
        }
        public ICommand ShowPrintPreview { get; private set; }
        public ICommand ShowPrintPreviewInNewTab { get; private set; }
        public DXTabControl DXTabControl {
            get { return dxTabControlCore; }
            set {
                if(DXTabControl == value)
                    return;
                if(DXTabControl != null)
                    DXTabControl.TabHidden -= new TabControlTabHiddenEventHandler(OnTabHidden);
                dxTabControlCore = value;
                OptionsDataContext = TreeListControl = dxTabControlCore == null ? null : (TreeListControl)((DXTabItem)dxTabControlCore.Items[0]).Content;
                if(DXTabControl != null)
                    DXTabControl.TabHidden += new TabControlTabHiddenEventHandler(OnTabHidden);
                if(TreeListControl != null)
                    TreeListControl.ShowBorder = ShowBorder;
            }
        }
        protected TreeListView View { get { return TreeListControl.View; } }
        protected virtual void OnShowPrintPreview(string documentName) {
            PrintHelper.ShowRibbonPrintPreviewDialog(LayoutHelper.FindParentObject<Window>(this), (IPrintableControl)TreeListControl.View, documentName);
        }
        protected virtual void OnShowPrintPreviewInNewTab(string documentName) {
            PrintableControlLink link = new PrintableControlLink((IPrintableControl)TreeListControl.View);
            DocumentPreviewControl preview = new DocumentPreviewControl() { DocumentSource = link };
            DXTabItem tabItem = CreateTabItem(preview, documentName);
            tabItem.Tag = link;
            DXTabControl.Items.Add(tabItem);
            DXTabControl.SelectedItem = tabItem;
            link.CreateDocument(true);
        }
        protected virtual DXTabItem CreateTabItem(DocumentPreviewControl preview, string documentName) {
            return new DXTabItem() { AllowHide = DefaultBoolean.True, Content = preview, Header = documentName };
        }
        protected void RemoveTab(DXTabItem tabItem) {
            if(tabItem.Tag != null)
                ((PrintableControlLink)tabItem.Tag).Dispose();
            tabItem.Content = null;
            DXTabControl.Items.Remove(tabItem);
        }
        protected override void Clear() {
            base.Clear();
            for(int i = DXTabControl.Items.Count - 1; i >= 0; i--) {
                RemoveTab((DXTabItem)DXTabControl.Items[i]);
            }
            DXTabControl = null;
        }
        void OnTabHidden(object sender, TabControlTabHiddenEventArgs e) {
            RemoveTab((DXTabItem)DXTabControl.Items[e.TabIndex]);
        }
    }
}
