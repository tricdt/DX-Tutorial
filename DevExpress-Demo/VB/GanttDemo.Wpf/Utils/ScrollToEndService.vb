Imports DevExpress.Mvvm.UI
Imports System.Windows.Controls

Namespace GanttDemo

    Public Interface IScrollToEndService

        Sub ScrollToEnd()

    End Interface

    Public Class ScrollToEndService
        Inherits ServiceBase
        Implements IScrollToEndService

        Private ReadOnly Property ActualScrollViewer As ScrollViewer
            Get
                Return TryCast(AssociatedObject, ScrollViewer)
            End Get
        End Property

        Private Sub ScrollToEnd() Implements IScrollToEndService.ScrollToEnd
            Dim scrollViewer = ActualScrollViewer
            If scrollViewer Is Nothing Then Return
            scrollViewer.ScrollToEnd()
        End Sub
    End Class
End Namespace
