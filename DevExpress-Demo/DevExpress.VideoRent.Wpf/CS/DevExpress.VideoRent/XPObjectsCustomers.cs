using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

#if SL
using PrivateAttribute = System.ObsoleteAttribute;
using DevExpress.Xpf.Drawing;
#endif

namespace DevExpress.VideoRent {
    public class Customer : Person, IComparable {
        Image photo;
        string middleName;
        string email;
        string address;
        string phone;
        string comments;
        CustomerDiscountLevel discountLevel;

        public Customer(Session session) : base(session) { }
        public Customer(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public Customer(Session session, int selfId, string firstName, string middleName, string lastName)
            : this(session, selfId) {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        public Customer(Session session, string firstName, string middleName, string lastName)
            : this(session) {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        public Customer(Session session, string firstName, string lastName) : this(session, firstName, string.Empty, lastName) { }
        public override void AfterConstruction() {
            base.AfterConstruction();
            discountLevel = CustomerDiscountLevel.FirstTime;
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("CustomerId"), Browsable(false), Private("", true)]
        public int _CustomerId {
            get { return GetPropertyValue<int>("_CustomerId"); }
            set { SetPropertyValue<int>("_CustomerId", value); }
        }
        [PersistentAlias("_CustomerId")]
        public int CustomerId {
            get { return GetPropertyValue<int>("_CustomerId"); }
            protected set { SetPropertyValue<int>("_CustomerId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int CustomerId {
            get { return GetPropertyValue<int>("CustomerId"); }
            protected set { SetPropertyValue<int>("CustomerId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "Customer"; } }
        public string MiddleName {
            get { return middleName; }
            set { SetPropertyValue<string>("MiddleName", ref middleName, value); }
        }
        public string Email {
            get { return email; }
            set { SetPropertyValue<string>("Email", ref email, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Address {
            get { return address; }
            set { SetPropertyValue<string>("Address", ref address, value); }
        }
        public string Phone {
            get { return phone; }
            set { SetPropertyValue<string>("Phone", ref phone, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Comments {
            get { return comments; }
            set { SetPropertyValue<string>("Comments", ref comments, value); }
        }
        [ValueConverter(typeof(ImageValueConverter))]
        public Image Photo {
            get { return photo; }
            set { SetPropertyValue<Image>("Photo", ref photo, value); }
        }
        public Image PhotoExist {
            get {
                if(photo == null) return ReferenceImages.UnknownPerson;
                return photo;
            }
        }
        [Association("Customer-Receipts")]
        public XPCollection<Receipt> Receipts { get { return GetCollection<Receipt>("Receipts"); } }
        public XPCollection<Rent> ActiveRents { get { return new XPCollection<Rent>(Session, CriteriaOperator.Parse("Customer = ? and Active = ?", this, true)); } }
        public CustomerDiscountLevel DiscountLevel {
            get { return discountLevel; }
            set { SetPropertyValue<CustomerDiscountLevel>("DiscountLevel", ref discountLevel, value); }
        }
        public string DiscountLevelCaption { get { return EnumTitlesKeeper<CustomerDiscountLevel>.GetTitle(DiscountLevel); } }
        public override bool AllowDelete { get { return base.AllowDelete && Session.FindObject<Receipt>(CriteriaOperator.Parse("Customer = ?", this)) == null; } }
        public bool OverdueTodayItemsExist() {
            foreach(Rent rent in ActiveRents)
                if(rent.ActiveType != (int)ActiveRentType.Active) return true;
            return false;
        }
        public decimal Discount { get { return (decimal)ReferenceData.CustomerDiscount.GetValue((int)DiscountLevel); } }
        public override string FullName {
            get {
                return GetFullName(FirstName, MiddleName, LastName);
            }
        }
        public string FullNameLastSorting {
            get {
                return GetFullName(LastName, FirstName, MiddleName);
            }
        }
        public static string GetFullName(string first, string second, string third) {
            string ret = first == null ? string.Empty : first.Trim();
            string secondTrim = second == null ? string.Empty : second.Trim();
            string thirdTrim = third == null ? string.Empty : third.Trim();
            if(secondTrim.Length != 0) ret += (ret.Length == 0 ? string.Empty : " ") + secondTrim;
            if(thirdTrim.Length != 0) ret += (ret.Length == 0 ? string.Empty : " ") + thirdTrim;
            return ret;
        }
        public virtual string FullNameAndCardNumber {
            get {
                return string.Format("{0}({1:d6})", FullName, CustomerId);
            }
        }
        public bool IsDebter {
            get { return Session.FindObject<Rent>(CriteriaOperator.Parse("Customer = ? and Active = ? and ActiveType = ?", this, true, ActiveRentType.Overdue)) != null; }
        }
        public Receipt DoRent(ICollection<RentInfo> rentsInfo) {
            Receipt receipt = null;
            foreach(RentInfo rentInfo in rentsInfo) {
                MovieItem item = rentInfo.Item.RentItem;
                if(item == null) continue;
                if(receipt == null) receipt = new Receipt(this, ReceiptType.Rentals);
                new Rent(receipt, item, rentInfo.Days);
            }
            if(receipt != null) receipt.CalcPayment();
            return receipt;
        }
        public Receipt Buy(ICollection<RentInfo> rentsInfo) {
            Receipt receipt = null;
            foreach(RentInfo rentInfo in rentsInfo) {
                MovieItem item = rentInfo.Item.SellItem;
                if(item == null) continue;
                if(receipt == null) receipt = new Receipt(this, ReceiptType.Purchases);
                new Rent(receipt, item, rentInfo.Days);
            }
            if(receipt != null) receipt.CalcPayment();
            return receipt;
        }
        public Receipt DoRent(RentInfo rentInfo) {
            return DoRent(new RentInfo[] { rentInfo });
        }
        public Receipt Buy(RentInfo rentInfo) {
            return Buy(new RentInfo[] { rentInfo });
        }
        public decimal GetPaymentSum() { 
            decimal ret = 0;
            foreach(Receipt reciept in Receipts)
                ret += reciept.Payment;
            return ret;
        }
        public void CalcDiscount() {
            decimal paymentSum = GetPaymentSum();
            DiscountLevel = GetDiscount(paymentSum);
        }
        public Receipt ReturnRents(ICollection<Rent> rents) {
            Receipt receipt = null;
            foreach(Rent rent in rents) {
                if(rent.Customer != this) continue;
                rent.Return();
                if(!rent.IsOverdue) continue;
                if(receipt == null) receipt = new Receipt(this, ReceiptType.LateFees);
                receipt.OverdueRents.Add(rent);
            }
            if(receipt != null) receipt.CalcPayment();
            return receipt;
        }
        public Receipt ReturnRent(Rent rent) {
            return ReturnRents(new Rent[] { rent });
        }
        public string GetCustomerInfoHtml() {
            string ret = string.Format("<b>{0}</b>", FullName);
            ret += string.Format("\r\n{1}: <b>{0:d6}</b>", CustomerId, Properties.Resources.CardNumber);
            if(BirthDate != null && BirthDate != DateTime.MinValue) ret += string.Format("\r\n{1}: <b>{0:d}</b>", BirthDate, Properties.Resources.BirthDate);
            if(!string.IsNullOrEmpty(Email)) ret += string.Format("\r\n{1}: <b>{0}</b>", Email, Properties.Resources.Email);
            if(!string.IsNullOrEmpty(Phone)) ret += string.Format("\r\n{1}: <b>{0}</b>", Phone, Properties.Resources.Phone);
            if(!string.IsNullOrEmpty(Address)) ret += string.Format("\r\n{1}: <b>{0}</b>", Address, Properties.Resources.Address);
            ret += string.Format("\r\n{2}: <b>{0}({1:p})</b>", EnumTitlesKeeper<CustomerDiscountLevel>.GetTitle(DiscountLevel), Discount, Properties.Resources.DiscountLevel);
            if(!string.IsNullOrEmpty(Comments)) ret += string.Format("\r\n{1}: <i>{0}</i>", Comments, Properties.Resources.Comments);
            return ret;
        }
        public override string ToString() {
            return FullName;
        }
        public static CustomerDiscountLevel GetDiscount(decimal paymentSum) {
            if(paymentSum > ReferenceData.GetDiscountLevelPayment(CustomerDiscountLevel.Prodigious))
                return CustomerDiscountLevel.Prodigious;
            else if(paymentSum > ReferenceData.GetDiscountLevelPayment(CustomerDiscountLevel.Active))
                return CustomerDiscountLevel.Active;
            else if(paymentSum > ReferenceData.GetDiscountLevelPayment(CustomerDiscountLevel.Occasional))
                return CustomerDiscountLevel.Occasional;
            else if(paymentSum > ReferenceData.GetDiscountLevelPayment(CustomerDiscountLevel.Basic))
                return CustomerDiscountLevel.Basic;
            return CustomerDiscountLevel.FirstTime;
        }
        #region IComparable Members
        int IComparable.CompareTo(object obj) {
            return string.Compare(FullName, string.Format("{0}", obj));
        }
        #endregion
#if DebugTest
        public void SetDiscountLevel(CustomerDiscountLevel level) {
            DiscountLevel = level;
        }
#endif
    }
}
