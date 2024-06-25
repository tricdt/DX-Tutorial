Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    Public Partial Class pageLayoutControl
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub HidePopupContent()
            If layoutItems.IsCustomization Then layoutItems.Controller.CustomizationController.SelectedElements.Clear()
            MyBase.HidePopupContent()
        End Sub

        Private Sub layoutItems_IsCustomizationChanged(ByVal sender As Object, ByVal e As EventArgs)
            If layoutItems.IsCustomization Then
                AddHandler layoutItems.Controller.CustomizationController.SelectionChanged, AddressOf layoutItems_SelectionChanged
            Else
                RemoveHandler layoutItems.Controller.CustomizationController.SelectionChanged, AddressOf layoutItems_SelectionChanged
            End If

            If PropertiesControl IsNot Nothing Then PropertiesControl.SelectedItem = Nothing
        End Sub

        Private Sub layoutItems_SelectionChanged(ByVal sender As Object, ByVal e As LayoutControlSelectionChangedEventArgs)
            If e.SelectedElements.Count = 1 AndAlso Not ReferenceEquals(e.SelectedElements(0), layoutItems) Then
                PropertiesControl.SelectedItem = e.SelectedElements(0)
            Else
                PropertiesControl.SelectedItem = Nothing
            End If
        End Sub

        Private Sub LayoutGroupHeaderTextEdit_EditValueChanging(ByVal sender As Object, ByVal e As EditValueChangingEventArgs)
            Dim textBox = CType(sender, TextEdit)
            Dim layoutGroup = TryCast(TryCast(sender, TextEdit).DataContext, LayoutGroup)
            If layoutGroup Is Nothing Then Return
            Dim parentLayoutGroup = TryCast(layoutGroup.Parent, LayoutGroup)
            If textBox.EditValue IsNot Nothing AndAlso Equals(textBox.EditValue, e.NewValue) AndAlso Not parentLayoutGroup.View.Equals(LayoutGroupView.Tabs) Then layoutGroup.View = LayoutGroupView.GroupBox
        End Sub

        Private Sub LayoutGroupIsCollapsibleCheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim checkEdit = CType(sender, CheckEdit)
            CType(checkEdit.DataContext, LayoutGroup).View = LayoutGroupView.GroupBox
        End Sub

        Private Sub LayoutGroupIsCollapsedCheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim checkEdit = CType(sender, CheckEdit)
            Dim group = CType(checkEdit.DataContext, LayoutGroup)
            group.View = LayoutGroupView.GroupBox
            group.IsCollapsible = True
        End Sub
    End Class

    Public Class LayoutControlTemplateSelector
        Inherits DataTemplateSelector

        Public Property LayoutGroupTemplate As DataTemplate

        Public Property LayoutItemTemplate As DataTemplate

        Public Property DefaultTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If TypeOf item Is LayoutGroup Then Return LayoutGroupTemplate
            If TypeOf item Is LayoutItem Then Return LayoutItemTemplate
            If TypeOf item Is TextEdit OrElse TypeOf item Is SampleLayoutItem Then Return DefaultTemplate
            Return New DataTemplate()
        End Function
    End Class
End Namespace
