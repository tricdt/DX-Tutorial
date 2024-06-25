Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.POCO

Namespace ChartsDemo

    Public Class ChartDataSourceViewModel(Of T)

        Public Property Sources As List(Of ChartDataSource(Of T))

        Public Overridable Property SelectedSource As ChartDataSource(Of T)

        Public Overridable ReadOnly Property ChartAnimationService As IChartAnimationService
            Get
                Return Nothing
            End Get
        End Property

        Public Sub Animate()
            If ChartAnimationService IsNot Nothing Then ChartAnimationService.Animate()
        End Sub

        Protected Sub OnSelectedSourceChanged()
            Animate()
        End Sub
    End Class

    Public Class ChartDataSource(Of T)

        Public Property Description As String

        Public Property DataSource As List(Of T)
    End Class

    Public Module ChartViewModelFactory

#Region "Nested classes"
        Private NotInheritable Class PolarFunctionsPointGenerator

            Public Shared Function CreateLemniskateData() As List(Of RangeDataPoint)
                Dim list As List(Of RangeDataPoint) = New List(Of RangeDataPoint)()
                For x As Double = 0 To 360 - 1 Step 5
                    Dim xRadian As Double = DegreeToRadian(x)
                    Dim cos As Double = Math.Cos(2 * xRadian)
                    Dim y As Double = Math.Pow(Math.Abs(cos), 2)
                    list.Add(New RangeDataPoint(x, y, 1))
                Next

                Return list
            End Function

            Public Shared Function CreateCardioidData() As List(Of RangeDataPoint)
                Dim list As List(Of RangeDataPoint) = New List(Of RangeDataPoint)()
                Const a As Double = 0.5
                For x As Double = 0 To 360 - 1 Step 15
                    Dim y As Double = 2 * a * Math.Cos(DegreeToRadian(x))
                    list.Add(New RangeDataPoint(x, y, 1))
                Next

                Return list
            End Function

            Public Shared Function CreateTaubinsHeartData() As List(Of RangeDataPoint)
                Dim list As List(Of RangeDataPoint) = New List(Of RangeDataPoint)()
                For x As Double = 0 To 360 - 1 Step 15
                    Dim xRadian As Double = DegreeToRadian(x)
                    Dim y As Double = 2 - 0.5 * Math.Sin(xRadian) + Math.Sin(xRadian) * Math.Sqrt(Math.Abs(Math.Cos(xRadian))) / (Math.Sin(xRadian) + 1.4)
                    list.Add(New RangeDataPoint(x, y, 1))
                Next

                Return list
            End Function

            Private Shared Function DegreeToRadian(ByVal degree As Double) As Double
                Return 2 * Math.PI / 360 * degree
            End Function
        End Class

        Public Structure RangeDataPoint

            Private ReadOnly xField As Double

            Private ReadOnly y1Field As Double

            Private ReadOnly y2Field As Double

            Public ReadOnly Property X As Double
                Get
                    Return xField
                End Get
            End Property

            Public ReadOnly Property Y1 As Double
                Get
                    Return y1Field
                End Get
            End Property

            Public ReadOnly Property Y2 As Double
                Get
                    Return y2Field
                End Get
            End Property

            Public Sub New(ByVal x As Double, ByVal y1 As Double, ByVal y2 As Double)
                xField = x
                y1Field = y1
                y2Field = y2
            End Sub
        End Structure

        Private NotInheritable Class CartesianFunctionsPointGenerator

            Const a As Integer = 10

            Public Shared Function CreateArchimedianSpiralPoints() As List(Of Point)
                Dim points = New List(Of Point)()
                For i As Integer = 0 To 720 - 1 Step 15
                    Dim t As Double = CDbl(i) / 180 * Math.PI
                    Dim x As Double = t * Math.Cos(t)
                    Dim y As Double = t * Math.Sin(t)
                    points.Add(New Point(x, y))
                Next

                Return points
            End Function

            Public Shared Function CreateCardioidPoints() As List(Of Point)
                Dim points = New List(Of Point)()
                For i As Integer = 0 To 360 - 1 Step 10
                    Dim t As Double = CDbl(i) / 180 * Math.PI
                    Dim x As Double = a * (2 * Math.Cos(t) - Math.Cos(2 * t))
                    Dim y As Double = a * (2 * Math.Sin(t) - Math.Sin(2 * t))
                    points.Add(New Point(x, y))
                Next

                Return points
            End Function

            Public Shared Function CreateCartesianFoliumPoints() As List(Of Point)
                Dim points = New List(Of Point)()
                For i As Integer = -30 To 125 - 1 Step 5
                    Dim t As Double = Math.Tan(CDbl(i) / 180 * Math.PI)
                    Dim x As Double = 3 * CDbl(a) * t / (t * t * t + 1)
                    Dim y As Double = x * t
                    points.Add(New Point(x, y))
                Next

                Return points
            End Function
        End Class

        Private MustInherit Class ScatterFunctionCalculatorBase

            Const spiralIntervalsCount As Integer = 72

            Const roseIntervalsCount As Integer = 288

            Const foliumSegmentIntervalsCount As Integer = 30

            Const roseParameter As Double = 7.0R / 4.0R

            Const foliumDistanceLimit As Double = 3.0

            Protected MustOverride Function NormalizeAngle(ByVal angle As Double) As Double

            Protected MustOverride Function ToRadian(ByVal angle As Double) As Double

            Protected MustOverride Function FromDegrees(ByVal angle As Double) As Double

            Private Function FilterPointsByRange(ByVal points As List(Of Point), ByVal min As Double, ByVal max As Double) As List(Of Point)
                Dim resultPoints As List(Of Point) = New List(Of Point)()
                For Each point As Point In points
                    Dim pointValue As Double = point.Y
                    If pointValue <= max AndAlso pointValue >= min Then resultPoints.Add(point)
                Next

                Return resultPoints
            End Function

            Private Function CreatePolarFunctionPoints(ByVal minAngleDegree As Double, ByVal maxAngleDegree As Double, ByVal intervalsCount As Integer, ByVal [function] As Func(Of Double, Double)) As List(Of Point)
                Dim points = New List(Of Point)()
                Dim minAngle As Double = FromDegrees(minAngleDegree)
                Dim maxAngle As Double = FromDegrees(maxAngleDegree)
                Dim angleStep As Double =(maxAngle - minAngle) / intervalsCount
                For pointIndex As Integer = 0 To intervalsCount
                    Dim angle As Double = minAngle + pointIndex * angleStep
                    Dim angleRadians As Double = ToRadian(angle)
                    Dim distance As Double = [function](angleRadians)
                    Dim normalAngle As Double = NormalizeAngle(angle)
                    points.Add(New Point(normalAngle, distance))
                Next

                Return points
            End Function

            Private Function ArchimedeanSpiralFunction(ByVal angleRadians As Double) As Double
                Return angleRadians
            End Function

            Private Function PolarRoseFunction(ByVal angleRadians As Double) As Double
                Return Math.Max(0.0, Math.Sin(roseParameter * angleRadians))
            End Function

            Private Function PolarFoliumFunction(ByVal angleRadians As Double) As Double
                Dim sin As Double = Math.Sin(angleRadians)
                Dim cos As Double = Math.Cos(angleRadians)
                Return 3.0 * sin * cos / (Math.Pow(sin, 3.0) + Math.Pow(cos, 3.0))
            End Function

            Public Function CreateArchimedeanSpiralData() As List(Of Point)
                Return CreatePolarFunctionPoints(0.0, 720.0, spiralIntervalsCount, New Func(Of Double, Double)(AddressOf ArchimedeanSpiralFunction))
            End Function

            Public Function CreateRoseData() As List(Of Point)
                Return CreatePolarFunctionPoints(0.0, 1440.0, roseIntervalsCount, New Func(Of Double, Double)(AddressOf PolarRoseFunction))
            End Function

            Public Function CreateFoliumData() As List(Of Point)
                Dim points1 = CreatePolarFunctionPoints(120.0, 180.0, foliumSegmentIntervalsCount, New Func(Of Double, Double)(AddressOf PolarFoliumFunction))
                Dim points2 = CreatePolarFunctionPoints(0.0, 90.0, foliumSegmentIntervalsCount, New Func(Of Double, Double)(AddressOf PolarFoliumFunction))
                Dim points3 = CreatePolarFunctionPoints(270.0, 330.0, foliumSegmentIntervalsCount, New Func(Of Double, Double)(AddressOf PolarFoliumFunction))
                Return FilterPointsByRange(points1.Concat(points2).Concat(points3).ToList(), 0.0, foliumDistanceLimit)
            End Function
        End Class

        Private Class RadianScatterFunctionCalculator
            Inherits ScatterFunctionCalculatorBase

            Protected Overrides Function NormalizeAngle(ByVal angle As Double) As Double
                Return angle Mod Math.PI * 2.0
            End Function

            Protected Overrides Function ToRadian(ByVal angle As Double) As Double
                Return angle
            End Function

            Protected Overrides Function FromDegrees(ByVal angle As Double) As Double
                Return angle * Math.PI / 180.0
            End Function
        End Class

        Private Class DegreeScatterFunctionCalculator
            Inherits ScatterFunctionCalculatorBase

            Protected Overrides Function NormalizeAngle(ByVal angle As Double) As Double
                Return angle Mod 360
            End Function

            Protected Overrides Function ToRadian(ByVal angle As Double) As Double
                Return angle * Math.PI / 180.0
            End Function

            Protected Overrides Function FromDegrees(ByVal angle As Double) As Double
                Return angle
            End Function
        End Class

