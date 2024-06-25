using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Threading;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.StockMarketTrader.Data;
using DevExpress.StockMarketTrader.Wpf.DataSources;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;

namespace DevExpress.StockMarketTrader.Wpf.ViewModel {
    public class MainViewModel {
        public const int Tick = 500;

        readonly DispatcherTimer updateTimer;
        readonly MarketDataProvider dataProvider;

        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        public virtual List<SymbolData> SymbolsSource { get { return dataProvider.SymbolsSource; } }
        public virtual TransitionEffect TransitionEffect { get; set; }
        public virtual DateTime CurrentTime { get; protected set; }
        public virtual InformationPanelViewModel InformationPanelModel { get; protected set; }
        public virtual int ThemeIndex { get; set; }

        public MainViewModel() {
            dataProvider = new MarketDataProvider();
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InformationPanelModel = InformationPanelViewModel.Create();
            ThemeIndex = 1;
            InitTimer();
        }
        void InitTimer() {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(UpdateOnTimer);
            updateTimer.Start();
        }
        void UpdateOnTimer(object sender, EventArgs e) {
            dataProvider.UpdateData();
            if (DocumentManagerService != null) {
                foreach (var document in DocumentManagerService.Documents) {
                    TabViewModel tabModel = document.Content as TabViewModel;
                    if (tabModel != null)
                        tabModel.UpdateData();
                }
                if (DocumentManagerService.ActiveDocument != null) {
                    TradingDataSource tradingSource = dataProvider.GetDataSource(((TabViewModel)DocumentManagerService.ActiveDocument.Content).Symbol);
                    InformationPanelModel.UpdateData(tradingSource.PreviousPrice,
                                                             tradingSource.CurrentPrice,
                                                             tradingSource.PriceDayAgo,
                                                             tradingSource.Change24,
                                                             tradingSource.High24,
                                                             tradingSource.Low24,
                                                             tradingSource.Volume24);
                }
            }
            CurrentTime = DateTime.Now;
        }

        protected void OnThemeIndexChanged() {
            switch (ThemeIndex) {
                case 0: ApplicationThemeHelper.ApplicationThemeName = Theme.VS2019DarkName; break;
                case 1: ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName; break;
            }
        }

        public void CreateTabView(object sender, EventArgs e) {
            SymbolData symbol = SymbolsSource[0];
            SelectedItemChangedEventArgs itemChangedArgs = e as SelectedItemChangedEventArgs;
            if (itemChangedArgs != null) {
                if (itemChangedArgs.NewItem is SymbolData) {
                    symbol = (SymbolData)itemChangedArgs.NewItem;
                    Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() => ((GridControl)sender).SelectedItem = null));
                }
                else
                    symbol = null;
            }
            if (symbol != null) {
                IDocument doc = DocumentManagerService.CreateDocument("TabView", TabViewModel.Create(dataProvider, symbol));
                doc.DestroyOnClose = true;
                doc.Show();
            }
        }
        public void TableViewLoaded(object sender, EventArgs e) {
            TableView view = sender as TableView;
            if (view.SearchControl != null)
                view.SearchControl.Focus();
        }
        public void AboutMenu() {
            var application = System.Windows.Application.Current;
            Xpf.Core.Native.AboutHelper.ShowAbout(new[] { Utils.About.ProductKind.DXperienceWPF }, "Stock Market Trader", application == null ? null : application.MainWindow);

        }
        public void DevExpressOnTheWebMenu() {
            Process.Start(new ProcessStartInfo { FileName = "http://www.devexpress.com", UseShellExecute = true });
        }
    }
}
