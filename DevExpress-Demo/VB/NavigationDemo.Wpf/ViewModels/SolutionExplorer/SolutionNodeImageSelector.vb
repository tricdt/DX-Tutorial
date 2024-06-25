Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid

Namespace NavigationDemo

    Public Class SolutionNodeImageSelector
        Inherits TreeListNodeImageSelector

        Shared Sub New()
            ImageCache = New Dictionary(Of TypeNode, ImageSource)()
        End Sub

        Private Shared ReadOnly ImageCache As Dictionary(Of TypeNode, ImageSource)

        Public Overrides Function [Select](ByVal rowData As TreeList.TreeListRowData) As ImageSource
            Dim solutionNode As SolutionNode = TryCast(rowData.Row, SolutionNode)
            If solutionNode Is Nothing Then Return Nothing
            Return GetImageByTypeNode(solutionNode.TypeNode)
        End Function

        Public Shared Function GetImageByTypeNode(ByVal typeNode As TypeNode) As ImageSource
            If ImageCache.ContainsKey(typeNode) Then Return ImageCache(typeNode)
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(GetImagePathByTypeNode(typeNode)), .Size = New Size(16, 16)}
            Dim image = CType(extension.ProvideValue(Nothing), ImageSource)
            ImageCache.Add(typeNode, image)
            Return image
        End Function

        Public Shared Function GetImagePathByTypeNode(ByVal typeNode As TypeNode) As String
            Return "pack://application:,,,/NavigationDemo;component/Images/SolutionExplorer/" & typeNode.ToString() & ".svg"
        End Function
    End Class
End Namespace
