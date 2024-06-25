Imports System.Windows.Controls

Namespace EditorsDemo

    Public Partial Class ImageEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            DataContext = New EmployeeViewModel()
            InitializeComponent()
        End Sub
    End Class
End Namespace
