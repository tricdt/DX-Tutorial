Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Namespace ControlsDemo

    <CodeFile("ViewModels/TaskbarServicesViewModel.(cs)")>
    <CodeFile("Helpers/ImageNameConverter.(cs)")>
    Public Partial Class TaskbarServices
        Inherits DemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Clear()
            Try
                Dim disposableViewModel As IDisposable = TryCast(ViewHelper.GetViewModelFromView(Me), IDisposable)
                If disposableViewModel IsNot Nothing Then disposableViewModel.Dispose()
            Finally
                MyBase.Clear()
            End Try
        End Sub
    End Class

    Public Class ReverseStackPanel
        Inherits Panel

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            Dim childAvailableSize As Size = New Size(availableSize.Width, Double.PositiveInfinity)
            Dim size As Size = New Size()
            For Each child As UIElement In InternalChildren
                child.Measure(childAvailableSize)
                size.Width = Math.Max(size.Width, child.DesiredSize.Width)
                size.Height += child.DesiredSize.Height
            Next

            Return size
        End Function

        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            Dim y As Double = 0.0
            For Each child As UIElement In InternalChildren.Cast(Of UIElement)().Reverse()
                Dim childHeight As Double = child.DesiredSize.Height
                child.Arrange(New Rect(New Point(0.0, y), New Size(finalSize.Width, childHeight)))
                y += childHeight
            Next

            Return finalSize
        End Function
    End Class
End Namespace
