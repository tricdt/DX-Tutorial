Namespace GridDemo.WcfSCService

    Public Partial Class SCEntities
        Inherits Global.System.Data.Services.Client.DataServiceContext

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Sub New(ByVal serviceRoot As Global.System.Uri)
            MyBase.New(serviceRoot)
            Me.ResolveName = New Global.System.Func(Of Global.System.Type, String)(AddressOf Me.ResolveNameFromType)
            Me.ResolveType = New Global.System.Func(Of String, Global.System.Type)(AddressOf Me.ResolveTypeFromName)
            OnContextCreated()
        End Sub

        Partial Private Sub OnContextCreated()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Function ResolveTypeFromName(ByVal typeName As String) As Global.System.Type
            If typeName.StartsWith("SCModel", Global.System.StringComparison.Ordinal) Then
                Return Me.GetType().Assembly.GetType(String.Concat("GridDemo.WcfSCService", typeName.Substring(17)), False)
            End If

            Return Nothing
        End Function

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Function ResolveNameFromType(ByVal clientType As Global.System.Type) As String
            If clientType.Namespace.Equals("GridDemo.WcfSCService", Global.System.StringComparison.Ordinal) Then
                Return String.Concat("SCModel.", clientType.Name)
            End If

            Return Nothing
        End Function

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public ReadOnly Property SCIssuesDemo As Global.System.Data.Services.Client.DataServiceQuery(Of SCIssuesDemo)
            Get
                If _SCIssuesDemo Is Nothing Then
                    _SCIssuesDemo = Me.CreateQuery(Of SCIssuesDemo)("SCIssuesDemo")
                End If

                Return _SCIssuesDemo
            End Get
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _SCIssuesDemo As Global.System.Data.Services.Client.DataServiceQuery(Of SCIssuesDemo)

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Sub AddToSCIssuesDemo(ByVal SCIssuesDemo As SCIssuesDemo)
            Me.AddObject("SCIssuesDemo", SCIssuesDemo)
        End Sub
    End Class

    <Global.System.Data.Services.Common.EntitySetAttribute("SCIssuesDemo")>
    <Global.System.Data.Services.Common.DataServiceKeyAttribute("Oid")>
    Public Partial Class SCIssuesDemo
        Implements Global.System.ComponentModel.INotifyPropertyChanged

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Shared Function CreateSCIssuesDemo(ByVal oid As Global.System.Guid) As SCIssuesDemo
            Dim SCIssuesDemo As SCIssuesDemo = New SCIssuesDemo()
            SCIssuesDemo.Oid = oid
            Return SCIssuesDemo
        End Function

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Oid As Global.System.Guid
            Get
                Return _Oid
            End Get

            Set(ByVal value As Global.System.Guid)
                Me.OnOidChanging(value)
                _Oid = value
                OnOidChanged()
                OnPropertyChanged("Oid")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Oid As Global.System.Guid

        Partial Private Sub OnOidChanging(ByVal value As Global.System.Guid)
        End Sub

        Partial Private Sub OnOidChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property ID As String
            Get
                Return _ID
            End Get

            Set(ByVal value As String)
                OnIDChanging(value)
                _ID = value
                OnIDChanged()
                OnPropertyChanged("ID")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _ID As String

        Partial Private Sub OnIDChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnIDChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Subject As String
            Get
                Return _Subject
            End Get

            Set(ByVal value As String)
                OnSubjectChanging(value)
                _Subject = value
                OnSubjectChanged()
                OnPropertyChanged("Subject")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Subject As String

        Partial Private Sub OnSubjectChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnSubjectChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property ModifiedOn As Global.System.Nullable(Of Global.System.DateTime)
            Get
                Return _ModifiedOn
            End Get

            Set(ByVal value As Global.System.Nullable(Of Global.System.DateTime))
                Me.OnModifiedOnChanging(value)
                _ModifiedOn = value
                OnModifiedOnChanged()
                OnPropertyChanged("ModifiedOn")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _ModifiedOn As Global.System.Nullable(Of Global.System.DateTime)

        Partial Private Sub OnModifiedOnChanging(ByVal value As Global.System.Nullable(Of Global.System.DateTime))
        End Sub

        Partial Private Sub OnModifiedOnChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property CreatedOn As Global.System.Nullable(Of Global.System.DateTime)
            Get
                Return _CreatedOn
            End Get

            Set(ByVal value As Global.System.Nullable(Of Global.System.DateTime))
                Me.OnCreatedOnChanging(value)
                _CreatedOn = value
                OnCreatedOnChanged()
                OnPropertyChanged("CreatedOn")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _CreatedOn As Global.System.Nullable(Of Global.System.DateTime)

        Partial Private Sub OnCreatedOnChanging(ByVal value As Global.System.Nullable(Of Global.System.DateTime))
        End Sub

        Partial Private Sub OnCreatedOnChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property ProductName As String
            Get
                Return _ProductName
            End Get

            Set(ByVal value As String)
                OnProductNameChanging(value)
                _ProductName = value
                OnProductNameChanged()
                OnPropertyChanged("ProductName")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _ProductName As String

        Partial Private Sub OnProductNameChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnProductNameChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property TechnologyName As String
            Get
                Return _TechnologyName
            End Get

            Set(ByVal value As String)
                OnTechnologyNameChanging(value)
                _TechnologyName = value
                OnTechnologyNameChanged()
                OnPropertyChanged("TechnologyName")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _TechnologyName As String

        Partial Private Sub OnTechnologyNameChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnTechnologyNameChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Urgent As Global.System.Nullable(Of Boolean)
            Get
                Return _Urgent
            End Get

            Set(ByVal value As Global.System.Nullable(Of Boolean))
                Me.OnUrgentChanging(value)
                _Urgent = value
                OnUrgentChanged()
                OnPropertyChanged("Urgent")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Urgent As Global.System.Nullable(Of Boolean)

        Partial Private Sub OnUrgentChanging(ByVal value As Global.System.Nullable(Of Boolean))
        End Sub

        Partial Private Sub OnUrgentChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Status As String
            Get
                Return _Status
            End Get

            Set(ByVal value As String)
                OnStatusChanging(value)
                _Status = value
                OnStatusChanged()
                OnPropertyChanged("Status")
            End Set
        End Property

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Status As String

        Partial Private Sub OnStatusChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnStatusChanged()
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Event PropertyChanged As Global.System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Overridable Sub OnPropertyChanged(ByVal [property] As String)
            RaiseEvent PropertyChanged(Me, New Global.System.ComponentModel.PropertyChangedEventArgs([property]))
        End Sub
    End Class
End Namespace
