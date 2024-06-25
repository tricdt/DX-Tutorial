Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Globalization
Imports System.Windows.Markup
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Diagram.Core

Namespace DevExpress.Diagram.Demos

    Friend Module MindMapHelpers

        Public Function GetMindMapStyle(ByVal item As DiagramItem, ByVal parent As DiagramItem) As DiagramItemStyleId
            If parent.IncomingConnectors.Count() = 0 Then Return OrgChartHelpers.GetStyle(Enumerable.Count(parent.OutgoingConnectors) + 1).GetPaleStyle()
            Return parent.ThemeStyleId.GetBrightStyle()
        End Function

        Public Function GetSize(ByVal itemLevel As Integer) As Size
            Select Case itemLevel
                Case 0
                    Return New Size(310, 250)
                Case 1
                    Return New Size(210, 140)
                Case 2
                    Return New Size(190, 125)
            End Select

            Return New Size(165, 110)
        End Function

        Public Function GetFontSize(ByVal itemLevel As Integer) As Single
            Select Case itemLevel
                Case 0
                    Return 32
                Case 1
                    Return 24
                Case 2
                    Return 18
            End Select

            Return 13.5F
        End Function
    End Module

    Public Class SelectedItemsConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value IsNot Nothing AndAlso (TypeOf value Is DiagramShape)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
