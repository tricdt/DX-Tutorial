Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Gantt

Namespace GanttDemo

    <CodeFile("Utils/CSVLoader.cs")>
    <CodeFile("ViewModels/PageLoadingViewModel.cs")>
    Public Partial Class RealTimeUpdates
        Inherits GanttDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub RequestGanttTimescaleRulers(ByVal sender As Object, ByVal e As RequestTimescaleRulersEventArgs)
            e.TimescaleRulers(0) = New TimescaleRuler(TimescaleUnit.Minute, "hh:mm")
            e.TimescaleRulers(1) = New TimescaleRuler(TimescaleUnit.Second, "s 'sec'", 1)
            e.TimescaleRulers(2) = New TimescaleRuler(TimescaleUnit.Millisecond, "fff", 250)
        End Sub
    End Class
End Namespace
