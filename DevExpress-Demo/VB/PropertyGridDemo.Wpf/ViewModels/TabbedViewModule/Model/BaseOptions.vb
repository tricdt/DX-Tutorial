Imports DevExpress.Mvvm
Imports System.ComponentModel.DataAnnotations

Namespace PropertyGridDemo

    <MetadataType(GetType(Metadata.BaseOptionsMetadata))>
    Public MustInherit Class BaseOptions
        Implements ISupportParentViewModel

        Public Sub New()
        End Sub

        Public Sub New(ByVal root As SeriesOptions)
            Me.root = root
        End Sub

        Private root As SeriesOptions

        Private Property ParentViewModel As Object Implements ISupportParentViewModel.ParentViewModel
            Get
                Return root
            End Get

            Set(ByVal value As Object)
                root = CType(value, SeriesOptions)
            End Set
        End Property
    End Class
End Namespace
