Imports System.Collections.Generic
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class SpinEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            InitSources()
        End Sub

        Private Sub InitSources()
            Dim styles As List(Of SpinStyle) = New List(Of SpinStyle)()
            styles.Add(SpinStyle.Vertical)
            styles.Add(SpinStyle.Horizontal)
            cboSpinStyle.ItemsSource = styles
        End Sub

        Private Sub cboSpinStyle_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            CType(editor.ActualButtons(0), SpinButtonInfo).SpinStyle = CType(e.NewValue, SpinStyle)
        End Sub
    End Class
End Namespace
