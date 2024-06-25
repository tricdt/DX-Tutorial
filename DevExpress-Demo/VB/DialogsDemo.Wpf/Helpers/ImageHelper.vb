Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace CommonDemo.Helpers

    Public Module ImageHelper

        Public Function GetJPGPhotos(ByVal directoryPath As String) As IEnumerable(Of PhotoData)
            Return Directory.GetFiles(directoryPath, "*.jpg").[Select](New Func(Of String, PhotoData)(AddressOf CommonDemo.Helpers.ImageHelper.GetJPGPhoto))
        End Function

        Public Function GetJPGPhoto(ByVal photoPath As String) As PhotoData
            Return New PhotoData(photoPath)
        End Function
    End Module

    Public Class ImagesHelper

        Public Shared Function GetWebIcon(ByVal icon As String) As ImageSource
            Return New BitmapImage(New Uri("/DialogsDemo;component/Images/TabControl/" & icon, UriKind.Relative))
        End Function
    End Class

    Public Class PhotoData

        Private _Photo As BitmapImage, _PhotoName As String

        Public Property Photo As BitmapImage
            Get
                Return _Photo
            End Get

            Private Set(ByVal value As BitmapImage)
                _Photo = value
            End Set
        End Property

        Public Property PhotoName As String
            Get
                Return _PhotoName
            End Get

            Private Set(ByVal value As String)
                _PhotoName = value
            End Set
        End Property

        Public Sub New(ByVal photoPath As String)
            Photo = New BitmapImage(New Uri(photoPath))
            PhotoName = Path.GetFileNameWithoutExtension(photoPath)
        End Sub
    End Class
End Namespace
