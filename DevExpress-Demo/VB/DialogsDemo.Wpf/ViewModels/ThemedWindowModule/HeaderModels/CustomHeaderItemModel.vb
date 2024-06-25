Imports DevExpress.Mvvm

Namespace DialogsDemo

    Public Class CustomHeaderItemModel
        Inherits BaseHeaderItemModel

        Protected Overridable ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property

        Public Property Content As String

        Public Sub Click()
            MessageBoxService.ShowMessage(String.Format("{0} command executed!", Content), "Demo", MessageButton.OK, MessageIcon.Information, MessageResult.OK)
        End Sub

        Public Sub New(ByVal index As Integer)
            ResourceKey = "CustomHeaderItem"
            Content = "Item " & index
        End Sub
    End Class
End Namespace
