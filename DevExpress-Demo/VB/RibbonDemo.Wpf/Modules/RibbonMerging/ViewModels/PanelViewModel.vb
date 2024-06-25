Imports System.Collections.Generic
Imports System.Windows

Namespace RibbonDemo

    Public MustInherit Class PanelViewModel

        Private _FontSizes As IEnumerable(Of Double?)

        Protected Property ParentViewModel As RibbonMergingViewModel

        Public Overridable Property Caption As String

        Public Overridable Property Location As Point

        Public Overridable Property IsActive As Boolean

        Public Property FontSizes As IEnumerable(Of Double?)
            Get
                Return _FontSizes
            End Get

            Protected Set(ByVal value As IEnumerable(Of Double?))
                _FontSizes = value
            End Set
        End Property

        Public Overridable Sub Close(ByVal param As Object)
            ParentViewModel.Panels.Remove(Me)
            If ParentViewModel.Panels.Count <> 0 Then ParentViewModel.Panels(0).IsActive = True
        End Sub

        Public Sub New()
            FontSizes = New Double?() {3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144}
        End Sub
    End Class
End Namespace
