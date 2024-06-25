using System;

namespace EditorsDemo {
    public partial class TimePickerModule : EditorsDemoModule {
        public TimePickerModule() {
            InitializeComponent();
            DateEditWithNavigatorAndTimePicker.DateTime = DateTime.Now;
            DateEditWithTimePicker.DateTime = DateTime.Now;
        }
    }
}
