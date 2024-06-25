using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace WindowsUIDemo {
    public partial class AppBarCommands : WindowsUIDemoModule {
        public AppBarCommands() {
            InitializeComponent();
        }
    }
    [POCOViewModel]
    public class PhotoCollection {
        public virtual List<Photo> Photos { get; set; }
        public virtual Photo SelectedItem { get; set; }
        public PhotoCollection() {
            Photos = new List<Photo>();
            AddPhoto("Mercedes-Benz", "/WindowsUIDemo;component/Images/Photos/Cars/0.jpg");
            AddPhoto("Mercedes-Benz", "/WindowsUIDemo;component/Images/Photos/Cars/1.jpg");
            AddPhoto("BMW", "/WindowsUIDemo;component/Images/Photos/Cars/2.jpg");
            AddPhoto("Rolls-Royce", "/WindowsUIDemo;component/Images/Photos/Cars/3.jpg");
            AddPhoto("Jaguar", "/WindowsUIDemo;component/Images/Photos/Cars/4.jpg");
            AddPhoto("Lexus", "/WindowsUIDemo;component/Images/Photos/Cars/5.jpg");
            AddPhoto("Lexus", "/WindowsUIDemo;component/Images/Photos/Cars/6.jpg");
            AddPhoto("Ford", "/WindowsUIDemo;component/Images/Photos/Cars/7.jpg");
            AddPhoto("Dodge", "/WindowsUIDemo;component/Images/Photos/Cars/8.jpg");
            AddPhoto("GMC", "/WindowsUIDemo;component/Images/Photos/Cars/9.jpg");
            AddPhoto("Nissan", "/WindowsUIDemo;component/Images/Photos/Cars/10.jpg");
        }
        void AddPhoto(string caption, string uri) {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(uri, UriKind.Relative);
            image.EndInit();
            Photos.Add(ViewModelSource.Create(() => new Photo { Caption = caption, SizeInfo = "700x467", Source = image, ViewSize = new Size(150, 100) }));
        }
        [Command]
        public void RotateClockwise() {
            if(SelectedItem != null) {
                switch(SelectedItem.Rotation) {
                    case Rotation.Rotate0:
                        SelectedItem.Rotation = Rotation.Rotate90;
                        break;
                    case Rotation.Rotate90:
                        SelectedItem.Rotation = Rotation.Rotate180;
                        break;
                    case Rotation.Rotate180:
                        SelectedItem.Rotation = Rotation.Rotate270;
                        break;
                    case Rotation.Rotate270:
                        SelectedItem.Rotation = Rotation.Rotate0;
                        break;
                }
            }
        }
        [Command]
        public void RotateCounterclockwise() {
            if(SelectedItem != null) {
                switch(SelectedItem.Rotation) {
                    case Rotation.Rotate0:
                        SelectedItem.Rotation = Rotation.Rotate270;
                        break;
                    case Rotation.Rotate90:
                        SelectedItem.Rotation = Rotation.Rotate0;
                        break;
                    case Rotation.Rotate180:
                        SelectedItem.Rotation = Rotation.Rotate90;
                        break;
                    case Rotation.Rotate270:
                        SelectedItem.Rotation = Rotation.Rotate180;
                        break;
                }
            }
        }
        [Command]
        public void Rotate180() {
            if(SelectedItem != null) {
                switch(SelectedItem.Rotation) {
                    case Rotation.Rotate0:
                        SelectedItem.Rotation = Rotation.Rotate180;
                        break;
                    case Rotation.Rotate90:
                        SelectedItem.Rotation = Rotation.Rotate270;
                        break;
                    case Rotation.Rotate180:
                        SelectedItem.Rotation = Rotation.Rotate0;
                        break;
                    case Rotation.Rotate270:
                        SelectedItem.Rotation = Rotation.Rotate90;
                        break;
                }
            }
        }
        [Command]
        public void RotateReset() {
            if(SelectedItem != null) {
                SelectedItem.Rotation = Rotation.Rotate0;
            }
        }
        [Command]
        public void ZoomIn() {
            if(SelectedItem != null) SelectedItem.Scale += 0.1;
        }
        [Command]
        public void ZoomOut() {
            if(SelectedItem != null) SelectedItem.Scale = Math.Max(0.1, SelectedItem.Scale - 0.1);
        }
        [Command]
        public void ResetScale() {
            if(SelectedItem != null) SelectedItem.Scale = 1;
        }
        [Command]
        public void Print() {
            if(SelectedItem == null) return;
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true) {
                printDialog.PrintVisual(new Image() { Source = SelectedItem.Source }, SelectedItem.Caption);
            }
        }
        [Command]
        public void Flip() {
            if(SelectedItem != null) SelectedItem.Flip = !SelectedItem.Flip;
        }
    }
    [POCOViewModel]
    public class Photo {
        public ImageSource Source { get; set; }
        public string Caption { get; set; }
        public string SizeInfo { get; set; }
        public Size ViewSize { get; set; }
        public virtual Rotation Rotation { get; set; }
        public virtual double Scale { get; set; }
        public virtual bool Flip { get; set; }
        public Photo() {
            Scale = 1.1;
            ViewSize = new Size(double.NaN, double.NaN);
        }
    }
}
