using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DevExpress.Xpf.PropertyGrid;

namespace PropertyGridDemo {
    public partial class ExpandabilityCustomizationModule : PropertyGridDemoModule {
        public ExpandabilityCustomizationModule() {
            InitializeComponent();
            DataContext = new Container {
                Simple = new ClassWithProperties { Id = 0, Name = "Apple" },
                Expandable = new ClassWithProperties { Id = 1, Name = "Pinapple" },
                NotExpandable = new ClassWithProperties { Id = 2, Name = "Orange" }
            };
        }
        void PropertyGridControl_OnCustomExpand(object sender, CustomExpandEventArgs args) {
            if(args.IsInitializing)
                args.IsExpanded = true;
        }
    }
    public class Container {
        public ClassWithProperties Simple { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ClassWithProperties Expandable { get; set; }
        [TypeConverter(typeof(NotExpandableConverter))]
        public ClassWithProperties NotExpandable { get; set; }
    }
    public class ClassWithProperties {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() {
            return Name;
        }
    }
    public class NotExpandableConverter : TypeConverter {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
            return false;
        }
    }
}
