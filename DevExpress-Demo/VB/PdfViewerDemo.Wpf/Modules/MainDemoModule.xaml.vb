Imports System.Reflection
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase

Namespace PdfViewerDemo

    <CodeFile("ViewModels/MainViewModel.(cs)")>
    Public Partial Class MainDemoModule
        Inherits PdfViewerDemoModule

        Public Sub New()
            InitializeComponent()
            Dim currentAssembly = Assembly.GetExecutingAssembly()
            DataContext = ViewModelSource.Create(Function() New MainViewModel With {.PdfStream = AssemblyHelper.GetResourceStream(currentAssembly, "Data/Demo.pdf", True)})
        End Sub
    End Class
End Namespace
