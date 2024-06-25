Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Printing
Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Input

Namespace GanttDemo

    Public MustInherit Class PrintingViewModelBase
        Inherits ViewModelBase

        Public Property PrintableControl As IPrintableControl

        <Command>
        Public Overridable Sub ReOpenPreviewTabs()
            Dim service = GetService(Of IDocumentManagerService)()
            For Each document In service.Documents.ToArray()
                OnTabClosed(document.Content)
                document.Close()
            Next

            ShowPreviewInNewTab()
        End Sub

        <Command>
        Public Overridable Sub ShowPreviewInNewTab()
            Call Logger.Log("ShowPreviewInNewTab")
            Dim service = GetService(Of IDocumentManagerService)()
            Dim link = New PrintableControlLink(PrintableControl)
            link.CreateDocument(True)
            Dim doc = service.CreateDocument(link)
            doc.Title = "Print Preview"
            doc.DestroyOnClose = True
            doc.Show()
        End Sub

        Public Sub OnTabClosed(ByVal tabContent As Object)
            CType(tabContent, PrintableControlLink).Dispose()
        End Sub
    End Class

    Public Class PrintableTabBehavior
        Inherits Behavior(Of DataViewBase)

        Public Property PrintableControl As IPrintableControl
            Get
                Return CType(GetValue(PrintableControlProperty), IPrintableControl)
            End Get

            Set(ByVal value As IPrintableControl)
                SetValue(PrintableControlProperty, value)
            End Set
        End Property

        Public Shared ReadOnly PrintableControlProperty As DependencyProperty = DependencyProperty.Register("PrintableControl", GetType(IPrintableControl), GetType(PrintableTabBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ShowPreviewTab As ICommand
            Get
                Return CType(GetValue(ShowPreviewTabProperty), ICommand)
            End Get

            Set(ByVal value As ICommand)
                SetValue(ShowPreviewTabProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ShowPreviewTabProperty As DependencyProperty = DependencyProperty.Register("ShowPreviewTab", GetType(ICommand), GetType(PrintableTabBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            PrintableControl = AssociatedObject
            AddHandler AssociatedObject.Loaded, AddressOf AssociatedObject_Loaded
        End Sub

        Private Sub AssociatedObject_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim action As Action = Sub()
                If ShowPreviewTab IsNot Nothing Then ShowPreviewTab.Execute(Nothing)
            End Sub
            AssociatedObject.Dispatcher.BeginInvoke(action, Threading.DispatcherPriority.Loaded)
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf AssociatedObject_Loaded
            PrintableControl = Nothing
        End Sub
    End Class
End Namespace
