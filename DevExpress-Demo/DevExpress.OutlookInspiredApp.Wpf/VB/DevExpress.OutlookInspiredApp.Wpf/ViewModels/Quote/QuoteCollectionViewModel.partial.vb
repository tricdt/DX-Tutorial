Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.ViewModels

    Partial Class QuoteCollectionViewModel
        Implements ISupportFiltering(Of Quote)

        Const NumberOfAverageQuotes As Integer = 300

        Public Overridable Property AverageQuotes As List(Of Quote)

        Public Function GetSummaryOpportunities(ByVal start As Date, ByVal [end] As Date) As IList(Of QuoteSummaryItem)
            Return QueriesHelper.GetSummaryOpportunities(CreateReadOnlyRepository().GetFilteredEntities(FilterExpression).Where(Function(x) x.Date >= start AndAlso x.Date <= [end])).ToList()
        End Function

        Protected Overrides Sub OnIsLoadingChanged()
            MyBase.OnIsLoadingChanged()
            If Not IsLoading Then
                UpdateAverageQuotes()
            End If
        End Sub

        Private Sub UpdateAverageQuotes()
            AverageQuotes = QueriesHelper.GetAverageQuotes(CreateReadOnlyRepository().GetFilteredEntities(FilterExpression), NumberOfAverageQuotes)
        End Sub

        Public Sub CreateCustomFilter()
            Call Messenger.Default.Send(New CreateCustomFilterMessage(Of Quote)())
        End Sub

        Public Overrides Sub [New]()
            ShowProductEditForm()
        End Sub

        Public Overrides Sub Edit(ByVal projectionEntity As QuoteInfo)
            ShowProductEditForm()
        End Sub

        Private Sub ShowProductEditForm()
            MessageBoxService.ShowMessage("You can easily create custom edit forms using the 40+ controls that ship as part of the DevExpress Data Editors Library. To see what you can build, activate the Employees module.", "Edit Opportunities", MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub

        Public Sub ShowMap(ByVal start As Date, ByVal [end] As Date)
            Dim mapViewModel As QuoteMapViewModel = ViewModelSource.Create(Function() New QuoteMapViewModel() With {.FilterExpression = Function(x) x.Date > start AndAlso x.Date < [end]})
            Dim document = GetRequiredService(Of IDocumentManagerService)().CreateDocument("QuoteMapView", mapViewModel, Nothing, Me)
            Log("OutlookInspiredApp: View Opportunities Map")
            document.Title = "DevAV - Opportunities"
            document.Show()
        End Sub

        Public Overrides Sub Delete(ByVal projectionEntity As QuoteInfo)
            MessageBoxService.ShowMessage("To ensure data integrity, the Opportunities module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Opportunity", MessageButton.OK)
        End Sub

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of Func(Of Quote, Boolean)) Implements ISupportFiltering(Of Quote).FilterExpression
            Get
                Return FilterExpression
            End Get

            Set(ByVal value As Expression(Of Func(Of Quote, Boolean)))
                FilterExpression = value
            End Set
        End Property
#End Region
    End Class
End Namespace
