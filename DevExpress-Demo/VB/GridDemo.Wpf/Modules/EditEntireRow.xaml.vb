Namespace GridDemo

    Public Partial Class EditEntireRow
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub HidePopupContent()
            view.CancelRowChanges()
            MyBase.HidePopupContent()
        End Sub

        Protected Overrides Sub ShowPopupContent()
            view.CancelRowChanges()
            MyBase.ShowPopupContent()
        End Sub
    End Class
End Namespace
