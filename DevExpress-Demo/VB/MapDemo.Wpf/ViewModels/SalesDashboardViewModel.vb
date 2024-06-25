Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Xml.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Class SalesDashboardViewModel

        Public Overridable Property GaugeVisibility As Boolean

        Public Overridable Property ActualStatistics As ObservableCollection(Of ProductGroupViewModel)

        Public Overridable Property Shops As ObservableCollection(Of ShopInfoViewModel)

        Public Overridable Property SalesDescription As String

        <BindableProperty(True, OnPropertyChangedMethodName:="SelectedShopPropertyChanged")>
        Public Overridable Property SelectedShop As ShopInfoViewModel

        Public Overridable Property MinSalesLevel As Double

        Public Overridable Property MaxSalesLevel As Double

        Public Overridable ReadOnly Property ChartControlService As IChartControlService
            Get
                Return Nothing
            End Get
        End Property

        Protected Sub SelectedShopPropertyChanged()
            If SelectedShop IsNot Nothing Then
                UpdateStatistics(SelectedShop)
            Else
                UpdateTotalStatistics()
            End If
        End Sub

        Public Sub New()
            Shops = New ObservableCollection(Of ShopInfoViewModel)()
            ActualStatistics = New ObservableCollection(Of ProductGroupViewModel)()
            LoadDataFromXML()
            UpdateMinMaxSales()
            UpdateTotalStatistics()
        End Sub

        Private Sub LoadDataFromXML()
            Dim productGroupNames As List(Of String) = New List(Of String)()
            Dim document As XDocument = LoadXmlFromResources("/Data/Sales.xml")
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("Sales").Elements()
                    Dim shopName As String = element.Element("ShopName").Value
                    Dim shopAddress As String = element.Element("ShopAddr").Value
                    Dim shopPhone As String = element.Element("ShopPhone").Value
                    Dim shopFax As String = element.Element("ShopFax").Value
                    Dim sw = New Stopwatch()
                    sw.Start()
                    Dim info As ShopInfoViewModel = ViewModelSource.Create(Function() New ShopInfoViewModel(shopName, shopAddress, shopPhone, shopFax))
                    sw.Stop()
                    For Each statElement As XElement In element.Element("ShopStatistics").Elements()
                        Dim groupName As String = statElement.Element("ProductsGroupName").Value
                        If Not productGroupNames.Contains(groupName) Then productGroupNames.Add(groupName)
                        Dim sales As Double = Convert.ToDouble(statElement.Element("ProductGroupSales").Value, CultureInfo.InvariantCulture)
                        info.AddProductGroup(groupName, sales)
                    Next

                    Dim geoPoint As GeoPoint = New GeoPoint(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture))
                    info.ShopLocation = geoPoint
                    Shops.Add(info)
                Next
            End If

            For Each groupName As String In productGroupNames
                ActualStatistics.Add(ViewModelSource.Create(Function() New ProductGroupViewModel(0.0, groupName)))
            Next

            UpdateTotalStatistics()
        End Sub

        Private Sub UpdateStatistics(ByVal info As ShopInfoViewModel)
            For Each productGroupInfo As ProductGroupViewModel In ActualStatistics
                productGroupInfo.Value = info.GetSalesByProductGroup(productGroupInfo.Name)
            Next

            SalesDescription = "Last Month Sales: " & info.Name
            If ChartControlService IsNot Nothing Then
                ChartControlService.UpdateData()
                ChartControlService.Animate()
            End If

            GaugeVisibility = True
        End Sub

        Private Sub UpdateMinMaxSales()
            Dim minSales As Double = Shops(0).Sales
            Dim maxSales As Double = Shops(0).Sales
            For Each info As ShopInfoViewModel In Shops
                If info.Sales > maxSales Then maxSales = info.Sales
                If info.Sales < minSales Then minSales = info.Sales
            Next

            MinSalesLevel = minSales - 10000
            MaxSalesLevel = maxSales + 10000
        End Sub

        Public Sub UpdateTotalStatistics()
            For Each info As ProductGroupViewModel In ActualStatistics
                info.Value = 0.0
                For Each shopInfo As ShopInfoViewModel In Shops
                    info.Value += shopInfo.GetSalesByProductGroup(info.Name)
                Next
            Next

            GaugeVisibility = False
            SalesDescription = "Last Month Sales: All Shops"
        End Sub
    End Class
End Namespace
