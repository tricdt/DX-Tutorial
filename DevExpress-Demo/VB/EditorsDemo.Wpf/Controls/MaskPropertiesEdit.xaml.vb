Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports EditorsDemo.ModuleResources

Namespace EditorsDemo

    Public Partial Class MaskPropertiesEdit
        Inherits UserControl

        Public Shared ReadOnly FocusedEditorProperty As DependencyProperty

        Public Shared ReadOnly SelectedCardInfoProperty As DependencyProperty

        Shared Sub New()
            FocusedEditorProperty = DependencyProperty.Register(NameOf(MaskPropertiesEdit.FocusedEditor), GetType(TextEdit), GetType(MaskPropertiesEdit), New PropertyMetadata(Nothing, Sub(d, e) CType(d, MaskPropertiesEdit).UpdateMask()))
            SelectedCardInfoProperty = DependencyProperty.Register(NameOf(MaskPropertiesEdit.SelectedCardInfo), GetType(CardInfo), GetType(MaskPropertiesEdit), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))
        End Sub

        Public Property FocusedEditor As TextEdit
            Get
                Return CType(GetValue(FocusedEditorProperty), TextEdit)
            End Get

            Set(ByVal value As TextEdit)
                SetValue(FocusedEditorProperty, value)
            End Set
        End Property

        Public Property SelectedCardInfo As CardInfo
            Get
                Return CType(GetValue(SelectedCardInfoProperty), CardInfo)
            End Get

            Set(ByVal value As CardInfo)
                SetValue(SelectedCardInfoProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            SetCustomMaskCases()
        End Sub

        Public Sub UpdateMask()
            UpdateVisibilities()
            If FocusedEditor Is Nothing Then
                cbAutoComplete.SelectedIndex = -1
                chIgnoreMaskBlank.IsChecked = False
                txtEditMask.EditValue = Nothing
                chBeep.IsChecked = False
                cbMaskType.SelectedIndex = -1
                txtPlaceHolder.EditValue = " "c
                cbSaveLiteral.IsChecked = False
                chShowPlaceHolders.IsChecked = False
                chUseMaskAsDisplayFormat.IsChecked = False
                chAllowNull.IsChecked = False
                Return
            End If

            cbAutoComplete.SelectedIndex = CInt(FocusedEditor.MaskAutoComplete)
            chIgnoreMaskBlank.IsChecked = FocusedEditor.MaskIgnoreBlank
            txtEditMask.EditValue = FocusedEditor.Mask
            chBeep.IsChecked = FocusedEditor.MaskBeepOnError
            If FocusedEditor.MaskType = MaskType.DateTime Then
                cbMaskType.SelectedIndex = 0
            ElseIf FocusedEditor.MaskType = MaskType.DateTimeAdvancingCaret Then
                cbMaskType.SelectedIndex = 1
            Else
                cbMaskType.SelectedIndex = -1
            End If

            txtPlaceHolder.EditValue = Convert.ToString(FocusedEditor.MaskPlaceHolder)
            cbSaveLiteral.IsChecked = FocusedEditor.MaskSaveLiteral
            chShowPlaceHolders.IsChecked = FocusedEditor.MaskShowPlaceHolders
            chUseMaskAsDisplayFormat.IsChecked = FocusedEditor.MaskUseAsDisplayFormat
            chAllowNull.IsChecked = FocusedEditor.AllowNullInput
        End Sub

        Private Sub UpdateVisibilities()
            If FocusedEditor Is Nothing Then Return
            cbAutoComplete.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.RegEx), Visibility.Visible, Visibility.Collapsed)
            lblAutoComplete.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.RegEx), Visibility.Visible, Visibility.Collapsed)
            chIgnoreMaskBlank.Visibility = If(FocusedEditor.MaskType = MaskType.Simple OrElse FocusedEditor.MaskType = MaskType.Regular, Visibility.Visible, Visibility.Collapsed)
            cbCardType.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.Custom), Visibility.Visible, Visibility.Collapsed)
            lblCardType.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.Custom), Visibility.Visible, Visibility.Collapsed)
            lblEditValue.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.Custom), Visibility.Collapsed, Visibility.Visible)
            lblEditMask.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.Custom), Visibility.Collapsed, Visibility.Visible)
            txtEditValue.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.Custom), Visibility.Collapsed, Visibility.Visible)
            txtEditMask.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.Custom), Visibility.Collapsed, Visibility.Visible)
            lblMaskType.Visibility = If(FocusedEditor.MaskType = MaskType.DateTime OrElse FocusedEditor.MaskType = MaskType.DateTime, Visibility.Visible, Visibility.Collapsed)
            cbMaskType.Visibility = If(FocusedEditor.MaskType = MaskType.DateTime OrElse FocusedEditor.MaskType = MaskType.DateTimeAdvancingCaret, Visibility.Visible, Visibility.Collapsed)
            txtPlaceHolder.Visibility = If(FocusedEditor.MaskType = MaskType.Simple OrElse FocusedEditor.MaskType = MaskType.Regular OrElse FocusedEditor.MaskType.Equals(MaskType.RegEx), Visibility.Visible, Visibility.Collapsed)
            lblPlaceHolder.Visibility = If(FocusedEditor.MaskType = MaskType.Simple OrElse FocusedEditor.MaskType = MaskType.Regular OrElse FocusedEditor.MaskType.Equals(MaskType.RegEx), Visibility.Visible, Visibility.Collapsed)
            cbSaveLiteral.Visibility = If(FocusedEditor.MaskType = MaskType.Simple OrElse FocusedEditor.MaskType = MaskType.Regular, Visibility.Visible, Visibility.Collapsed)
            chShowPlaceHolders.Visibility = If(FocusedEditor.MaskType.Equals(MaskType.RegEx), Visibility.Visible, Visibility.Collapsed)
            chAllowNull.Visibility = If(FocusedEditor.MaskType = MaskType.DateTime OrElse FocusedEditor.MaskType = MaskType.DateTimeAdvancingCaret OrElse FocusedEditor.MaskType = MaskType.Numeric, Visibility.Visible, Visibility.Collapsed)
        End Sub

        Private Sub SetCustomMaskCases()
            cbCardType.ItemsSource = GetCardTypes()
            cbCardType.SelectedIndex = 0
        End Sub

        Private Sub cbAutoComplete_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskAutoComplete = CType([Enum].Parse(GetType(AutoCompleteType), CStr(CType(sender, ComboBoxEdit).SelectedItem), True), AutoCompleteType)
        End Sub

        Private Sub chIgnoreMaskBlank_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskIgnoreBlank = chIgnoreMaskBlank.IsChecked.Value
        End Sub

        Private Sub txtEditMask_LostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            Dim maskBackup As String = FocusedEditor.Mask
            Try
                FocusedEditor.Mask = CStr(txtEditMask.EditValue)
            Catch
                MessageBox.Show("Invalid mask", "Error")
                FocusedEditor.Mask = maskBackup
                CType(sender, TextEdit).EditValue = maskBackup
            End Try
        End Sub

        Private Sub chBeep_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskBeepOnError = chBeep.IsChecked.Value
        End Sub

        Private Sub cbMaskType_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskType = CType([Enum].Parse(GetType(MaskType), CStr(CType(sender, ComboBoxEdit).SelectedItem), True), MaskType)
        End Sub

        Private Sub txtPlaceHolder_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskPlaceHolder = If(String.IsNullOrEmpty(CStr(txtPlaceHolder.EditValue)), " "c, Convert.ToChar(txtPlaceHolder.EditValue))
        End Sub

        Private Sub cbSaveLiteral_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskSaveLiteral = cbSaveLiteral.IsChecked.Value
        End Sub

        Private Sub chShowPlaceHolders_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskShowPlaceHolders = chShowPlaceHolders.IsChecked.Value
        End Sub

        Private Sub chUseMaskAsDisplayFormat_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.MaskUseAsDisplayFormat = chUseMaskAsDisplayFormat.IsChecked.Value
        End Sub

        Private Sub chAllowNull_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If FocusedEditor Is Nothing Then Return
            FocusedEditor.AllowNullInput = chAllowNull.IsChecked.Value
        End Sub

        Private Sub OncbCardType_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            FocusedEditor?.Clear()
            SelectedCardInfo = CType(cbCardType.SelectedItem, CardInfo)
            FocusedEditor?.Focus()
        End Sub

        Private Function GetCardTypes() As List(Of CardInfo)
            Dim list = New List(Of CardInfo)()
            list.Add(New CardInfo("Visa/MC", "XXXX XXXX XXXX XXXX"))
            list.Add(New CardInfo("Amex", "XXXX XXXXXX XXXXX"))
            list.Add(New CardInfo("DX Bank", "XXX XXX XXX XXX XX"))
            Return list
        End Function
    End Class
End Namespace
