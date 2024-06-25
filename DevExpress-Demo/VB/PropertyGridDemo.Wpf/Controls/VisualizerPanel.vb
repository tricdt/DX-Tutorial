Imports System
Imports System.Windows.Controls

Namespace PropertyGridDemo

    Public Class VisualizerPanel
        Inherits StackPanel

        Public Event Changed As EventHandler

        Protected Overrides Sub OnVisualChildrenChanged(ByVal visualAdded As Windows.DependencyObject, ByVal visualRemoved As Windows.DependencyObject)
            MyBase.OnVisualChildrenChanged(visualAdded, visualRemoved)
            Dispatcher.BeginInvoke(New Action(AddressOf RaiseChanged))
        End Sub

        Private Sub RaiseChanged()
            RaiseEvent Changed(Me, EventArgs.Empty)
        End Sub
    End Class
End Namespace
