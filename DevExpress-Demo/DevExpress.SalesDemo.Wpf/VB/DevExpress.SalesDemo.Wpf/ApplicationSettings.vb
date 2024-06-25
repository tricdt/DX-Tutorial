Imports System
Imports System.Windows

Namespace DevExpress.SalesDemo.Wpf

    Public Class ApplicationSettings

        Public Shared ReadOnly Property [Default] As ApplicationSettings
            Get
                Return CType(Application.Current.Resources("ApplicationSettings"), ApplicationSettings)
            End Get
        End Property

        Public Property MainWindowTitle As String

        Public Property MainWindowMinWidth As Double

        Public Property MainWindowMinHeight As Double

        Public Property MainWindowWidth As Double

        Public Property MainWindowHeight As Double

        Public Property MainWindowIcon As Uri

        Public Property MainWindowState As WindowState
    End Class
End Namespace
