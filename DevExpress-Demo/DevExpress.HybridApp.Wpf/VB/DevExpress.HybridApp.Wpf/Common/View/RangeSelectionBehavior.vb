Imports System
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Editors.RangeControl

Namespace DevExpress.DevAV

    Public Class RangeSelectionBehavior
        Inherits Behavior(Of RangeControl)

        Private _MoveRangeLeftCommand As ICommand, _MoveRangeRightCommand As ICommand

        Public Sub New()
            MoveRangeLeftCommand = New DelegateCommand(Sub() MoveRangeLeft(), AddressOf CanMoveRangeLeft)
            MoveRangeRightCommand = New DelegateCommand(Sub() MoveRangeRight(), AddressOf CanMoveRangeRight)
        End Sub

        Private Shared ReadOnly SelectionStartProperty As DependencyProperty = DependencyProperty.Register("SelectionStart", GetType(Object), GetType(RangeSelectionBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, RangeSelectionBehavior).SelectionChanged()))

        Private Shared ReadOnly SelectionEndProperty As DependencyProperty = DependencyProperty.Register("SelectionEnd", GetType(Object), GetType(RangeSelectionBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, RangeSelectionBehavior).SelectionChanged()))

        Public Property MoveRangeLeftCommand As ICommand
            Get
                Return _MoveRangeLeftCommand
            End Get

            Private Set(ByVal value As ICommand)
                _MoveRangeLeftCommand = value
            End Set
        End Property

        Public Property MoveRangeRightCommand As ICommand
            Get
                Return _MoveRangeRightCommand
            End Get

            Private Set(ByVal value As ICommand)
                _MoveRangeRightCommand = value
            End Set
        End Property

        Public Property MinimumRangeChange As Integer

        Private Sub MoveRangeLeft()
            Dim rangeChange As TimeSpan = GetRangeChange()
            AssociatedObject.SelectionRangeStart = CDate(AssociatedObject.SelectionRangeStart) - rangeChange
            AssociatedObject.SelectionRangeEnd = CDate(AssociatedObject.SelectionRangeEnd) - rangeChange
        End Sub

        Private Sub MoveRangeRight()
            Dim rangeChange As TimeSpan = GetRangeChange()
            AssociatedObject.SelectionRangeEnd = CDate(AssociatedObject.SelectionRangeEnd) + rangeChange
            AssociatedObject.SelectionRangeStart = CDate(AssociatedObject.SelectionRangeStart) + rangeChange
        End Sub

        Private Function CanMoveRangeLeft() As Boolean
            Return If(HasNullValues(), False, CDate(AssociatedObject.SelectionRangeStart) - CDate(AssociatedObject.RangeStart) > GetRangeChange())
        End Function

        Private Function CanMoveRangeRight() As Boolean
            Return If(HasNullValues(), False, CDate(AssociatedObject.RangeEnd) - CDate(AssociatedObject.SelectionRangeEnd) > GetRangeChange())
        End Function

        Private Function GetRangeChange() As TimeSpan
            Dim rangeChange As TimeSpan = CDate(AssociatedObject.SelectionRangeEnd) - CDate(AssociatedObject.SelectionRangeStart)
            If rangeChange.TotalDays < MinimumRangeChange Then rangeChange = New TimeSpan(MinimumRangeChange, 0, 0, 0, 0)
            Return rangeChange
        End Function

        Private Function HasNullValues() As Boolean
            Return AssociatedObject Is Nothing OrElse AssociatedObject.RangeStart Is Nothing OrElse AssociatedObject.RangeEnd Is Nothing OrElse AssociatedObject.SelectionRangeStart Is Nothing OrElse AssociatedObject.SelectionRangeEnd Is Nothing
        End Function

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Call BindingOperations.SetBinding(Me, SelectionStartProperty, New Binding(RangeControl.SelectionRangeStartProperty.Name) With {.Source = AssociatedObject})
            Call BindingOperations.SetBinding(Me, SelectionEndProperty, New Binding(RangeControl.SelectionRangeEndProperty.Name) With {.Source = AssociatedObject})
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            BindingOperations.ClearBinding(Me, SelectionStartProperty)
            BindingOperations.ClearBinding(Me, SelectionEndProperty)
        End Sub

        Private Sub SelectionChanged()
            CType(MoveRangeLeftCommand, DelegateCommand).RaiseCanExecuteChanged()
            CType(MoveRangeRightCommand, DelegateCommand).RaiseCanExecuteChanged()
        End Sub
    End Class
End Namespace
