Imports DevExpress.DemoData.Models.Mapping
Imports DevExpress.Xpf.DemoCenterBase
Imports System.Collections.ObjectModel
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports System.Data.SQLite
Imports System.IO
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.DataAnnotations

Namespace DevExpress.DemoData.Models

    Public Partial Class Department

        Public Property ID As Long

        Public Property Name As String
    End Class
End Namespace

Namespace DevExpress.DemoData

    Public Class IssueDataLoader
        Inherits DataLoaderBase

        Private itemsLoaded As Boolean = False

        Private projectsLoaded As Boolean = False

        Private usersLoaded As Boolean = False

        Private context As IssueListContext = New IssueListContext()

        Public ReadOnly Property Items As ObservableCollection(Of Item)
            Get
                LoadIfNeed(itemsLoaded, context.Items)
                Return context.Items.Local
            End Get
        End Property

        Public ReadOnly Property Projects As ObservableCollection(Of Project)
            Get
                LoadIfNeed(projectsLoaded, context.Projects)
                Return context.Projects.Local
            End Get
        End Property

        Public ReadOnly Property Users As ObservableCollection(Of User)
            Get
                LoadIfNeed(usersLoaded, context.Users)
                Return context.Users.Local
            End Get
        End Property
    End Class

    Public Enum IssueType
        Request
        Bug
    End Enum

    Friend Module IssuePriorityHelper

        Public Const CellMergingImagesPath As String = "pack://application:,,,/GridDemo;component/Images/CellMerging/"
    End Module

    Public Enum IssuePriority
        <Image(CellMergingImagesPath & "Low.svg")>
        Low
        <Image(CellMergingImagesPath & "Medium.svg")>
        Medium
        <Image(CellMergingImagesPath & "High.svg")>
        High
    End Enum

    Public Enum Status
        [New]
        Postponed
        Fixed
        Rejected
    End Enum
End Namespace

Namespace DevExpress.DemoData.Models

    Public Partial Class Item

        Public Property ID As Long

        Public Property Name As String

        Public Property Type As IssueType

        Public Property ProjectID As Long?

        Public Property Priority As IssuePriority?

        Public Property Status As Status?

        Public Property CreatorID As Long?

        Public Property CreatedDate As Date?

        Public Property OwnerID As Long?

        Public Property ModifiedDate As Date?

        Public Property FixedDate As Date?

        Public Property Description As String

        Public Property Resolution As String
    End Class
End Namespace

Namespace DevExpress.DemoData.Models

    Public Partial Class IssueListContext
        Inherits DbContext

        Public Sub New()
            MyBase.New(CreateConnection(), True)
        End Sub

        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Public Sub New(ByVal connection As DbConnection)
            MyBase.New(connection, True)
        End Sub

        Private Shared defaultContext As IssueListContext

        Public Shared ReadOnly Property [Default] As IssueListContext
            Get
                Return If(defaultContext, Function()
                    defaultContext = New IssueListContext()
                    Return defaultContext
                End Function())
            End Get
        End Property

        Shared Sub New()
            Database.SetInitializer(Of IssueListContext)(Nothing)
        End Sub

        Public Property Departments As DbSet(Of Department)

        Public Property Items As DbSet(Of Item)

        Public Property Projects As DbSet(Of Project)

        Public Property ProjectTeams As DbSet(Of ProjectTeam)

        Public Property Schedulers As DbSet(Of Scheduler)

        Public Property Tasks As DbSet(Of Task)

        Public Property Users As DbSet(Of User)

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New DepartmentMap())
            modelBuilder.Configurations.Add(New ItemMap())
            modelBuilder.Configurations.Add(New ProjectMap())
            modelBuilder.Configurations.Add(New ProjectTeamMap())
            modelBuilder.Configurations.Add(New SchedulerMap())
            modelBuilder.Configurations.Add(New TaskMap())
            modelBuilder.Configurations.Add(New UserMap())
        End Sub

        Private Shared filePath As String

        Private Shared Function CreateConnection() As DbConnection
            If Equals(filePath, Nothing) Then filePath = DemoRunner.GetDBFileSafe("issue-list.db")
            Try
                Dim attributes = File.GetAttributes(filePath)
                If attributes.HasFlag(FileAttributes.ReadOnly) Then
                    File.SetAttributes(filePath, attributes And Not FileAttributes.ReadOnly)
                End If
            Catch
            End Try

            Dim connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection()
            If Not Equals(filePath, DemoRunner.DBFileFailedString) Then connection.ConnectionString = New SQLiteConnectionStringBuilder With {.DataSource = filePath}.ConnectionString
            Return connection
        End Function
    End Class
