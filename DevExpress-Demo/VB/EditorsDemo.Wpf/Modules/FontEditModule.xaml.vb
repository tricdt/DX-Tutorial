Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings

Namespace EditorsDemo

    Public Partial Class FontEditModule
        Inherits EditorsDemoModule

        Private selectedColorField As Color = Colors.Black

        Private Property SelectedColor As Color
            Get
                Return selectedColorField
            End Get

            Set(ByVal value As Color)
                selectedColorField = value
                If richControl IsNot Nothing Then richControl.TextColor = value
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
            richControl.Text = "The DXEditors Library offers a collection of advanced data editors available for use within data entry forms, option editors and data-aware controls. Our editors provide seamless integration with the rest of our product line, including the data grid and toolbar-menu controls. When it comes to data input and representation, the DevExpress Data Editors Library is unmatched in providing the same level of customization and flexibility."
        End Sub

        <DefaultValue(False)>
        Private Property IsInUpdate As Boolean

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CType(eFontSize.EditSettings, ComboBoxEditSettings).ItemsSource = FontSizes.Sizes
            UpdateBarsValues()
            richControl.Focus()
        End Sub

        Private Sub eFontFamily_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If IsInUpdate Then Return
            richControl.TextFontFamily = eFontFamily.EditValue
            FocusRichControl()
        End Sub

        Private Sub eFontSize_EditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If IsInUpdate Then Return
            richControl.TextFontSize = eFontSize.EditValue
            FocusRichControl()
        End Sub

        Private Sub richControl_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateBarsValues()
        End Sub

        Private Sub UpdateBarsValues()
            IsInUpdate = True
            eFontFamily.EditValue = richControl.TextFontFamily
            eFontSize.EditValue = richControl.TextFontSize
            Dim textAlignment As TextAlignment = richControl.GetTextAlignment()
            bLeft.IsChecked = Equals(textAlignment, TextAlignment.Left)
            bCenter.IsChecked = Equals(textAlignment, TextAlignment.Center)
            bRight.IsChecked = Equals(textAlignment, TextAlignment.Right)
            bBold.IsChecked = richControl.TextIsBold
            bItalic.IsChecked = richControl.TextIsItalic
            bUnderline.IsChecked = richControl.TextIsUnderline
            IsInUpdate = False
        End Sub

        Private Sub FocusRichControl()
            richControl.Dispatcher.BeginInvoke(DispatcherPriority.Normal, New ThreadStart(Sub() richControl.Focus()))
        End Sub

        Private Sub fontColorChooser_ColorChanged(ByVal sender As Object, ByVal e As EventArgs)
            If IsInUpdate Then Return
            SelectedColor = CType(sender, ColorChooser).Color
            FocusRichControl()
        End Sub

        Private Sub bBold_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then Return
            richControl.TextIsBold = CBool(bBold.IsChecked)
            FocusRichControl()
        End Sub

        Private Sub bItalic_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then Return
            richControl.TextIsItalic = CBool(bItalic.IsChecked)
            FocusRichControl()
        End Sub

        Private Sub bUnderline_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then Return
            richControl.TextIsUnderline = CBool(bUnderline.IsChecked)
            FocusRichControl()
        End Sub

        Private Sub bLeft_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate OrElse richControl Is Nothing Then Return
            If CBool(CType(sender, BarCheckItem).IsChecked) Then richControl.ToggleTextAlignmentLeft()
            FocusRichControl()
        End Sub

        Private Sub bCenter_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate OrElse richControl Is Nothing Then Return
            If CBool(CType(sender, BarCheckItem).IsChecked) Then richControl.ToggleTextAlignmentCenter()
            FocusRichControl()
        End Sub

        Private Sub bRight_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate OrElse richControl Is Nothing Then Return
            If CBool(CType(sender, BarCheckItem).IsChecked) Then richControl.ToggleTextAlignmentRight()
            FocusRichControl()
        End Sub

        Private Sub bFontColor_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            If IsInUpdate Then Return
            richControl.TextColor = SelectedColor
            FocusRichControl()
        End Sub
    End Class
End Namespace
