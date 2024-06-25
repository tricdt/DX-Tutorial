using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PropertyGridDemo {
    public enum BorderOptionsType {
        Empty,
        Solid
    }    
    public static class BorderOptionsExtensions {
        public static BorderOptions CreateBorderOptions(this BorderOptionsType type, SeriesOptions options) {
            switch (type) {
                case BorderOptionsType.Solid:
                    return ViewModelSource.Factory<SeriesOptions, SolidBorderOptions>(x => new SolidBorderOptions(x))(options);
            }
            return ViewModelSource.Factory<SeriesOptions, BorderOptions>(x => new BorderOptions(x))(options);
        }
    }
    [MetadataType(typeof(Metadata.BorderOptionsDataMetadata))]
    public class BorderOptionsData {
        public BorderOptionsData() {
            Color = Colors.Transparent;
            Opacity = 0d;
            Thickness = 0d;
            DashStyle = DashStyles.Solid;
        }
        public Color Color { get; set; }
        public double Opacity { get; set; }
        public double Thickness { get; set; }
        public virtual DashStyle DashStyle { get; set; }
    }
    [MetadataType(typeof(Metadata.BorderOptionsMetadata))]
    public class BorderOptions : BaseOptions {
        public BorderOptions() { }
        public BorderOptionsType BorderType { get; set; }
        public BorderOptions(SeriesOptions root) : base(root) { }
        public virtual BorderOptionsData Data { get; set; }
    }
    [MetadataType(typeof(Metadata.SolidBorderOptionsMetadata))]
    public class SolidBorderOptions : BorderOptions {
        public SolidBorderOptions() : this(null) { }
        public SolidBorderOptions(SeriesOptions root) : base(root) {
            Color = Colors.Black;
            Opacity = 1d;
            Thickness = 0d;
            DashStyle = DashStyles.Solid;

        }
        public virtual Color Color { get; set; }
        public virtual double Opacity { get; set; }
        public virtual double Thickness { get; set; }
        public virtual DashStyle DashStyle { get; set; }
        protected void OnColorChanged() {
            Opacity = (double)Color.A / 255;
            UpdateData();
        }
        protected void OnOpacityChanged() {
            Color = new Color() { A = (byte)(Opacity * 255), R = Color.R, G = Color.G, B = Color.B };
            UpdateData();
        }
        protected void OnThicknessChanged() {
            UpdateData();
        }
        protected void OnDashStyleChanged() {
            UpdateData();
        }

        protected void UpdateData() {
            Data = new BorderOptionsData() {
                Color = this.Color,
                Opacity = this.Opacity,
                Thickness = this.Thickness,
                DashStyle = this.DashStyle
            };
        }
    }
}
