Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.XtraScheduler.iCalendar

Namespace ProductsDemo.Modules

    Public Class SchedulerModuleViewModel
        Inherits ViewModelBase

        Private openScheduleCommandField As ICommand

        Private saveScheduleCommandField As ICommand

        Public ReadOnly Property Appointments As ObservableCollection(Of Appointment)
            Get
                Return Model.Appointments
            End Get
        End Property

        Protected Property Model As CalendarModel

        Public ReadOnly Property OpenScheduleCommand As ICommand
            Get
                If openScheduleCommandField Is Nothing Then openScheduleCommandField = New OpenScheduleCommand(Me)
                Return openScheduleCommandField
            End Get
        End Property

        Public ReadOnly Property SaveScheduleCommand As ICommand
            Get
                If saveScheduleCommandField Is Nothing Then saveScheduleCommandField = New SaveScheduleCommand(Me)
                Return saveScheduleCommandField
            End Get
        End Property

        Public Sub New()
            Model = New CalendarModel()
        End Sub
    End Class

    Public MustInherit Class CustomScheduleCommandBase
        Implements ICommand

        Private viewModelField As SchedulerModuleViewModel

        Public Sub New(ByVal viewModel As SchedulerModuleViewModel)
            viewModelField = viewModel
        End Sub

        Public ReadOnly Property ViewModel As SchedulerModuleViewModel
            Get
                Return viewModelField
            End Get
        End Property

#Region "Event CanExecuteChanged"
        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Overridable Sub OnCanExecuteChanged()
            Dim handler As EventHandler = CanExecuteChangedEvent
            If handler IsNot Nothing Then handler(Me, EventArgs.Empty)
        End Sub

#End Region
        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            Dim creator As ISchedulerExchangeCreator = TryCast(parameter, ISchedulerExchangeCreator)
            If creator Is Nothing Then Return
            ExecuteCore(creator)
        End Sub

        Protected MustOverride Sub ExecuteCore(ByVal creator As ISchedulerExchangeCreator)
    End Class

    Public Class OpenScheduleCommand
        Inherits CustomScheduleCommandBase

        Public Sub New(ByVal viewModel As SchedulerModuleViewModel)
            MyBase.New(viewModel)
        End Sub

        Protected Overrides Sub ExecuteCore(ByVal creator As ISchedulerExchangeCreator)
            ViewModel.Appointments.Clear()
            Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
            openFileDialog.Filter = "iCalendar Files (.ics)|*.ics"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim importer As iCalendarImporter = creator.CreateImporter()
                Using stream As Stream = openFileDialog.OpenFile()
                    importer.Import(stream)
                End Using
            End If
        End Sub
    End Class

    Public Class SaveScheduleCommand
        Inherits CustomScheduleCommandBase

        Public Sub New(ByVal viewModel As SchedulerModuleViewModel)
            MyBase.New(viewModel)
        End Sub

        Protected Overrides Sub ExecuteCore(ByVal creator As ISchedulerExchangeCreator)
            Dim openFileDialog As SaveFileDialog = New SaveFileDialog()
            openFileDialog.Filter = "iCalendar Files (.ics)|*.ics"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim exporter As iCalendarExporter = creator.CreateExporter()
                Using stream As Stream = openFileDialog.OpenFile()
                    exporter.Export(stream)
                End Using
            End If
        End Sub
    End Class

    Public Interface ISchedulerExchangeCreator

        Function CreateImporter() As iCalendarImporter

        Function CreateExporter() As iCalendarExporter

    End Interface
End Namespace
