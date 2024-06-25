Imports System.Globalization
Imports DevExpress.Office.NumberConverters
Imports DevExpress.XtraRichEdit

Namespace RichEditDemo

    Public Module AutoCorrectHelper

        Public Sub AutoCorrect(ByVal e As AutoCorrectEventArgs)
            Dim info As AutoCorrectInfo = e.AutoCorrectInfo
            e.AutoCorrectInfo = Nothing
            If info.Text.Length <= 0 OrElse Not info.Text.Contains("%") Then Return
            Dim characterPosition As Integer = info.Text.IndexOf("%")
            Dim decrementCount As Integer = info.Text.Length - characterPosition - 1
            For i As Integer = 0 To decrementCount - 1
                info.DecrementEndPosition()
            Next

            While True
                If Not info.DecrementStartPosition() Then Return
                If IsSeparator(info.Text(0)) Then Return
                If info.Text(0) = "%"c Then
                    Dim replaceString As String = CalculateFunction(info.Text)
                    If Not String.IsNullOrEmpty(replaceString) Then
                        info.ReplaceWith = replaceString
                        e.AutoCorrectInfo = info
                    End If

                    Return
                End If
            End While
        End Sub

        Private Function IsSeparator(ByVal ch As Char) As Boolean
            Return ch <> "%"c AndAlso (ch = Microsoft.VisualBasic.Strings.ChrW(13) OrElse ch = Microsoft.VisualBasic.Strings.ChrW(10) OrElse Char.IsPunctuation(ch) OrElse Char.IsSeparator(ch) OrElse Char.IsWhiteSpace(ch))
        End Function

        Private Function CalculateFunction(ByVal name As String) As String
            name = name.ToLower()
            If name.Length > 2 AndAlso name(0) = "%"c AndAlso name.EndsWith("%") Then
                Dim value As Integer
                If Integer.TryParse(name.Substring(1, name.Length - 2), value) Then
                    Dim converter As OrdinalBasedNumberConverter = OrdinalBasedNumberConverter.CreateConverter(NumberingFormat.CardinalText, LanguageId.English)
                    Return converter.ConvertNumber(value)
                End If
            End If

            Select Case name
                Case "%date%"
                    Return Date.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)
                Case "%time%"
                    Return Date.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern)
                Case "%bye%"
                    Return "Yours sincerely," & Microsoft.VisualBasic.Constants.vbCrLf & "David B. Smith"
                Case Else
                    Return String.Empty
            End Select
        End Function
    End Module
End Namespace
