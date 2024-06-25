Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm.UI

Namespace DiagramDemo

    Public Interface IScrollToEndService

        Sub ScrollToEnd()

    End Interface

    Public Class ScrollToEndService
        Inherits ServiceBase
        Implements IScrollToEndService

        Public Shared ReadOnly ScrollViewerProperty As DependencyProperty = DependencyProperty.Register("ScrollViewer", GetType(ScrollViewer), GetType(ScrollToEndService), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ScrollViewer As ScrollViewer
            Get
                Return CType(GetValue(ScrollViewerProperty), ScrollViewer)
            End Get

            Set(ByVal value As ScrollViewer)
                SetValue(ScrollViewerProperty, value)
            End Set
        End Property

        Private ReadOnly Property ActualScrollViewer As ScrollViewer
            Get
                Return If(ScrollViewer, TryCast(AssociatedObject, ScrollViewer))
            End Get
        End Property

        Private Sub ScrollToEnd() Implements IScrollToEndService.ScrollToEnd
            Dim scrollViewer = ActualScrollViewer
            If scrollViewer Is Nothing Then Return
            scrollViewer.ScrollToEnd()
        End Sub
    End Class
End Namespace
