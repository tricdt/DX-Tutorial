Imports DevExpress.Mvvm
Imports System.ComponentModel

' This class demonstrates the code that will be generated by the POCO mechanism at runtime
Namespace MVVMDemo.AsyncCommands

    Public Class AsyncPOCOCommandsViewModel_RuntimeGeneratedCode
        Inherits AsyncPOCOCommandsViewModel
        Implements INotifyPropertyChanged

        Private _CalculateCommand As AsyncCommand

        Public ReadOnly Property CalculateCommand As AsyncCommand
            Get
                Return If(_CalculateCommand, Function()
                    _CalculateCommand = New AsyncCommand(AddressOf Calculate)
                    Return _CalculateCommand
                End Function())
            End Get
        End Property

#Region "properties"
        Public Overrides Property Progress As Integer
            Get
                Return MyBase.Progress
            End Get

            Set(ByVal value As Integer)
                If MyBase.Progress = value Then Return
                MyBase.Progress = value
                RaisePropertyChanged("Progress")
            End Set
        End Property

#End Region
#Region "INotifyPropertyChanged implementation"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim handler = PropertyChangedEvent
            If handler IsNot Nothing Then handler(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
#End Region
    End Class
End Namespace
