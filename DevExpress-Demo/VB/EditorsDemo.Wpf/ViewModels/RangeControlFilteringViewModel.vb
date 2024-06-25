Imports DevExpress.Mvvm.DataAnnotations
Imports EditorsDemo.SCService
Imports System
Imports System.Linq
Imports DevExpress.DemoData
Imports DevExpress.Mvvm

Namespace EditorsDemo

    Public Class RangeControlFilteringViewModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Private _Source As Object, _StartDate As DateTime, _EndDate As DateTime, _VisibleStartDate As DateTime, _VisibleEndDate As DateTime

        Public Sub New()
            Me.EndDate = New System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1)
            Me.StartDate = Me.EndDate.AddMonths(-6)
            Me.Source = DevExpress.DemoData.NWindDataProvider.InvoicesUpToDate.Where(Function(x) x.OrderDate >= Me.StartDate AndAlso x.OrderDate <= Me.EndDate).ToList()
            Me.VisibleStartDate = Me.StartDate
            Me.SelectedStart = Me.VisibleStartDate
            Me.VisibleEndDate = Me.EndDate
            Me.SelectedEnd = Me.VisibleEndDate
        End Sub

        Public Property Source As Object
            Get
                Return _Source
            End Get

            Private Set(ByVal value As Object)
                _Source = value
            End Set
        End Property

        Public Property StartDate As DateTime
            Get
                Return _StartDate
            End Get

            Private Set(ByVal value As DateTime)
                _StartDate = value
            End Set
        End Property

        Public Property EndDate As DateTime
            Get
                Return _EndDate
            End Get

            Private Set(ByVal value As DateTime)
                _EndDate = value
            End Set
        End Property

        Public Property VisibleStartDate As DateTime
            Get
                Return _VisibleStartDate
            End Get

            Private Set(ByVal value As DateTime)
                _VisibleStartDate = value
            End Set
        End Property

        Public Property VisibleEndDate As DateTime
            Get
                Return _VisibleEndDate
            End Get

            Private Set(ByVal value As DateTime)
                _VisibleEndDate = value
            End Set
        End Property

        Public Property SelectedStart As DateTime
            Get
                Return GetProperty(Function() Me.SelectedStart)
            End Get

            Set(ByVal value As DateTime)
                SetProperty(Function() Me.SelectedStart, value)
                Me.UpdateFilter()
            End Set
        End Property

        Public Property SelectedEnd As DateTime
            Get
                Return GetProperty(Function() Me.SelectedEnd)
            End Get

            Set(ByVal value As DateTime)
                SetProperty(Function() Me.SelectedEnd, value)
                Me.UpdateFilter()
            End Set
        End Property

        Public Property FilterString As String
            Get
                Return GetProperty(Function() Me.FilterString)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() Me.FilterString, value)
            End Set
        End Property

        Private Sub UpdateFilter()
            Me.FilterString = System.[String].Format("([OrderDate] >= #{0}# AND [OrderDate] < #{1}#)", Me.SelectedStart, Me.SelectedEnd)
        End Sub
    End Class
End Namespace
