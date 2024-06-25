Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public MustInherit Class MVVMDemoModule
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
