Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid

Namespace TreeListDemo

    Public Partial Class TreeListDemoRibbon
        Inherits UserControl

        Public Shared ReadOnly ExportTreeListProperty As DependencyProperty = DependencyProperty.Register("ExportTreeList", GetType(TreeListControl), GetType(TreeListDemoRibbon), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ExportTreeList As TreeListControl
            Get
                Return CType(GetValue(ExportTreeListProperty), TreeListControl)
            End Get

            Set(ByVal value As TreeListControl)
                SetValue(ExportTreeListProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
