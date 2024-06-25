Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core
Imports DevExpress.DemoData.Helpers

Namespace EditorsDemo

    Public Class DemoValuesProvider

        Public ReadOnly Property CardLayouts As IEnumerable(Of CardLayout)
            Get
                Return DevExpress.Utils.EnumExtensions.GetValues(GetType(CardLayout)).Cast(Of CardLayout)()
            End Get
        End Property

        Public ReadOnly Property Alignments As IEnumerable(Of Alignment)
            Get
                Return DevExpress.Utils.EnumExtensions.GetValues(GetType(Alignment)).Cast(Of Alignment)()
            End Get
        End Property

        Public ReadOnly Property NavigationStyles As IEnumerable(Of GridViewNavigationStyle)
            Get
                Return DevExpress.Utils.EnumExtensions.GetValues(GetType(GridViewNavigationStyle)).Cast(Of GridViewNavigationStyle)()
            End Get
        End Property
    End Class
End Namespace
