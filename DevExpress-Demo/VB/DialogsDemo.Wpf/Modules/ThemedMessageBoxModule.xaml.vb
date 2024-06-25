Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DialogsDemo.Helpers

Namespace DialogsDemo

    <CodeFile("Helpers/PredefinedFormat.cs")>
    Public Partial Class ThemedMessageBoxModule
        Inherits DemoModule

        Public Sub New()
            InitializeComponent()
            UpdateDefaultButtonSource()
        End Sub

        Private Sub ShowThemedMessageBox(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim commonParameters = New ThemedMessageBoxParameters(CType(icons.EditValue, MessageBoxImage)) With {.AllowTextSelection = allowTextSelectionEdit.IsChecked.Value}
            Dim seconds As Integer
            If Integer.TryParse(CStr(timerTimeout.SelectedItem), seconds) Then
                commonParameters.TimerTimeout = TimeSpan.FromSeconds(seconds)
                If predefinedFormats.EditValue IsNot Nothing Then commonParameters.TimerFormat = predefinedFormats.EditValue?.ToString()
            End If

            dialogResult.Content = ThemedMessageBox.Show(captionEdit.DisplayText, contentEdit.DisplayText, CType(buttons.EditValue, MessageBoxButton), CType(defaultButton.EditValue, MessageBoxResult), commonParameters)
        End Sub

        Private Sub OnButtonsChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateDefaultButtonSource()
        End Sub

        Private Sub UpdateDefaultButtonSource()
            defaultButton.ItemsSource = DialogsDemoHelper.GetMessageBoxResults(CType(buttons.EditValue, MessageBoxButton))
            defaultButton.SelectedIndex = 0
            UpdatePredefinedFormats()
        End Sub

        Private Sub GenerateFormats(ByVal defaultButton As MessageBoxResult, ByVal timeout As Integer)
            Dim formats = New List(Of PredefinedFormat)()
            formats.Add(New PredefinedFormat($"{defaultButton} ({timeout} sec.)", "{0} ({1:%s} sec.)"))
            formats.Add(New PredefinedFormat($"{defaultButton} {timeout} sec.", "{0} {1:%s} sec."))
            formats.Add(New PredefinedFormat($"{defaultButton} {timeout} secons to close", "{0} {1:%s} secons to close"))
            formats.Add(New PredefinedFormat($"{defaultButton} close after {timeout} sec.", "{0} close after {1:%s} sec."))
            predefinedFormats.ItemsSource = formats
            predefinedFormats.SelectedIndex = 0
        End Sub

        Private Sub UpdatePredefinedFormats()
            Dim seconds As Integer
            If Integer.TryParse(CStr(timerTimeout.SelectedItem), seconds) Then
                GenerateFormats(CType(defaultButton.EditValue, MessageBoxResult), seconds)
            End If
        End Sub

        Private Sub OnDefaultButtonChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            UpdatePredefinedFormats()
        End Sub

        Private Sub OnTimerTimeoutChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            UpdatePredefinedFormats()
        End Sub
    End Class
End Namespace
