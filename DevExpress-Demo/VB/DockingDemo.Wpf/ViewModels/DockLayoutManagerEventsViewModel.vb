Imports DevExpress.Mvvm.Native
Imports System
Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace DockingDemo.ViewModels

    Public Class DockLayoutManagerEventsViewModel
        Inherits ViewModelBase

        Private ReadOnly Property EventsService As IDockLayoutManagerEventsService
            Get
                Return ServiceContainer.GetService(Of IDockLayoutManagerEventsService)()
            End Get
        End Property

        Public Sub New()
        End Sub

        Private clearLogCommandField As ICommand

        Public ReadOnly Property ClearLogCommand As ICommand
            Get
                If clearLogCommandField Is Nothing Then clearLogCommandField = DelegateCommandFactory.Create(New Action(AddressOf ClearLog))
                Return clearLogCommandField
            End Get
        End Property

        Private Sub ClearLog()
            EventsService.ClearEventsLog()
        End Sub
    End Class
End Namespace
