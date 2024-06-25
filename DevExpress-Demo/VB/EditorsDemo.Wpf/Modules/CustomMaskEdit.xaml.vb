Imports DevExpress.Data.Mask
Imports DevExpress.Xpf.Editors
Imports System.Text

Namespace EditorsDemo

    Public Partial Class CustomMaskEdit
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnCustomMask(ByVal sender As Object, ByVal args As CustomMaskEventArgs)
            Dim template = mask.SelectedCardInfo.Template
            If args.ActionType <> CustomTextMaskInputAction.Init AndAlso (template.Length < args.ResultEditText.Length OrElse Not IsNumbericText(args.InsertedText)) Then
                args.Cancel()
                Return
            End If

            If args.ActionType = CustomTextMaskInputAction.Backspace AndAlso args.CurrentCursorPosition = 0 Then Return
            If args.ActionType = CustomTextMaskInputAction.Delete AndAlso args.CurrentCursorPosition = args.CurrentEditText.Length Then Return
            Dim cursor = args.ResultCursorPosition
            Dim editText = args.ResultEditText.Replace(" ", "")
            Dim editTextPosition = 0
            Dim templatePosition = 0
            Dim result As StringBuilder = New StringBuilder()
            For Each tItem In template
                If editText.Length <= editTextPosition Then Exit For
                If tItem <> "X"c Then
                    result.Append(tItem)
                    If(templatePosition = args.CurrentCursorPosition AndAlso args.ActionType <> CustomTextMaskInputAction.Backspace) OrElse args.CurrentCursorPosition = 0 AndAlso args.CurrentEditText.Length < 1 Then
                        cursor += 1
                    End If
                Else
                    result.Append(editText(System.Math.Min(System.Threading.Interlocked.Increment(editTextPosition), editTextPosition - 1)))
                End If

                templatePosition += 1
            Next

            args.SetResult(result.ToString(), If(cursor > result.Length, result.Length, cursor))
        End Sub

        Private Function IsNumbericText(ByVal insertedText As String) As Boolean
            For Each ch In insertedText.ToCharArray()
                If Not Char.IsDigit(ch) Then Return False
            Next

            Return True
        End Function
    End Class
End Namespace
