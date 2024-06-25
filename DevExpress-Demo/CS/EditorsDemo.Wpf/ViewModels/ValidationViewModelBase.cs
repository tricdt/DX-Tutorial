using DevExpress.Mvvm;
using System;
using System.Collections.Generic;

namespace EditorsDemo {
    public class ValidationViewModelBase : ViewModelBase {
        public class CardInfo {
            public string Name { get; set; }
            public string DisplayName { get; set; }
        }
        public static readonly List<CardInfo> Cards = new List<CardInfo>();
        static ValidationViewModelBase() {
            Cards.Add(new CardInfo() { Name = "VISA", DisplayName = "VISA (13 digits)" });
            Cards.Add(new CardInfo() { Name = "Master Card", DisplayName = "Master Card (16 digits)" });
            Cards.Add(new CardInfo() { Name = "American Express", DisplayName = "American Express (15 digits)" });
            Cards.Add(new CardInfo() { Name = "Diners Club", DisplayName = "Diners Club (13 digits)" });
        }

        public string Login {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Mail {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string ConfirmMail {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string FirstName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string LastName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public decimal Age {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public string CardType {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CardNumber {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public DateTime CardExpDate {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public ValidationViewModelBase() {
            Login = "TestUser";
            Mail = "testmail@devexpress.com";
            FirstName = "John";
            LastName = "Joe";
            CardType = "VISA";
            CardExpDate = DateTime.Today.AddDays(-1);
        }
    }
}
