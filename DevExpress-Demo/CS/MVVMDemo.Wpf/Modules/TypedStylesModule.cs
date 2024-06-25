using MVVMDemo.TypedStylesDemo;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class TypedStylesModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/TypedStyles";
            const string uri = "119416/Common-Concepts/Typed-Styles";
            return new IShowcaseInfo[] {
                LoadShowcase("Typed Styles - standard controls", uri,
                    path, new[] { typeof(StandardControlsTypedStylesView) }),
                LoadShowcase("Typed Styles - DevExpress controls", uri,
                    path, new[] { typeof(DXControlsTypedStylesView) }),
                LoadShowcase("Attached properties", uri,
                    path, new[] { typeof(AttachedPropertiesView) }),
                LoadShowcase("Events and attached events", uri,
                    path, new[] { typeof(EventsView) }),
                LoadShowcase("Markup extensions", uri,
                    path, new[] { typeof(MarkupExtensionsView) }),
                LoadShowcase("Bindings and Dynamic Extensions", uri,
                    path, new[] { typeof(BindingsAndDynamicResourcesView) }),
                LoadShowcase("DXBinding and DXCommand", uri,
                    path, new[] { typeof(DXBindingAndDXCommandView), typeof(DXBindingAndDXCommandViewModel) }),
                LoadShowcase("Base styles", uri,
                    path, new[] { typeof(BaseStylesView) }),
                LoadShowcase("Mixing with standard setters", uri,
                    path, new[] { typeof(StandardSettersView) }),
                LoadShowcase("Implicit styles", uri,
                    path, new[] { typeof(ImplicitStylesView) }),
                LoadShowcase("Typed triggers", uri,
                    path, new[] { typeof(StyleTriggersView) }),
                LoadShowcase("Typed data triggers", uri,
                    path, new[] { typeof(DataTriggersView) }),
                LoadShowcase("Typed triggers in template", uri,
                    path, new[] { typeof(TemplateTriggersView) }),
            };
        }
    }
}
