Imports DevExpress.Xpf.Charts
Imports System.ComponentModel.DataAnnotations

Namespace PropertyGridDemo

    <MetadataType(GetType(Metadata.MirrorOptionsMetadata))>
    Public Class MirrorOptions

        Public Sub New()
            MirrorLength = 50
        End Sub

        Public Overridable Property MirrorLength As Double

        Public Overridable Property Show As Boolean

        Public Overridable Property ActualLength As Double

        Protected Sub OnMirrorLengthChanged()
            UpdateActualLength()
        End Sub

        Protected Sub OnShowChanged()
            UpdateActualLength()
        End Sub

        Private Sub UpdateActualLength()
            If Show Then
                ActualLength = MirrorLength
            Else
                ActualLength = 0R
            End If
        End Sub
    End Class

    <MetadataType(GetType(Metadata.LabelOptionsMetadata))>
    Public Class LabelOptions

        Public Overridable Property ShowLabel As Boolean

        Public Overridable Property Position As Bar2DLabelPosition
    End Class

    <MetadataType(GetType(Metadata.VisibleLabelOptionsMetadata))>
    Public Class VisibleLabelOptions
        Inherits LabelOptions

    End Class
End Namespace
