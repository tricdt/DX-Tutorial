Imports System.Windows
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Partial Class SideBySideStackedBarTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub chart_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GroupSeries()
        End Sub

        Private Sub lbeGroupBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GroupSeries()
            chart.Animate()
        End Sub

        Private Sub GroupSeries()
            For Each series As BarSideBySideStackedSeries2D In chart.Diagram.Series
                Dim genderAge As GenderAgeInfo = CType(series.Tag, GenderAgeInfo)
                series.StackedGroup = If(lbeGroupBy.SelectedIndex = 0, genderAge.Gender, genderAge.Age)
                If Equals(CStr(series.StackedGroup), "65 years and older") Then
                    series.StackedGroup = "65+ years"
                End If
            Next
        End Sub
    End Class
End Namespace
