Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Partial Class OLAPBrowser
        Inherits PivotGridDemoModule

        Const YearFieldName As String = "[Date].[Calendar].[Calendar Year]"

        Const CategoryFieldName As String = "[Product].[Product].[Product]"

        Const TotalCostFieldName As String = "[Measures].[Total Product Cost]"

        Const FreightFieldName As String = "[Measures].[Freight Cost]"

        Const QuantityOrderFieldName As String = "[Measures].[Order Quantity]"

        Protected Const DefaultFieldWidth As Integer = 90

        Public Shared ReadOnly Property UseXmlaConnection As Boolean
            Get
                Return Not DevExpress.XtraPivotGrid.Data.OLAPMetaGetter.IsProviderAvailable
            End Get
        End Property

        Private Async Shared Function GetSampleConnectionString() As Task(Of String)
            Return Await Task.Run(Function() "Provider=msolap;Data Source=" & GetSampleFileName() & ";Initial Catalog=" & GetSampleCatalogName() & ";Cube Name=Adventure Works")
        End Function

        Private Shared Function GetSampleCatalogName() As String
            If UseXmlaConnection Then Return "Adventure Works DW Standard Edition"
            Return "Adventure Works"
        End Function

        Private Shared Function GetSampleFileName() As String
            If UseXmlaConnection Then Return "http://demos.devexpress.com/Services/OLAP/msmdpump.dll"
            Dim sampleFileName As String = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath))
            If File.Exists(sampleFileName) Then
                Try
                    File.SetAttributes(sampleFileName, FileAttributes.Normal)
                Catch
                End Try
            End If

            Return sampleFileName
        End Function

        Private Function PivotConnectionString() As String
            Return pivotGrid.OlapConnectionString
        End Function

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Async Sub PivotGridDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.ShowLoadingPanel = True
            Await InitPivotGrid(Await GetSampleConnectionString())
            pivotGrid.ShowLoadingPanel = False
        End Sub

        Private Function IsSampleCube() As Boolean
            Return pivotGrid.OlapConnectionString.Contains("Cube Name=Adventure Works")
        End Function

        Private Sub InitPivotLayoutSampleOlapDB()
            If pivotGrid.Fields.Count = 0 OrElse Not IsSampleCube() Then Return
            pivotGrid.BeginUpdate()
            Dim fieldProduct As PivotGridField = pivotGrid.Fields(CategoryFieldName), fieldYear As PivotGridField = pivotGrid.Fields(YearFieldName), fieldTotalCost As PivotGridField = pivotGrid.Fields(TotalCostFieldName), fieldFreightCost As PivotGridField = pivotGrid.Fields(FreightFieldName), fieldOrderQuantity As PivotGridField = pivotGrid.Fields(QuantityOrderFieldName)
            If fieldProduct Is Nothing OrElse fieldYear Is Nothing OrElse fieldTotalCost Is Nothing OrElse fieldFreightCost Is Nothing OrElse fieldOrderQuantity Is Nothing Then
                pivotGrid.EndUpdateAsync()
                Return
            End If

            fieldProduct.Area = FieldArea.RowArea
            fieldYear.Area = FieldArea.ColumnArea
            fieldYear.SortOrder = FieldSortOrder.Descending
            fieldTotalCost.Width = DefaultFieldWidth + 20
            fieldTotalCost.CellFormat = "c2"
            fieldFreightCost.Width = DefaultFieldWidth
            fieldFreightCost.CellFormat = "c2"
            fieldOrderQuantity.Width = DefaultFieldWidth + 5
            fieldProduct.Visible = True
            fieldYear.Visible = True
            fieldTotalCost.Visible = True
            fieldFreightCost.Visible = True
            fieldOrderQuantity.Visible = True
            pivotGrid.EndUpdateAsync()
        End Sub

        Private Async Function InitPivotGrid(ByVal connectionString As String) As Task(Of Boolean)
            If String.IsNullOrWhiteSpace(connectionString) Then
                pivotGrid.DataSource = Nothing
                Return True
            End If

            If Equals(PivotConnectionString(), connectionString) Then Return True
            pivotGrid.OlapConnectionString = Nothing
            pivotGrid.BeginUpdate()
            pivotGrid.Fields.Clear()
            pivotGrid.Groups.Clear()
            If UseXmlaConnection Then pivotGrid.OlapDataProvider = OlapDataProvider.Xmla
            pivotGrid.EndUpdate()
            Await pivotGrid.SetOlapConnectionStringAsync(connectionString)
            If pivotGrid.Fields.Count = 0 Then
                Await pivotGrid.RetrieveFieldsAsync(FieldArea.FilterArea, False)
                InitPivotLayoutSampleOlapDB()
            End If

            Return True
        End Function

        Private dialog As DataSourceDialog

        Private Sub OnShowConnectionClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If pivotGrid Is Nothing OrElse pivotGrid.IsAsyncInProgress Then Return
            dialog = New DataSourceDialog()
            Dim pars As FloatingContainerParameters = New FloatingContainerParameters()
            pars.AllowSizing = False
            pars.CloseOnEscape = True
            pars.Title = "OLAP Connection"
            pars.ClosedDelegate = AddressOf DialogClosed
            Call FloatingContainer.ShowDialogContent(dialog, Me, New Size(600, 230), pars)
        End Sub

        Private Async Sub DialogClosed(ByVal close As Boolean?)
            Call Application.Current.MainWindow.Activate()
            If dialog Is Nothing Then Return
            Dim connectionString As String = dialog.GetConnectionString()
            dialog = Nothing
            If close <> True Then Return
            If String.IsNullOrWhiteSpace(connectionString) Then
                Return
            End If

            Await InitPivotGrid(connectionString)
        End Sub
    End Class
End Namespace
