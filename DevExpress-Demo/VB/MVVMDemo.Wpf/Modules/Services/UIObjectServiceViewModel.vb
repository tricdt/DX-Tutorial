Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Diagnostics
Imports System.IO
Imports System.Windows

Namespace MVVMDemo.Services

    Public Class UIObjectServiceViewModel

        Public Sub Export()
            Const format As String = "Pdf"
            Dim path = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), format.ToLower())
            Dim service As IUIObjectService = GetRequiredService(Of IUIObjectService)("GridService")
            Using file As FileStream = System.IO.File.Create(path)
                service.Object.View.ExportToPdf(file)
            End Using

            If MessageBox.Show("Do you want to open the following file?" & Microsoft.VisualBasic.Constants.vbCrLf & path, "Open File", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                Call Process.Start(New ProcessStartInfo With {.FileName = path, .UseShellExecute = True})
            Else
                File.Delete(path)
            End If
        End Sub

        Public Sub ShowColumnChooser()
            Dim service As IUIObjectService = GetRequiredService(Of IUIObjectService)("TableViewService")
            service.Object.ShowColumnChooser()
        End Sub
    End Class
End Namespace
