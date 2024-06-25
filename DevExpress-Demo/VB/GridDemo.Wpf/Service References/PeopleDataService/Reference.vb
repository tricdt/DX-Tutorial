Namespace GridDemo.PeopleDataService

    Public Partial Class Entities
        Inherits Global.System.Data.Services.Client.DataServiceContext

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Sub New(ByVal serviceRoot As Global.System.Uri)
            MyBase.New(serviceRoot)
            Me.ResolveName = New Global.System.Func(Of Global.System.Type, String)(AddressOf Me.ResolveNameFromType)
            Me.ResolveType = New Global.System.Func(Of String, Global.System.Type)(AddressOf Me.ResolveTypeFromName)
            OnContextCreated()
        End Sub

        Partial Private Sub OnContextCreated()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Function ResolveTypeFromName(ByVal typeName As String) As Global.System.Type
            If typeName.StartsWith("PersonDataBase.PeopleManagementContextModel", Global.System.StringComparison.Ordinal) Then
                Return Me.GetType().Assembly.GetType(String.Concat("GridDemo.PeopleDataService", typeName.Substring(43)), False)
            End If

            Return Nothing
        End Function

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Function ResolveNameFromType(ByVal clientType As Global.System.Type) As String
            If clientType.Namespace.Equals("GridDemo.PeopleDataService", Global.System.StringComparison.Ordinal) Then
                Return String.Concat("PersonDataBase.PeopleManagementContextModel.", clientType.Name)
            End If

            Return Nothing
        End Function

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public ReadOnly Property People As Global.System.Data.Services.Client.DataServiceQuery(Of Person)
            Get
                If _People Is Nothing Then
                    _People = Me.CreateQuery(Of Person)("People")
                End If

                Return _People
            End Get
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _People As Global.System.Data.Services.Client.DataServiceQuery(Of Person)

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Sub AddToPeople(ByVal person As Person)
            Me.AddObject("People", person)
        End Sub
    End Class

    <Global.System.Data.Services.Common.EntitySetAttribute("People")>
    <Global.System.Data.Services.Common.DataServiceKeyAttribute("PersonID")>
    Public Partial Class Person
        Implements Global.System.ComponentModel.INotifyPropertyChanged

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Shared Function CreatePerson(ByVal personID As String) As Person
            Dim person As Person = New Person()
            person.PersonID = personID
            Return person
        End Function

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property PersonID As String
            Get
                Return _PersonID
            End Get

            Set(ByVal value As String)
                OnPersonIDChanging(value)
                _PersonID = value
                OnPersonIDChanged()
                OnPropertyChanged("PersonID")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _PersonID As String

        Partial Private Sub OnPersonIDChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnPersonIDChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property FullName As String
            Get
                Return _FullName
            End Get

            Set(ByVal value As String)
                OnFullNameChanging(value)
                _FullName = value
                OnFullNameChanged()
                OnPropertyChanged("FullName")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _FullName As String

        Partial Private Sub OnFullNameChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnFullNameChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Company As String
            Get
                Return _Company
            End Get

            Set(ByVal value As String)
                OnCompanyChanging(value)
                _Company = value
                OnCompanyChanged()
                OnPropertyChanged("Company")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Company As String

        Partial Private Sub OnCompanyChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnCompanyChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property JobTitle As String
            Get
                Return _JobTitle
            End Get

            Set(ByVal value As String)
                OnJobTitleChanging(value)
                _JobTitle = value
                OnJobTitleChanged()
                OnPropertyChanged("JobTitle")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _JobTitle As String

        Partial Private Sub OnJobTitleChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnJobTitleChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property City As String
            Get
                Return _City
            End Get

            Set(ByVal value As String)
                OnCityChanging(value)
                _City = value
                OnCityChanged()
                OnPropertyChanged("City")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _City As String

        Partial Private Sub OnCityChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnCityChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Address As String
            Get
                Return _Address
            End Get

            Set(ByVal value As String)
                OnAddressChanging(value)
                _Address = value
                OnAddressChanged()
                OnPropertyChanged("Address")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Address As String

        Partial Private Sub OnAddressChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnAddressChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Phone As String
            Get
                Return _Phone
            End Get

            Set(ByVal value As String)
                OnPhoneChanging(value)
                _Phone = value
                OnPhoneChanged()
                OnPropertyChanged("Phone")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Phone As String

        Partial Private Sub OnPhoneChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnPhoneChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Email As String
            Get
                Return _Email
            End Get

            Set(ByVal value As String)
                OnEmailChanging(value)
                _Email = value
                OnEmailChanged()
                OnPropertyChanged("Email")
            End Set
        End Property

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Email As String

        Partial Private Sub OnEmailChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnEmailChanged()
        End Sub

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Event PropertyChanged As Global.System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Overridable Sub OnPropertyChanged(ByVal [property] As String)
            RaiseEvent PropertyChanged(Me, New Global.System.ComponentModel.PropertyChangedEventArgs([property]))
        End Sub
    End Class
End Namespace
