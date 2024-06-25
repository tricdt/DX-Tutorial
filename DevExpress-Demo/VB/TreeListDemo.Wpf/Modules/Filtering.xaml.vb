Imports System.Collections.Generic
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.ComponentModel
Imports DevExpress.Mvvm

Namespace TreeListDemo

    Public Partial Class Filtering
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class FiltrationModuleViewModel
        Inherits BindableBase

        Public Sub New()
            InitData()
            ShowAutoFilterRow = True
            ExpandNodesOnFiltering = True
            ShowCriteriaInAutoFilterRow = True
        End Sub

        Private Sub InitData()
            Filters = New List(Of Filter)()
            Filters.Add(New Filter("All", ""))
            Filters.Add(New Filter("Administration", "Contains([JobTitle], 'Administrator')"))
            Filters.Add(New Filter("Older than 35", "[Age] > 35"))
            Filters.Add(New Filter("Male", "[Gender] = 'M'"))
            Filters.Add(New Filter("Female", "[Gender] = 'F'"))
            Filters.Add(New Filter("Upcoming Birthdays", "[BirthdayMarkVisibility] = 'True'"))
            SearchPanelModes = New List(Of ShowSearchPanelMode)()
            SearchPanelModes.Add(ShowSearchPanelMode.Always)
            SearchPanelModes.Add(ShowSearchPanelMode.Default)
            SearchPanelModes.Add(ShowSearchPanelMode.Never)
            FilterModes = New List(Of TreeListFilteringMode)()
            FilterModes.Add(TreeListFilteringMode.Nodes)
            FilterModes.Add(TreeListFilteringMode.ParentBranch)
            FilterModes.Add(TreeListFilteringMode.EntireBranch)
            FilterModes.Add(TreeListFilteringMode.Recursive)
        End Sub

        Public Property Filters As List(Of Filter)

        Public Property SearchPanelModes As List(Of ShowSearchPanelMode)

        Public Property FilterModes As List(Of TreeListFilteringMode)

        Private showAutoFilterRowCore As Boolean

        Public Property ShowAutoFilterRow As Boolean
            Get
                Return showAutoFilterRowCore
            End Get

            Set(ByVal value As Boolean)
                SetProperty(showAutoFilterRowCore, value, Function() ShowAutoFilterRow)
            End Set
        End Property

        Private showCriteriaInAutoFilterRowCore As Boolean

        Public Property ShowCriteriaInAutoFilterRow As Boolean
            Get
                Return showCriteriaInAutoFilterRowCore
            End Get

            Set(ByVal value As Boolean)
                SetProperty(showCriteriaInAutoFilterRowCore, value, Function() ShowCriteriaInAutoFilterRow)
            End Set
        End Property

        Private expandNodesOnFilteringCore As Boolean

        Public Property ExpandNodesOnFiltering As Boolean
            Get
                Return expandNodesOnFilteringCore
            End Get

            Set(ByVal value As Boolean)
                SetProperty(expandNodesOnFilteringCore, value, Function() ExpandNodesOnFiltering)
            End Set
        End Property
    End Class

    Public Class Filter

        Private _Name As String, _FilterString As String

        Public Sub New(ByVal name As String, ByVal filterString As String)
            Me.Name = name
            Me.FilterString = filterString
        End Sub

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property FilterString As String
            Get
                Return _FilterString
            End Get

            Private Set(ByVal value As String)
                _FilterString = value
            End Set
        End Property
    End Class
End Namespace
