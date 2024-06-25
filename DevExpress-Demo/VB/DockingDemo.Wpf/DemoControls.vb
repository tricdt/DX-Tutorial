Imports DevExpress.Xpf.Editors
Imports System.Windows.Controls

Namespace DockingDemo

    Public Class CodeViewPresenter
        Inherits DevExpress.Xpf.DemoBase.Helpers.Internal.CodeViewPresenter

    End Class

    Public Class AutoScrollableTextEdit
        Inherits TextEdit

        Public Sub New()
            AddHandler EditValueChanged, AddressOf AutoScrollableTextEdit_EditValueChanged
        End Sub

        Private Sub AutoScrollableTextEdit_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Dispatcher.BeginInvoke(New System.Action(Sub() ScrollToEnd()))
        End Sub

        Public Sub ScrollToEnd()
            If EditCore IsNot Nothing Then CType(EditCore, TextBox).ScrollToEnd()
        End Sub
    End Class
End Namespace