#End Region
#Region "Scatter Line ViewModels"
        Public Function CreatePolarScatterLineViewModel() As ChartDataSourceViewModel(Of Point)
            Return CreateScatterViewModel(New DegreeScatterFunctionCalculator())
        End Function

        Public Function CreateRadarScatterLineViewModel() As ChartDataSourceViewModel(Of Point)
            Return CreateScatterViewModel(New RadianScatterFunctionCalculator())
        End Function

        Private Function CreateScatterViewModel(ByVal calculator As ScatterFunctionCalculatorBase) As ChartDataSourceViewModel(Of Point)
            Dim viewModel = ViewModelSource.Create(Of ChartDataSourceViewModel(Of Point))()
            viewModel.Sources = New List(Of ChartDataSource(Of Point)) From {New ChartDataSource(Of Point) With {.Description = "Archimedean Spiral", .DataSource = calculator.CreateArchimedeanSpiralData()}, New ChartDataSource(Of Point) With {.Description = "Polar Rose", .DataSource = calculator.CreateRoseData()}, New ChartDataSource(Of Point) With {.Description = "Polar Folium", .DataSource = calculator.CreateFoliumData()}}
            viewModel.SelectedSource = viewModel.Sources.First()
            Return viewModel
        End Function

#End Region
#Region "Polar ViewModel"
        Public Function CreatePolarViewModel() As ChartDataSourceViewModel(Of RangeDataPoint)
            Dim viewModel = ViewModelSource.Create(Of ChartDataSourceViewModel(Of RangeDataPoint))()
            viewModel.Sources = New List(Of ChartDataSource(Of RangeDataPoint)) From {New ChartDataSource(Of RangeDataPoint) With {.Description = "Taubin's Heart", .DataSource = PolarFunctionsPointGenerator.CreateTaubinsHeartData()}, New ChartDataSource(Of RangeDataPoint) With {.Description = "Cardioid", .DataSource = PolarFunctionsPointGenerator.CreateCardioidData()}, New ChartDataSource(Of RangeDataPoint) With {.Description = "Lemniscate", .DataSource = PolarFunctionsPointGenerator.CreateLemniskateData()}}
            viewModel.SelectedSource = viewModel.Sources.Last()
            Return viewModel
        End Function

#End Region
#Region "Cartesian ViewModel"
        Public Function CreateCartesianViewModel() As ChartDataSourceViewModel(Of Point)
            Dim viewModel = ViewModelSource.Create(Of ChartDataSourceViewModel(Of Point))()
            viewModel.Sources = New List(Of ChartDataSource(Of Point)) From {New ChartDataSource(Of Point) With {.Description = "Archimedean Spiral", .DataSource = CartesianFunctionsPointGenerator.CreateArchimedianSpiralPoints()}, New ChartDataSource(Of Point) With {.Description = "Cardioid", .DataSource = CartesianFunctionsPointGenerator.CreateCardioidPoints()}, New ChartDataSource(Of Point) With {.Description = "Cartesian Folium", .DataSource = CartesianFunctionsPointGenerator.CreateCartesianFoliumPoints()}}
            viewModel.SelectedSource = viewModel.Sources.First()
            Return viewModel
        End Function
#End Region
    End Module

    Public Interface IChartDemoViewModel

        Sub Unload()

        Sub PauseTimer()

        Sub ResumeTimer()

        Sub UpdateDataSource()

    End Interface
End Namespace
