Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Markup
Imports DevExpress.Diagram.Core
Imports DevExpress.Internal
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Xpf.Ribbon
Imports DiagramDemo
Imports System.Runtime.CompilerServices

Namespace DiagramDemo

    Public Class DiagramDemoModule
        Inherits DevExpress.Xpf.DemoBase.DemoModule

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme As Boolean
            Get
                Return False
            End Get
        End Property

        Protected Overridable Sub LoadDemoData()
        End Sub

        Protected Overrides Function ShowApplicationButton() As Boolean
            Dim designerControl = TryCast(Me.FindName("diagramControl"), DevExpress.Xpf.Diagram.DiagramDesignerControl)
            Dim customRibbon = TryCast(Me.FindName("ribbonControl"), DevExpress.Xpf.Ribbon.RibbonControl)
            Return designerControl IsNot Nothing OrElse (customRibbon IsNot Nothing AndAlso customRibbon.ApplicationMenu IsNot Nothing)
        End Function
    End Class

    Public Module DiagramControlDemoExtensions

        <Extension()>
        Public Sub LoadDemoData(ByVal diagramControl As DevExpress.Xpf.Diagram.DiagramControl, ByVal dataSourceName As String)
            diagramControl.LoadDocument(DiagramDemo.DiagramDemoHelper.GetDataFilePath(dataSourceName))
        End Sub
    End Module

    Public Module DiagramDemoHelper

        Public Function GetDataFilePath(ByVal relativePath As String) As String
            Return DevExpress.Internal.DataDirectoryHelper.GetFile("Diagram\" & relativePath, DevExpress.Internal.DataDirectoryHelper.DataFolderName)
        End Function

        Public Function GetCircleDiagramItemPosition(ByVal radius As Double, ByVal phase As Double, ByVal diagramCenter As System.Windows.Point, ByVal itemSize As System.Windows.Size) As Point
            Dim point = DiagramDemo.DiagramDemoHelper.GetCartesianPointByPolarPoint(radius, phase)
            Dim offsetX As Double = diagramCenter.X - itemSize.Width / 2R
            Dim offsetY As Double = diagramCenter.Y - itemSize.Height / 2R
            point.Offset(offsetX, offsetY)
            Return point
        End Function

        Public Function GetCartesianPointByPolarPoint(ByVal magnitude As Double, ByVal phase As Double) As Point
            Dim x As Double = magnitude * System.Math.Cos(phase)
            Dim y As Double = magnitude * System.Math.Sin(phase)
            Return New System.Windows.Point(x, y)
        End Function

        Public Sub LayoutCircleDiagramItems(ByVal items As DevExpress.Xpf.Diagram.DiagramItem(), ByVal pageSize As System.Windows.Size, ByVal radius As Double)
            Dim phase As Double = -System.Math.PI / 2R
            Dim phaseDelta As Double = 2 * System.Math.PI / items.Length
            Dim centerX As Double = pageSize.Width / 2R
            Dim centerY As Double = pageSize.Height / 2R
            Dim center As System.Windows.Point = New System.Windows.Point(centerX, centerY)
            For Each item In items
                item.Position = DiagramDemo.DiagramDemoHelper.GetCircleDiagramItemPosition(radius, phase, center, item.DesiredSize)
                phase += phaseDelta
            Next
        End Sub
    End Module

    Public Class DiagramContentItemTool
        Inherits System.Windows.Markup.MarkupExtension

        Public Property ToolId As String

        Public Property ToolName As String

        Public Property CustomStyleId As String

        Public Property DefaultSize As Size

        Public Property IsQuick As Boolean

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return New DevExpress.Diagram.Core.FactoryItemTool(Me.ToolId, Function() Me.ToolName, Function(diagram) Me.CreateItem(), Me.DefaultSize, Me.IsQuick)
        End Function

        Protected Overridable Function CreateItem() As DiagramItem
            Return New DevExpress.Xpf.Diagram.DiagramContentItem With {.CustomStyleId = Me.CustomStyleId}
        End Function
    End Class
End Namespace

Namespace DevExpress.Diagram.Demos

    Public Module DiagramDemoFileHelper

        Public Function GetDataStream(ByVal fileName As String) As Stream
            Dim path As String = DiagramDemo.DiagramDemoHelper.GetDataFilePath(fileName)
            Return System.IO.File.OpenRead(path)
        End Function

        Public Function GetResourceStream(ByVal path As String) As Stream
            Dim assembly = GetType(DevExpress.Diagram.Demos.DiagramDemoFileHelper).Assembly
            Return DevExpress.Utils.AssemblyHelper.GetResourceStream(assembly, path, True)
        End Function
    End Module
End Namespace
