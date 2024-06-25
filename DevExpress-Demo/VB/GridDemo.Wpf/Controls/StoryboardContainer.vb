Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation

Namespace GridDemo

    <ContentProperty("Storyboard")>
    Public Class StoryboardContainer
        Inherits Control

        Public Shared Function CreateStoryboard(ByVal resourceHolder As Control, ByVal resourceName As String) As Storyboard
            Dim c As ContentControl = New ContentControl() With {.Template = CType(resourceHolder.Resources(resourceName), ControlTemplate)}
            c.ApplyTemplate()
            Dim container As StoryboardContainer = CType(VisualTreeHelper.GetChild(c, 0), StoryboardContainer)
            Return container.Storyboard
        End Function

        Public Property Storyboard As Storyboard
    End Class
End Namespace
