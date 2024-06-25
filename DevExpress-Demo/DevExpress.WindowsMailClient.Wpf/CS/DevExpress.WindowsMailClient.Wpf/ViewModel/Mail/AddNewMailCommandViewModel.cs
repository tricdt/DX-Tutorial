using DevExpress.Mvvm;
using DevExpress.WindowsMailClient.Wpf.Internal;
using System;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class AddNewMailCommandViewModel : CommandViewModel {
        public static AddNewMailCommandViewModel Create(string content, Action action, string glyph) {
            return new AddNewMailCommandViewModel() {
                Content = content,
                Command = new DelegateCommand(action),
                Glyph = ImageSourceHelper.CreateSvgImageSource(glyph) 
            };
        }
    }
}
