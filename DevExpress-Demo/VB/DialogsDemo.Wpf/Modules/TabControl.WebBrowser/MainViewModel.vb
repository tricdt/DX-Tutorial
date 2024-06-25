Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace CommonDemo.TabControl.WebBrowser

    Public Class MainViewModel

        Public Sub LaunchDemo()
            GetService(Of IWindowService).Show("WebBrowserView", Nothing, Me)
        End Sub
    End Class
End Namespace
