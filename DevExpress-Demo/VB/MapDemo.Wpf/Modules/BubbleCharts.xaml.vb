Imports DevExpress.Xpf.Editors
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Data

Namespace MapDemo

    Public Partial Class BubbleCharts
        Inherits MapDemoModule

        Const MinMag As Double = 6.5

        Const MaxMag As Double = 9

        Private dataView As ICollectionView

        Public ReadOnly Property Data As ICollectionView
            Get
                Return dataView
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            lbYearsFilter.SelectAll()
            DataContext = Me
            SetData()
        End Sub

        Private Sub SetData()
            Dim earthquakes As List(Of EarthquakeViewModel) = EarthquakesData.Instance.Items
            dataView = CollectionViewSource.GetDefaultView(earthquakes)
            UpdateFilter(MinMag, MaxMag)
        End Sub

        Private Sub UpdateFilter(ByVal minMagnitude As Double, ByVal maxMagnitude As Double)
            If Data IsNot Nothing Then Data.Filter = Function(item)
                Dim earthquake As EarthquakeViewModel = CType(item, EarthquakeViewModel)
                Return GetMagnitudeFilter(earthquake, minMagnitude, maxMagnitude) AndAlso GetYearFilter(earthquake, lbYearsFilter.SelectedItems)
            End Function
        End Sub

        Private Function GetMagnitudeFilter(ByVal earthquake As EarthquakeViewModel, ByVal min As Double, ByVal max As Double) As Boolean
            Return earthquake.Magnitude >= min AndAlso earthquake.Magnitude <= max
        End Function

        Private Function GetYearFilter(ByVal earthquake As EarthquakeViewModel, ByVal selectedYears As IList(Of Object)) As Boolean
            For Each item As ListBoxEditItem In selectedYears
                Dim year As Integer = Convert.ToInt32(item.Tag)
                If earthquake.Year = year Then Return True
            Next

            Return False
        End Function

        Private Sub lbYearsFilter_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Update()
        End Sub

        Private Sub tbMagnitudeFilter_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Update()
        End Sub

        Private Sub Update()
            Dim range As TrackBarEditRange = CType(tbMagnitudeFilter.EditValue, TrackBarEditRange)
            UpdateFilter(range.SelectionStart, range.SelectionEnd)
        End Sub
    End Class
End Namespace
