Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Accordion

Namespace NavigationDemo

    Public Class NavigationPaneViewModel

        Protected ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return GetRequiredService(Of IDocumentManagerService)()
            End Get
        End Property

        Public Overridable Property Groups As ObservableCollection(Of GroupDescription)

        Public Overridable Property SelectedGroup As GroupDescription

        Public Overridable Property OnStartup As Boolean

        Private contactsNavigationViewModel As ContactsNavigationViewModel

        Public Sub OnLoaded()
            If OnStartup Then OnSelectedItemChanged(New AccordionSelectedItemChangedEventArgs(Nothing, Nothing, Enumerable.Single(Groups, Function(x) x.Id = NavigationId.Mail).Items.First()))
        End Sub

        Public Sub OnSelectedItemChanged(ByVal e As AccordionSelectedItemChangedEventArgs)
            If e.OldItem IsNot Nothing Then OnStartup = False
            Dim newItem = TryCast(e.NewItem, ItemDescription)
            If newItem Is Nothing Then Return
            Dim document = DocumentManagerService.CreateDocument(GetDocumentType(newItem.Id), GetDocumentParameter(newItem.Id), Me)
            document.DestroyOnClose = True
            document.Show()
        End Sub

        Public Sub New()
            OnStartup = True
            Groups = CreateGroups()
            SelectedGroup = Groups(0)
        End Sub

        Private Function CreateGroups() As ObservableCollection(Of GroupDescription)
            Dim groups = New ObservableCollection(Of GroupDescription)()
            contactsNavigationViewModel = ContactsNavigationViewModel.Create()
            groups.Add(GroupDescription.Create(NavigationId.Mail, items:=New ItemDescription() {ItemDescription.Create(NavigationId.Inbox), ItemDescription.Create(NavigationId.Outbox), ItemDescription.Create(NavigationId.SentItems, "Sent Items"), ItemDescription.Create(NavigationId.DeletedItems, "Deleted Items"), ItemDescription.Create(NavigationId.Drafts)}))
            groups.Add(GroupDescription.Create(NavigationId.Calendar, items:=New ItemDescription() {ItemDescription.Create(NavigationId.Calendar_Content)}))
            groups.Add(GroupDescription.Create(NavigationId.Contacts, items:=New ItemDescription() {ItemDescription.Create(NavigationId.Contacts_Content, navigationViewModel:=contactsNavigationViewModel)}))
            groups.Add(GroupDescription.Create(NavigationId.Tasks, items:=New ItemDescription() {ItemDescription.Create(NavigationId.Tasks)}))
            groups.Add(GroupDescription.Create(NavigationId.Notes, items:=New ItemDescription() {ItemDescription.Create(NavigationId.Notes)}))
            groups.Add(GroupDescription.Create(NavigationId.FolderList, "Folder List", items:=New ItemDescription() {ItemDescription.Create(NavigationId.PersonalFolders, "Personal Folders", New ItemDescription() {ItemDescription.Create(NavigationId.Contacts), ItemDescription.Create(NavigationId.DeletedItems, "Deleted Items"), ItemDescription.Create(NavigationId.Drafts), ItemDescription.Create(NavigationId.Inbox), ItemDescription.Create(NavigationId.Journal), ItemDescription.Create(NavigationId.Notes), ItemDescription.Create(NavigationId.Outbox), ItemDescription.Create(NavigationId.SentItems, "Sent Items"), ItemDescription.Create(NavigationId.Tasks)})}, showChildrenExpandButton:=True))
            groups.Add(GroupDescription.Create(NavigationId.Shortcuts, items:=New ItemDescription() {ItemDescription.Create(NavigationId.Shortcuts)}))
            groups.Add(GroupDescription.Create(NavigationId.Journal, items:=New ItemDescription() {ItemDescription.Create(NavigationId.Journal)}))
            Return groups
        End Function

        Private Function GetDocumentType(ByVal itemId As NavigationId) As String
            Dim resultId = itemId
            If MailGroupContainsItem(itemId) Then
                resultId = NavigationId.Mail
            ElseIf itemId = NavigationId.Calendar_Content OrElse itemId = NavigationId.PersonalFolders Then
                Return "Info"
            ElseIf itemId = NavigationId.Contacts_Content Then
                Return "Contacts"
            End If

            Return resultId.ToString()
        End Function

        Private Function GetDocumentParameter(ByVal itemId As NavigationId) As Object
            If MailGroupContainsItem(itemId) Then
                Return itemId
            ElseIf itemId = NavigationId.Calendar_Content OrElse itemId = NavigationId.PersonalFolders Then
                Return String.Empty
            ElseIf itemId = NavigationId.Contacts OrElse itemId = NavigationId.Contacts_Content Then
                Return contactsNavigationViewModel
            End If

            Return Nothing
        End Function

        Private Function MailGroupContainsItem(ByVal itemId As NavigationId) As Boolean
            Return Enumerable.Single(Groups, Function(x) x.Id = NavigationId.Mail).Items.SingleOrDefault(Function(y) y.Id = itemId) IsNot Nothing
        End Function
    End Class

    Public Class GroupDescription
        Inherits ItemDescriptionBase

        Public Shared Function Create(ByVal id As NavigationId, ByVal Optional title As String = Nothing, ByVal Optional items As IList(Of ItemDescription) = Nothing, ByVal Optional showChildrenExpandButton As Boolean = False) As GroupDescription
            Return ViewModelSource.Create(Function() New GroupDescription(id, title, items, showChildrenExpandButton))
        End Function

        Protected Sub New(ByVal id As NavigationId, ByVal title As String, ByVal items As IList(Of ItemDescription), ByVal showChildrenExpandButton As Boolean)
            MyBase.New(id, title, items)
            Icon = GetIcon(Me.Id.ToString())
            If Me.Items.Any() Then SelectedItem = Me.Items.First()
            For Each item In Me.Items
                item.ShowExpandButton = showChildrenExpandButton
            Next
        End Sub

        Public Overridable Property SelectedItem As ItemDescription
    End Class

    Public Class ItemDescription
        Inherits ItemDescriptionBase

        Public Shared Function Create(ByVal id As NavigationId, ByVal Optional title As String = Nothing, ByVal Optional items As IList(Of ItemDescription) = Nothing, ByVal Optional navigationViewModel As NavigationViewModelBase = Nothing) As ItemDescription
            Return ViewModelSource.Create(Function() New ItemDescription(id, title, items, navigationViewModel))
        End Function

        Protected Sub New(ByVal id As NavigationId, ByVal title As String, ByVal items As IList(Of ItemDescription), ByVal navigationViewModel As NavigationViewModelBase)
            MyBase.New(id, title, items, navigationViewModel)
            If Me.Id <> NavigationId.Calendar_Content AndAlso Me.Id <> NavigationId.Contacts_Content Then Icon = GetIcon(Me.Id.ToString())
        End Sub
    End Class

    Public Class ItemDescriptionBase

        Private _Id As NavigationId, _Title As String, _Icon As Uri, _NavigationViewModel As NavigationViewModelBase

        Protected Sub New(ByVal id As NavigationId, ByVal title As String, ByVal items As IList(Of ItemDescription), ByVal Optional navigationViewModel As NavigationViewModelBase = Nothing)
            Me.Id = id
            Me.Title = If(title, Me.Id.ToString())
            Me.Items = New List(Of ItemDescription)(If(items, Enumerable.Empty(Of ItemDescription)()))
            ShowExpandButton = True
            Me.NavigationViewModel = navigationViewModel
        End Sub

        Public Property Id As NavigationId
            Get
                Return _Id
            End Get

            Protected Set(ByVal value As NavigationId)
                _Id = value
            End Set
        End Property

        Public Property Title As String
            Get
                Return _Title
            End Get

            Protected Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Public Property Icon As Uri
            Get
                Return _Icon
            End Get

            Protected Set(ByVal value As Uri)
                _Icon = value
            End Set
        End Property

        Public Overridable Property ShowExpandButton As Boolean

        Public Property NavigationViewModel As NavigationViewModelBase
            Get
                Return _NavigationViewModel
            End Get

            Protected Set(ByVal value As NavigationViewModelBase)
                _NavigationViewModel = value
            End Set
        End Property

        Public Overridable Property Items As IList(Of ItemDescription)
    End Class

    Public Enum NavigationId
        Mail
        Inbox
        Outbox
        SentItems
        DeletedItems
        Drafts
        Calendar
        Calendar_Content
        Contacts
        Contacts_Content
        Tasks
        Notes
        FolderList
        PersonalFolders
        Shortcuts
        Journal
    End Enum
End Namespace
