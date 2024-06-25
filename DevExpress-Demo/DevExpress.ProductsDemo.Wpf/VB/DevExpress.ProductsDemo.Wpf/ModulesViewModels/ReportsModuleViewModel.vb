Imports System
Imports DevExpress.DemoData.Helpers
Imports DevExpress.XtraReports

Namespace ProductsDemo.Modules

    Public Class ReportsModuleViewModel

        Shared Sub New()
            Call PatchDemoReportsConnectionStrings()
        End Sub

        Public Overridable Property DocumentSource As IReport

        Public Sub OnLoaded()
            UpdateReport()
        End Sub

        Public Sub OnUnloaded()
            DocumentSource.StopPageBuilding()
        End Sub

        Private Sub UpdateReport()
            DocumentSource = New DevExpress.DevAV.MasterDetailReport.Report()
            DocumentSource.CreateDocument(True)
        End Sub

        Private Shared Sub PatchDemoReportsConnectionStrings()
            Dim path As String = Utils.GetRelativePath("nwind.mdb")
            AppDomain.CurrentDomain.SetData("DataDirectory", IO.Path.GetDirectoryName(path))
        End Sub
    End Class
End Namespace
