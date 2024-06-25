Imports DevExpress.Mvvm.DataAnnotations

Namespace PropertyGridDemo.Metadata

    Public Module MirrorOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of MirrorOptions))
            builder.Property(Function(x) x.Show).PropertyGridEditor("MirrorOptions.Show")
            builder.Property(Function(x) x.MirrorLength).PropertyGridEditor("MirrorOptions.MirrorLength").LocatedAt(0)
        End Sub
    End Module

    Public Module LabelOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of LabelOptions))
            builder.Property(Function(x) x.ShowLabel).PropertyGridEditor("LabelOptions.ShowLabel").LocatedAt(0)
        End Sub
    End Module

    Public Module VisibleLabelOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of VisibleLabelOptions))
            builder.Property(Function(x) x.Position).PropertyGridEditor("VisibleLabelOptions.Position")
        End Sub
    End Module
End Namespace
