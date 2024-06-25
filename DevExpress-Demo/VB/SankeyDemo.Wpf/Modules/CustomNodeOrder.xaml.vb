Imports DevExpress.Xpf.Charts.Sankey
Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace SankeyDemo

    Public Partial Class CustomNodeOrder
        Inherits SankeyDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class CustomNodeOrderDemoToolTipHeaderConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is SankeyNode Then
                Dim node As SankeyNode = CType(value, SankeyNode)
                Dim prefix As String = ""
                Select Case node.Level
                    Case 0
                        prefix = "Company"
                    Case 1
                        prefix = "Product Category"
                    Case 2
                        prefix = "Ship mode"
                    Case 3
                        prefix = "Customer Country"
                End Select

                Return prefix & ": " & node.Tag.ToString()
            ElseIf TypeOf value Is SankeyLink Then
                Return CType(value, SankeyLink).SourceNode.Tag.ToString() & " → " & CType(value, SankeyLink).TargetNode.Tag.ToString()
            End If

            Return String.Empty
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class CustomNodeOrderDemoToolTipContentConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim text As String = ""
            Dim format As NumberFormatInfo = CultureInfo.InvariantCulture.NumberFormat
            If TypeOf value Is SankeyNode Then
                Dim node As SankeyNode = CType(value, SankeyNode)
                text = "$" & node.TotalWeight.ToString("#,0.00", format)
            ElseIf TypeOf value Is SankeyLink Then
                Dim link As SankeyLink = CType(value, SankeyLink)
                text = "$" & link.TotalWeight.ToString("#,0.00", format)
            End If

            Return text
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class NodeComparerConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim comparerName As String = values(0).ToString()
            Dim comparer As AscendingOrDescendingNodeComparer
            If Equals(comparerName, "Total Weight") Then
                comparer = New TotalWeightComparer()
            ElseIf Equals(comparerName, "Output Link Count") Then
                comparer = New OutputLinkCountComparer()
            Else
                comparer = New NodeNameComparer()
            End If

            comparer.Ascending = False
            If Equals(values(1).ToString(), "Ascending") Then comparer.Ascending = True
            Return comparer
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
