using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class SeriesPointMovingViewModel {
        public static SeriesPointMovingViewModel Create() {
            return ViewModelSource.Create(() => new SeriesPointMovingViewModel());
        }

        const double MinValue = 0;
        const double MaxRetailPriceValue = 500;
        const double MaxValue = 1000 * MaxRetailPriceValue;

        SeriePointMovingLegendViewModel[] legends;
        SeriePointMovingPaneViewModel[] panes = new SeriePointMovingPaneViewModel[] {
            SeriePointMovingPaneViewModel.Create(),
            SeriePointMovingPaneViewModel.Create()
        };
        SeriePointMovingAxisYViewModel[] axesY = new SeriePointMovingAxisYViewModel[] {
            SeriePointMovingAxisYViewModel.Create("Retail Price (USD)", "{V}", false, true, AxisAlignment.Near),
            SeriePointMovingAxisYViewModel.Create("Income (USD)", "{V:0,}M", true, false, AxisAlignment.Far),
            SeriePointMovingAxisYViewModel.Create("Units", "{V:0,}K", true, true, AxisAlignment.Near)
        };
        SeriesPointMovingSeriesViewModel currentSeriesItem = null;
        DraggableItem currentSeriesPointItem = null;
        ChartControl currentChartControl = null;
        ISetCursorService SetCursorService { get { return this.GetService<ISetCursorService>(); } }

        public virtual SeriePointMovingLegendViewModel[] Legends { get { return legends; } }
        public virtual SeriePointMovingPaneViewModel[] Panes { get { return panes; } }
        public virtual SeriePointMovingAxisYViewModel[] AxesY { get { return axesY; } }
        public virtual List<SeriesPointMovingSeriesViewModel> SeriesItems { get; protected set; }

        protected SeriesPointMovingViewModel() {
            CreateLegends();
            CreateSeriesItems();
            UpdateTotalIncome();
        }
        void CreateLegends() {
            legends = new SeriePointMovingLegendViewModel[] {
                SeriePointMovingLegendViewModel.Create(panes[0], HorizontalPosition.RightOutside, VerticalPosition.Top, null),
                SeriePointMovingLegendViewModel.Create(panes[1], HorizontalPosition.RightOutside, VerticalPosition.Top, null),
                SeriePointMovingLegendViewModel.Create(panes[1], HorizontalPosition.Center, VerticalPosition.BottomOutside,
                    new List<SeriesPointMovingCustomLegendItemViewModel>() { SeriesPointMovingCustomLegendItemViewModel.Create("Total income: ") })
            };
        }
        void CreateSeriesItems() {
            DraggableDataModel dataModel = DraggableDataModel.CreateModel();
            SeriesItems = new List<SeriesPointMovingSeriesViewModel>(5);
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Income", dataModel, legends[0], panes[0], axesY[1]));
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Retail Price", dataModel, legends[0], panes[0], axesY[0]));
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Stock", dataModel, legends[1], panes[1], axesY[2]));
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Demand", dataModel, legends[1], panes[1], axesY[2]));
            SeriesItems.Add(SeriesPointMovingSeriesViewModel.Create("Production", dataModel, legends[1], panes[1], axesY[2]));
        }
        void SetNewPointValue(double newValue) {
            if (newValue < MinValue || (newValue > MaxRetailPriceValue && currentSeriesItem.Name == "Retail Price") || newValue > MaxValue)
                return;
            if (newValue == 0) return;

            switch (currentSeriesItem.Name) {
                case "Production":
                    SeriesItems[0].DraggableItems.UpdateProduction(currentSeriesPointItem, newValue);
                    break;
                case "Demand":
                    SeriesItems[0].DraggableItems.UpdateDemand(currentSeriesPointItem, newValue);
                    break;
                case "Retail Price":
                    SeriesItems[0].DraggableItems.UpdateCost(currentSeriesPointItem, newValue);
                    break;
            }
            UpdateTotalIncome();
        }
        void SetHighlight(bool highlighted) {
            currentSeriesPointItem.IsHighlighted = highlighted;
            currentSeriesItem.IsHighlighted = highlighted;
        }
        void UpdateTotalIncome() {
            legends[2].CustomItems[0].Text = string.Format("Total income: ${0}K", SeriesItems[0].DraggableItems.TotalIncome.ToString("N0"));
        }
        public void OnMouseDown(MouseDownData mouseDownData) {
            if (mouseDownData != null && mouseDownData.Series.Name != "Income") {
                currentSeriesItem = mouseDownData.Series;
                currentSeriesPointItem = mouseDownData.CurrentItem;
                currentChartControl = mouseDownData.Chart;
                SetHighlight(true);
                currentChartControl.CaptureMouse();
            }
        }
        public void OnMouseMove(MouseMoveData moveData) {
            if (moveData == null)
                return;
            if (moveData.IsOnDraggablePoint)
                SetCursorService.SetCursor(Cursors.SizeNS);
            else if (currentSeriesPointItem == null)
                SetCursorService.SetCursor(Cursors.Arrow);
            if (currentSeriesPointItem != null && currentSeriesItem.Pane == moveData.Pane)
                SetNewPointValue(moveData.DiagramNumericalValue);
        }
        public void OnMouseUp() {
            if (currentSeriesItem != null) {
                SetHighlight(false);
                currentSeriesItem = null;
                currentSeriesPointItem = null;
                currentChartControl.ReleaseMouseCapture();
                currentChartControl = null;
            }
        }
    }
    public class SeriesPointMovingSeriesViewModel {
        public static SeriesPointMovingSeriesViewModel Create(string name, DraggableDataModel draggableItems, SeriePointMovingLegendViewModel legend,
                                                         SeriePointMovingPaneViewModel pane, SeriePointMovingAxisYViewModel axisY) {
            return ViewModelSource.Create(() => new SeriesPointMovingSeriesViewModel(name, draggableItems, legend, pane, axisY));
        }
        public virtual string Name { get; protected set; }
        public virtual SeriePointMovingLegendViewModel Legend { get; set; }
        public virtual SeriePointMovingPaneViewModel Pane { get; set; }
        public virtual SeriePointMovingAxisYViewModel AxisY { get; set; }
        public virtual DraggableDataModel DraggableItems { get; set; }
        public virtual bool IsHighlighted { get; set; }

        public SeriesPointMovingSeriesViewModel(string name, DraggableDataModel draggableItems, SeriePointMovingLegendViewModel legend,
                                           SeriePointMovingPaneViewModel pane, SeriePointMovingAxisYViewModel axisY) {
            Name = name;
            Legend = legend;
            Pane = pane;
            AxisY = axisY;
            DraggableItems = draggableItems;
            IsHighlighted = false;
        }
    }
    public class SeriePointMovingLegendViewModel {
        public static SeriePointMovingLegendViewModel Create(SeriePointMovingPaneViewModel dockTarget, HorizontalPosition horizontalAlignment,
                                                             VerticalPosition verticalAlignment, List<SeriesPointMovingCustomLegendItemViewModel> customItems) {
            return ViewModelSource.Create(() => new SeriePointMovingLegendViewModel(dockTarget, horizontalAlignment, verticalAlignment, customItems));
        }

        public virtual SeriePointMovingPaneViewModel DockTarget { get; protected set; }
        public virtual HorizontalPosition HorizontalPosition { get; protected set; }
        public virtual VerticalPosition VerticalPosition { get; protected set; }
        public virtual List<SeriesPointMovingCustomLegendItemViewModel> CustomItems { get; set; }

        protected SeriePointMovingLegendViewModel(SeriePointMovingPaneViewModel dockTarget, HorizontalPosition horizontalAlignment,
                                                  VerticalPosition verticalAlignment, List<SeriesPointMovingCustomLegendItemViewModel> customItems) {
            DockTarget = dockTarget;
            HorizontalPosition = horizontalAlignment;
            VerticalPosition = verticalAlignment;
            CustomItems = customItems;
        }
    }

    public class SeriesPointMovingCustomLegendItemViewModel {
        public static SeriesPointMovingCustomLegendItemViewModel Create(string text) {
            return ViewModelSource.Create(() => new SeriesPointMovingCustomLegendItemViewModel(text));
        }

        public virtual string Text { get; set; }
        protected SeriesPointMovingCustomLegendItemViewModel(string text) {
            Text = text;
        }
    }

    public class SeriePointMovingPaneViewModel {
        public static SeriePointMovingPaneViewModel Create() {
            return ViewModelSource.Create(() => new SeriePointMovingPaneViewModel());
        }
    }

    public class SeriePointMovingAxisYViewModel {
        public static SeriePointMovingAxisYViewModel Create(string title, string textPattern, bool alwaysShowZeroLevel, bool gridLinesVisible, AxisAlignment alignment) {
            return ViewModelSource.Create(() => new SeriePointMovingAxisYViewModel(title, textPattern, alwaysShowZeroLevel, gridLinesVisible, alignment));
        }
        public virtual string Title { get; protected set; }
        public virtual string TextPattern { get; protected set; }
        public virtual bool AlwaysShowZeroLevel { get; protected set; }
        public virtual bool GridLinesVisible { get; set; }
        public virtual AxisAlignment Alignment { get; protected set; }

        protected SeriePointMovingAxisYViewModel(string title, string textPattern, bool alwaysShowZeroLevel, bool gridLinesVisible, AxisAlignment alignment) {
            Title = title;
            TextPattern = textPattern;
            AlwaysShowZeroLevel = alwaysShowZeroLevel;
            GridLinesVisible = gridLinesVisible;
            Alignment = alignment;
        }
    }
    public class SeriesPointLabelViewModel {

    }

    public interface ISetCursorService {
        void SetCursor(Cursor cursor);
    }

    public class SetCursorService : ServiceBase, ISetCursorService {
        public void SetCursor(Cursor cursor) {
            ((SeriesPointMovingDemo)AssociatedObject).Cursor = cursor;
        }
    }
}
