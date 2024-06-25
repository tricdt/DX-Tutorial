Namespace ChartsDemo

    Public Partial Class Area3DTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private isAnimationCompletedField As Boolean = False

        Public Overrides ReadOnly Property IsAnimationCompleted As Boolean
            Get
                Return isAnimationCompletedField
            End Get
        End Property

        Private Sub Storyboard_Completed(ByVal sender As Object, ByVal e As System.EventArgs)
            isAnimationCompletedField = True
        End Sub
    End Class
End Namespace
