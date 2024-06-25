Imports DevExpress.Xpf.DemoBase.Helpers
Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Xml

Namespace GridDemo

    Public Class Person

        Public Property Id As Integer

        Public Property FullName As String

        Public Property Company As String

        Public Property JobTitle As String

        Public Property City As String

        Public Property Address As String

        Public Property Phone As String

        Public Property Email As String
    End Class

    Public Class PersonGenerator

        Private rnd As Random = New Random()

        Private lastNames As List(Of String) = Dump("LastNames.xml")

        Private firstNames As List(Of String) = Dump("FirstNames.xml")

        Private cities As List(Of String) = Dump("Cities.xml")

        Private streets As List(Of String) = Dump("Streets.xml")

        Private Shared employeeJobTitles As String() = {"Accounting Manager", "Assistant Sales Agent", "Assistant Sales Representative", "Coordinator Foreign Markets", "Export Administrator", "International Marketing Manager", "Marketing Assistant", "Marketing Manager", "Marketing Representative", "Order Administrator", "Product Manager", "Purchasing Agent", "Purchasing Manager", "Regional Account Representative", "Sales Agent", "Sales Associate", "Sales Manager", "Sales Representative"}

        Private Shared ownerJobTitles As String() = {"Owner", "Owner/Marketing Assistant"}

        Private Shared companySuffixes As String() = {"General Partnership", "LP", "LLP", "LLP", "LLLP", "LLC", "PLLC", "Corp.", "Inc.", "PC", "Company", "Limited", "Urban Development", "Finance", "Mobile", "Foundation", "Association", "Bank", "Industries", "Motors", "Electric"}

        Private Shared Function Dump(ByVal fileName As String) As List(Of String)
            Dim result = New List(Of String)()
            Dim assembly As Assembly = GetType(PersonGenerator).Assembly
            Dim file = assembly.GetManifestResourceStream(DemoHelper.GetPath(assembly.GetName().Name & ".Data.", assembly) & fileName)
            If file IsNot Nothing Then
                Dim reader = New XmlTextReader(file)
                While reader.Read()
                    If reader.NodeType = XmlNodeType.Text Then
                        Dim nodeValue As String = reader.Value
                        If Not String.IsNullOrEmpty(nodeValue) Then result.Add(reader.Value)
                    End If
                End While
            End If

            Return result
        End Function

        Public Function CreatePerson() As Person
            Dim person = New Person()
            Dim firstName As String = GenerateFirstName()
            Dim lastName As String = GenerateLastName()
            person.FullName = CreateFullName(firstName, lastName)
            Dim companyBaseName As String = GetRandomElement(lastNames)
            Dim companySuffix As String = GetRandomElement(companySuffixes)
            person.Company = CreateCompanyName(companyBaseName, companySuffix)
            person.JobTitle = GenerateJobTitle()
            person.City = GenerateCity()
            person.Address = GenerateAddress()
            person.Phone = GeneratePhone()
            person.Email = CreateEmail(firstName, lastName, companyBaseName)
            Return person
        End Function

        Private Function GenerateFirstName() As String
            Return GetRandomElement(firstNames)
        End Function

        Private Function GenerateLastName() As String
            Return GetRandomElement(lastNames)
        End Function

        Private Function CreateFullName(ByVal firstName As String, ByVal lastName As String) As String
            Return firstName & " " & lastName
        End Function

        Private Function CreateCompanyName(ByVal companyBase As String, ByVal companySuffix As String) As String
            Return companyBase & " " & companySuffix
        End Function

        Private Function GenerateJobTitle() As String
            If rnd.NextDouble() > 0.9 Then Return GetRandomElement(ownerJobTitles)
            Return GetRandomElement(employeeJobTitles)
        End Function

        Private Function GenerateCity() As String
            Return GetRandomElement(cities)
        End Function

        Private Function GenerateAddress() As String
            Return String.Format("{0}, {1}-{2}", GetRandomElement(streets), rnd.Next(1, 99), rnd.Next(10, 999))
        End Function

        Private Function GeneratePhone() As String
            Return String.Format("({0}) {1}-{2}", rnd.Next(100, 999), rnd.Next(100, 999), rnd.Next(1000, 9999))
        End Function

        Private Function CreateEmail(ByVal firstName As String, ByVal lastName As String, ByVal companyBaseName As String) As String
            Return firstName.ToLowerInvariant() & "." & lastName.ToLowerInvariant() & "@" & companyBaseName.ToLowerInvariant() & ".com"
        End Function

        Private Function GetRandomElement(Of T)(ByVal list As IList(Of T)) As T
            Return list(rnd.Next(0, list.Count - 1))
        End Function
    End Class
End Namespace
