Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Friend Module RandomSequenceGenerator

        Public Function GenerateRandomSequence(ByVal random As Random, ByVal length As Integer) As List(Of Double)
            Dim selector As Double = random.NextDouble()
            If selector < 0.33 Then Return GenerateExponentialDistribution(random, length)
            If selector < 0.66 Then
                Return GenerateSpecialDistribution(random, length)
            Else
                Return GenerateNormalDistribution(random, length)
            End If
        End Function

        Private Function GenerateNormalDistribution(ByVal random As Random, ByVal length As Integer) As List(Of Double)
            Dim list As List(Of Double) = New List(Of Double)(length)
            Dim mean As Double = random.Next(450, 550)
            Dim stdDev As Double = random.Next(50, 70)
            For i As Integer = 0 To length - 1
                Dim u1 As Double = 1.0 - random.NextDouble()
                Dim u2 As Double = 1.0 - random.NextDouble()
                Dim randStdNormal As Double = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2)
                list.Add(mean + stdDev * randStdNormal)
            Next

            Return list
        End Function

        Private Function GenerateExponentialDistribution(ByVal random As Random, ByVal length As Integer) As List(Of Double)
            Dim list As List(Of Double) = New List(Of Double)(length)
            Dim minVal As Double = random.Next(250, 300)
            Dim maxVal As Double = minVal + 300
            Dim generatedCount As Integer = 0
            Dim lambda As Double = random.NextDouble() * 2
            While generatedCount < length
                Dim u As Double = random.NextDouble()
                Dim t As Double = -Math.Log(u) / lambda
                Dim increment As Double =(maxVal - minVal) / 6.0
                Dim result As Double = minVal + t * increment
                If result < maxVal Then
                    list.Add(result)
                    generatedCount += 1
                End If
            End While

            Return list
        End Function

        Private Function GenerateSpecialDistribution(ByVal random As Random, ByVal length As Integer) As List(Of Double)
            Dim list As List(Of Double) = New List(Of Double)(length)
            Dim min As Integer = random.Next(100, 250)
            Dim [step] As Integer = random.Next(30, 70)
            For i As Integer = 0 To CInt(length * 0.05) - 1
                list.Add(random.Next(min, min + [step]))
            Next

            For i As Integer = 0 To CInt(length * 0.025) - 1
                list.Add(random.Next(min + [step] + 1, min + 2 * [step]))
            Next

            For i As Integer = 0 To CInt(length * 0.075) - 1
                list.Add(random.Next(min + 2 * [step] + 1, min + 3 * [step]))
            Next

            For i As Integer = 0 To CInt(length * 0.10) - 1
                list.Add(random.Next(min + 3 * [step] + 1, min + 4 * [step]))
            Next

            For i As Integer = 0 To CInt(length * 0.20) - 1
                list.Add(random.Next(min + 4 * [step] + 1, min + 5 * [step]))
            Next

            For i As Integer = 0 To CInt(length * 0.30) - 1
                list.Add(random.Next(min + 5 * [step] + 1, min + 6 * [step]))
            Next

            For i As Integer = 0 To CInt(length * 0.20) - 1
                list.Add(random.Next(min + 6 * [step] + 1, min + 7 * [step]))
            Next

            For i As Integer = 0 To CInt(length * 0.05) + 1 - 1
                list.Add(random.Next(min + 7 * [step] + 1, min + 8 * [step]))
            Next

            Return list
        End Function
    End Module
End Namespace
