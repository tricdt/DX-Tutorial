Imports DevExpress.Xpf.Core

Namespace DevExpress.SalesDemo.Wpf

    Public Partial Class MainWindow
        Inherits ThemedWindow

        Public Sub New()
            Utils.About.UAlgo.Default.DoEventObject(Utils.About.UAlgo.kDemo, Utils.About.UAlgo.pWPF, Me)
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
