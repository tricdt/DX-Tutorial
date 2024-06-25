Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Grid
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("Controls/Converters.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GridCellMultiColumnLookupEditorTemplates.xaml")>
    Public Partial Class CellEditors
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim settings As ComboBoxEditSettings = New ComboBoxEditSettings() With {.IsTextEditable = False}
            LookUpEditBase.SetupComboBoxSettingsEnumItemSource(Of Priority)(settings)
            colPriority.EditSettings = settings
            grid.ItemsSource = CreateOutlookDataTable(1000)
            AddHandler booleanColumnEditorListBox.EditValueChanged, New EditValueChangedEventHandler(AddressOf booleanColumnEditorListBox_EditValueChanged)
            AddHandler editorButtonShowModeListBox.EditValueChanged, New EditValueChangedEventHandler(AddressOf editorButtonShowModeListBox_EditValueChanged)
            AddHandler viewsListBox.EditValueChanged, New EditValueChangedEventHandler(AddressOf viewsListBox_SelectionChanged)
            alternativeDisplayTemplateCheckBox.IsChecked = True
            alternativeEditTemplateCheckBox.IsChecked = True
            AddHandler editorShowModeCombobox.EditValueChanged, New EditValueChangedEventHandler(AddressOf editorShowModeCombobox_EditValueChanged)
        End Sub

#Region "options"
        Private Sub editorShowModeCombobox_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateEditorShowMode()
        End Sub

        Private Sub viewsListBox_SelectionChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If viewsListBox.SelectedIndex = 0 Then
                grid.View = CType(FindResource("columnView"), GridViewBase)
            End If

            If viewsListBox.SelectedIndex = 1 Then
                grid.View = CType(FindResource("cardView"), GridViewBase)
            End If

            UpdateEditorButtonShowMode()
            UpdateEditorShowMode()
        End Sub

        Private Sub booleanColumnEditorListBox_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If booleanColumnEditorListBox.SelectedIndex = 0 Then
                colHasAttachment.EditSettings = Nothing
            End If

            If booleanColumnEditorListBox.SelectedIndex = 1 Then
                colHasAttachment.EditSettings = New TextEditSettings()
            End If

            If booleanColumnEditorListBox.SelectedIndex = 2 Then
                colHasAttachment.EditSettings = New ComboBoxEditSettings() With {.ItemsSource = New Boolean() {True, False}}
            End If
        End Sub

        Private Sub editorButtonShowModeListBox_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateEditorButtonShowMode()
        End Sub

        Private Sub UpdateEditorButtonShowMode()
            If editorButtonShowModeListBox.SelectedIndex = 0 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowOnlyInEditor
            End If

            If editorButtonShowModeListBox.SelectedIndex = 1 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedCell
            End If

            If editorButtonShowModeListBox.SelectedIndex = 2 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedRow
            End If

            If editorButtonShowModeListBox.SelectedIndex = 3 Then
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowAlways
            End If
        End Sub

        Private Sub UpdateEditorShowMode()
            If editorShowModeCombobox.SelectedIndex = 0 Then
                grid.View.EditorShowMode = EditorShowMode.MouseDown
            End If

            If editorShowModeCombobox.SelectedIndex = 1 Then
                grid.View.EditorShowMode = EditorShowMode.MouseDownFocused
            End If

            If editorShowModeCombobox.SelectedIndex = 2 Then
                grid.View.EditorShowMode = EditorShowMode.MouseUp
            End If

            If editorShowModeCombobox.SelectedIndex = 3 Then
                grid.View.EditorShowMode = EditorShowMode.MouseUpFocused
            End If
        End Sub

        Private Sub colHoursActive_Validate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            Dim value As Double = Convert.ToDouble(e.Value, e.Culture)
            If value <= 0 OrElse 1000 < value Then
                e.SetError("The Hours Active value must be greater than zero and less than or equal to 1000", ErrorType.Default)
            End If
        End Sub

#End Region
#Region "colHoursActive options"
        Private Sub alternativeDisplayTemplateCheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.CellDisplayTemplate = CType(Resources("alternativeHoursActiveDisplayTemplate"), DataTemplate)
        End Sub

        Private Sub alternativeDisplayTemplateCheckBox_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.CellDisplayTemplate = Nothing
        End Sub

        Private Sub alternativeEditTemplateCheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.CellEditTemplate = CType(Resources("alternativeHoursActiveEditTemplate"), DataTemplate)
        End Sub

        Private Sub alternativeEditTemplateCheckBox_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            colHoursActive.CellEditTemplate = Nothing
        End Sub

#End Region
#Region "custom cell editors events"
        Private Sub TableView_GetIsEditorActivationAction(ByVal sender As Object, ByVal e As GetIsEditorActivationActionEventArgs)
            If Equals(e.Column.FieldName, "HoursActive") AndAlso e.ActivationAction = ActivationAction.KeyDown Then
                e.IsActivationAction = IsSliderCommand(e.KeyDownEventArgs.Key) AndAlso (Keyboard.Modifiers = ModifierKeys.None OrElse Keyboard.Modifiers = ModifierKeys.Control)
            End If
        End Sub

        Private Sub TableView_ProcessEditorActivationAction(ByVal sender As Object, ByVal e As ProcessEditorActivationActionEventArgs)
            If Equals(e.Column.FieldName, "HoursActive") AndAlso e.ActivationAction = ActivationAction.KeyDown Then
                e.RaiseEventAgain = IsSliderCommand(e.KeyDownEventArgs.Key)
            End If

            If Equals(e.Column.FieldName, "HoursActive") AndAlso e.ActivationAction = ActivationAction.MouseLeftButtonDown Then
                e.RaiseEventAgain = True
            End If
        End Sub

        Private Function IsSliderCommand(ByVal key As Key) As Boolean
            Select Case key
                Case Key.Add, Key.Subtract, Key.OemPlus, Key.OemMinus
                    Return True
                Case Else
                    Return False
            End Select
        End Function
#End Region
    End Class
End Namespace
