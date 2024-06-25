Imports System
Imports System.Globalization
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Friend Class Bar2DKindToTickmarksLengthConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Dim bar2DKind As Bar2DKind = TryCast(value, Bar2DKind)
            If bar2DKind IsNot Nothing Then
                Select Case bar2DKind.Name
                    Case "Glass Cylinder"
                        Return 18
                    Case "Quasi-3D Bar"
                        Return 9
                End Select
            End If

            Return 5
        End Function
    End Class
End Namespace
