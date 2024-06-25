Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Utils
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel

Namespace DevExpress.DevAV.ViewModels

    Public Class FilterTreeViewModel(Of TEntity As Class, TPrimaryKey)
        Implements DevExpress.DevAV.ViewModels.IFilterTreeViewModel

        Private _WarningParameterName As String, _StaticCategoryName As String, _CustomCategoryName As String

        Shared Sub New()
            Dim enums = GetType(DevExpress.DevAV.EmployeeStatus).Assembly.GetTypes().Where(Function(t) t.IsEnum)
            For Each e As System.Type In enums
                Call DevExpress.Data.Filtering.EnumProcessingHelper.RegisterEnum(e)
            Next
        End Sub

        Public Shared Function Create(ByVal settings As DevExpress.DevAV.ViewModels.IFilterTreeModelPageSpecificSettings, ByVal entities As System.Linq.IQueryable(Of TEntity), ByVal registerEntityChangedMessageHandler As System.Action(Of Object, System.Action)) As FilterTreeViewModel(Of TEntity, TPrimaryKey)
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New DevExpress.DevAV.ViewModels.FilterTreeViewModel(Of TEntity, TPrimaryKey)(settings, entities, registerEntityChangedMessageHandler))
        End Function

        Private ReadOnly entities As System.Linq.IQueryable(Of TEntity)

        Private ReadOnly settings As DevExpress.DevAV.ViewModels.IFilterTreeModelPageSpecificSettings

        Private viewModel As Object

        Protected Sub New(ByVal settings As DevExpress.DevAV.ViewModels.IFilterTreeModelPageSpecificSettings, ByVal entities As System.Linq.IQueryable(Of TEntity), ByVal registerEntityChangedMessageHandler As System.Action(Of Object, System.Action))
            Me.settings = settings
            Me.entities = entities
            Me.StaticFilters = Me.CreateFilterItems(settings.StaticFilters)
            Me.CustomFilters = Me.CreateFilterItems(settings.CustomFilters)
            Me.SelectedItem = Me.StaticFilters.FirstOrDefault()
            Me.AllowFiltersContextMenu = True
            Me.WarningParameterName = "Overdue Tasks"
            Call DevExpress.Mvvm.Messenger.[Default].Register(Of DevExpress.Mvvm.ViewModel.EntityMessage(Of TEntity, TPrimaryKey))(Me, Sub(message) Me.UpdateFilters(message))
            Call DevExpress.Mvvm.Messenger.[Default].Register(Of DevExpress.DevAV.ViewModels.CreateCustomFilterMessage(Of TEntity))(Me, Sub(message) Me.CreateCustomFilter())
            Me.UpdateFilters()
            Me.StaticCategoryName = settings.StaticFiltersTitle
            Me.CustomCategoryName = settings.CustomFiltersTitle
            Me.CategoriesProp = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterCategory)() From {New DevExpress.DevAV.ViewModels.FilterCategory(Me.StaticCategoryName, Me, Me.StaticFilters), New DevExpress.DevAV.ViewModels.FilterCategory(Me.CustomCategoryName, Me, Me.CustomFilters, isCustom:=True)}
        End Sub

        Public Overridable Property CategoriesProp As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterCategory)

        Public Overridable Property StaticFilters As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem)

        Public Overridable Property CustomFilters As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem)

        Public Overridable Property SelectedItem As FilterItem

        Public Overridable Property ActiveFilterItem As FilterItem

        Public Property WarningParameterName As String
            Get
                Return _WarningParameterName
            End Get

            Private Set(ByVal value As String)
                _WarningParameterName = value
            End Set
        End Property

        Public Overridable Property WarningInfo As String

        Public Property NavigateAction As Action Implements Global.DevExpress.DevAV.ViewModels.IFilterTreeViewModel.NavigateAction

        Public Property StaticCategoryName As String
            Get
                Return _StaticCategoryName
            End Get

            Private Set(ByVal value As String)
                _StaticCategoryName = value
            End Set
        End Property

        Public Property CustomCategoryName As String
            Get
                Return _CustomCategoryName
            End Get

            Private Set(ByVal value As String)
                _CustomCategoryName = value
            End Set
        End Property

        Public ReadOnly Property AllowCustomFilters As Boolean
            Get
                Return Me.settings.CustomFilters IsNot Nothing
            End Get
        End Property

        Public Property AllowFiltersContextMenu As Boolean
            Get
                Return Me.AllowCustomFilters AndAlso Me.allowFiltersContextMenuField
            End Get

            Set(ByVal value As Boolean)
                Me.allowFiltersContextMenuField = value
            End Set
        End Property

        Private allowFiltersContextMenuField As Boolean

        Public Sub DeleteCustomFilter(ByVal filterItem As DevExpress.DevAV.ViewModels.FilterItem)
            Me.CustomFilters.Remove(filterItem)
            Me.SaveCustomFilters()
        End Sub

        Public Sub DuplicateFilter(ByVal filterItem As DevExpress.DevAV.ViewModels.FilterItem)
            Dim newItem = filterItem.Clone("Copy of " & filterItem.Name, Nothing)
            Me.CustomFilters.Add(newItem)
            Me.SaveCustomFilters()
        End Sub

        Public Sub ResetCustomFilters()
            If Me.CustomFilters.Contains(Me.SelectedItem) Then Me.SelectedItem = Nothing
            Me.settings.CustomFilters = New DevExpress.DevAV.ViewModels.FilterInfoList()
            Me.CustomFilters.Clear()
            Me.settings.SaveSettings()
        End Sub

        Public Sub ModifyCustomFilter(ByVal existing As DevExpress.DevAV.ViewModels.FilterItem)
            Dim clone As DevExpress.DevAV.ViewModels.FilterItem = existing.Clone()
            Dim filterViewModel = Me.CreateCustomFilterViewModel(clone, True)
            Me.ShowFilter(clone, filterViewModel, Sub()
                existing.FilterCriteria = clone.FilterCriteria
                existing.Name = clone.Name
                Me.SaveCustomFilters()
                If existing Is Me.SelectedItem Then Me.OnSelectedItemChanged()
            End Sub)
        End Sub

        Public Sub ModifyCustomFilterCriteria(ByVal existing As DevExpress.DevAV.ViewModels.FilterItem, ByVal criteria As DevExpress.Data.Filtering.CriteriaOperator)
            If Not System.[Object].ReferenceEquals(existing.FilterCriteria, Nothing) AndAlso Not System.[Object].ReferenceEquals(criteria, Nothing) AndAlso Equals(existing.FilterCriteria.ToString(), criteria.ToString()) Then Return
            existing.FilterCriteria = criteria
            Me.SaveCustomFilters()
            If existing Is Me.SelectedItem Then Me.OnSelectedItemChanged()
            Me.UpdateFilters()
        End Sub

        Public Sub ResetToAll()
            Me.SelectedItem = Me.StaticFilters(0)
        End Sub

        Public Sub CreateCustomFilter()
            Dim filterItem As DevExpress.DevAV.ViewModels.FilterItem = Me.CreateFilterItem(String.Empty, Nothing, Nothing)
            Dim filterViewModel = Me.CreateCustomFilterViewModel(filterItem, True)
            Me.ShowFilter(filterItem, filterViewModel, Sub() Me.AddNewCustomFilter(filterItem))
        End Sub

        Public Sub AddCustomFilter(ByVal name As String, ByVal filterCriteria As DevExpress.Data.Filtering.CriteriaOperator, ByVal Optional imageUri As String = Nothing)
            Me.AddNewCustomFilter(Me.CreateFilterItem(name, filterCriteria, imageUri))
        End Sub

        Protected Overridable Sub OnSelectedItemChanged()
            Me.ActiveFilterItem = If(Me.SelectedItem Is Nothing, Me.StaticFilters(0), Me.SelectedItem.Clone())
            Me.UpdateFilterExpression()
            Me.NavigateCore()
        End Sub

        Public Overridable Sub Navigate()
            Me.NavigateCore()
        End Sub

        Private Sub NavigateCore()
            If Me.NavigateAction IsNot Nothing Then Me.NavigateAction.Invoke()
        End Sub

        Private Sub SetViewModel(ByVal viewModel As Object) Implements Global.DevExpress.DevAV.ViewModels.IFilterTreeViewModel.SetViewModel
            Me.viewModel = viewModel
            Dim viewModelContainer = TryCast(viewModel, DevExpress.DevAV.ViewModels.IFilterTreeViewModelContainer(Of TEntity, TPrimaryKey))
            If viewModelContainer IsNot Nothing Then viewModelContainer.FilterTreeViewModel = Me
            Me.UpdateFilterExpression()
        End Sub

        Private Sub UpdateFilterExpression()
            Dim viewModel As DevExpress.DevAV.ViewModels.ISupportFiltering(Of TEntity) = TryCast(Me.viewModel, DevExpress.DevAV.ViewModels.ISupportFiltering(Of TEntity))
            If viewModel IsNot Nothing Then viewModel.FilterExpression = If(Me.ActiveFilterItem Is Nothing, Nothing, Me.GetWhereExpression(Me.ActiveFilterItem.FilterCriteria))
        End Sub

        Private Function CreateFilterItems(ByVal filters As System.Collections.Generic.IEnumerable(Of DevExpress.DevAV.ViewModels.FilterInfo)) As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem)
            If filters Is Nothing Then Return New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem)()
            Return New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem)(filters.[Select](Function(x) Me.CreateFilterItem(x.Name, DevExpress.Data.Filtering.CriteriaOperator.Parse(x.FilterCriteria), x.ImageUri)))
        End Function

        Const NewFilterName As String = "New Filter"

        Private Sub AddNewCustomFilter(ByVal filterItem As DevExpress.DevAV.ViewModels.FilterItem)
            If String.IsNullOrEmpty(filterItem.Name) Then
                Dim prevIndex As Integer = System.Linq.Enumerable.DefaultIfEmpty(Of System.Int32)(System.Linq.Enumerable.[Select](Of System.Text.RegularExpressions.Match, Global.System.Int32)(System.Linq.Enumerable.Where(Of System.Text.RegularExpressions.Match)(System.Linq.Enumerable.[Select](Of DevExpress.DevAV.ViewModels.FilterItem, Global.System.Text.RegularExpressions.Match)(Me.CustomFilters, CType((Function(fi) CType((System.Text.RegularExpressions.Regex.Match(CStr((fi.Name)), CStr((DevExpress.DevAV.ViewModels.FilterTreeViewModel(Of TEntity, TPrimaryKey).NewFilterName & " (?<index>\d+)")))), System.Text.RegularExpressions.Match)), System.Func(Of DevExpress.DevAV.ViewModels.FilterItem, System.Text.RegularExpressions.Match))), CType((Function(m) CBool((m.Success))), System.Func(Of System.Text.RegularExpressions.Match, System.[Boolean]))), CType((Function(m) CInt((Integer.Parse(CStr((m.Groups(CStr(("index"))).Value)))))), System.Func(Of System.Text.RegularExpressions.Match, System.Int32))), CInt((0))).Max()
                filterItem.Name = DevExpress.DevAV.ViewModels.FilterTreeViewModel(Of TEntity, TPrimaryKey).NewFilterName & " " & (prevIndex + 1)
            Else
                Dim existing = Me.CustomFilters.FirstOrDefault(Function(fi) Equals(fi.Name, filterItem.Name))
                If existing IsNot Nothing Then Me.CustomFilters.Remove(existing)
            End If

            Me.CustomFilters.Add(filterItem)
            Me.SaveCustomFilters()
        End Sub

        Private Sub SaveCustomFilters()
            Me.settings.CustomFilters = Me.SaveToSettings(Me.CustomFilters)
            Me.settings.SaveSettings()
        End Sub

        Private Function SaveToSettings(ByVal filters As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem)) As FilterInfoList
            Return New DevExpress.DevAV.ViewModels.FilterInfoList(filters.[Select](Function(fi) New DevExpress.DevAV.ViewModels.FilterInfo With {.Name = fi.Name, .FilterCriteria = DevExpress.Data.Filtering.CriteriaOperator.ToString(fi.FilterCriteria)}))
        End Function

        Private Sub UpdateFilters(ByVal message As DevExpress.Mvvm.ViewModel.EntityMessage(Of TEntity, TPrimaryKey))
            If message.MessageType = DevExpress.Mvvm.ViewModel.EntityMessageType.Changed Then TryCast(Me.entities, DevExpress.Mvvm.DataModel.IRepository(Of TEntity, TPrimaryKey)).[Do](Sub(repo)
                repo.Find(message.PrimaryKey).[Do](Sub(x) repo.Reload(x))
            End Sub)
            Me.UpdateFilters()
        End Sub

        Private Sub UpdateFilters()
            For Each item In Me.StaticFilters.Concat(Me.CustomFilters)
                Dim entityCount = Me.GetEntityCount(item.FilterCriteria)
                If Equals(item.Name, Me.WarningParameterName) Then Me.WarningInfo = entityCount.ToString()
                item.Update(entityCount)
            Next
        End Sub

        Private Sub ShowFilter(ByVal filterItem As DevExpress.DevAV.ViewModels.FilterItem, ByVal filterViewModel As DevExpress.DevAV.CustomFilterViewModel, ByVal onSave As System.Action)
            Dim okCommand = New DevExpress.Mvvm.UICommand() With {.Caption = "OK", .IsDefault = True, .Command = New DevExpress.Mvvm.DelegateCommand(Nothing, Function() Me.IsFilterValid(filterViewModel))}
            Dim cancelCommand = New DevExpress.Mvvm.UICommand() With {.Caption = "Cancel", .IsCancel = True}
            If Me.FilterDialogService.ShowDialog({okCommand, cancelCommand}, "Create Custom Filter", "CustomFilterView", filterViewModel) IsNot okCommand Then Return
            filterItem.FilterCriteria = filterViewModel.FilterCriteria
            filterItem.Name = filterViewModel.FilterName
            Me.ActiveFilterItem = filterItem
            If filterViewModel.Save Then
                onSave()
                Me.UpdateFilters()
            End If
        End Sub

        Private Function IsFilterValid(ByVal filterViewModel As DevExpress.DevAV.CustomFilterViewModel) As Boolean
            Dim operands As DevExpress.Data.Filtering.OperandValue()
            Call DevExpress.Data.Filtering.CriteriaOperator.ToString(filterViewModel.FilterCriteria, operands)
            Return Not operands.Any(Function(x) x.Value Is Nothing)
        End Function

        Private Function CreateCustomFilterViewModel(ByVal existing As DevExpress.DevAV.ViewModels.FilterItem, ByVal save As Boolean) As CustomFilterViewModel
            Dim viewModel = DevExpress.DevAV.CustomFilterViewModel.Create(GetType(TEntity), Me.settings.HiddenFilterProperties, Me.settings.AdditionalFilterProperties)
            viewModel.FilterCriteria = existing.FilterCriteria
            viewModel.FilterName = existing.Name
            viewModel.Save = save
            viewModel.SetParentViewModel(Me)
            Return viewModel
        End Function

        Private Function CreateFilterItem(ByVal name As String, ByVal filterCriteria As DevExpress.Data.Filtering.CriteriaOperator, ByVal imageUri As String) As FilterItem
            Return DevExpress.DevAV.ViewModels.FilterItem.Create(Me.GetEntityCount(filterCriteria), name, filterCriteria, imageUri, Me)
        End Function

        Private Function GetEntityCount(ByVal criteria As DevExpress.Data.Filtering.CriteriaOperator) As Integer
            Return Me.entities.Where(Me.GetWhereExpression(CType((criteria), DevExpress.Data.Filtering.CriteriaOperator)).Compile()).Count()
        End Function

        Private Function GetWhereExpression(ByVal criteria As DevExpress.Data.Filtering.CriteriaOperator) As Expression(Of System.Func(Of TEntity, Boolean))
            Dim caseInsensitiveCriteria = DevExpress.Data.Helpers.StringsTolowerCloningHelper.Process(criteria)
            Return If(Me.IsInDesignMode(), DevExpress.Data.Utils.CriteriaOperatorToExpressionConverter.GetLinqToObjectsWhere(Of TEntity)(caseInsensitiveCriteria), DevExpress.Data.Utils.CriteriaOperatorToExpressionConverter.GetGenericWhere(Of TEntity)(caseInsensitiveCriteria))
        End Function

        Private ReadOnly Property FilterDialogService As IDialogService
            Get
                Return Me.GetRequiredService(Of DevExpress.Mvvm.IDialogService)("FilterDialogService")
            End Get
        End Property

        Private ReadOnly Property Categories As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterCategory) Implements Global.DevExpress.DevAV.ViewModels.IFilterTreeViewModel.Categories
            Get
                Return Me.CategoriesProp
            End Get
        End Property
    End Class

    Public Interface IFilterTreeViewModelContainer(Of TEntity As Class, TPrimaryKey)

        Property FilterTreeViewModel As FilterTreeViewModel(Of TEntity, TPrimaryKey)

    End Interface

    Public Class CreateCustomFilterMessage(Of TEntity As Class)
    End Class

    Public Class FilterCategory

        Private _Name As String, _FilterTreeViewModel As IFilterTreeViewModel, _FilterItems As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem), _IsCustom As Boolean

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property FilterTreeViewModel As IFilterTreeViewModel
            Get
                Return _FilterTreeViewModel
            End Get

            Private Set(ByVal value As IFilterTreeViewModel)
                _FilterTreeViewModel = value
            End Set
        End Property

        Public Property FilterItems As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem)
            Get
                Return _FilterItems
            End Get

            Private Set(ByVal value As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem))
                _FilterItems = value
            End Set
        End Property

        Public Property IsCustom As Boolean
            Get
                Return _IsCustom
            End Get

            Private Set(ByVal value As Boolean)
                _IsCustom = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal filterTreeViewModel As DevExpress.DevAV.ViewModels.IFilterTreeViewModel, ByVal filterItems As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterItem), ByVal Optional isCustom As Boolean = False)
            Me.Name = name
            Me.FilterItems = filterItems
            Me.FilterTreeViewModel = filterTreeViewModel
            Me.IsCustom = isCustom
        End Sub
    End Class

    Public Interface IFilterTreeViewModel

        Sub SetViewModel(ByVal content As Object)

        Property NavigateAction As Action

        ReadOnly Property Categories As ObservableCollection(Of DevExpress.DevAV.ViewModels.FilterCategory)

    End Interface
End Namespace
