Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace NavigationDemo

    Public Class CollapseModeViewModel

        Private _FiltersData As ReadOnlyCollection(Of NavigationDemo.FilterGroup)

        Public Property FiltersData As ReadOnlyCollection(Of FilterGroup)
            Get
                Return _FiltersData
            End Get

            Private Set(ByVal value As ReadOnlyCollection(Of FilterGroup))
                _FiltersData = value
            End Set
        End Property

        Public Sub New()
            FiltersData = New ReadOnlyCollection(Of FilterGroup)(CreateFilters())
        End Sub

        Private Function CreateFilters() As FilterGroup()
            Dim experienceItems = New FilterItem() {New FilterItem("> 24 years", "(DateDiffYear([HireDate], Today()) > 24)"), New FilterItem("> 19 and <= 24 years", "(DateDiffYear([HireDate], Today()) > 19 AND DateDiffYear([HireDate], Today()) <= 24)"), New FilterItem("> 14 and <= 19 years", "(DateDiffYear([HireDate], Today()) > 14 AND DateDiffYear([HireDate], Today()) <= 19)"), New FilterItem("<= 14 years", "(DateDiffYear([HireDate], Today()) <= 14)")}
            Dim factory = ViewModelSource.Factory(Function(ByVal name As String, ByVal filterString As String, ByVal showInCollapsedMode As Boolean, ByVal icon As Byte()) New FilterItem(name, filterString, showInCollapsedMode, icon))
            Dim regionItems = EmployeesWithPhotoData.DataSource.[Select](Function(x) x.CountryRegionName).Distinct().[Select](Function(x) factory(x, String.Format("([CountryRegionName] = '{0}')", x), False, GetFlag(x))).ToArray()
            Enumerable.ToList(Enumerable.Take(regionItems, 4)).ForEach(Sub(x) x.ShowInCollapsedMode = True)
            Return New FilterGroup() {New FilterGroup("Experience", experienceItems), New FilterGroup("Region", regionItems.ToArray())}
        End Function

        Private Shared Function GetFlag(ByVal country As String) As Byte()
            Dim countryInfo = CountriesData.DataSource.FirstOrDefault(Function(x) Equals(x.ActualName, country))
            Return If(countryInfo Is Nothing, Nothing, countryInfo.Flag)
        End Function
    End Class

    Public Class FilterGroup

        Private _Name As String, _FilterItems As List(Of NavigationDemo.FilterItem)

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property FilterItems As List(Of FilterItem)
            Get
                Return _FilterItems
            End Get

            Private Set(ByVal value As List(Of FilterItem))
                _FilterItems = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal filterItems As FilterItem())
            Me.Name = name
            Me.FilterItems = filterItems.ToList()
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class FilterItem

        Private _Name As String, _FilterString As String, _Icon As Object

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property FilterString As String
            Get
                Return _FilterString
            End Get

            Private Set(ByVal value As String)
                _FilterString = value
            End Set
        End Property

        Public Property ShowInCollapsedMode As Boolean

        <BindableProperty>
        Public Overridable Property CanSelect As Boolean

        Public Property Icon As Object
            Get
                Return _Icon
            End Get

            Private Set(ByVal value As Object)
                _Icon = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal filterString As String, ByVal Optional showInCollapsedMode As Boolean = False, ByVal Optional icon As Byte() = Nothing)
            Me.Name = name
            Me.FilterString = filterString
            Me.ShowInCollapsedMode = showInCollapsedMode
            Me.Icon = icon
            CanSelect = True
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
