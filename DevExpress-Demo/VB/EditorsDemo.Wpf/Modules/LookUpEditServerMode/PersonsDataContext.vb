Imports DevExpress.Xpf.DemoBase
Imports System.Data.Entity
Imports System.IO

Namespace GridDemo

    Public Class PersonsDataContext
        Inherits DbContext

        Public Shared ReadOnly FileName As String = Path.GetFullPath("PersonData.db")

        Public Sub New()
            MyBase.New(SQLiteDataBaseGenerator.CreateConnection(LookUpInstantFeedbackModeViewModel.DatabaseInfo), True)
        End Sub

        Public Property People As DbSet(Of Person)
    End Class
End Namespace
