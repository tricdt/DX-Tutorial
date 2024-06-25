Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel

Namespace PropertyGridDemo.Metadata

    Public Module SeriesOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of SeriesOptions))
            builder.Property(Function(x) x.FillType).PropertyGridEditor("FillType")
            builder.Property(Function(x) x.BorderType).PropertyGridEditor("BorderType")
            builder.Property(Function(x) x.ShowLabel).PropertyGridEditor("ShowLabel")
            builder.Property(Function(x) x.Fill).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
            builder.Property(Function(x) x.Border).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
            builder.Property(Function(x) x.Mirror).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
            builder.Property(Function(x) x.Label).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor")
        End Sub
    End Module

    Public Module CommonSeriesOptionsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of CommonSeriesOptions))
            builder.PropertyGridEditor("Options")
            builder.Property(Function(x) x.Model).PropertyGridEditor("Options.Model")
        End Sub
    End Module
End Namespace
