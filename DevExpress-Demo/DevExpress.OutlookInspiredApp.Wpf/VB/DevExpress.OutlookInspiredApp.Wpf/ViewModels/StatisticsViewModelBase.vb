Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map
Imports System.Collections.Generic
Imports System
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public MustInherit Class StatisticsViewModelBase

        Private unitOfWork As IDevAVDbUnitOfWork

        Protected Sub New()
            unitOfWork = GetUnitOfWorkFactory().CreateUnitOfWork()
            FilterType = Period.Lifetime
        End Sub

        Public Event FilterTypeChanged As EventHandler(Of PeriodEventArgs)

        Public Property EntityId As Long

        <BindableProperty(OnPropertyChangedMethodName:="UpdateStatistics")>
        Public Overridable Property SelectedAddress As CityViewModel

        Public Sub ThisMonthFilter()
            FilterType = Period.ThisMonth
        End Sub

        Public Function CanThisMonthFilter() As Boolean
            Return Not Equals(FilterType, Period.ThisMonth)
        End Function

        Public Sub YTDFilter()
            FilterType = Period.ThisYear
        End Sub

        Public Function CanYTDFilter() As Boolean
            Return Not Equals(FilterType, Period.ThisYear)
        End Function

        Public Sub LifetimeFilter()
            FilterType = Period.Lifetime
        End Sub

        Public Function CanLifetimeFilter() As Boolean
            Return Not Equals(FilterType, Period.Lifetime)
        End Function

        Public Overridable Property FilterType As Period

        Protected Sub OnFilterTypeChanged()
            RaiseEvent FilterTypeChanged(Me, New PeriodEventArgs(FilterType))
            UpdateStatistics()
        End Sub

        Public Overridable Property ActualStatistics As IEnumerable(Of SalesViewModel)

        Protected Sub UpdateStatistics()
            If SelectedAddress Is Nothing Then Return
            ActualStatistics = GetActualStatistics(unitOfWork)
        End Sub

        Protected MustOverride Function GetActualStatistics(ByVal unitOfWork As IDevAVDbUnitOfWork) As IEnumerable(Of SalesViewModel)
    End Class

    Public Class SalesViewModel

        Public Sub New(ByVal name As String, ByVal total As Decimal)
            Me.Name = name
            Me.Total = total
        End Sub

        Public Property Name As String

        Public Property Total As Decimal
    End Class

    Public Class CityViewModel

        Private crestField As Lazy(Of Crest)

        Private stateField As Lazy(Of State)

        Public Shared Function Create(ByVal address As Address, ByVal unitOfWork As IDevAVDbUnitOfWork) As CityViewModel
            Return ViewModelSource.Create(Function() New CityViewModel(address, unitOfWork))
        End Function

        Protected Sub New(ByVal address As Address, ByVal unitOfWork As IDevAVDbUnitOfWork)
            Me.Address = address
            Location = New GeoPoint(address.Latitude, address.Longitude)
            stateField = New Lazy(Of State)(Function() unitOfWork.States.First(Function(s) s.ShortName = address.State))
            crestField = New Lazy(Of Crest)(Function() unitOfWork.Crests.First(Function(c) String.Equals(c.CityName, address.City)))
        End Sub

        Public ReadOnly Property Crest As Crest
            Get
                Return crestField.Value
            End Get
        End Property

        Public ReadOnly Property State As State
            Get
                Return stateField.Value
            End Get
        End Property

        Public Property Address As Address

        Public Property Location As GeoPoint

        Public Overridable Property IsVisible As Boolean

        Public Overridable Property Sales As IEnumerable(Of SalesViewModel)

        Protected Sub OnSalesChanged()
            IsVisible = Not(Sales Is Nothing OrElse Sales.Count() = 0)
        End Sub
    End Class

    Public Class PeriodEventArgs
        Inherits EventArgs

        Public Sub New(ByVal period As Period)
            Me.Period = period
        End Sub

        Public Property Period As Period
    End Class
End Namespace
