Imports System
Imports System.Linq
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Friend Class StandardDeviationCustomAggregateFunction
        Inherits CustomAggregateFunction

        Public Overrides Function ToString() As String
            Return "StdDev (Custom)"
        End Function

        Public Overrides Function Calculate(ByVal groupInfo As GroupInfo) As Double()
            Dim sum As Double = 0.0
            For Each value As Double In groupInfo.Values1
                sum += value
            Next

            Dim len As Integer = groupInfo.Values1.Count()
            Dim averageAmount As Double = sum / len
            Dim standardDeviationSquareSum As Double = 0.0
            For Each value As Double In groupInfo.Values1
                Dim deviation As Double = value - averageAmount
                standardDeviationSquareSum += deviation * deviation
            Next

            Dim stdDev As Double = Math.Sqrt(standardDeviationSquareSum / len)
            Return New Double(0) {stdDev}
        End Function
    End Class
End Namespace
