using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Map;

namespace MapDemo {
    [POCOViewModel]
    public class PuzzleViewModel {
        PuzzleLayoutGenerator puzzleGenerator;
        bool isLoading;
        bool isEditing;

        public virtual IList<MapItem> Items { get; protected set; }
        public IList<MapItem> SolvedItems { get; private set; }
        public ICommand GenerateItemsCommand { get; private set; }
        public virtual bool AllowSaveActions { get; protected set; }
        public virtual ICommand TranslateItemsCommand { get; set; }
        public virtual ICommand ClearSavedActionsCommand { get; set; }
        public virtual ObservableCollection<MapItem> ActiveItems { get; set; }
        public virtual Func<CoordPoint, Point> CoordPointToScreenPoint { get; set; }
        public virtual ICommand ZoomToRegion { get; set; }
        public virtual MapItem ActiveItem { get; set; }
        [DependsOnProperties("ActiveItem")]
        public string Name { get { return ActiveItem != null ? ActiveItem.Attributes["NAME_LONG"].Value.ToString() : string.Empty; } }
        [DependsOnProperties("ActiveItem")]
        public string Capital { get { return ActiveItem != null ? ActiveItem.Attributes["CAPITAL"].Value.ToString() : string.Empty; } }

        public PuzzleViewModel() {
            GenerateItemsCommand = new DelegateCommand(GenerateItems);
            SolvedItems = new ObservableCollection<MapItem>();
        }

        public static GeoPoint GetItemLocation(ISupportCoordPoints item) {
            return (GeoPoint)item.Points[0];
        }

        void GenerateItems() {
            ActiveItem = null;
            SolvedItems.Clear();
            ZoomToRegion.Execute(new object[] { new GeoPoint(15, -180), new GeoPoint(-62, -45), 0.05 });
            this.isLoading = true;
            AllowSaveActions = false;
            IEnumerable<MapPathInfo> pathInfos = this.puzzleGenerator.GeneratePathInfos();
            ObservableCollection<MapItem> items = new ObservableCollection<MapItem>();
            foreach(MapPathInfo pathInfo in pathInfos) {
                MapPath copiedPath = (MapPath)pathInfo.Path.Clone();
                copiedPath.Attributes.Add(new MapItemAttribute() { Name = "RealLocation", Type = typeof(GeoPoint), Value = pathInfo.RealLocation });
                Vector delta = CoordPointToScreenPoint(pathInfo.GameCenter) - CoordPointToScreenPoint(pathInfo.RealCenter);
                ActiveItems = new ObservableCollection<MapItem>() { copiedPath };
                TranslateItemsCommand.Execute(delta);
                items.Add(copiedPath);
            }
            Items = items;
            AllowSaveActions = true;
            this.isLoading = false;
        }
        double CalculateDistance(Point point1, Point point2) {
            double dx = point1.X - point2.X;
            double dy = point1.Y - point2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        void ShowWinMessage() {
            MessageBoxResult result = ThemedMessageBox.Show("You won!", "Well done! All countries are on their places! Restart game?", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
                GenerateItems();
        }
        void MoveItemToSolveLayer(MapPath mapPath) {
            Items.Remove(mapPath);
            SolvedItems.Add(mapPath);
            ActiveItem = mapPath;
            ActiveItems.Clear();
            if(Items.Count == 0)
                ShowWinMessage();
        }
        public void OnItemsLoaded(IEnumerable<MapItem> items) {
            this.puzzleGenerator = new PuzzleLayoutGenerator(items);
            GenerateItems();
        }
        public void OnItemEdited(IEnumerable<MapItem> items) {
            if(this.isLoading || this.isEditing)
                return;
            foreach(MapPath item in items) {
                GeoPoint realLocation = (GeoPoint)item.Attributes["RealLocation"].Value;
                GeoPoint currentLocation = GetItemLocation(item);
                Point desiredLocation = CoordPointToScreenPoint(realLocation);
                Point actualLocation = CoordPointToScreenPoint(currentLocation);
                if(CalculateDistance(desiredLocation, actualLocation) < 20) {
                    item.CanMove = false;
                    Vector delta = desiredLocation - actualLocation;
                    ActiveItems = new ObservableCollection<MapItem>() { item };
                    this.isEditing = true;
                    TranslateItemsCommand.Execute(delta);
                    this.isEditing = false;
                    MoveItemToSolveLayer(item);
                    ClearSavedActionsCommand.Execute(null);
                }
            }
        }
    }
    public class MapPathInfo {
        readonly MapPath path;
        readonly GeoPoint realLocation;
        readonly GeoPoint realCenter;
        readonly GeoPoint gameCenter;
        readonly string name;
        readonly string captial;

        public MapPath Path { get { return path; } }
        public GeoPoint RealLocation { get { return realLocation; } }
        public GeoPoint RealCenter { get { return realCenter; } }
        public GeoPoint GameCenter { get { return gameCenter; } }
        public string Name { get { return name; } }
        public string Capital { get { return captial; } }

        public MapPathInfo(MapPath path, GeoPoint realLocation, GeoPoint realCenter, GeoPoint gameCenter) {
            this.path = path;
            this.realLocation = realLocation;
            this.realCenter = realCenter;
            this.gameCenter = gameCenter;
            this.name = path.Attributes["NAME_LONG"].Value.ToString();
            this.captial = path.Attributes["CAPITAL"].Value.ToString();
        }
    }
}
