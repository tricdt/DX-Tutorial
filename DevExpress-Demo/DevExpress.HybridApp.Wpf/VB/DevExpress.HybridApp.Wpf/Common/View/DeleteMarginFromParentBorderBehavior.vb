Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Mvvm.UI.Interactivity

Namespace DevExpress.DevAV.Common.View

    Public Class DeleteMarginFromParentBorderBehavior
        Inherits Behavior(Of FrameworkElement)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            SearchBorderAndMarginReset(AssociatedObject)
        End Sub

        Private Sub SearchBorderAndMarginReset(ByVal obj As FrameworkElement)
            While obj IsNot Nothing AndAlso Not Equals(obj.Name, "Border")
                obj = TryCast(VisualTreeHelper.GetParent(obj), FrameworkElement)
            End While

            If obj Is Nothing Then Return
            obj.Margin = New Thickness(0)
        End Sub
    End Class
End Namespace
