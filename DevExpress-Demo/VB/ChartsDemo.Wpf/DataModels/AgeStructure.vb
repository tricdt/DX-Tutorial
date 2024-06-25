Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class AgeStructure

        Private ageStructureTableField As DataTable

        Private ReadOnly Property AgeStructureTable As DataTable
            Get
                If ageStructureTableField Is Nothing Then ageStructureTableField = LoadPopulationAgeStructure()
                Return ageStructureTableField
            End Get
        End Property

        Private Function LoadPopulationAgeStructure() As DataTable
            Dim uri As Uri = New Uri("pack://application:,,,/ChartsDemo;component/Data/Population.xml")
            Dim xmlStream As Stream = Application.GetResourceStream(uri).Stream
            Dim xmlDataSet As DataSet = New DataSet()
            xmlDataSet.ReadXml(xmlStream)
            xmlStream.Close()
            Return xmlDataSet.Tables("Population")
        End Function

        Public ReadOnly Property DataByAgeAndGender As IList
            Get
                Return AgeStructureTable.AsEnumerable().[Select](Function(row) New With {.GenderAge = New GenderAgeInfo(row.Field(Of String)("Gender"), row.Field(Of String)("Age")), .Country = row.Field(Of String)("Country"), .Population = row.Field(Of Long)("Population")}).ToList()
            End Get
        End Property

        Public Function GetDataByMaleAge() As IList
            Return AgeStructureTable.AsEnumerable().Where(Function(row) Equals(row.Field(Of String)("Gender"), "Male")).[Select](Function(row) New With {.Age = row.Field(Of String)("Age"), .Country = row.Field(Of String)("Country"), .Population = row.Field(Of Long)("Population")}).ToList()
        End Function

        Public Function GetPopulationAgeStructure() As IList
            Return AgeStructureTable.AsEnumerable().[Select](Function(row) New AgePopulation(row.Field(Of String)("Country"), row.Field(Of String)("Age"), row.Field(Of String)("Gender"), row.Field(Of Long)("Population"))).ToList()
        End Function

        Public Function GetGenderAgeItemsWithPopulation() As IList
            Dim genderAgePopulationItemPair As Dictionary(Of String, List(Of PopulationItem)) = New Dictionary(Of String, List(Of PopulationItem))()
            For Each row As DataRow In AgeStructureTable.AsEnumerable()
                Dim gender As String = row.Field(Of String)("Gender")
                Dim population As Long = row.Field(Of Long)("Population")
                Dim populationString As String = population.ToString("0,,.00")
                Dim country As String = row.Field(Of String)("Country")
                Dim age As String = row.Field(Of String)("Age")
                Dim key As String = gender & ": " & age
                Dim populationItemsBuffer As List(Of PopulationItem)
                If genderAgePopulationItemPair.TryGetValue(key, populationItemsBuffer) Then
                    populationItemsBuffer.Add(New PopulationItem(country, gender, population, populationString))
                Else
                    populationItemsBuffer = New List(Of PopulationItem)()
                    populationItemsBuffer.Add(New PopulationItem(country, gender, population, populationString))
                    genderAgePopulationItemPair.Add(key, populationItemsBuffer)
                End If
            Next

            Dim genderAgeItems As List(Of GenderAgeItem) = New List(Of GenderAgeItem)()
            For Each keyValue As KeyValuePair(Of String, List(Of PopulationItem)) In genderAgePopulationItemPair
                genderAgeItems.Add(GenderAgeItem.Create(keyValue.Key, keyValue.Value))
            Next

            Return genderAgeItems
        End Function
    End Class

    Public Structure GenderAgeInfo

        Private ReadOnly genderField As String

        Private ReadOnly ageField As String

        Public ReadOnly Property Gender As String
            Get
                Return genderField
            End Get
        End Property

        Public ReadOnly Property Age As String
            Get
                Return ageField
            End Get
        End Property

        Public Sub New(ByVal gender As String, ByVal age As String)
            genderField = gender
            ageField = age
        End Sub

        Public Overrides Function ToString() As String
            Return genderField & ": " & ageField
        End Function
    End Structure

    Public Class GenderAgeItem

        Public Shared Function Create(ByVal name As String, ByVal items As IList(Of PopulationItem)) As GenderAgeItem
            Return ViewModelSource.Create(Function() New GenderAgeItem(name, items))
        End Function

        Public Overridable Property Name As String

        Public Overridable Property Color As Color

        Public Overridable Property Items As IList(Of PopulationItem)

        Public Overridable Property IsShowLabelsChecked As Boolean

        Public Overridable Property IsShowToolTipsChecked As Boolean

        Public Overridable Property Model As Bar2DModel

        Public Overridable Property Legend As LegendViewModel

        Public Overridable Property Pane As PaneViewModel

        Protected Sub New(ByVal name As String, ByVal items As IList(Of PopulationItem))
            Me.Name = name
            Me.Items = items
            IsShowToolTipsChecked = True
        End Sub
    End Class

    Public Structure PopulationItem

        Private ReadOnly countryField As String

        Private ReadOnly genderField As String

        Private ReadOnly populationField As Long

        Private ReadOnly populationStringField As String

        Public ReadOnly Property Country As String
            Get
                Return countryField
            End Get
        End Property

        Public ReadOnly Property Gender As String
            Get
                Return genderField
            End Get
        End Property

        Public ReadOnly Property Population As Long
            Get
                Return populationField
            End Get
        End Property

        Public ReadOnly Property PopulationString As String
            Get
                Return populationStringField
            End Get
        End Property

        Public Sub New(ByVal country As String, ByVal gender As String, ByVal population As Long, ByVal populationString As String)
            countryField = country
            genderField = gender
            populationField = population
            populationStringField = populationString
        End Sub
    End Structure

    Public Class AgePopulation

        Private ReadOnly nameField As String

        Private ReadOnly ageField As String

        Private ReadOnly sexField As String

        Private ReadOnly populationField As Double

        Public ReadOnly Property Name As String
            Get
                Return nameField
            End Get
        End Property

        Public ReadOnly Property Age As String
            Get
                Return ageField
            End Get
        End Property

        Public ReadOnly Property Sex As String
            Get
                Return sexField
            End Get
        End Property

        Public ReadOnly Property SexAgeKey As String
            Get
                Return sexField.ToString() & ": " & ageField
            End Get
        End Property

        Public ReadOnly Property CountryAgeKey As String
            Get
                Return nameField & ": " & ageField
            End Get
        End Property

        Public ReadOnly Property CountrySexKey As String
            Get
                Return nameField & ": " & sexField.ToString()
            End Get
        End Property

        Public ReadOnly Property Population As Double
            Get
                Return populationField
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal age As String, ByVal gender As String, ByVal population As Double)
            nameField = name
            ageField = age
            sexField = gender
            populationField = population
        End Sub
    End Class
End Namespace
