Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Windows.Input
Imports DevExpress.Xpf.Grid.LookUp
Imports DevExpress.Xpf.Core.Native

Namespace EditorsDemo

    <CodeFile("ModuleResources/LookUpEditTemplates.xaml")>
    <CodeFile("ViewModels/LookUpEditorDemoViewModel.cs")>
    Public Partial Class LookUpEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            ViewModel = New LookUpEditorDemoViewModel()
            DataContext = ViewModel
            Call Keyboard.AddGotKeyboardFocusHandler(Me, New KeyboardFocusChangedEventHandler(AddressOf OnKeyBoardFocusChanged))
            InitializeComponent()
        End Sub

        Private Property ViewModel As LookUpEditorDemoViewModel

        Private Sub OnKeyBoardFocusChanged(ByVal sender As Object, ByVal e As KeyboardFocusChangedEventArgs)
            Dim focused = TryCast(e.NewFocus, DependencyObject)
            ViewModel.FocusedEditor = If(TypeOf focused Is LookUpEdit, TryCast(focused, LookUpEdit), LayoutHelper.FindParentObject(Of LookUpEdit)(focused))
        End Sub
    End Class

    Public Class LookUpEditOptionsTemplateSelector
        Inherits DataTemplateSelector

        Public Property LookUpTemplate As DataTemplate

        Public Property SearchLookUpTemplate As DataTemplate

        Public Property MultiSelectLookUpTemplate As DataTemplate

        Public Property PlaceHolderTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim edit = TryCast(item, LookUpEditBase)
            If edit IsNot Nothing Then
                Select Case edit.Name
                    Case "defaultLookUpEdit"
                        Return LookUpTemplate
                    Case "searchLookUpEdit"
                        Return SearchLookUpTemplate
                    Case "multiSelectLookUpEdit"
                        Return MultiSelectLookUpTemplate
                End Select
            End If

            Return PlaceHolderTemplate
        End Function
    End Class
End Namespace
