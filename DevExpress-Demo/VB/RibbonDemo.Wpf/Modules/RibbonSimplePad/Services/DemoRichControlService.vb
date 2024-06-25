Imports DevExpress.Mvvm.UI
Imports System.Windows.Documents

Namespace RibbonDemo

    Public Interface IDemoRichControlService

        Sub InsertImage(ByVal imageViewModel As InlineImageViewModel)

        Function GetViewModelFromContainer() As InlineImageViewModel

        Sub Clear()

    End Interface

    Public Class DemoRichControlService
        Inherits ServiceBase
        Implements IDemoRichControlService

        Protected ReadOnly Property RichControl As DemoRichControl
            Get
                Return TryCast(AssociatedObject, DemoRichControl)
            End Get
        End Property

        Public Sub InsertImage(ByVal imageViewModel As InlineImageViewModel) Implements IDemoRichControlService.InsertImage
            RichControl.InsertContainer(New InlineUIContainer() With {.Child = New InlineImage(imageViewModel)})
        End Sub

        Public Function GetViewModelFromContainer() As InlineImageViewModel Implements IDemoRichControlService.GetViewModelFromContainer
            If RichControl.Container IsNot Nothing Then Return CType(CType(RichControl.Container, InlineUIContainer).Child, InlineImage).InlineImageViewModel
            Return Nothing
        End Function

        Public Sub Clear() Implements IDemoRichControlService.Clear
            RichControl.ClearCommand.Execute(Nothing)
        End Sub
    End Class
End Namespace
