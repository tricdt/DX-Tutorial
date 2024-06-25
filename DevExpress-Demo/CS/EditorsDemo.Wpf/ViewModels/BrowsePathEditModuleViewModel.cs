using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Utils.Svg;
using DevExpress.Xpf.Core.Native;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EditorsDemo {
    public class BrowsePathEditModuleViewModel : ViewModelBase {

        string sourceFilePath;
        public string SourceFilePath {
            get { return sourceFilePath; }
            set {
                if(sourceFilePath == value)
                    return;
                sourceFilePath = value;

                RaisePropertyChanged(() => SourceFilePath);

                if(!File.Exists(sourceFilePath))
                    return;

                SourceImage = WpfSvgRenderer.CreateImageSource(SvgImage.FromFile(SourceFilePath));

                ResultFilePath = Path.Combine(
                    Path.GetDirectoryName(sourceFilePath),
                    string.Format("{0}.png", Path.GetFileNameWithoutExtension(sourceFilePath))
                );
            }
        }
        public string ResultFilePath {
            get { return GetProperty(() => ResultFilePath); }
            set { SetProperty(() => ResultFilePath, value); }
        }

        public ImageSource SourceImage {
            get { return GetProperty(() => SourceImage); }
            set { SetProperty(() => SourceImage, value); }
        }

        public int ResultFileSizeKilobytes {
            get { return GetProperty(() => ResultFileSizeKilobytes); }
            set { SetProperty(() => ResultFileSizeKilobytes, value); }
        }

        IMessageBoxService messageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }

        bool CheckCanConvert() {
            if(!File.Exists(SourceFilePath)) {
                messageBoxService.ShowMessage("Specify the source file.", "Warning", MessageButton.OK, MessageIcon.Warning);
                return false;
            }
            if(string.IsNullOrEmpty(ResultFilePath)) {
                messageBoxService.ShowMessage("Specify the destination file.", "Warning", MessageButton.OK, MessageIcon.Warning);
                return false;
            }
            return true;
        }

        [Command]
        public void Convert() {
            if(!CheckCanConvert())
                return;
            try {
                var k = SourceImage.Width / SourceImage.Height;
                var height = 200d;
                var width = height * k;
                var img = new System.Windows.Controls.Image { Source = SourceImage };
                img.Arrange(new Rect(0, 0, width, height));

                var bitmap = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(img);

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using(var stream = new FileStream(ResultFilePath, FileMode.Create)) {
                    encoder.Save(stream);
                    ResultFileSizeKilobytes = (int)(stream.Length / 1000);
                }

                if(messageBoxService.Show("Operation completed. Do you want to open the destination file and review the processed image?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    DevExpress.Data.Utils.SafeProcess.Start(ResultFilePath);
                }
            } catch(Exception ex) {
                messageBoxService.ShowMessage(ex.Message, "Error", MessageButton.OK, MessageIcon.Error);
            }
        }

        [Command]
        public void Clear() {
            SourceImage = null;
            SourceFilePath = null;
            ResultFilePath = null;
            ResultFileSizeKilobytes = 0;
        }
    }
}
