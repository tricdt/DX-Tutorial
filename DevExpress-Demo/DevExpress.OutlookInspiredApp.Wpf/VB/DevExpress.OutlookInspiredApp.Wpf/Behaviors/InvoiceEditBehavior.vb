Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Reports.Spreadsheet
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Spreadsheet
Imports DevExpress.Xpf.Spreadsheet.Extensions.Internal

Namespace DevExpress.DevAV

    Public Class InvoiceEditBehavior
        Inherits Behavior(Of SpreadsheetControl)

        Public Shared ReadOnly SpreadsheetDataSourceProperty As DependencyProperty = DependencyProperty.Register("SpreadsheetDataSource", GetType(Tuple(Of IDevAVDbUnitOfWork, Order)), GetType(InvoiceEditBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, InvoiceEditBehavior).InitSpreadsheet()))

        Public Property SpreadsheetDataSource As Tuple(Of IDevAVDbUnitOfWork, Order)
            Get
                Return CType(GetValue(SpreadsheetDataSourceProperty), Tuple(Of IDevAVDbUnitOfWork, Order))
            End Get

            Set(ByVal value As Tuple(Of IDevAVDbUnitOfWork, Order))
                SetValue(SpreadsheetDataSourceProperty, value)
            End Set
        End Property

        Private unitOfWork As IDevAVDbUnitOfWork

        Private invoiceHelper As InvoiceHelper

        Private order As Order

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Subscribe()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            Unsubscribe()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            InitSpreadsheet()
        End Sub

        Private Sub InitSpreadsheet()
            If AssociatedObject Is Nothing OrElse Not AssociatedObject.IsLoaded Then Return
            LoadInvoiceTemplate()
            ParseDataSource()
            CreateInvoiceHelper()
        End Sub

        Private Sub OnPreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As Input.MouseButtonEventArgs)
            Dim cell = AssociatedObject.GetCellFromPoint(e.GetPosition(AssociatedObject).ToDrawingPoint())
            invoiceHelper.OnPreviewMouseLeftButton(cell)
        End Sub

        Private Sub CellValueChanged(ByVal sender As Object, ByVal e As XtraSpreadsheet.SpreadsheetCellEventArgs)
            invoiceHelper.CellValueChanged(sender, e)
        End Sub

        Private Sub OnSelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            invoiceHelper.SelectionChanged()
        End Sub

        Private Sub CreateInvoiceHelper()
            Dim collections As OrderCollections = New OrderCollections()
            Dim actions As EditActions = New EditActions()
            If unitOfWork IsNot Nothing Then
                actions.ActivateEditor = Sub()
                    Dim activeSheet = AssociatedObject.ActiveWorksheet
                    If Equals(activeSheet.Name, CellsHelper.InvoiceWorksheetName) AndAlso activeSheet.CustomCellInplaceEditors.GetCustomCellInplaceEditors(activeSheet.Selection).Any() Then AssociatedObject.OpenCellEditor(XtraSpreadsheet.CellEditorMode.Edit)
                End Sub
                actions.CloseEditor = Sub()
                    If AssociatedObject.IsCellEditorActive Then AssociatedObject.CloseCellEditor(XtraSpreadsheet.CellEditorEnterValueMode.Cancel)
                End Sub
                actions.GetCustomerStores = Function(x) LoadCustomerStores(x)
                actions.CreateOrderItem = Function() unitOfWork.OrderItems.Create(False)
                actions.AddOrderItem = Sub(x) unitOfWork.OrderItems.Add(x)
                actions.RemoveOrderItem = Sub(x) unitOfWork.OrderItems.Remove(x)
                actions.IsDefaultActions = False
                collections = CreateOrderCollections()
            End If

            invoiceHelper = New InvoiceHelper(AssociatedObject.Document, New Tuple(Of OrderCollections, Order)(collections, order), actions)
        End Sub

        Private Sub OnCustomCellEdit(ByVal sender As Object, ByVal e As SpreadsheetCustomCellEditEventArgs)
            If Not e.ValueObject.IsText OrElse unitOfWork Is Nothing Then Return
            Dim editorInfo = CellsHelper.FindEditor(e.ValueObject.TextValue)
            If editorInfo IsNot Nothing Then e.EditSettings = CreateSpinEditSettings(editorInfo.MinValue, editorInfo.MaxValue, editorInfo.Increment)
        End Sub

        Private Sub LoadInvoiceTemplate()
            Using stream = InvoiceHelper.GetInvoiceTemplate()
                AssociatedObject.LoadDocument(stream)
            End Using
        End Sub

        Private Sub OnProtectionWarning(ByVal sender As Object, ByVal e As System.ComponentModel.HandledEventArgs)
            e.Handled = True
        End Sub

        Private Sub Subscribe()
            AddHandler AssociatedObject.Loaded, AddressOf OnLoaded
            AddHandler AssociatedObject.CustomCellEdit, AddressOf OnCustomCellEdit
            AddHandler AssociatedObject.SelectionChanged, AddressOf OnSelectionChanged
            AddHandler AssociatedObject.CellValueChanged, AddressOf CellValueChanged
            AddHandler AssociatedObject.PreviewMouseLeftButtonDown, AddressOf OnPreviewMouseLeftButtonDown
            AddHandler AssociatedObject.ProtectionWarning, AddressOf OnProtectionWarning
        End Sub

        Private Sub Unsubscribe()
            RemoveHandler AssociatedObject.Loaded, AddressOf OnLoaded
            RemoveHandler AssociatedObject.CustomCellEdit, AddressOf OnCustomCellEdit
            RemoveHandler AssociatedObject.SelectionChanged, AddressOf OnSelectionChanged
            RemoveHandler AssociatedObject.CellValueChanged, AddressOf CellValueChanged
            RemoveHandler AssociatedObject.PreviewMouseLeftButtonDown, AddressOf OnPreviewMouseLeftButtonDown
            RemoveHandler AssociatedObject.ProtectionWarning, AddressOf OnProtectionWarning
        End Sub

        Private Function CreateOrderCollections() As OrderCollections
            Dim collections = New OrderCollections()
            collections.Customers = unitOfWork.Customers
            collections.Products = unitOfWork.Products
            collections.Employees = unitOfWork.Employees
            collections.CustomerStores = LoadCustomerStores(order.CustomerId)
            Return collections
        End Function

        Private Function LoadCustomerStores(ByVal customerId As Long?) As IEnumerable(Of CustomerStore)
            Return unitOfWork.CustomerStores.Where(Function(x) x.CustomerId.Value = customerId.Value)
        End Function

        Private Sub ParseDataSource()
            unitOfWork = SpreadsheetDataSource.Item1
            order = SpreadsheetDataSource.Item2
        End Sub

        Private Shared Function CreateSpinEditSettings(ByVal minValue As Integer, ByVal maxValue As Integer, ByVal increment As Integer) As SpinEditSettings
            Dim settings As SpinEditSettings = New SpinEditSettings()
            settings.MinValue = minValue
            settings.MaxValue = maxValue
            settings.Increment = increment
            settings.IsFloatValue = False
            Return settings
        End Function
    End Class
End Namespace
