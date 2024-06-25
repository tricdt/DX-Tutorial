Imports DevExpress.Mvvm.UI
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Xpf.Map
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks

Namespace DevExpress.DevAV.ViewModels

    Public Interface IMapRouteService

        Function CalculateRouteAsync(ByVal waypoints As System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Map.RouteWaypoint), ByVal unit As DevExpress.Xpf.Map.DistanceMeasureUnit, ByVal optimization As DevExpress.Xpf.Map.BingRouteOptimization, ByVal mode As DevExpress.Xpf.Map.BingTravelMode) As Task(Of DevExpress.Xpf.Map.RouteCalculationResult)

    End Interface
End Namespace

Namespace DevExpress.DevAV

    Public Class MapRouteService
        Inherits DevExpress.Mvvm.UI.ServiceBase
        Implements DevExpress.DevAV.ViewModels.IMapRouteService

        Private ReadOnly Property Layer As InformationLayer
            Get
                Return CType(Me.AssociatedObject, DevExpress.Xpf.Map.InformationLayer)
            End Get
        End Property

        Private ReadOnly Property Provider As BingRouteDataProvider
            Get
                Return CType(Me.Layer.DataProvider, DevExpress.Xpf.Map.BingRouteDataProvider)
            End Get
        End Property

        Private taskSource As System.Threading.Tasks.TaskCompletionSource(Of DevExpress.Xpf.Map.RouteCalculationResult)

        Public Function CalculateRouteAsync(ByVal waypoints As System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Map.RouteWaypoint), ByVal unit As DevExpress.Xpf.Map.DistanceMeasureUnit, ByVal optimization As DevExpress.Xpf.Map.BingRouteOptimization, ByVal mode As DevExpress.Xpf.Map.BingTravelMode) As Task(Of DevExpress.Xpf.Map.RouteCalculationResult) Implements Global.DevExpress.DevAV.ViewModels.IMapRouteService.CalculateRouteAsync
            Me.taskSource = New System.Threading.Tasks.TaskCompletionSource(Of DevExpress.Xpf.Map.RouteCalculationResult)()
            Me.Provider.RouteOptions = New DevExpress.Xpf.Map.BingRouteOptions With {.Mode = mode, .DistanceUnit = unit, .RouteOptimization = optimization}
            Me.Provider.CalculateRoute(waypoints.ToList())
            AddHandler Me.Provider.RouteCalculated, AddressOf Me.Provider_RouteCalculated
            Return Me.taskSource.Task
        End Function

        Private Sub Provider_RouteCalculated(ByVal sender As Object, ByVal e As DevExpress.Xpf.Map.BingRouteCalculatedEventArgs)
            RemoveHandler Me.Provider.RouteCalculated, AddressOf Me.Provider_RouteCalculated
            If e.Cancelled Then
                Me.taskSource.SetCanceled()
            ElseIf e.[Error] IsNot Nothing Then
                Me.taskSource.SetException(e.[Error])
            Else
                Me.taskSource.SetResult(e.CalculationResult)
            End If
        End Sub
    End Class
End Namespace
