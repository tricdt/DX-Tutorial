Imports DevExpress.Xpf.Charts.Sankey
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media

Namespace SankeyDemo

    Public Partial Class Colorizer
        Inherits SankeyDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class ColorizerDemoToolTipConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim text As String = ""
            If TypeOf value Is SankeyNode Then
                Dim node As SankeyNode = CType(value, SankeyNode)
                If Math.Round(node.SourceWeight, 3) <> 0 Then text += String.Format("Total import: ${0} billion", Math.Round(node.SourceWeight * 1000, 2))
                If Math.Round(node.TargetWeight, 3) <> 0 Then
                    If Not String.IsNullOrEmpty(text) Then text += Environment.NewLine
                    text += String.Format("Total export: ${0} billion", Math.Round(node.TargetWeight * 1000, 2))
                End If
            ElseIf TypeOf value Is SankeyLink Then
                Dim link As SankeyLink = CType(value, SankeyLink)
                text = String.Format("${0} billion", Math.Round(link.TotalWeight * 1000, 2))
            End If

            Return text
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ColorizerItemToColorizerConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal value As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim colorizerName As String = value(0).ToString()
            Dim colorizer As SankeyColorizerBase
            If Equals(colorizerName, "Continent Colorizer (Custom)") Then
                colorizer = New ContinentColorizer() With {.ContinentCountriesPairs = GetContinentCountriesPairs_ColorizerDemo(), .ContinentColorPairs = GetContinentColorPairs_ColorizerDemo()}
            ElseIf Equals(colorizerName, "Gradient Colorizer (Custom)") Then
                colorizer = GradientColorizer.Create(CType(value(1), List(Of Export)))
            ElseIf Equals(colorizerName, "Palette Colorizer") Then
                colorizer = New SankeyPaletteColorizer()
            Else
                colorizer = New SankeyPaletteColorizer() With {.LinkBrush = New SolidColorBrush(Color.FromRgb(134, 134, 134))}
            End If

            Return colorizer
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class NodeAlignmentConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim nodeAlignmentString As String = value.ToString()
            If Equals(nodeAlignmentString, "Far") Then
                Return SankeyNodeAlignment.Far
            ElseIf Equals(nodeAlignmentString, "Near") Then
                Return SankeyNodeAlignment.Near
            Else
                Return SankeyNodeAlignment.Center
            End If
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
