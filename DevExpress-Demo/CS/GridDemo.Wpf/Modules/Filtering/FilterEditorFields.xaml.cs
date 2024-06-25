using System.Windows.Controls;
using DevExpress.Xpf.Core.FilteringUI;

namespace GridDemo.Filtering {
    public partial class FilterEditorFields : UserControl {
        public FilterEditorFields() {
            InitializeComponent();
        }

        #region !
        void OnQueryFields(object sender, QueryFieldsEventArgs e) {
            var shipCountry = e.Fields["ShipCountry"];
            var shipCity = e.Fields["ShipCity"];
            var shipAddress = e.Fields["ShipAddress"];

            shipCountry.Caption = "Country";
            shipCity.Caption = "City";
            shipAddress.Caption = "Address";

            e.Fields.Remove(shipCountry);
            e.Fields.Remove(shipCity);
            e.Fields.Remove(shipAddress);

            var shipGroup = new FieldItem { Caption = "Ship" };
            shipGroup.Children.Add(shipCountry);
            shipGroup.Children.Add(shipCity);
            shipGroup.Children.Add(shipAddress);
            e.Fields.Add(shipGroup);
        }
        #endregion
    }
}
