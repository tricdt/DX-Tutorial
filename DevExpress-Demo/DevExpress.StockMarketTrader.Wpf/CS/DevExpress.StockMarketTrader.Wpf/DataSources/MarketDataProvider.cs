using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using DevExpress.StockMarketTrader.Data;
using DevExpress.StockMarketTrader.Wpf.Data;
using DevExpress.Xpf.DemoBase.Helpers;

namespace DevExpress.StockMarketTrader.Wpf.DataSources {
    public class MarketDataProvider {
        const int ordersCount = 2;
        public const double PriceDeviation = 0.05;

        readonly List<SymbolData> symbolsSource;
        readonly Dictionary<SymbolData, TradingDataSource> symbolDataSources;
        readonly Random rnd;

        List<OpenOrderData> openOrdersDataSource;
        List<OrderHistoryData> ordersHistoryDataSource;
        List<TradeHistoryData> tradesDataSource;
        
        public List<OpenOrderData> OpenOrdersDataSource { get { return openOrdersDataSource; } }
        public List<OrderHistoryData> OrdersHistoryDataSource { get { return ordersHistoryDataSource; } }
        public List<TradeHistoryData> TradesDataSource { get { return tradesDataSource; } }
        public virtual List<SymbolData> SymbolsSource { get { return symbolsSource; } }

        public MarketDataProvider() {
            symbolsSource = new List<SymbolData>();
            symbolDataSources = new Dictionary<SymbolData, TradingDataSource>();
            rnd = new Random();
            LoadSymbolInfo();
            InitOrders();
        }

        void LoadSymbolInfo() {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(DemoHelper.GetPath("DevExpress.StockMarketTrader.Wpf.Resources.", typeof(MarketDataProvider).Assembly) + "Symbols.xml");
            if (stream != null) {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                var nodes = doc.LastChild != null ? doc.LastChild.SelectNodes("//Symbol") : null;
                if (nodes != null) {
                    foreach (XmlNode node in nodes)
                        symbolsSource.Add(SymbolData.Create(node.Attributes["Name"].Value,
                                                            double.Parse(node.Attributes["BasePrice"].Value),
                                                            node.Attributes["Group"].Value,
                                                            rnd.Next(2000000, 2500000)));
                }
            }
            stream.Close();
        }
        void InitOrders() {
            openOrdersDataSource = OrdersGenerator.GenerateOpenOrders(symbolsSource, ordersCount);
            ordersHistoryDataSource = OrdersGenerator.GenerateOrdersHistory(symbolsSource, ordersCount, openOrdersDataSource);
            tradesDataSource = OrdersGenerator.GenerateTradeHistory(ordersHistoryDataSource);
        }
        public double GenerateSymbolCurrentPrice(SymbolData symbol) {
            return symbol.BasePrice * (1 + PriceDeviation * (2 * rnd.NextDouble() - 1));
        }
        public double GenerateSymbolChange24(SymbolData symbol) {
            return symbol.BasePrice * PriceDeviation * (2 * rnd.NextDouble() - 1);
        }
        public double GenerateSymbolVolumeChanges() {
            return rnd.Next(1, 10);
        }

        public TradingDataSource GetDataSource(SymbolData symbol) {
            TradingDataSource dataSource = null;
            symbolDataSources.TryGetValue(symbol, out dataSource);
            return dataSource;
        }
        public void OnSymbolChanged(SymbolData oldSymbol, SymbolData newSymbol, double currentPrice) {
            if (oldSymbol != null) {
                oldSymbol.DecreaseWatchCount();
                if (oldSymbol.WatchCount == 0)                    
                    symbolDataSources.Remove(oldSymbol);                
            }
            if (newSymbol != null) {
                newSymbol.IncreaseWatchCount();
                if (newSymbol.WatchCount == 1) {
                    TradingDataSource tradingSource = new TradingDataSource(currentPrice);
                    symbolDataSources.Add(newSymbol, tradingSource);
                }
            }
        }
        public void UpdateData() {
            foreach (TradingDataSource dataSource in symbolDataSources.Values)
                dataSource.UpdateMarketState();

            foreach (SymbolData symbol in symbolsSource) {
                TradingDataSource dataSource = GetDataSource(symbol);
                if (dataSource != null) {
                    symbol.CurrentPrice = dataSource.CurrentPrice;
                    symbol.Change24 = dataSource.Change24;
                    symbol.Volume24 = dataSource.Volume24;
                }
                else {
                    symbol.CurrentPrice = GenerateSymbolCurrentPrice(symbol);
                    symbol.Change24 = GenerateSymbolChange24(symbol);
                    symbol.Volume24 += GenerateSymbolVolumeChanges();
                }
            }
        }

    }
}
