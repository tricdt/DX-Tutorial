Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Windows
Imports CommonDemo.Helpers
Imports DevExpress.Mvvm.DataAnnotations

Namespace ControlsDemo

    <POCOViewModel(ImplementIDataErrorInfo:=True)>
    Public Class NewJumpTaskWindowViewModel

        Public Sub New()
            Icons = New NamedIcon() {New NamedIcon() With {.Caption = "Moon", .Icon = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/Moon.svg", New Size(16, 16))}, New NamedIcon() With {.Caption = "Sun", .Icon = ImagesHelper.GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/Sun.svg", New Size(16, 16))}}
            CustomCategory = ""
            Title = "Title"
            Description = ""
            MessageText = "Message"
            Icon = Icons.ElementAt(0)
        End Sub

        Public Property Icons As IEnumerable(Of NamedIcon)

        Public Overridable Property CustomCategory As String

        <Required>
        Public Overridable Property Title As String

        Public Overridable Property Icon As NamedIcon

        Public Overridable Property Description As String

        Public Overridable Property MessageText As String
    End Class
End Namespace
