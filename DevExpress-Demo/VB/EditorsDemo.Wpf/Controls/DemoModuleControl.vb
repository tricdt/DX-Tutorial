Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo

    Public Class EditorsDemoModule
        Inherits DemoModule

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme As Boolean
            Get
                Return False
            End Get
        End Property
    End Class
End Namespace

Namespace CommonDemo

    Public Class CommonDemoModule
        Inherits EditorsDemo.EditorsDemoModule

    End Class
End Namespace
