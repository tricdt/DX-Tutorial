Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class ProgressBarEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            cbContentSettings.ItemsSource = New List(Of ContentDisplayMode) From {ContentDisplayMode.None, ContentDisplayMode.Value}
            Call ComboBoxEdit.SetupComboBoxEnumItemSource(Of Orientation, Orientation)(cbOrientation)
            cbOrientation.EditValue = Orientation.Horizontal
            SetupOrientation()
            DataContext = New OutlookViewModel()
        End Sub

        Private Sub SetupOrientation()
            If Equals(cbOrientation.EditValue, Orientation.Horizontal) Then
                editor.Width = 400
                editor.ClearValue(HeightProperty)
                editor.Orientation = Orientation.Horizontal
            Else
                editor.Height = 400
                editor.ClearValue(WidthProperty)
                editor.Orientation = Orientation.Vertical
            End If
        End Sub

        Private Sub cbBarStyle_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If cbBarStyle.SelectedIndex = 0 Then
                editor.StyleSettings = New ProgressBarStyleSettings()
            Else
                editor.StyleSettings = New ProgressBarMarqueeStyleSettings()
            End If
        End Sub

        Private Sub CheckEdit_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If String.IsNullOrEmpty(TryCast(cbDisplayFormat.EditValue, String)) OrElse cbDisplayFormat.SelectedIndex = 0 Then cbDisplayFormat.SelectedIndex = 1
        End Sub

        Private Sub CheckEdit_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If cbDisplayFormat.SelectedIndex = 1 Then cbDisplayFormat.SelectedIndex = 0
        End Sub

        Private Sub cbDisplayFormat_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Try
                editor.DisplayFormatString = CStr(e.NewValue)
            Catch
                editor.DisplayFormatString = ""
            End Try
        End Sub

        Private Sub cbOrientation_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            SetupOrientation()
        End Sub
    End Class
End Namespace
