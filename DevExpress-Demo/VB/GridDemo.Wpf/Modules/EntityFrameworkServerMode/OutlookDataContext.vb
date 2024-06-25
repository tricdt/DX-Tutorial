Imports DevExpress.Xpf.DemoBase
Imports System.Data.Entity
Imports System.IO

Namespace GridDemo

    Public Class OutlookDataContext
        Inherits DbContext

        Public Shared ReadOnly FileName As String = Path.GetFullPath("OutlookData.db")

        Public Sub New()
            MyBase.New(SQLiteDataBaseGenerator.CreateConnection(EntityFrameworkInstantFeedbackModeViewModel.DatabaseInfo), True)
        End Sub

        Public Property OutlookDataRecords As DbSet(Of OutlookDataRecord)
    End Class

    Public Class OutlookDataRecord

        Public Property Id As Long

        Public Property Sent As Date

        Public Property HasAttachment As Boolean

        Public Property Size As Long

        Public Property From As String

        Public Property Subject As String
    End Class
End Namespace
