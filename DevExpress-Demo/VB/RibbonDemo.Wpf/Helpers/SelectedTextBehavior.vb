Imports DevExpress.Mvvm.UI.Interactivity
Imports System.Windows
Imports System.Windows.Controls

Namespace BarsDemo

    Public Class SelectedTextBehavior
        Inherits Behavior(Of TextBox)

        Public Shared ReadOnly SelectionStartProperty As DependencyProperty = DependencyProperty.Register("SelectionStart", GetType(Integer), GetType(SelectedTextBehavior), New PropertyMetadata(0, New PropertyChangedCallback(AddressOf SelectionStartChangedCallback)))

        Public Shared ReadOnly SelectionLengthProperty As DependencyProperty = DependencyProperty.Register("SelectionLength", GetType(Integer), GetType(SelectedTextBehavior), New PropertyMetadata(0, New PropertyChangedCallback(AddressOf SelectionLengthChangedCallback)))

        Public Shared ReadOnly SelectedTextProperty As DependencyProperty = DependencyProperty.Register("SelectedText", GetType(String), GetType(SelectedTextBehavior), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf SelectedTextChangedCallback)))

        Public Property SelectionStart As Integer
            Get
                Return CInt(GetValue(SelectionStartProperty))
            End Get

            Set(ByVal value As Integer)
                SetValue(SelectionStartProperty, value)
            End Set
        End Property

        Public Property SelectionLength As Integer
            Get
                Return CInt(GetValue(SelectionLengthProperty))
            End Get

            Set(ByVal value As Integer)
                SetValue(SelectionLengthProperty, value)
            End Set
        End Property

        Public Property SelectedText As String
            Get
                Return CStr(GetValue(SelectedTextProperty))
            End Get

            Set(ByVal value As String)
                SetValue(SelectedTextProperty, value)
            End Set
        End Property

        Private Shared Sub SelectionStartChangedCallback(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, SelectedTextBehavior).SelectionStartChangedCallback(e)
        End Sub

        Private Shared Sub SelectionLengthChangedCallback(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, SelectedTextBehavior).SelectionLengthChangedCallback(e)
        End Sub

        Private Shared Sub SelectedTextChangedCallback(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, SelectedTextBehavior).SelectedTextChangedCallback(e)
        End Sub

        Private isUpdate As Boolean = False

        Private Sub SelectedTextChangedCallback(ByVal e As DependencyPropertyChangedEventArgs)
            If Not isUpdate Then AssociatedObject.SelectedText = CStr(e.NewValue)
        End Sub

        Private Sub SelectionLengthChangedCallback(ByVal e As DependencyPropertyChangedEventArgs)
            If Not isUpdate Then AssociatedObject.SelectionLength = CInt(e.NewValue)
        End Sub

        Private Sub SelectionStartChangedCallback(ByVal e As DependencyPropertyChangedEventArgs)
            If Not isUpdate Then AssociatedObject.SelectionStart = CInt(e.NewValue)
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.SelectionChanged, AddressOf AssociatedObject_SelectionChanged
            AddHandler AssociatedObject.TextChanged, AddressOf AssociatedObject_TextChanged
        End Sub

        Private Sub AssociatedObject_TextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)
            AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource()
        End Sub

        Private Sub AssociatedObject_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            isUpdate = True
            SelectedText = AssociatedObject.SelectedText
            SelectionStart = AssociatedObject.SelectionStart
            SelectionLength = AssociatedObject.SelectionLength
            isUpdate = False
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.SelectionChanged, AddressOf AssociatedObject_SelectionChanged
            RemoveHandler AssociatedObject.TextChanged, AddressOf AssociatedObject_TextChanged
        End Sub
    End Class
End Namespace
