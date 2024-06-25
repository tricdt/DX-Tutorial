Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace GanttDemo

    <CodeFile("ViewModels/StartupBusinessPlanViewModel.cs")>
    Public Partial Class StartupPlan
        Inherits GanttDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class StripLineTemplateSelector
        Inherits DataTemplateSelector

        Public Property StripLineTemplate As DataTemplate

        Public Property WeeklyStripLineTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If TypeOf item Is GanttStripLine Then Return StripLineTemplate
            If TypeOf item Is WeeklyStripLine Then Return WeeklyStripLineTemplate
            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class

    Public Class DayOfWeekToDaysOfWeekConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return [Enum].Parse(GetType(DaysOfWeek), [Enum].GetName(GetType(DayOfWeek), value))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
