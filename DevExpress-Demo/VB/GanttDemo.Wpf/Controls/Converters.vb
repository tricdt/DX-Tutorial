Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core

Namespace GanttDemo

    Public Class TabItemEventArgsToDataConverter
        Inherits EventArgsConverterBase(Of TabControlTabRemovedEventArgs)

        Protected Overrides Function Convert(ByVal sender As Object, ByVal args As TabControlTabRemovedEventArgs) As Object
            Return CType(args.Item, DXTabItem).DataContext
        End Function
    End Class
End Namespace
