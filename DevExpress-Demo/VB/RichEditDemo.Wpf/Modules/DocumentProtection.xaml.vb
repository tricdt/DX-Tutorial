Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.XtraRichEdit.API.Native
Imports System.Linq

Namespace RichEditDemo

    Public Partial Class DocumentProtection
        Inherits RichEditDemoModule

        Private ReadOnly userService As UserService = New UserService()

        Public Sub New()
            InitializeComponent()
            richEdit.ReplaceService(userService)
        End Sub

        Private Sub RichEditControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddHandler richEdit.DocumentLoaded, AddressOf RichEditControl_DocumentLoaded
            richEdit.DocumentSource = DemoUtils.GetRelativePath("DocumentProtection.docx")
        End Sub

        Private Sub RichEditControl_DocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            Dim rangePermissions As RangePermissionCollection = richEdit.Document.BeginUpdateRangePermissions()
            richEdit.Document.CancelUpdateRangePermissions(rangePermissions)
            Dim users As List(Of String) = rangePermissions.[Select](Function(rangePermission) rangePermission.UserName).Where(Function(name) Not String.IsNullOrEmpty(name)).Distinct().ToList()
            userService.Update(users)
            cbUserName.ItemsSource = users
            If users.Count > 0 Then cbUserName.EditValue = users(0)
        End Sub
    End Class
End Namespace
