Imports System.ComponentModel

Namespace ChartsDemo

    Public Class ConstantLinesAndStripsViewModel
        Implements INotifyPropertyChanged

        Private dataField As OperatingSurfaceTemperatureData = New OperatingSurfaceTemperatureData()

        Private optimalTemperatureField As Double

        Public ReadOnly Property Data As OperatingSurfaceTemperatureData
            Get
                Return dataField
            End Get
        End Property

        Public Property OptimalTemperature As Double
            Get
                Return optimalTemperatureField
            End Get

            Set(ByVal value As Double)
                If value < dataField.MinOptimalTemperature Then
                    optimalTemperatureField = dataField.MinOptimalTemperature
                ElseIf value > dataField.MaxOptimalTemperature Then
                    optimalTemperatureField = dataField.MaxOptimalTemperature
                Else
                    optimalTemperatureField = value
                End If

                OnPropertyChanged("OptimalTemperature")
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New()
            optimalTemperatureField = dataField.OptimalTemperature
        End Sub

        Private Sub OnPropertyChanged(ByVal propertyName As String)
            If PropertyChangedEvent IsNot Nothing Then
                Dim e As PropertyChangedEventArgs = New PropertyChangedEventArgs(propertyName)
                RaiseEvent PropertyChanged(Me, e)
            End If
        End Sub
    End Class
End Namespace
