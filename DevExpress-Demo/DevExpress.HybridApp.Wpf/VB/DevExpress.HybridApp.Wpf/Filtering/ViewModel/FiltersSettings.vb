Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Properties
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel
Imports System

Namespace DevExpress.DevAV.ViewModels

    Friend Module FiltersSettings

        Public Function GetDashboardFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Employee, Long)
            Return FilterTreeViewModel(Of Employee, Long).Create(New FilterTreeModelPageSpecificSettings(Of Settings)(Settings.Default, Nothing, Nothing, Nothing, Nothing), CreateUnitOfWork().Employees, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Employee, Long))).SetParentViewModel(parentViewModel)
        End Function

        Public Function GetTasksFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of EmployeeTask, Long)
            Return FilterTreeViewModel(Of EmployeeTask, Long).Create(New FilterTreeModelPageSpecificSettings(Of Settings)(Settings.Default, Nothing, Function(x) x.TasksStaticFilters, Nothing, Nothing), CreateUnitOfWork().Tasks, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of EmployeeTask, Long))).SetParentViewModel(parentViewModel)
        End Function

        Public Function GetEmployeesFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Employee, Long)
            Return FilterTreeViewModel(Of Employee, Long).Create(New FilterTreeModelPageSpecificSettings(Of Settings)(Settings.Default, "Status", Function(x) x.EmployeesStaticFilters, Nothing, Nothing), CreateUnitOfWork().Employees, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Employee, Long))).SetParentViewModel(parentViewModel)
        End Function

        Public Function GetProductsFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Product, Long)
            Return FilterTreeViewModel(Of Product, Long).Create(New FilterTreeModelPageSpecificSettings(Of Settings)(Settings.Default, "Category", Function(x) x.ProductsStaticFilters, Function(x) x.ProductsCustomFilters, {BindableBase.GetPropertyName(Function() New Product().Engineer), BindableBase.GetPropertyName(Function() New Product().Brochure), BindableBase.GetPropertyName(Function() New Product().Support), BindableBase.GetPropertyName(Function() New Product().PrimaryImage), BindableBase.GetPropertyName(Function() New Product().ProductImage)}), CreateUnitOfWork().Products, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Product, Long))).SetParentViewModel(parentViewModel)
        End Function

        Public Function GetCustomersFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Customer, Long)
            Return FilterTreeViewModel(Of Customer, Long).Create(New FilterTreeModelPageSpecificSettings(Of Settings)(Settings.Default, "Favorites", Nothing, Function(x) x.CustomersCustomFilters, {BindableBase.GetPropertyName(Function() New Customer().Id), BindableBase.GetPropertyName(Function() New Customer().BillingAddress), BindableBase.GetPropertyName(Function() New Customer().HomeOffice), BindableBase.GetPropertyName(Function() New Customer().Image), BindableBase.GetPropertyName(Function() New Customer().Website)}, {BindableBase.GetPropertyName(Function() New Customer().BillingAddress) & "." & BindableBase.GetPropertyName(Function() New Address().City), BindableBase.GetPropertyName(Function() New Customer().BillingAddress) & "." & BindableBase.GetPropertyName(Function() New Address().State), BindableBase.GetPropertyName(Function() New Customer().BillingAddress) & "." & BindableBase.GetPropertyName(Function() New Address().ZipCode)}), CreateUnitOfWork().Customers, New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Customer, Long))).SetParentViewModel(parentViewModel)
        End Function

        Public Function GetSalesFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Order, Long)
            Return FilterTreeViewModel(Of Order, Long).Create(New FilterTreeModelPageSpecificSettings(Of Settings)(Settings.Default, Nothing, Nothing, Nothing, Nothing), CreateUnitOfWork().Orders.ActualOrders(), New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Order, Long))).SetParentViewModel(parentViewModel)
        End Function

        Public Function GetOpportunitiesFilterTree(ByVal parentViewModel As Object) As FilterTreeViewModel(Of Quote, Long)
            Return FilterTreeViewModel(Of Quote, Long).Create(New FilterTreeModelPageSpecificSettings(Of Settings)(Settings.Default, Nothing, Nothing, Nothing, Nothing), CreateUnitOfWork().Quotes.ActualQuotes(), New Action(Of Object, Action)(AddressOf RegisterEntityChangedMessageHandler(Of Quote, Long))).SetParentViewModel(parentViewModel)
        End Function

        Private Function CreateUnitOfWork() As IDevAVDbUnitOfWork
            Return GetUnitOfWorkFactory().CreateUnitOfWork()
        End Function

        Private Sub RegisterEntityChangedMessageHandler(Of TEntity, TPrimaryKey)(ByVal recipient As Object, ByVal handler As Action)
            Call Messenger.Default.Register(Of EntityMessage(Of TEntity, TPrimaryKey))(recipient, Sub(message) handler())
        End Sub
    End Module
End Namespace
