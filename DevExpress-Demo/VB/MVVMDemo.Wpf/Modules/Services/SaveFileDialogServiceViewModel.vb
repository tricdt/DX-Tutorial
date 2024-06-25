Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.Services

    Public Class SaveFileDialogServiceViewModel

        Public Overridable Property FileName As String

        Public Sub ShowDialog(ByVal serviceName As String)
            Dim service As ISaveFileDialogService = GetService(Of ISaveFileDialogService)(serviceName)
            service.Filter = "All files (*.*)|*.*|Image files (*.png;*.jpeg)|*.png;*.jpeg"
            service.DefaultFileName = "ISaveFileDialogService"
            service.DefaultExt = "png"
            If service.ShowDialog() Then
                FileName = service.File.GetFullName()
            End If
        End Sub
    End Class
End Namespace
