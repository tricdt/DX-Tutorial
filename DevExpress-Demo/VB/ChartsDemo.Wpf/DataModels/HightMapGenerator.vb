Imports System
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Windows.Threading
Imports DevExpress.Utils

Namespace ChartsDemo

    Public Class HeightMapGenerator

        Private _MapValues As DoubleCollection, _MapX As DoubleCollection, _MapY As DoubleCollection

        Public Property MapValues As DoubleCollection
            Get
                Return _MapValues
            End Get

            Private Set(ByVal value As DoubleCollection)
                _MapValues = value
            End Set
        End Property

        Public Property MapX As DoubleCollection
            Get
                Return _MapX
            End Get

            Private Set(ByVal value As DoubleCollection)
                _MapX = value
            End Set
        End Property

        Public Property MapY As DoubleCollection
            Get
                Return _MapY
            End Get

            Private Set(ByVal value As DoubleCollection)
                _MapY = value
            End Set
        End Property

        Public Shared ReadOnly Property HeightMapUri As Uri
            Get
                Return AssemblyHelper.GetResourceUri(GetType(RegularGridDataTab).Assembly, "/Data/Heightmap.jpg")
            End Get
        End Property

        Public Sub New()
            GenerateMap(HeightMapUri)
        End Sub

        Private Sub GenerateMap(ByVal uri As Uri)
            Dim ImageData As ImageData = New ImageData(uri)
            Dim pixels As PixelColor(,) = ImageData.GetPixels()
            Dim countX As Integer = pixels.GetLength(0)
            Dim countY As Integer = pixels.GetLength(1)
            Dim startX As Integer = 0
            Dim startY As Integer = 0
            Dim gridStep As Integer = 100
            Dim minY As Double = -300
            Dim maxY As Double = 3000
            Dim mapX As DoubleCollection = New DoubleCollection(countX)
            Dim mapY As DoubleCollection = New DoubleCollection(countY)
            Dim values As DoubleCollection = New DoubleCollection(countX * countY)
            Dim fullY As Boolean = False
            For i As Integer = 0 To countX - 1
                mapX.Add(startX + i * gridStep)
                For j As Integer = 0 To countY - 1
                    If Not fullY Then mapY.Add(startY + j * gridStep)
                    Dim value As Double = GetMapValue(pixels(i, j), minY, maxY)
                    values.Add(value)
                Next

                fullY = True
            Next

            Me.MapY = mapY
            Me.MapX = mapX
            MapValues = values
        End Sub

        Private Function GetMapValue(ByVal color As PixelColor, ByVal min As Double, ByVal max As Double) As Double
            Dim normalizedValue As Double = color.Gray / 255R
            Return min + normalizedValue * (max - min)
        End Function
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure PixelColor

        Public Blue As Byte

        Public Green As Byte

        Public Red As Byte

        Public Alpha As Byte

        Private ReadOnly Property BlueInt As Integer
            Get
                Return Blue
            End Get
        End Property

        Private ReadOnly Property GreenInt As Integer
            Get
                Return Green
            End Get
        End Property

        Private ReadOnly Property RedInt As Integer
            Get
                Return Red
            End Get
        End Property

        Public ReadOnly Property Gray As Byte
            Get
                Return CByte((BlueInt + GreenInt + RedInt) \ 3)
            End Get
        End Property
    End Structure

    Public Class ImageData

        Private ReadOnly streamResourceInfo As StreamResourceInfo

        Public Sub New(ByVal uri As Uri)
            streamResourceInfo = Application.GetResourceStream(uri)
        End Sub

        Private Function GetPixels(ByVal source As BitmapSource) As PixelColor(,)
            If source.Format <> PixelFormats.Bgra32 Then source = New FormatConvertedBitmap(source, PixelFormats.Bgra32, Nothing, 0)
            Dim result As PixelColor(,) = New PixelColor(source.PixelWidth - 1, source.PixelHeight - 1) {}
            Dim stride As Integer = source.PixelWidth * (source.Format.BitsPerPixel \ 8)
            CopyPixels(source, result, stride, 0)
            Return result
        End Function

        Private Sub CopyPixels(ByVal source As BitmapSource, ByVal pixels As PixelColor(,), ByVal stride As Integer, ByVal offset As Integer)
            Dim height = source.PixelHeight
            Dim width = source.PixelWidth
            Dim pixelBytes = New Byte(height * width * 4 - 1) {}
            source.CopyPixels(pixelBytes, stride, 0)
            Dim y0 As Integer = offset \ width
            Dim x0 As Integer = offset - width * y0
            For y As Integer = 0 To height - 1
                For x As Integer = 0 To width - 1
                    pixels(x + x0, y + y0) = New PixelColor With {.Blue = pixelBytes((y * width + x) * 4 + 0), .Green = pixelBytes((y * width + x) * 4 + 1), .Red = pixelBytes((y * width + x) * 4 + 2), .Alpha = pixelBytes((y * width + x) * 4 + 3)}
                Next
            Next
        End Sub

        Private Sub DoEvents()
            Call Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, New Action(Sub()
            End Sub))
        End Sub

        Public Function GetPixels() As PixelColor(,)
            Dim pixels As PixelColor(,) = New PixelColor(-1, -1) {}
            Try
                Dim source As BitmapImage = New BitmapImage()
                source.BeginInit()
                source.StreamSource = streamResourceInfo.Stream
                source.EndInit()
                While source.IsDownloading
                    DoEvents()
                End While

                pixels = GetPixels(source)
            Catch
            End Try

            Return pixels
        End Function
    End Class
End Namespace
