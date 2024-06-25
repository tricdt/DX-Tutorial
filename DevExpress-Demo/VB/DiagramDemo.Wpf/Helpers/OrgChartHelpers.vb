Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Linq
Imports System.Runtime.CompilerServices

Namespace DevExpress.Diagram.Demos

    Public Module OrgChartHelpers

        Private ReadOnly styles As DiagramItemStyleId() = DiagramShapeStyleId.Styles.ToArray()

        Public Function GetStyleID(ByVal item As DiagramItem) As DiagramItemStyleId
            Return GetStyle(GetItemLevel(item))
        End Function

        Public Function GetStyle(ByVal id As Integer) As DiagramItemStyleId
            Select Case id Mod 7
                Case 0
                    Return DiagramShapeStyleId.Balanced1
                Case 1
                    Return DiagramShapeStyleId.Balanced2
                Case 2
                    Return DiagramShapeStyleId.Balanced3
                Case 3
                    Return DiagramShapeStyleId.Balanced4
                Case 4
                    Return DiagramShapeStyleId.Balanced5
                Case 5
                    Return DiagramShapeStyleId.Balanced6
                Case 6
                    Return DiagramShapeStyleId.Balanced7
            End Select

            Return DiagramShapeStyleId.Variant1
        End Function

        <Extension()>
        Public Function GetBrightStyle(ByVal styleId As DiagramItemStyleId) As DiagramItemStyleId
            Dim index = Array.IndexOf(styles, styleId)
            If index = -1 Then Return DiagramShapeStyleId.Variant1
            Return styles(If(index > 10, index - 7, index))
        End Function

        <Extension()>
        Public Function GetPaleStyle(ByVal styleId As DiagramItemStyleId) As DiagramItemStyleId
            Dim index = Array.IndexOf(styles, styleId)
            If index = -1 Then Return DiagramShapeStyleId.Variant1
            Return styles(If(index < 39, index + 7, index))
        End Function

        Public Function GetItemLevel(ByVal item As DiagramItem) As Integer
            Dim level = 0
            Dim parent = GetParent(item)
            While parent IsNot Nothing
                level += 1
                parent = GetParent(parent)
            End While

            Return level
        End Function

        Private Function GetParent(ByVal item As DiagramItem) As DiagramItem
            Dim incomingConnector = item.IncomingConnectors.FirstOrDefault()
            Return If(incomingConnector Is Nothing, Nothing, CType(incomingConnector.BeginItem, DiagramItem))
        End Function
    End Module
End Namespace
