using MVVMDemo.Behaviors;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class BehaviorsModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/Behaviors";
            return new ShowcaseInfo[] {
                LoadShowcase("EventToCommand", "17369/Common-Concepts/MVVM-Framework/Behaviors/Predefined-Set/EventToCommand", 
                    path, new[] { typeof(EventToCommandView), typeof(EventToCommandViewModel) }),
                LoadShowcase("KeyToCommand", "113865/MVVM-Framework/Behaviors/Predefined-Set/KeyToCommand",
                    path, new[] { typeof(KeyToCommandView), typeof(KeyToCommandViewModel) }),
                LoadShowcase("FocusBehavior", "17370/MVVM-Framework/Behaviors/Predefined-Set/FocusBehavior",
                    path, new[] { typeof(FocusBehaviorView) }),
                LoadShowcase("EnumItemsSourceBehavior", "18089/MVVM-Framework/Behaviors/Predefined-Set/EnumItemsSourceBehavior",
                    path, new[] { typeof(EnumItemsSourceBehaviorView), typeof(UserRole) }),
                LoadShowcase("ConfirmationBehavior", "17372/MVVM-Framework/Behaviors/Predefined-Set/ConfirmationBehavior",
                    path, new[] { typeof(ConfirmationBehaviorView), typeof(ConfirmationBehaviorViewModel) }),
                LoadShowcase("DependencyPropertyBehavior", "17373/MVVM-Framework/Behaviors/Predefined-Set/DependencyPropertyBehavior",
                    path, new[] { typeof(DependencyPropertyBehaviorView), typeof(DependencyPropertyBehaviorViewModel) }),
                LoadShowcase("CompositeCommandBehavior", "18124/MVVM-Framework/Behaviors/Predefined-Set/CompositeCommandBehavior",
                    path, new[] { typeof(CompositeCommandBehaviorView), typeof(CompositeCommandBehaviorViewModel) }),
                LoadShowcase("ValidationErrorsHostBehavior", "17371/MVVM-Framework/Behaviors/Predefined-Set/ValidationErrorsHostBehavior",
                    path, new[] { typeof(ValidationErrorsHostBehaviorView), typeof(ValidationErrorsHostBehaviorViewModel) }),
                LoadShowcase("Implementing a Custom Behavior", "17458/MVVM-Framework/Behaviors/How-to-Create-a-Custom-Behavior",
                    path, new[] { typeof(SelectAllOnGotFocusBehavior), typeof(CustomBehaviorView) }),
                LoadShowcase("Assigning Behaviors in Style", "17457/MVVM-Framework/Behaviors/Getting-Started#InStyle",
                    path, new[] { typeof(BehaviorsInStyleView) }),
            };
        }
    }
}
