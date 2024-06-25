using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Bars;
using System.ComponentModel;
using System.Windows.Controls;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class AddDeleteBar : Control {
        #region Dependency Properties
        public static readonly DependencyProperty AddItemGlyphProperty;
        public static readonly DependencyProperty DeleteItemGlyphProperty;
        public static readonly DependencyProperty AddItemContentProperty;
        public static readonly DependencyProperty DeleteItemContentProperty;
        public static readonly DependencyProperty AddItemDisplayModeProperty;
        public static readonly DependencyProperty DeleteItemDisplayModeProperty;
        public static readonly DependencyProperty AllowDeleteProperty;
        public static readonly DependencyProperty AllowEditingProperty;
        public static readonly DependencyProperty AllowEditingItemGlyphProperty;
        public static readonly DependencyProperty AllowEditingItemContentProperty;
        public static readonly DependencyProperty BarDockInfoContainerTypeProperty;
        public static readonly DependencyProperty TopContentProperty;
        public static readonly DependencyProperty MainContentProperty;
        public static readonly DependencyProperty MainContentTemplateProperty;
        public static readonly DependencyProperty BottomContentProperty;
        public static readonly DependencyProperty IsAllowEditingItemVisibleProperty;
        static AddDeleteBar() {
            Type ownerType = typeof(AddDeleteBar);
            AddItemGlyphProperty = DependencyProperty.Register("AddItemGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null));
            DeleteItemGlyphProperty = DependencyProperty.Register("DeleteItemGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null));
            AddItemContentProperty = DependencyProperty.Register("AddItemContent", typeof(object), ownerType, new PropertyMetadata(null));
            DeleteItemContentProperty = DependencyProperty.Register("DeleteItemContent", typeof(object), ownerType, new PropertyMetadata(null));
            AddItemDisplayModeProperty = DependencyProperty.Register("AddItemDisplayMode", typeof(BarItemDisplayMode), ownerType, new PropertyMetadata(BarItemDisplayMode.Default));
            DeleteItemDisplayModeProperty = DependencyProperty.Register("DeleteItemDisplayMode", typeof(BarItemDisplayMode), ownerType, new PropertyMetadata(BarItemDisplayMode.Default));
            AllowDeleteProperty = DependencyProperty.Register("AllowDelete", typeof(bool), ownerType, new PropertyMetadata(false));
            AllowEditingProperty = DependencyProperty.Register("AllowEditing", typeof(bool), ownerType, new PropertyMetadata(false,
                (d, e) => ((AddDeleteBar)d).RaiseAllowEditingChanged(e)));
            AllowEditingItemGlyphProperty = DependencyProperty.Register("AllowEditingItemGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null));
            AllowEditingItemContentProperty = DependencyProperty.Register("AllowEditingItemContent", typeof(object), ownerType, new PropertyMetadata(null));
            BarDockInfoContainerTypeProperty = DependencyProperty.Register("BarDockInfoContainerType", typeof(BarContainerType), ownerType, new PropertyMetadata(BarContainerType.None));
            TopContentProperty = DependencyProperty.Register("TopContent", typeof(object), ownerType, new PropertyMetadata(null));
            MainContentProperty = DependencyProperty.Register("MainContent", typeof(object), ownerType, new PropertyMetadata(null,
                (d, e) => ((AddDeleteBar)d).RaiseMainContentChanged(e)));
            MainContentTemplateProperty = DependencyProperty.Register("MainContentTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            BottomContentProperty = DependencyProperty.Register("BottomContent", typeof(object), ownerType, new PropertyMetadata(null));
            IsAllowEditingItemVisibleProperty = DependencyProperty.Register("IsAllowEditingItemVisible", typeof(bool), ownerType, new PropertyMetadata(false));
        }
        #endregion
        BarItem addItem;
        BarItem deleteItem;

        public AddDeleteBar() {
            DefaultStyleKey = typeof(AddDeleteBar);
        }
        public ImageSource AddItemGlyph { get { return (ImageSource)GetValue(AddItemGlyphProperty); } set { SetValue(AddItemGlyphProperty, value); } }
        public ImageSource DeleteItemGlyph { get { return (ImageSource)GetValue(DeleteItemGlyphProperty); } set { SetValue(DeleteItemGlyphProperty, value); } }
        public object AddItemContent { get { return GetValue(AddItemContentProperty); } set { SetValue(AddItemContentProperty, value); } }
        public object DeleteItemContent { get { return GetValue(DeleteItemContentProperty); } set { SetValue(DeleteItemContentProperty, value); } }
        public BarItemDisplayMode AddItemDisplayMode { get { return (BarItemDisplayMode)GetValue(AddItemDisplayModeProperty); } set { SetValue(AddItemDisplayModeProperty, value); } }
        public BarItemDisplayMode DeleteItemDisplayMode { get { return (BarItemDisplayMode)GetValue(DeleteItemDisplayModeProperty); } set { SetValue(DeleteItemDisplayModeProperty, value); } }
        public bool AllowDelete { get { return (bool)GetValue(AllowDeleteProperty); } set { SetValue(AllowDeleteProperty, value); } }
        public bool AllowEditing { get { return (bool)GetValue(AllowEditingProperty); } set { SetValue(AllowEditingProperty, value); } }
        public ImageSource AllowEditingItemGlyph { get { return (ImageSource)GetValue(AllowEditingItemGlyphProperty); } set { SetValue(AllowEditingItemGlyphProperty, value); } }
        public object AllowEditingItemContent { get { return GetValue(AllowEditingItemContentProperty); } set { SetValue(AllowEditingItemContentProperty, value); } }
        public bool IsAllowEditingItemVisible { get { return (bool)GetValue(IsAllowEditingItemVisibleProperty); } set { SetValue(IsAllowEditingItemVisibleProperty, value); } }
        public BarContainerType BarDockInfoContainerType { get { return (BarContainerType)GetValue(BarDockInfoContainerTypeProperty); } set { SetValue(BarDockInfoContainerTypeProperty, value); } }
        public object TopContent { get { return GetValue(TopContentProperty); } set { SetValue(TopContentProperty, value); } }
        public object MainContent { get { return GetValue(MainContentProperty); } set { SetValue(MainContentProperty, value); } }
        public DataTemplate MainContentTemplate { get { return (DataTemplate)GetValue(MainContentTemplateProperty); } set { SetValue(MainContentTemplateProperty, value); } }
        public object BottomContent { get { return GetValue(BottomContentProperty); } set { SetValue(BottomContentProperty, value); } }
        public event EventHandler AddItemClick;
        public event EventHandler DeleteItemClick;
        protected virtual void OnAddItemItemClick(object sender, ItemClickEventArgs e) {
            if(AddItemClick != null)
                AddItemClick(this, EventArgs.Empty);
        }
        protected virtual void OnDeleteItemItemClick(object sender, ItemClickEventArgs e) {
            if(DeleteItemClick != null)
                DeleteItemClick(this, EventArgs.Empty);
        }
        protected virtual void RaiseMainContentChanged(DependencyPropertyChangedEventArgs e) { }
        protected virtual void RaiseAllowEditingChanged(DependencyPropertyChangedEventArgs e) { }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            this.addItem = (BarItem)GetTemplateChild("AddItem");
            this.deleteItem = (BarItem)GetTemplateChild("DeleteItem");
            if(this.addItem != null)
                this.addItem.ItemClick += OnAddItemItemClick;
            if(this.deleteItem != null)
                this.deleteItem.ItemClick += OnDeleteItemItemClick;
        }
    }
    public class AssociationsGrid : AddDeleteBar {
        #region Dependency Properties
        public static readonly DependencyProperty GridStyleProperty;
        public static readonly DependencyProperty ViewStyleProperty;
        static readonly DependencyProperty GridViewProperty;
        static AssociationsGrid() {
            Type ownerType = typeof(AssociationsGrid);
            GridStyleProperty = DependencyProperty.Register("GridStyle", typeof(Style), ownerType, new PropertyMetadata(null));
            ViewStyleProperty = DependencyProperty.Register("ViewStyle", typeof(Style), ownerType, new PropertyMetadata(null));
            GridViewProperty = DependencyProperty.Register("GridView", typeof(DataViewBase), ownerType, new PropertyMetadata(null,
                (d, e) => ((AssociationsGrid)d).RaiseGridViewChanged(e)));
        }
        #endregion

        public AssociationsGrid() {
            DefaultStyleKey = typeof(AssociationsGrid);
            SetBinding(GridViewProperty, new Binding("MainContent.View") { Source = this, Mode = BindingMode.OneWay });
            SetBinding(AllowEditingProperty, new Binding("MainContent.View.AllowEditing") { Source = this, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        }
        public Style GridStyle { get { return (Style)GetValue(GridStyleProperty); } set { SetValue(GridStyleProperty, value); } }
        public Style ViewStyle { get { return (Style)GetValue(ViewStyleProperty); } set { SetValue(ViewStyleProperty, value); } }

        protected override void OnAddItemItemClick(object sender, ItemClickEventArgs e) {
            CloseEditor();
            base.OnAddItemItemClick(sender, e);
        }
        protected override void OnDeleteItemItemClick(object sender, ItemClickEventArgs e) {
            CloseEditor();
            base.OnDeleteItemItemClick(sender, e);
        }
        protected override void RaiseMainContentChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseMainContentChanged(e);
            GridControl gridControl = e.NewValue as GridControl;
            if(gridControl != null)
                gridControl.SetBinding(GridControl.StyleProperty, new Binding("GridStyle") { Source = this });
        }
        protected override void RaiseAllowEditingChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseAllowEditingChanged(e);
            CloseEditor();
        }
        GridControl GridControl { get { return MainContent as GridControl; } }
        DataViewBase GridView { get { return (GridViewBase)GetValue(GridViewProperty); } set { SetValue(GridViewProperty, value); } }
        void RaiseGridViewChanged(DependencyPropertyChangedEventArgs e) {
            GridViewBase gridView = (GridViewBase)e.NewValue;
            if(gridView != null)
                gridView.SetBinding(GridViewBase.StyleProperty, new Binding("ViewStyle") { Source = this });
        }
        void CloseEditor() {
            if(GridView != null)
                GridView.CloseEditor();
        }
    }
}
