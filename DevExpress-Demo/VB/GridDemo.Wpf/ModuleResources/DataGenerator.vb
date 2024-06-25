Imports DevExpress.DemoData.Models
Imports DevExpress.Utils
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Data
Imports System.Linq
Imports System.Reflection
Imports System.Windows
Imports System.Runtime.InteropServices

Namespace GridDemo

    Public Class DataGenerator

        Private Shared AddrAdnCity As Tuple(Of String, String)()

        Private Shared Groups As String() = New String() {"Administration", "Inventory", "Manufacturing", "Quality", "Research", "Sales"}

        Private Shared Titles As String()

        Private Shared Employers As String()

        Private Shared ProductData As ProductData()

        Private Shared Salary As Decimal() = New Decimal() {5000D, 5500D, 6000D, 6500D, 7000D}

        Private Shared FirstNames_Male As String() = New String() {"Bob", "Michael", "Mike", "Bryan", "Steve", "Alex", "Don", "David", "Jim", "Jo"}

        Private Shared FirstNames_Female As String() = New String() {"Anne", "Sandra", "Samantha"}

        Private Shared FirstNames As String()

        Private Shared LastNames As String() = New String() {"Dodsworth", "Smith", "Miller", "Vargas", "Mares", "Ralls", "Seamans", "Myer", "Moreland", "Walton", "Masters", "Berry", "Hines"}

        Private Shared Passwords As String() = New String() {"password1", "password2", "password3"}

        Private Shared Chars As Char() = New Char() {"a"c, "b"c, "c"c, "d"c, "A"c, "B"c, "C"c, "D"c}

        Private Shared Strings As String() = New String() {"string1", "string2", "string3", "string4"}

        Private Shared ReadOnly CreditCardNumbers As String() = {"4485 5203 4487 8460", "4929 8448 6659 9351", "4539 1920 8815 9808", "4916 5573 0352 8925", "4349 1774 2510 4328", "5126 0553 5816 2164", "5402 9639 5883 2075", "5522 0549 0780 3824", "5327 0535 5504 5628", "5467 8397 9624 7043", "4202 8362 8974 5932", "4532 7478 9667 0846", "4556 5699 5656 1108", "4024 0071 7281 2579", "4929 5135 1421 5294", "5248 7638 5153 8318", "5408 5017 0496 8761", "5242 5923 9945 0348", "5560 8327 0369 4684", "5319 2540 0145 7629"}

        Private Shared ReadOnly CreditCardNumberRandom As Random = New Random()

        Private Shared rnd As Random = New Random(Date.Now.Millisecond)

        Shared Sub New()
            Dim allFirstNames As List(Of String) = New List(Of String)(FirstNames_Male)
            allFirstNames.AddRange(FirstNames_Female)
            FirstNames = allFirstNames.ToArray()
            Dim categoryDataList As List(Of CategoryData) = OrderDataGenerator.CategoryDataList
            ProductData = ExtractProductDataList(categoryDataList).ToArray()
            Titles = ExtractTitles().ToArray()
            Employers = ExtractEmployers().ToArray()
            AddrAdnCity = ExtractAddressAndCity().ToArray()
        End Sub

        Public Shared Function GetFirstName(ByVal Optional gndr As Gender? = Nothing) As String
            If Not gndr.HasValue Then Return FirstNames(rnd.Next(0, FirstNames.Length))
            Dim value As Gender = gndr.Value
            Select Case value
                Case Gender.Male
                    Return FirstNames_Male(rnd.Next(0, FirstNames_Male.Length))
                Case Else
                    Return FirstNames_Female(rnd.Next(0, FirstNames_Female.Length))
            End Select
        End Function

        Public Shared Function GetProductData() As ProductData
            Return ProductData(rnd.Next(0, ProductData.Length))
        End Function

        Public Shared Function GetLastName() As String
            Return LastNames(rnd.Next(0, LastNames.Length))
        End Function

        Public Shared Function GetGroup() As String
            Return Groups(rnd.Next(0, Groups.Length))
        End Function

        Public Shared Function GetTitle() As String
            Return Titles(rnd.Next(0, Titles.Length))
        End Function

        Public Shared Function GetEmployer() As String
            Return Employers(rnd.Next(0, Employers.Length))
        End Function

        Public Shared Function GetPhone() As String
            Dim phone As String = String.Empty
            While phone.Length <= 10
                phone += rnd.Next(10).ToString()
            End While

            Return phone
        End Function

        Public Shared Function GetEmail() As String
            Return GetString() & "@example.com"
        End Function

        Public Shared Function GetAddressAndCity() As Tuple(Of String, String)
            Return AddrAdnCity(rnd.Next(0, AddrAdnCity.Length))
        End Function

        Public Shared Function GetNullableInt() As Integer?
            If GetIsNull() Then Return Nothing
            Return GetInt()
        End Function

        Public Shared Function GetInt() As Integer
            Return rnd.Next(1, 100)
        End Function

        Public Shared Function GetNullableDecimal() As Decimal?
            If GetIsNull() Then Return Nothing
            Return GetDecimal()
        End Function

        Public Shared Function GetSalary() As Decimal
            Return Salary(rnd.Next(0, Salary.Length))
        End Function

        Public Shared Function GetCreditCardNumber() As String
            Return CreditCardNumbers(CreditCardNumberRandom.Next(CreditCardNumbers.Length))
        End Function

        Public Shared Function GetFacebookAddress(ByVal firstName As String, ByVal lastName As String) As String
            Return String.Format("https://www.facebook.com/{0}{1}", firstName.ToLower(), lastName.ToLower())
        End Function

        Public Shared Function GetDecimal() As Decimal
            Return New Decimal(GetDouble())
        End Function

        Public Shared Function GetNullableDouble() As Double?
            If GetIsNull() Then Return Nothing
            Return GetDouble()
        End Function

        Public Shared Function GetDouble() As Double
            Return Math.Round(rnd.NextDouble() * 100, 2)
        End Function

        Private Shared Function GetIsNull() As Boolean
            Return rnd.Next(0, 10) < 3
        End Function

        Private Shared Function GetSSN() As String
            Dim ssn As String = String.Empty
            While ssn.Length <= 11
                If ssn.Length = 3 OrElse ssn.Length = 6 Then
                    ssn += "-"
                Else
                    ssn += rnd.Next(10).ToString()
                End If
            End While

            Return ssn
        End Function

        Public Shared Function GetPoint() As Point
            Return New Point(Math.Round(rnd.NextDouble() * 10, 2), Math.Round(rnd.NextDouble() * 10, 2))
        End Function

        Public Shared Function GetMultiLineString() As String
            Return "Line 1." & Microsoft.VisualBasic.Constants.vbLf & "Line 2."
        End Function

        Public Shared Function GetAge() As Integer
            Return rnd.Next(30, 50)
        End Function

        Public Shared Function GetString() As String
            Return Strings(rnd.Next(0, Strings.Length))
        End Function

        Public Shared Function GetPassword() As String
            Return Passwords(rnd.Next(0, Passwords.Length))
        End Function

        Public Shared Function GetNullableBool() As Boolean?
            If GetIsNull() Then Return Nothing
            Return GetBool()
        End Function

        Public Shared Function GetBool() As Boolean
            Return rnd.Next(2) = 1
        End Function

        Public Shared Function GetNullableChar() As Char?
            If GetIsNull() Then Return Nothing
            Return GetChar()
        End Function

        Public Shared Function GetChar() As Char
            Return Chars(rnd.Next(0, Chars.Length))
        End Function

        Public Shared Function GetNullableEnumValue(ByVal enumType As Type) As Object
            If GetIsNull() Then Return Nothing
            Return GetEnumValue(enumType)
        End Function

        Public Shared Function GetEnumValue(ByVal enumType As Type) As Object
            Dim values As Array = enumType.GetValues
            Return values.GetValue(rnd.Next(0, values.Length))
        End Function

        Public Shared Function GetGender() As Gender
            Return CType(GetEnumValue(GetType(Gender)), Gender)
        End Function

        Public Shared Function GetNullableDateTime() As Date?
            If GetIsNull() Then Return Nothing
            Return GetDateTime()
        End Function

        Public Shared Function GetDateTime() As Date
            Return Date.Now - TimeSpan.FromDays(rnd.Next(60))
        End Function

        Public Shared Function GetHireDate() As Date
            Return Date.Now - TimeSpan.FromDays(rnd.Next(60, 365 * 5))
        End Function

        Public Shared Function GetBirthDate() As Date
            Return Date.Now - TimeSpan.FromDays(GetAge() * rnd.Next(300, 400))
        End Function

        Public Shared Function GetObjects(ByVal objectType As Type, ByVal count As Integer, ParamArray skipProperties As String()) As List(Of Object)
            Dim result As List(Of Object) = New List(Of Object)()
            While result.Count <= count
                result.Add(GetNewObject(objectType, skipProperties))
            End While

            Return result
        End Function

        Public Shared Function GetNewObject(ByVal objectType As Type, ParamArray skipProperties As String()) As Object
            Dim newObject As Object = Activator.CreateInstance(objectType)
            Dim typeInfo As PropertyInfo() = objectType.GetProperties(BindingFlags.Instance Or BindingFlags.Public).Where(Function(x) Not skipProperties.Contains(x.Name)).ToArray()
            Dim productData As ProductData = GetProductData()
            Dim currentGender As Gender = GetGender()
            Dim addrAdnCity As Tuple(Of String, String) = GetAddressAndCity()
            For Each info As PropertyInfo In typeInfo
                If Not info.CanWrite Then Continue For
                If info.PropertyType Is GetType(Gender) Then
                    info.SetValue(newObject, currentGender, Nothing)
                    Continue For
                End If

                Dim dType As DataTypeAttribute = GetDataTypeAttribute(info)
                If dType IsNot Nothing Then
                    Select Case dType.DataType
                        Case DataType.MultilineText
                            info.SetValue(newObject, GetMultiLineString(), Nothing)
                            Continue For
                        Case DataType.Password
                            info.SetValue(newObject, GetPassword(), Nothing)
                            Continue For
                        Case DataType.PhoneNumber
                            info.SetValue(newObject, GetPhone(), Nothing)
                            Continue For
                        Case DataType.Currency
                            If Equals(info.Name, "Salary") Then
                                info.SetValue(newObject, GetSalary(), Nothing)
                                Continue For
                            End If

                            info.SetValue(newObject, GetDecimal(), Nothing)
                            Continue For
                    End Select
                End If

                If info.PropertyType.IsEnum Then
                    info.SetValue(newObject, GetEnumValue(info.PropertyType), Nothing)
                    Continue For
                End If

                If IsNullableType(info.PropertyType) AndAlso GetNullableType(info.PropertyType).IsEnum Then
                    info.SetValue(newObject, GetNullableEnumValue(GetNullableType(info.PropertyType)), Nothing)
                    Continue For
                End If

                Dim nullable As Boolean = False
                If IsPropertyOfType(info, GetType(Integer), nullable) Then
                    If Equals(info.Name, "Age") Then
                        info.SetValue(newObject, GetAge(), Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, If(nullable, GetNullableInt(), GetInt()), Nothing)
                    Continue For
                End If

                If info.PropertyType Is GetType(String) Then
                    If Equals(info.Name, "FirstName") Then
                        info.SetValue(newObject, GetFirstName(currentGender), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "LastName") Then
                        info.SetValue(newObject, GetLastName(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "SSN") Then
                        info.SetValue(newObject, GetSSN(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Phone") OrElse Equals(info.Name, "PhoneNumberProperty") Then
                        info.SetValue(newObject, GetPhone(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Email") Then
                        info.SetValue(newObject, GetEmail(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Address") Then
                        info.SetValue(newObject, addrAdnCity.Item1, Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "City") Then
                        info.SetValue(newObject, addrAdnCity.Item2, Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "ProductName") Then
                        info.SetValue(newObject, productData.Name, Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "CustomerName") Then
                        info.SetValue(newObject, GetFirstName() & " " & GetLastName(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Group") Then
                        info.SetValue(newObject, GetGroup(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Title") Then
                        info.SetValue(newObject, GetTitle(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Employer") Then
                        info.SetValue(newObject, GetEmployer(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "PasswordProperty") Then
                        info.SetValue(newObject, GetPassword(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "MultilineTextProperty") Then
                        info.SetValue(newObject, GetMultiLineString(), Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, GetString(), Nothing)
                    Continue For
                End If

                If IsPropertyOfType(info, GetType(Date), nullable) Then
                    If Equals(info.Name, "BirthDate") Then
                        info.SetValue(newObject, GetBirthDate(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "HireDate") Then
                        info.SetValue(newObject, GetHireDate(), Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, If(nullable, GetNullableDateTime(), GetDateTime()), Nothing)
                    Continue For
                End If

                If IsPropertyOfType(info, GetType(Boolean), nullable) Then
                    info.SetValue(newObject, If(nullable, GetNullableBool(), GetBool()), Nothing)
                    Continue For
                End If

                If IsPropertyOfType(info, GetType(Double), nullable) Then
                    info.SetValue(newObject, If(nullable, GetNullableDouble(), GetDouble()), Nothing)
                    Continue For
                End If

                If IsPropertyOfType(info, GetType(Decimal), nullable) Then
                    If Equals(info.Name, "Price") Then
                        info.SetValue(newObject, productData.Price, Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Salary") Then
                        info.SetValue(newObject, GetSalary(), Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, If(nullable, GetNullableDecimal(), GetDecimal()), Nothing)
                    Continue For
                End If

                If IsPropertyOfType(info, GetType(Char), nullable) Then
                    info.SetValue(newObject, If(nullable, GetNullableChar(), GetChar()), Nothing)
                    Continue For
                End If

                If info.PropertyType Is GetType(Point) Then
                    info.SetValue(newObject, GetPoint(), Nothing)
                    Continue For
                End If

                If info.PropertyType Is GetType(CategoryData) Then
                    info.SetValue(newObject, productData.Category, Nothing)
                    Continue For
                End If
            Next

            Return newObject
        End Function

        Private Shared Function IsPropertyOfType(ByVal info As PropertyInfo, ByVal targetType As Type, <Out> ByRef nullable As Boolean) As Boolean
            nullable = IsNullableType(info.PropertyType)
            Dim type = If(nullable, GetNullableType(info.PropertyType), info.PropertyType)
            Return Equals(type, targetType)
        End Function

        Private Shared Function IsNullableType(ByVal propertyType As Type) As Boolean
            Return propertyType.IsGenericType AndAlso propertyType.GetGenericTypeDefinition().Name.Contains("Nullable")
        End Function

        Private Shared Function GetNullableType(ByVal propertyType As Type) As Type
            Return propertyType.GetGenericArguments()(0)
        End Function

        Private Shared Function GetDataTypeAttribute(ByVal info As PropertyInfo) As DataTypeAttribute
            For Each attr As Object In info.GetCustomAttributes(False)
                Dim result As DataTypeAttribute = TryCast(attr, DataTypeAttribute)
                If result IsNot Nothing Then Return result
            Next

            Return Nothing
        End Function

        Public Shared Function ExtractTitles() As List(Of String)
            Return NWindContext.Create().Employees.[Select](Function(e) e.Title).Distinct().ToList()
        End Function

        Public Shared Function ExtractEmployers() As List(Of String)
            Return NWindContext.Create().Customers.[Select](Function(c) c.CompanyName).Distinct().ToList()
        End Function

        Public Shared Function ExtractAddressAndCity() As List(Of Tuple(Of String, String))
            Return NWindContext.Create().Customers.[Select](Function(c) New With {c.Address, c.City}).Distinct().ToList().[Select](Function(p) Tuple.Create(p.Address, p.City)).ToList()
        End Function
    End Class
End Namespace
