using System;

namespace DevExpress.VideoRent.ViewModel.Helpers {
    public class CommandEventArgs : EventArgs {
        public CommandEventArgs(object parameter, object value) {
            Parameter = parameter;
            Value = value;
        }
        public object Parameter { get; private set; }
        public object Value { get; set; }
    }
    public delegate void CommandEventHandler(object sender, CommandEventArgs e);
}
