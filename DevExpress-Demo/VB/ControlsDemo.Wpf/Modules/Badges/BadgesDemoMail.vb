Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Data
#If DXCORE3
using Microsoft.EntityFrameworkCore;
using DevExpress.Internal;
#Else
Imports System.Data.Entity
#End If
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports DevExpress.DevAV
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.XtraRichEdit

Namespace ControlsDemo

#If Not DXCORE3
    Public Class DevAVDbContext
        Inherits DevAVDbBase

        Shared Sub New()
            Call Database.SetInitializer(Of DevAVDbContext)(Nothing)
        End Sub
    End Class

#End If
    Public Module DevAVHelper

        Private employeesField As Dictionary(Of String, Employee) = Nothing

        Private ReadOnly Property Employees As Dictionary(Of String, Employee)
            Get
                If employeesField Is Nothing Then
#If DXCORE3
                    SetFilePath();
                    var devAvDb = new DevExpress.DevAV.DevAVDb(string.Format("Data Source={0}", filePath));
                    devAvDb.Pictures.Load();
#Else
                    Dim devAvDb = New DevAVDbContext()
#End If
                    devAvDb.Employees.Load()
                    employeesField = devAvDb.Employees.Local.ToDictionary(Function(x) x.Email)
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
        Friend Function GetEmployee(ByVal email As String) As Employee
            Dim result As Employee
            If Employees.TryGetValue(email, result) Then Return result
            Return Nothing
        End Function
    End Module

    Public Module MailItems

        Private _DataSet As DataSet

        Friend Property DataSet As DataSet
            Get
                Return _DataSet
            End Get

            Private Set(ByVal value As DataSet)
                _DataSet = value
            End Set
        End Property

        Private messagesField As ObservableCollection(Of Message) = Nothing

        Private ReadOnly Property MailTable As DataTable
            Get
                Return CreateDataTable("Messages")
            End Get
        End Property

        Private Function CreateDataTable(ByVal table As String) As DataTable
            If DataSet Is Nothing Then
                DataSet = New DataSet()
                Dim assembly As Assembly = GetType(Badges).Assembly
                Using stream As Stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("ControlsDemo.Modules.Badges.", assembly) & "MailDevAv.xml")
                    Call DataSet.ReadXml(stream)
                End Using

                Call DataSet.Relations.Add(DataSet.Tables("Employees").Columns("Email"), DataSet.Tables("Messages").Columns("From"))
            End If

            Return DataSet.Tables(table)
        End Function

        Public ReadOnly Property Messages As ObservableCollection(Of Message)
            Get
                Try
                    If messagesField Is Nothing Then
                        messagesField = New ObservableCollection(Of Message)()
                        Dim tbl As DataTable = MailTable
                        If tbl IsNot Nothing Then
                            For Each row As DataRow In tbl.Rows
                                Call messagesField.Add(New Message(row))
                            Next
                        End If
                    End If
                Catch __unusedException1__ As Exception
                    messagesField = New ObservableCollection(Of Message)()
                End Try

                Return messagesField
            End Get
        End Property
    End Module

    Public Class EmployeeMessages

        Public Sub New(ByVal mailEmployee As MailEmployee, ByVal messages As IEnumerable(Of Message))
            Employee = mailEmployee
            Me.Messages = messages.ToList()
            UrgentMessagesCount = Me.Messages.Where(Function(x) x.Priority > 1).Count()
        End Sub

        Public Property Employee As MailEmployee

        Public Property Messages As List(Of Message)

        Public Property UrgentMessagesCount As Integer
    End Class

    Public Class MailEmployee

        Private _FirstName As String, _LastName As String, _FullName As String, _Email As String, _Department As String, _Status As MailEmployeeStatus, _Gender As String

        Protected Overloads Function Equals(ByVal other As MailEmployee) As Boolean
            Return Equals(Email, other.Email)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If obj.GetType() IsNot [GetType]() Then Return False
            Return Equals(CType(obj, MailEmployee))
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return(If(Not Equals(FullName, Nothing), FullName.GetHashCode(), 0))
        End Function

        Public Shared Operator =(ByVal left As MailEmployee, ByVal right As MailEmployee) As Boolean
            Return Equals(left, right)
        End Operator

        Public Shared Operator <>(ByVal left As MailEmployee, ByVal right As MailEmployee) As Boolean
            Return Not Equals(left, right)
        End Operator

        Private Shared statusGenerator As Random = New Random()

        Public Sub New(ByVal row As DataRow)
            FirstName = String.Format("{0}", row("FirstName"))
            LastName = String.Format("{0}", row("LastName"))
            FullName = String.Format("{0} {1}", FirstName, LastName)
            Gender = String.Format("{0}", row("Gender"))
            Email = String.Format("{0}", row("Email"))
            Status = CType(statusGenerator.Next(0, 2), MailEmployeeStatus)
            Dim employee = DevAVHelper.GetEmployee(String.Format("{0}", row("Email")))
            If employee IsNot Nothing Then
                Dim convertedDept = [Enum].GetName(GetType(EmployeeDepartment), employee.Department)
                Dim fieldDept = GetType(EmployeeDepartment).GetField(convertedDept, BindingFlags.Static Or BindingFlags.Public)
                Department = If(DataAnnotationsAttributeHelper.GetFieldDisplayName(fieldDept), convertedDept)
            End If
        End Sub

        Public Property FirstName As String
            Get
                Return _FirstName
            End Get

            Private Set(ByVal value As String)
                _FirstName = value
            End Set
        End Property

        Public Property LastName As String
            Get
                Return _LastName
            End Get

            Private Set(ByVal value As String)
                _LastName = value
            End Set
        End Property

        Public Property FullName As String
            Get
                Return _FullName
            End Get

            Private Set(ByVal value As String)
                _FullName = value
            End Set
        End Property

        Public Property Email As String
            Get
                Return _Email
            End Get

            Private Set(ByVal value As String)
                _Email = value
            End Set
        End Property

        Public Property Department As String
            Get
                Return _Department
            End Get

            Private Set(ByVal value As String)
                _Department = value
            End Set
        End Property

        Public Property Status As MailEmployeeStatus
            Get
                Return _Status
            End Get

            Private Set(ByVal value As MailEmployeeStatus)
                _Status = value
            End Set
        End Property

        Public ReadOnly Property Initials As String
            Get
                Return String.Format("{0}{1}", FirstName.First(), LastName.First())
            End Get
        End Property

        Public Property Gender As String
            Get
                Return _Gender
            End Get

            Private Set(ByVal value As String)
                _Gender = value
            End Set
        End Property
    End Class

    Public Class Message

        Public Sub New(ByVal row As DataRow)
            Employee = New MailEmployee(row.GetParentRow(MailItems.DataSet.Relations(0)))
            Received = Date.Now.AddDays(CInt(row("Day")))
            Subject = String.Format("{0}", row("Subject"))
            HasAttachment = CBool(row("HasAttachment"))
            Read = Delay > TimeSpan.FromHours(6)
            If Delay > TimeSpan.FromHours(50) AndAlso Delay < TimeSpan.FromHours(100) Then Read = False
            Text = String.Format("{0}", row("Text"))
            Priority = CalcPriority()
            If Not(TypeOf row("CategoryID") Is DBNull) Then MailFolder = CType(row("CategoryID"), MailFolder)
        End Sub

        Public Property Employee As MailEmployee

        Public Property Received As Date

        Public Property Subject As String

        Public Property HasAttachment As Boolean

        Public Property Read As Boolean

        Public Property Text As String

        Public Property Priority As Integer

        Public Property MailFolder As MailFolder

        Private plainTextField As String

        Public ReadOnly Property PlainText As String
            Get
                If Equals(plainTextField, Nothing) Then plainTextField = PlainTextProvider.GetPlainText(Text)
                Return plainTextField
            End Get
        End Property

        Friend ReadOnly Property Delay As TimeSpan
            Get
                Return Date.Now - Received
            End Get
        End Property

        Private Function CalcPriority() As Integer
            If Subject.IndexOf("Review") >= 0 OrElse Subject.IndexOf("Important") >= 0 Then Return 2
            If Subject.IndexOf("FW:") >= 0 AndAlso Delay > TimeSpan.FromHours(48) Then Return 0
            Return 1
        End Function
    End Class

    Public Enum MailFolder
        All = 0
        General
        Management
        IT
        Sales
        Support
        Engineering
        HR
        Design
    End Enum

    Public Enum MailEmployeeStatus
        Available
        Away
        Busy
    End Enum

    Public Module PlainTextProvider

        Private rich As RichEditDocumentServer = New RichEditDocumentServer()

        Public Function GetPlainText(ByVal mhtText As String) As String
            rich.MhtText = mhtText
            Return If(String.IsNullOrEmpty(rich.Text), "", rich.Text.TrimStart().Substring(CInt(0), CInt(Math.Min(CInt(100), CInt(mhtText.Length)))).Replace(CChar(Microsoft.VisualBasic.Strings.ChrW(13)), CChar(" "c)).Replace(Microsoft.VisualBasic.Strings.ChrW(10), " "c))
        End Function
    End Module
End Namespace
