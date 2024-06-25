using System.Collections.Generic;
using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {
    [CodeFile("ViewModels/OutlookViewModel.(cs)")]
    public partial class DatePickerModule : EditorsDemoModule {
        public DatePickerModule() {
            InitializeComponent();
            InitSources();
            DataContext = new OutlookViewModel();
        }
        void InitSources() {
            List<MaskWrapper> dateMasks = new List<MaskWrapper>();
            dateMasks.Add(new MaskWrapper() { MaskName = "Short Date", MaskString = "d" });
            dateMasks.Add(new MaskWrapper() { MaskName = "Long Date", MaskString = "D" });
            dateMasks.Add(new MaskWrapper() { MaskName = "Month & Day", MaskString = "m" });
            dateMasks.Add(new MaskWrapper() { MaskName = "Year & Month", MaskString = "y" });
            dateMask.ItemsSource = dateMasks;
        }
        public class MaskWrapper {
            public string MaskName { get; set; }
            public string MaskString { get; set; }
        }
    }
}
