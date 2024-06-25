using DevExpress.Office;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo {
    public class CustomsRichEditCommandFactoryService : IRichEditCommandFactoryService {
        IRichEditCommandFactoryService _service;
        RichEditControl _control;
        ButtonEdit _searchTextBox;

        public CustomsRichEditCommandFactoryService(RichEditControl richEditControl, IRichEditCommandFactoryService richEditCommandFactoryService, ButtonEdit searchTextBox) {
            this._control = richEditControl;
            this._service = richEditCommandFactoryService;
            this._searchTextBox = searchTextBox;
        }

        RichEditCommand IRichEditCommandFactoryService.CreateCommand(RichEditCommandId id) {
            if (id.Equals(RichEditCommandId.Find))
                return new CustomFindCommand(this._control, this._searchTextBox);
            return this._service.CreateCommand(id);
        }
    }
    public class CustomFindCommand : FindCommand {
        ButtonEdit _searchTextBox;
        char[] _separators = new char[] { Characters.ParagraphMark, Characters.PageBreak, Characters.TabMark };

        public CustomFindCommand(IRichEditControl richEditControl, ButtonEdit searchTextBox)
            : base(richEditControl) {
            this._searchTextBox = searchTextBox;
        }

        protected override void ShowForm(string searchString) {
            this._searchTextBox.Focus();
            string selectedText = Control.Document.GetText(Control.Document.Selection);
            if (string.IsNullOrEmpty(selectedText) || selectedText.IndexOfAny(this._separators) >= 0)
                return;
            this._searchTextBox.Text = selectedText.Trim();
        }
    }
}
