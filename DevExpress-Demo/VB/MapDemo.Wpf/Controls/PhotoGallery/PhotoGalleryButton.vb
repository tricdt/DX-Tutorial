Imports System.Windows
Imports System.Windows.Markup

Namespace MapDemo

    <ContentProperty("Content")>
    Public Class PhotoGalleryButton
        Inherits VisibleControl

        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(Object), GetType(PhotoGalleryButton), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly ContentTemplateProperty As DependencyProperty = DependencyProperty.Register("ContentTemplate", GetType(DataTemplate), GetType(PhotoGalleryButton), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Content As Object
            Get
                Return CObj(GetValue(ContentProperty))
            End Get

            Set(ByVal value As Object)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Property ContentTemplate As DataTemplate
            Get
                Return CType(GetValue(ContentTemplateProperty), DataTemplate)
            End Get

            Set(ByVal value As DataTemplate)
                SetValue(ContentTemplateProperty, value)
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(PhotoGalleryButton)
        End Sub
    End Class
End Namespace
