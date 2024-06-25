using DevExpress.DevAV;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProductsDemo.Modules {
    public class Contact : BindableBase, ICloneable {
        Name name;
        public Name Name {
            get { return name; }
            set {
                if(name != value) {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public string FirstName { get { return this.Name != null ? this.Name.FirstName : null; } }
        public string LastName { get { return this.Name != null ? this.Name.LastName : null; } }
        ImageSource photo;
        public ImageSource Photo {
            get { return photo; }
            set {
                if(photo != value) {
                    photo = value;
                    RaisePropertyChanged("Photo");
                }
            }
        }
        Address address;
        public Address Address {
            get { return address; }
            set {
                if(address != value) {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }
        string email;
        public string Email {
            get { return email; }
            set {
                if(email != value) {
                    email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }
        string phone;
        public string Phone {
            get { return phone; }
            set {
                if(phone != value) {
                    phone = value;
                    RaisePropertyChanged("Phone");
                }
            }
        }
        DateTime birthDate;
        public DateTime BirthDate {
            get { return birthDate; }
            set {
                if(birthDate != value) {
                    birthDate = value;
                    RaisePropertyChanged("BirthDate");
                }
            }
        }
        PersonPrefix prefix;
        public PersonPrefix Prefix {
            get { return prefix; }
            set {
                if(!object.Equals(prefix, value)) {
                    prefix = value;
                    RaisePropertyChanged("Prefix");
                }
            }
        }
        string notes;
        public string Notes {
            get { return notes; }
            set {
                if(notes != value) {
                    notes = value;
                    RaisePropertyChanged("Notes");
                }
            }
        }
        List<int> activity;
        public List<int> Activity {
            get { return activity; }
            set {
                if(activity != value) {
                    activity = value;
                    RaisePropertiesChanged("Activity");
                }
            }
        }

        public Contact() {
            Name = new Name();
            Address = new Address();
        }

        public void Assign(Contact contact) {
            this.Name.Assign(contact.Name);
            this.Photo = contact.Photo;
            this.Address.Assign(contact.Address);
            this.Email = contact.Email;
            this.Phone = contact.Phone;
            this.BirthDate = contact.BirthDate;
            this.Prefix = contact.Prefix;
            this.Notes = contact.Notes;
            this.Activity = contact.Activity;
            RaisePropertiesChanged(string.Empty);
        }

        public object Clone() {
            return new Contact() { Address = (Address)this.Address.Clone(), BirthDate = this.BirthDate, Email = this.Email, Prefix= this.Prefix, Name = (Name)this.Name.Clone(), Photo = this.Photo, Phone = this.Phone, Notes = this.Notes, Activity = this.Activity };
        }
    }

    public class Name : BindableBase, ICloneable {
        string firstName;
        public string FirstName {
            get { return firstName; }
            set {
                if(firstName != value) {
                    firstName = value;
                    RaisePropertyChanged("FirstName");
                    UpdateFullName();
                }
            }
        }
        string middleName;
        public string MiddleName {
            get { return middleName; }
            set {
                if(middleName != value) {
                    middleName = value;
                    RaisePropertyChanged("MiddleName");
                    UpdateFullName();
                }
            }
        }
        string lastName;
        public string LastName {
            get { return lastName; }
            set {
                if(lastName != value) {
                    lastName = value;
                    RaisePropertyChanged("LastName");
                    UpdateFullName();
                }
            }
        }
        string fullName;
        public string FullName {
            get { return fullName; }
            set {
                if(fullName != value) {
                    fullName = value;
                    RaisePropertyChanged("FullName");
                }
            }
        }
        ContactTitle title;
        public ContactTitle Title {
            get { return title; }
            set {
                if(title != value) {
                    title = value;
                    RaisePropertyChanged("Title");
                    UpdateFullName();
                }
            }
        }
        void UpdateFullName() {
            FullName = string.Format("{0}{1}{2}{3}", Title == ContactTitle.None ? String.Empty : GetFormatString(Title.ToString()), GetFormatString(FirstName), GetFormatString(MiddleName), GetFormatString(LastName));
        }
        string GetFormatString(string name) {
            if(string.IsNullOrEmpty(name))
                return string.Empty;
            return string.Format("{0} ", name);
        }
        public void Assign(Name name) {
            this.FirstName = name.FirstName;
            this.MiddleName = name.MiddleName;
            this.LastName = name.LastName;
            this.Title = name.Title;
        }
        public override string ToString() {
            return FullName;
        }
        public object Clone() {
            return new Name() { FirstName = this.FirstName, MiddleName = this.MiddleName, LastName = this.LastName, FullName = this.FullName, Title = this.Title };
        }
    }
    public class Address : BindableBase, ICloneable {
        public Address() { }
        internal Address(string addressString) {
            try {
                string[] lines = addressString.Split(',');
                this.AddressLine = lines[0].Trim();
                this.City = lines[1].Trim();
                this.State = lines[2].Trim().Substring(0, 2);
                string temp = lines[2].Trim();
                this.Zip = temp.Substring(3, temp.Length - 3);
            }
            catch { }
        }
        string addressLine;
        public string AddressLine {
            get { return addressLine; }
            set {
                if(addressLine != value) {
                    addressLine = value;
                    RaisePropertyChanged("AddressLine");
                }
            }
        }
        string state;
        public string State {
            get { return state; }
            set {
                if(state != value) {
                    state = value;
                    RaisePropertyChanged("State");
                }
            }
        }
        string city;
        public string City {
            get { return city; }
            set {
                if(city != value) {
                    city = value;
                    RaisePropertyChanged("City");
                }
            }
        }
        string zip;
        public string Zip {
            get { return zip; }
            set {
                if(zip != value) {
                    zip = value;
                    RaisePropertyChanged("Zip");
                }
            }
        }
        public override string ToString() {
            return GetAddressFullString(Environment.NewLine);
        }
        public string GetAddressFullString(string separator) {
            return string.Format("{0}{1}{2}{3}{4}", AddressLine, separator, GetFormatString(City), GetFormatString(State, false), GetFormatString(Zip, false)); 
        }
        string GetFormatString(string name, bool addComma = true) {
            if(string.IsNullOrEmpty(name))
                return string.Empty;
            string format = addComma ? "{0}, " : "{0} ";
            return string.Format(format, name);
        }
        public void Assign(Address address) {
            this.AddressLine = address.AddressLine;
            this.State = address.State;
            this.City = address.City;
            this.Zip = address.Zip;
        }

        public object Clone() {
            return new Address() { AddressLine = this.AddressLine, City = this.City, State = this.State, Zip = this.Zip };
        }
    }
    

    public enum ContactTitle { None, Dr, Miss, Mr, Mrs, Ms, Prof }
}
