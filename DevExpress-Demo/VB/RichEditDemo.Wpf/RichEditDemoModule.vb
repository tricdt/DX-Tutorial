Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Xpf.Utils
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo

    Public Class RichEditDemoModule
        Inherits DemoModule

        Private Shared ReadOnly RichEditControlPropertyKey As DependencyPropertyKey

        Public Shared ReadOnly RichEditControlProperty As DependencyProperty

        Shared Sub New()
            RichEditControlPropertyKey = DependencyPropertyManager.RegisterReadOnly("RichEditControl", GetType(RichEditControl), GetType(RichEditDemoModule), New FrameworkPropertyMetadata(CType(Nothing, PropertyChangedCallback)))
            RichEditControlProperty = RichEditControlPropertyKey.DependencyProperty
        End Sub

        Public Property RichEditControl As RichEditControl
            Get
                Return CType(GetValue(RichEditControlProperty), RichEditControl)
            End Get

            Private Set(ByVal value As RichEditControl)
                SetValue(RichEditControlPropertyKey, value)
            End Set
        End Property

        Private Sub OnRichEditControlLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetFocus(RichEditControl)
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            RichEditControl = If(TryCast(Content, RichEditControl), LayoutTreeHelper.GetVisualChildren(CType(Content, DependencyObject)).OfType(Of RichEditControl)().FirstOrDefault())
            If RichEditControl IsNot Nothing Then
                AddHandler RichEditControl.Loaded, AddressOf OnRichEditControlLoaded
                Call New RichEditDemoExceptionsHandler(RichEditControl).Install()
                SetBehaviorOptions()
                RichEditControl.ReplaceService(Of IUserAccountService)(New UserAccountService())
            End If
        End Sub

        Private Sub SetBehaviorOptions()
            RichEditControl.BehaviorOptions.FontSource = DevExpress.XtraRichEdit.RichEditBaseValueSource.Document
            RichEditControl.BehaviorOptions.ForeColorSource = DevExpress.XtraRichEdit.RichEditBaseValueSource.Document
        End Sub

        Protected Friend Overridable Sub SetFocus(ByVal control As RichEditControl)
            If control Is Nothing Then Return
            If control.KeyCodeConverter IsNot Nothing Then control.KeyCodeConverter.Focus()
        End Sub

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            If RichEditControl IsNot Nothing Then RichEditControl.ShowHoverMenu = True
        End Sub

        Protected Overrides Sub HidePopupContent()
            If RichEditControl IsNot Nothing Then RichEditControl.ShowHoverMenu = False
            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
