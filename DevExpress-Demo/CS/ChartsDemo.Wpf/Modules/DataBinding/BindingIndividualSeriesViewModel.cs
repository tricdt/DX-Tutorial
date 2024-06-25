using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;

namespace ChartsDemo {
    public class BindingIndividualSeriesViewModel {
        public static BindingIndividualSeriesViewModel Create() {
            return ViewModelSource.Create(() => new BindingIndividualSeriesViewModel());
        }

        readonly DevAVSales devAVSales = new DevAVSales();

        public virtual ObservableCollection<string> ArgumentDataMembers { get; protected set; }
        public virtual ObservableCollection<string> ValueDataMembers { get; protected set; }
        public virtual ObservableCollection<string> Categories { get; protected set; }
        public virtual string SelectedArgumentDataMember { get; set; }
        public virtual string SelectedValueDataMember { get; set; }
        public virtual string SelectedCategory { get; set; }
        public virtual string FilterString { get; protected set; }
        public List<DevAVSaleItem> DevAVNorthData { get { return this.devAVSales.GetProductsByCompany(0); } }
        public List<DevAVSaleItem> DevAVSouthData { get { return this.devAVSales.GetProductsByCompany(1); } }

        protected BindingIndividualSeriesViewModel() {
            ArgumentDataMembers = new ObservableCollection<string> {
                "Product",
                "Category"
            };
            ValueDataMembers = new ObservableCollection<string> {
                "Income",
                "Revenue"
            };
            Categories = new ObservableCollection<string> {
                "All Categories",
                "Photo",
                "Cell Phones",
                "Computers",
                "TV, Audio"
            };
            SelectedArgumentDataMember = ArgumentDataMembers[0];
            SelectedValueDataMember = ValueDataMembers[0];
            SelectedCategory = Categories[0];
        }

        protected void OnSelectedCategoryChanged() {
            if (SelectedCategory == "All Categories")
                FilterString = string.Empty;
            else
                FilterString = string.Format("[Category] = '{0}'", SelectedCategory);
        }

    }
}
