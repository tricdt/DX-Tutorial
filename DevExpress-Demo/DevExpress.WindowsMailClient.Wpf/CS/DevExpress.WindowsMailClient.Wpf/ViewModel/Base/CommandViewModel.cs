using DevExpress.Mvvm;
using DevExpress.WindowsMailClient.Wpf.Internal;
using System;
using System.Windows.Input;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class CommandViewModel : ContentViewModel {
        public static CommandViewModel Create(object content, string glyph, Action executeMethod, Func<bool> canExecuteMethod) {
            return new CommandViewModel {
                Content = content,
                Glyph = ImageSourceHelper.CreateSvgImageSource(glyph),
                Command = new DelegateCommand(executeMethod, canExecuteMethod)
            };
        }
        public CommandViewModel() { }

        public virtual ICommand Command { get; set; }
    }
}
