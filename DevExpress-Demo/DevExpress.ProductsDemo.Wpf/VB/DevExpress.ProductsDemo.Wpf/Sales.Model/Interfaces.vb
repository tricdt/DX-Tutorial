Imports System
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Namespace DevExpress.SalesDemo.Model

    Public Interface IDataProvider

        Function GetTotalSalesByRange(ByVal start As Date, ByVal [end] As Date) As SalesGroup

        Function GetSales(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)

        Function GetSalesByChannel(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)

        Function GetSalesByProduct(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)

        Function GetSalesByRegion(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)

        Function GetSalesBySector(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)

    End Interface

    Public Module DataProviderExtensions

        <Extension()>
        Public Function GetTotalSalesByRange(ByVal dataProvider As IDataProvider, ByVal range As DateTimeRange) As SalesGroup
            Verify(dataProvider)
            Return dataProvider.GetTotalSalesByRange(range.Start, range.End)
        End Function

        <Extension()>
        Public Function GetSales(ByVal dataProvider As IDataProvider, ByVal range As DateTimeRange, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)
            Verify(dataProvider)
            Return dataProvider.GetSales(range.Start, range.End, period)
        End Function

        <Extension()>
        Public Function GetSalesByChannel(ByVal dataProvider As IDataProvider, ByVal range As DateTimeRange, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)
            Verify(dataProvider)
            Return dataProvider.GetSalesByChannel(range.Start, range.End, period)
        End Function

        <Extension()>
        Public Function GetSalesByProduct(ByVal dataProvider As IDataProvider, ByVal range As DateTimeRange, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)
            Verify(dataProvider)
            Return dataProvider.GetSalesByProduct(range.Start, range.End, period)
        End Function

        <Extension()>
        Public Function GetSalesByRegion(ByVal dataProvider As IDataProvider, ByVal range As DateTimeRange, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)
            Verify(dataProvider)
            Return dataProvider.GetSalesByRegion(range.Start, range.End, period)
        End Function

        <Extension()>
        Public Function GetSalesBySector(ByVal dataProvider As IDataProvider, ByVal range As DateTimeRange, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup)
            Verify(dataProvider)
            Return dataProvider.GetSalesBySector(range.Start, range.End, period)
        End Function

        Private Sub Verify(ByVal dataProvider As IDataProvider)
            If dataProvider Is Nothing Then Throw New ArgumentNullException()
        End Sub
    End Module

    Public Interface IDataGenerator

        Event GenerationStart As ProgressEventHandler

        Event GenerationProgress As ProgressEventHandler

        Event GenerationComplete As ProgressEventHandler

    End Interface

    Public Delegate Sub ProgressEventHandler(ByVal sender As Object, ByVal e As ProgressEventArgs)

    Public Class ProgressEventArgs

        Private _Progress As Integer

        Public Sub New(ByVal progress As Integer)
            Me.Progress = progress
        End Sub

        Public Property Progress As Integer
            Get
                Return _Progress
            End Get

            Private Set(ByVal value As Integer)
                _Progress = value
            End Set
        End Property
    End Class
End Namespace
