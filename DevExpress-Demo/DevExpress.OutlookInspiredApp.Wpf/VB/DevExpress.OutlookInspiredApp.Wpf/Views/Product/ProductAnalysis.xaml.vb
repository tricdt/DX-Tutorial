Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Spreadsheet

Namespace DevExpress.DevAV.Views.Product

    Public Partial Class ProductAnalysis
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
            Using stream = GetAnalysisTemplate(AnalysisTemplate.ProductSales)
                Me.spreadsheetControl.LoadDocument(stream)
            End Using
        End Sub

        Private Sub OnDocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            LoadData()
            CType(Me.splashScreenService, Mvvm.ISplashScreenService).HideSplashScreen()
        End Sub

        Private Sub LoadData()
            Dim ViewModel As ProductAnalysisViewModel = CType(DataContext, ProductAnalysisViewModel)
            Me.spreadsheetControl.Document.BeginUpdate()
            Dim financialReportWorksheet = Me.spreadsheetControl.Document.Worksheets("Financial Report")
            Dim financialReportItems = ViewModel.GetFinancialReport().ToList()
            Dim frProducts = financialReportItems.[Select](Function(i) i.ProductName).Distinct().OrderBy(Function(i) i).ToList()
            financialReportWorksheet.Import(frProducts, 17, 1, True)
            For Each item As ProductsAnalysis.Item In financialReportItems
                Dim rowOffset As Integer = frProducts.IndexOf(item.ProductName)
                Dim columnOffset As Integer = MonthOffsetFromStart(item.Date) \ 12
                If rowOffset < 0 OrElse columnOffset < 0 Then Continue For
                financialReportWorksheet.Cells(17 + rowOffset, 3 + columnOffset * 2).SetValue(item.Total)
            Next

            Dim financialDataWorksheet = Me.spreadsheetControl.Document.Worksheets("Financial Data")
            Dim financialDataItems = ViewModel.GetFinancialData().ToList()
            For Each item As ProductsAnalysis.Item In financialDataItems
                Dim rowOffset As Integer = MonthOffsetFromStart(item.Date)
                Dim columnOffset As Integer = GetColumnIndex(item.ProductCategory)
                If rowOffset < 0 OrElse columnOffset < 0 Then Continue For
                financialDataWorksheet.Cells(6 + rowOffset, 3 + columnOffset).SetValue(item.Total)
            Next

            Me.spreadsheetControl.Document.Worksheets.ActiveWorksheet = financialReportWorksheet
            Me.spreadsheetControl.Document.EndUpdate()
        End Sub

        Private Function GetColumnIndex(ByVal category As ProductCategory) As Integer
            Select Case category
                Case ProductCategory.Televisions
                    Return 0
                Case ProductCategory.Monitors
                    Return 1
                Case ProductCategory.VideoPlayers
                    Return 2
                Case ProductCategory.Automation
                    Return 3
                Case Else
                    Return -1
            End Select
        End Function
    End Class
End Namespace
