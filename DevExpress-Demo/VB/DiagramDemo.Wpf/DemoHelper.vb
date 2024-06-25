Imports System.Collections
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports DevExpress.Diagram.Core
Imports DevExpress.Utils

Namespace DevExpress.Diagram.Demos

    Friend Module DemoHelper

        Public Function CreatePredefinedSvgStencil(ByVal stencilId As String, ByVal stencilName As String, ByVal Optional visible As Boolean = False) As DiagramStencil
            Dim stencil As DiagramStencil = New DiagramStencil(stencilId, stencilName, visible)
            InitializePredefinedStencil(stencil)
            Return stencil
        End Function

        Private Sub InitializePredefinedStencil(ByVal stencil As DiagramStencil)
            Const directoryPath As String = "images/officeplan"
            Dim assembly = GetType(DemoHelper).Assembly
            Dim filePaths = AssemblyHelper.GetResources(assembly).OfType(Of DictionaryEntry)().[Select](Function(x) CStr(x.Key)).Where(Function(x) x.StartsWith(directoryPath)).OrderBy(Function(x) x)
            For Each filePath In filePaths
                Dim fileName As String = Regex.Match(filePath.Replace("%20", " "), "(?<=\/)[A-z0-9 ]*(?=\.svg)").Value
                Dim shapeId As String = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(fileName)
                Dim stream = AssemblyHelper.GetResourceStream(assembly, filePath, True)
                Dim sd = ShapeDescription.CreateSvgShape(shapeId, shapeId, stream, False)
                stencil.RegisterShape(sd)
            Next
        End Sub

        Public Sub InitializeSvgShape(ByVal stencil As DiagramStencil, ByVal shape As IDiagramShape)
            If shape IsNot Nothing AndAlso stencil.ContainsShape(shape.Shape) Then
                shape.CanEdit = False
            End If
        End Sub

        Public Function CreateStencilFromFile(ByVal fileName As String, ByVal stencilId As String, ByVal stencilName As String, ByVal Optional visible As Boolean = False) As DiagramStencil
            Dim customShapesStencil As DiagramStencil = Nothing
            Using stream = File.OpenRead(fileName)
                customShapesStencil = DiagramStencil.Create(stencilId, stencilName, stream, Function(s) s, visible)
            End Using

            Return customShapesStencil
        End Function

        Public Function CreateExtendedStencilCollection(ParamArray exStencils As DiagramStencil()) As DiagramStencilCollection
            Return New DiagramStencilCollection(exStencils.Concat(DiagramToolboxRegistrator.Stencils))
        End Function
    End Module
End Namespace
