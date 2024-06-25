using System.Collections.Generic;

namespace BarsDemo {
    public class CommandModel {
        public string Caption { get; set; }
        public string Glyph { get; set; }
    }
    public class GroupModel : CommandModel {
        public List<CommandModel> Commands { get; set; }
    }
    public class EditorModel : CommandModel {
        public object EditValue { get; set; }
    }
    public class LabelModel : CommandModel {
        public object Content { get; set; }
    }
}
