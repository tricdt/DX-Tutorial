Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.Entity
Imports System.IO
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.DevAV
Imports DevExpress.Xpf.Core.Native
Imports System.Windows
Imports System.ComponentModel

Namespace ProductsDemo.Modules

    Public Class DataBaseHelper
        Inherits DependencyObject

        Private _Contacts As List(Of ProductsDemo.Modules.Contact), _Tasks As List(Of ProductsDemo.Modules.Task), _Appointments As ObservableCollection(Of ProductsDemo.Modules.Appointment)

        Private Shared instanse As DataBaseHelper = Nothing

        Private employees As List(Of Employee)

        Shared Public ReadOnly Property Instance As DataBaseHelper
            Get
                If instanse Is Nothing Then
                    instanse = New DataBaseHelper()
                End If

                Return instanse
            End Get
        End Property

        Private Sub New()
            GetEmployees()
            Contacts = FillContacts()
            FillAppointments()
            Tasks = FillTasks()
        End Sub

        Private Function FillTasks() As List(Of Task)
            Dim ret As List(Of Task) = New List(Of Task)()
            For i As Integer = 0 To TaskGenerator.CustomerCount - 1
                For Each s As String In CollectionResources.OfficeTasks
                    ret.Add(TaskGenerator.CreateTask(s, TaskCategory.Work))
                Next
            Next

            For Each s As String In CollectionResources.HouseTasks
                ret.Add(TaskGenerator.CreateTask(s, TaskCategory.HouseChores))
            Next

            For Each s As String In CollectionResources.ShoppingTasks
                ret.Add(TaskGenerator.CreateTask(s, TaskCategory.Shopping))
            Next

            Return ret
        End Function

        Private Function FillContacts() As List(Of Contact)
            Dim result = New List(Of Contact)()
            employees.ForEach(Sub(e)
                Dim contact = CreateContact(e)
                FillPersonInformation(contact, e)
                result.Add(contact)
            End Sub)
            Return result
        End Function

        Private Function CreateContact(ByVal employee As Employee) As Contact
            Dim contact = New Contact()
            contact.Name = New Name()
            contact.Name.MiddleName = String.Empty
            contact.Email = employee.Email
            contact.Address = New Address(employee.Address.ToString())
            contact.Phone = employee.MobilePhone
            contact.Activity = GetContactActivity(employee.Id)
            If employee.Picture IsNot Nothing Then contact.Photo = ImageHelper.CreateImageFromStream(New MemoryStream(TryCast(employee.Picture.Data, Byte())))
            Return contact
        End Function

        Private Function GetContactActivity(ByVal employeeId As Long) As List(Of Integer)
            Dim activity As List(Of Integer) = New List(Of Integer)()
            Dim rnd As Random = New Random(CInt(employeeId))
            For i As Integer = 0 To 6 - 1
                activity.Add(rnd.Next(0, 10))
            Next

            Return activity
        End Function

        Private Shared Sub FillPersonInformation(ByVal contact As Contact, ByVal employee As Employee)
            contact.Name.FirstName = employee.FirstName
            contact.Name.LastName = employee.LastName
            contact.Prefix = employee.Prefix
            contact.BirthDate = employee.BirthDate.Value
        End Sub

        Private Sub FillAppointments()
            Appointments = New ObservableCollection(Of Appointment)()
            Dim appointmentsTable As DataTable = CreateDataTable("Appointments")
            If appointmentsTable IsNot Nothing AndAlso appointmentsTable.Rows.Count > 0 Then
                For Each row As DataRow In appointmentsTable.Rows
                    Appointments.Add(CreateAppointment(row))
                Next
            End If
        End Sub

        Private Function CreateDataTable(ByVal table As String) As DataTable
            Dim dataSet As DataSet = New DataSet()
            Try
                Dim dataFile As String = Utils.GetRelativePath("MailDevAv.xml")
                If Not Equals(dataFile, String.Empty) Then
                    Dim fi As FileInfo = New FileInfo(dataFile)
                    dataSet.ReadXml(fi.FullName)
                    Return dataSet.Tables(table)
                End If
            Catch __unusedException1__ As Exception
            End Try

            Return Nothing
        End Function

        Private Function CreateAppointment(ByVal row As DataRow) As Appointment
            Dim appointment As Appointment = New Appointment()
            appointment.EventType = CType(row("EventType"), Integer?)
            appointment.StartDate = CType(row("StartDate"), Date?)
            appointment.EndDate = CType(row("EndDate"), Date?)
            appointment.AllDay = CType(row("AllDay"), Boolean?)
            appointment.Subject = Convert.ToString(row("Subject"))
            appointment.Location = Convert.ToString(row("Location"))
            appointment.Description = Convert.ToString(row("Description"))
            appointment.Status = CType(row("Status"), Integer?)
            appointment.Label = CType(row("Label"), Integer?)
            appointment.RecurrenceInfo = Convert.ToString(row("RecurrenceInfo"))
            appointment.ReminderInfo = Convert.ToString(row("ReminderInfo"))
            appointment.ContactInfo = Convert.ToString(row("ContactInfo"))
            Return appointment
        End Function

        Public Property Contacts As List(Of Contact)
            Get
                Return _Contacts
            End Get

            Private Set(ByVal value As List(Of Contact))
                _Contacts = value
            End Set
        End Property

        Public Property Tasks As List(Of Task)
            Get
                Return _Tasks
            End Get

            Private Set(ByVal value As List(Of Task))
                _Tasks = value
            End Set
        End Property

        Public Property Appointments As ObservableCollection(Of Appointment)
            Get
                Return _Appointments
            End Get

            Private Set(ByVal value As ObservableCollection(Of Appointment))
                _Appointments = value
            End Set
        End Property

        Private Sub GetEmployees()
            employees = If(IsInDesignMode(), New List(Of Employee)(), DevAVHelper.Employees)
        End Sub

        Private Function IsInDesignMode() As Boolean
            Return DesignerProperties.GetIsInDesignMode(Me)
        End Function
    End Class

    Public Module DevAVHelper

        Private employeesField As List(Of Employee) = Nothing

        Private unknownUserPictureField As ImageSource

        Friend ReadOnly Property Employees As List(Of Employee)
            Get
                If employeesField Is Nothing Then
                    Dim devAvDb As DevAVDb = New DevAVDb()
                    devAvDb.Employees.Load()
                    employeesField = devAvDb.Employees.Local.ToList()
                End If

                Return employeesField
            End Get
        End Property

        Friend ReadOnly Property UnknownUserPicture As ImageSource
            Get
                If unknownUserPictureField Is Nothing Then
                    unknownUserPictureField = New BitmapImage(GetAppImageUri("Contacts/Unknown-user", UriKind.RelativeOrAbsolute))
                    Return unknownUserPictureField
                End If

                Return unknownUserPictureField
            End Get
        End Property

        Public Function GetNameByEmail(ByVal mail As String, ByVal Optional isDesignMode As Boolean = False) As String
            If String.IsNullOrEmpty(mail) OrElse isDesignMode Then Return String.Empty
            Dim employee = Employees.FirstOrDefault(Function(p) Equals(p.Email, mail))
            Return If(employee Is Nothing, String.Empty, employee.FullName)
        End Function

        Public Function GetPictureByEmail(ByVal mail As String, ByVal Optional isDesignMode As Boolean = False) As ImageSource
            If String.IsNullOrEmpty(mail) OrElse isDesignMode Then Return UnknownUserPicture
            Dim employee = Employees.FirstOrDefault(Function(p) Equals(p.Email, mail))
            If employee IsNot Nothing AndAlso employee.Picture IsNot Nothing AndAlso employee.Picture.Data IsNot Nothing Then Return ImageHelper.CreateImageFromStream(New MemoryStream(TryCast(employee.Picture.Data, Byte())))
            Return UnknownUserPicture
        End Function

        Private Function GetAppImageUri(ByVal path As String, ByVal Optional uriKind As UriKind = UriKind.Relative) As Uri
            Return New Uri(String.Format("/DevExpress.ProductsDemo.Xpf;component/Images/{0}.png", path), uriKind)
        End Function
    End Module

    Friend Class TaskGenerator

        Public Shared CustomerCount As Integer = 10

        Private Shared rndGenerator As Random = New Random()

        Private Shared customersField As List(Of Contact)

        Friend Shared ReadOnly Property Customers As List(Of Contact)
            Get
                If customersField Is Nothing Then
                    customersField = New List(Of Contact)()
                    Dim temp As List(Of Contact) = DataBaseHelper.Instance.Contacts
                    If temp.Count > CustomerCount Then
                        While customersField.Count < CustomerCount
                            Dim contact As Contact = GetCustomer(rndGenerator.Next(temp.Count - 1), customersField, temp)
                            If contact IsNot Nothing Then customersField.Add(contact)
                        End While
                    End If
                End If

                Return customersField
            End Get
        End Property

        Private Shared Function GetCustomer(ByVal index As Integer, ByVal customers As List(Of Contact), ByVal contacts As List(Of Contact)) As Contact
            Dim contact As Contact = contacts(index)
            For Each c As Contact In customers
                If ReferenceEquals(c, contact) Then Return Nothing
            Next

            Return contact
        End Function

        Public Shared Function CreateTask(ByVal subject As String, ByVal category As TaskCategory) As Task
            Dim task As Task = New Task(subject, category, Date.Now.AddHours(-rndGenerator.Next(96)))
            Dim rndStatus As Integer = rndGenerator.Next(10)
            If task.TimeDiff.TotalHours > 12 Then
                If task.TimeDiff.TotalHours > 80 Then
                    task.Status = TaskStatus.Completed
                Else
                    task.Status = TaskStatus.InProgress
                    task.PercentComplete = rndGenerator.Next(9) * 10
                End If

                task.StartDate = task.CreatedDate.AddMinutes(rndGenerator.Next(720)).Date
            End If

            If rndStatus <> 5 Then task.DueDate = task.CreatedDate.AddHours(90 - rndStatus * 9 + 24).Date
            If rndStatus > 8 Then task.Priority = TaskPriority.High
            If rndStatus < 3 Then task.Priority = TaskPriority.Low
            If rndStatus = 6 AndAlso task.Status = TaskStatus.InProgress Then task.Status = TaskStatus.Deferred
            If rndStatus = 4 AndAlso task.Status = TaskStatus.InProgress AndAlso task.PercentComplete < 40 Then task.Status = TaskStatus.WaitingOnSomeoneElse
            If task.Category = TaskCategory.Work AndAlso rndStatus <> 7 AndAlso Customers.Count > 0 Then task.AssignTo = Customers(rndGenerator.Next(Customers.Count))
            If task.Status = TaskStatus.Completed Then
                If task.StartDate Is Nothing Then task.StartDate = task.CreatedDate.AddHours(12).Date
                task.CompletedDate = task.StartDate.Value.AddHours(rndGenerator.Next(48) + 24)
            End If

            Return task
        End Function
    End Class

    Public Class CollectionResources

        Public Shared HouseTasks As List(Of String) = New List(Of String)() From {"Set-up home theater (surround sound) system", "Install 3 overhead lights in bedroom", "Change light bulbs in backyard", "Install a programmable thermostat", "Install ceiling fan in John's bedroom", "Install LED lights in kitchen", "Check wiring in main electricity panel", "Replace master bedroom light switch with dimmer", "Install an new electric outlet in garage", "Install electric outlet and ethernet drop in closet", "Install chandelier in dining room", "Hook up DVD Player to TV for kids", "Clean the House top to bottom", "Light cleaning of the house", "Clean the entire house", "Clean and organize basement", "Pick up clothes for charity event", "Ironing, laundry and vacuuming", "Take kids to park and play baseball on Sunday", "Clean art studio", "Bake brownies and send them to neighbors", "Assemble Kitchen Cart", "Move piano", "Clean backyard", "Clean out garage", "Organize guest bedroom", "Clean out closet", "Preapre for yard sale", "Sorting clothing for give-away", "Organize Storage Room"}

        Public Shared ShoppingTasks As List(Of String) = New List(Of String)() From {"Shopping at Macy's", "Return coffee machine", "Purchase plastic trash bins", "Shop for dinner ingredients at the store", "Buy new utensils for kitchen", "Send post card to Timothy", "Buy dining table and TV stand online", "Buy ingredients for Pasta Bolognese", "Size 3 diapers (3 cases)", "Order 3 pizzas", "Find out where to buy the new tablet", "Buy broccoli and tomatoes", "Buy bottle of Champagne", "Grocery shopping at Market Basket", "Find a bike at a store close to me", "Return jeans at JCrew", "Buy dog food for Fido", "Buy rigid foam insulation", "Purchase 3 24-packs of bottled Coke", "Purchase & deliver flowers to my home"}

        Public Shared OfficeTasks As List(Of String) = New List(Of String)() From {"Input bank statement transactions into Excel spreadsheet", "Schedule appointments and pay bills", "Place new address stickers on envelopes", "Set up and arrange appointments", "Copy PDF file into Word", "Organize business expenses (last 6 months)", "Return samples to vendor", "Organize receipts and match them up with business expenses and trips ", "File papers and receipts", "Ship out CDs to customers", "Respond to e-mails until noon", "Enter expenses into an online accounting system", "Conduct inventory of all furniture in office", "Arrange travel to conference", "Staple flyers to gift bags", "File and shred mail", "Print copies of brochures", "Enter all receipts into an Excel spreadsheet", "Research possible vendors", "Sort through paper receipts", "Re-package products for retail sale", "Scan docs, and put in desktop folder", "Print registration stickers"}
    End Class
End Namespace
