Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Bars
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace BarsDemo

    Public Class RootView
        Inherits ContentControl

        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(RootView), New FrameworkPropertyMetadata(GetType(RootView)))
        End Sub

        Protected Overrides Sub OnContentChanged(ByVal oldContent As Object, ByVal newContent As Object)
            MyBase.OnContentChanged(oldContent, newContent)
            TryCast(newContent, DependencyObject).[Do](Sub(x) BindingOperations.SetBinding(x, MergingProperties.ToolBarMergeStyleProperty, New Binding() With {.Path = New PropertyPath(MergingProperties.ToolBarMergeStyleProperty), .Source = Me}))
        End Sub
    End Class
End Namespace
