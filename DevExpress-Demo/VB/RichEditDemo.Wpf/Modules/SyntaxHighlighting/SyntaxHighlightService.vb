Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text.RegularExpressions
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo

    Public Class SyntaxHighlightService
        Implements ISyntaxHighlightService

        Private ReadOnly _editor As RichEditControl

        Private _keywords As Regex

        Private _quotedString As Regex = New Regex("""[^""\\]*(?:\\.[^""\\]*)*""")

        Private _commentedString As Regex = New Regex("(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/)|(//.*)")

        Public Sub New(ByVal editor As RichEditControl)
            _editor = editor
            Dim keywords As String() = {"break", "boolean", "case", "catch", "class", "const", "continue", "default", "delete", "do", "else", "enum", "export", "extends", "fase", "finally", "for", "function", "if", "import", "in", "new", "null", "return", "super", "switch", "this", "throw", "true", "try", "typeof", "var", "void", "while", "with", "module", "protected", "implements", "interface", "package", "private", "public", "static", "any", "number", "string", "symbol", "abstract", "as", "constructor", "from", "get", "is", "namespace", "of", "set", "type", "let"}
            _keywords = New Regex("\b(" & String.Join("|", keywords.[Select](Function(w) Regex.Escape(w))) & ")\b")
        End Sub

        Public Sub ApplySyntaxHighlight()
            Dim tokens As List(Of SyntaxHighlightToken) = New List(Of SyntaxHighlightToken)()
            Dim ranges As DocumentRange() = _editor.Document.FindAll(_quotedString)
            For Each range As DocumentRange In ranges
                tokens.Add(CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Brown))
            Next

            ranges = _editor.Document.FindAll(_commentedString)
            For Each range As DocumentRange In ranges
                Dim tokenIndex As Integer = FindIntersectedTokenIndex(range, tokens)
                If tokenIndex < 0 Then
                    tokens.Add(CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Green))
                ElseIf range.Start.ToInt() < tokens(tokenIndex).Start Then
                    tokens(tokenIndex) = CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Green)
                End If
            Next

            ranges = _editor.Document.FindAll(_keywords)
            For Each range As DocumentRange In ranges
                If Not IsRangeInTokens(range, tokens) Then tokens.Add(CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Blue))
            Next

            tokens.Sort(Function(token1, token2) token1.Start.CompareTo(token2.Start))
            tokens = CombineWithPlainTextTokens(tokens)
            _editor.Document.ApplySyntaxHighlight(tokens)
        End Sub

        Private Function CombineWithPlainTextTokens(ByVal tokens As List(Of SyntaxHighlightToken)) As List(Of SyntaxHighlightToken)
            Dim result As List(Of SyntaxHighlightToken) = New List(Of SyntaxHighlightToken)(tokens.Count * 2 + 1)
            Dim documentStart As Integer = _editor.Document.Range.Start.ToInt()
            Dim documentEnd As Integer = _editor.Document.Range.End.ToInt()
            If tokens.Count = 0 Then
                result.Add(CreateToken(documentStart, documentEnd, Color.Black))
            Else
                Dim firstToken As SyntaxHighlightToken = tokens(0)
                If documentStart < firstToken.Start Then result.Add(CreateToken(documentStart, firstToken.Start, Color.Black))
                result.Add(firstToken)
                For i As Integer = 1 To tokens.Count - 1
                    Dim token As SyntaxHighlightToken = tokens(i)
                    Dim prevToken As SyntaxHighlightToken = tokens(i - 1)
                    If prevToken.End <> token.Start Then result.Add(CreateToken(prevToken.End, token.Start, Color.Black))
                    result.Add(token)
                Next

                Dim lastToken As SyntaxHighlightToken = tokens(tokens.Count - 1)
                If documentEnd > lastToken.End Then result.Add(CreateToken(lastToken.End, documentEnd, Color.Black))
            End If

            Return result
        End Function

        Private Function CreateToken(ByVal start As Integer, ByVal [end] As Integer, ByVal foreColor As Color) As SyntaxHighlightToken
            Dim properties As SyntaxHighlightProperties = New SyntaxHighlightProperties()
            properties.ForeColor = foreColor
            Return New SyntaxHighlightToken(start, [end] - start, properties)
        End Function

        Private Function IsRangeInTokens(ByVal range As DocumentRange, ByVal tokens As List(Of SyntaxHighlightToken)) As Boolean
            Return tokens.Any(Function(t) IsIntersect(range, t))
        End Function

        Private Function FindIntersectedTokenIndex(ByVal range As DocumentRange, ByVal tokens As List(Of SyntaxHighlightToken)) As Integer
            Return tokens.FindIndex(Function(t) IsIntersect(range, t))
        End Function

        Private Function IsIntersect(ByVal range As DocumentRange, ByVal token As SyntaxHighlightToken) As Boolean
            Dim start As Integer = range.Start.ToInt()
            Dim [end] As Integer = range.End.ToInt()
            Return start < token.End AndAlso token.Start < [end]
        End Function

        Private Sub ForceExecute() Implements ISyntaxHighlightService.ForceExecute
            ApplySyntaxHighlight()
        End Sub

        Private Sub Execute() Implements ISyntaxHighlightService.Execute
            ApplySyntaxHighlight()
        End Sub
    End Class
End Namespace
