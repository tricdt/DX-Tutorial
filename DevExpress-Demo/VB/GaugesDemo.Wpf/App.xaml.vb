Imports System.Windows
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoCenterBase

Namespace WpfDemo

    Public Partial Class App
        Inherits Application

        Shared Sub New()
            Call DemoBaseControl.SetApplicationTheme()
            DemoRunner.ShowApplicationSplashScreen(allowDrag:=True)
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
