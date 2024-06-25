Imports System.Windows.Controls
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.PdfViewer
Imports DevExpress.Xpf.PdfViewer.Helpers

Namespace ProductsDemo.Modules

    Public Partial Class PdfViewerDemo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            Dim source As Object = Utils.GetRelativePath("Demo.pdf")
            DataContext = ViewModelSource.Create(Function() New MainViewModel With {.PdfSource = source})
        End Sub
    End Class

    Public Class MainViewModel

        Public Overridable Property PdfSource As Object

        Public Sub ShowNewDocument()
        End Sub
    End Class

    Public Class PdfDocumentAttachedBehavior
        Inherits Behavior(Of PdfViewerControl)

        Private ReadOnly Property Model As MainViewModel
            Get
                Return TryCast(AssociatedObject.DataContext, MainViewModel)
            End Get
        End Property
    End Class
End Namespace
