using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using DevExpress.Xpf.PropertyGrid;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;

namespace PropertyGridDemo {
    public class LayoutCustomizationViewModel {
        public LayoutCustomizationViewModel() {
            Name = "Andrew Fuller";
            Photo = "Images/Persons/m03.jpg";
            Rank = 5;
            InitializeSkills();
            InitializeAddress();
            Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.";
        }
        public bool CanIncreaseRank() {
            return Rank < 10;
        }
        public bool CanDecreaseRank() {
            return Rank > 0;
        }
        public void IncreaseRank() {
            Rank++;
        }
        public void DecreaseRank() {
            Rank--;
        }
        void InitializeSkills() {
            Careers = new ObservableCollection<CareerInfo>();

            var sManager = new CareerInfo();
            sManager.JobTitle = "Sales Manager";
            sManager.FromDate = new DateTime(1998, 05, 14);
            sManager.ToDate = new DateTime(2001, 03, 22);
            sManager.Skills.Add("Speaking");
            sManager.Skills.Add("Coordination");

            var sRepr = new CareerInfo();
            sRepr.JobTitle = "Sales Representative";
            sRepr.FromDate = new DateTime(2001, 04, 10);
            sRepr.ToDate = new DateTime(2007, 09, 11);
            sRepr.Skills.Add("Decision Making");

            Careers.Add(sManager);
            Careers.Add(sRepr);
        }
        void InitializeAddress() {
            Address = AddressInfo.Create();
            Address.Country = "USA";
            Address.City = "Tacoma";
            Address.Address = "908 W. Capital Way";
        }

        public virtual string Photo { get; set; }
        public virtual string Name { get; set; }
        public virtual uint Rank { get; protected set; }
        public virtual ObservableCollection<CareerInfo> Careers { get; set; }
        public virtual bool Retired { get; set; }
        public virtual AddressInfo Address { get; set; }
        public virtual string Notes { get; set; }
    }
    [POCOViewModel]
    public class AddressInfo {
        public static AddressInfo Create() {
            return ViewModelSource.Create(() => new AddressInfo());
        }
        protected AddressInfo() { }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
    }
    public class CareerInfo {
        public CareerInfo() {
            Skills = new ObservableCollection<string>();
        }
        public string JobTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public ObservableCollection<string> Skills { get; private set; }
    }   
}
