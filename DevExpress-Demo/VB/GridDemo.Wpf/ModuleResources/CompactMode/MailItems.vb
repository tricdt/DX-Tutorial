Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Reflection
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.XtraRichEdit
Imports System.IO

Namespace GridDemo

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
                Dim assembly As Assembly = GetType(StockItems).Assembly
                Using stream As Stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) & "MailDevAv.xml")
                    DataSet.ReadXml(stream)
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

    Public Class MailEmployee

        Private _FirstName As String, _LastName As String, _FullName As String, _Gender As String, _Image As Byte()

        Public Sub New(ByVal row As DataRow)
            FirstName = String.Format("{0}", row("FirstName"))
            LastName = String.Format("{0}", row("LastName"))
            FullName = String.Format("{0} {1}", FirstName, LastName)
            Gender = String.Format("{0}", row("Gender"))
            Dim employee = GetEmployee(String.Format("{0}", row("Email")))
            If employee IsNot Nothing Then
                FullName = employee.FullName
                Dim picture = employee.Picture
                If picture IsNot Nothing Then Image = picture.Data
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

        Public Property Image As Byte()
            Get
                Return _Image
            End Get

            Private Set(ByVal value As Byte())
                _Image = value
            End Set
        End Property
    End Class

    Public Class Message

        Public Sub New(ByVal row As DataRow)
            Employee = New MailEmployee(row.GetParentRow(DataSet.Relations(0)))
            Received = Date.Now.AddDays(CInt(row("Day")))
            Subject = String.Format("{0}", row("Subject"))
            HasAttachment = CBool(row("HasAttachment"))
            Read = Delay > TimeSpan.FromHours(6)
            If Delay > TimeSpan.FromHours(50) AndAlso Delay < TimeSpan.FromHours(100) Then Read = False
            Text = String.Format("{0}", row("Text"))
            Priority = CalcPriority()
            If Not(TypeOf row("CategoryID") Is DBNull) Then MailFolder = CType(row("CategoryID"), MailFolder)
        End Sub

        Private _employee As MailEmployee

        Public Property Employee As MailEmployee
            Get
                Return _employee
            End Get

            Set(ByVal value As MailEmployee)
                _employee = value
            End Set
        End Property

        Private _received As Date

        Public Property Received As Date
            Get
                Return _received
            End Get

            Set(ByVal value As Date)
                _received = value
            End Set
        End Property

        Private _subject As String

        Public Property Subject As String
            Get
                Return _subject
            End Get

            Set(ByVal value As String)
                _subject = value
            End Set
        End Property

        Private _hasAttachment As Boolean

        Public Property HasAttachment As Boolean
            Get
                Return _hasAttachment
            End Get

            Set(ByVal value As Boolean)
                _hasAttachment = value
            End Set
        End Property

        Private _read As Boolean

        Public Property Read As Boolean
            Get
                Return _read
            End Get

            Set(ByVal value As Boolean)
                _read = value
            End Set
        End Property

        Private _text As String

        Public Property Text As String
            Get
                Return _text
            End Get

            Set(ByVal value As String)
                _text = value
            End Set
        End Property

        Private _priority As Integer

        Public Property Priority As Integer
            Get
                Return _priority
            End Get

            Set(ByVal value As Integer)
                _priority = value
            End Set
        End Property

        Private _mailFolder As MailFolder

        Public Property MailFolder As MailFolder
            Get
                Return _mailFolder
            End Get

            Set(ByVal value As MailFolder)
                _mailFolder = value
            End Set
        End Property

        Private _plainText As String

        Public ReadOnly Property PlainText As String
            Get
                If Equals(_plainText, Nothing) Then _plainText = GetPlainText(Text)
                Return _plainText
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

    Public Module PlainTextProvider

        Private rich As RichEditDocumentServer = New RichEditDocumentServer()

        Public Function GetPlainText(ByVal mhtText As String) As String
            rich.MhtText = mhtText
            Return If(String.IsNullOrEmpty(rich.Text), "", rich.Text.TrimStart().Substring(CInt(0), CInt(Math.Min(CInt(100), CInt(mhtText.Length)))).Replace(CChar(Microsoft.VisualBasic.Strings.ChrW(13)), CChar(" "c)).Replace(Microsoft.VisualBasic.Strings.ChrW(10), " "c))
        End Function
    End Module
End Namespace
