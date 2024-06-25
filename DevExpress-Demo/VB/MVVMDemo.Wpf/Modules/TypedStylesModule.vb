Imports MVVMDemo.TypedStylesDemo
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class TypedStylesModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/TypedStyles"
            Const uri As String = "119416/Common-Concepts/Typed-Styles"
            Return New IShowcaseInfo() {LoadShowcase("Typed Styles - standard controls", uri, path, {GetType(StandardControlsTypedStylesView)}), LoadShowcase("Typed Styles - DevExpress controls", uri, path, {GetType(DXControlsTypedStylesView)}), LoadShowcase("Attached properties", uri, path, {GetType(AttachedPropertiesView)}), LoadShowcase("Events and attached events", uri, path, {GetType(EventsView)}), LoadShowcase("Markup extensions", uri, path, {GetType(MarkupExtensionsView)}), LoadShowcase("Bindings and Dynamic Extensions", uri, path, {GetType(BindingsAndDynamicResourcesView)}), LoadShowcase("DXBinding and DXCommand", uri, path, {GetType(DXBindingAndDXCommandView), GetType(DXBindingAndDXCommandViewModel)}), LoadShowcase("Base styles", uri, path, {GetType(BaseStylesView)}), LoadShowcase("Mixing with standard setters", uri, path, {GetType(StandardSettersView)}), LoadShowcase("Implicit styles", uri, path, {GetType(ImplicitStylesView)}), LoadShowcase("Typed triggers", uri, path, {GetType(StyleTriggersView)}), LoadShowcase("Typed data triggers", uri, path, {GetType(DataTriggersView)}), LoadShowcase("Typed triggers in template", uri, path, {GetType(TemplateTriggersView)})}
        End Function
    End Class
End Namespace
