Imports System
Imports System.Collections.Generic
Imports System.Windows.Media.Media3D

Namespace ChartsDemo

    Public Module SurfaceDataGenerator

        Public Function Generate() As List(Of Point3D)
            Dim points As List(Of Point3D) = New List(Of Point3D)()
            For j As Double = -25.0 To 25.0 - 1 Step 0.75R
                For i As Double = -15.0 To 15.0 - 1 Step 0.75R
                    Dim x As Double = 2 + i + Math.Sin(j / 4.0) * 6
                    Dim y As Double = 1 + j + Math.Cos(i / 4.0)
                    Dim z As Double = 5.5 * Math.Sin(i / 3.0) * Math.Sin(j / 3.0)
                    points.Add(New Point3D(x, y, z))
                Next
            Next

            Return points
        End Function
    End Module
End Namespace
