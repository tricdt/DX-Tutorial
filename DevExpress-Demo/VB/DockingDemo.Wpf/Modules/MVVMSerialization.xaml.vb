Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Layout.Core
Imports DevExpress.Xpf.Docking

Namespace DockingDemo

    <CodeFile("ViewModels/SerializationViewModel.(cs)")>
    Public Partial Class MVVMSerialization
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class MVVMSerializationLayoutAdapter
        Implements ILayoutAdapter

#Region "ILayoutAdapter Members"
        Private Function Resolve(ByVal owner As DockLayoutManager, ByVal item As Object) As String Implements ILayoutAdapter.Resolve
            Dim panelHost As BaseLayoutItem = owner.GetItem("PanelHost")
            If panelHost Is Nothing Then
                panelHost = New LayoutGroup() With {.Name = "PanelHost", .DestroyOnClosingChildren = False}
                owner.LayoutRoot.Add(panelHost)
            End If

            Return "PanelHost"
        End Function
#End Region
    End Class
End Namespace
