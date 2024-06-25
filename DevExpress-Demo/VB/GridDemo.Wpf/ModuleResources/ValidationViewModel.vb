Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class ValidationViewModel

        Private _Items As List(Of GridDemo.CollectionInfo)

        Const RowCount As Integer = 8

        Private Shared Function GetValidationData() As IEnumerable(Of GridDemo.ValidationData)
            Dim result = New System.Collections.Generic.List(Of GridDemo.ValidationData)()
            For Each employee As DevExpress.Xpf.DemoBase.DataClasses.Employee In DevExpress.Xpf.DemoBase.DataClasses.EmployeesData.DataSource
                Dim data = New GridDemo.ValidationData() With {.FirstName = employee.FirstName, .LastName = employee.LastName, .Email = employee.EmailAddress, .Address = employee.AddressLine1, .Phone = employee.Phone, .Title = employee.JobTitle, .ZipCode = employee.PostalCode, .HireDate = employee.HireDate, .Salary = GridDemo.DataGenerator.GetSalary(), .Facebook = GridDemo.DataGenerator.GetFacebookAddress(employee.FirstName, employee.LastName), .CreditCard = GridDemo.DataGenerator.GetCreditCardNumber()}
                result.Add(data)
                If result.Count > GridDemo.ValidationViewModel.RowCount Then Exit For
            Next

            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).FirstName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).FirstName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).LastName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).LastName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).HireDate = System.DateTime.Today.AddDays(2)
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).HireDate = System.DateTime.Today.AddDays(3)
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Salary = -10000
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Salary = 1000001
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).CreditCard = "0000 0000 0000 0000"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).CreditCard = "1234 1234 1234 1234"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Address = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Address = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).ZipCode = "000"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).ZipCode = "123"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Phone = "555 1234"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Phone = "(00o)555 1234"
            Dim dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("@", " ")
            dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("com", "")
            dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Facebook = dataItem.Email
            dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Facebook = dataItem.Facebook.Replace("com", "")
            Return result
        End Function

        Private Shared Function GetValidationData_FluentAPI() As IEnumerable(Of GridDemo.ValidationData_FluentAPI)
            Dim result = New System.Collections.Generic.List(Of GridDemo.ValidationData_FluentAPI)()
            For Each employee As DevExpress.Xpf.DemoBase.DataClasses.Employee In DevExpress.Xpf.DemoBase.DataClasses.EmployeesData.DataSource
                Dim data = New GridDemo.ValidationData_FluentAPI() With {.FirstName = employee.FirstName, .LastName = employee.LastName, .Email = employee.EmailAddress, .Address = employee.AddressLine1, .Phone = employee.Phone, .Title = employee.JobTitle, .ZipCode = employee.PostalCode, .HireDate = employee.HireDate, .Salary = GridDemo.DataGenerator.GetSalary(), .Facebook = GridDemo.DataGenerator.GetFacebookAddress(employee.FirstName, employee.LastName), .CreditCard = GridDemo.DataGenerator.GetCreditCardNumber()}
                result.Add(data)
                If result.Count > GridDemo.ValidationViewModel.RowCount Then Exit For
            Next

            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).FirstName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).FirstName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).LastName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).LastName = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).HireDate = System.DateTime.Today.AddDays(2)
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).HireDate = System.DateTime.Today.AddDays(3)
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Salary = -10000
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Salary = 1000001
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).CreditCard = "0000 0000 0000 0000"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).CreditCard = "1234 1234 1234 1234"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Address = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Address = Nothing
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).ZipCode = "000"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).ZipCode = "123"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Phone = "555 1234"
            result(CInt((GridDemo.ValidationViewModel.GetRandomRowIndex()))).Phone = "(00o)555 1234"
            Dim dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("@", " ")
            dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Email = dataItem.Email.Replace("com", "")
            dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Facebook = dataItem.Email
            dataItem = result(GridDemo.ValidationViewModel.GetRandomRowIndex())
            dataItem.Facebook = dataItem.Facebook.Replace("com", "")
            Return result
        End Function

        Private Shared ReadOnly random As System.Random = New System.Random()

        Private Shared Function GetRandomRowIndex() As Integer
            Return GridDemo.ValidationViewModel.random.[Next](GridDemo.ValidationViewModel.RowCount)
        End Function

        Protected Sub New()
            Me.Items = New System.Collections.Generic.List(Of GridDemo.CollectionInfo) From {New GridDemo.CollectionInfo(GridDemo.ValidationViewModel.GetValidationData(), "Validation via Data Annotation attributes"), New GridDemo.CollectionInfo(GridDemo.ValidationViewModel.GetValidationData_FluentAPI(), "Validation via Fluent API")}
            Me.SelectedCollectionInfo = Me.Items.First()
        End Sub

        Public Property Items As List(Of GridDemo.CollectionInfo)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of GridDemo.CollectionInfo))
                _Items = value
            End Set
        End Property

        Public Overridable Property SelectedCollectionInfo As CollectionInfo
    End Class
End Namespace
