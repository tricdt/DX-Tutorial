Imports DevExpress.Mvvm
Imports System.Threading.Tasks

Namespace MVVMDemo.AsyncCommands

    Public Class AsyncDelegateCommandsViewModel
        Inherits ViewModelBase

        Private _CalculateCommand As AsyncCommand

        Public Property CalculateCommand As AsyncCommand
            Get
                Return _CalculateCommand
            End Get

            Private Set(ByVal value As AsyncCommand)
                _CalculateCommand = value
            End Set
        End Property

        Private Async Function Calculate() As Task
            For i As Integer = 0 To 100
                If CalculateCommand.IsCancellationRequested Then
                    Progress = 0
                    Return
                End If

                Progress = i
                Await Task.Delay(20)
            Next
        End Function

        Private _Progress As Integer

        Public Property Progress As Integer
            Get
                Return _Progress
            End Get

            Set(ByVal value As Integer)
                SetValue(_Progress, value)
            End Set
        End Property

        Public Sub New()
            CalculateCommand = New AsyncCommand(AddressOf Calculate)
        End Sub
    End Class
End Namespace
