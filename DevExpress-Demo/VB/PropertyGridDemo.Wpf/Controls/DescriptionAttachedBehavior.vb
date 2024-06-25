Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid

Namespace PropertyGridDemo

    Public Class DescriptionAttachedBehavior
        Inherits DependencyObject

        Private Shared ReadOnly DescriptionProperty As DependencyProperty

        Shared Sub New()
            DescriptionProperty = DependencyProperty.RegisterAttached("Description", GetType(Object), GetType(DescriptionAttachedBehavior), New FrameworkPropertyMetadata(Nothing, AddressOf DescriptionChanged))
        End Sub

        Public Shared Sub SetDescription(ByVal d As DependencyObject, ByVal value As Object)
            d.SetValue(DescriptionProperty, value)
        End Sub

        Public Shared Function GetDescription(ByVal d As DependencyObject) As Object
            Return d.GetValue(DescriptionProperty)
        End Function

        Private Shared Sub DescriptionChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim rtb = TryCast(d, RichTextBox)
            If rtb Is Nothing OrElse rtb.Document Is Nothing Then Return
            rtb.Document.Blocks.Clear()
            Dim [property] = TryCast(e.NewValue, PropertyDescriptor)
            If [property] Is Nothing Then Return
            Dim control As ContentControl = New ContentControl() With {.Template = TryCast(rtb.FindResource(DynamicallyAssignDataEditorsTemplateSelector.GetTemplateName([property]) & "Description"), ControlTemplate)}
            control.ApplyTemplate()
            Dim container As ParagraphContainer = TryCast(VisualTreeHelper.GetChild(control, 0), ParagraphContainer)
            rtb.Document.Blocks.Add(container.Paragraph)
        End Sub
    End Class
End Namespace
