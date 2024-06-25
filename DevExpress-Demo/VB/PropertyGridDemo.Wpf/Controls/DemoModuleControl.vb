Imports System
Imports DevExpress.Xpf.DemoBase

Namespace PropertyGridDemo

    Public Class PropertyGridDemoModule
        Inherits DemoModule

        Shared Sub New()
            Dim ownerType As Type = GetType(PropertyGridDemoModule)
        End Sub

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme As Boolean
            Get
                Return False
            End Get
        End Property
    End Class
End Namespace
