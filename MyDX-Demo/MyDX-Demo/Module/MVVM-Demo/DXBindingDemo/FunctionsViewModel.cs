using DevExpress.Mvvm;
using MyDX_Demo.Module.MVVM_Demo.Data;
using System.Linq;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.DXBindingDemo
{
    public class FunctionsViewModel
    {
        public class PersonData : BindableBase
        {
            public string FullName { get; private set; }
            readonly string country;

            public PersonData(string fullName, string country)
            {
                FullName = fullName;
                this.country = country;
            }

            public Address LoadAddress()
            {
                return new Address(country);
            }
        }
        public class Address : BindableBase 
        {
            public Address(string country)
            {
                Country = country;
            }

            public string Country { get; private set; }
        }
        public virtual PersonData Person { get; protected set; }

        int personIndex;
        readonly PersonData[] persons;
        public void Next()
        {
            Person = persons[personIndex % 3];
            personIndex++;
        }
        public virtual string UserName { get; set; }
        public virtual bool AcceptTerms { get; set; }
        public void Register()
        {
            MessageBox.Show("Registered");
        }
        protected FunctionsViewModel()
        {
            string[] countries = new[] { "Norway", "Denmark", "Sweden" };
            persons = PersonInfo.Persons
                .Select((person, index) => new PersonData(person.FullName, countries[index]))
                .ToArray();
            Next();
        }
    }
}
