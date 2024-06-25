using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace GanttDemo.Examples {
    public partial class CustomizeWorkingTime : UserControl {
        static CustomizeWorkingTime() {
            Holidays = DateTimeHelper.GetHolidays(DateTime.Today.AddYears(-1), DateTime.Today.AddYears(1)).ToArray();
        }

        public static IEnumerable<DateTime> Holidays { get; private set; }

        public CustomizeWorkingTime() {
            InitializeComponent();
        }
    }
}
