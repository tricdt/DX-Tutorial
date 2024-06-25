Imports System
Imports System.Globalization
Imports System.IO
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Data

Namespace GridDemo

    Public Class OrderNameConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            If values.Length <> 2 Then Return Nothing
            If Not(TypeOf values(1) Is ColumnSortOrder) Then Return Nothing
            Dim order As ColumnSortOrder = CType(values(1), ColumnSortOrder)
            If Equals(values(0).ToString(), "Received") Then
                Select Case order
                    Case ColumnSortOrder.Ascending
                        Return "Oldest"
                    Case ColumnSortOrder.Descending
                        Return "Newest"
                    Case Else
                        Return Nothing
                End Select
            End If

            Select Case order
                Case ColumnSortOrder.Ascending
                    Return "Ascending"
                Case ColumnSortOrder.Descending
                    Return "Descending"
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ByteImageConverter
        Implements IValueConverter

        Public Property DecodePixelHeight As Integer

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim imageData As Byte() = TryCast(value, Byte())
            If imageData Is Nothing Then Return Nothing
            Dim biImg As BitmapImage = New BitmapImage()
            RenderOptions.SetBitmapScalingMode(biImg, BitmapScalingMode.HighQuality)
            Dim ms As MemoryStream = New MemoryStream(imageData)
            biImg.BeginInit()
            biImg.DecodePixelHeight = DecodePixelHeight
            biImg.StreamSource = ms
            biImg.EndInit()
            Dim imgSrc As ImageSource = TryCast(biImg, ImageSource)
            Return imgSrc
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class NameToColorConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim name As String = TryCast(value, String)
            If String.IsNullOrEmpty(name) Then Return Colors.Black
            Dim color As Byte = CByte((Math.Abs(name.GetHashCode()) Mod 5))
            Select Case color
                Case 0
                    Return CType(ColorConverter.ConvertFromString("#FF31669C"), Color)
                Case 1
                    Return CType(ColorConverter.ConvertFromString("#FF598A7A"), Color)
                Case 2
                    Return CType(ColorConverter.ConvertFromString("#FFCCA464"), Color)
                Case 3
                    Return CType(ColorConverter.ConvertFromString("#FFE06B4C"), Color)
                Case 4
                    Return CType(ColorConverter.ConvertFromString("#FF558AC0"), Color)
                Case Else
                    Return Colors.Black
            End Select
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class PriorityGroupValueConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Integer Then
                Dim priority As Integer = CInt(value)
                Select Case priority
                    Case 0
                        Return "Low priority"
                    Case 1
                        Return "Normal priority"
                    Case 2
                        Return "High priority"
                    Case Else
                        Return Nothing
                End Select
            End If

            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class AttachmentGroupValueConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean Then
                Dim attachment As Boolean = CBool(value)
                Return If(attachment, "Has Attachment", "Hasn't Attachment")
            End If

            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
