Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections
Imports System.Linq

Namespace GridDemo

    Public Class ScrollBarAnnotationsModeConverter
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object
            Dim list = TryCast(value, IEnumerable)
            If list Is Nothing Then Return ScrollBarAnnotationMode.None
            Dim result As ScrollBarAnnotationMode = ScrollBarAnnotationMode.None
            For Each item In list.OfType(Of ValueSelectorItem)().[Select](Function(x) CType(x.Content, ScrollBarAnnotationMode))
                result = result Or item
            Next

            Return result
        End Function
    End Class
End Namespace
