Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.RichEdit
Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace DevExpress.DevAV

    Public Class RichEditControlZoomBehavior
        Inherits Behavior(Of RichEditControl)

        Private _ZoomInCommand As ICommand, _ZoomOutCommand As ICommand

        Private Shared minZoomFactor As Single = 0.3F

        Private Shared maxZoomFactor As Single = 1.7F

        Private Shared stepZoomFactor As Single = 0.1F

        Public Property ZoomInCommand As ICommand
            Get
                Return _ZoomInCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ZoomInCommand = value
            End Set
        End Property

        Public Property ZoomOutCommand As ICommand
            Get
                Return _ZoomOutCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ZoomOutCommand = value
            End Set
        End Property

        Public Sub New()
            ZoomInCommand = New DelegateCommand(Sub() AssociatedObject.ActiveView.ZoomFactor += stepZoomFactor, Function() AssociatedObject IsNot Nothing AndAlso AssociatedObject.ActiveView IsNot Nothing AndAlso AssociatedObject.ActiveView.ZoomFactor + stepZoomFactor < maxZoomFactor)
            ZoomOutCommand = New DelegateCommand(Sub() AssociatedObject.ActiveView.ZoomFactor -= stepZoomFactor, Function() AssociatedObject IsNot Nothing AndAlso AssociatedObject.ActiveView IsNot Nothing AndAlso AssociatedObject.ActiveView.ZoomFactor - stepZoomFactor > minZoomFactor)
        End Sub
    End Class
End Namespace
