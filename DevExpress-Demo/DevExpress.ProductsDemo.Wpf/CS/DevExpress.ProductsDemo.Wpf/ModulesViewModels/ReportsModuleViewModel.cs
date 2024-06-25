using System;
using System.IO;
using System.Configuration;
using DevExpress.DemoData.Helpers;
using DevExpress.XtraReports;

namespace ProductsDemo.Modules {
    public class ReportsModuleViewModel {
        static ReportsModuleViewModel() {
            PatchDemoReportsConnectionStrings();
        }
        public virtual IReport DocumentSource { get; set; }

        public void OnLoaded() {
            UpdateReport();
        }
        public void OnUnloaded() {
            DocumentSource.StopPageBuilding();
        }
        void UpdateReport() {
            DocumentSource = new DevExpress.DevAV.MasterDetailReport.Report();
            DocumentSource.CreateDocument(true);
        }
        static void PatchDemoReportsConnectionStrings() {
            string path = Utils.GetRelativePath("nwind.mdb");
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(path));
        }
    }
}
