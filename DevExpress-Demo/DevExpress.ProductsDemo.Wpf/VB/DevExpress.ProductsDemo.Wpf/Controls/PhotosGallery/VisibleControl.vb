Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace ProductsDemo

    <TemplateVisualState(Name:="Pressed", GroupName:="CommonStates"), TemplateVisualState(Name:="MouseOver", GroupName:="CommonStates"), TemplateVisualState(Name:="Normal", GroupName:="CommonStates"), TemplateVisualState(Name:="Unfocused", GroupName:="FocusStates"), TemplateVisualState(Name:="Focused", GroupName:="FocusStates"), TemplateVisualState(Name:="Visible", GroupName:="VisibleStates"), TemplateVisualState(Name:="Invisible", GroupName:="VisibleStates")>
    Public Class VisibleControl
        Inherits Control

        Public Shared ReadOnly VisibleProperty As DependencyProperty = DependencyProperty.Register("Visible", GetType(Boolean), GetType(VisibleControl), New PropertyMetadata(True, New PropertyChangedCallback(AddressOf VisiblePropertyChanged)))

        Private Shared Sub VisiblePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim control As VisibleControl = TryCast(d, VisibleControl)
            If control IsNot Nothing AndAlso Not e.NewValue.Equals(e.OldValue) Then
                control.ChangeVisualState()
                control.VisibleChanged()
            End If
        End Sub

        Public Property Visible As Boolean
            Get
                Return CBool(GetValue(VisibleProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(VisibleProperty, value)
            End Set
        End Property

        Private isPressedField As Boolean = False

        Private isMouseOverField As Boolean = False

        Private isFocusedField As Boolean = False

        Public Event Click As RoutedEventHandler

        Private Overloads Sub ChangeVisualState()
            ChangeVisualState(True)
        End Sub

        Private Overloads Sub ChangeVisualState(ByVal useTransitions As Boolean)
            If Not Visible Then
                VisualStateManager.GoToState(Me, "Invisible", useTransitions)
            Else
                VisualStateManager.GoToState(Me, "Visible", useTransitions)
                If isPressedField Then
                    VisualStateManager.GoToState(Me, "Pressed", useTransitions)
                Else
                    If isMouseOverField Then
                        VisualStateManager.GoToState(Me, "MouseOver", useTransitions)
                    Else
                        VisualStateManager.GoToState(Me, "Normal", useTransitions)
                    End If
                End If
            End If

            If isFocusedField AndAlso Visible Then
                VisualStateManager.GoToState(Me, "Focused", useTransitions)
                Return
            End If

            VisualStateManager.GoToState(Me, "Unfocused", useTransitions)
        End Sub

        Private Sub OnClick()
            RaiseEvent Click(Me, New RoutedEventArgs())
        End Sub

        Protected Overrides Sub OnMouseEnter(ByVal e As MouseEventArgs)
            MyBase.OnMouseEnter(e)
            isMouseOverField = True
            ChangeVisualState()
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As MouseEventArgs)
            MyBase.OnMouseLeave(e)
            isMouseOverField = False
            isPressedField = False
            ChangeVisualState()
        End Sub

        Protected Overrides Sub OnGotFocus(ByVal e As RoutedEventArgs)
            MyBase.OnGotFocus(e)
            isFocusedField = True
            ChangeVisualState()
        End Sub

        Protected Overrides Sub OnLostFocus(ByVal e As RoutedEventArgs)
            MyBase.OnLostFocus(e)
            isFocusedField = False
            ChangeVisualState()
        End Sub

        Protected Overrides Sub OnMouseLeftButtonDown(ByVal e As MouseButtonEventArgs)
            MyBase.OnMouseLeftButtonDown(e)
            isPressedField = True
            ChangeVisualState()
            OnClick()
        End Sub

        Protected Overrides Sub OnMouseLeftButtonUp(ByVal e As MouseButtonEventArgs)
            MyBase.OnMouseLeftButtonUp(e)
            If isPressedField Then
                isPressedField = False
                ChangeVisualState()
            End If
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            ChangeVisualState(False)
        End Sub

        Protected Overridable Sub VisibleChanged()
        End Sub
    End Class
End Namespace
