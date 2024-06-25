using DevExpress.Xpf.PivotGrid;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.DevAV.Views {
    public partial class QuoteCollectionView : UserControl {
        public QuoteCollectionView() {
            InitializeComponent();
        }
        void CustomSummary(object sender, PivotCustomSummaryEventArgs e) {
            switch(e.DataField.FieldName) {
                case "Percentage":
                    decimal quoteSummary = 0;
                    decimal opportunitySummary = 0;
                    foreach(PivotDrillDownDataRow row in e.CreateDrillDownDataSource()) {
                        quoteSummary += (decimal)row["Total"];
                        opportunitySummary += (decimal)row["MoneyOpportunity"];
                    }
                    if(quoteSummary != 0)
                        e.CustomValue = 100M * opportunitySummary / quoteSummary;
                    break;
            }
        }
    }
}
