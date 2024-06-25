Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.DXEventDemo

    Public Class EventArgsViewModel
        Inherits BindableBase

        Public Sub ShowPersonDetail(ByVal person As PersonInfo)
            If person IsNot Nothing Then MessageBox.Show(person.FullName, "Detail info")
        End Sub
    End Class
End Namespace
