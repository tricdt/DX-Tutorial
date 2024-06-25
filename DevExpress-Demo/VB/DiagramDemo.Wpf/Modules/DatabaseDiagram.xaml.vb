Imports DevExpress.Data.Filtering
Imports DevExpress.Diagram.Demos
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Xpf.Diagram.Themes
Imports System.Windows
Imports System.Windows.Controls
Imports ColumnDefinition = DevExpress.Diagram.Demos.ColumnDefinition

Namespace DiagramDemo

    <CodeFile("ViewModels/DatabaseDiagramViewModel.(cs)")>
    <CodeFile("Data/DatabaseDiagramData.(cs)")>
    Public Partial Class DatabaseDiagram
        Inherits DiagramDemoModule

        Private evaluationOperator As TableRelationEvaluationOperator

        Public Sub New()
            evaluationOperator = New TableRelationEvaluationOperator()
            CriteriaOperator.RegisterCustomFunction(evaluationOperator)
            InitializeComponent()
        End Sub

        Protected Overrides Sub Finalize()
            CriteriaOperator.UnregisterCustomFunction(evaluationOperator)
        End Sub

        Private Sub OnItemsGenerated(ByVal sender As Object, ByVal e As DiagramItemsGeneratedEventArgs)
            diagramControl.FitToDrawing()
            diagramControl.ZoomFactor = 1R
        End Sub

        Private Sub OnPanelLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim offset = GetPanelBottomRightOffset()
            Dim location = diagramPanel.GetBounds(diagramPanel).BottomRight
            location.Offset(-panAndZoomGroup.FloatSize.Width - offset, -panAndZoomGroup.FloatSize.Height - offset)
            panAndZoomGroup.FloatLocation = location
        End Sub

        Private Function GetPanelBottomRightOffset() As Double
            Dim key = New DiagramDesignerControlThemeKeysExtension() With {.ResourceKey = DiagramDesignerControlThemeKeys.PanZoomBottomRightOffset}
            Return CDbl(diagramControl.FindResource(key))
        End Function
    End Class

    Public Class DatabaseDiagramItemTemplateSelector
        Inherits DataTemplateSelector

        Public Property TableTemplate As DataTemplate

        Public Property ColumnTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If TypeOf item Is TableDefinition Then
                Return TableTemplate
            ElseIf TypeOf item Is ColumnDefinition Then
                Return ColumnTemplate
            End If

            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class
End Namespace
