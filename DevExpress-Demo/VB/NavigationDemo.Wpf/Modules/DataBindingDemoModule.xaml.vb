Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace NavigationDemo

    <CodeFile("ViewModels/DataBindingViewModel.(cs)")>
    Public Partial Class DataBindingDemoModule
        Inherits AccordionDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class EmployeeDetailsTemplateSelector
        Inherits DataTemplateSelector

        Public Property EmployeeDetailsTemplate As DataTemplate

        Public Property EmptyDetailsTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If TypeOf item Is Employee Then Return EmployeeDetailsTemplate
            Return EmptyDetailsTemplate
        End Function
    End Class
End Namespace
