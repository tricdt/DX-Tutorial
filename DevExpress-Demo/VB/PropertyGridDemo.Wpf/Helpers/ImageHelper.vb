Imports System
Imports System.Windows.Media
Imports DevExpress.Xpf.Core

Namespace PropertyGridDemo

    Public Module ImageHelper

        Public Function GetSvgImage(ByVal imageName As String) As ImageSource
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(String.Format("pack://application:,,,/PropertyGridDemo;component/Images/{0}.svg", imageName)), .Size = New Windows.Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function
    End Module
End Namespace
