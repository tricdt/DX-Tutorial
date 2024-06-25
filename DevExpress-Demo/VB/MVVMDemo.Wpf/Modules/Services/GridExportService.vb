Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Grid
Imports System.IO
Imports System

Namespace MVVMDemo.Services

    Public Class GridExportService
        Inherits ServiceBase
        Implements IExportService

        Private Sub Export(ByVal stream As Stream, ByVal format As ExportFormat) Implements IExportService.Export
            Dim grid As GridControl = CType(AssociatedObject, GridControl)
            Select Case format
                Case ExportFormat.Xlsx
                    grid.View.ExportToXlsx(stream)
                Case ExportFormat.Pdf
                    grid.View.ExportToPdf(stream)
                Case Else
                    Throw New InvalidOperationException()
            End Select
        End Sub
    End Class
End Namespace
