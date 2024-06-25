Imports System.Windows
Imports DevExpress.Xpf.Editors

Namespace LayoutControlDemo

    Public Partial Class pageFlowLayoutControl
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub StretchContentCheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each child In layoutItems.GetLogicalChildren(False)
                child.Width = Double.NaN
            Next
        End Sub

        Private Sub AllowLayerSizing_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            layoutItems.ShowLayerSeparators = CBool(e.NewValue)
            layoutItems.AllowLayerSizing = CBool(e.NewValue)
        End Sub
    End Class
End Namespace
