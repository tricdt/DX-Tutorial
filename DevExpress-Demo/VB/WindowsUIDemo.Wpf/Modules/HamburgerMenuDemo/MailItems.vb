Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Reflection
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.XtraRichEdit
Imports System.IO

Namespace WindowsUIDemo

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

        Private messagesField As ObservableCollection(Of Message)

        Private ReadOnly Property MailTable As DataTable
            Get
                Return CreateDataTable("Messages")
            End Get
        End Property

        Private Function CreateDataTable(ByVal table As String) As DataTable
            If DataSet Is Nothing Then
                DataSet = New DataSet()
                Dim assembly As Assembly = GetType(Message).Assembly
                Using stream As Stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("WindowsUIDemo.Data.", assembly) & "MailDevAv.xml")
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
                        Dim index As Integer = 0
                        If tbl IsNot Nothing Then
                            For Each row As DataRow In tbl.Rows
                                Dim newMessage = New Message(row)
                                newMessage.UpdateMailType(GetMailType(Math.Min(Threading.Interlocked.Increment(index), index - 1)))
                                messagesField.Add(newMessage)
                            Next
                        End If
                    End If
                Catch __unusedException1__ As Exception
                    messagesField = New ObservableCollection(Of Message)()
                End Try

                Return messagesField
            End Get
        End Property

        Private Function GetMailType(ByVal rowIndex As Integer) As MailType
            If rowIndex Mod 15 = 0 Then Return MailType.Draft
            If rowIndex Mod 7 = 0 Then Return MailType.Deleted
            If rowIndex Mod 5 = 0 Then Return MailType.Sent
            Return MailType.Inbox
        End Function
    End Module

    Public Class Message
        Implements INotifyPropertyChanged

        Private ReadOnly Property random As Random
            Get
                Return New Random(Date.Now.Millisecond)
            End Get
        End Property

        Private _row As DataRow

        Private _received As Date

        Private _read, _deleted, _hasAttachment As Boolean

        Private _priority As Integer = 1

        Private _mailType As MailType

        Private _mailFolder As MailFolder

        Private _plainText As String, _from As String = String.Empty, _subject As String = String.Empty, _text As String = String.Empty

        Public Sub New()
            _received = Date.Now
        End Sub

        Public Sub New(ByVal row As DataRow)
            Employee = New MailEmployee(row.GetParentRow(DataSet.Relations(0)))
            _row = row
            _received = Date.Now.AddDays(CInt(row("Day")))
            _from = String.Format("{0}", row("From"))
            _subject = String.Format("{0}", row("Subject"))
            _hasAttachment = CBool(row("HasAttachment"))
            IsUnread = False
            If random.Next() Mod 10 = 0 Then IsUnread = True
            _text = String.Format("{0}", row("Text"))
            _deleted = False
            _priority = CalcPriority()
            If Not(TypeOf row("CategoryID") Is DBNull) Then _mailFolder = CType(row("CategoryID"), MailFolder)
        End Sub

        Private Function CalcPriority() As Integer
            If Subject.IndexOf("Review") >= 0 OrElse Subject.IndexOf("Important") >= 0 Then Return 2
            If Subject.IndexOf("FW:") >= 0 AndAlso Delay > TimeSpan.FromHours(48) Then Return 0
            Return 1
        End Function

        Private _employee As MailEmployee

        Public Property Employee As MailEmployee
            Get
                Return _employee
            End Get

            Set(ByVal value As MailEmployee)
                _employee = value
            End Set
        End Property

        Public Property Received As Date
            Get
                Return _received
            End Get

            Set(ByVal value As Date)
                _received = value
            End Set
        End Property

        Public Property From As String
            Get
                Return _from
            End Get

            Set(ByVal value As String)
                _from = value
            End Set
        End Property

        Public Property Subject As String
            Get
                Return _subject
            End Get

            Set(ByVal value As String)
                _subject = value
            End Set
        End Property

        Public ReadOnly Property SubjectDisplayText As String
            Get
                Return String.Format("{0}", Subject)
            End Get
        End Property

        Public ReadOnly Property Attachment As Boolean
            Get
                Return _hasAttachment
            End Get
        End Property

        Public ReadOnly Property Read As Integer
            Get
                Return If(_read, 1, 0)
            End Get
        End Property

        Public Property Priority As Integer
            Get
                Return _priority
            End Get

            Set(ByVal value As Integer)
                _priority = value
            End Set
        End Property

        Public Property IsUnread As Boolean
            Get
                Return Not _read
            End Get

            Set(ByVal value As Boolean)
                _read = Not value
            End Set
        End Property

        Friend ReadOnly Property Folder As String
            Get
                Return String.Format("{0}", _mailFolder)
            End Get
        End Property

        Public Property Text As String
            Get
                Return _text
            End Get

            Set(ByVal value As String)
                _text = value
                OnPropertyChanged("PlainText")
            End Set
        End Property

        Public ReadOnly Property PlainText As String
            Get
                If Equals(_plainText, Nothing) Then _plainText = GetPlainText(Text)
                Return _plainText
            End Get
        End Property

        Public Property MailType As MailType
            Get
                Return _mailType
            End Get

            Set(ByVal value As MailType)
                _mailType = value
            End Set
        End Property

        Public Property MailFolder As MailFolder
            Get
                Return _mailFolder
            End Get

            Set(ByVal value As MailFolder)
                _mailFolder = value
            End Set
        End Property

        Public Property Deleted As Boolean
            Get
                Return _deleted
            End Get

            Set(ByVal value As Boolean)
                _deleted = value
            End Set
        End Property

        Friend ReadOnly Property Delay As TimeSpan
            Get
                Return Date.Now - _received
            End Get
        End Property

        Public Sub ToggleRead()
            _read = Not _read
            OnPropertyChanged("Read")
            OnPropertyChanged("IsUnread")
        End Sub

        Public Sub UpdateMailType(ByVal type As MailType)
            _mailType = type
            OnPropertyChanged("MailType")
        End Sub

#Region "INotifyPropertyChanged"
        Private Sub OnPropertyChanged(ByVal Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#End Region
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

    Public Enum MailType
        Inbox
        Deleted
        Sent
        Draft
    End Enum

    Friend Module ObjectHelper

        Private rich As RichEditDocumentServer = New RichEditDocumentServer()

        Public Function GetPlainText(ByVal mhtText As String) As String
            rich.MhtText = mhtText
            Return rich.Text.TrimStart().Substring(CInt(0), CInt(Math.Min(CInt(100), CInt(mhtText.Length)))).Replace(CChar(Microsoft.VisualBasic.Strings.ChrW(13)), CChar(" "c)).Replace(Microsoft.VisualBasic.Strings.ChrW(10), " "c)
        End Function
    End Module

    Public Class MailEmployee

        Private _FirstName As String, _LastName As String, _FullName As String, _Gender As String, _Image As Byte()

        Public Sub New(ByVal row As DataRow)
            FirstName = String.Format("{0}", row("FirstName"))
            LastName = String.Format("{0}", row("LastName"))
            FullName = String.Format("{0} {1}", FirstName, LastName)
            Gender = String.Format("{0}", row("Gender"))
            Dim employee = GetEmployee(String.Format("{0}", row("Email")))
            Image = Nothing
            If employee IsNot Nothing Then
                FullName = employee.FullName
                Dim picture = employee.Picture
                If picture IsNot Nothing Then Image = picture.Data
                Return
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
End Namespace
