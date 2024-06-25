using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PropertyGridDemo {
    public enum FillOptionsType {
        NoFill,
        SolidFill,
        PictureFill,       
    }
    [MetadataType(typeof(Metadata.FillOptionsMetadata))]
    public class FillOptions : BaseOptions {
        public FillOptions() {  }
        public FillOptions(SeriesOptions root) : base(root) {
        }
        public FillOptionsType FillType { get; set; }
        public virtual Brush Result { get; set; }
    }
    [MetadataType(typeof(Metadata.SolidFillOptionsMetadata))]
    public class SolidFillOptions : FillOptions {
        public SolidFillOptions() {  }
        public SolidFillOptions(SeriesOptions root) : base(root) { }
        public virtual Color Color { get; set; }
        public virtual Double Opacity { get; set; }
        protected void OnColorChanged() {
            Opacity = (double)Color.A / 255;
            UpdateBrush();
        }        
        protected void OnOpacityChanged() {
            Color = new Color() { A = (byte)(Opacity * 255), R = Color.R, G = Color.G, B = Color.B };
            UpdateBrush();
        }
        private void UpdateBrush() {            
            Result = new SolidColorBrush(Color);
            Result.Freeze();
        }
    }
    [MetadataType(typeof(Metadata.PictureFillOptionsMetadata))]
    public class PictureFillOptions : FillOptions {
        public PictureFillOptions() : this(null) { }
        public PictureFillOptions(SeriesOptions root) : base(root) { Opacity = 1; }
        public virtual BitmapImage Picture { get; set; }
        public virtual Double Opacity { get; set; }
        protected virtual IOpenFileDialogService OpenFileDialogService { get { return null; } }
        public void SelectPicture() {
            if (OpenFileDialogService.ShowDialog()) {
                Picture = new BitmapImage(new Uri(OpenFileDialogService.GetFullFileName()));
            }
        }
        public bool CanClearPicture() {
            return Picture != null;
        }
        public void ClearPicture() {
            Picture = null;
        }
        protected void OnPictureChanged() {
            UpdateBrush();
        }
        protected void OnOpacityChanged() {
            UpdateBrush();
        }
        private void UpdateBrush() {
            Result = Picture.With(x => new ImageBrush(x) { Opacity = Opacity });
        }
    }
    public static class FillOptionsExtensions {
        public static FillOptions CreateFillOptions(this FillOptionsType type, SeriesOptions options) {
            switch (type) {                
                case FillOptionsType.SolidFill:
                    return ViewModelSource.Factory<SeriesOptions, SolidFillOptions>(x=> new SolidFillOptions(x))(options);
                case FillOptionsType.PictureFill:
                    return ViewModelSource.Factory<SeriesOptions, PictureFillOptions>(x => new PictureFillOptions(x))(options);
            }
            return ViewModelSource.Factory<SeriesOptions, FillOptions>(x => new FillOptions(x))(options);
        }
    }
}
