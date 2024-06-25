Imports MVVMDemo.Converters
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class ConvertersModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/Converters"
            Const uri As String = "17361/MVVM-Framework/Converters"
            Return New ShowcaseInfo() {LoadShowcase("BooleanToVisibilityConverter", uri & "#BooleanToVisibilityConverter", path, {GetType(BooleanToVisibilityConverterView)}), LoadShowcase("BooleanNegationConverter", uri & "#BooleanNegationConverter", path, {GetType(BooleanNegationConverterView)}), LoadShowcase("BooleanToObjectConverter", uri & "#BooleanToObjectConverter", path, {GetType(BooleanToObjectConverterView)}), LoadShowcase("StringToBooleanConverter", uri & "#StringToBooleanConverter", path, {GetType(StringToBooleanConverterView)}), LoadShowcase("ObjectToBooleanConverter", uri & "#ObjectToBooleanConverter", path, {GetType(ObjectToBooleanConverterView)}), LoadShowcase("NumericToBooleanConverter", uri & "#NumericToBooleanConverter", path, {GetType(NumericToBooleanConverterView)}), LoadShowcase("NumericToVisibilityConverter", uri & "#NumericToVisibilityConverter", path, {GetType(NumericToVisibilityConverterView)}), LoadShowcase("ObjectToObjectConverter", uri & "#ObjectToObjectConverter", path, {GetType(ObjectToObjectConverterView), GetType(StartState)}), LoadShowcase("FormatStringConverter", uri & "#FormatStringConverter", path, {GetType(FormatStringConverterView)})}
        End Function
    End Class
End Namespace
