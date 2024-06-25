Imports DevExpress.Xpf.Charts
Imports System

Namespace ProductsDemo

    Friend Module ChartFactory

        Public Function GenerateDiagram(ByVal seriesType As Type, ByVal diagramType As Type, ByVal showPointsLabels As Boolean?) As Diagram
            Dim seriesTemplate As Series = CreateSeriesInstance(seriesType)
            Dim diagram As Diagram = CreateDiagramBySeriesType(diagramType)
            If TypeOf diagram Is XYDiagram2D Then Call PrepareXYDiagram2D(TryCast(diagram, XYDiagram2D))
            If TypeOf diagram Is XYDiagram3D Then Call PrepareXYDiagram3D(TryCast(diagram, XYDiagram3D))
            If TypeOf diagram Is Diagram3D Then CType(diagram, Diagram3D).RuntimeRotation = True
            diagram.SeriesDataMember = "Series"
            seriesTemplate.ArgumentDataMember = "Arguments"
            seriesTemplate.ValueDataMember = "Values"
            If seriesTemplate.Label Is Nothing Then seriesTemplate.Label = New SeriesLabel()
            seriesTemplate.LabelsVisibility = showPointsLabels.Value = True
            If TypeOf seriesTemplate Is PieSeries2D OrElse TypeOf seriesTemplate Is PieSeries3D Then
                seriesTemplate.LegendTextPattern = "{A}"
                seriesTemplate.Label.TextPattern = "{A}: {VP:P0}"
            Else
                seriesTemplate.LegendTextPattern = "{A}: {V}"
                seriesTemplate.ShowInLegend = True
            End If

            diagram.SeriesTemplate = seriesTemplate
            Return diagram
        End Function

        Private Sub PrepareXYDiagram2D(ByVal diagram As XYDiagram2D)
            If diagram Is Nothing Then Return
            diagram.AxisX = New AxisX2D()
            diagram.AxisX.Label = New AxisLabel()
            diagram.AxisX.Label.Staggered = True
        End Sub

        Private Sub PrepareXYDiagram3D(ByVal diagram As XYDiagram3D)
            If diagram Is Nothing Then Return
            diagram.AxisX = New AxisX3D()
            diagram.AxisX.Label = New AxisLabel()
            diagram.AxisX.Label.Visible = False
        End Sub

        Public Function CreateSeriesInstance(ByVal seriesType As Type) As Series
            Dim series As Series = CType(Activator.CreateInstance(seriesType), Series)
            Dim supportTransparency As ISupportTransparency = TryCast(series, ISupportTransparency)
            If supportTransparency IsNot Nothing Then
                Dim flag As Boolean = TypeOf series Is AreaSeries2D
                flag = flag OrElse TypeOf series Is AreaSeries3D
                If flag Then
                    supportTransparency.Transparency = 0.4
                Else
                    supportTransparency.Transparency = 0
                End If
            End If

            Return series
        End Function

        Private Function CreateDiagramBySeriesType(ByVal diagramType As Type) As Diagram
            Return CType(Activator.CreateInstance(diagramType), Diagram)
        End Function
    End Module
End Namespace
