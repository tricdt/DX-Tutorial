Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Drawing
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    <CodeFile("ViewModels/CarsViewModel.(cs)")>
    Public Partial Class pageCars
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class CarDataTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim control = CType(container, FlowLayoutControl)
            Return CType(control.Resources(item.GetType().Name & "DataTemplate"), DataTemplate)
        End Function
    End Class
End Namespace
