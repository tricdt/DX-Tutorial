Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Namespace GridDemo

    Public Class ResizeableDataRow
        Inherits Control
        Implements IResizeHelperOwner

        Private Shared ReadOnly random As Random = New Random(1)

        Public Shared ReadOnly RowHeightProperty As DependencyProperty

        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(ResizeableDataRow), New FrameworkPropertyMetadata(GetType(ResizeableDataRow)))
            RowHeightProperty = DependencyProperty.RegisterAttached("RowHeight", GetType(Double), GetType(ResizeableDataRow), New PropertyMetadata(0R))
            Call RowData.RowDataProperty.OverrideMetadata(GetType(ResizeableDataRow), New FrameworkPropertyMetadata(AddressOf OnScrollViewerVerticalOffsetChanged))
        End Sub

        Private Shared Sub OnScrollViewerVerticalOffsetChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ResizeableDataRow).OnRowDataChanged(CType(e.OldValue, RowData), CType(e.NewValue, RowData))
        End Sub

        Public Shared Sub SetRowHeight(ByVal element As DependencyObject, ByVal value As Double)
            element.SetValue(RowHeightProperty, value)
        End Sub

        Public Shared Function GetRowHeight(ByVal element As DependencyObject) As Double
            Return CDbl(element.GetValue(RowHeightProperty))
        End Function

        Private resizeHelper As ResizeHelper

        Private ReadOnly Property RowData As RowData
            Get
                Return CType(DataContext, RowData)
            End Get
        End Property

        Private Property RowHeight As Double
            Get
                Return GetRowHeight(RowData.RowState)
            End Get

            Set(ByVal value As Double)
                SetRowHeight(RowData.RowState, value)
            End Set
        End Property

        Public Sub New()
            resizeHelper = New ResizeHelper(Me)
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Dim resizer As Thumb = CType(GetTemplateChild("PART_Resizer"), Thumb)
            resizeHelper.Init(resizer)
            InitializeRowHeight()
        End Sub

        Private Sub OnRowDataChanged(ByVal oldValue As RowData, ByVal newValue As RowData)
            If oldValue IsNot Nothing Then RemoveHandler oldValue.ContentChanged, New EventHandler(AddressOf RowData_ContentChanged)
            If newValue IsNot Nothing Then
                AddHandler newValue.ContentChanged, New EventHandler(AddressOf RowData_ContentChanged)
                InitializeRowHeight()
            End If
        End Sub

        Private Sub RowData_ContentChanged(ByVal sender As Object, ByVal e As EventArgs)
            InitializeRowHeight()
        End Sub

        Private Sub InitializeRowHeight()
            If RowHeight = 0 Then RowHeight = 75 + 80 * random.NextDouble()
        End Sub

#Region "IResizeHelperOwner Members"
        Private Property ActualSize As Double Implements IResizeHelperOwner.ActualSize
            Get
                Return RowHeight
            End Get

            Set(ByVal value As Double)
                RowHeight = value
            End Set
        End Property

        Private Sub ChangeSize(ByVal delta As Double) Implements IResizeHelperOwner.ChangeSize
            RowHeight = Math.Min(300, Math.Max(20, RowHeight + delta))
        End Sub

        Private Sub OnDoubleClick() Implements IResizeHelperOwner.OnDoubleClick
        End Sub

        Private Sub SetIsResizing(ByVal isResizing As Boolean) Implements IResizeHelperOwner.SetIsResizing
        End Sub

        Private ReadOnly Property SizeHelper As SizeHelperBase Implements IResizeHelperOwner.SizeHelper
            Get
                Return VerticalSizeHelper.Instance
            End Get
        End Property
#End Region
    End Class

    Public Class ResizeableCard
        Inherits Control

        Public Shared ReadOnly ScaleFactorProperty As DependencyProperty

        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(ResizeableCard), New FrameworkPropertyMetadata(GetType(ResizeableCard)))
            ScaleFactorProperty = DependencyProperty.RegisterAttached("ScaleFactor", GetType(Double), GetType(ResizeableCard), New PropertyMetadata(1R))
        End Sub

        Public Shared Sub SetScaleFactor(ByVal element As DependencyObject, ByVal value As Double)
            element.SetValue(ScaleFactorProperty, value)
        End Sub

        Public Shared Function GetScaleFactor(ByVal element As DependencyObject) As Double
            Return CDbl(element.GetValue(ScaleFactorProperty))
        End Function
    End Class

    Public Module UnsafeNativeMethods

        <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
        Public Function SetCursorPos(ByVal x As Integer, ByVal y As Integer) As Boolean
        End Function
    End Module

    Public Class CommandManagerAttachedBehavior
        Inherits Behavior(Of FrameworkElement)

        Public Shared ReadOnly CanExecuteHandlerCommandProperty As DependencyProperty

        Shared Sub New()
            CanExecuteHandlerCommandProperty = DependencyProperty.Register("CanExecuteHandlerCommand", GetType(ICommand), GetType(CommandManagerAttachedBehavior))
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Call CommandManager.AddCanExecuteHandler(AssociatedObject, New CanExecuteRoutedEventHandler(AddressOf CanExecute))
        End Sub

        Protected Overrides Sub OnDetaching()
            Call CommandManager.RemoveCanExecuteHandler(AssociatedObject, New CanExecuteRoutedEventHandler(AddressOf CanExecute))
            MyBase.OnDetaching()
        End Sub

        Private Sub CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            If CanExecuteHandlerCommand Is Nothing Then Return
            CanExecuteHandlerCommand.Execute(e)
        End Sub

        Public Property CanExecuteHandlerCommand As ICommand
            Get
                Return CType(GetValue(CanExecuteHandlerCommandProperty), ICommand)
            End Get

            Set(ByVal value As ICommand)
                SetValue(CanExecuteHandlerCommandProperty, value)
            End Set
        End Property
    End Class
End Namespace
