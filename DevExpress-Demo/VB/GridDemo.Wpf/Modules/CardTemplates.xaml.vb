Imports System.Windows

Namespace GridDemo

    Public Partial Class CardTemplates
        Inherits GridDemoModule

#Region "SelectedTabIndex attached property"
        Public Shared ReadOnly SelectedTabIndexProperty As DependencyProperty = DependencyProperty.RegisterAttached("SelectedTabIndex", GetType(Integer), GetType(CardTemplates), New PropertyMetadata(0, Nothing, Function(d, baseValue) If((CInt(baseValue) = -1), GetSelectedTabIndex(d), baseValue)))

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
