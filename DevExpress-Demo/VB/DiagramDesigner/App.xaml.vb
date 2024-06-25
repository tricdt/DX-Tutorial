Imports System.Windows
Imports DevExpress.Xpf.Core

Namespace DiagramDesigner

    Public Partial Class App
        Inherits Application

        Public Sub New()
            ApplicationThemeHelper.ApplicationThemeName = Theme.Default.Name
        End Sub
    End Class
End Namespace
