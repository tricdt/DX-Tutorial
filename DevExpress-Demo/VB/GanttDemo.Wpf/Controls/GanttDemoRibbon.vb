Imports DevExpress.Xpf.Gantt
Imports System.Windows
Imports System.Windows.Controls

Namespace GanttDemo

    Public Partial Class GanttDemoRibbon
        Inherits UserControl

        Public Shared ReadOnly ExportGanttProperty As DependencyProperty = DependencyProperty.Register("ExportGantt", GetType(GanttControl), GetType(GanttDemoRibbon), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ExportGantt As GanttControl
            Get
                Return CType(GetValue(ExportGanttProperty), GanttControl)
            End Get

            Set(ByVal value As GanttControl)
                SetValue(ExportGanttProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
