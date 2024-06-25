Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.POCO
Imports System.Collections.Generic
Imports System.Linq

Namespace ControlsDemo.TabControl.CustomTheme

    Public Class MainViewModel

        Public Shared Function Create() As MainViewModel
            Return ViewModelSource.Create(Function() New MainViewModel())
        End Function

        Protected Sub New()
            If IsInDesignMode() Then Return
            Employees = NWindContext.Create().Employees.ToList()
        End Sub

        Public Overridable Property Employees As List(Of Employee)
    End Class
End Namespace
