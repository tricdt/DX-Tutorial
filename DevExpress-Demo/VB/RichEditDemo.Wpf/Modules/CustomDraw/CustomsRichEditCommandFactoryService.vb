Imports DevExpress.Office
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo

    Public Class CustomsRichEditCommandFactoryService
        Implements IRichEditCommandFactoryService

        Private _service As IRichEditCommandFactoryService

        Private _control As RichEditControl

        Private _searchTextBox As ButtonEdit

        Public Sub New(ByVal richEditControl As RichEditControl, ByVal richEditCommandFactoryService As IRichEditCommandFactoryService, ByVal searchTextBox As ButtonEdit)
            _control = richEditControl
            _service = richEditCommandFactoryService
            _searchTextBox = searchTextBox
        End Sub

        Private Function CreateCommand(ByVal id As RichEditCommandId) As RichEditCommand Implements IRichEditCommandFactoryService.CreateCommand
            If id.Equals(RichEditCommandId.Find) Then Return New CustomFindCommand(_control, _searchTextBox)
            Return _service.CreateCommand(id)
        End Function
    End Class

    Public Class CustomFindCommand
        Inherits FindCommand

        Private _searchTextBox As ButtonEdit

        Private _separators As Char() = New Char() {Characters.ParagraphMark, Characters.PageBreak, Characters.TabMark}

        Public Sub New(ByVal richEditControl As IRichEditControl, ByVal searchTextBox As ButtonEdit)
            MyBase.New(richEditControl)
            _searchTextBox = searchTextBox
        End Sub

        Protected Overrides Sub ShowForm(ByVal searchString As String)
            _searchTextBox.Focus()
            Dim selectedText As String = Control.Document.GetText(Control.Document.Selection)
            If String.IsNullOrEmpty(selectedText) OrElse selectedText.IndexOfAny(_separators) >= 0 Then Return
            _searchTextBox.Text = selectedText.Trim()
        End Sub
    End Class
End Namespace
