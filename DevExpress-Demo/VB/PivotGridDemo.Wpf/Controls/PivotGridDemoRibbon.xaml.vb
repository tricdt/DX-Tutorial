Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Partial Class PivotGridDemoRibbon
        Inherits UserControl

        Public Shared ReadOnly ExportPivotGridProperty As DependencyProperty = DependencyProperty.Register("ExportPivotGrid", GetType(PivotGridControl), GetType(PivotGridDemoRibbon), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ExportPivotGrid As PivotGridControl
            Get
                Return CType(GetValue(ExportPivotGridProperty), PivotGridControl)
            End Get

            Set(ByVal value As PivotGridControl)
                SetValue(ExportPivotGridProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
