Imports MVVMDemo.Behaviors
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class BehaviorsModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/Behaviors"
            Return New ShowcaseInfo() {LoadShowcase("EventToCommand", "17369/Common-Concepts/MVVM-Framework/Behaviors/Predefined-Set/EventToCommand", path, {GetType(EventToCommandView), GetType(EventToCommandViewModel)}), LoadShowcase("KeyToCommand", "113865/MVVM-Framework/Behaviors/Predefined-Set/KeyToCommand", path, {GetType(KeyToCommandView), GetType(KeyToCommandViewModel)}), LoadShowcase("FocusBehavior", "17370/MVVM-Framework/Behaviors/Predefined-Set/FocusBehavior", path, {GetType(FocusBehaviorView)}), LoadShowcase("EnumItemsSourceBehavior", "18089/MVVM-Framework/Behaviors/Predefined-Set/EnumItemsSourceBehavior", path, {GetType(EnumItemsSourceBehaviorView), GetType(UserRole)}), LoadShowcase("ConfirmationBehavior", "17372/MVVM-Framework/Behaviors/Predefined-Set/ConfirmationBehavior", path, {GetType(ConfirmationBehaviorView), GetType(ConfirmationBehaviorViewModel)}), LoadShowcase("DependencyPropertyBehavior", "17373/MVVM-Framework/Behaviors/Predefined-Set/DependencyPropertyBehavior", path, {GetType(DependencyPropertyBehaviorView), GetType(DependencyPropertyBehaviorViewModel)}), LoadShowcase("CompositeCommandBehavior", "18124/MVVM-Framework/Behaviors/Predefined-Set/CompositeCommandBehavior", path, {GetType(CompositeCommandBehaviorView), GetType(CompositeCommandBehaviorViewModel)}), LoadShowcase("ValidationErrorsHostBehavior", "17371/MVVM-Framework/Behaviors/Predefined-Set/ValidationErrorsHostBehavior", path, {GetType(ValidationErrorsHostBehaviorView), GetType(ValidationErrorsHostBehaviorViewModel)}), LoadShowcase("Implementing a Custom Behavior", "17458/MVVM-Framework/Behaviors/How-to-Create-a-Custom-Behavior", path, {GetType(SelectAllOnGotFocusBehavior), GetType(CustomBehaviorView)}), LoadShowcase("Assigning Behaviors in Style", "17457/MVVM-Framework/Behaviors/Getting-Started#InStyle", path, {GetType(BehaviorsInStyleView)})}
        End Function
    End Class
End Namespace
