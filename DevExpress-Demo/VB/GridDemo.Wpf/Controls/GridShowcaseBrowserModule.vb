Imports DevExpress.Xpf.DemoBase

Namespace GridDemo

    Public MustInherit Class GridShowcaseBrowserModule
        Inherits ShowcaseBrowserModule

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme As Boolean
            Get
                Return False
            End Get
        End Property

        Protected Overridable Sub LoadDemoData()
        End Sub
    End Class
End Namespace
