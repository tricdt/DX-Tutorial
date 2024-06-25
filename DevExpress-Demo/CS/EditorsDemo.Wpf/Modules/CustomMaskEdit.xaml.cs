using DevExpress.Data.Mask;
using DevExpress.Xpf.Editors;
using System.Text;

namespace EditorsDemo {
    
    
    
    public partial class CustomMaskEdit : EditorsDemoModule {
        public CustomMaskEdit() {
            InitializeComponent();
        }
        private void OnCustomMask(object sender, CustomMaskEventArgs args) {
            var template = mask.SelectedCardInfo.Template;
            if (args.ActionType != CustomTextMaskInputAction.Init && (template.Length < args.ResultEditText.Length || !IsNumbericText(args.InsertedText))) {
                args.Cancel();
                return;
            }

            if (args.ActionType == CustomTextMaskInputAction.Backspace && args.CurrentCursorPosition == 0) return;
            if (args.ActionType == CustomTextMaskInputAction.Delete && args.CurrentCursorPosition == args.CurrentEditText.Length) return;

            var cursor = args.ResultCursorPosition;
            var editText = args.ResultEditText.Replace(" ", "");
            var editTextPosition = 0;
            var templatePosition = 0;
            StringBuilder result = new StringBuilder();
            foreach (var tItem in template) {
                if (editText.Length <= editTextPosition) break;
                if (tItem != 'X') {
                    result.Append(tItem);
                    if ((templatePosition == args.CurrentCursorPosition && args.ActionType!= CustomTextMaskInputAction.Backspace) || (args.CurrentCursorPosition == 0 && args.CurrentEditText.Length < 1)) {
                        cursor++;
                    }
                } else
                    result.Append(editText[editTextPosition++]);
                templatePosition++;
            }
            args.SetResult(result.ToString(), cursor > result.Length ? result.Length : cursor);
        }

        bool IsNumbericText(string insertedText) {
            foreach (var ch in insertedText.ToCharArray()) {
                if (!char.IsDigit(ch)) return false;
            }
            return true;
        }
    }
}
