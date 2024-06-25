using System;
using System.Drawing;
using System.ServiceModel.Syndication;
using System.Xml;
using DevExpress.Office;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditDemo {
    public partial class DynamicContent : RichEditDemoModule {
        public DynamicContent() {
            InitializeComponent();
        }

        void RichEditControl_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e) {
            if (e.VariableName == "rssFeed") {
                e.KeepLastParagraph = true;
                e.Value = GenerateRssFeed();
                if (e.Value != null)
                    e.Handled = true;
            }
        }
        RichEditDocumentServer GenerateRssFeed() {
            RichEditDocumentServer server = new RichEditDocumentServer();
            Document document = server.Document;
            AbstractNumberingList abstractNumberingList = document.AbstractNumberingLists.BulletedListTemplate.CreateNew();
            document.NumberingLists.CreateNew(abstractNumberingList.Index);

            SyndicationFeed feed = null;
            try {
                using (XmlReader reader = XmlReader.Create("https://community.devexpress.com/blogs/MainFeed.aspx")) {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch {
                return null;
            }

            document.BeginUpdate();
            foreach (SyndicationItem item in feed.Items)
                AddSyndicationItem(document, item);
            document.EndUpdate();
            return server;
        }
        void AddSyndicationItem(Document document, SyndicationItem item) {
            Paragraph paragraph = document.Paragraphs.Append();
            paragraph.LineSpacing = 1f;
            paragraph.ListIndex = 0;
            paragraph.SpacingAfter = 3;

            DocumentRange range = document.InsertText(paragraph.Range.Start, item.Title.Text);
            CharacterProperties properties = document.BeginUpdateCharacters(range);
            properties.FontSize = 12f;
            properties.FontName = "Segoe UI";
            document.EndUpdateCharacters(properties);

            if (item.Links.Count > 0) {
                Hyperlink hyperlink = document.Hyperlinks.Create(range);
                hyperlink.NavigateUri = item.Links[0].Uri.ToString();
            }

            range = document.InsertText(range.End, String.Format("{0}Published {1}", Characters.LineBreak, item.PublishDate.DateTime));
            properties = document.BeginUpdateCharacters(range);
            properties.FontSize = 8f;
            properties.FontName = "Segoe UI";
            properties.ForeColor = Color.Gray;
            document.EndUpdateCharacters(properties);
        }
    }
}
