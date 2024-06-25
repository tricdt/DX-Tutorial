Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm.POCO

Namespace ChartsDemo

    Public Class BoxPlotViewModel

        Public Shared Function Create() As BoxPlotViewModel
            Return ViewModelSource.Create(Function() New BoxPlotViewModel())
        End Function

        Const InitialResultCount As Integer = 25

        Const MeasurementsCount As Integer = 500

        Const SecondPointSeriesArgumentOffset As Double = 2R

        Private random As Random = New Random(3)

        Private randomValues1 As List(Of Double)

        Private randomValues2 As List(Of Double)

        Public Overridable Property CurrentExperimentRandomValues1 As ObservableCollection(Of PointData)

        Public Overridable Property CurrentExperimentRandomValues2 As ObservableCollection(Of PointData)

        Public Overridable Property ExperimentResults1 As ObservableCollection(Of BoxPlotPoint)

        Public Overridable Property ExperimentResults2 As ObservableCollection(Of BoxPlotPoint)

        Public Overridable Property ExperimentNumber As Integer

        Public Overridable Property CurrentExperimentStripMinLimit As Double

        Public Overridable Property CurrentExperimentStripMaxLimit As Double

        Public Overridable Property ShowMeanLine As Boolean

        Public Overridable Property MeanLineThickness As Integer

        Public Sub New()
            ExperimentNumber = 0
            CurrentExperimentRandomValues1 = New ObservableCollection(Of PointData)()
            CurrentExperimentRandomValues2 = New ObservableCollection(Of PointData)()
            ExperimentResults1 = New ObservableCollection(Of BoxPlotPoint)()
            ExperimentResults2 = New ObservableCollection(Of BoxPlotPoint)()
            MeanLineThickness = 1
            GenerateInitialResults()
        End Sub

        Private Sub GenerateInitialResults()
            For i As Integer = InitialResultCount - 1 To 0 + 1 Step -1
                Dim measurements1 As List(Of Double) = GenerateRandomSequence(random, MeasurementsCount)
                Dim measurements2 As List(Of Double) = GenerateRandomSequence(random, MeasurementsCount)
                ExperimentNumber += 1
                Dim point1 As BoxPlotPoint = New BoxPlotPoint(ExperimentNumber, measurements1)
                Dim point2 As BoxPlotPoint = New BoxPlotPoint(ExperimentNumber, measurements2)
                ExperimentResults1.Add(point1)
                ExperimentResults2.Add(point2)
            Next

            randomValues1 = GenerateRandomSequence(random, MeasurementsCount)
            randomValues2 = GenerateRandomSequence(random, MeasurementsCount)
            For i As Integer = 0 To MeasurementsCount - 1
                CurrentExperimentRandomValues1.Add(New PointData(randomValues1(i), random))
                CurrentExperimentRandomValues2.Add(New PointData(randomValues2(i), random, SecondPointSeriesArgumentOffset))
            Next

            ExperimentNumber += 1
            ExperimentResults1.Add(New BoxPlotPoint(ExperimentNumber, randomValues1))
            ExperimentResults2.Add(New BoxPlotPoint(ExperimentNumber, randomValues2))
            CurrentExperimentStripMinLimit = InitialResultCount - 0.5
            CurrentExperimentStripMaxLimit = InitialResultCount + 0.5
        End Sub

        Public Function CanAddNewDataSet() As Boolean
            Return True
        End Function

        Public Sub AddNewDataSet()
            CurrentExperimentRandomValues1.Clear()
            CurrentExperimentRandomValues2.Clear()
            randomValues1 = GenerateRandomSequence(random, MeasurementsCount)
            randomValues2 = GenerateRandomSequence(random, MeasurementsCount)
            For i As Integer = 0 To MeasurementsCount - 1
                CurrentExperimentRandomValues1.Add(New PointData(randomValues1(i), random))
                CurrentExperimentRandomValues2.Add(New PointData(randomValues2(i), random, SecondPointSeriesArgumentOffset))
            Next

            ExperimentNumber += 1
            CurrentExperimentStripMinLimit = ExperimentNumber - 0.5
            CurrentExperimentStripMaxLimit = ExperimentNumber + 0.5
        End Sub

        Public Sub CalculateBoxPlotForLastExperiment()
            If ExperimentNumber <> Enumerable.Last(ExperimentResults1).ExperimentNumber Then
                ExperimentResults1.Add(New BoxPlotPoint(ExperimentNumber, randomValues1))
                ExperimentResults2.Add(New BoxPlotPoint(ExperimentNumber, randomValues2))
            End If
        End Sub
    End Class
End Namespace
