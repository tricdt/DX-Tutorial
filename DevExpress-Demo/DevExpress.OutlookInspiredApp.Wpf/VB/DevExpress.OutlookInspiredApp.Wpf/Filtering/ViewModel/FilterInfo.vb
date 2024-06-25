Imports System.Collections.Generic

Namespace DevExpress.DevAV.ViewModels

    Public Class FilterInfo

        Public Property Name As String

        Public Property FilterCriteria As String

        Public Property ImageUri As String
    End Class

    Public Class FilterInfoList
        Inherits List(Of FilterInfo)

        Public Sub New()
        End Sub

        Public Sub New(ByVal filters As IEnumerable(Of FilterInfo))
            MyBase.New(filters)
        End Sub
    End Class
End Namespace
