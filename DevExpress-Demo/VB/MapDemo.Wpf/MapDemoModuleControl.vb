Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Class MapDemoModule
        Inherits DemoModule

        Protected Overrides Sub Hide()
            For Each map As MapControl In FindMap(Me)
                map.HideToolTip()
            Next

            MyBase.Hide()
        End Sub
    End Class
End Namespace
