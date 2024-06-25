Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.Docking.Base

Namespace DockingDemo

    Public Partial Class DockHintsVisualizer
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler DemoDockContainer.ShowingDockHints, AddressOf OnShowingDockHints
        End Sub

        Public ReadOnly Property AllowVisualizerEventsChecked As Boolean
            Get
                Return allowVisualizerEvents.IsChecked.Value
            End Get
        End Property

        Private Sub OnShowingDockHints(ByVal sender As Object, ByVal e As ShowingDockHintsEventArgs)
            If Not AllowVisualizerEventsChecked Then Return
            If Equals(e.DraggingTarget, Panel1) OrElse (TypeOf e.DraggingTarget Is LayoutGroup) Then
                e.HideAll()
                Return
            End If

            If Equals(e.DraggingTarget, Panel2) Then
                Return
            End If

            If Equals(e.DraggingTarget, Panel3) Then
                e.DisableAll()
                Return
            End If

            If Equals(e.DraggingTarget, Panel4) Then
                e.Disable(DockHint.SideLeft)
                e.Disable(DockHint.AutoHideLeft)
                e.Disable(DockHint.CenterLeft)
                e.Disable(DockHint.CenterRight)
                e.Hide(DockGuide.Top)
                e.Hide(DockGuide.Bottom)
                Return
            End If
        End Sub
    End Class
End Namespace
