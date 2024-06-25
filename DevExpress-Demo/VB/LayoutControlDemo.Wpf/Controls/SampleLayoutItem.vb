Imports System
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    <TemplateVisualState(Name:=SampleLayoutItem.UnselectedStateName, GroupName:=SampleLayoutItem.SelectionStatesGroupName)>
    <TemplateVisualState(Name:=SampleLayoutItem.SelectedStateName, GroupName:=SampleLayoutItem.SelectionStatesGroupName)>
    Public Class SampleLayoutItem
        Inherits ControlBase
        Implements ISampleLayoutItem

#Region "Dependency Properties"
        Public Shared ReadOnly CaptionProperty As DependencyProperty = DependencyProperty.Register("Caption", GetType(String), GetType(SampleLayoutItem), Nothing)

#End Region  ' Dependency Properties
        Private _IsSelected As Boolean

        Public Sub New()
            DefaultStyleKey = GetType(SampleLayoutItem)
        End Sub

        Public Property Caption As String
            Get
                Return CStr(GetValue(CaptionProperty))
            End Get

            Set(ByVal value As String)
                SetValue(CaptionProperty, value)
            End Set
        End Property

        Public Property IsMaximized As Boolean
            Get
                Return TypeOf Parent Is FlowLayoutControl AndAlso CType(Parent, FlowLayoutControl).MaximizedElement Is Me
            End Get

            Set(ByVal value As Boolean)
                If IsMaximized <> value And TypeOf Parent Is FlowLayoutControl Then CType(Parent, FlowLayoutControl).MaximizedElement = If(value, Me, Nothing)
            End Set
        End Property

        Public Property IsSelected As Boolean Implements ISampleLayoutItem.IsSelected
            Get
                Return _IsSelected
            End Get

            Set(ByVal value As Boolean)
                If IsSelected = value Then Return
                _IsSelected = value
                UpdateState(True)
                RaiseEvent IsSelectedChanged(Me, EventArgs.Empty)
            End Set
        End Property

        Public Event IsSelectedChanged As EventHandler

#Region "Template"
        Friend Const SelectionStatesGroupName As String = "SelectionStates"

        Friend Const UnselectedStateName As String = "Unselected"

        Friend Const SelectedStateName As String = "Selected"

#End Region  ' Template
        Protected Overrides Function CreateController() As ControlControllerBase
            Return New SampleLayoutItemController(Me)
        End Function

        Protected Overrides Sub UpdateState(ByVal useTransitions As Boolean)
            MyBase.UpdateState(useTransitions)
            GoToState(If(IsSelected, SelectedStateName, UnselectedStateName), True)
        End Sub
    End Class

    Public Interface ISampleLayoutItem
        Inherits IControl

        Property IsSelected As Boolean

    End Interface

    Public Class SampleLayoutItemController
        Inherits ControlControllerBase

        Public Sub New(ByVal control As ISampleLayoutItem)
            MyBase.New(control)
        End Sub

        Public ReadOnly Property ISampleLayoutItem As ISampleLayoutItem
            Get
                Return TryCast(IControl, ISampleLayoutItem)
            End Get
        End Property

#Region "Keyboard and Mouse Handling"
        Protected Overrides Sub OnMouseLeftButtonDown(ByVal e As DXMouseButtonEventArgs)
            MyBase.OnMouseLeftButtonDown(e)
            ISampleLayoutItem.IsSelected = True
        End Sub
#End Region  ' Keyboard and Mouse Handling
    End Class
End Namespace
