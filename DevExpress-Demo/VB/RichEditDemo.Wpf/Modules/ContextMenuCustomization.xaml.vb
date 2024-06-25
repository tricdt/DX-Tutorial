Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.RichEdit

Namespace RichEditDemo

    Public Partial Class ContextMenuCustomization
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub RichEditControl_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
            If(e.MenuType And RichEditMenuType.Text) <> 0 Then
                If ceBoldText.IsChecked = True Then e.Customizations.Add(New BarButtonItem() With {.Command = RichEditUICommand.FormatFontBold, .Content = "Custom Bold Text", .CommandParameter = richEdit})
                If ceClearFormatting.IsChecked = True Then e.Customizations.Add(New BarButtonItem() With {.Command = RichEditUICommand.FormatClearFormatting, .Content = "Custom Clear Formatting", .CommandParameter = richEdit})
            End If

            If(e.MenuType And RichEditMenuType.TableCell) <> 0 Then
                If ceShowGridlines.IsChecked = True Then e.Customizations.Add(New BarButtonItem() With {.Command = RichEditUICommand.TableToggleShowGridlines, .Content = "Custom Show Gridlines", .CommandParameter = richEdit})
            End If
        End Sub
    End Class
End Namespace
