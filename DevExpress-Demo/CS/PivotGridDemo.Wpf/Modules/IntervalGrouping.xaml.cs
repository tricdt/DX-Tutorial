using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class IntervalGrouping : PivotGridDemoModule {
        public class GroupIntervalItem {
            public FieldGroupInterval GroupInterval { get; set; }
            public string Caption { get; set; }
        }

        public IntervalGrouping() {
            InitializeComponent();
        }

        public static IEnumerable<object> FieldGroupIntervals {
            get {
                return new[] {
                     new GroupIntervalItem { GroupInterval = FieldGroupInterval.Default, Caption = "Exact Date" },
					
                     new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateYear, Caption = "Year" },
					 new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateQuarter, Caption = "Quarter" },
					 new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateMonth, Caption = "Month" },
					 new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateDay, Caption = "Day" },

					 new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateMonthYear, Caption = "Month-Year" },
					 new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateQuarterYear, Caption = "Quarter-Year" },
					 new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateWeekYear, Caption = "Week-Year" },
					 new GroupIntervalItem { GroupInterval = FieldGroupInterval.Date, Caption = "Date" }
				};
            }
        }

		public static IEnumerable<object> FieldExtendedGroupIntervals {
			get {
				  return new[] {
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.Default, Caption = "Exact Date" },

					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateYear, Caption = "Year" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateQuarter, Caption = "Quarter" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateMonth, Caption = "Month" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateDay, Caption = "Day" },

					new GroupIntervalItem { GroupInterval = FieldGroupInterval.Hour, Caption = "Hour" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.Minute, Caption = "Minute" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.Second, Caption = "Second" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateDayOfYear, Caption = "Day of Year" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateDayOfWeek, Caption = "Day of Week" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateWeekOfYear, Caption = "Week of Year" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateWeekOfMonth, Caption = "Week of Month" },

					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateMonthYear, Caption = "Month-Year" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateQuarterYear, Caption = "Quarter-Year" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateWeekYear, Caption = "Week-Year" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.Date, Caption = "Date" },

					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateHour, Caption = "Date-Hour" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateHourMinute, Caption = "Date-Minute" },
					new GroupIntervalItem { GroupInterval = FieldGroupInterval.DateHourMinuteSecond, Caption = "Date-Hour-Minute-Second" }
				  };
			}
		}

        void pivotGrid_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e) {
			if(object.ReferenceEquals(e.Field, fieldOrderDate)) {
				if(((DataSourceColumnBinding)e.Field.DataBinding).GroupInterval == FieldGroupInterval.DateWeekYear) {
					DateTime date = (DateTime)e.Value;
					CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
					int weekNumber = culture.Calendar.GetWeekOfYear(date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
					e.DisplayText = string.Format("Week {0} of {1}", weekNumber, date.Year);
					if(e.ValueType == FieldValueType.Total)
						e.DisplayText += " Total";
				}
			}
		}
	}
}
