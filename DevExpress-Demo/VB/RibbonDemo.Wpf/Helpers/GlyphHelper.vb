Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace RibbonDemo

    Public Class GlyphHelper

        Public Shared Function GetGlyph(ByVal path As String) As ImageSource
            If path.EndsWith(".svg") Then
                Return TryCast(New DXImageExtension(path).ProvideValue(Nothing), ImageSource)
            Else
                Return New BitmapImage(AssemblyHelper.GetResourceUri(GetType(MVVMRibbon).Assembly, path))
            End If
        End Function
    End Class
End Namespace
