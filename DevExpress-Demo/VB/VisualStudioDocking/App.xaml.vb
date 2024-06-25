Imports DevExpress.Xpf.DemoBase
Imports System.Windows

Namespace VisualStudioDocking

    Public Partial Class App
        Inherits Application

        Shared Sub New()
            Call DemoBaseControl.SetApplicationTheme()
        End Sub

#If DEBUG
        Public ReadOnly Property IsDebug As Boolean
            Get
                Return True
            End Get
        End Property
#End If
    End Class
End Namespace
