Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace ChartsDemo

    Public Class BoxPlotPoint

        Private _ExperimentNumber As Integer, _Min As Double, _Quartile1 As Double, _Median As Double, _Quartile3 As Double, _Max As Double, _Mean As Double, _Outliers As List(Of Double)

        Public Property ExperimentNumber As Integer
            Get
                Return _ExperimentNumber
            End Get

            Private Set(ByVal value As Integer)
                _ExperimentNumber = value
            End Set
        End Property

        Public Property Min As Double
            Get
                Return _Min
            End Get

            Private Set(ByVal value As Double)
                _Min = value
            End Set
        End Property

        Public Property Quartile1 As Double
            Get
                Return _Quartile1
            End Get

            Private Set(ByVal value As Double)
                _Quartile1 = value
            End Set
        End Property

        Public Property Median As Double
            Get
                Return _Median
            End Get

            Private Set(ByVal value As Double)
                _Median = value
            End Set
        End Property

        Public Property Quartile3 As Double
            Get
                Return _Quartile3
            End Get

            Private Set(ByVal value As Double)
                _Quartile3 = value
            End Set
        End Property

        Public Property Max As Double
            Get
                Return _Max
            End Get

            Private Set(ByVal value As Double)
                _Max = value
            End Set
        End Property

        Public Property Mean As Double
            Get
                Return _Mean
            End Get

            Private Set(ByVal value As Double)
                _Mean = value
            End Set
        End Property

        Public Property Outliers As List(Of Double)
            Get
                Return _Outliers
            End Get

            Private Set(ByVal value As List(Of Double))
                _Outliers = value
            End Set
        End Property

        Public Sub New(ByVal experimentNumber As Integer, ByVal randomSequence As List(Of Double))
            Me.ExperimentNumber = experimentNumber
            CalculateBoxPlotValues(randomSequence)
        End Sub

        Private Sub CalculateBoxPlotValues(ByVal randomSequence As List(Of Double))
            Dim quartiles As Tuple(Of Double, Double, Double) = CalculateQuartiles(randomSequence)
            Quartile1 = quartiles.Item1
            Quartile3 = quartiles.Item3
            Median = quartiles.Item2
            Dim averageAndMinMax As Tuple(Of Double, Double, Double) = CalculateAverageAndMinMax(randomSequence)
            Mean = averageAndMinMax.Item1
            Min = Math.Max(averageAndMinMax.Item2, Quartile1 - 1.5 * (Quartile3 - Quartile1))
            Max = Math.Min(averageAndMinMax.Item3, Quartile3 + 1.5 * (Quartile3 - Quartile1))
            Outliers = randomSequence.Where(Function(d) d > Max OrElse d < Min).ToList()
        End Sub

        Private Function CalculateAverageAndMinMax(ByVal randomSequence As List(Of Double)) As Tuple(Of Double, Double, Double)
            Dim average As Double = 0
            Dim min As Double = Double.MaxValue
            Dim max As Double = Double.MinValue
            For Each d As Double In randomSequence
                average += d
                If d > max Then max = d
                If d < min Then min = d
            Next

            average = average / randomSequence.Count
            Return New Tuple(Of Double, Double, Double)(average, min, max)
        End Function

        Private Function CalculateQuartiles(ByVal randomSequence As List(Of Double)) As Tuple(Of Double, Double, Double)
            randomSequence.Sort()
            Dim middleIndex As Integer = CInt(randomSequence.Count \ 2)
            Dim quartile1 As Double = 0
            Dim quartile2 As Double
            Dim quartile3 As Double = 0
            If randomSequence.Count Mod 2 = 0 Then
                quartile2 =(randomSequence(middleIndex - 1) + randomSequence(middleIndex)) / 2
                Dim middleIndexOfHalf As Integer = middleIndex \ 2
                If middleIndex Mod 2 = 0 Then
                    quartile1 =(randomSequence(middleIndexOfHalf - 1) + randomSequence(middleIndexOfHalf)) / 2
                    quartile3 =(randomSequence(middleIndex + middleIndexOfHalf - 1) + randomSequence(middleIndex + middleIndexOfHalf)) / 2
                Else
                    quartile1 = randomSequence(middleIndexOfHalf)
                    quartile3 = randomSequence(middleIndexOfHalf + middleIndex)
                End If
            ElseIf randomSequence.Count = 1 Then
                quartile1 = randomSequence(0)
                quartile2 = randomSequence(0)
                quartile3 = randomSequence(0)
            Else
                quartile2 = randomSequence(middleIndex)
                If(randomSequence.Count - 1) Mod 4 = 0 Then
                    Dim quarterIndex As Integer = CInt((randomSequence.Count - 1) \ 4)
                    quartile1 = randomSequence(quarterIndex - 1) * .25 + randomSequence(quarterIndex) * .75
                    quartile3 = randomSequence(3 * quarterIndex) * .75 + randomSequence(3 * quarterIndex + 1) * .25
                ElseIf(randomSequence.Count - 3) Mod 4 = 0 Then
                    Dim quarterIndex As Integer = CInt((randomSequence.Count - 3) \ 4)
                    quartile1 = randomSequence(quarterIndex) * .75 + randomSequence(quarterIndex + 1) * .25
                    quartile3 = randomSequence(3 * quarterIndex + 1) * .25 + randomSequence(3 * quarterIndex + 2) * .75
                End If
            End If

            Return New Tuple(Of Double, Double, Double)(quartile1, quartile2, quartile3)
        End Function
    End Class

    Public Class PointData

        Private _Argument As Double, _Value As Double

        Public Property Argument As Double
            Get
                Return _Argument
            End Get

            Private Set(ByVal value As Double)
                _Argument = value
            End Set
        End Property

        Public Property Value As Double
            Get
                Return _Value
            End Get

            Private Set(ByVal value As Double)
                _Value = value
            End Set
        End Property

        Public Sub New(ByVal val As Double, ByVal rnd As Random, ByVal Optional argumentOffset As Double = 0)
            Argument = rnd.NextDouble() + argumentOffset
            Value = val
        End Sub
    End Class
End Namespace
