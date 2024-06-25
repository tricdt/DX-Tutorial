Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Utils
Imports DevExpress.XtraGrid
Imports ProductsDemo.Controls

Namespace ProductsDemo.Modules

    Public Class GridContactsModuleViewModel
        Inherits GridViewModelBase(Of Contact)

        Public Sub New()
            CheckedItemType = "Card"
            InitializeIndexBarSource()
            SelectedIndexBarString = IndexBarSource(0)
        End Sub

        Public Overridable Property FilteredItemsSource As ObservableCollection(Of Contact)

        Public Overridable Property IndexBarSource As List(Of String)

        Public Overridable Property SelectedIndexBarString As String

        Public Sub SetTableView()
            ChangeView(ViewType.TableView)
        End Sub

        Public Sub SetCardView()
            ChangeView(ViewType.CardView)
        End Sub

        Public Sub GroupByAlphabet()
            GroupBy("Name.FullName")
        End Sub

        Public Sub GroupByState()
            GroupBy("Address.State")
        End Sub

        Protected Overridable ReadOnly Property NotificationService As INotificationService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable Sub OnSelectedIndexBarStringChanged()
            UpdateFilteredItemsSource()
        End Sub

        Public Overrides Sub ShowNewItemWindow()
            ShowNewContactWindow()
        End Sub

        Private Sub InitializeIndexBarSource()
            Dim source As List(Of String) = New List(Of String)()
            For Each contact As Contact In ItemsSource
                Dim firstChar As String = contact.LastName.Substring(0, 1).ToUpper()
                If source.Contains(firstChar) Then Continue For
                source.Add(firstChar)
            Next

            source.Sort()
            source.Insert(0, "ALL")
            IndexBarSource = source
        End Sub

        Private Sub UpdateFilteredItemsSource()
            Dim source As ObservableCollection(Of Contact) = New ObservableCollection(Of Contact)()
            For Each contact As Contact In ItemsSource
                If Equals(SelectedIndexBarString, IndexBarSource(0)) OrElse (Not String.IsNullOrEmpty(contact.LastName) AndAlso contact.LastName.ToUpper().StartsWith(SelectedIndexBarString)) Then source.Add(contact)
            Next

            FilteredItemsSource = source
        End Sub

        Protected Overrides Sub OnItemsSourceCollectionChanged(ByVal sender As Object, ByVal e As Collections.Specialized.NotifyCollectionChangedEventArgs)
            MyBase.OnItemsSourceCollectionChanged(sender, e)
            UpdateFilteredItemsSource()
        End Sub

        Protected Overrides Function GetColumns() As List(Of GridColumnModel)
            Dim columns As List(Of GridColumnModel) = New List(Of GridColumnModel)()
            columns.Add(New GridColumnModel() With {.Name = "Prefix", .DisplayName = String.Empty, .AllowEditing = DefaultBoolean.False, .Width = 40, .FixedWidth = True})
            columns.Add(New GridColumnModel() With {.Name = "Name.FullName", .DisplayName = "Name", .AllowEditing = DefaultBoolean.False, .GroupInterval = ColumnGroupInterval.Alphabetical})
            columns.Add(New GridColumnModel() With {.Name = "Email", .AllowEditing = DefaultBoolean.False, .Width = 182, .FixedWidth = True})
            columns.Add(New GridColumnModel() With {.Name = "Address.State", .DisplayName = "State", .AllowEditing = DefaultBoolean.False, .Width = 46, .FixedWidth = True})
            columns.Add(New GridColumnModel() With {.Name = "Address.City", .DisplayName = "City", .AllowEditing = DefaultBoolean.False, .Width = 80, .FixedWidth = True})
            columns.Add(New GridColumnModel() With {.Name = "Phone", .AllowEditing = DefaultBoolean.False, .Mask = "(\(\d\d\d\)) \d{1,3}-\d\d\d\d", .Width = 90, .FixedWidth = True})
            columns.Add(New GridColumnModel() With {.Name = "Activity", .AllowEditing = DefaultBoolean.False, .Width = 70, .FixedWidth = True})
            Return columns
        End Function

        Protected Overrides Sub InitializeDefaultView()
            ChangeView(ViewType.CardView)
            CheckedItemType = ItemType.Card
        End Sub

        Protected Overrides Function GetItemsSource() As List(Of Contact)
            Return DataBaseHelper.Instance.Contacts
        End Function

        Protected Overrides Sub GroupBy(ByVal fieldName As String)
            ChangeView(ViewType.TableView)
            MyBase.GroupBy(fieldName)
        End Sub

        Public Overridable Property EditableContact As Contact

        Public Overridable Property CurrentView As ViewType

        Public Property States As List(Of String)

        Public Property Cities As List(Of String)

        Private contactWindow As EditContactWindow = Nothing

        Private Sub ChangeView(ByVal viewType As ViewType)
            ClearModel()
            ChangeViewCore(viewType)
        End Sub

        Private Sub ChangeViewCore(ByVal viewType As ViewType)
            CurrentView = viewType
        End Sub

        Private Sub ShowEditContactWindowCore(ByVal contact As Contact)
            contactWindow = New EditContactWindow()
            EditableContact = contact
            States = GetStates()
            Cities = GetCities()
            contactWindow.DataContext = Me
            contactWindow.ShowDialog()
        End Sub

        Private isNew As Boolean

        Public Sub ShowEditContactWindow()
            If SelectedItem Is Nothing Then Return
            isNew = False
            ShowEditContactWindowCore(CType(SelectedItem.Clone(), Contact))
        End Sub

        Public Sub ShowNewContactWindow()
            isNew = True
            ShowEditContactWindowCore(New Contact())
        End Sub

        Public Sub SaveContact()
            If isNew Then
                ItemsSource.Add(EditableContact)
                SelectedItem = EditableContact
                NotificationService.CreatePredefinedNotification("Contact Created", EditableContact.Name.FullName, "", EditableContact.Photo).ShowAsync()
            Else
                SelectedItem.Assign(EditableContact)
                NotificationService.CreatePredefinedNotification("Contact Changed", EditableContact.Name.FullName, "", EditableContact.Photo).ShowAsync()
            End If

            contactWindow.Close()
        End Sub

        Public Sub CloseContactWindow()
            contactWindow.Close()
        End Sub

        Public Sub DeleteContact()
            NotificationService.CreatePredefinedNotification("Contact Deleted", SelectedItem.Name.FullName, "", SelectedItem.Photo).ShowAsync()
            ItemsSource.Remove(SelectedItem)
        End Sub

        Private Function GetCities() As List(Of String)
            Dim cities As Collections.IEnumerable =(From contact In ItemsSource Select contact.Address.City).OrderBy(Function(s) s).Distinct()
            Return cities.Cast(Of String)().ToList()
        End Function

        Private Function GetStates() As List(Of String)
            Dim states As Collections.IEnumerable =(From contact In ItemsSource Select contact.Address.State).OrderBy(Function(s) s).Distinct()
            Return states.Cast(Of String)().ToList()
        End Function
    End Class

    Public Enum ViewType
        TableView
        CardView
    End Enum
End Namespace
