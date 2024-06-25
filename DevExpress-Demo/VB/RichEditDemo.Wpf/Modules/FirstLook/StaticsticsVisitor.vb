Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Linq
Imports System.Text

Namespace RichEditDemo

    Public Class StaticsticsVisitor
        Inherits DocumentVisitorBase

        Private ReadOnly buffer As StringBuilder

        Private ReadOnly includeTextboxes As Boolean

        Private noSpacesCharacterCountField As Integer

        Private withSpacesCharacterCountField As Integer

        Private wordCountField As Integer

        Private paragraphCountField As Integer

        Public Sub New(ByVal includeTextboxes As Boolean)
            buffer = New StringBuilder()
            Me.includeTextboxes = includeTextboxes
        End Sub

        Public ReadOnly Property NoSpacesCharacterCount As Integer
            Get
                Return noSpacesCharacterCountField
            End Get
        End Property

        Public ReadOnly Property WithSpacesCharacterCount As Integer
            Get
                Return withSpacesCharacterCountField
            End Get
        End Property

        Public ReadOnly Property WordCount As Integer
            Get
                Return wordCountField
            End Get
        End Property

        Public ReadOnly Property ParagraphCount As Integer
            Get
                Return paragraphCountField
            End Get
        End Property

        Public Overrides Sub Visit(ByVal text As DocumentText)
            buffer.Append(text.Text)
        End Sub

        Public Overrides Sub Visit(ByVal textBox As DocumentTextBox)
            If Not includeTextboxes Then Return
            Dim iterator As DocumentIterator = textBox.GetIterator(True)
            Dim visitor As StaticsticsVisitor = New StaticsticsVisitor(False)
            While iterator.MoveNext()
                iterator.Current.Accept(visitor)
            End While

            noSpacesCharacterCountField += visitor.NoSpacesCharacterCount
            withSpacesCharacterCountField += visitor.WithSpacesCharacterCount
            wordCountField += visitor.WordCount
            paragraphCountField += visitor.ParagraphCount
        End Sub

        Public Overrides Sub Visit(ByVal paragraphEnd As DocumentParagraphEnd)
            FinishParagraph()
        End Sub

        Public Overrides Sub Visit(ByVal sectionEnd As DocumentSectionEnd)
            FinishParagraph()
        End Sub

        Private Sub FinishParagraph()
            Dim text As String = buffer.ToString()
            noSpacesCharacterCountField += text.Count(Function(c) Not Char.IsWhiteSpace(c))
            withSpacesCharacterCountField += text.Length
            wordCountField += text.Split((New Char() {" "c, "."c, "!"c, "?"c}), StringSplitOptions.RemoveEmptyEntries).Length
            If Not String.IsNullOrWhiteSpace(text) Then paragraphCountField += 1
            buffer.Length = 0
        End Sub
    End Class
End Namespace
