Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo

    Public Module AnimationHelper

        Public Function CreateSeriesAnimation(ByVal animationKind As AnimationKind, ByVal seriesIndex As Integer) As SeriesAnimationBase
            If animationKind Is Nothing OrElse animationKind.Type Is Nothing Then Return Nothing
            Dim seriesAnimation = CType(Activator.CreateInstance(animationKind.Type), SeriesAnimationBase)
            If Equals(animationKind.Name, "None") Then
                seriesAnimation.Duration = New TimeSpan(0)
                seriesAnimation.BeginTime = New TimeSpan(0)
            Else
                seriesAnimation.BeginTime = TimeSpan.FromMilliseconds(Math.Round(seriesAnimation.Duration.TotalMilliseconds / 4.0) * seriesIndex)
            End If

            Return seriesAnimation
        End Function

        Public Function CreatePointAnimation(ByVal animationKind As AnimationKind, ByVal seriesAnimation As SeriesAnimationBase, ByVal seriesIndex As Integer) As SeriesPointAnimationBase
            If animationKind Is Nothing OrElse animationKind.Type Is Nothing Then Return Nothing
            Dim pointAnimation = CType(Activator.CreateInstance(animationKind.Type), SeriesPointAnimationBase)
            If Equals(animationKind.Name, "None") Then
                pointAnimation.Duration = New TimeSpan(0)
                pointAnimation.PointDelay = New TimeSpan(0)
                pointAnimation.BeginTime = New TimeSpan(0)
            ElseIf seriesAnimation IsNot Nothing Then
                pointAnimation.BeginTime = seriesAnimation.BeginTime
            Else
                pointAnimation.BeginTime = TimeSpan.FromMilliseconds(pointAnimation.PointDelay.TotalMilliseconds * seriesIndex)
            End If

            Return pointAnimation
        End Function

        Public Sub InitializeAnimationListBoxEdit(ByVal listBoxEdit As ListBoxEdit, ByVal animationKinds As IEnumerable(Of AnimationKind), ByVal defaultAnimationType As Type)
            Dim allAnimationKinds As List(Of AnimationKind) = New List(Of AnimationKind)()
            Dim noneAnimation As AnimationKind = GetNoneAnimation(animationKinds)
            allAnimationKinds.Add(noneAnimation)
            allAnimationKinds.AddRange(animationKinds)
            listBoxEdit.ClearValue(ListBoxEdit.SelectedItemProperty)
            listBoxEdit.ItemsSource = allAnimationKinds
            listBoxEdit.SelectedItem = If(animationKinds.Where(Function(x) x.Type Is defaultAnimationType).FirstOrDefault(), noneAnimation)
        End Sub

        Private Function GetNoneAnimation(ByVal animationKinds As IEnumerable(Of AnimationKind)) As AnimationKind
            If animationKinds Is Nothing OrElse Not animationKinds.Any() Then Return New AnimationKind(Nothing, "None")
            Return New AnimationKind(Enumerable.First(animationKinds).Type, "None")
        End Function

        Public Function GetDefaultPointAnimationType(ByVal series As Series) As Type
            If TypeOf series Is BarSideBySideSeries2D OrElse TypeOf series Is RangeBarSeries2D Then Return GetType(Bar2DGrowUpAnimation)
            If TypeOf series Is BarStackedSeries2D Then Return GetType(Bar2DDropInAnimation)
            If TypeOf series Is PointSeries2D Then Return GetType(Marker2DSlideFromLeftAnimation)
            If TypeOf series Is BubbleSeries2D Then Return GetType(Marker2DWidenAnimation)
            If TypeOf series Is LineSeries2D OrElse TypeOf series Is AreaSeries2D OrElse TypeOf series Is RangeAreaSeries2D Then Return GetType(Marker2DFadeInAnimation)
            If TypeOf series Is AreaStackedSeries2D Then Return GetType(AreaStacked2DFadeInAnimation)
            If TypeOf series Is FinancialSeries2D Then Return GetType(Stock2DSlideFromTopAnimation)
            If TypeOf series Is PieSeries2D Then Return GetType(Pie2DGrowUpAnimation)
            If TypeOf series Is FunnelSeries2D Then Return GetType(Funnel2DWidenAnimation)
            If TypeOf series Is CircularSeriesBase2D Then Return GetType(CircularMarkerSlideToCenterAnimation)
            If TypeOf series Is BoxPlotSeries2D Then Return GetType(BoxPlot2DSlideFromTopAnimation)
            Return Nothing
        End Function

        Public Function GetDefaultSeriesAnimationType(ByVal series As Series) As Type
            If TypeOf series Is LineSeries2D Then Return GetType(Line2DStretchFromNearAnimation)
            If TypeOf series Is AreaSeries2D OrElse TypeOf series Is RangeAreaSeries2D Then Return GetType(Area2DStretchFromNearAnimation)
            If TypeOf series Is AreaStackedSeries2D Then Return GetType(Area2DDropFromFarAnimation)
            If TypeOf series Is CircularAreaSeries2D Then Return GetType(CircularAreaZoomInAnimation)
            If TypeOf series Is CircularLineSeries2D Then Return GetType(CircularLineZoomInAnimation)
            If TypeOf series Is CircularRangeAreaSeries2D Then Return GetType(CircularAreaZoomInAnimation)
            If TypeOf series Is BoxPlotSeries2D Then Return GetType(Line2DUnwindAnimation)
            Return Nothing
        End Function
    End Module
End Namespace
