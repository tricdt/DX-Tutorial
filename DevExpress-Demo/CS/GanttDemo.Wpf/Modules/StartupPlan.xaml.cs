using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace GanttDemo {
    [CodeFile("ViewModels/StartupBusinessPlanViewModel.cs")]
    public partial class StartupPlan : GanttDemoModule {
        public StartupPlan() {
            InitializeComponent();
        }
    }
    
    public class StripLineTemplateSelector: DataTemplateSelector {
        public DataTemplate StripLineTemplate { get; set; }
        public DataTemplate WeeklyStripLineTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if(item is GanttStripLine)
                return StripLineTemplate;
            if(item is WeeklyStripLine)
                return WeeklyStripLineTemplate;
            return base.SelectTemplate(item, container);
        }
    }

    public class DayOfWeekToDaysOfWeekConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Enum.Parse(typeof(DaysOfWeek), Enum.GetName(typeof(DayOfWeek), value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
