Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace CommonDemo.Helpers

    Public Module ImagesHelper

        Private _LasVegasPhotos As IEnumerable(Of System.Windows.Media.ImageSource), _NaturePhotos As IEnumerable(Of System.Windows.Media.ImageSource), _GroupIcon As ImageSource

        Public Property LasVegasPhotos As IEnumerable(Of ImageSource)
            Get
                Return _LasVegasPhotos
            End Get

            Private Set(ByVal value As IEnumerable(Of ImageSource))
                _LasVegasPhotos = value
            End Set
        End Property

        Public ReadOnly Property LasVegasPhoto1 As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(0)
            End Get
        End Property

        Public ReadOnly Property LasVegasPhoto2 As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(1)
            End Get
        End Property

        Public ReadOnly Property LasVegasPhoto3 As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(2)
            End Get
        End Property

        Public ReadOnly Property LasVegasPhoto4 As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(3)
            End Get
        End Property

        Public Function GetRandomLasVegasPhoto() As ImageSource
            Return LasVegasPhotos.ElementAt(rnd.Next(LasVegasPhotos.Count() - 1))
        End Function

        Public Property NaturePhotos As IEnumerable(Of ImageSource)
            Get
                Return _NaturePhotos
            End Get

            Private Set(ByVal value As IEnumerable(Of ImageSource))
                _NaturePhotos = value
            End Set
        End Property

        Public ReadOnly Property NaturePhoto1 As ImageSource
            Get
                Return NaturePhotos.ElementAt(0)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto2 As ImageSource
            Get
                Return NaturePhotos.ElementAt(1)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto3 As ImageSource
            Get
                Return NaturePhotos.ElementAt(2)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto4 As ImageSource
            Get
                Return NaturePhotos.ElementAt(3)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto5 As ImageSource
            Get
                Return NaturePhotos.ElementAt(4)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto6 As ImageSource
            Get
                Return NaturePhotos.ElementAt(5)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto7 As ImageSource
            Get
                Return NaturePhotos.ElementAt(6)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto8 As ImageSource
            Get
                Return NaturePhotos.ElementAt(7)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto9 As ImageSource
            Get
                Return NaturePhotos.ElementAt(8)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto10 As ImageSource
            Get
                Return NaturePhotos.ElementAt(9)
            End Get
        End Property

        Public Function GetRandomNaturePhoto() As ImageSource
            Return NaturePhotos.ElementAt(rnd.Next(NaturePhotos.Count() - 1))
        End Function

        Public Property GroupIcon As ImageSource
            Get
                Return _GroupIcon
            End Get

            Private Set(ByVal value As ImageSource)
                _GroupIcon = value
            End Set
        End Property

        Public Function GetWebIcon(ByVal icon As String) As ImageSource
            Return New BitmapImage(New Uri("/ControlsDemo;component/Images/TabControl/" & icon, UriKind.Relative))
        End Function

        Public Function GetSvgImage(ByVal imagePath As String, ByVal imageSize As Size) As ImageSource
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(imagePath), .Size = imageSize}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function

        Sub New()
            Dim lasVegasUriPath As String = "/ControlsDemo;component/Images/Photos/Las Vegas/"
            Dim getLasVegasImage As Func(Of String, ImageSource) = Function(x) New BitmapImage(New Uri(lasVegasUriPath & x, UriKind.Relative))
            Dim lasVegasPhotos = New List(Of ImageSource)()
            lasVegasPhotos.Add(getLasVegasImage("DSC_4405.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("DSC_4498.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("DSC_4647.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("DSC_4721.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("IMG_1216.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("IMG_1280.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("IMG_1285.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("IMG_1321.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("IMG_1327.jpg"))
            lasVegasPhotos.Add(getLasVegasImage("IMG_1345.jpg"))
            ImagesHelper.LasVegasPhotos = lasVegasPhotos
            Dim natureUriPath As String = "/ControlsDemo;component/Images/Photos/Nature/"
            Dim getNatureImage As Func(Of String, ImageSource) = Function(x) New BitmapImage(New Uri(natureUriPath & x, UriKind.Relative))
            Dim naturePhotos = New List(Of ImageSource)()
            naturePhotos.Add(getNatureImage("01.JPG"))
            naturePhotos.Add(getNatureImage("02.JPG"))
            naturePhotos.Add(getNatureImage("03.JPG"))
            naturePhotos.Add(getNatureImage("04.JPG"))
            naturePhotos.Add(getNatureImage("05.JPG"))
            naturePhotos.Add(getNatureImage("06.JPG"))
            naturePhotos.Add(getNatureImage("07.JPG"))
            naturePhotos.Add(getNatureImage("08.JPG"))
            naturePhotos.Add(getNatureImage("09.JPG"))
            naturePhotos.Add(getNatureImage("10.JPG"))
            ImagesHelper.NaturePhotos = naturePhotos
            GroupIcon = GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TabControl/GroupIcon.svg", New Size(24, 24))
        End Sub

        Private ReadOnly rnd As Random = New Random()
    End Module
End Namespace
