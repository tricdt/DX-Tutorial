Imports System
Imports System.Windows.Media
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.TreeList

Namespace NavigationDemo

    <CodeFile("ViewModels/ReportLibraryViewModel.(cs)")>
    Public Partial Class ReportLibraryDemoModule
        Inherits AccordionDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class ReportLibraryNodeImageSelector
        Inherits TreeListNodeImageSelector

        Private Shared ReadOnly Folder As ImageSource = WpfSvgRenderer.CreateImageSource(New Uri("pack://application:,,,/" & AssemblyInfo.SRAssemblyImages & ";component/SvgImages/Business Objects/BO_Folder.svg"))

        Private Shared ReadOnly Report As ImageSource = WpfSvgRenderer.CreateImageSource(New Uri("pack://application:,,,/" & AssemblyInfo.SRAssemblyImages & ";component/SvgImages/Business Objects/BO_Report.svg"))

        Public Overrides Function [Select](ByVal rowData As TreeListRowData) As ImageSource
            Return If(CType(rowData.Row, ReportLibraryNode).IsFolder, Folder, Report)
        End Function
    End Class
End Namespace
