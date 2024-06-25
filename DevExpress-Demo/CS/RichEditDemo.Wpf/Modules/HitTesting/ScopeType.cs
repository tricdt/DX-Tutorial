using System.ComponentModel;

namespace RichEditDemo {
    public enum ScopeType {
        [Description("Page")]
        Page,
        [Description("Main Page Area")]
        MainPageArea,
        [Description("Header Page Area")]
        HeaderPageArea,
        [Description("Footer Page Area")]
        FooterPageArea
    }
}
