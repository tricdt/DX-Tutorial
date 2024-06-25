using System;

namespace DevExpress.VideoRent.ViewModel.Helpers {
    public class SaveLoadLayoutDataEventArgs : EventArgs {
        public SaveLoadLayoutDataEventArgs(bool clearing) {
            Clearing = clearing;
        }
        public bool Clearing { get; private set; }
    }
    public delegate void SaveLoadLayoutDataEventHandler(object sender, SaveLoadLayoutDataEventArgs e);
}
