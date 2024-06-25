Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model
Imports System

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class PieBarRangeViewModel
        Inherits DataViewModel

        Private _ModuleType As String

        Public Shared Function Create(ByVal moduleType As String) As PieBarRangeViewModel
            Return ViewModelSource.Create(Function() New PieBarRangeViewModel(moduleType))
        End Function

        Protected Property ModuleType As String
            Get
                Return _ModuleType
            End Get

            Private Set(ByVal value As String)
                _ModuleType = value
            End Set
        End Property

        Protected Sub New()
            Me.New(Modules.Regions)
        End Sub

        Protected Sub New(ByVal moduleType As String)
            Me.ModuleType = moduleType
            BarViewModel = BarViewModel.Create()
            PieViewModel = PieViewModel.Create()
            RangeViewModel = RangeViewModel.Create()
            AddHandler RangeViewModel.RangeChanged, AddressOf OnRangeViewModelRangeChanged
            AddHandler RangeViewModel.SelectedRangeChanged, AddressOf OnRangeViewModelSelectedRangeChanged
            RequestDataForRange()
            RequestDataForPieAndBar()
        End Sub

        Public Overridable Property RangeViewModel As RangeViewModel

        Public Overridable Property BarViewModel As BarViewModel

        Public Overridable Property PieViewModel As PieViewModel

        Private Sub OnRangeViewModelRangeChanged(ByVal sender As Object, ByVal e As EventArgs)
            RequestDataForRange()
        End Sub

        Private Sub OnRangeViewModelSelectedRangeChanged(ByVal sender As Object, ByVal e As EventArgs)
            RequestDataForPieAndBar()
        End Sub

        Private Sub RequestDataForRange()
            If RangeViewModel.RangeStart Is Nothing OrElse RangeViewModel.RangeEnd Is Nothing OrElse RangeViewModel.RangeStart >= RangeViewModel.RangeEnd Then Return
            RequestData("RangeViewModel", Function(x) x.GetSales(RangeViewModel.RangeStart.Value, RangeViewModel.RangeEnd.Value.AddDays(1), GroupingPeriod.Day), Sub(x) RangeViewModel.LoadData(x))
        End Sub

        Private Sub RequestDataForPieAndBar()
            Dim range = RangeViewModel.GetSelectedRange()
            If range Is Nothing Then Return
            RequestData("Data", Function(x)
                If Equals(ModuleType, Modules.Regions) Then
                    Return x.GetSalesByRegion(range.Value, GroupingPeriod.All)
                ElseIf Equals(ModuleType, Sectors) Then
                    Return x.GetSalesBySector(range.Value, GroupingPeriod.All)
                ElseIf Equals(ModuleType, Products) Then
                    Return x.GetSalesByProduct(range.Value, GroupingPeriod.All)
                ElseIf Equals(ModuleType, Channels) Then
                    Return x.GetSalesByChannel(range.Value, GroupingPeriod.All)
                End If

                Throw New NotImplementedException()
            End Function, Sub(x)
                BarViewModel.LoadData(x)
                PieViewModel.LoadData(x)
            End Sub)
        End Sub
    End Class
End Namespace
