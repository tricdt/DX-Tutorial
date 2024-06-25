Imports System
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native

Namespace ControlsDemo

    Public Class TileNavPaneViewModel
        Inherits TileNavBaseViewModel

        Private _ShowNotificationCommand As ICommand

        Public Sub New()
            Call Messenger.Default.Register(Of NotificationMessage)(Me, New Action(Of NotificationMessage)(AddressOf OnNotificationMessage))
            ShowNotificationCommand = DelegateCommandFactory.Create(New Action(Of String)(AddressOf OnShowNotificationCommand))
        End Sub

        Public Property ShowNotificationCommand As ICommand
            Get
                Return _ShowNotificationCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ShowNotificationCommand = value
            End Set
        End Property

        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return ServiceContainer.GetService(Of IMessageBoxService)()
            End Get
        End Property

        Protected Overridable Sub OnNotificationMessage(ByVal message As NotificationMessage)
            OnShowNotificationCommand(message.Source)
        End Sub

        Protected Overrides Sub OnViewUnloaded()
            MyBase.OnViewUnloaded()
            Call Messenger.Default.Unregister(Of NotificationMessage)(Me, New Action(Of NotificationMessage)(AddressOf OnNotificationMessage))
        End Sub

        Private Sub OnShowNotificationCommand(ByVal message As String)
            MessageBoxService.ShowMessage(message)
        End Sub
    End Class
End Namespace
