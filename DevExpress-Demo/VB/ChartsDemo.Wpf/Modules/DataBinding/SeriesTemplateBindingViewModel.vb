Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class SeriesTemplateBindingViewModel

        Public Shared Function Create() As SeriesTemplateBindingViewModel
            Return ViewModelSource.Create(Function() New SeriesTemplateBindingViewModel())
        End Function

        Public ReadOnly Property AnimationService As IChartAnimationService
            Get
                Return GetService(Of IChartAnimationService)()
            End Get
        End Property

        Public Overridable Property SeriesDataMembers As ObservableCollection(Of String)

        Public Overridable Property ArgumentDataMembers As ObservableCollection(Of String)

        Public Overridable Property ValueDataMembers As ObservableCollection(Of String)

        Public Overridable Property SelectedSeriesDataMember As String

        Public Overridable Property SelectedArgumentDataMember As String

        Public Overridable Property SelectedValueDataMember As String

        Public Overridable Property SelectedSummaryFunction As SummaryFunction

        Public Overridable Property AxisYTitle As String

        Public Overridable Property ChartTitle As String

        Public Overridable Property ActualSummaryFunction As SummaryFunctionBase

        Public ReadOnly Property DevAVSalesData As List(Of DevAVSaleItem)
            Get
                Return New DevAVSales().GetProductsByMonths()
            End Get
        End Property

        Protected Sub New()
            SeriesDataMembers = New ObservableCollection(Of String) From {"Company", "Product", "Month"}
            SelectedSeriesDataMember = "Company"
            ArgumentDataMembers = New ObservableCollection(Of String) From {"Product", "Month"}
            SelectedArgumentDataMember = "Product"
            ValueDataMembers = New ObservableCollection(Of String) From {"Income", "Revenue"}
            SelectedValueDataMember = "Revenue"
            SelectedSummaryFunction = SummaryFunction.Sum
        End Sub

        Protected Sub OnSelectedSeriesDataMemberChanged(ByVal oldValue As String)
            If ArgumentDataMembers Is Nothing Then Return
            ArgumentDataMembers.Remove(SelectedSeriesDataMember)
            ArgumentDataMembers.Add(oldValue)
            If AnimationService IsNot Nothing Then AnimationService.Animate()
        End Sub

        Protected Sub OnSelectedArgumentDataMemberChanged(ByVal oldValue As String)
            If SeriesDataMembers Is Nothing Then Return
            SeriesDataMembers.Remove(SelectedArgumentDataMember)
            SeriesDataMembers.Add(oldValue)
            If AnimationService IsNot Nothing Then AnimationService.Animate()
        End Sub

        Protected Sub OnSelectedSummaryFunctionChanged()
            UpdateSummaryFunction()
        End Sub

        Private Sub UpdateSummaryFunction()
            Select Case SelectedSummaryFunction
                Case SummaryFunction.Average
                    ChartTitle = "Average Order Amount"
                    AxisYTitle = "Amount (USD)"
                    ActualSummaryFunction = New AverageSummaryFunction() With {.ValueDataMember = SelectedValueDataMember}
                Case SummaryFunction.Custom_StdDev
                    ChartTitle = "Standard Deviation from Average Order Amount"
                    AxisYTitle = "Deviation (USD)"
                    ActualSummaryFunction = New StdDevSummaryFunction() With {.ValueDataMember = SelectedValueDataMember}
                Case SummaryFunction.Maximum
                    ChartTitle = "Maximal Order Amount"
                    AxisYTitle = "Amount (USD)"
                    ActualSummaryFunction = New MaxSummaryFunction() With {.ValueDataMember = SelectedValueDataMember}
                Case SummaryFunction.Minimum
                    ChartTitle = "Minimal Order Amount"
                    AxisYTitle = "Amount (USD)"
                    ActualSummaryFunction = New MinSummaryFunction() With {.ValueDataMember = SelectedValueDataMember}
                Case SummaryFunction.Sum
                    ChartTitle = "Sales Volume"
                    AxisYTitle = "Volume (USD)"
                    ActualSummaryFunction = New SumSummaryFunction() With {.ValueDataMember = SelectedValueDataMember}
                Case Else
                    Throw New NotSupportedException("Only the Average, Custom, Max, Min and Sum Summary Functions are supported.")
            End Select

            If AnimationService IsNot Nothing Then AnimationService.Animate()
        End Sub

        Protected Sub OnSelectedValueDataMemberChanged()
            UpdateSummaryFunction()
        End Sub
    End Class

    Public Enum SummaryFunction
        Average
        Maximum
        Minimum
        Sum
        Custom_StdDev
    End Enum

    Public Class StdDevSummaryFunction
        Inherits DataMemberSummaryFunction

        Public Overrides ReadOnly Property Name As String
            Get
                Return "STDDEV"
            End Get
        End Property

        Protected Overrides Function CreateSeriesPoints(ByVal series As Series, ByVal argument As Object, ByVal functionArguments As String(), ByVal values As DataSourceValues(), ByVal colors As Object()) As SeriesPoint()
            Dim amount As Double() = New Double(values.Length - 1) {}
            Dim sum As Double = 0.0
            For i As Integer = 0 To values.Length - 1
                amount(i) = Convert.ToDouble(values(i)(functionArguments(0)))
                sum += amount(i)
            Next

            Dim averageAmount As Double = sum / values.Length
            Dim standardDeviationSquareSum As Double = 0.0
            For i As Integer = 0 To values.Length - 1
                Dim deviation As Double = amount(i) - averageAmount
                standardDeviationSquareSum += deviation * deviation
            Next

            Dim seriesPoint As SeriesPoint = New SeriesPoint() With {.Argument = argument.ToString(), .Value = Math.Round(Math.Sqrt(standardDeviationSquareSum / values.Length), 2)}
            Return New SeriesPoint() {seriesPoint}
        End Function
    End Class

    Public Class BindingProxy
        Inherits Freezable

        Public Shared ReadOnly DataProperty As DependencyProperty = DependencyProperty.Register("Data", GetType(Object), GetType(BindingProxy), New UIPropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Data As Object
            Get
                Return CObj(GetValue(DataProperty))
            End Get

            Set(ByVal value As Object)
                SetValue(DataProperty, value)
            End Set
        End Property

        Protected Overrides Function CreateInstanceCore() As Freezable
            Return New BindingProxy()
        End Function
    End Class
End Namespace
