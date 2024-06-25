Imports DevExpress.Mvvm.DataAnnotations

Namespace PropertyGridDemo.Metadata

    Public Module BorderOptionsDataMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of BorderOptionsData))
        End Sub
    End Module

    Public Module BorderOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of BorderOptions))
            builder.Property(Function(x) x.Data).Hidden()
            builder.Property(Function(x) x.BorderType).PropertyGridEditor("BorderOptions.BorderType").LocatedAt(0)
        End Sub
    End Module

    Public Module SolidBorderOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of SolidBorderOptions))
            builder.Property(Function(x) x.Color).PropertyGridEditor("CommonBorderAndFillOptions.Color")
            builder.Property(Function(x) x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity")
            builder.Property(Function(x) x.Thickness).PropertyGridEditor("SolidBorderOptions.Thickness")
            builder.Property(Function(x) x.DashStyle).PropertyGridEditor("SolidBorderOptions.DashStyle")
        End Sub
    End Module
End Namespace
