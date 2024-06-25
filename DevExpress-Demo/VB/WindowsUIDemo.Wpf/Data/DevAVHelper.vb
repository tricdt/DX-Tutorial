Imports System.Collections.Generic
#If DXCORE3
using Microsoft.EntityFrameworkCore;
using System.IO;
using DevExpress.Internal;
#Else
Imports System.Data.Entity
#End If
Imports System.Linq

Namespace WindowsUIDemo

    Public Module DevAVHelper

        Private employeesField As List(Of DevExpress.DevAV.Employee) = Nothing

        Friend ReadOnly Property Employees As List(Of DevExpress.DevAV.Employee)
            Get
                If employeesField Is Nothing Then
#If DXCORE3
                    SetFilePath();
                    var devAvDb = new DevExpress.DevAV.DevAVDb(string.Format("Data Source={0}", filePath));
                    devAvDb.Pictures.Load();
#Else
                    Dim devAvDb = New DevExpress.DevAV.DevAVDbContext()
#End If
                    devAvDb.Employees.Load()
                    employeesField = devAvDb.Employees.Local.ToList()
                End If

                Return employeesField
            End Get
        End Property

#If DXCORE3
        static string filePath;
        static void SetFilePath() {
            if(filePath == null)
                filePath = DataDirectoryHelper.GetFile("devav.sqlite3", DataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
        }
#End If
        Friend Function GetEmployee(ByVal email As String) As DevExpress.DevAV.Employee
            Return Employees.SingleOrDefault(Function(x) x.Email.Equals(email))
        End Function
    End Module
End Namespace
