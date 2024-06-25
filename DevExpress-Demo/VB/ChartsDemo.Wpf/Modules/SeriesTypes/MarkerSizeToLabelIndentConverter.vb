Imports System
Imports System.Globalization

Namespace ChartsDemo

    Friend Class MarkerSizeToLabelIndentConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Return(CDbl(value)) / 2
        End Function
    End Class
End Namespace
