Imports System.Windows
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.UI
Imports DevExpress.DevAV.Common.ViewModel

Namespace DevExpress.DevAV.Common.View

    Public Class MainWindowService
        Inherits ServiceBase
        Implements IMainWindowService

        Public Property Title As String Implements IMainWindowService.Title
            Get
                Return Application.Current.[With](Function(a) a.MainWindow).[With](Function(w) w.Title)
            End Get

            Set(ByVal value As String)
                Call Application.Current.[With](Function(a) a.MainWindow).[Do](Sub(w) w.Title = value)
            End Set
        End Property
    End Class
End Namespace
