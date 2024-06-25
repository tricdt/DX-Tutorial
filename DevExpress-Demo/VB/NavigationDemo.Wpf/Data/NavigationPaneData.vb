Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace NavigationDemo

    Public Module NavigationPaneData

        Public rnd As System.Random = New System.Random()

        Private users As String() = New String() {"Peter Dolan", "Ryan Fischer", "Richard Fisher", "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"}

        Private subject As String() = New String() {"Implementing DevExpress MasterView control into Accounting System.", "Web Edition: Data Entry Page. The issue with date validation.", "Payables Due Calculator. It is ready for testing.", "Web Edition: Search Page. It is ready for testing.", "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.", "Receivables Calculator. Where can I found the complete specs", "Ledger: Inconsistency. Please fix it.", "Receivables Printing. It is ready for testing.", "Screen Redraw. Somebody has to look at it.", "Email System. What library we are going to use?", "Adding New Vendors Fails. This module doesn't work completely!", "History. Will we track the sales history in our system?", "Main Menu: Add a File menu. File menu is missed!!!", "Currency Mask. The current currency mask in completely inconvinience.", "Drag & Drop. In the schedule module drag & drop is not available.", "Data Import. What competitors databases will we support?", "Reports. The list of incomplete reports.", "Data Archiving. This features is still missed in our application", "Email Attachments. How to add the multiple attachment? I did not find a way to do it.", "Check Register. We are using different paths for different modules.", "Data Export. Our customers asked for export into Excel"}

        Private Function GetBoolean() As Boolean
            Dim ret As Integer = NavigationDemo.NavigationPaneData.rnd.[Next](7)
            Return ret > 4
        End Function

        Private Function GetIsRead() As Boolean
            Dim ret As Integer = NavigationDemo.NavigationPaneData.rnd.[Next](7)
            Return ret > 1
        End Function

        Private Function GetImportance() As Integer
            Dim ret As Integer = NavigationDemo.NavigationPaneData.rnd.[Next](7)
            If ret > 2 Then ret = 1
            Return ret
        End Function

        Private Function GetSent() As DateTime
            Dim ret As System.DateTime = System.DateTime.Now
            Dim r As Integer = NavigationDemo.NavigationPaneData.rnd.[Next](12)
            If r > 1 Then ret = ret.AddDays(-NavigationDemo.NavigationPaneData.rnd.[Next](50))
            ret = ret.AddMinutes(-NavigationDemo.NavigationPaneData.rnd.[Next](ret.Minute + ret.Hour * 60 + 360))
            Return ret
        End Function

        Private Function GetReceived(ByVal sent As System.DateTime) As DateTime
            Return sent.AddMinutes(10 + NavigationDemo.NavigationPaneData.rnd.[Next](120))
        End Function

        Private Function GetSubject() As String
            Return NavigationDemo.NavigationPaneData.subject(NavigationDemo.NavigationPaneData.rnd.[Next](NavigationDemo.NavigationPaneData.subject.Length - 1))
        End Function

        Private Function GetRandomUser() As String
            Return NavigationDemo.NavigationPaneData.users(NavigationDemo.NavigationPaneData.rnd.[Next](NavigationDemo.NavigationPaneData.users.Length - 2))
        End Function

        Private Function GetFixedUser() As String
            Return NavigationDemo.NavigationPaneData.users(NavigationDemo.NavigationPaneData.users.Length - 1)
        End Function

        Public ReadOnly Property JournalData As IList(Of NavigationDemo.JournalItem)
            Get
                Return NavigationDemo.NavigationPaneData.journalDataField
            End Get
        End Property

        Public ReadOnly Property NotesData As IList(Of NavigationDemo.NotesItem)
            Get
                Return NavigationDemo.NavigationPaneData.notesDataField
            End Get
        End Property

        Public ReadOnly Property TasksData As IList(Of NavigationDemo.TasksItem)
            Get
                Return NavigationDemo.NavigationPaneData.tasksDataField
            End Get
        End Property

        Public ReadOnly Property ContactsData As IList(Of NavigationDemo.ContactItem)
            Get
                Return NavigationDemo.NavigationPaneData.contactsDataField
            End Get
        End Property

        Public ReadOnly Property MailData As IDictionary(Of NavigationDemo.NavigationId, System.Collections.Generic.IList(Of NavigationDemo.MailItem))
            Get
                Return NavigationDemo.NavigationPaneData.mailDataField
            End Get
        End Property

        Private journalDataField As System.Collections.Generic.IList(Of NavigationDemo.JournalItem) = NavigationDemo.NavigationPaneData.CreateJournalData()

        Private notesDataField As System.Collections.Generic.IList(Of NavigationDemo.NotesItem) = NavigationDemo.NavigationPaneData.CreateNotesData()

        Private tasksDataField As System.Collections.Generic.IList(Of NavigationDemo.TasksItem) = NavigationDemo.NavigationPaneData.CreateTasksData()

        Private contactsDataField As System.Collections.Generic.IList(Of NavigationDemo.ContactItem) = NavigationDemo.NavigationPaneData.CreateContactsData()

        Private mailDataField As System.Collections.Generic.Dictionary(Of NavigationDemo.NavigationId, System.Collections.Generic.IList(Of NavigationDemo.MailItem)) = NavigationDemo.NavigationPaneData.CreateMailData()

        Private Function CreateJournalData() As IList(Of NavigationDemo.JournalItem)
            Dim result = New System.Collections.Generic.List(Of NavigationDemo.JournalItem)()
            For i As Integer = 0 To 10 - 1
                result.Add(New NavigationDemo.JournalItem(NavigationDemo.NavigationPaneData.GetSent(), NavigationDemo.NavigationPaneData.GetSubject()))
            Next

            Return result
        End Function

        Private Function CreateNotesData() As IList(Of NavigationDemo.NotesItem)
            Dim result = New System.Collections.Generic.List(Of NavigationDemo.NotesItem)()
            For i As Integer = 0 To 10 - 1
                result.Add(New NavigationDemo.NotesItem(NavigationDemo.NavigationPaneData.GetRandomUser(), NavigationDemo.NavigationPaneData.GetSubject()))
            Next

            Return result
        End Function

        Private Function CreateTasksData() As IList(Of NavigationDemo.TasksItem)
            Dim result = New System.Collections.Generic.List(Of NavigationDemo.TasksItem)()
            For i As Integer = 0 To 10 - 1
                result.Add(New NavigationDemo.TasksItem(NavigationDemo.NavigationPaneData.GetIcon("Tasks"), NavigationDemo.NavigationPaneData.GetBoolean(), NavigationDemo.NavigationPaneData.GetSubject(), NavigationDemo.NavigationPaneData.GetSent()))
            Next

            Return result
        End Function

        Private Function CreateMailData() As Dictionary(Of NavigationDemo.NavigationId, System.Collections.Generic.IList(Of NavigationDemo.MailItem))
            Dim result = New System.Collections.Generic.Dictionary(Of NavigationDemo.NavigationId, System.Collections.Generic.IList(Of NavigationDemo.MailItem))()
            result.Add(NavigationDemo.NavigationId.Inbox, NavigationDemo.NavigationPaneData.CreateMailCollection())
            result.Add(NavigationDemo.NavigationId.Outbox, NavigationDemo.NavigationPaneData.CreateEmptyMailCollection())
            result.Add(NavigationDemo.NavigationId.SentItems, NavigationDemo.NavigationPaneData.CreateMailCollection(allItemsRead:=True, fromFixedUser:=True))
            result.Add(NavigationDemo.NavigationId.DeletedItems, NavigationDemo.NavigationPaneData.CreateMailCollection(allItemsRead:=True))
            result.Add(NavigationDemo.NavigationId.Drafts, NavigationDemo.NavigationPaneData.CreateEmptyMailCollection())
            Return result
        End Function

        Private Function CreateMailCollection(ByVal Optional allItemsRead As Boolean? = Nothing, ByVal Optional fromFixedUser As Boolean = False) As IList(Of NavigationDemo.MailItem)
            Dim result = New System.Collections.Generic.List(Of NavigationDemo.MailItem)()
            For i As Integer = 0 To 80 - 1
                Dim sent As System.DateTime = NavigationDemo.NavigationPaneData.GetSent()
                result.Add(New NavigationDemo.MailItem(NavigationDemo.NavigationPaneData.GetImportance(), NavigationDemo.NavigationPaneData.GetBoolean(), If(allItemsRead, NavigationDemo.NavigationPaneData.GetIsRead()), NavigationDemo.NavigationPaneData.GetSubject(), If(fromFixedUser, NavigationDemo.NavigationPaneData.GetFixedUser(), NavigationDemo.NavigationPaneData.GetRandomUser()), If(fromFixedUser, NavigationDemo.NavigationPaneData.GetRandomUser(), NavigationDemo.NavigationPaneData.GetFixedUser()), sent, NavigationDemo.NavigationPaneData.GetReceived(sent)))
            Next

            Return result
        End Function

        Private Function CreateContactsData() As IList(Of NavigationDemo.ContactItem)
            Return New System.Collections.Generic.List(Of NavigationDemo.ContactItem)(DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData.DataSource.Take(15).[Select](Function(x) New NavigationDemo.ContactItem() With {.FirstName = x.FirstName, .LastName = x.LastName, .Email = x.EmailAddress}))
        End Function

        Private Function CreateEmptyMailCollection() As IList(Of NavigationDemo.MailItem)
            Return New System.Collections.Generic.List(Of NavigationDemo.MailItem)()
        End Function

        Public Function GetIcon(ByVal id As String) As Uri
            Return New System.Uri("/NavigationDemo;component/Images/Navigation/" & id & ".svg", System.UriKind.RelativeOrAbsolute)
        End Function
    End Module

    Public Class JournalItem

        Public Sub New()
            Me.[Date] = System.DateTime.Now
            Me.Time = System.DateTime.Now
        End Sub

        Public Sub New(ByVal dateTime As System.DateTime, ByVal description As String)
            Me.[Date] = dateTime
            Me.Time = dateTime
            Me.Description = description
        End Sub

        Public Property [Date] As DateTime

        Public Property Time As DateTime

        Public Property Description As String
    End Class

    Public Class NotesItem

        Public Sub New()
        End Sub

        Public Sub New(ByVal name As String, ByVal description As String)
            Me.Name = name
            Me.Description = description
        End Sub

        Public Property Name As String

        Public Property Description As String
    End Class

    Public Class TasksItem

        Public Sub New()
            Me.Image = NavigationDemo.NavigationPaneData.GetIcon("Tasks")
            Me.[Date] = System.DateTime.Now
        End Sub

        Public Sub New(ByVal image As System.Uri, ByVal check As Boolean, ByVal subject As String, ByVal [date] As System.DateTime)
            Me.Image = image
            Me.Check = check
            Me.Subject = subject
            Me.[Date] = [date]
        End Sub

        Public Property Image As Uri

        Public Property Check As Boolean

        Public Property Subject As String

        Public Property [Date] As DateTime
    End Class

    Public Class MailItem

        Public Sub New(ByVal importance As Integer, ByVal hasAttachment As Boolean, ByVal isRead As Boolean, ByVal subject As String, ByVal from As String, ByVal [to] As String, ByVal sent As System.DateTime, ByVal receive As System.DateTime)
            Me.Importance = importance
            Me.HasAttachment = hasAttachment
            Me.IsRead = isRead
            Me.Image = NavigationDemo.NavigationPaneData.GetIcon(If(Me.IsRead, "TextBox", "Mail"))
            Me.Subject = subject
            Me.From = from
            Me.[To] = [to]
            Me.Sent = sent
            Me.Receive = receive
        End Sub

        Public Property Importance As Integer

        Public Property HasAttachment As Boolean

        Public Property IsRead As Boolean

        Public Property Image As Uri

        Public Property Subject As String

        Public Property From As String

        Public Property [To] As String

        Public Property Sent As DateTime

        Public Property Receive As DateTime
    End Class

    Public Class ContactItem

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Email As String
    End Class
End Namespace
