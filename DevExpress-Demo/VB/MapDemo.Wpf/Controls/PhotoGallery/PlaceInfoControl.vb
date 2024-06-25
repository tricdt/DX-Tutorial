Imports System.Windows
Imports System.Windows.Input

Namespace MapDemo

    Public Class PlaceInfoControl
        Inherits VisibleControl

        Private changePlaceCommandField As ChangePlaceCommand

        Public Shared ReadOnly PlaceInfoProperty As DependencyProperty = DependencyProperty.Register("PlaceInfo", GetType(PlaceInfo), GetType(PlaceInfoControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property PlaceInfo As PlaceInfo
            Get
                Return CType(GetValue(PlaceInfoProperty), PlaceInfo)
            End Get

            Set(ByVal value As PlaceInfo)
                SetValue(PlaceInfoProperty, value)
            End Set
        End Property

        Public ReadOnly Property ChangePlaceCommand As ChangePlaceCommand
            Get
                Return changePlaceCommandField
            End Get
        End Property

        Public Event ShowNextPlace As RoutedEventHandler

        Public Event ShowPrevPlace As RoutedEventHandler

        Public Sub New()
            changePlaceCommandField = New ChangePlaceCommand(Me)
            DefaultStyleKey = GetType(PlaceInfoControl)
        End Sub

        Public Sub OnShowNextPlace()
            RaiseEvent ShowNextPlace(Me, New RoutedEventArgs())
        End Sub

        Public Sub OnShowPreviousPlace()
            RaiseEvent ShowPrevPlace(Me, New RoutedEventArgs())
        End Sub
    End Class

    Public Class ChangePlaceCommand
        Implements ICommand

        Private ReadOnly placeInfoControl As PlaceInfoControl

        Public Sub New(ByVal placeInfoControl As PlaceInfoControl)
            Me.placeInfoControl = placeInfoControl
        End Sub

        Private Event _canExecuteChanged As System.EventHandler

        Private Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            If _canExecuteChangedEvent IsNot Nothing Then Return placeInfoControl IsNot Nothing
            Return False
        End Function

        Private Custom Event CanExecuteChanged As System.EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(ByVal value As System.EventHandler)
                AddHandler _canExecuteChanged, value
            End AddHandler

            RemoveHandler(ByVal value As System.EventHandler)
                RemoveHandler _canExecuteChanged, value
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
                RaiseEvent _canExecuteChanged(sender, e)
            End RaiseEvent
        End Event

        Private Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            If Not(TypeOf parameter Is NavDirection) Then Return
            Dim navDirection As NavDirection = CType(parameter, NavDirection)
            Select Case navDirection
                Case NavDirection.Next
                    placeInfoControl.OnShowNextPlace()
                Case NavDirection.Previous
                    placeInfoControl.OnShowPreviousPlace()
            End Select
        End Sub
    End Class

    Public Enum NavDirection
        [Next]
        Previous
    End Enum
End Namespace
