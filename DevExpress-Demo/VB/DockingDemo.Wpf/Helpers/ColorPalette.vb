Imports System.Collections.Generic
Imports System.Windows.Media

Namespace DockingDemo.Helpers

    Public Class ColorPalette

        Private Shared ReadOnly Palette As List(Of Color)

        Shared Sub New()
            Palette = New List(Of Color)() From {ColorFromString("#a02277"), ColorFromString("#4668a5"), ColorFromString("#90a091"), ColorFromString("#33a4be"), ColorFromString("#460ea5"), ColorFromString("#139e79"), ColorFromString("#5848a5"), ColorFromString("#9462ae"), ColorFromString("#65fc03"), ColorFromString("#fcc003")}
        End Sub

        Public Shared Function GetColor(ByVal number As Integer) As Color
            Dim color As Integer = number Mod Palette.Count
            Return Palette(color)
        End Function

        Private Shared Function ColorFromString(ByVal colorString As String) As Color
            Return CType(If(ColorConverter.ConvertFromString(colorString), Colors.Transparent), Color)
        End Function
    End Class
End Namespace
