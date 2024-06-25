Imports System
Imports System.Windows
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Xml.Linq

Namespace ProductsDemo

    Public Class DemoValuesProvider

        Public Const DevExpressBingKey As String = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWpfProductsDemo
    End Class

    Public Module DataLoader

        Public Function LoadXmlFromResources(ByVal fileName As String) As XDocument
            Try
                fileName = "/DevExpress.ProductsDemo.Wpf;component" & fileName
                Dim uri As Uri = New Uri(fileName, UriKind.RelativeOrAbsolute)
                Dim info As StreamResourceInfo = Application.GetResourceStream(uri)
                Return XDocument.Load(info.Stream)
            Catch
                Return Nothing
            End Try
        End Function

        Public Function LoadXmlFromFile(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(fileName)
            Catch
                Return Nothing
            End Try
        End Function
    End Module

    Public Class PhotoGalleryResources

        Public ReadOnly Property CityInformationControlSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/CityInformationControl.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property LabelControlImageSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/Label.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property PlaceInfoControlPrevImageSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/PrevPlace.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property PlaceInfoControlNextImageSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/NextPlace.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public Sub New()
        End Sub
    End Class
End Namespace
