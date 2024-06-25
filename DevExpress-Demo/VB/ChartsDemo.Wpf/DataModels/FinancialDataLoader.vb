Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Xml.Linq

Namespace ChartsDemo

    Public Class FinancialDataLoader

        Private ReadOnly positiveDynamic As ImageSource = New BitmapImage(New Uri("/ChartsDemo;component/Images/ArrowUp.png", UriKind.Relative))

        Private ReadOnly negativeDynamic As ImageSource = New BitmapImage(New Uri("/ChartsDemo;component/Images/ArrowDown.png", UriKind.Relative))

        Private ReadOnly zeroDynamic As ImageSource = New BitmapImage(New Uri("/ChartsDemo;component/Images/ZeroDynamic.png", UriKind.Relative))

        Private Function GetStockDynamic(ByVal previousPointValue As Decimal, ByVal currentPointValue As Decimal) As StockDynamic
            If previousPointValue < currentPointValue Then
                Return New StockDynamic(New SolidColorBrush(Color.FromArgb(255, 63, 171, 0)), positiveDynamic)
            ElseIf previousPointValue > currentPointValue Then
                Return New StockDynamic(New SolidColorBrush(Color.FromArgb(255, 213, 50, 35)), negativeDynamic)
            Else
                Return New StockDynamic(New SolidColorBrush(Color.FromArgb(255, 161, 161, 161)), zeroDynamic)
            End If
        End Function

        Private Function ReadDataPointFromXML(ByVal element As XElement) As StockDataPoint
            Dim point As StockDataPoint = New StockDataPoint() With {.TradeDate = Date.ParseExact(element.Element("Date").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture), .Open = Convert.ToDecimal(element.Element("Open").Value, CultureInfo.InvariantCulture), .Close = Convert.ToDecimal(element.Element("Close").Value, CultureInfo.InvariantCulture), .Low = Convert.ToDecimal(element.Element("Low").Value, CultureInfo.InvariantCulture), .High = Convert.ToDecimal(element.Element("High").Value, CultureInfo.InvariantCulture), .ToolTipData = New ToolTipStockData()}
            point.ToolTipData.Owner = point
            Return point
        End Function

        Public Function GetGoogleStockData() As List(Of StockDataPoint)
            Dim document As XDocument = LoadXmlFromResources("/Data/GoogleStockData.xml")
            Dim result As List(Of StockDataPoint) = New List(Of StockDataPoint)()
            If document IsNot Nothing Then
                Dim elements As IEnumerable(Of XElement) = document.Element("StockPrices").Elements("StockPrice").Reverse()
                Dim previousPoint As StockDataPoint = ReadDataPointFromXML(elements.ElementAt(0))
                For Each element As XElement In elements
                    Dim point As StockDataPoint = ReadDataPointFromXML(element)
                    point.ToolTipData.OpenDynamic = GetStockDynamic(previousPoint.Open, point.Open).ImageSource
                    point.ToolTipData.CloseDynamic = GetStockDynamic(previousPoint.Close, point.Close).ImageSource
                    point.ToolTipData.HighDynamic = GetStockDynamic(previousPoint.High, point.High).ImageSource
                    point.ToolTipData.LowDynamic = GetStockDynamic(previousPoint.Low, point.Low).ImageSource
                    point.ToolTipData.OpenFontBrush = GetStockDynamic(previousPoint.Open, point.Open).Brush
                    point.ToolTipData.CloseFontBrush = GetStockDynamic(previousPoint.Close, point.Close).Brush
                    point.ToolTipData.HighFontBrush = GetStockDynamic(previousPoint.High, point.High).Brush
                    point.ToolTipData.LowFontBrush = GetStockDynamic(previousPoint.Low, point.Low).Brush
                    result.Add(point)
                    previousPoint = point
                Next
            End If

            Return result
        End Function
    End Class

    Public Class StockDataPoint

        Public Property ToolTipData As ToolTipStockData

        Public Property TradeDate As Date

        Public Property High As Decimal

        Public Property Low As Decimal

        Public Property Open As Decimal

        Public Property Close As Decimal
    End Class

    Public Class StockDynamic

        Private _Brush As Brush, _ImageSource As ImageSource

        Public Property Brush As Brush
            Get
                Return _Brush
            End Get

            Private Set(ByVal value As Brush)
                _Brush = value
            End Set
        End Property

        Public Property ImageSource As ImageSource
            Get
                Return _ImageSource
            End Get

            Private Set(ByVal value As ImageSource)
                _ImageSource = value
            End Set
        End Property

        Public Sub New(ByVal brush As Brush, ByVal imageSource As ImageSource)
            Me.Brush = brush
            Me.ImageSource = imageSource
        End Sub
    End Class

    Public Class ToolTipStockData

        Public Property Owner As StockDataPoint

        Public Property HighDynamic As ImageSource

        Public Property LowDynamic As ImageSource

        Public Property OpenDynamic As ImageSource

        Public Property CloseDynamic As ImageSource

        Public Property HighFontBrush As Brush

        Public Property LowFontBrush As Brush

        Public Property OpenFontBrush As Brush

        Public Property CloseFontBrush As Brush
    End Class
End Namespace
