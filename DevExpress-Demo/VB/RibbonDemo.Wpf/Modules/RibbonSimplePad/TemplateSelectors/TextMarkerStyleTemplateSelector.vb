Imports System.Windows
Imports System.Windows.Controls

Namespace RibbonDemo

    Public Class TextMarkerStyleTemplateSelector
        Inherits DataTemplateSelector

        Public Property Dictionary As TemplateSelectorDictionary

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Return Dictionary(CType(item, TextMarkerStyle).ToString())
        End Function
    End Class
End Namespace
