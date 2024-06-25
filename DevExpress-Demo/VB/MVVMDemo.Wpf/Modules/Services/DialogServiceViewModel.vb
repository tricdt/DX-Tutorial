Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Threading.Tasks
Imports System.Windows

Namespace MVVMDemo.Services

    Public Class DialogServiceViewModel

        Public Sub ShowDetail(ByVal serviceName As String)
            Dim detailViewModel As DialogServiceDetailViewModel = DialogServiceDetailViewModel.Create()
            Dim registerCommand As UICommand = New UICommand() With {.Caption = "Register", .IsDefault = True, .Command = New DelegateCommand(Sub()
            End Sub, Function() Not String.IsNullOrEmpty(detailViewModel.CustomerName))}
            Dim cancelCommand As UICommand = New UICommand() With {.Caption = "Cancel", .IsCancel = True}
            Dim service As IDialogService = GetService(Of IDialogService)(serviceName)
            Dim result As UICommand = service.ShowDialog(dialogCommands:={registerCommand, cancelCommand}, title:="Registration Dialog", viewModel:=detailViewModel)
            If result Is registerCommand Then MessageBox.Show("Registered: " & detailViewModel.CustomerName)
        End Sub

        Public Sub ShowDetailAsync(ByVal serviceName As String)
            Dim detailViewModel As DialogServiceDetailViewModel = DialogServiceDetailViewModel.Create()
            Dim registerCommand As UICommand = New UICommand() With {.Caption = "Register", .IsDefault = True, .Command = New AsyncCommand(AddressOf Delay, Function() Not String.IsNullOrEmpty(detailViewModel.CustomerName)), .AsyncDisplayMode = AsyncDisplayMode.Wait}
            Dim cancelCommand As UICommand = New UICommand() With {.Caption = "Cancel", .IsCancel = True}
            Dim service As IDialogService = GetService(Of IDialogService)(serviceName)
            Dim result As UICommand = service.ShowDialog(dialogCommands:={registerCommand, cancelCommand}, title:="Registration Dialog", viewModel:=detailViewModel)
            If result Is registerCommand Then MessageBox.Show("Registered: " & detailViewModel.CustomerName)
        End Sub

        Private Async Function Delay() As Task
            Await Task.Delay(3000)
        End Function
    End Class
End Namespace
