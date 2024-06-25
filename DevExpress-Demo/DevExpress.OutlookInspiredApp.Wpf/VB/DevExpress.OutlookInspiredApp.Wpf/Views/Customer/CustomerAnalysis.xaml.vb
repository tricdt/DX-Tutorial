Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Spreadsheet

Namespace DevExpress.DevAV.Views.Customer

    Public Partial Class CustomerAnalysis
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            SubscribeEvents()
        End Sub

        Private Sub SubscribeEvents()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Me.spreadsheetControl.DocumentLoaded, AddressOf Me.OnDocumentLoaded
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CType(Me.splashScreenService, Mvvm.ISplashScreenService).ShowSplashScreen(String.Empty)
            LoadTemplate()
        End Sub

        Private Sub LoadTemplate()
            Using stream = GetAnalysisTemplate(AnalysisTemplate.CustomerSales)
                Me.spreadsheetControl.LoadDocument(stream)
            End Using
        End Sub

        Private Sub OnDocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            LoadData()
            CType(Me.splashScreenService, Mvvm.ISplashScreenService).HideSplashScreen()
        End Sub

        Private Sub LoadData()
            Dim ViewModel As CustomerAnalysisViewModel = CType(DataContext, CustomerAnalysisViewModel)
            Me.spreadsheetControl.Document.BeginUpdate()
            Dim salesReportWorksheet = Me.spreadsheetControl.Document.Worksheets("Sales Report")
            Dim salesReportItems = ViewModel.GetSalesReport().ToList()
            Dim frCustomers = salesReportItems.[Select](Function(i) i.CustomerName).Distinct().OrderBy(Function(i) i).ToList()
            salesReportWorksheet.Import(frCustomers, 14, 1, True)
            For Each item As CustomersAnalysis.Item In salesReportItems
                Dim rowOffset As Integer = frCustomers.IndexOf(item.CustomerName)
                Dim columnOffset As Integer = MonthOffsetFromStart(item.Date) \ 12
                If rowOffset < 0 OrElse columnOffset < 0 Then Continue For
                salesReportWorksheet.Cells(14 + rowOffset, 3 + columnOffset * 2).SetValue(item.Total)
            Next

            Dim salesDataWorksheet = Me.spreadsheetControl.Document.Worksheets("Sales Data")
            Dim salesDataItems = ViewModel.GetSalesData().ToList()
            Dim states = salesDataItems.[Select](Function(i) i.State).Distinct().OrderBy(Function(i) i).ToList()
            salesDataWorksheet.Import(ViewModel.GetStates(states), 5, 3, False)
            For Each item As CustomersAnalysis.Item In salesDataItems
                Dim rowOffset As Integer = MonthOffsetFromStart(item.Date)
                Dim columnOffset As Integer = states.IndexOf(item.State)
                If rowOffset < 0 OrElse columnOffset < 0 Then Continue For
                salesDataWorksheet.Cells(6 + rowOffset, 3 + columnOffset).SetValue(item.Total)
            Next

            Me.spreadsheetControl.Document.Worksheets.ActiveWorksheet = salesReportWorksheet
            Me.spreadsheetControl.Document.EndUpdate()
        End Sub
    End Class
End Namespace
