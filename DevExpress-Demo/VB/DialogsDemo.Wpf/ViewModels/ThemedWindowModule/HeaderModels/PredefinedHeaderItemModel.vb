Imports DevExpress.Mvvm.DataAnnotations

Namespace DialogsDemo

    Public Class HelpHeaderItemModel
        Inherits BaseHeaderItemModel

        Public Property ImageSource As String

        Public Function CanHelp() As Boolean
            Return True
        End Function

        Public Sub Help()
            System.Diagnostics.Process.Start("http://www.devexpress.com")
        End Sub

        Public Sub New()
            ResourceKey = "HelpHeaderItem"
        End Sub
    End Class

    Public Class PinHeaderItemModel
        Inherits BaseHeaderItemModel

        Public Property ImageSource As String

        Public Function CanPin() As Boolean
            Return Not BaseModel.PinWindow
        End Function

        <Command(UseCommandManager:=True)>
        Public Sub Pin()
            BaseModel.PinWindow = True
        End Sub

        Public Sub New()
            ResourceKey = "PinHeaderItem"
        End Sub
    End Class
End Namespace
