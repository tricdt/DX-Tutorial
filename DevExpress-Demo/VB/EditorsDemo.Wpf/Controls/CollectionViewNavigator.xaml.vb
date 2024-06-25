Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Utils
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input

Namespace DXDemo.Controls

    Public Partial Class CollectionViewNavigator
        Inherits UserControl

        Private _DeleteGroup As ICommand, _DeleteSort As ICommand, _AddGroup As ICommand, _AddSort As ICommand

        Public Shared ReadOnly IsSynchronizedWithCurrentItemProperty As DependencyProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", GetType(Boolean), GetType(CollectionViewNavigator), New UIPropertyMetadata(True))

        Public Shared ReadOnly CollectionViewProperty As DependencyProperty = DependencyPropertyManager.Register("CollectionView", GetType(ICollectionView), GetType(CollectionViewNavigator), New UIPropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly IsSynchronizedWithCurrentItemEditorVisibilityProperty As DependencyProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItemEditorVisibility", GetType(Visibility), GetType(CollectionViewNavigator), New UIPropertyMetadata(Visibility.Visible))

        Public Shared ReadOnly EditableCollectionViewVisibilityProperty As DependencyProperty = DependencyPropertyManager.Register("EditableCollectionViewVisibility", GetType(Visibility), GetType(CollectionViewNavigator), New UIPropertyMetadata(Visibility.Visible))

        Private directionsField As IList = New List(Of ListSortDirection)() From {ListSortDirection.Ascending, ListSortDirection.Descending}

        Private fieldsField As IList = New List(Of String)() From {"JobTitle", "FirstName", "LastName", "BirthDate"}

        Public ReadOnly Property Directions As IList
            Get
                Return directionsField
            End Get
        End Property

        Public ReadOnly Property Fields As IList
            Get
                Return fieldsField
            End Get
        End Property

        Public Property IsSynchronizedWithCurrentItem As Boolean
            Get
                Return CBool(GetValue(IsSynchronizedWithCurrentItemProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsSynchronizedWithCurrentItemProperty, value)
            End Set
        End Property

        Public Property CollectionView As ICollectionView
            Get
                Return CType(GetValue(CollectionViewProperty), ICollectionView)
            End Get

            Set(ByVal value As ICollectionView)
                SetValue(CollectionViewProperty, value)
            End Set
        End Property

        Public Property IsSynchronizedWithCurrentItemEditorVisibility As Visibility
            Get
                Return CType(GetValue(IsSynchronizedWithCurrentItemEditorVisibilityProperty), Visibility)
            End Get

            Set(ByVal value As Visibility)
                SetValue(IsSynchronizedWithCurrentItemEditorVisibilityProperty, value)
            End Set
        End Property

        Public Property EditableCollectionViewVisibility As Visibility
            Get
                Return CType(GetValue(EditableCollectionViewVisibilityProperty), Visibility)
            End Get

            Set(ByVal value As Visibility)
                SetValue(EditableCollectionViewVisibilityProperty, value)
            End Set
        End Property

        Public Property CurrentSortDescription As Integer

        Public Property CurrentGroupDescription As Integer

        Public Property CurrentGroupFieldName As String

        Public Property CurrentSortFieldName As String

        Public Property CurrentSortDirection As ListSortDirection

        Public Property DeleteGroup As ICommand
            Get
                Return _DeleteGroup
            End Get

            Private Set(ByVal value As ICommand)
                _DeleteGroup = value
            End Set
        End Property

        Public Property DeleteSort As ICommand
            Get
                Return _DeleteSort
            End Get

            Private Set(ByVal value As ICommand)
                _DeleteSort = value
            End Set
        End Property

        Public Property AddGroup As ICommand
            Get
                Return _AddGroup
            End Get

            Private Set(ByVal value As ICommand)
                _AddGroup = value
            End Set
        End Property

        Public Property AddSort As ICommand
            Get
                Return _AddSort
            End Get

            Private Set(ByVal value As ICommand)
                _AddSort = value
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            root.DataContext = Me
            DeleteGroup = New DelegateCommand(Of Object)(AddressOf OnDeleteGroup, AddressOf CanDeleteGroup)
            DeleteSort = New DelegateCommand(Of Object)(AddressOf OnDeleteSort, AddressOf CanDeleteSort)
            AddGroup = New DelegateCommand(Of Object)(AddressOf OnAddGroup)
            AddSort = New DelegateCommand(Of Object)(AddressOf OnAddSort)
        End Sub

        Private Sub OnDeleteGroup(ByVal parameter As Object)
            If CurrentGroupDescription >= 0 Then CollectionView.GroupDescriptions.RemoveAt(CurrentGroupDescription)
        End Sub

        Private Sub OnDeleteSort(ByVal parameter As Object)
            If CurrentSortDescription >= 0 Then
                CollectionView.GroupDescriptions.Remove(FindGroupDescription(CurrentSortDescription))
                CollectionView.SortDescriptions.RemoveAt(CurrentSortDescription)
            End If
        End Sub

        Private Sub OnAddGroup(ByVal parameter As Object)
            If ContainsGroupDescription(CurrentGroupFieldName) Then Return
            CollectionView.GroupDescriptions.Add(New PropertyGroupDescription(CurrentGroupFieldName))
            If Not ContainsSortDescription(CurrentGroupFieldName) Then CollectionView.SortDescriptions.Add(New SortDescription(CurrentGroupFieldName, ListSortDirection.Ascending))
        End Sub

        Private Sub OnAddSort(ByVal parameter As Object)
            If ContainsSortDescription(CurrentSortFieldName) Then Return
            CollectionView.SortDescriptions.Add(New SortDescription(CurrentSortFieldName, CurrentSortDirection))
        End Sub

        Public Function CanDeleteGroup(ByVal parameter As Object) As Boolean
            Return CurrentGroupDescription >= 0
        End Function

        Public Function CanDeleteSort(ByVal parameter As Object) As Boolean
            Return CurrentSortDescription >= 0
        End Function

        Private Function ContainsGroupDescription(ByVal fieldName As String) As Boolean
            For Each desc As PropertyGroupDescription In CollectionView.GroupDescriptions
                If Equals(desc.PropertyName, fieldName) Then Return True
            Next

            Return False
        End Function

        Private Function FindGroupDescription(ByVal index As Integer) As PropertyGroupDescription
            Dim name As String = CollectionView.SortDescriptions(CurrentSortDescription).PropertyName
            For Each desc As PropertyGroupDescription In CollectionView.GroupDescriptions
                If Equals(desc.PropertyName, name) Then Return desc
            Next

            Return Nothing
        End Function

        Private Function ContainsSortDescription(ByVal fieldName As String) As Boolean
            For Each desc As SortDescription In CollectionView.SortDescriptions
                If Equals(desc.PropertyName, fieldName) Then Return True
            Next

            Return False
        End Function
    End Class
End Namespace
