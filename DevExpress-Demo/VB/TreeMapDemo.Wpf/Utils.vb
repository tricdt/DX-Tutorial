Imports System
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Xml
Imports System.Xml.Linq

Namespace TreeMapDemo

    Public Module DataLoader

        Private Function GetStream(ByVal fileName As String) As Stream
            Dim uri As Uri = GetResourceUri(fileName)
            Return Application.GetResourceStream(uri).Stream
        End Function

        Public Function GetResourceUri(ByVal fileName As String) As Uri
            fileName = "/TreeMapDemo;component" & fileName
            Return New Uri(fileName, UriKind.RelativeOrAbsolute)
        End Function

        Public Function LoadXDocumentFromResources(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(GetStream(fileName))
            Catch
                Return Nothing
            End Try
        End Function

        Public Function LoadXmlDocumentFromResources(ByVal fileName As String) As XmlDocument
            Dim document As XmlDocument = New XmlDocument()
            Try
                document.Load(GetStream(fileName))
                Return document
            Catch
                Return Nothing
            End Try
        End Function

        Public Function CreateDataSet(ByVal fileName As String) As DataTable
            If Not String.IsNullOrWhiteSpace(fileName) Then
                Dim dataSet As DataSet = New DataSet()
                dataSet.ReadXml(GetStream(fileName))
                If dataSet.Tables.Count > 0 Then Return dataSet.Tables(0)
            End If

            Return Nothing
        End Function
    End Module

    Public Class LeafVerticalStackPanel
        Inherits Panel

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            For Each child As UIElement In Children
                child.Measure(availableSize)
            Next

            Return availableSize
        End Function

        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            Dim freeRect As Rect = New Rect(New Point(0, 0), finalSize)
            For Each child As UIElement In Children
                Dim element As FrameworkElement = TryCast(child, FrameworkElement)
                If element IsNot Nothing Then
                    Dim size As Size = element.DesiredSize
                    Dim horzPosition As Double = If(element.HorizontalAlignment = HorizontalAlignment.Right, freeRect.Right - size.Width, freeRect.Left)
                    Dim vertPosition As Double = If(element.VerticalAlignment = VerticalAlignment.Bottom, freeRect.Bottom - size.Height, freeRect.Top)
                    Dim locationRect As Rect = New Rect(New Point(horzPosition, vertPosition), size)
                    If freeRect.Contains(locationRect) AndAlso freeRect.Height > locationRect.Height AndAlso freeRect.Width >= (If(TypeOf element Is StackPanel, locationRect.Width, locationRect.Width + element.Margin.Left)) Then
                        element.Visibility = Visibility.Visible
                        element.Arrange(locationRect)
                        If element.VerticalAlignment = VerticalAlignment.Bottom Then
                            freeRect = New Rect(New Point(freeRect.Left, freeRect.Top), New Size(freeRect.Width, freeRect.Height - size.Height))
                        Else
                            freeRect = New Rect(New Point(freeRect.Left, freeRect.Top + size.Height), New Point(freeRect.Right, freeRect.Bottom))
                        End If
                    Else
                        element.Visibility = Visibility.Hidden
                        Exit For
                    End If
                End If
            Next

            Return finalSize
        End Function
    End Class

    Public Class StringToImagePathConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim stringValue As String = TryCast(value, String)
            If Not Equals(stringValue, Nothing) Then Return GetResourceUri("/Images/" & stringValue & ".png")
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class CountToTextConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If System.Convert.ToInt32(value) = 1 Then Return value.ToString() & " medal"
            Return value.ToString() & " medals"
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class EnergyTypeToBrushConverter
        Implements IValueConverter

        Private Function GetColor(ByVal typeName As String) As Color
            Select Case typeName
                Case "Nuclear"
                    Return Color.FromRgb(&H9D, &H90, &HA0)
                Case "Oil"
                    Return Color.FromRgb(&H2A, &H5F, &H8E)
                Case "Natural Gas"
                    Return Color.FromRgb(&H62, &H9D, &HD1)
                Case "Hydro Electric"
                    Return Color.FromRgb(&H29, &H7F, &HD5)
                Case "Coal"
                    Return Color.FromRgb(&H4A, &H66, &HAC)
                Case Else
                    Return Colors.Transparent
            End Select
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String Then Return New SolidColorBrush(GetColor(CStr(value)))
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class WidthToVisibilityConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Const imageWidth As Integer = 24

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(System.Convert.ToInt32(value) >= imageWidth, Visibility.Visible, Visibility.Collapsed)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class HeatmapDataSourceGenerator

        Const offset As Integer = 15

        Const speedRange As Integer = 7

        Const velocity As Integer = 10

        Public Const SizeX As Integer = 600, SizeY As Integer = 300

        Private random As Random = New Random(100)

        Private speed As Integer(,)

        Private values As Integer(,)

        Private min As Integer = 0

        Private delta As Integer = 0

        Private ReadOnly Property LengthX As Integer
            Get
                Return SizeY + offset
            End Get
        End Property

        Private ReadOnly Property LengthY As Integer
            Get
                Return SizeX + offset
            End Get
        End Property

        Public Sub New()
            InitializeMatrix()
        End Sub

        Private Sub InitializeMatrix()
            speed = New Integer(314, 614) {}
            values = New Integer(314, 614) {}
            values(0, 0) = 0
            speed(0, 0) = 0
            Dim minLength As Integer = Math.Min(LengthX, LengthY)
            For i As Integer = 1 To minLength - 1
                values(i, 0) = values(i - 1, 0) + speed(i - 1, 0)
                values(0, i) = values(0, i - 1) + speed(0, i - 1)
                speed(i, 0) = speed(i - 1, 0) + random.Next(0 - speedRange, 1 + speedRange)
                speed(0, i) = speed(0, i - 1) + random.Next(0 - speedRange, 1 + speedRange)
            Next

            For i As Integer = minLength To LengthX - 1
                values(i, 0) = values(i - 1, 0) + speed(i - 1, 0)
                speed(i, 0) = speed(i - 1, 0) + random.Next(0 - speedRange, 1 + speedRange)
            Next

            For i As Integer = minLength To LengthY - 1
                values(0, i) = values(0, i - 1) + speed(0, i - 1)
                speed(0, i) = speed(0, i - 1) + random.Next(0 - speedRange, 1 + speedRange)
            Next

            For i As Integer = 1 To LengthX - 1
                For j As Integer = 1 To LengthY - 1
                    values(i, j) =(values(i - 1, j - 1) + speed(i - 1, j - 1) + speed(i - 1, j) + speed(i, j - 1)) \ 2
                    speed(i, j) =(speed(i - 1, j) + speed(i, j - 1)) \ 2 + random.Next(0 - speedRange, 1 + speedRange)
                Next
            Next

            For i As Integer = 0 To LengthX - 1
                For j As Integer = 0 To LengthY - 1
                    If values(i, j) > delta Then delta = values(i, j)
                    If values(i, j) < min Then min = values(i, j)
                Next
            Next

            delta -= min
        End Sub

        Private Sub UpdateMatrix()
            For i As Integer = 0 To LengthX - 1
                For j As Integer = 0 To LengthY - velocity - 1
                    values(i, j) = values(i, j + velocity)
                    speed(i, j) = speed(i, j + velocity)
                Next
            Next

            For i As Integer = LengthY - velocity To LengthY - 1
                values(0, i) = values(0, i - 1) + speed(0, i - 1)
                speed(0, i) = speed(0, i - 1) + random.Next(0 - speedRange, 1 + speedRange)
                For j As Integer = 1 To LengthX - 1
                    values(j, i) =(values(j - 1, i - 1) + speed(j - 1, i - 1) + speed(j, i - 1) + speed(j - 1, i)) \ 2
                    speed(j, i) =(speed(j, i - 1) + speed(j - 1, i)) \ 2 + random.Next(0 - speedRange, 1 + speedRange)
                Next
            Next
        End Sub

        Public Shared Function GetArray(ByVal size As Integer) As Integer()
            Return New Integer(size - 1) {}.[Select](Function(v, i) i).ToArray()
        End Function

        Public Function GetMatrix() As Double(,)
            UpdateMatrix()
            Dim matrix As Double(,) = New Double(299, 599) {}
            For i As Integer = 0 To SizeY - 1
                For j As Integer = 0 To SizeX - 1
                    matrix(i, j) =(values(i + offset, j + offset) - min) * 16777216.0 / delta - 8388608
                Next
            Next

            Return matrix
        End Function
    End Class
End Namespace
