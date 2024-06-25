Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Namespace NavigationDemo.Utils

    Public Class FilterBase

        Public Overridable ReadOnly Property Name As String
            Get
                Return String.Empty
            End Get
        End Property

        Public Function ApplyFilter(ByVal uri As String, ByVal Optional size As Windows.Size? = Nothing) As Windows.Media.Imaging.BitmapImage
            Dim image = If(size.HasValue, ApplyMatrix(uri, Matrix, New Size(CInt(size.Value.Width), CInt(size.Value.Height))), ApplyMatrix(uri, Matrix))
            Return CreateWPFImage(image)
        End Function

        Protected Overridable ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix()
            End Get
        End Property

        Protected Function ApplyMatrix(ByVal uri As String, ByVal matrix As ColorMatrix, ByVal Optional size As Size? = Nothing) As Image
            Dim src As Bitmap = If(size IsNot Nothing, New Bitmap(Image.FromFile(uri), size.Value), New Bitmap(uri))
            Dim dest As Bitmap = New Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb)
            Using graphics As Graphics = Graphics.FromImage(dest)
                Dim bmpAttributes As ImageAttributes = New ImageAttributes()
                bmpAttributes.SetColorMatrix(matrix)
                graphics.DrawImage(src, New Rectangle(0, 0, src.Width, src.Height), 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, bmpAttributes)
            End Using

            src.Dispose()
            Return dest
        End Function

        Protected Function CreateWPFImage(ByVal image As Image) As Windows.Media.Imaging.BitmapImage
            Dim bitmapImage = New Windows.Media.Imaging.BitmapImage()
            bitmapImage.BeginInit()
            Dim stream = New MemoryStream()
            image.Save(stream, ImageFormat.Bmp)
            stream.Seek(0, SeekOrigin.Begin)
            bitmapImage.StreamSource = stream
            bitmapImage.EndInit()
            Return bitmapImage
        End Function
    End Class

    Public Class PolaroidFilter
        Inherits FilterBase

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Polaroid"
            End Get
        End Property

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {1.438F, -0.062F, -0.062F, 0, 0}, New Single() {-0.122F, 1.378F, -0.122F, 0, 0}, New Single() {0.016F, -0.016F, 1.438F, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0.03F, 0.05F, -0.2F, 0, 1}})
            End Get
        End Property
    End Class

    Public Class GrayScaleFilter
        Inherits FilterBase

        Public Overrides ReadOnly Property Name As String
            Get
                Return "GrayScale"
            End Get
        End Property

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {.3F, .3F, .3F, 0, 0}, New Single() {.59F, .59F, .59F, 0, 0}, New Single() {.11F, .11F, .11F, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})
            End Get
        End Property
    End Class

    Public Class NegativeFilter
        Inherits FilterBase

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Negative"
            End Get
        End Property

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {-1, 0, 0, 0, 0}, New Single() {0, -1, 0, 0, 0}, New Single() {0, 0, -1, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {1, 1, 1, 0, 1}})
            End Get
        End Property
    End Class

    Public Class SepiaFilter
        Inherits FilterBase

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Sepia"
            End Get
        End Property

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {.393F, .349F, .272F, 0, 0}, New Single() {.769F, .686F, .534F, 0, 0}, New Single() {.189F, .168F, .131F, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})
            End Get
        End Property
    End Class

    Public Class BGRFilter
        Inherits FilterBase

        Public Overrides ReadOnly Property Name As String
            Get
                Return "BGR"
            End Get
        End Property

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {0, 0, 1, 0, 0}, New Single() {0, 1, 0, 0, 0}, New Single() {1, 0, 0, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})
            End Get
        End Property
    End Class

    Public Class GBRFilter
        Inherits FilterBase

        Public Overrides ReadOnly Property Name As String
            Get
                Return "GBR"
            End Get
        End Property

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {0, 1, 0, 0, 0}, New Single() {0, 0, 1, 0, 0}, New Single() {1, 0, 0, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})
            End Get
        End Property
    End Class

    Public Class UniversalFilter
        Inherits FilterBase

        Public Sub New()
            b = 0
            g = b
            r = g
        End Sub

        Private r, g, b As Integer

        Public Overloads Function ApplyFilter(ByVal uri As String, ByVal r As Integer, ByVal g As Integer, ByVal b As Integer) As Windows.Media.Imaging.BitmapImage
            Me.r = r
            Me.g = g
            Me.b = b
            Dim image = ApplyMatrix(uri, Matrix)
            Return CreateWPFImage(image)
        End Function

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Universal"
            End Get
        End Property

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {1 + CSng(r / 255.0F), 0, 0, 0, 0}, New Single() {0, 1 + CSng(g / 255.0F), 0, 0, 0}, New Single() {0, 0, 1 + CSng(b / 255.0F), 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0.1F, 0.1F, 0.1F, 0, 1}})
            End Get
        End Property
    End Class

    Public Class ContrastFilter
        Inherits FilterBase

        Public Sub New()
            translate = 0
            scale = translate
        End Sub

        Private scale As Single

        Private translate As Single

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Contrast"
            End Get
        End Property

        Public Overloads Function ApplyFilter(ByVal uri As String, ByVal val As Integer) As Windows.Media.Imaging.BitmapImage
            scale = val
            translate =(-.5F * scale + .5F) * 255.0F
            Dim image = ApplyMatrix(uri, Matrix)
            Return CreateWPFImage(image)
        End Function

        Protected Overrides ReadOnly Property Matrix As ColorMatrix
            Get
                Return New ColorMatrix(New Single()() {New Single() {1 + scale / 100, 0, 0, 0, translate}, New Single() {0, 1 + scale / 100, 0, 0, translate}, New Single() {0, 0, 1 + scale / 100, 0, translate}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})
            End Get
        End Property
    End Class
End Namespace
