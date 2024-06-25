namespace MapDemo {
    public class PhotoGalleryItemControl : VisibleControl  {
        public PhotoGalleryItemControl() {
            DefaultStyleKey = typeof(PhotoGalleryItemControl);
            MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(OnMouseButtonUp);
        }
        void OnMouseButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            e.Handled = true;
        }
    }
}
