Imports DevExpress.Data.Linq
Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic
#If Not DXCORE3
Imports System.Data.SQLite

#Else
using Microsoft.Data.Sqlite;
#End If
Namespace GridDemo

    Public Class LookUpInstantFeedbackModeViewModel
        Inherits InstantFeedbackModeViewModelBase(Of Person)

        Private _Orders As List(Of GridDemo.OrderData)

#Region "DatabaseInfo"
        Private Shared ReadOnly Generator As PersonGenerator = New PersonGenerator()

#If Not DXCORE3
        Public Shared ReadOnly DatabaseInfo As DatabaseInfo = New DatabaseInfo(PersonsDataContext.FileName, "People", GetType(Person), AddressOf CreateValues, Function(sql, connection) New SQLiteCommand(sql, CType(connection, SQLiteConnection)))

#Else
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            PersonsDataContext.FileName, "People", typeof(Person), CreateValues, (sql, connection) => new SqliteCommand(sql, (SqliteConnection)connection));
#End If
        Private Shared Function CreateValues() As IDictionary(Of String, Object)
            Dim person = Generator.CreatePerson()
            Dim dict = New Dictionary(Of String, Object)()
            dict.Add("FullName", person.FullName)
            dict.Add("Company", person.Company)
            dict.Add("JobTitle", person.JobTitle)
            dict.Add("City", person.City)
            dict.Add("Address", person.Address)
            dict.Add("Phone", person.Phone)
            dict.Add("Email", person.Email)
            Return dict
        End Function

#End Region
        Protected Sub New()
            MyBase.New(DatabaseInfo, Function() New PersonsDataContext().People)
            Orders = GenerateOrders(200)
        End Sub

        Public Property Orders As List(Of OrderData)
            Get
                Return _Orders
            End Get

            Private Set(ByVal value As List(Of OrderData))
                _Orders = value
            End Set
        End Property

        Public Overridable Property InstantFeedbackSource As EntityInstantFeedbackSource

        Protected Overrides Sub AssignSourcesCore()
            MyBase.AssignSourcesCore()
            DisposeInstantFeedbackSource()
            Dim source = New EntityInstantFeedbackSource() With {.KeyExpression = "Id"}
            AddHandler source.GetQueryable, Sub(o, e) e.QueryableSource = getQueryable()
            InstantFeedbackSource = source
        End Sub

        Public Overrides Sub OnUnloaded()
            MyBase.OnUnloaded()
            DisposeInstantFeedbackSource()
        End Sub

        Private Sub DisposeInstantFeedbackSource()
            If InstantFeedbackSource IsNot Nothing Then
                InstantFeedbackSource.Dispose()
                InstantFeedbackSource = Nothing
            End If
        End Sub
    End Class
End Namespace
