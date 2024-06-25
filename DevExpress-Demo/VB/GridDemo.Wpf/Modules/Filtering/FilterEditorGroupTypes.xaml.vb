Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.FilteringUI

Namespace GridDemo.Filtering

    Public Partial Class FilterEditorGroupTypes
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub OnQueryGroupTypes(ByVal sender As Object, ByVal e As QueryGroupTypesEventArgs)
            e.AllowNotOr = False
            e.AllowOr = False
        End Sub
#End Region
    End Class
End Namespace
