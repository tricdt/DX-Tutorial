using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Linq;
using System.Text;

namespace RichEditDemo {
    public class StaticsticsVisitor : DocumentVisitorBase {
        readonly StringBuilder buffer;
        readonly bool includeTextboxes;
        int noSpacesCharacterCount;
        int withSpacesCharacterCount;
        int wordCount;
        int paragraphCount;

        public StaticsticsVisitor(bool includeTextboxes) {
            this.buffer = new StringBuilder();
            this.includeTextboxes = includeTextboxes;
        }

        public int NoSpacesCharacterCount { get { return noSpacesCharacterCount; } }
        public int WithSpacesCharacterCount { get { return withSpacesCharacterCount; } }
        public int WordCount { get { return wordCount; } }
        public int ParagraphCount { get { return paragraphCount; } }

        public override void Visit(DocumentText text) {
            this.buffer.Append(text.Text);
        }
        public override void Visit(DocumentTextBox textBox) {
            if(!this.includeTextboxes)
                return;
            DocumentIterator iterator = textBox.GetIterator(true);
            StaticsticsVisitor visitor = new StaticsticsVisitor(false);
            while(iterator.MoveNext())
                iterator.Current.Accept(visitor);
            this.noSpacesCharacterCount += visitor.NoSpacesCharacterCount;
            this.withSpacesCharacterCount += visitor.WithSpacesCharacterCount;
            this.wordCount += visitor.WordCount;
            this.paragraphCount += visitor.ParagraphCount;
        }
        public override void Visit(DocumentParagraphEnd paragraphEnd) {
            FinishParagraph();
        }
        public override void Visit(DocumentSectionEnd sectionEnd) {
            FinishParagraph();
        }
        void FinishParagraph() {
            string text = this.buffer.ToString();
            this.noSpacesCharacterCount += text.Count(c => !Char.IsWhiteSpace(c));
            this.withSpacesCharacterCount += text.Length;
            this.wordCount += text.Split(new char[] { ' ', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
            if(!string.IsNullOrWhiteSpace(text))
                this.paragraphCount++;
            this.buffer.Length = 0;
        }
    }
}
