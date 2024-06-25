Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization
Imports System.Collections
Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.IO
Imports System.ComponentModel
Imports System.Windows.Data
Imports System.Globalization
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Grid.TreeList
Imports System.Windows.Media

Namespace TreeListDemo

    <XmlRoot("Countries")>
    Public Class CountriesData
        Inherits List(Of Country)

        Private Shared dataSourceField As IList = Nothing

        Public Shared ReadOnly Property DataSource As IList
            Get
                If dataSourceField IsNot Nothing Then Return dataSourceField
                Dim assembly As Assembly = GetType(CountriesData).Assembly
                Dim stream As Stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Data/", assembly) & "Countries.xml", True)
                Dim s As XmlSerializer = New XmlSerializer(GetType(CountriesData), New XmlRootAttribute("Countries"))
                dataSourceField = CType(s.Deserialize(stream), IList)
                Return dataSourceField
            End Get
        End Property
    End Class

    Public Class Country

        Public Property Name As String

        Public Property Flag As Byte()
    End Class

    Public Class SpaceObjectData
        Inherits List(Of SpaceObjects)

        Public Shared ReadOnly Property DataSource As IList(Of SpaceObjects)
            Get
                Dim assembly As Assembly = GetType(SpaceObjectData).Assembly
                Dim stream As Stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Data/", assembly) & "SpaceObjects.xml", True)
                Dim s As XmlSerializer = New XmlSerializer(GetType(SpaceObjectData), New XmlRootAttribute("NewDataSet"))
                Return CType(s.Deserialize(stream), List(Of SpaceObjects))
            End Get
        End Property
    End Class

    Public Class SpaceObjects

        Public Property ObjectId As Integer

        Public Property ParentId As Integer

        Public Property Name As String

        Public Property WikiPage As String

        Public Property ImageData As Byte()

        Public Property ImageHint As String

        Public Property MeanRadiusInKM As Single

        Public Property MeanRadiusByEarth As String

        Public Property Volume10pow9KM3 As String

        Public Property VolumeRByEarth As Single

        Public Property Mass10pow21kg As Single

        Public Property MassByEarth As Single

        Public Property DensitygBycm3 As Single

        Public Property SurfaceGravitymBys2 As Single

        Public Property SurfaceGravityByEarth As Single

        Public Property TypeOfObject As String

        Public Property IsExpanded As Boolean
    End Class

    Public Class SalesData

        Public Sub New(ByVal id As Integer, ByVal regionId As Integer, ByVal region As String, ByVal marchSales As Decimal, ByVal septemberSales As Decimal, ByVal marchSalesPrev As Decimal, ByVal septermberSalesPrev As Decimal, ByVal marketShare As Double)
            Me.ID = id
            Me.RegionID = regionId
            Me.Region = region
            Me.MarchSales = marchSales
            Me.SeptemberSales = septemberSales
            Me.MarchSalesPrev = marchSalesPrev
            SeptemberSalesPrev = septermberSalesPrev
            Me.MarketShare = marketShare
        End Sub

        Public Property ID As Integer

        Public Property RegionID As Integer

        Public Property Region As String

        Public Property MarchSales As Decimal

        Public Property SeptemberSales As Decimal

        Public Property MarchSalesPrev As Decimal

        Public Property SeptemberSalesPrev As Decimal

        Public Property MarketShare As Double
    End Class

    Public Class SalesDataGenerator

        Public Shared Function CreateData() As List(Of SalesData)
            Dim sales As List(Of SalesData) = New List(Of SalesData)()
            sales.Add(New SalesData(0, -1, "Western Europe", 30540, 33000, 32220, 35500, .70))
            sales.Add(New SalesData(1, 0, "Austria", 22000, 28000, 26120, 28500, .92))
            sales.Add(New SalesData(2, 0, "Belgium", 13000, 9640, 14500, 11200, .16))
            sales.Add(New SalesData(3, 0, "Denmark", 21000, 18100, 17120, 15500, .56))
            sales.Add(New SalesData(4, 0, "Finland", 17000, 17420, 18120, 19200, .44))
            sales.Add(New SalesData(5, 0, "France", 23020, 27000, 20120, 24200, .51))
            sales.Add(New SalesData(6, 0, "Germany", 30540, 33000, 32220, 35500, .93))
            sales.Add(New SalesData(7, 0, "Greece", 15600, 13200, 11500, 11000, .11))
            sales.Add(New SalesData(8, 0, "Ireland", 9530, 10939, 12620, 12990, .34))
            sales.Add(New SalesData(9, 0, "Italy", 17299, 19321, 14120, 15945, .22))
            sales.Add(New SalesData(10, 0, "Liechtenstein", 1650, 1820, 1410, 1710, .25))
            sales.Add(New SalesData(11, 0, "Luxembourg", 1920, 1710, 2010, 1620, .18))
            sales.Add(New SalesData(12, 0, "Monaco", 1280, 1350, 1100, 1400, .6))
            sales.Add(New SalesData(13, 0, "Netherlands", 8902, 9214, 7400, 9600, .85))
            sales.Add(New SalesData(14, 0, "Norway", 5400, 7310, 5200, 6880, .7))
            sales.Add(New SalesData(15, 0, "Portugal", 9220, 4271, 4100, 3880, .5))
            sales.Add(New SalesData(16, 0, "Spain", 12900, 10300, 14300, 14900, .82))
            sales.Add(New SalesData(17, 0, "Switzerland", 9323, 10730, 7244, 9400, .14))
            sales.Add(New SalesData(18, 0, "United Kingdom", 14580, 13967, 15200, 16900, .91))
            sales.Add(New SalesData(19, -1, "Eastern Europe", 22500, 24580, 21225, 22698, .62))
            sales.Add(New SalesData(20, 19, "Albania", 2400, 2725, 2140, 2610, .42))
            sales.Add(New SalesData(21, 19, "Belarus", 7315, 18800, 8240, 17480, .34))
            sales.Add(New SalesData(22, 19, "Bosnia and Herzegovina", 6030, 8010, 6120, 7900, .29))
            sales.Add(New SalesData(23, 19, "Bulgaria", 6300, 2821, 5200, 4880, .8))
            sales.Add(New SalesData(24, 19, "Croatia", 4200, 3890, 3880, 4430, .29))
            sales.Add(New SalesData(25, 19, "Czech Republic", 19500, 15340, 16230, 14980, .13))
            sales.Add(New SalesData(26, 19, "Estonia", 3100, 2950, 3300, 3050, .55))
            sales.Add(New SalesData(27, 19, "Hungary", 13495, 13900, 10245, 9560, .14))
            sales.Add(New SalesData(28, 19, "Latvia", 3250, 3400, 3330, 3650, .7))
            sales.Add(New SalesData(29, 19, "Lithuania", 8250, 6400, 8330, 6350, .65))
            sales.Add(New SalesData(30, 19, "Macedonia", 1900, 2100, 1750, 2050, .45))
            sales.Add(New SalesData(31, 19, "Poland", 8930, 9440, 12200, 12150, .52))
            sales.Add(New SalesData(32, 19, "Romania", 4900, 5100, 5241, 6284, .30))
            sales.Add(New SalesData(33, 19, "Russia", 22500, 24580, 21225, 22698, .85))
            sales.Add(New SalesData(34, 19, "Serbia", 12730, 12420, 12935, 12800, .8))
            sales.Add(New SalesData(35, 19, "Slovakia", 11420, 11365, 12225, 12520, .74))
            sales.Add(New SalesData(36, 19, "Slovenia", 7300, 6950, 7280, 7010, .51))
            sales.Add(New SalesData(37, -1, "North America", 31400, 32800, 30300, 31940, .84))
            sales.Add(New SalesData(38, 37, "Antigua and Barbuda", 950, 840, 800, 825, .68))
            sales.Add(New SalesData(39, 37, "Bahamas", 2850, 2740, 3000, 2925, .75))
            sales.Add(New SalesData(40, 37, "Barbados", 2100, 2225, 2250, 2480, .83))
            sales.Add(New SalesData(41, 37, "Belize", 3200, 3225, 3250, 3480, .77))
            sales.Add(New SalesData(42, 37, "Canada", 25390, 27000, 5200, 6880, .64))
            sales.Add(New SalesData(43, 37, "Costa Rica", 4100, 4350, 4050, 4150, .80))
            sales.Add(New SalesData(44, 37, "Cuba", 5600, 5880, 5410, 8900, .84))
            sales.Add(New SalesData(45, 37, "Dominica", 900, 840, 910, 900, .9))
            sales.Add(New SalesData(46, 37, "Dominican Republic", 5450, 5800, 5600, 5200, .2))
            sales.Add(New SalesData(47, 37, "Mexico", 12400, 12650, 12700, 12850, .65))
            sales.Add(New SalesData(48, 37, "USA", 31400, 32800, 30300, 31940, .87))
            sales.Add(New SalesData(49, -1, "South America", 16380, 17590, 15400, 16680, .32))
            sales.Add(New SalesData(50, 49, "Argentina", 16380, 17590, 15400, 16680, .88))
            sales.Add(New SalesData(51, 49, "Bolivia", 4380, 5590, 5400, 5680, .92))
            sales.Add(New SalesData(52, 49, "Brazil", 4560, 9480, 3900, 6100, .10))
            sales.Add(New SalesData(53, 49, "Chile", 7500, 7680, 8100, 8555, .7))
            sales.Add(New SalesData(54, 49, "Colombia", 24400, 25780, 25300, 26750, .72))
            sales.Add(New SalesData(55, 49, "Peru", 19300, 18980, 19400, 19550, .81))
            sales.Add(New SalesData(56, 49, "Venezuela", 6300, 6980, 6400, 6550, .2))
            sales.Add(New SalesData(57, -1, "Asia", 20388, 22547, 22500, 25756, .52))
            sales.Add(New SalesData(58, 57, "India", 4642, 5320, 4200, 6470, .44))
            sales.Add(New SalesData(59, 57, "Japan", 9457, 12859, 8300, 8733, .70))
            sales.Add(New SalesData(60, 57, "China", 20388, 22547, 22500, 25756, .82))
            Return sales
        End Function
    End Class

    <XmlRoot("EmployeeTasks")>
    Public Class EmployeedTasks
        Inherits List(Of EmployeeTask)

        Private Shared dataSourceField As IList(Of EmployeeTask) = Nothing

        Private Shared editableDataSourceField As ObservableCollection(Of EmployeeTask)

        Private Shared employeeNamesField As List(Of String)

        Public Shared ReadOnly Property DataSource As IList(Of EmployeeTask)
            Get
                If dataSourceField IsNot Nothing Then Return dataSourceField
                dataSourceField = GetEmployeeTasks()
                Return dataSourceField
            End Get
        End Property

        Private Shared Function GetEmployeeTasks() As IList(Of EmployeeTask)
            Dim assembly As Assembly = GetType(EmployeedTasks).Assembly
            Dim stream As Stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Data/", assembly) & "EmployeeTasks.xml", True)
            Dim s As XmlSerializer = New XmlSerializer(GetType(EmployeedTasks), New XmlRootAttribute("EmployeeTasks"))
            Return CType(s.Deserialize(stream), IList(Of EmployeeTask))
        End Function

        Public Shared ReadOnly Property EditableDataSource As ObservableCollection(Of EmployeeTask)
            Get
                If editableDataSourceField IsNot Nothing Then Return editableDataSourceField
                editableDataSourceField = New ObservableCollection(Of EmployeeTask)(GetEmployeeTasks().Take(28))
                Return editableDataSourceField
            End Get
        End Property

        Public Shared ReadOnly Property EmployeeNames As List(Of String)
            Get
                If employeeNamesField IsNot Nothing Then Return employeeNamesField
                employeeNamesField = DataSource.[Select](Function(e) e.Employee).ToList()
                Return employeeNamesField
            End Get
        End Property
    End Class

    Public Class EmployeeTask
        Implements IEditableObject

        Public Sub New()
            Status = -1
            Priority = Status
        End Sub

        Public Property ID As Integer

        Public Property ParentID As Integer

        Public Property Name As String

        Public Property Description As String

        Public Property Employee As String

        Public Property StartDate As Date

        Public Property DueDate As Date

        Public Property Priority As Integer

        Public ReadOnly Property HasDescription As Boolean
            Get
                Return Not String.IsNullOrEmpty(Description)
            End Get
        End Property

        Public ReadOnly Property IsCompleted As Boolean
            Get
                Return Status = 100
            End Get
        End Property

        Public Property Status As Integer

        Public Property IsUpdated As Boolean

        Private Sub BeginEdit() Implements IEditableObject.BeginEdit
            IsUpdated = False
        End Sub

        Private Sub CancelEdit() Implements IEditableObject.CancelEdit
            IsUpdated = False
        End Sub

        Private Sub EndEdit() Implements IEditableObject.EndEdit
            IsUpdated = True
        End Sub
    End Class

    Public Class PriorityIconConverter
        Implements IValueConverter

        Private Shared PriorityImages As List(Of ImageSource)

        Shared Sub New()
            PriorityImages = New List(Of ImageSource)()
            PriorityImages.Add(GetSvgImage("Flag_Yellow"))
            PriorityImages.Add(GetSvgImage("Flag_Red"))
        End Sub

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim status As Integer = CInt(value)
            If status < 0 Then Return Nothing
            Return PriorityImages(status)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class EmployeeTaskImageSelector
        Inherits DevExpress.Xpf.Grid.TreeListNodeImageSelector

        Private Shared TaskImages As List(Of ImageSource)

        Shared Sub New()
            TaskImages = New List(Of ImageSource)()
            TaskImages.Add(GetSvgImage("Task"))
            TaskImages.Add(GetSvgImage("Note"))
        End Sub

        Public Overrides Function [Select](ByVal rowData As TreeListRowData) As ImageSource
            If rowData.Level = 0 Then Return TaskImages(0)
            Return TaskImages(1)
        End Function
    End Class
End Namespace
