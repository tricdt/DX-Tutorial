Imports DevExpress.Xpf.Charts.Sankey
Imports System
Imports System.Collections.Generic

Namespace SankeyDemo

    Public MustInherit Class AscendingOrDescendingNodeComparer
        Implements IComparer(Of SankeyNode)

        Public Property Ascending As Boolean

        Public MustOverride Function Compare(ByVal x As SankeyNode, ByVal y As SankeyNode) As Integer Implements IComparer(Of SankeyNode).Compare
    End Class

    Public Class TotalWeightComparer
        Inherits AscendingOrDescendingNodeComparer

        Public Overrides Function Compare(ByVal x As SankeyNode, ByVal y As SankeyNode) As Integer
            Return If(Ascending, 1, -1) * Math.Sign(x.TotalWeight - y.TotalWeight)
        End Function

        Public Overrides Function ToString() As String
            Return "Total Weight"
        End Function
    End Class

    Public Class OutputLinkCountComparer
        Inherits AscendingOrDescendingNodeComparer
        Implements IComparer(Of SankeyNode)

        Public Overrides Function Compare(ByVal x As SankeyNode, ByVal y As SankeyNode) As Integer Implements IComparer(Of SankeyNode).Compare
            If x.OutputLinks.Count <> 0 Then
                Return If(Ascending, 1, -1) * Math.Sign(x.OutputLinks.Count - y.OutputLinks.Count)
            Else
                Return If(Ascending, 1, -1) * Math.Sign(x.InputLinks.Count - y.InputLinks.Count)
            End If
        End Function

        Public Overrides Function ToString() As String
            Return "Output Link Count"
        End Function
    End Class

    Public Class NodeNameComparer
        Inherits AscendingOrDescendingNodeComparer
        Implements IComparer(Of SankeyNode)

        Public Overrides Function Compare(ByVal x As SankeyNode, ByVal y As SankeyNode) As Integer Implements IComparer(Of SankeyNode).Compare
            Return If(Ascending, 1, -1) * x.Tag.ToString().CompareTo(y.Tag.ToString())
        End Function

        Public Overrides Function ToString() As String
            Return "Node Name"
        End Function
    End Class
End Namespace
