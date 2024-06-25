using System;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Diagram.Demos;

namespace DiagramDemo {
    public class OrgChartViewModel : ViewModelBase {
        public event EventHandler SelectedTemplateChanged;

        public EmployeeTemplate[] Templates { get; private set; }
        public virtual EmployeeTemplate SelectedTemplate { get; set; }

        public Employee[] Employees { get; private set; }
        public virtual Employee SelectedEmployee { get; set; }

        public OrgChartViewModel(Employee[] employees, EmployeeTemplate[] templates) {
            this.Employees = employees;
            this.Templates = templates;
            SelectedTemplate = Templates.FirstOrDefault();
        }

        protected void OnSelectedTemplateChanged() {
            if(SelectedTemplateChanged != null)
                SelectedTemplateChanged(this, EventArgs.Empty);
        }
    }
    public class EmployeeTemplate {
        public string Name { get; private set; }
        public object Image { get; private set; }

        public EmployeeTemplate(string name, object image) {
            this.Name = name;
            this.Image = image;
        }
    }
}