End Namespace

Namespace DevExpress.DemoData.Models

    Public Partial Class Project

        Public Property ID As Long

        Public Property Name As String

        Public Property ManagerID As Long?
    End Class
End Namespace

Namespace DevExpress.DemoData.Models

    Public Partial Class ProjectTeam

        Public Property ID As Long

        Public Property ProjectID As Long?

        Public Property UserID As Long?

        Public Property [Function] As String
    End Class
End Namespace

Namespace DevExpress.DemoData.Models

    Public Partial Class Scheduler

        Public Property ID As Long

        Public Property ProjectID As Long?

        Public Property UserID As Long?

        Public Property Sunday As Short?

        Public Property Monday As Short?

        Public Property Tuesday As Short?

        Public Property Wednesday As Short?

        Public Property Thursday As Short?

        Public Property Friday As Short?

        Public Property Saturday As Short?
    End Class
End Namespace

Namespace DevExpress.DemoData.Models

    Public Partial Class Task

        Public Property ID As Long

        Public Property Name As String

        Public Property ParentID As Long

        Public Property UserID As Long

        Public Property StartDate As Date?

        Public Property EndDate As Date?

        Public Property Done As Boolean

        Public Property Priority As Long?

        Public Property Complete As Double?
    End Class
End Namespace

Namespace DevExpress.DemoData.Models

    Public Partial Class User

        Public Property ID As Long

        Public Property FirstName As String

        Public Property MiddleName As String

        Public Property LastName As String

        Public Property Country As String

        Public Property PostalCode As String

        Public Property City As String

        Public Property Address As String

        Public Property Phone As String

        Public Property Fax As String

        Public Property Email As String

        Public Property HomePage As String

        Public Property DepartmentID As Long?

        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
    End Class
End Namespace

