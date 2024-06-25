using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.DevAV.ViewModels;
using DevExpress.Spreadsheet;

namespace DevExpress.DevAV.Views.Customer {
    public partial class CustomerAnalysis : UserControl {
        public CustomerAnalysis() {
            InitializeComponent();
            SubscribeEvents();
        }

        void SubscribeEvents() {
            Loaded += OnLoaded;
            spreadsheetControl.DocumentLoaded += OnDocumentLoaded;
        }
        
        void OnLoaded(object sender, RoutedEventArgs e) {
            ((Mvvm.ISplashScreenService)splashScreenService).ShowSplashScreen(string.Empty);
            LoadTemplate();
        }
        void LoadTemplate() {
            using(var stream = AnalysisTemplatesHelper.GetAnalysisTemplate(AnalysisTemplate.CustomerSales))
                spreadsheetControl.LoadDocument(stream);
        }

        void OnDocumentLoaded(object sender, EventArgs e) {            
            LoadData();
            ((Mvvm.ISplashScreenService)splashScreenService).HideSplashScreen();
        }
        void LoadData() {
            CustomerAnalysisViewModel ViewModel = (CustomerAnalysisViewModel)this.DataContext;
            spreadsheetControl.Document.BeginUpdate();
            var salesReportWorksheet = spreadsheetControl.Document.Worksheets["Sales Report"];
            var salesReportItems = ViewModel.GetSalesReport().ToList(); 
            var frCustomers = salesReportItems
                .Select(i => i.CustomerName)
                .Distinct()
                .OrderBy(i => i).ToList();
            salesReportWorksheet.Import(frCustomers, 14, 1, true);
            foreach(CustomersAnalysis.Item item in salesReportItems) {
                int rowOffset = frCustomers.IndexOf(item.CustomerName);
                int columnOffset = (AnalysisPeriod.MonthOffsetFromStart(item.Date) / 12);
                if (rowOffset < 0 || columnOffset < 0) continue;
                salesReportWorksheet.Cells[14 + rowOffset, 3 + columnOffset * 2].SetValue(item.Total);
            }
            var salesDataWorksheet = spreadsheetControl.Document.Worksheets["Sales Data"];
            var salesDataItems = ViewModel.GetSalesData().ToList(); 
            var states = salesDataItems.Select(i => i.State).Distinct().OrderBy(i => i).ToList();

            salesDataWorksheet.Import(ViewModel.GetStates(states), 5, 3, false);
            foreach(CustomersAnalysis.Item item in salesDataItems) {
                int rowOffset = AnalysisPeriod.MonthOffsetFromStart(item.Date);
                int columnOffset = states.IndexOf(item.State);
                if (rowOffset < 0 || columnOffset < 0) continue;
                salesDataWorksheet.Cells[6 + rowOffset, 3 + columnOffset].SetValue(item.Total);
            }
            spreadsheetControl.Document.Worksheets.ActiveWorksheet = salesReportWorksheet;
            spreadsheetControl.Document.EndUpdate();
        }
    }
}
