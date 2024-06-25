Imports DevExpress.Xpf.DemoBase
Imports System.Data.Entity
Imports System.IO

Namespace PivotGridDemo

    Public Class OrderDataContext
        Inherits DbContext

        Public Shared ReadOnly FileName As String = Path.GetFullPath("OrderData.db")

        Public Sub New()
            MyBase.New(SQLiteDataBaseGenerator.CreateConnection(ServerModeViewModel.DatabaseInfo), True)
        End Sub

        Public Property OrderDataRecords As DbSet(Of OrderDataRecord)
    End Class

    Public Class OrderDataRecord

        Public Property Id As Integer

        Public Property OrderDate As Date

        Public Property Quantity As Integer

        Public Property UnitPrice As Decimal

        Public Property CustomerName As String

        Public Property ProductName As String

        Public Property CategoryName As String

        Public Property SalesPersonName As String
    End Class

    Public Class ProductDataRecord

        Public Property ProductName As String

        Public Property CategoryName As String

        Public Property UnitPrice As Decimal
    End Class
End Namespace