Namespace DevExpress.DemoData.Models.Mapping

    Public Class DepartmentMap
        Inherits EntityTypeConfiguration(Of Department)

        Public Sub New()
            HasKey(Function(t) t.ID)
            [Property](Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            [Property](Function(t) t.Name).HasMaxLength(100)
            ToTable("Departments")
            [Property](Function(t) t.ID).HasColumnName("ID")
            [Property](Function(t) t.Name).HasColumnName("Name")
        End Sub
    End Class
End Namespace

Namespace DevExpress.DemoData.Models.Mapping

    Public Class ItemMap
        Inherits EntityTypeConfiguration(Of Item)

        Public Sub New()
            HasKey(Function(t) New With {t.ID})
            [Property](Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            [Property](Function(t) t.Name).HasMaxLength(50)
            [Property](Function(t) t.Description).HasMaxLength(2147483647)
            [Property](Function(t) t.Resolution).HasMaxLength(2147483647)
            ToTable("Items")
            [Property](Function(t) t.ID).HasColumnName("ID")
            [Property](Function(t) t.Name).HasColumnName("Name")
            [Property](Function(t) t.Type).HasColumnName("Type")
            [Property](Function(t) t.ProjectID).HasColumnName("ProjectID")
            [Property](Function(t) t.Priority).HasColumnName("Priority")
            [Property](Function(t) t.Status).HasColumnName("Status")
            [Property](Function(t) t.CreatorID).HasColumnName("CreatorID")
            [Property](Function(t) t.CreatedDate).HasColumnName("CreatedDate")
            [Property](Function(t) t.OwnerID).HasColumnName("OwnerID")
            [Property](Function(t) t.ModifiedDate).HasColumnName("ModifiedDate")
            [Property](Function(t) t.FixedDate).HasColumnName("FixedDate")
            [Property](Function(t) t.Description).HasColumnName("Description")
            [Property](Function(t) t.Resolution).HasColumnName("Resolution")
        End Sub
    End Class
End Namespace

Namespace DevExpress.DemoData.Models.Mapping

    Public Class ProjectMap
        Inherits EntityTypeConfiguration(Of Project)

        Public Sub New()
            HasKey(Function(t) t.ID)
            [Property](Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            [Property](Function(t) t.Name).HasMaxLength(100)
            ToTable("Projects")
            [Property](Function(t) t.ID).HasColumnName("ID")
            [Property](Function(t) t.Name).HasColumnName("Name")
            [Property](Function(t) t.ManagerID).HasColumnName("ManagerID")
        End Sub
    End Class
End Namespace

Namespace DevExpress.DemoData.Models.Mapping

    Public Class ProjectTeamMap
        Inherits EntityTypeConfiguration(Of ProjectTeam)

        Public Sub New()
            HasKey(Function(t) t.ID)
            [Property](Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            [Property](Function(t) t.Function).HasMaxLength(50)
            ToTable("ProjectTeam")
            [Property](Function(t) t.ID).HasColumnName("ID")
            [Property](Function(t) t.ProjectID).HasColumnName("ProjectID")
            [Property](Function(t) t.UserID).HasColumnName("UserID")
            [Property](Function(t) t.Function).HasColumnName("Function")
        End Sub
    End Class
End Namespace

Namespace DevExpress.DemoData.Models.Mapping

    Public Class SchedulerMap
        Inherits EntityTypeConfiguration(Of Scheduler)

        Public Sub New()
            HasKey(Function(t) t.ID)
            [Property](Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            ToTable("Scheduler")
            [Property](Function(t) t.ID).HasColumnName("ID")
            [Property](Function(t) t.ProjectID).HasColumnName("ProjectID")
            [Property](Function(t) t.UserID).HasColumnName("UserID")
            [Property](Function(t) t.Sunday).HasColumnName("Sunday")
            [Property](Function(t) t.Monday).HasColumnName("Monday")
            [Property](Function(t) t.Tuesday).HasColumnName("Tuesday")
            [Property](Function(t) t.Wednesday).HasColumnName("Wednesday")
            [Property](Function(t) t.Thursday).HasColumnName("Thursday")
            [Property](Function(t) t.Friday).HasColumnName("Friday")
            [Property](Function(t) t.Saturday).HasColumnName("Saturday")
        End Sub
    End Class
End Namespace

Namespace DevExpress.DemoData.Models.Mapping

    Public Class TaskMap
        Inherits EntityTypeConfiguration(Of Task)

        Public Sub New()
            HasKey(Function(t) New With {t.ID, t.Name, t.ParentID, t.UserID, t.Done})
            [Property](Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            [Property](Function(t) t.Name).IsRequired().HasMaxLength(50)
            [Property](Function(t) t.ParentID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            [Property](Function(t) t.UserID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            ToTable("Tasks")
            [Property](Function(t) t.ID).HasColumnName("ID")
            [Property](Function(t) t.Name).HasColumnName("Name")
            [Property](Function(t) t.ParentID).HasColumnName("ParentID")
            [Property](Function(t) t.UserID).HasColumnName("UserID")
            [Property](Function(t) t.StartDate).HasColumnName("StartDate")
            [Property](Function(t) t.EndDate).HasColumnName("EndDate")
            [Property](Function(t) t.Done).HasColumnName("Done")
            [Property](Function(t) t.Priority).HasColumnName("Priority")
            [Property](Function(t) t.Complete).HasColumnName("Complete")
        End Sub
    End Class
End Namespace

Namespace DevExpress.DemoData.Models.Mapping

    Public Class UserMap
        Inherits EntityTypeConfiguration(Of User)

        Public Sub New()
            HasKey(Function(t) t.ID)
            [Property](Function(t) t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            [Property](Function(t) t.FirstName).HasMaxLength(25)
            [Property](Function(t) t.MiddleName).HasMaxLength(20)
            [Property](Function(t) t.LastName).HasMaxLength(25)
            [Property](Function(t) t.Country).HasMaxLength(15)
            [Property](Function(t) t.PostalCode).HasMaxLength(10)
            [Property](Function(t) t.City).HasMaxLength(15)
            [Property](Function(t) t.Address).HasMaxLength(60)
            [Property](Function(t) t.Phone).HasMaxLength(24)
            [Property](Function(t) t.Fax).HasMaxLength(24)
            [Property](Function(t) t.Email).HasMaxLength(50)
            [Property](Function(t) t.HomePage).HasMaxLength(50)
            ToTable("Users")
            [Property](Function(t) t.ID).HasColumnName("ID")
            [Property](Function(t) t.FirstName).HasColumnName("FirstName")
            [Property](Function(t) t.MiddleName).HasColumnName("MiddleName")
            [Property](Function(t) t.LastName).HasColumnName("LastName")
            [Property](Function(t) t.Country).HasColumnName("Country")
            [Property](Function(t) t.PostalCode).HasColumnName("PostalCode")
            [Property](Function(t) t.City).HasColumnName("City")
            [Property](Function(t) t.Address).HasColumnName("Address")
            [Property](Function(t) t.Phone).HasColumnName("Phone")
            [Property](Function(t) t.Fax).HasColumnName("Fax")
            [Property](Function(t) t.Email).HasColumnName("Email")
            [Property](Function(t) t.HomePage).HasColumnName("HomePage")
            [Property](Function(t) t.DepartmentID).HasColumnName("DepartmentID")
        End Sub
    End Class
End Namespace
