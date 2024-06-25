Imports System.Windows
Imports System.Windows.Controls

Namespace BarsDemo

    Public Partial Class FontSettingsView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class FontSettingsModel

        Public Overridable Property IsBold As Boolean

        Public Overridable Property IsItalic As Boolean

        Public Overridable Property IsUnderline As Boolean

        Public Overridable Property Weight As FontWeight

        Public Overridable Property Decorations As TextDecorationCollection

        Public Overridable Property Style As FontStyle

        Public Sub OnIsBoldChanged()
            Weight = If(IsBold, FontWeights.Bold, FontWeights.Normal)
        End Sub

        Public Sub OnIsItalicChanged()
            Style = If(IsItalic, FontStyles.Italic, FontStyles.Normal)
        End Sub

        Public Sub OnIsUnderlineChanged()
            Decorations = If(IsUnderline, TextDecorations.Underline, New TextDecorationCollection())
        End Sub
    End Class
End Namespace
