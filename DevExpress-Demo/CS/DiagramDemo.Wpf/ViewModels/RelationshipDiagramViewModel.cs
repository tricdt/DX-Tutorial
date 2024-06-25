using System;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Diagram.Demos;
using DevExpress.Mvvm.DataAnnotations;

namespace DiagramDemo {
    public class RelationshipDiagramViewModel : ViewModelBase {
        public Employee[] Employees { get; private set; }
        public RelationshipInfo[] Relationships { get; private set; }
        [BindableProperty]
        public virtual Employee[] FriendsCollection { get; protected set; }
        [BindableProperty]
        public virtual Employee[] KnownPersonsCollection { get; protected set; }

        public virtual Employee SelectedEmployee { get; set; }

        public RelationshipDiagramViewModel(Employee[] employees) {
            Employees = RelationshipsData.GetEmployees();
            Relationships = RelationshipsData.GetRelationships(Employees);
        }

        protected void OnSelectedEmployeeChanged() {
            FriendsCollection = RelationshipsData.GetEmployeeRelationships(SelectedEmployee, Relationships, EmployeeRelationship.Friends);
            KnownPersonsCollection = RelationshipsData.GetEmployeeRelationships(SelectedEmployee, Relationships, EmployeeRelationship.KnowEachOther);
        }
    }
}
