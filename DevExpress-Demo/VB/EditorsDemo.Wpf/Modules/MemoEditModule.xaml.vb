Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace EditorsDemo

    Public Partial Class MemoEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            editor.EditValue = "MemoEdit is a multi-line text editor. In addition to the advanced text input features derived from the TextEdit control, it offers numerous options for multi-line text management.
- Optional ENTER and TAB key processing.
- Customizable visibility for vertical and horizontal scrollbars.
- Optional text word-wrapping."
            InitSources()
            DataContext = CarsData.DataSource
        End Sub

        Private Sub InitSources()
            Dim wrapping As TextWrapping() = New TextWrapping() {TextWrapping.Wrap, TextWrapping.NoWrap, TextWrapping.WrapWithOverflow}
            lbTextWrapping.ItemsSource = wrapping
            Dim scrollVisibilities As ScrollBarVisibility() = New ScrollBarVisibility() {ScrollBarVisibility.Auto, ScrollBarVisibility.Disabled, ScrollBarVisibility.Hidden, ScrollBarVisibility.Visible}
            cbHorizontalScroll.ItemsSource = scrollVisibilities
            cbVerticalScroll.ItemsSource = scrollVisibilities
        End Sub
    End Class
End Namespace
