Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native

Namespace PropertyGridDemo

    Public Class SelectedElementVisualizer
        Inherits Grid

        Public Shared ReadOnly SelectedElementProperty As DependencyProperty

        Shared Sub New()
            Dim ownerType As Type = GetType(SelectedElementVisualizer)
            SelectedElementProperty = DependencyProperty.Register("SelectedElement", GetType(FrameworkElement), ownerType, New FrameworkPropertyMetadata(Nothing, FrameworkPropertyMetadataOptions.AffectsMeasure, AddressOf PropertyChangedCallback))
        End Sub

        Private Shared Sub PropertyChangedCallback(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, SelectedElementVisualizer).SelectedElementChanged(CType(e.OldValue, FrameworkElement), CType(e.NewValue, FrameworkElement))
        End Sub

        Private ReadOnly childrenLocker As Locker = New Locker()

        Public Sub New()
            Selector = New Selector()
        End Sub

        Protected Overrides Sub OnVisualChildrenChanged(ByVal visualAdded As DependencyObject, ByVal visualRemoved As DependencyObject)
            MyBase.OnVisualChildrenChanged(visualAdded, visualRemoved)
            childrenLocker.DoLockedActionIfNotLocked(New Action(AddressOf ChildrenUpdate))
        End Sub

        Private Sub ChildrenUpdate()
            Children.Remove(Selector)
            Children.Add(Selector)
        End Sub

        Private Property Selector As Selector

        Public Property SelectedElement As FrameworkElement
            Get
                Return CType(GetValue(SelectedElementProperty), FrameworkElement)
            End Get

            Set(ByVal value As FrameworkElement)
                SetValue(SelectedElementProperty, value)
            End Set
        End Property

        Protected Overridable Sub SelectedElementChanged(ByVal oldElement As FrameworkElement, ByVal newElement As FrameworkElement)
            If oldElement IsNot Nothing Then AddHandler oldElement.LayoutUpdated, AddressOf Me.OnSelectedElementLayoutUpdated
            If newElement IsNot Nothing Then AddHandler newElement.SizeChanged, AddressOf Me.OnSelectedElementLayoutUpdated
            BuildLayout()
        End Sub

        Private Sub OnSelectedElementLayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            BuildLayout()
        End Sub

        Private Sub BuildLayout()
            If Not LayoutHelper.IsChildElement(Me, SelectedElement) Then
                Selector.Visibility = Visibility.Collapsed
                Return
            End If

            Selector.Visibility = Visibility.Visible
            Dim rect As Rect = LayoutHelper.GetRelativeElementRect(SelectedElement, Me)
            Selector.Margin = New Thickness(rect.Left, rect.Top, ActualWidth - rect.Left - rect.Width, ActualHeight - rect.Top - rect.Height)
        End Sub
    End Class

    Public Class Selector
        Inherits Grid

        Public Sub New()
            Dim border As Border = New Border() With {.Margin = New Thickness(-2), .BorderBrush = Brushes.Red, .BorderThickness = New Thickness(2), .Opacity = 0.35, .HorizontalAlignment = HorizontalAlignment.Stretch, .VerticalAlignment = VerticalAlignment.Stretch}
            Children.Add(border)
        End Sub
    End Class
End Namespace
