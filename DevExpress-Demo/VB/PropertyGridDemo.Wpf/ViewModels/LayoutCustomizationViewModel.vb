Imports System
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel
Imports System.Windows.Controls
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations

Namespace PropertyGridDemo

    Public Class LayoutCustomizationViewModel

        Public Sub New()
            Name = "Andrew Fuller"
            Photo = "Images/Persons/m03.jpg"
            Rank = 5
            InitializeSkills()
            InitializeAddress()
            Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."
        End Sub

        Public Function CanIncreaseRank() As Boolean
            Return Rank < 10
        End Function

        Public Function CanDecreaseRank() As Boolean
            Return Rank > 0
        End Function

        Public Sub IncreaseRank()
            Rank += 1
        End Sub

        Public Sub DecreaseRank()
            Rank -= 1
        End Sub

        Private Sub InitializeSkills()
            Careers = New ObservableCollection(Of CareerInfo)()
            Dim sManager = New CareerInfo()
            sManager.JobTitle = "Sales Manager"
            sManager.FromDate = New DateTime(1998, 05, 14)
            sManager.ToDate = New DateTime(2001, 03, 22)
            sManager.Skills.Add("Speaking")
            sManager.Skills.Add("Coordination")
            Dim sRepr = New CareerInfo()
            sRepr.JobTitle = "Sales Representative"
            sRepr.FromDate = New DateTime(2001, 04, 10)
            sRepr.ToDate = New DateTime(2007, 09, 11)
            sRepr.Skills.Add("Decision Making")
            Careers.Add(sManager)
            Careers.Add(sRepr)
        End Sub

        Private Sub InitializeAddress()
            Address = AddressInfo.Create()
            Address.Country = "USA"
            Address.City = "Tacoma"
            Address.Address = "908 W. Capital Way"
        End Sub

        Public Overridable Property Photo As String

        Public Overridable Property Name As String

        Public Overridable Property Rank As UInteger

        Public Overridable Property Careers As ObservableCollection(Of CareerInfo)

        Public Overridable Property Retired As Boolean

        Public Overridable Property Address As AddressInfo

        Public Overridable Property Notes As String
    End Class

    <POCOViewModel>
    Public Class AddressInfo

        Public Shared Function Create() As AddressInfo
            Return ViewModelSource.Create(Function() New AddressInfo())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Address As String

        Public Overridable Property City As String

        Public Overridable Property Country As String
    End Class

    Public Class CareerInfo

        Private _Skills As ObservableCollection(Of String)

        Public Sub New()
            Skills = New ObservableCollection(Of String)()
        End Sub

        Public Property JobTitle As String

        Public Property FromDate As Date

        Public Property ToDate As Date

        Public Property Skills As ObservableCollection(Of String)
            Get
                Return _Skills
            End Get

            Private Set(ByVal value As ObservableCollection(Of String))
                _Skills = value
            End Set
        End Property
    End Class
End Namespace
