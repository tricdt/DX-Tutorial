Imports System
Imports System.Windows.Controls
Imports DevExpress.Xpf.Gantt

Namespace GanttDemo.Examples

    Public Partial Class CustomizeTimescale
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Shared ReadOnly _90MinutesRuler As TimescaleRuler = New TimescaleRuler(TimescaleUnit.Minute, displayFormat:="H:mm", count:=90)

        Private Shared ReadOnly _1weekRuler As TimescaleRuler = New TimescaleRuler(TimescaleUnit.Week, formatProvider:=New CustomDateTimeRangeFormatter())

        Private Sub GanttView_RequestTimeScales(ByVal sender As Object, ByVal e As RequestTimescaleRulersEventArgs)
            If e.Zoom >= TimeSpan.FromMinutes(1) AndAlso e.Zoom < TimeSpan.FromMinutes(3) Then
                e.TimescaleRulers(2) = _90MinutesRuler
            ElseIf e.Zoom >= TimeSpan.FromMinutes(30) AndAlso e.Zoom < TimeSpan.FromHours(2) Then
                e.TimescaleRulers(1) = _1weekRuler
            End If
        End Sub
#End Region
    End Class
End Namespace
