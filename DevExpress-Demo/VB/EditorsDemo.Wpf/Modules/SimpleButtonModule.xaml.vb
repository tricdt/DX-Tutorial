Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Bars
Imports DevExpress.Mvvm
Imports System.Threading.Tasks

Namespace EditorsDemo

    Public Partial Class SimpleButtonModule
        Inherits EditorsDemoModule

        Public ReadOnly Property EventLog As ObservableCollection(Of String)

        Private asynCommand As AsyncCommand

        Public Sub New()
            InitializeComponent()
            EventLog = New ObservableCollection(Of String)()
            rbSimpleButton.IsChecked = True
            asynCommand = New AsyncCommand(AddressOf OnAsyncButton)
            simpleButtonAsync.Command = asynCommand
        End Sub

        Private Sub OnRadioButtonChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim radioButton As RadioButton = TryCast(sender, RadioButton)
            Select Case radioButton.Name
                Case "rbSimpleButton"
                    propertyGrid.SelectedObject = simpleButton
                Case "rbSplitButton"
                    propertyGrid.SelectedObject = splitButton
                Case "rbDropDownButton"
                    propertyGrid.SelectedObject = dropdownButton
                Case "rbAsyncSimpleButton"
                    propertyGrid.SelectedObject = simpleButtonAsync
            End Select
        End Sub

        Private Sub AddStringInLog(ByVal message As String)
            EventLog.Add(message)
            If EventLog.Count > 20 Then EventLog.RemoveAt(0)
        End Sub

        Private Sub AddStringInLog(ByVal button As Object)
            Dim buttonItem = TryCast(button, BarButtonItem)
            If buttonItem IsNot Nothing Then
                AddStringInLog(String.Format("{0} - {1} '{2}' Click", Date.Now.ToShortTimeString(), buttonItem.GetType().Name, buttonItem.Content))
            Else
                AddStringInLog(String.Format("{0} - {1} Click", Date.Now.ToShortTimeString(), button.GetType().Name))
            End If
        End Sub

        Private Sub OnButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = TryCast(sender, SimpleButton)
            If button IsNot Nothing AndAlso button.ButtonKind = ButtonKind.Toggle Then AddStringInLog(String.Format("{0} - {1} state is: {2}", Date.Now.ToShortTimeString(), button.GetType().Name, button.IsChecked))
            AddStringInLog(sender)
        End Sub

        Private Sub OnBarButtonItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            AddStringInLog(sender)
        End Sub

        Private Async Function OnAsyncButton() As Task
            AddStringInLog(String.Format("{0} - Async SimpleButton Action Started", Date.Now.ToShortTimeString()))
            Dim idx = 0
            While Math.Min(Threading.Interlocked.Increment(idx), idx - 1) < 300
                If asynCommand.IsCancellationRequested Then
                    AddStringInLog(String.Format("{0} - Async SimpleButton: Action Cancelled", Date.Now.ToShortTimeString()))
                    Return
                End If

                Await Task.Delay(10)
            End While

            AddStringInLog(String.Format("{0} - Async SimpleButton: Action Finished", Date.Now.ToShortTimeString()))
        End Function
    End Class
End Namespace
