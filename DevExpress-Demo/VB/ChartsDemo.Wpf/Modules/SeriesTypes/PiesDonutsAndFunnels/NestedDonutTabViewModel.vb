Namespace ChartsDemo

    Public Class NestedDonutTabViewModel

        Private _DataSource As Object

        Const AgeDataMember As String = "Age"

        Const GenderDataMember As String = "Sex"

        Public Overridable Property ArgumentDataMember As String

        Public Overridable Property SeriesDataMember As String

        Public Overridable Property DemoTitle As String

        Public Overridable Property HintDataMember As String

        Public Overridable Property GenderLegendVisible As Boolean

        Public Overridable Property AgeLegendVisible As Boolean

        Public Property DataSource As Object
            Get
                Return _DataSource
            End Get

            Private Set(ByVal value As Object)
                _DataSource = value
            End Set
        End Property

        Public Sub New()
            DataSource = New AgeStructure().GetPopulationAgeStructure()
            ArgumentDataMember = AgeDataMember
        End Sub

        Protected Sub OnArgumentDataMemberChanged()
            HintDataMember = If(Equals(ArgumentDataMember, AgeDataMember), GenderDataMember, AgeDataMember)
            SeriesDataMember = "Country" & HintDataMember & "Key"
            DemoTitle = "Population: " & ArgumentDataMember & " Structure"
            GenderLegendVisible = Equals(ArgumentDataMember, AgeDataMember)
            AgeLegendVisible = Not GenderLegendVisible
        End Sub
    End Class
End Namespace
