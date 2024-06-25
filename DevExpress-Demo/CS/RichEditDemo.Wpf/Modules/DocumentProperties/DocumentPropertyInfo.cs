using DevExpress.Mvvm;

namespace RichEditDemo {
    public class DocumentPropertyInfo : BindableBase {
        public DocumentPropertyInfo(string displayName, string name = null) {
            DisplayName = displayName;
            Name = name ?? displayName.ToUpperInvariant();
        }

        public string DisplayName { get; private set; }
        public string Name { get; private set; }
    }
}
