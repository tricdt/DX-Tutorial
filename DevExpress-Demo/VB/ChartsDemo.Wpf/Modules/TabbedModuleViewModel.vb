Imports System.Windows.Controls
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core

Namespace ChartsDemo

    Public Class TabbedModuleViewModel

        Public Shared Function Create() As TabbedModuleViewModel
            Return ViewModelSource.Create(Function() New TabbedModuleViewModel())
        End Function

        Public Overridable Property SelectedTab As DXTabItem

        Protected Sub New()
        End Sub
    End Class

    Public Class NotCachedTabbedModuleViewModel

        Public Shared Function Create() As NotCachedTabbedModuleViewModel
            Return ViewModelSource.Create(Function() New NotCachedTabbedModuleViewModel())
        End Function

        Public Overridable Property SelectedTab As DXTabItem

        Public Overridable Property Options As StackPanel
    End Class
End Namespace
