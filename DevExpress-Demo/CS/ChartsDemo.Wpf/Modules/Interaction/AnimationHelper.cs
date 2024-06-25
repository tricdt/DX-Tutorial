using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    public static class AnimationHelper {
        public static SeriesAnimationBase CreateSeriesAnimation(AnimationKind animationKind, int seriesIndex) {
            if (animationKind == null || animationKind.Type == null) return null;
            var seriesAnimation = (SeriesAnimationBase)Activator.CreateInstance(animationKind.Type);
            if (animationKind.Name == "None") {
                seriesAnimation.Duration = new TimeSpan(0);
                seriesAnimation.BeginTime = new TimeSpan(0);
            }
            else {
                seriesAnimation.BeginTime = TimeSpan.FromMilliseconds(Math.Round(seriesAnimation.Duration.TotalMilliseconds / 4.0) * seriesIndex);
            }
            return seriesAnimation;
        }
        public static SeriesPointAnimationBase CreatePointAnimation(AnimationKind animationKind, SeriesAnimationBase seriesAnimation, int seriesIndex) {
            if (animationKind == null || animationKind.Type == null) return null;
            var pointAnimation = (SeriesPointAnimationBase)Activator.CreateInstance(animationKind.Type);
            if (animationKind.Name == "None") {
                pointAnimation.Duration = new TimeSpan(0);
                pointAnimation.PointDelay = new TimeSpan(0);
                pointAnimation.BeginTime = new TimeSpan(0);
            }
            else if (seriesAnimation != null) {
                pointAnimation.BeginTime = seriesAnimation.BeginTime;
            }
            else {
                pointAnimation.BeginTime = TimeSpan.FromMilliseconds(pointAnimation.PointDelay.TotalMilliseconds * seriesIndex);
            }
            return pointAnimation;
        }

        public static void InitializeAnimationListBoxEdit(ListBoxEdit listBoxEdit, IEnumerable<AnimationKind> animationKinds, Type defaultAnimationType) {
            List<AnimationKind> allAnimationKinds = new List<AnimationKind>();
            AnimationKind noneAnimation = GetNoneAnimation(animationKinds);
            allAnimationKinds.Add(noneAnimation);
            allAnimationKinds.AddRange(animationKinds);
            listBoxEdit.ClearValue(ListBoxEdit.SelectedItemProperty);
            listBoxEdit.ItemsSource = allAnimationKinds;
            listBoxEdit.SelectedItem = animationKinds.Where(x => x.Type == defaultAnimationType).FirstOrDefault() ?? noneAnimation;
        }
        static AnimationKind GetNoneAnimation(IEnumerable<AnimationKind> animationKinds) {
            if (animationKinds == null || !animationKinds.Any())
                return new AnimationKind(null, "None");
            return new AnimationKind(animationKinds.First().Type, "None");
        }

        public static Type GetDefaultPointAnimationType(Series series) {
            if (series is BarSideBySideSeries2D || series is RangeBarSeries2D)
                return typeof(Bar2DGrowUpAnimation);
            if (series is BarStackedSeries2D)
                return typeof(Bar2DDropInAnimation);
            if (series is PointSeries2D)
                return typeof(Marker2DSlideFromLeftAnimation);
            if (series is BubbleSeries2D)
                return typeof(Marker2DWidenAnimation);
            if (series is LineSeries2D || series is AreaSeries2D || series is RangeAreaSeries2D)
                return typeof(Marker2DFadeInAnimation);
            if (series is AreaStackedSeries2D)
                return typeof(AreaStacked2DFadeInAnimation);
            if (series is FinancialSeries2D)
                return typeof(Stock2DSlideFromTopAnimation);
            if (series is PieSeries2D)
                return typeof(Pie2DGrowUpAnimation);
            if (series is FunnelSeries2D)
                return typeof(Funnel2DWidenAnimation);
            if (series is CircularSeriesBase2D)
                return typeof(CircularMarkerSlideToCenterAnimation);
            if (series is BoxPlotSeries2D)
                return typeof(BoxPlot2DSlideFromTopAnimation);
            return null;
        }
        public static Type GetDefaultSeriesAnimationType(Series series) {
            if (series is LineSeries2D)
                return typeof(Line2DStretchFromNearAnimation);
            if (series is AreaSeries2D || series is RangeAreaSeries2D)
                return typeof(Area2DStretchFromNearAnimation);
            if (series is AreaStackedSeries2D)
                return typeof(Area2DDropFromFarAnimation);
            if (series is CircularAreaSeries2D)
                return typeof(CircularAreaZoomInAnimation);
            if (series is CircularLineSeries2D)
                return typeof(CircularLineZoomInAnimation);
            if (series is CircularRangeAreaSeries2D)
                return typeof(CircularAreaZoomInAnimation);
            if (series is BoxPlotSeries2D)
                return typeof(Line2DUnwindAnimation);
            return null;
        }
    }
}
