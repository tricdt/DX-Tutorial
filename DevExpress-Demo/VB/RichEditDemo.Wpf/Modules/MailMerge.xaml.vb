Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models
Imports DevExpress.Office.Services

Namespace RichEditDemo

    Public Partial Class MailMerge
        Inherits RichEditDemoModule

        Private employeesField As IList(Of Employee)

        Private ReadOnly Property Employees As IList(Of Employee)
            Get
                If employeesField Is Nothing Then employeesField = DevExpress.DemoData.NWindDataProvider.Employees
                Return employeesField
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            Dim uriService As IUriStreamService = richEdit.GetService(Of IUriStreamService)()
            uriService.RegisterProvider(New DBUriStreamProvider(Employees))
            Dim dataSource = CreateDataSource()
            gridControl1.ItemsSource = dataSource
            richEdit.MailMergeOptions.DataSource = dataSource
            AddHandler gridControl1.View.FocusedRowHandleChanged, AddressOf View_FocusedRowChanged
        End Sub

        Private Function CreateDataSource() As IEnumerable(Of Object)
            Return(From e In Employees From c In e.Customers Select New With {e.EmployeeID, e.LastName, e.FirstName, e.Title, e.TitleOfCourtesy, e.BirthDate, e.HireDate, .Employees = New With {e.Address, e.City, e.Region, e.PostalCode, e.Country}, e.HomePhone, e.Extension, e.Photo, e.Notes, e.ReportsTo, e.Email, c.CustomerID, c.CompanyName, c.ContactName, c.ContactTitle, .Customers = New With {c.Address, c.City, c.Region, c.PostalCode, c.Country}, c.Phone, c.Fax}).ToList()
        End Function

        Private Sub View_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.FocusedRowHandleChangedEventArgs)
            RichEditControl.MailMergeOptions.ActiveRecord = gridControl1.GetListIndexByRowHandle(e.RowData.RowHandle.Value)
        End Sub
    End Class
End Namespace
