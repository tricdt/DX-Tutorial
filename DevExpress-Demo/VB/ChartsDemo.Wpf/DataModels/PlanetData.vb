Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class PlanetInfo

        Private ReadOnly planetField As String

        Private ReadOnly massField As Double

        Private ReadOnly moonsNumberField As Integer

        Public ReadOnly Property Planet As String
            Get
                Return planetField
            End Get
        End Property

        Public ReadOnly Property Mass As Double
            Get
                Return massField
            End Get
        End Property

        Public ReadOnly Property MoonsNumber As Integer
            Get
                Return moonsNumberField
            End Get
        End Property

        Public Sub New(ByVal planet As String, ByVal mass As Double, ByVal moonsNumber As Integer)
            planetField = planet
            massField = mass
            moonsNumberField = moonsNumber
        End Sub
    End Class

    Public Module PlanetData

        Public ReadOnly Property Data As List(Of PlanetInfo)
            Get
                Return New List(Of PlanetInfo)() From {New PlanetInfo("Mercury", 0.06, 0), New PlanetInfo("Venus", 0.82, 0), New PlanetInfo("Earth", 1, 1), New PlanetInfo("Mars", 0.11, 2), New PlanetInfo("Jupiter", 318, 69), New PlanetInfo("Saturn", 95, 62), New PlanetInfo("Uranus", 14.6, 27), New PlanetInfo("Neptune", 17.2, 14)}
            End Get
        End Property
    End Module
End Namespace
