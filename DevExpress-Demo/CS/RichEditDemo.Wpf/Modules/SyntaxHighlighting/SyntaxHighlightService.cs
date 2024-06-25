using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo {
    public class SyntaxHighlightService : ISyntaxHighlightService {
        readonly RichEditControl _editor;
        Regex _keywords;
        Regex _quotedString = new Regex(@"""[^""\\]*(?:\\.[^""\\]*)*""");
        Regex _commentedString = new Regex(@"(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/)|(//.*)");

        public SyntaxHighlightService(RichEditControl editor) {
            this._editor = editor;
            string[] keywords = { "break", "boolean", "case", "catch", "class", "const", "continue", "default", "delete", "do", "else", "enum", "export", "extends", "fase", "finally", "for", "function", "if", "import", "in", "new", "null", "return", "super", "switch", "this", "throw", "true", "try", "typeof", "var", "void", "while", "with", "module", "protected", "implements", "interface", "package", "private", "public", "static", "any", "number", "string", "symbol", "abstract", "as", "constructor", "from", "get", "is", "namespace", "of", "set", "type", "let" };
            this._keywords = new Regex(@"\b(" + string.Join("|", keywords.Select(w => Regex.Escape(w))) + @")\b");
        }

        public void ApplySyntaxHighlight() {
            List<SyntaxHighlightToken> tokens = new List<SyntaxHighlightToken>();
            DocumentRange[] ranges = this._editor.Document.FindAll(this._quotedString);
            foreach (DocumentRange range in ranges)
                tokens.Add(CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Brown));

            ranges = this._editor.Document.FindAll(this._commentedString);
            foreach (DocumentRange range in ranges) {
                int tokenIndex = FindIntersectedTokenIndex(range, tokens);
                if (tokenIndex < 0)
                    tokens.Add(CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Green));
                else if (range.Start.ToInt() < tokens[tokenIndex].Start)
                    tokens[tokenIndex] = CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Green);
            }

            ranges = this._editor.Document.FindAll(this._keywords);
            foreach (DocumentRange range in ranges) {
                if (!IsRangeInTokens(range, tokens))
                    tokens.Add(CreateToken(range.Start.ToInt(), range.End.ToInt(), Color.Blue));
            }

            tokens.Sort((token1, token2) => { return token1.Start.CompareTo(token2.Start); });
            tokens = CombineWithPlainTextTokens(tokens);
            this._editor.Document.ApplySyntaxHighlight(tokens);
        }
        List<SyntaxHighlightToken> CombineWithPlainTextTokens(List<SyntaxHighlightToken> tokens) {
            List<SyntaxHighlightToken> result = new List<SyntaxHighlightToken>(tokens.Count * 2 + 1);
            int documentStart = this._editor.Document.Range.Start.ToInt();
            int documentEnd = this._editor.Document.Range.End.ToInt();
            if (tokens.Count == 0)
                result.Add(CreateToken(documentStart, documentEnd, Color.Black));
            else {
                SyntaxHighlightToken firstToken = tokens[0];
                if (documentStart < firstToken.Start)
                    result.Add(CreateToken(documentStart, firstToken.Start, Color.Black));
                result.Add(firstToken);
                for (int i = 1; i < tokens.Count; i++) {
                    SyntaxHighlightToken token = tokens[i];
                    SyntaxHighlightToken prevToken = tokens[i - 1];
                    if (prevToken.End != token.Start)
                        result.Add(CreateToken(prevToken.End, token.Start, Color.Black));
                    result.Add(token);
                }
                SyntaxHighlightToken lastToken = tokens[tokens.Count - 1];
                if (documentEnd > lastToken.End)
                    result.Add(CreateToken(lastToken.End, documentEnd, Color.Black));
            }
            return result;
        }
        SyntaxHighlightToken CreateToken(int start, int end, Color foreColor) {
            SyntaxHighlightProperties properties = new SyntaxHighlightProperties();
            properties.ForeColor = foreColor;
            return new SyntaxHighlightToken(start, end - start, properties);
        }
        bool IsRangeInTokens(DocumentRange range, List<SyntaxHighlightToken> tokens) {
            return tokens.Any(t => IsIntersect(range, t));
        }
        int FindIntersectedTokenIndex(DocumentRange range, List<SyntaxHighlightToken> tokens) {
            return tokens.FindIndex(t => IsIntersect(range, t));
        }
        bool IsIntersect(DocumentRange range, SyntaxHighlightToken token) {
            int start = range.Start.ToInt();
            int end = range.End.ToInt();
            return start < token.End && token.Start < end;
        }
        void ISyntaxHighlightService.ForceExecute() {
            ApplySyntaxHighlight();
        }
        void ISyntaxHighlightService.Execute() {
            ApplySyntaxHighlight();
        }
    }
}
