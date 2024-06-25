Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace ProductsDemo

    Public Class ViewTypeToBoolConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Boolean) AndAlso TypeOf value Is PhotoGalleryViewType AndAlso TypeOf parameter Is PhotoGalleryViewType Then Return CType(value, PhotoGalleryViewType) = CType(parameter, PhotoGalleryViewType)
            Return False
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Boolean Then
                If targetType Is GetType(PhotoGalleryViewType) Then
                    Return If(CBool(value), PhotoGalleryViewType.Gallery, PhotoGalleryViewType.Map)
                End If
            End If

            Return Nothing
        End Function
    End Class

    Public Class ViewTypeToVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) AndAlso TypeOf value Is PhotoGalleryViewType AndAlso TypeOf parameter Is PhotoGalleryViewType Then Return If(CType(value, PhotoGalleryViewType) = CType(parameter, PhotoGalleryViewType), Visibility.Visible, Visibility.Hidden)
            Return Visibility.Hidden
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                If targetType Is GetType(PhotoGalleryViewType) Then
                    Return If(CType(value, Visibility) = Visibility.Visible, PhotoGalleryViewType.Gallery, PhotoGalleryViewType.Map)
                End If
            End If

            Return Nothing
        End Function
    End Class

    Public Enum PhotoGalleryViewType
        Map
        Gallery
        Detail
    End Enum
End Namespace
