Imports System.Collections.Generic
Imports DevExpress.Mvvm.DataAnnotations

Namespace GridDemo

    <POCOViewModel>
    Public Class CompactModeModel

        Public ReadOnly Property Data As IEnumerable(Of Message)
            Get
                Return Messages
            End Get
        End Property
    End Class
End Namespace
