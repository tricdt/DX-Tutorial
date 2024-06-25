using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Windows.Threading;
using DevExpress.Xpf.Core.Native;
using System.Globalization;
using DevExpress.Xpf.Editors.DateNavigator;

namespace EditorsDemo {
    public partial class DateNavigatorModule : EditorsDemoModule {
        bool disableWeekend;
        bool highlightUSHolidays;
        public DateNavigatorModule() {
            InitializeComponent();
            navigator.RequestCellState += NavigatorOnRequestCellState;
            styleSettingsComboBox.ItemsSource = new List<StyleSettingsViewModel>() {
                new StyleSettingsViewModel() { Name = "Outlook", Value = new DateNavigatorOutlookStyleSettings() },
                new StyleSettingsViewModel() { Name = "Classic", Value = new DateNavigatorStyleSettings() }
            };
            styleSettingsComboBox.SelectedIndex = 0;
        }
        void NavigatorOnRequestCellState(object sender, DateNavigatorRequestCellStateEventArgs args) {
            if (disableWeekend && (args.DateTime.DayOfWeek == DayOfWeek.Saturday || args.DateTime.DayOfWeek == DayOfWeek.Sunday))
                args.CellState |= DateNavigatorCellState.IsDisabled;
            if (highlightUSHolidays && IsUSHoliday(args.DateTime))
                args.CellState |= DateNavigatorCellState.IsHoliday;
        }
        void OnDisabledWeekendEditValueChanged(object sender, EditValueChangedEventArgs e) {
            disableWeekend = (bool)e.NewValue;
            navigator.RefreshCellStates();
        }
        void OnHighlightUSHolidaysEditValueChanged(object sender, EditValueChangedEventArgs e) {
            highlightUSHolidays = (bool)e.NewValue;
            navigator.RefreshCellStates();
        }
        void OnStyleSettingsEditValueChanged(object sender, EditValueChangedEventArgs e) {
            navigator.StyleSettings = (DateNavigatorStyleSettingsBase)e.NewValue;
        }
        bool IsUSHoliday(DateTime date) {
            int nthWeekDay    = (int)(Math.Ceiling((double)date.Day / 7.0d));
            DayOfWeek dayName = date.DayOfWeek;
            bool isThursday   = dayName == DayOfWeek.Thursday;
            bool isFriday     = dayName == DayOfWeek.Friday;
            bool isMonday     = dayName == DayOfWeek.Monday;
            bool isWeekend    = dayName == DayOfWeek.Saturday || dayName == DayOfWeek.Sunday;
        
            
            if ((date.Month == 12 && date.Day == 31 && isFriday) ||
                (date.Month == 1 && date.Day == 1 && !isWeekend) ||
                (date.Month == 1 && date.Day == 2 && isMonday)) return true;
        
            
            if (date.Month == 1 && isMonday && nthWeekDay == 3) return true;
        
            
            if (date.Month == 2 && isMonday && nthWeekDay == 3) return true;
        
            
            if (date.Month == 5 && isMonday && date.AddDays(7).Month == 6) return true;
        
            
            if ((date.Month == 7 && date.Day == 3 && isFriday) ||
                (date.Month == 7 && date.Day == 4 && !isWeekend) ||
                (date.Month == 7 && date.Day == 5 && isMonday)) return true;
        
            
            if (date.Month == 9 && isMonday && nthWeekDay == 1) return true;
        
            
            if (date.Month == 10 && isMonday && nthWeekDay == 2) return true;
        
            
            if ((date.Month == 11 && date.Day == 10 && isFriday) ||
                (date.Month == 11 && date.Day == 11 && !isWeekend) ||
                (date.Month == 11 && date.Day == 12 && isMonday)) return true;
        
            
            if (date.Month == 11 && isThursday && nthWeekDay == 4) return true;
        
            
            if ((date.Month == 12 && date.Day == 24 && isFriday) ||
                (date.Month == 12 && date.Day == 25 && !isWeekend) ||
                (date.Month == 12 && date.Day == 26 && isMonday)) return true;
        
            return false;
        }
    }

    public class StyleSettingsViewModel {
        public string Name { get; set; }
        public DateNavigatorStyleSettingsBase Value { get; set; }
    }
}
