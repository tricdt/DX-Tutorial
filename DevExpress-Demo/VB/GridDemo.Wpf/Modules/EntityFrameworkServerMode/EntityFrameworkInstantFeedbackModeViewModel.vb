Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
#If Not DXCORE3
Imports System.Data.SQLite

#Else
using Microsoft.Data.Sqlite;
#End If
Namespace GridDemo

    Public Class EntityFrameworkInstantFeedbackModeViewModel
        Inherits InstantFeedbackModeViewModelBase(Of OutlookDataRecord)

#Region "DatabaseInfo"
#If Not DXCORE3
        Public Shared ReadOnly DatabaseInfo As DatabaseInfo = New DatabaseInfo(OutlookDataContext.FileName, "OutlookDataRecords", GetType(OutlookDataRecord), AddressOf CreateValues, Function(sql, connection) New SQLiteCommand(sql, CType(connection, SQLiteConnection)))

#Else
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            OutlookDataContext.FileName, "OutlookDataRecords", typeof(OutlookDataRecord), CreateValues, (sql, connection) => new SqliteCommand(sql, (SqliteConnection)connection));
#End If
        Private Shared Function CreateValues() As Dictionary(Of String, Object)
            Dim subject = GetSubject()
            Dim hasAttachment = GetHasAttachment()
            Dim size = GetSize(hasAttachment)
            Dim from = GetFrom()
            Dim sent = GetSentDate()
            Dim dict = New Dictionary(Of String, Object)()
            dict.Add("Subject", subject)
            dict.Add("HasAttachment", hasAttachment)
            dict.Add("Size", size)
            dict.Add("From", from)
            dict.Add("Sent", sent)
            Return dict
        End Function

#End Region
        Protected Sub New()
            MyBase.New(DatabaseInfo, Function() New OutlookDataContext().OutlookDataRecords)
        End Sub
    End Class
End Namespace
