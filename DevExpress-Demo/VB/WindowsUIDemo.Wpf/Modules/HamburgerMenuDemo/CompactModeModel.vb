Imports System.Collections.Generic
Imports System.Linq

Namespace WindowsUIDemo

    Public Class CompactModeModel

        Public ReadOnly Property Data As IEnumerable(Of Message)
            Get
                Return Messages
            End Get
        End Property

        Public Property FocusedRow As Message

        Public Sub New()
            FocusedRow = Data.First()
        End Sub
    End Class
End Namespace
