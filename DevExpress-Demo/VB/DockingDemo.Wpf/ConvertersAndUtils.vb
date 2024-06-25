Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.Docking.Base
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace DockingDemo

    Public Class UniversalContainer(Of T)

        Public Property Name As String

        Public Property DisplayName As String

        Public Property Value As T

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return If(TypeOf obj Is UniversalContainer(Of T), Equals(Value, CType(obj, UniversalContainer(Of T)).Value), False)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Value.GetHashCode()
        End Function
    End Class

    Public Class AutoHideExpandModeContainer
        Inherits UniversalContainer(Of AutoHideExpandMode)

    End Class

    Public Class AutoHideModeContainer
        Inherits UniversalContainer(Of AutoHideMode)

    End Class

    Public Class TileLayoutContainer
        Inherits UniversalContainer(Of TileLayout)

    End Class

    Public Class UniversalContainerConverter(Of T)
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is T, GetContainer(CType(value, T)), Nothing)
        End Function

        Protected Overridable Function GetContainer(ByVal value As T) As Object
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return CType(value, UniversalContainer(Of T)).Value
        End Function
#End Region
    End Class

    Public Class UniversalContainerConverter2(Of TValue, TContainer As UniversalContainer(Of TValue))
        Inherits UniversalContainerConverter(Of TValue)

        Protected Overrides Function GetContainer(ByVal value As TValue) As Object
            Dim container As TContainer = Activator.CreateInstance(Of TContainer)()
            container.Value = value
            Return container
        End Function
    End Class

    Public Class AutoHideExpandModeContainerConverter
        Inherits UniversalContainerConverter2(Of AutoHideExpandMode, AutoHideExpandModeContainer)

    End Class

    Public Class AutoHideModeContainerConverter
        Inherits UniversalContainerConverter2(Of AutoHideMode, AutoHideModeContainer)

    End Class

    Public Class TileLayoutContainerConverter
        Inherits UniversalContainerConverter(Of TileLayout)

    End Class

    Public Class SourceConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Enum TileLayout
        [Default]
        Layout3x2
        Layout3x3
        Layout4x2
    End Enum

    Friend Module TileLayoutExtension

        Public Function GetRowCount(ByVal layout As TileLayout) As Integer
            Return If((layout = TileLayout.Layout3x3), 3, 2)
        End Function

        Public Function GetColumnsCount(ByVal layout As TileLayout) As Integer
            Return If((layout = TileLayout.Layout4x2), 4, 3)
        End Function
    End Module

    Friend Module TileImageHelper

        Private images As IDictionary(Of Integer, Image) = New Dictionary(Of Integer, Image)()

        Public Function GetImage(ByVal index As Integer) As Image
            Dim result As Image = Nothing
            If Not images.TryGetValue(index, result) Then
                Dim uri As Uri = New Uri(String.Format("/Images/TileImages/{0:D2}.jpg", index), UriKind.Relative)
                result = New Image()
                result.Source = New BitmapImage(uri)
                images.Add(index, result)
            End If

            Return result
        End Function
    End Module

    Public Module FontSizes

        Private itemsCore As Double() = New Double() {3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0, 32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0, 80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0}

        Public ReadOnly Property Items As Double()
            Get
                Return itemsCore
            End Get
        End Property
    End Module

    Public Module FontFamilies

        Private fontNamesField As List(Of String)

        Public ReadOnly Property FontNames As List(Of String)
            Get
                If fontNamesField Is Nothing Then fontNamesField = GetSystemFontNames()
                Return fontNamesField
            End Get
        End Property

        Private Function GetSystemFontNames() As List(Of String)
            Dim systemFontNames As List(Of String) = New List(Of String)()
            For Each fam As FontFamily In Fonts.SystemFontFamilies
                systemFontNames.Add(fam.Source)
            Next

            Return systemFontNames
        End Function
    End Module

    Public Class Employee
        Inherits DependencyObject

        Public Property TitleOfCourtesy As String

        Public Property Title As String

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Notes As String

        Public Property Address As String

        Public Property Country As String

        Public Property City As String

        Public Property HomePhone As String

        Public Property Region As String

        Public Property PostalCode As String

        Public Property BirthDate As String

        Public Property HireDate As String

        Public Property Extension As String

        Public Property Photo As ImageSource

        Public Shared Function CreateSampleData() As List(Of Employee)
            Dim employees As List(Of Employee) = New List(Of Employee)()
            Dim employee As Employee = New Employee()
            employee.TitleOfCourtesy = "Dr."
            employee.Title = "Sales Representative"
            employee.FirstName = "Andrew"
            employee.LastName = "Fuller"
            employee.Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."
            employee.Address = "908 W. Capital Way"
            employee.Country = "USA"
            employee.City = "Tacoma"
            employee.HomePhone = "(206) 555-9482"
            employee.Region = "WA"
            employee.PostalCode = "98401"
            employee.Photo = New BitmapImage(New Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person1.png", UriKind.RelativeOrAbsolute))
            employees.Add(employee)
            employee = New Employee()
            employee.FirstName = "Janet"
            employee.LastName = "Leverling"
            employee.Notes = "Janet has a BS degree in chemistry from Boston College (1984).  She has also completed a certificate program in food retailing management.  Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992."
            employee.City = "Kirkland"
            employee.Photo = New BitmapImage(New Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person2.png", UriKind.RelativeOrAbsolute))
            employees.Add(employee)
            employee = New Employee()
            employee.TitleOfCourtesy = "Dr."
            employee.LastName = "Evil"
            employee.Photo = New BitmapImage(New Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person3.png", UriKind.RelativeOrAbsolute))
            employees.Add(employee)
            Return employees
        End Function
    End Class

    Public Class CommandsModel

        Private _Close As ICommand, _MDIStyle As ICommand, _Cascade As ICommand, _TileHorizontal As ICommand, _TileVertical As ICommand, _ArrangeIcons As ICommand

        Public Property Close As ICommand
            Get
                Return _Close
            End Get

            Private Set(ByVal value As ICommand)
                _Close = value
            End Set
        End Property

        Public Property MDIStyle As ICommand
            Get
                Return _MDIStyle
            End Get

            Private Set(ByVal value As ICommand)
                _MDIStyle = value
            End Set
        End Property

        Public Property Cascade As ICommand
            Get
                Return _Cascade
            End Get

            Private Set(ByVal value As ICommand)
                _Cascade = value
            End Set
        End Property

        Public Property TileHorizontal As ICommand
            Get
                Return _TileHorizontal
            End Get

            Private Set(ByVal value As ICommand)
                _TileHorizontal = value
            End Set
        End Property

        Public Property TileVertical As ICommand
            Get
                Return _TileVertical
            End Get

            Private Set(ByVal value As ICommand)
                _TileVertical = value
            End Set
        End Property

        Public Property ArrangeIcons As ICommand
            Get
                Return _ArrangeIcons
            End Get

            Private Set(ByVal value As ICommand)
                _ArrangeIcons = value
            End Set
        End Property

        Public Sub New()
            Close = DockControllerCommand.Close
            MDIStyle = MDIControllerCommand.ChangeMDIStyle
            Cascade = MDIControllerCommand.Cascade
            TileHorizontal = MDIControllerCommand.TileHorizontal
            TileVertical = MDIControllerCommand.TileVertical
            ArrangeIcons = MDIControllerCommand.ArrangeIcons
        End Sub

        Public Property Target As Object
    End Class
End Namespace
