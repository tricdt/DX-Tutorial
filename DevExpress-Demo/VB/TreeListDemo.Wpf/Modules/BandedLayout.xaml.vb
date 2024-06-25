Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace TreeListDemo

    Public Partial Class BandedLayout
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class BandedViewViewModel

        Public ReadOnly Property SpaceObjects As IList(Of SpaceObjects)
            Get
                Return SpaceObjectData.DataSource
            End Get
        End Property
    End Class
End Namespace
