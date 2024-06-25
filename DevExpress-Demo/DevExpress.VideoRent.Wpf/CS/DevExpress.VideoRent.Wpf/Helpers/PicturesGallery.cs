using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Collections;
using System.ComponentModel;
using DevExpress.Xpf.Bars;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class PicturesGallery : AddDeleteBar {
        #region Dependency Properties
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty CurrentPictureProperty;
        public static readonly DependencyProperty ShowBarProperty;
        static PicturesGallery() {
            Type ownerType = typeof(PicturesGallery);
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IList), ownerType, new PropertyMetadata(null));
            CurrentPictureProperty = DependencyProperty.Register("CurrentPicture", typeof(Picture), ownerType, new PropertyMetadata(null));
            ShowBarProperty = DependencyProperty.Register("ShowBar", typeof(bool), ownerType, new PropertyMetadata(true));
        }
        #endregion

        public PicturesGallery() {
            DefaultStyleKey = typeof(PicturesGallery);
            MainContent = this;
        }
        public IList ItemsSource { get { return (IList)GetValue(ItemsSourceProperty); } set { SetValue(ItemsSourceProperty, value); } }
        public bool ShowBar { get { return (bool)GetValue(ShowBarProperty); } set { SetValue(ShowBarProperty, value); } }
        public Picture CurrentPicture { get { return (Picture)GetValue(CurrentPictureProperty); } set { SetValue(CurrentPictureProperty, value); } }
    }
}
