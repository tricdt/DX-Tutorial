Imports System
Imports System.Windows.Controls

Namespace ControlsDemo.GalleryDemo

    Public Enum ControlPanelCommand
        Print
        HorSize
        VerSize
        RotateLeft
        RotateRight
        Prior
        [Next]
        ZoomToOriginalSize
        AutoSize
        Play
        [Stop]
        ZoomValueChanged
    End Enum

    Public Class ControlPanelEventArgs
        Inherits EventArgs

        Private _Command As ControlPanelCommand

        Public Property Command As ControlPanelCommand
            Get
                Return _Command
            End Get

            Protected Set(ByVal value As ControlPanelCommand)
                _Command = value
            End Set
        End Property

        Public Sub New(ByVal command As ControlPanelCommand)
            Me.Command = command
        End Sub
    End Class

    Public Delegate Sub ControlPanelCommandClickEventHandler(ByVal sender As Object, ByVal e As ControlPanelEventArgs)

    Public Partial Class ControlPanel
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Property ZoomValue As Double
            Get
                Return ZoomScroll.Value
            End Get

            Set(ByVal value As Double)
                ZoomScroll.SetZoomValue(value, 0)
            End Set
        End Property

        Public Sub SetAndAnimateZoomValue(ByVal value As Double)
            ZoomScroll.SetZoomValue(value, 0.5)
        End Sub

        Public Event CommandClick As ControlPanelCommandClickEventHandler

        Protected Overridable Sub OnPrintClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.Print))
        End Sub

        Protected Overridable Sub OnHorSizeClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.HorSize))
        End Sub

        Protected Overridable Sub OnVerSizeClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.VerSize))
        End Sub

        Protected Overridable Sub OnRotateLeftClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.RotateLeft))
        End Sub

        Protected Overridable Sub OnRotateRightClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.RotateRight))
        End Sub

        Protected Overridable Sub OnPrevClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.Prior))
        End Sub

        Protected Overridable Sub OnNextClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.Next))
        End Sub

        Protected Overridable Sub On1to1Click(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.ZoomToOriginalSize))
        End Sub

        Protected Overridable Sub OnAutoSizeClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.AutoSize))
        End Sub

        Protected Overridable Sub OnPlayClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.Play))
        End Sub

        Private Sub ZoomScroll_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CommandClick(Me, New ControlPanelEventArgs(ControlPanelCommand.ZoomValueChanged))
        End Sub
    End Class
End Namespace
