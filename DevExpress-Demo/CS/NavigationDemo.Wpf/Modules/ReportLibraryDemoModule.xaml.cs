using System;
using System.Windows.Media;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;

namespace NavigationDemo {
    [CodeFile("ViewModels/ReportLibraryViewModel.(cs)")]
    public partial class ReportLibraryDemoModule : AccordionDemoModule {
        public ReportLibraryDemoModule() {
            InitializeComponent();
        }
    }
    public class ReportLibraryNodeImageSelector : TreeListNodeImageSelector {
        static readonly ImageSource Folder = WpfSvgRenderer.CreateImageSource(new Uri("pack://application:,,,/" + AssemblyInfo.SRAssemblyImages + ";component/SvgImages/Business Objects/BO_Folder.svg"));
        static readonly ImageSource Report = WpfSvgRenderer.CreateImageSource(new Uri("pack://application:,,,/" + AssemblyInfo.SRAssemblyImages + ";component/SvgImages/Business Objects/BO_Report.svg"));
        public override ImageSource Select(TreeListRowData rowData) {
            return ((ReportLibraryNode)rowData.Row).IsFolder ? Folder : Report;
        }
    }
}
