Imports DevExpress.Mvvm.DataAnnotations
Imports System.Windows
Imports System.Windows.Controls

Namespace BarsDemo

    Public Partial Class AlignmentView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class AlignmentModel

        <BindableProperty(OnPropertyChangedMethodName:="OnChanged")>
        Public Overridable Property IsLeft As Boolean

        <BindableProperty(OnPropertyChangedMethodName:="OnChanged")>
        Public Overridable Property IsRight As Boolean

        <BindableProperty(OnPropertyChangedMethodName:="OnChanged")>
        Public Overridable Property IsCenter As Boolean

        <BindableProperty(OnPropertyChangedMethodName:="OnChanged")>
        Public Overridable Property IsJustify As Boolean

        Public Overridable Property Alignment As TextAlignment

        Public Sub New()
            IsLeft = True
        End Sub

        Public Sub OnChanged()
            If IsLeft Then Alignment = TextAlignment.Left
            If IsRight Then Alignment = TextAlignment.Right
            If IsJustify Then Alignment = TextAlignment.Justify
            If IsCenter Then Alignment = TextAlignment.Center
        End Sub
    End Class
End Namespace
