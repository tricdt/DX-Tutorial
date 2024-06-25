Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel

Namespace PropertyGridDemo.Metadata

    Public Module FillOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of FillOptions))
            builder.Property(Function(x) x.Result).Hidden()
            builder.Property(Function(x) x.FillType).PropertyGridEditor("FillOptions.FillType").LocatedAt(0)
        End Sub
    End Module

    Public Module SolidFillOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of SolidFillOptions))
            builder.Property(Function(x) x.Color).PropertyGridEditor("CommonBorderAndFillOptions.Color")
            builder.Property(Function(x) x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity")
        End Sub
    End Module

    Public Module PictureFillOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of PictureFillOptions))
            builder.Property(Function(x) x.Picture).PropertyGridEditor("PictureFillOptions.Picture")
            builder.Property(Function(x) x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity")
        End Sub
    End Module
End Namespace
