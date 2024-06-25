Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports System.Globalization

Namespace DevExpress.Demos.SalesDBGenerator

    Public Class SqlHelper(Of T As System.Data.Common.DbConnection, Command As {System.Data.Common.DbCommand, New})

        Public Function ReadValue(ByVal connection As T, ByVal selectQuery As String, ParamArray pars As System.Data.Common.DbParameter()) As Object
            Using sql = DevExpress.Demos.SalesDBGenerator.SqlHelper(Of T, Command).CreateCommand(selectQuery, connection)
                sql.CommandTimeout = 1000
                If pars IsNot Nothing Then sql.Parameters.AddRange(pars)
                Try
                    Return Me.CheckDbNull(sql.ExecuteScalar())
                Catch
                    Return Nothing
                End Try
            End Using
        End Function

        Private Function CheckDbNull(ByVal value As Object) As Object
            If value Is Nothing Then Return Nothing
            If System.[Object].ReferenceEquals(value, System.DBNull.Value) Then Return Nothing
            Return value
        End Function

        Private Shared Function CreateCommand(ByVal selectQuery As String, ByVal connection As T) As DbCommand
            Return New Command() With {.CommandText = selectQuery, .Connection = connection}
        End Function

        Public Function ReadValues(ByVal connection As T, ByVal selectQuery As String, ParamArray pars As System.Data.Common.DbParameter()) As List(Of Object())
            Dim res As System.Collections.Generic.List(Of Object()) = New System.Collections.Generic.List(Of Object())()
            Try
                Using sql = DevExpress.Demos.SalesDBGenerator.SqlHelper(Of T, Command).CreateCommand(selectQuery, connection)
                    sql.CommandTimeout = 5000
                    If pars IsNot Nothing Then sql.Parameters.AddRange(pars)
                    Using reader As System.Data.Common.DbDataReader = sql.ExecuteReader()
                        If Not reader.HasRows Then Return res
                        While reader.Read()
                            Dim values As Object() = New Object(reader.FieldCount - 1) {}
                            reader.GetValues(values)
                            res.Add(values)
                        End While
                    End Using
                End Using
            Catch
            End Try

            Return res
        End Function

        Public Function GetString(ByVal val As Object) As String
            If val Is System.DBNull.Value OrElse val Is Nothing Then Return String.Empty
            Return val.ToString()
        End Function

        Public Function GetDateInv(ByVal val As Object) As DateTime
            If val Is System.DBNull.Value OrElse val Is Nothing Then Return System.DateTime.MinValue
            Return System.DateTime.ParseExact(val.ToString(), "d/M/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        End Function

        Public Function GetInt(ByVal val As Object) As Integer
            If val Is System.DBNull.Value OrElse val Is Nothing Then Return 0
            Return System.Convert.ToInt32(val)
        End Function

        Public Function GetDate(ByVal value As Object) As DateTime
            If value Is Nothing OrElse value Is System.DBNull.Value Then Return System.DateTime.MinValue
            If TypeOf value Is System.DateTime Then Return CDate(value)
            Return Me.GetDateInv(value)
        End Function

        Public Function GetDecimal(ByVal value As Object) As Decimal
            If value Is Nothing OrElse value Is System.DBNull.Value Then Return 0
            Return CDec(System.Convert.ChangeType(value, GetType(Decimal)))
        End Function

        Public Function GetGuid(ByVal value As Object) As Guid
            If value Is Nothing OrElse value Is System.DBNull.Value Then Return System.Guid.Empty
            Return New System.Guid(value.ToString())
        End Function

        Public Function GetBool(ByVal value As Object) As Boolean
            If value Is Nothing OrElse value Is System.DBNull.Value Then Return False
            If TypeOf value Is Boolean Then Return CBool(value)
            If TypeOf value Is Integer Then Return CInt(value) = 1
            Return False
        End Function
    End Class
End Namespace
