Imports DevExpress.Xpf.Editors
Imports System.Windows
Imports System.Windows.Controls

Namespace EditorsDemo

    Public Class BarCodeSymbologyTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim element As FrameworkElement = TryCast(container, FrameworkElement)
            If element Is Nothing OrElse item Is Nothing OrElse Not(TypeOf item Is BarCodeStyleSettings) Then Return MyBase.SelectTemplate(item, container)
            Return CType(element.FindResource(CType(item, BarCodeStyleSettings).GeneratorBase.Name), DataTemplate)
        End Function
    End Class
End Namespace
