using System;
using System.Collections.Generic;
using System.Linq;


namespace DevExpress.Diagram.Demos {
    public static class RelationshipsData {
        public static Employee[] GetEmployees() {
            return EmployeesData.FilteredEmployees.Take(9).ToArray();
        }
        public static RelationshipInfo[] GetRelationships(Employee[] employees) {
            int index = 0;
            var relations = new List<RelationshipInfo>();
            for(int i = 0; i < employees.Length; i++) {
                for(int j = i + 1; j < employees.Length; j++) {
                    if((index % 4) <= 1)
                        relations.Add(new RelationshipInfo(employees[i], employees[j], (EmployeeRelationship)(index % 4)));
                    index++;
                }
            }
            return relations.ToArray();
        }
        public static Employee[] GetEmployeeRelationships(Employee employee, IEnumerable<RelationshipInfo> relationships, EmployeeRelationship relationshipKind) {
            return relationships
                .Where(x => x.Relationship == relationshipKind)
                .Select(x => GetRelationship(employee, x))
                .Where(x => x != null)
                .ToArray();
        }
        static Employee GetRelationship(Employee employee, RelationshipInfo x) {
            if(x.Source == employee)
                return x.Target;
            if(x.Target == employee)
                return x.Source;
            return null;
        }
    }
    public class RelationshipInfo {
        public RelationshipInfo(Employee source, Employee target, EmployeeRelationship relationship) {
            Source = source;
            Target = target;
            Relationship = relationship;
        }

        public Employee Source { get; private set; }
        public Employee Target { get; private set; }
        public EmployeeRelationship Relationship { get; private set; }
    }

    public enum EmployeeRelationship {
        KnowEachOther,
        Friends
    }
}
