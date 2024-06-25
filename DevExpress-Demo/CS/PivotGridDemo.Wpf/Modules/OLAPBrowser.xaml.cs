using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    
    public partial class OLAPBrowser : PivotGridDemoModule {

        const string YearFieldName = "[Date].[Calendar].[Calendar Year]";
        const string CategoryFieldName = "[Product].[Product].[Product]";
        const string TotalCostFieldName = "[Measures].[Total Product Cost]";
        const string FreightFieldName = "[Measures].[Freight Cost]";
        const string QuantityOrderFieldName = "[Measures].[Order Quantity]";
        protected const int DefaultFieldWidth = 90;
        
        public static bool UseXmlaConnection {
			get {
				return !DevExpress.XtraPivotGrid.Data.OLAPMetaGetter.IsProviderAvailable;
			}
		}

        async static Task<string> GetSampleConnectionString() {
            return await Task.Run(() => { return "Provider=msolap;Data Source=" + GetSampleFileName() + ";Initial Catalog=" + GetSampleCatalogName() + ";Cube Name=Adventure Works"; });
        }

        static string GetSampleCatalogName() {
            if(UseXmlaConnection)
                return "Adventure Works DW Standard Edition";
            return "Adventure Works";
        }

        static string GetSampleFileName() {
            if(UseXmlaConnection)
                return "http://demos.devexpress.com/Services/OLAP/msmdpump.dll";
			string sampleFileName = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath));
			if(File.Exists(sampleFileName))
				try {
					File.SetAttributes(sampleFileName, FileAttributes.Normal);
				} catch { }
			return sampleFileName;
		}

        string PivotConnectionString() {
            return pivotGrid.OlapConnectionString;
        }
        public OLAPBrowser() {
            InitializeComponent();
        }
        async void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.ShowLoadingPanel = true;
            await InitPivotGrid(await GetSampleConnectionString());
            pivotGrid.ShowLoadingPanel = false;
        }
        bool IsSampleCube() {
            return pivotGrid.OlapConnectionString.Contains("Cube Name=Adventure Works");
        }
        
        void InitPivotLayoutSampleOlapDB() {
            if(pivotGrid.Fields.Count == 0 || !IsSampleCube()) 
                return;
            pivotGrid.BeginUpdate();
            PivotGridField fieldProduct = pivotGrid.Fields[CategoryFieldName],
                fieldYear = pivotGrid.Fields[YearFieldName],
                fieldTotalCost = pivotGrid.Fields[TotalCostFieldName],
                fieldFreightCost = pivotGrid.Fields[FreightFieldName],
                fieldOrderQuantity = pivotGrid.Fields[QuantityOrderFieldName];
            if(fieldProduct == null ||
                fieldYear == null ||
                fieldTotalCost == null ||
                fieldFreightCost == null ||
                fieldOrderQuantity == null) {
                pivotGrid.EndUpdateAsync();
                return;
            }
            fieldProduct.Area = FieldArea.RowArea;
            fieldYear.Area = FieldArea.ColumnArea;
            fieldYear.SortOrder = FieldSortOrder.Descending;
            fieldTotalCost.Width = DefaultFieldWidth + 20;
            fieldTotalCost.CellFormat = "c2";
            fieldFreightCost.Width = DefaultFieldWidth;
            fieldFreightCost.CellFormat = "c2";
            fieldOrderQuantity.Width = DefaultFieldWidth + 5;
            fieldProduct.Visible = true;
            fieldYear.Visible = true;
            fieldTotalCost.Visible = true;
            fieldFreightCost.Visible = true;
            fieldOrderQuantity.Visible = true;
            pivotGrid.EndUpdateAsync();
        }

        async Task<bool> InitPivotGrid(string connectionString) {
            if(string.IsNullOrWhiteSpace(connectionString)) {
                pivotGrid.DataSource = null;
                return true;
            }
            if(PivotConnectionString() == connectionString) 
                return true;
            pivotGrid.OlapConnectionString = null;
            pivotGrid.BeginUpdate();
            pivotGrid.Fields.Clear();
            pivotGrid.Groups.Clear();
            if(UseXmlaConnection)
                pivotGrid.OlapDataProvider = OlapDataProvider.Xmla;
            pivotGrid.EndUpdate();
            await pivotGrid.SetOlapConnectionStringAsync(connectionString);
            if(pivotGrid.Fields.Count == 0) {
                await pivotGrid.RetrieveFieldsAsync(FieldArea.FilterArea, false);
                InitPivotLayoutSampleOlapDB();
            }
            return true;
        }

        DataSourceDialog dialog;
        void OnShowConnectionClick(object sender, RoutedEventArgs e) {
            if(pivotGrid == null || pivotGrid.IsAsyncInProgress)
                return;
            dialog = new DataSourceDialog();
            FloatingContainerParameters pars = new FloatingContainerParameters();
            pars.AllowSizing = false;
            pars.CloseOnEscape = true;
            pars.Title = "OLAP Connection";
            pars.ClosedDelegate = DialogClosed;
            FloatingContainer.ShowDialogContent(dialog, this, new Size(600, 230), pars);
        }

       async void DialogClosed(bool? close) {
            Application.Current.MainWindow.Activate();
            if(dialog == null)
                return;
            String connectionString = dialog.GetConnectionString();
            dialog = null;
            if(close != true)
                return;
            if(string.IsNullOrWhiteSpace(connectionString)) {
                return;
            }
            await InitPivotGrid(connectionString);
        }
    }
}
