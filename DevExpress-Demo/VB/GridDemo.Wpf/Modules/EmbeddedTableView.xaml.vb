Imports System.Windows

Namespace GridDemo

    Public Partial Class EmbeddedTableView
        Inherits GridDemoModule

#Region "SelectedTabIndex attached property"
        Public Shared ReadOnly SelectedTabIndexProperty As DependencyProperty = DependencyProperty.RegisterAttached("SelectedTabIndex", GetType(Integer), GetType(EmbeddedTableView), New PropertyMetadata(0))

        Public Shared Sub SetSelectedTabIndex(ByVal element As DependencyObject, ByVal value As Integer)
            element.SetValue(SelectedTabIndexProperty, value)
        End Sub

        Public Shared Function GetSelectedTabIndex(ByVal element As DependencyObject) As Integer
            Return CInt(element.GetValue(SelectedTabIndexProperty))
        End Function

#End Region
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
