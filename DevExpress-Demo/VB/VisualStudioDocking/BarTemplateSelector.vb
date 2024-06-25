Imports System.Windows
Imports System.Windows.Controls

Namespace VisualStudioDocking.ViewModels

    Public Class BarItemTemplateSelector
        Inherits DataTemplateSelector

        Public Property BarCheckItemTemplate As DataTemplate

        Public Property BarItemTemplate As DataTemplate

        Public Property BarSubItemTemplate As DataTemplate

        Public Property BarItemSeparatorTemplate As DataTemplate

        Public Property BarComboBoxTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim commandViewModel As CommandViewModel = TryCast(item, CommandViewModel)
            If commandViewModel IsNot Nothing Then
                Dim template As DataTemplate = Nothing
                If commandViewModel.Owner IsNot Nothing Then template = BarCheckItemTemplate
                If commandViewModel.IsSubItem Then template = BarSubItemTemplate
                If commandViewModel.IsSeparator Then template = BarItemSeparatorTemplate
                If commandViewModel.IsComboBox Then template = BarComboBoxTemplate
                Return If(template Is Nothing, BarItemTemplate, template)
            End If

            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class

    Public Class BarTemplateSelector
        Inherits DataTemplateSelector

        Public Property MainMenuTemplate As DataTemplate

        Public Property ToolbarTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim barModel As BarModel = TryCast(item, BarModel)
            If barModel IsNot Nothing Then
                Return If(barModel.IsMainMenu, MainMenuTemplate, ToolbarTemplate)
            End If

            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class
End Namespace
