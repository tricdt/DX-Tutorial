Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid

Namespace DevExpress.DevAV

    Public Class GridColumnsBehavior
        Inherits Behavior(Of GridControl)

        Public Shared ReadOnly ColumnsVisibilityProperty As DependencyProperty = DependencyProperty.Register("ColumnsVisibility", GetType(Visibility), GetType(GridColumnsBehavior), New FrameworkPropertyMetadata(Visibility.Visible, Sub(d, e) CType(d, GridColumnsBehavior).OnColumnsVisibilityChanged(e)))

        Public Property ColumnsVisibility As Visibility
            Get
                Return CType(GetValue(ColumnsVisibilityProperty), Visibility)
            End Get

            Set(ByVal value As Visibility)
                SetValue(ColumnsVisibilityProperty, value)
            End Set
        End Property

        Public Property InvisibleColumnsName As String()

        Private Sub OnColumnsVisibilityChanged(ByVal e As DependencyPropertyChangedEventArgs)
            If AssociatedObject Is Nothing OrElse AssociatedObject.Columns Is Nothing Then Return
            If ColumnsVisibility = Visibility.Collapsed Then
                For Each name As String In InvisibleColumnsName
                    Dim columnName = name
                    Dim column As GridColumn = AssociatedObject.Columns.SingleOrDefault(Function(x) Equals(x.FieldName, columnName))
                    If column IsNot Nothing Then column.Visible = False
                Next
            Else
                Dim visibleIndex As Integer = 0
                For Each column In AssociatedObject.Columns
                    column.Visible = True
                    column.VisibleIndex = System.Math.Min(System.Threading.Interlocked.Increment(visibleIndex), visibleIndex - 1)
                Next
            End If
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
