using DevExpress.Xpf.DemoBase.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace GridDemo {
    public class Person {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class PersonGenerator {
        Random rnd = new Random();

        List<string> lastNames = Dump("LastNames.xml");
        List<string> firstNames = Dump("FirstNames.xml");
        List<string> cities = Dump("Cities.xml");
        List<string> streets = Dump("Streets.xml");

        static string[] employeeJobTitles = {
                "Accounting Manager",
                "Assistant Sales Agent",
                "Assistant Sales Representative",
                "Coordinator Foreign Markets",
                "Export Administrator",
                "International Marketing Manager",
                "Marketing Assistant",
                "Marketing Manager",
                "Marketing Representative",
                "Order Administrator",
                "Product Manager",
                "Purchasing Agent",
                "Purchasing Manager",
                "Regional Account Representative",
                "Sales Agent",
                "Sales Associate",
                "Sales Manager",
                "Sales Representative"
            };
        static string[] ownerJobTitles = {
                "Owner",
                "Owner/Marketing Assistant",
            };
        static string[] companySuffixes = {
            "General Partnership",
            "LP",
            "LLP",
            "LLP",
            "LLLP",
            "LLC",
            "PLLC",
            "Corp.",
            "Inc.",
            "PC",
            "Company",
            "Limited",
            "Urban Development",
            "Finance",
            "Mobile",
            "Foundation",
            "Association",
            "Bank",
            "Industries",
            "Motors",
            "Electric"
        };

        static List<string> Dump(string fileName) {
            var result = new List<string>();
            Assembly assembly = typeof(PersonGenerator).Assembly;
            var file = assembly.GetManifestResourceStream(DemoHelper.GetPath(assembly.GetName().Name + ".Data.", assembly) + fileName);
            if(file != null) {
                var reader = new XmlTextReader(file);
                while(reader.Read()) {
                    if(reader.NodeType == XmlNodeType.Text) {
                        string nodeValue = reader.Value;
                        if(!string.IsNullOrEmpty(nodeValue))
                            result.Add(reader.Value);
                    }
                }
            }
            return result;
        }

        public Person CreatePerson() {
            var person = new Person();
            string firstName = GenerateFirstName();
            string lastName = GenerateLastName();
            person.FullName = CreateFullName(firstName, lastName);
            string companyBaseName = GetRandomElement(lastNames);
            string companySuffix = GetRandomElement(companySuffixes);
            person.Company = CreateCompanyName(companyBaseName, companySuffix);
            person.JobTitle = GenerateJobTitle();
            person.City = GenerateCity();
            person.Address = GenerateAddress();
            person.Phone = GeneratePhone();
            person.Email = CreateEmail(firstName, lastName, companyBaseName);
            return person;
        }

        string GenerateFirstName() {
            return GetRandomElement(firstNames);
        }
        string GenerateLastName() {
            return GetRandomElement(lastNames);
        }
        string CreateFullName(string firstName, string lastName) {
            return firstName + " " + lastName;
        }
        string CreateCompanyName(string companyBase, string companySuffix) {
            return companyBase + " " + companySuffix;
        }
        string GenerateJobTitle() {
            if(rnd.NextDouble() > 0.9)
                return GetRandomElement(ownerJobTitles);
            return GetRandomElement(employeeJobTitles);
        }
        string GenerateCity() {
            return GetRandomElement(cities);
        }
        string GenerateAddress() {
            return string.Format("{0}, {1}-{2}", GetRandomElement(streets), rnd.Next(1, 99), rnd.Next(10, 999));
        }
        string GeneratePhone() {
            return string.Format("({0}) {1}-{2}", rnd.Next(100, 999), rnd.Next(100, 999), rnd.Next(1000, 9999));
        }
        string CreateEmail(string firstName, string lastName, string companyBaseName) {
            return firstName.ToLowerInvariant() + "." + lastName.ToLowerInvariant() + "@" + companyBaseName.ToLowerInvariant() + ".com";
        }
        T GetRandomElement<T>(IList<T> list) {
            return list[rnd.Next(0, list.Count - 1)];
        }
    }
}
