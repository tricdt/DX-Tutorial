Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.Editors

Namespace DockingDemo

    Public Partial Class ItemsVisibility
        Inherits DockingDemoModule

        Public Shared ReadOnly SelectedEmployeeProperty As DependencyProperty

        Shared Sub New()
            SelectedEmployeeProperty = DependencyProperty.Register("SelectedEmployee", GetType(Employee), GetType(ItemsVisibility))
        End Sub

        Private currentPerson As Integer

        Private editMode As Boolean

        Public Sub New()
            InitializeComponent()
            AddHandler dockManager.Loaded, AddressOf dockManager_Loaded
            SelectedEmployee = list(0)
            Dim currentTeamBinding As Binding = New Binding() With {.Path = New PropertyPath("SelectedEmployee"), .Source = Me}
            dockManager.SetBinding(DataContextProperty, currentTeamBinding)
        End Sub

        Public Property SelectedEmployee As Employee
            Get
                Return CType(GetValue(SelectedEmployeeProperty), Employee)
            End Get

            Set(ByVal value As Employee)
                SetValue(SelectedEmployeeProperty, value)
            End Set
        End Property

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim buttonEdit As Button = CType(sender, Button)
            editMode = Not editMode
            If editMode Then
                buttonEdit.Content = "End edit"
            Else
                buttonEdit.Content = "Start edit"
            End If

            UpdateItemsVisibility()
        End Sub

        Private Sub ButtonNext_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SelectedEmployee = list(System.Threading.Interlocked.Increment(currentPerson) Mod list.Count)
            UpdateItemsVisibility()
        End Sub

        Private Sub ButtonPrev_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            currentPerson -= 1
            If currentPerson < 0 Then currentPerson = list.Count - 1
            SelectedEmployee = list(currentPerson Mod list.Count)
            UpdateItemsVisibility()
        End Sub

        Private Sub dockManager_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateItemsVisibility()
        End Sub

        Private Sub UpdateItemsVisibility()
            Dim items As BaseLayoutItem() = dockManager.GetItems()
            For Each item As BaseLayoutItem In items
                Dim controlItem As LayoutControlItem = TryCast(item, LayoutControlItem)
                If controlItem IsNot Nothing AndAlso TypeOf controlItem.Control Is TextEdit Then
                    Dim edit As TextEdit = CType(controlItem.Control, TextEdit)
                    edit.IsReadOnly = Not editMode
                    If editMode OrElse edit.EditValue IsNot Nothing Then
                        controlItem.Visibility = Visibility.Visible
                    Else
                        controlItem.Visibility = Visibility.Collapsed
                    End If
                End If
            Next
        End Sub

        Private ReadOnly list As List(Of Employee) = Employee.CreateSampleData()
    End Class
End Namespace
