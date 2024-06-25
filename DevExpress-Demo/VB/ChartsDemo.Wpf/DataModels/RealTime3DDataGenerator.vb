Imports System
Imports System.Collections.Generic
Imports System.Windows.Media.Media3D

Namespace ChartsDemo

    Public Class RealTime3DDataGenerator

#Region "ElevationData"
        Public Class ElevationData

            Private directionXField As Integer

            Private directionZField As Integer

            Private directionYField As Integer

            Public Property Elevation As Point3D

            Public ReadOnly Property DirectionX As Integer
                Get
                    Return directionXField
                End Get
            End Property

            Public ReadOnly Property DirectionZ As Integer
                Get
                    Return directionZField
                End Get
            End Property

            Public ReadOnly Property DirectionY As Integer
                Get
                    Return directionYField
                End Get
            End Property

            Public Sub New(ByVal elevation As Point3D)
                Me.Elevation = elevation
                directionXField = 1
                directionZField = 1
                directionYField = 1
            End Sub

            Public Sub UpdateDirection(ByVal sideLength As Integer)
                If Elevation.X < 0 OrElse Elevation.X >= sideLength Then directionXField *= -1
                If Elevation.Z < 0 OrElse Elevation.Z >= sideLength Then directionZField *= -1
                If Elevation.Y < -sideLength \ 3 OrElse Elevation.Y > sideLength \ 3 Then directionYField *= -1
            End Sub
        End Class

#End Region
        Const OffsetFactor As Double = Math.PI * 3R / 2R

        Const RadiusFactor As Double = 10R / Math.PI

        Const ElevationsCount As Integer = 25

        Private ReadOnly rnd As Random = New Random(0)

        Private ReadOnly vertices As ElevationData()

        Private sensitivityDirection As Integer = 1

        Private sensitivity As Double

        Public Property Size As Integer

        Public Sub New()
            vertices = New ElevationData(24) {}
        End Sub

        Private Sub NextElevations()
            Dim k As Double = Size / 66R
            For i As Integer = 0 To ElevationsCount - 1
                vertices(i).UpdateDirection(Size)
                Dim dx As Double = k * vertices(i).DirectionX
                Dim dz As Double = k * vertices(i).DirectionZ
                Dim dy As Double = k * vertices(i).DirectionY
                Dim x As Double = vertices(i).Elevation.X + dx
                Dim z As Double = vertices(i).Elevation.Z + dz
                Dim y As Double = vertices(i).Elevation.Y + dy
                vertices(i).Elevation = New Point3D(x, y, z)
            Next
        End Sub

        Private Sub NextSensitivity()
            If sensitivity < Size * 0.15 Then sensitivityDirection = 1
            If sensitivity > Size * 0.6 Then sensitivityDirection = -1
            sensitivity += sensitivityDirection * 0.2
        End Sub

        Public Sub RecreateElevations()
            sensitivity = Size * 0.5
            For i As Integer = 0 To ElevationsCount - 1
                Dim elevation As Point3D = New Point3D(rnd.Next(0, Size), rnd.Next(-Size \ 3, Size \ 3), rnd.Next(0, Size))
                vertices(i) = New ElevationData(elevation)
            Next
        End Sub

        Public Function GenerateArguments() As IEnumerable(Of Object)
            Dim arguments As List(Of Object) = New List(Of Object)()
            For i As Integer = 0 To Size - 1
                arguments.Add(i)
            Next

            Return arguments
        End Function

        Public Function GenerateValues() As IEnumerable(Of Double)
            NextElevations()
            NextSensitivity()
            Dim values As Double() = New Double(Size * Size - 1) {}
            For j As Integer = 0 To vertices.Length - 1
                Dim dataX As Double = vertices(j).Elevation.X
                Dim dataZ As Double = vertices(j).Elevation.Z
                Dim dataY As Double = vertices(j).Elevation.Y
                Dim counter As Integer = 0
                For x As Integer = 0 To Size - 1
                    For z As Integer = 0 To Size - 1
                        Dim dx As Double = dataX - x
                        Dim dz As Double = dataZ - z
                        Dim distance As Double = Math.Sqrt(dx * dx + dz * dz)
                        If distance <= sensitivity Then
                            Dim percent As Double = 1.0 - distance / sensitivity
                            Dim coef As Double = dataY - values(counter)
                            Dim elevate As Double = coef * (Math.Sin(percent * RadiusFactor + OffsetFactor) + 1.0) / 2
                            values(counter) += elevate
                        End If

                        counter += 1
                    Next
                Next
            Next

            Return values
        End Function
    End Class
End Namespace
