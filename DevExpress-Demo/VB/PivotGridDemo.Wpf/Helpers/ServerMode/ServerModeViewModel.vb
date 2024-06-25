Imports DevExpress.Xpf.DemoBase
Imports PivotGridDemo.Helpers
Imports System.Data.SQLite

Namespace PivotGridDemo

    Public Class ServerModeViewModel
        Inherits ServerModeViewModelBase(Of OrderDataRecord)

#Region "DatabaseInfo"
        Public Shared ReadOnly DatabaseInfo As DatabaseInfo = New DatabaseInfo(OrderDataContext.FileName, "OrderDataRecords", GetType(OrderDataRecord), AddressOf DatabaseHelper.CreateValues, Function(sql, connection) New SQLiteCommand(sql, CType(connection, SQLiteConnection)))

#End Region
        Protected Sub New()
            MyBase.New(DatabaseInfo, Function() New OrderDataContext().OrderDataRecords)
        End Sub
    End Class
End Namespace
