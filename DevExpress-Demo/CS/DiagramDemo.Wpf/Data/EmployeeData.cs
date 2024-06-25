using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DevExpress.Diagram.Demos {
    [XmlRoot("Employees")]
    public class EmployeesWithPhotoData : List<Employee> {
    }
    public class Employee {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string JobTitle { get; set; }
        public string GroupName { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string StateProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string CountryRegionName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Title { get; set; }
        public byte[] CroppedImageData { get; set; }
        public byte[] ImageData { get; set; }
        public override string ToString() {
            return FirstName + " " + LastName;
        }
    }
    public static class EmployeesData {
        public static readonly EmployeesWithPhotoData FilteredEmployees;

        static EmployeesData() {
            using(var stream = DiagramDemoFileHelper.GetDataStream("FilteredEmployeesWithPhoto.xml")) {
                var serializer = new XmlSerializer(typeof(EmployeesWithPhotoData));
                FilteredEmployees = (EmployeesWithPhotoData)serializer.Deserialize(stream);
            }
        }
        public static IEnumerable<object> GetOrgChartEmployees() {
            EmployeesWithPhotoData allEmployees;
            using(var stream = DiagramDemoFileHelper.GetDataStream("EmployeesWithPhoto.xml")) {
                var serializer = new XmlSerializer(typeof(EmployeesWithPhotoData));
                allEmployees = (EmployeesWithPhotoData)serializer.Deserialize(stream);
            }
            foreach(var pair in idMap) {
                foreach(var childID in pair.Value) {
                    var copyID = childID;
                    allEmployees.Find(x => Equals(copyID, x.Id)).ParentId = pair.Key;
                }
            }
            return allEmployees;
        }
        #region id map
        static Dictionary<int, int[]> idMap = new Dictionary<int, int[]>() {
            
            { 109 , new [] { 42, 117, 102 } },
            
            { 42 , new [] { 149, 150, 28 } },
            { 117 , new [] { 188, 6 } },
            { 102 , new [] { 46 } },
            
            { 149 , new [] { 119 } },
            { 150 , new [] { 140, 30, 191 } },
            { 28 , new [] { 82, 70 } },
            { 188 , new [] { 71, 274 } },
            { 6 , new [] { 261 } },
            { 46 , new [] { 266, 103, 139, 216 } },
            
            { 119 , new [] { 130, 12 } },
            { 30 , new [] { 3 } },
            { 191 , new [] { 263, 5 } },
            { 82 , new [] { 265, 11, 4 } },
            { 71 , new [] { 270, 217 } },
            { 274 , new [] { 79, 114, 273 } },
            { 266 , new [] { 268 }  },
            { 103 , new [] { 278, 283, 276 } },
            { 139 , new [] { 290, 284 } },
            { 216 , new [] { 287 } },
            
            { 130 , new [] { 281, 288 } },
            { 12  , new [] { 280, 277 } },
            { 263 , new [] { 275, 148, 218 } },
            { 114 , new [] { 225, 49} },
            { 283 , new [] { 260, 21 } },
            { 287 , new [] { 200 } },
            
            { 288 , new [] { 44 } },
            { 148 , new [] { 206, 41, 145 } },
        };
        #endregion
    }
}
