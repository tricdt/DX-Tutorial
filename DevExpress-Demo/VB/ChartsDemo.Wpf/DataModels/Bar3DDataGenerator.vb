Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media.Media3D

Namespace ChartsDemo

    Friend Class Bar3DDataGenerator

#Region "Point3DComparer"
        Private Class Point3DComparer
            Implements IEqualityComparer(Of Point3D)

            Const DefaultEps As Double = 10e-12

            Private ReadOnly eps As Double = DefaultEps

            Public Sub New()
            End Sub

            Public Sub New(ByVal eps As Double)
                Me.eps = eps
            End Sub

            Public Overloads Function Equals(ByVal p1 As Point3D, ByVal p2 As Point3D) As Boolean Implements IEqualityComparer(Of Point3D).Equals
                Return Math.Abs(p1.X - p2.X) < eps AndAlso Math.Abs(p1.Y - p2.Y) < eps AndAlso Math.Abs(p1.Z - p2.Z) < eps
            End Function

            Public Overloads Function GetHashCode(ByVal e1 As Point3D) As Integer Implements IEqualityComparer(Of Point3D).GetHashCode
                Return e1.GetHashCode()
            End Function
        End Class

#End Region
        Const PointsCount As Integer = 300

        Const MaxValue As Integer = 200

        Public Function Generate() As List(Of Point3D)
            Dim random = New Random(1)
            Dim data = New List(Of Point3D)()
            For i As Integer = 0 To PointsCount - 1
                Dim point As Point3D
                Do
                    Dim x As Double = random.NextDouble() * MaxValue * 2
                    Dim y As Double = random.NextDouble() * MaxValue * 2
                    Dim z As Double = random.NextDouble() * MaxValue
                    point = New Point3D(x, y, z)
                Loop While data.Contains(point, New Point3DComparer(1.0))

                data.Add(point)
            Next

            Return data
        End Function
    End Class
End Namespace
