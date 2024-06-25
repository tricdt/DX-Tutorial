Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core
Imports System.Collections.ObjectModel

Namespace EditorsDemo

    <POCOViewModel>
    Public Class NotifyIconViewModel

        Public Property EventsLog As ObservableCollection(Of String)

        Protected Overridable ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable ReadOnly Property NotifyIconService As INotifyIconService
            Get
                Return Nothing
            End Get
        End Property

        Private Sub AddStringInLog(ByVal logString As String)
            EventsLog.Add(Date.Now.ToShortTimeString() & logString)
            If EventsLog.Count > 20 Then EventsLog.RemoveAt(0)
        End Sub

        Public Sub New()
            EventsLog = New ObservableCollection(Of String)()
            AddStringInLog(" - NotifyIcon is created")
        End Sub

        Public Sub ShowMessageBox()
            MessageBoxService.ShowMessage("hello")
        End Sub

        Public Sub SetStatusIcon(ByVal icon As Object)
            AddStringInLog(" - NotifyIcon state changed to " & icon.ToString())
            NotifyIconService.SetStatusIcon(icon)
        End Sub

        Public Sub ResetStatusIcon()
            AddStringInLog(" - NotifyIcon state reseted on default state")
            NotifyIconService.ResetStatusIcon()
        End Sub

        Public Sub DoIconLeftAction()
            AddStringInLog(" - NotifyIcon left mouse button click")
            MessageBoxService.ShowMessage("NotifyIcon left mouse button click!")
        End Sub

        Public Sub PopupButtonClick(ByVal buttonName As Object)
            AddStringInLog(" - NotifyIcon context menu button '" & buttonName.ToString() & "' click")
        End Sub
    End Class
End Namespace
