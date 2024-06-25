using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.ComponentModel;
using System.Windows.Media;
using DevExpress.Xpf.Bars;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class PhotoViewer : AddDeleteBar {
        #region Dependency Properties
        public static readonly DependencyProperty NextItemGlyphProperty;
        public static readonly DependencyProperty PrevItemGlyphProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty CurrentPictureProperty;
        public static readonly DependencyProperty CanShowNextProperty;
        public static readonly DependencyProperty CanShowPrevProperty;
        static PhotoViewer() {
            Type ownerType = typeof(PhotoViewer);
            NextItemGlyphProperty = DependencyProperty.Register("NextItemGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null));
            PrevItemGlyphProperty = DependencyProperty.Register("PrevItemGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null));
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IBindingList), ownerType, new PropertyMetadata(null,
                (d, e) => ((PhotoViewer)d).RaiseItemsSourceChanged(e)));
            CurrentPictureProperty = DependencyProperty.Register("CurrentPicture", typeof(Picture), ownerType, new PropertyMetadata(null,
                (d, e) => ((PhotoViewer)d).RaiseCurrentPictureChanged(e)));
            CanShowNextProperty = DependencyProperty.Register("CanShowNext", typeof(bool), ownerType, new PropertyMetadata(false));
            CanShowPrevProperty = DependencyProperty.Register("CanShowPrev", typeof(bool), ownerType, new PropertyMetadata(false));
        }
        #endregion
        int currentPictureListIndex = 0;

        public PhotoViewer() {
            DefaultStyleKey = typeof(PhotoViewer);
            MainContent = this;
        }
        public ImageSource NextItemGlyph { get { return (ImageSource)GetValue(NextItemGlyphProperty); } set { SetValue(NextItemGlyphProperty, value); } }
        public ImageSource PrevItemGlyph { get { return (ImageSource)GetValue(PrevItemGlyphProperty); } set { SetValue(PrevItemGlyphProperty, value); } }
        public IList ItemsSource { get { return (IList)GetValue(ItemsSourceProperty); } set { SetValue(ItemsSourceProperty, value); } }
        public Picture CurrentPicture { get { return (Picture)GetValue(CurrentPictureProperty); } set { SetValue(CurrentPictureProperty, value); } }
        public bool CanShowNext { get { return (bool)GetValue(CanShowNextProperty); } set { SetValue(CanShowNextProperty, value); } }
        public bool CanShowPrev { get { return (bool)GetValue(CanShowPrevProperty); } set { SetValue(CanShowPrevProperty, value); } }
        public void ShowNext() {
            if(!CanShowNext) return;
            ++currentPictureListIndex;
            UpdateCurrentPicture();
        }
        public void ShowPrev() {
            if(!CanShowPrev) return;
            --currentPictureListIndex;
            UpdateCurrentPicture();
        }
        void RaiseCurrentPictureChanged(DependencyPropertyChangedEventArgs e) {
            if(ItemsSource == null || CurrentPicture == null) return;
            currentPictureListIndex = ItemsSource.IndexOf(CurrentPicture);
            if(currentPictureListIndex < 0)
                currentPictureListIndex = 0;
        }
        void RaiseItemsSourceChanged(DependencyPropertyChangedEventArgs e) {
            IBindingList oldValue = (IBindingList)e.OldValue;
            IBindingList newValue = (IBindingList)e.NewValue;
            if(oldValue != null)
                oldValue.ListChanged -= OnItemsSourceListChanged;
            if(newValue != null) {
                newValue.ListChanged += OnItemsSourceListChanged;
                OnItemsSourceListChanged(newValue, new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
        void OnItemsSourceListChanged(object sender, ListChangedEventArgs e) {
            UpdateCurrentPicture();
        }
        void UpdateCurrentPicture() {
            if(ItemsSource == null || ItemsSource.Count == 0) {
                CanShowNext = false;
                CanShowPrev = false;
                CurrentPicture = null;
                return;
            }
            if(currentPictureListIndex >= ItemsSource.Count)
                currentPictureListIndex = 0;
            CanShowPrev = currentPictureListIndex > 0;
            CanShowNext = currentPictureListIndex < ItemsSource.Count - 1;
            if(CurrentPicture != null && ItemsSource.IndexOf(CurrentPicture) == currentPictureListIndex) return;
            CurrentPicture = (Picture)ItemsSource[currentPictureListIndex];
        }
        void OnPrevItemItemClick(object sender, ItemClickEventArgs e) { ShowPrev(); }
        void OnNextItemItemClick(object sender, ItemClickEventArgs e) { ShowNext(); }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            BarItem nextItem = (BarItem)GetTemplateChild("NextItem");
            BarItem prevItem = (BarItem)GetTemplateChild("PrevItem");
            nextItem.ItemClick += OnNextItemItemClick;
            prevItem.ItemClick += OnPrevItemItemClick;
        }
    }
}
