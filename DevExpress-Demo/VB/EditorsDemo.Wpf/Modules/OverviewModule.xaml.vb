Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo

    <CodeFile("ViewModels/OverviewDemoViewModel.(cs)")>
    Public Partial Class OverviewModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New EmployeeViewModel()
        End Sub
    End Class
End Namespace
