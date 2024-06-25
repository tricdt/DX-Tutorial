Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Windows.Data

Namespace EditorsDemo

    Public Class GroupNameToImageConverter
        Implements IValueConverter

        Private Shared _Instance As GroupNameToImageConverter

        Private Shared ReadOnly images As List(Of String) = New List(Of String) From {"administration", "inventory", "manufacturing", "quality", "research", "sales"}

        Shared Sub New()
            Instance = New GroupNameToImageConverter()
        End Sub

        Public Shared Property Instance As GroupNameToImageConverter
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As GroupNameToImageConverter)
                _Instance = value
            End Set
        End Property

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim group = If(value IsNot Nothing, value.ToString().ToLower(), String.Empty)
            Dim imageName = images.FirstOrDefault(Function(x) group.Contains(x))
            Return If(Not String.IsNullOrEmpty(imageName), New Uri(String.Format("pack://application:,,,/EditorsDemo;component/Images/Groups/{0}.svg", imageName)), Nothing)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ProductItemToImageConverter
        Implements IValueConverter

        Private Shared _Instance As ProductItemToImageConverter

        Shared Sub New()
            Instance = New ProductItemToImageConverter()
        End Sub

        Public Shared Property Instance As ProductItemToImageConverter
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As ProductItemToImageConverter)
                _Instance = value
            End Set
        End Property

        Private ReadOnly regex As Regex = New Regex("[^A-Z]+", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim category = If(value IsNot Nothing, value.ToString(), String.Empty)
            Return If(Not String.IsNullOrEmpty(category), New Uri(String.Format("pack://application:,,,/EditorsDemo;component/Images/Products/{0}.svg", Clear(category))), Nothing)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Private Function Clear(ByVal category As String) As String
            Return regex.Replace(category, "")
        End Function
    End Class
End Namespace
