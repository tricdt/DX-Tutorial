using MVVMDemo.Converters;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class ConvertersModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/Converters";
            const string uri = "17361/MVVM-Framework/Converters";
            return new ShowcaseInfo[] {
                LoadShowcase("BooleanToVisibilityConverter", uri + "#BooleanToVisibilityConverter", 
                    path, new[] { typeof(BooleanToVisibilityConverterView) }),
                LoadShowcase("BooleanNegationConverter", uri + "#BooleanNegationConverter",
                    path, new[] { typeof(BooleanNegationConverterView) }),
                LoadShowcase("BooleanToObjectConverter", uri + "#BooleanToObjectConverter",
                    path, new[] { typeof(BooleanToObjectConverterView) }),
                LoadShowcase("StringToBooleanConverter", uri + "#StringToBooleanConverter",
                    path, new[] { typeof(StringToBooleanConverterView) }),
                LoadShowcase("ObjectToBooleanConverter", uri + "#ObjectToBooleanConverter",
                    path, new[] { typeof(ObjectToBooleanConverterView) }),
                LoadShowcase("NumericToBooleanConverter", uri + "#NumericToBooleanConverter",
                    path, new[] { typeof(NumericToBooleanConverterView) }),
                LoadShowcase("NumericToVisibilityConverter", uri + "#NumericToVisibilityConverter",
                    path, new[] { typeof(NumericToVisibilityConverterView) }),
                LoadShowcase("ObjectToObjectConverter", uri + "#ObjectToObjectConverter",
                    path, new[] { typeof(ObjectToObjectConverterView), typeof(StartState)}),
                LoadShowcase("FormatStringConverter", uri + "#FormatStringConverter",
                    path, new[] { typeof(FormatStringConverterView) }),
            };
        }
    }
}
