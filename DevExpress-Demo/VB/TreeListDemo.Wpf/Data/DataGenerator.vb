Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.Data
Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Utils
Imports DevExpress.DemoData.Models
Imports System.Runtime.InteropServices

Namespace TreeListDemo

    Public Class DataGenerator

        Private Shared AddrAdnCity As System.Tuple(Of String, String)()

        Private Shared Groups As String() = New String() {"Administration", "Inventory", "Manufacturing", "Quality", "Research", "Sales"}

        Private Shared Titles As String()

        Private Shared Employers As String()

        Private Shared ProductData As TreeListDemo.ProductData()

        Private Shared Salary As Decimal() = New Decimal() {5000D, 5500D, 6000D, 6500D, 7000D}

        Private Shared FirstNames_Male As String() = New String() {"Bob", "Michael", "Mike", "Bryan", "Steve", "Alex", "Don", "David", "Jim", "Jo"}

        Private Shared FirstNames_Female As String() = New String() {"Anne", "Sandra", "Samantha"}

        Private Shared FirstNames As String()

        Private Shared LastNames As String() = New String() {"Dodsworth", "Smith", "Miller", "Vargas", "Mares", "Ralls", "Seamans", "Myer", "Moreland", "Walton", "Masters", "Berry", "Hines"}

        Private Shared Passwords As String() = New String() {"password1", "password2", "password3"}

        Private Shared Chars As Char() = New Char() {"a"c, "b"c, "c"c, "d"c, "A"c, "B"c, "C"c, "D"c}

        Private Shared Strings As String() = New String() {"string1", "string2", "string3", "string4"}

        Private Shared rnd As System.Random = New System.Random(System.DateTime.Now.Millisecond)

        Shared Sub New()
            Dim allFirstNames As System.Collections.Generic.List(Of String) = New System.Collections.Generic.List(Of String)(TreeListDemo.DataGenerator.FirstNames_Male)
            allFirstNames.AddRange(TreeListDemo.DataGenerator.FirstNames_Female)
            TreeListDemo.DataGenerator.FirstNames = allFirstNames.ToArray()
            Dim categoryDataList As System.Collections.Generic.List(Of TreeListDemo.CategoryData) = TreeListDemo.DataGenerator.ExtractCategoryDataList()
            TreeListDemo.DataGenerator.ProductData = TreeListDemo.DataGenerator.ExtractProductDataList(CType((categoryDataList), System.Collections.Generic.List(Of TreeListDemo.CategoryData))).ToArray()
            TreeListDemo.DataGenerator.Titles = TreeListDemo.DataGenerator.ExtractTitles().ToArray()
            TreeListDemo.DataGenerator.Employers = TreeListDemo.DataGenerator.ExtractEmployers().ToArray()
            TreeListDemo.DataGenerator.AddrAdnCity = TreeListDemo.DataGenerator.ExtractAddressAndCity().ToArray()
        End Sub

        Public Shared Function GetFirstName(ByVal Optional gndr As TreeListDemo.Gender? = Nothing) As String
            If Not gndr.HasValue Then Return TreeListDemo.DataGenerator.FirstNames(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.FirstNames.Length))
            Dim value As TreeListDemo.Gender = gndr.Value
            Select Case value
                Case TreeListDemo.Gender.Male
                    Return TreeListDemo.DataGenerator.FirstNames_Male(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.FirstNames_Male.Length))
                Case Else
                    Return TreeListDemo.DataGenerator.FirstNames_Female(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.FirstNames_Female.Length))
            End Select
        End Function

        Public Shared Function GetProductData() As ProductData
            Return TreeListDemo.DataGenerator.ProductData(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.ProductData.Length))
        End Function

        Public Shared Function GetLastName() As String
            Return TreeListDemo.DataGenerator.LastNames(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.LastNames.Length))
        End Function

        Public Shared Function GetGroup() As String
            Return TreeListDemo.DataGenerator.Groups(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.Groups.Length))
        End Function

        Public Shared Function GetTitle() As String
            Return TreeListDemo.DataGenerator.Titles(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.Titles.Length))
        End Function

        Public Shared Function GetEmployer() As String
            Return TreeListDemo.DataGenerator.Employers(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.Employers.Length))
        End Function

        Public Shared Function GetPhone() As String
            Dim phone As String = System.[String].Empty
            While phone.Length <= 10
                phone += TreeListDemo.DataGenerator.rnd.[Next](CInt((10))).ToString()
            End While

            Return phone
        End Function

        Public Shared Function GetEmail() As String
            Return TreeListDemo.DataGenerator.GetString() & "@example.com"
        End Function

        Public Shared Function GetAddressAndCity() As Tuple(Of String, String)
            Return TreeListDemo.DataGenerator.AddrAdnCity(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.AddrAdnCity.Length))
        End Function

        Public Shared Function GetNullableInt() As Integer?
            If TreeListDemo.DataGenerator.GetIsNull() Then Return Nothing
            Return TreeListDemo.DataGenerator.GetInt()
        End Function

        Public Shared Function GetInt() As Integer
            Return TreeListDemo.DataGenerator.rnd.[Next](1, 100)
        End Function

        Private Shared id As Integer = 0

        Private Shared Function GetTreeID() As Integer
            Dim currentId As Integer = TreeListDemo.DataGenerator.id + 1
            TreeListDemo.DataGenerator.id += 1
            Return currentId
        End Function

        Public Shared Function GetID() As Tuple(Of Integer, Integer)
            Dim id As Integer = TreeListDemo.DataGenerator.GetTreeID()
            Dim parentID As Integer = id Mod 4
            Return New System.Tuple(Of Integer, Integer)(id, parentID)
        End Function

        Public Shared Function GetNullableDecimal() As Decimal?
            If TreeListDemo.DataGenerator.GetIsNull() Then Return Nothing
            Return TreeListDemo.DataGenerator.GetDecimal()
        End Function

        Public Shared Function GetSalary() As Decimal
            Return TreeListDemo.DataGenerator.Salary(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.Salary.Length))
        End Function

        Public Shared Function GetDecimal() As Decimal
            Return New Decimal(TreeListDemo.DataGenerator.GetDouble())
        End Function

        Public Shared Function GetNullableDouble() As Double?
            If TreeListDemo.DataGenerator.GetIsNull() Then Return Nothing
            Return TreeListDemo.DataGenerator.GetDouble()
        End Function

        Public Shared Function GetDouble() As Double
            Return System.Math.Round(TreeListDemo.DataGenerator.rnd.NextDouble() * 100, 2)
        End Function

        Private Shared Function GetIsNull() As Boolean
            Return TreeListDemo.DataGenerator.rnd.[Next](0, 10) < 3
        End Function

        Private Shared Function GetSSN() As String
            Dim ssn As String = System.[String].Empty
            While ssn.Length <= 11
                If ssn.Length = 3 OrElse ssn.Length = 6 Then
                    ssn += "-"
                Else
                    ssn += TreeListDemo.DataGenerator.rnd.[Next](CInt((10))).ToString()
                End If
            End While

            Return ssn
        End Function

        Public Shared Function GetPoint() As Point
            Return New System.Windows.Point(System.Math.Round(TreeListDemo.DataGenerator.rnd.NextDouble() * 10, 2), System.Math.Round(TreeListDemo.DataGenerator.rnd.NextDouble() * 10, 2))
        End Function

        Public Shared Function GetMultiLineString() As String
            Return "Line 1." & Global.Microsoft.VisualBasic.Constants.vbLf & "Line 2."
        End Function

        Public Shared Function GetAge() As Integer
            Return TreeListDemo.DataGenerator.rnd.[Next](30, 50)
        End Function

        Public Shared Function GetString() As String
            Return TreeListDemo.DataGenerator.Strings(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.Strings.Length))
        End Function

        Public Shared Function GetPassword() As String
            Return TreeListDemo.DataGenerator.Passwords(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.Passwords.Length))
        End Function

        Public Shared Function GetNullableBool() As Boolean?
            If TreeListDemo.DataGenerator.GetIsNull() Then Return Nothing
            Return TreeListDemo.DataGenerator.GetBool()
        End Function

        Public Shared Function GetBool() As Boolean
            Return TreeListDemo.DataGenerator.rnd.[Next](2) = 1
        End Function

        Public Shared Function GetNullableChar() As Char?
            If TreeListDemo.DataGenerator.GetIsNull() Then Return Nothing
            Return TreeListDemo.DataGenerator.GetChar()
        End Function

        Public Shared Function GetChar() As Char
            Return TreeListDemo.DataGenerator.Chars(TreeListDemo.DataGenerator.rnd.[Next](0, TreeListDemo.DataGenerator.Chars.Length))
        End Function

        Public Shared Function GetNullableEnumValue(ByVal enumType As System.Type) As Object
            If TreeListDemo.DataGenerator.GetIsNull() Then Return Nothing
            Return TreeListDemo.DataGenerator.GetEnumValue(enumType)
        End Function

        Public Shared Function GetEnumValue(ByVal enumType As System.Type) As Object
            Dim values As System.Array = DevExpress.Utils.EnumExtensions.GetValues(enumType)
            Return values.GetValue(TreeListDemo.DataGenerator.rnd.[Next](0, values.Length))
        End Function

        Public Shared Function GetGender() As Gender
            Return CType(TreeListDemo.DataGenerator.GetEnumValue(GetType(TreeListDemo.Gender)), TreeListDemo.Gender)
        End Function

        Public Shared Function GetNullableDateTime() As System.DateTime?
            If TreeListDemo.DataGenerator.GetIsNull() Then Return Nothing
            Return TreeListDemo.DataGenerator.GetDateTime()
        End Function

        Public Shared Function GetDateTime() As DateTime
            Return System.DateTime.Now - System.TimeSpan.FromDays(TreeListDemo.DataGenerator.rnd.[Next](60))
        End Function

        Public Shared Function GetHireDate() As DateTime
            Return System.DateTime.Now - System.TimeSpan.FromDays(TreeListDemo.DataGenerator.rnd.[Next](60, 365 * 5))
        End Function

        Public Shared Function GetBirthDate() As DateTime
            Return System.DateTime.Now - System.TimeSpan.FromDays(TreeListDemo.DataGenerator.GetAge() * TreeListDemo.DataGenerator.rnd.[Next](300, 400))
        End Function

        Public Shared Function GetObjects(ByVal objectType As System.Type, ByVal count As Integer) As List(Of Object)
            TreeListDemo.DataGenerator.id = 0
            Dim result As System.Collections.Generic.List(Of Object) = New System.Collections.Generic.List(Of Object)()
            While result.Count <= count
                result.Add(TreeListDemo.DataGenerator.GetNewObject(objectType))
            End While

            Return result
        End Function

        Public Shared Function GetNewObject(ByVal objectType As System.Type) As Object
            Dim newObject As Object = System.Activator.CreateInstance(objectType)
            Dim typeInfo As System.Reflection.PropertyInfo() = objectType.GetProperties(System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.[Public])
            Dim productData As TreeListDemo.ProductData = TreeListDemo.DataGenerator.GetProductData()
            Dim currentGender As TreeListDemo.Gender = TreeListDemo.DataGenerator.GetGender()
            Dim addrAdnCity As System.Tuple(Of String, String) = TreeListDemo.DataGenerator.GetAddressAndCity()
            Dim id As System.Tuple(Of Integer, Integer) = TreeListDemo.DataGenerator.GetID()
            For Each info As System.Reflection.PropertyInfo In typeInfo
                If Not info.CanWrite Then Continue For
                If info.PropertyType Is GetType(TreeListDemo.Gender) Then
                    info.SetValue(newObject, currentGender, Nothing)
                    Continue For
                End If

                Dim dType As System.ComponentModel.DataAnnotations.DataTypeAttribute = TreeListDemo.DataGenerator.GetDataTypeAttribute(info)
                If dType IsNot Nothing Then
                    Select Case dType.DataType
                        Case System.ComponentModel.DataAnnotations.DataType.MultilineText
                            info.SetValue(newObject, TreeListDemo.DataGenerator.GetMultiLineString(), Nothing)
                            Continue For
                        Case System.ComponentModel.DataAnnotations.DataType.Password
                            info.SetValue(newObject, TreeListDemo.DataGenerator.GetPassword(), Nothing)
                            Continue For
                        Case System.ComponentModel.DataAnnotations.DataType.PhoneNumber
                            info.SetValue(newObject, TreeListDemo.DataGenerator.GetPhone(), Nothing)
                            Continue For
                        Case System.ComponentModel.DataAnnotations.DataType.Currency
                            If Equals(info.Name, "Salary") Then
                                info.SetValue(newObject, TreeListDemo.DataGenerator.GetSalary(), Nothing)
                                Continue For
                            End If

                            info.SetValue(newObject, TreeListDemo.DataGenerator.GetDecimal(), Nothing)
                            Continue For
                    End Select
                End If

                If info.PropertyType.IsEnum Then
                    info.SetValue(newObject, TreeListDemo.DataGenerator.GetEnumValue(info.PropertyType), Nothing)
                    Continue For
                End If

                If TreeListDemo.DataGenerator.IsNullableType(info.PropertyType) AndAlso TreeListDemo.DataGenerator.GetNullableType(CType((info.PropertyType), System.Type)).IsEnum Then
                    info.SetValue(newObject, TreeListDemo.DataGenerator.GetNullableEnumValue(TreeListDemo.DataGenerator.GetNullableType(info.PropertyType)), Nothing)
                    Continue For
                End If

                Dim nullable As Boolean = False
                If TreeListDemo.DataGenerator.IsPropertyOfType(info, GetType(Integer), nullable) Then
                    If Equals(info.Name, "Age") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetAge(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "ID") Then
                        info.SetValue(newObject, id.Item1, Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "ParentID") Then
                        info.SetValue(newObject, id.Item2, Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, If(nullable, TreeListDemo.DataGenerator.GetNullableInt(), TreeListDemo.DataGenerator.GetInt()), Nothing)
                    Continue For
                End If

                If info.PropertyType Is GetType(String) Then
                    If Equals(info.Name, "FirstName") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetFirstName(currentGender), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "LastName") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetLastName(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "SSN") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetSSN(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Phone") OrElse Equals(info.Name, "PhoneNumberProperty") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetPhone(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Email") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetEmail(), Nothing)
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
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetFirstName() & " " & TreeListDemo.DataGenerator.GetLastName(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Group") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetGroup(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Title") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetTitle(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Employer") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetEmployer(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "PasswordProperty") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetPassword(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "MultilineTextProperty") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetMultiLineString(), Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, TreeListDemo.DataGenerator.GetString(), Nothing)
                    Continue For
                End If

                If TreeListDemo.DataGenerator.IsPropertyOfType(info, GetType(System.DateTime), nullable) Then
                    If Equals(info.Name, "BirthDate") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetBirthDate(), Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "HireDate") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetHireDate(), Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, If(nullable, TreeListDemo.DataGenerator.GetNullableDateTime(), TreeListDemo.DataGenerator.GetDateTime()), Nothing)
                    Continue For
                End If

                If TreeListDemo.DataGenerator.IsPropertyOfType(info, GetType(Boolean), nullable) Then
                    info.SetValue(newObject, If(nullable, TreeListDemo.DataGenerator.GetNullableBool(), TreeListDemo.DataGenerator.GetBool()), Nothing)
                    Continue For
                End If

                If TreeListDemo.DataGenerator.IsPropertyOfType(info, GetType(Double), nullable) Then
                    info.SetValue(newObject, If(nullable, TreeListDemo.DataGenerator.GetNullableDouble(), TreeListDemo.DataGenerator.GetDouble()), Nothing)
                    Continue For
                End If

                If TreeListDemo.DataGenerator.IsPropertyOfType(info, GetType(Decimal), nullable) Then
                    If Equals(info.Name, "Price") Then
                        info.SetValue(newObject, productData.Price, Nothing)
                        Continue For
                    End If

                    If Equals(info.Name, "Salary") Then
                        info.SetValue(newObject, TreeListDemo.DataGenerator.GetSalary(), Nothing)
                        Continue For
                    End If

                    info.SetValue(newObject, If(nullable, TreeListDemo.DataGenerator.GetNullableDecimal(), TreeListDemo.DataGenerator.GetDecimal()), Nothing)
                    Continue For
                End If

                If TreeListDemo.DataGenerator.IsPropertyOfType(info, GetType(Char), nullable) Then
                    info.SetValue(newObject, If(nullable, TreeListDemo.DataGenerator.GetNullableChar(), TreeListDemo.DataGenerator.GetChar()), Nothing)
                    Continue For
                End If

                If info.PropertyType Is GetType(System.Windows.Point) Then
                    info.SetValue(newObject, TreeListDemo.DataGenerator.GetPoint(), Nothing)
                    Continue For
                End If

                If info.PropertyType Is GetType(TreeListDemo.CategoryData) Then
                    info.SetValue(newObject, productData.Category, Nothing)
                    Continue For
                End If
            Next

            Return newObject
        End Function

        Private Shared Function IsPropertyOfType(ByVal info As System.Reflection.PropertyInfo, ByVal targetType As System.Type, <Out> ByRef nullable As Boolean) As Boolean
            nullable = TreeListDemo.DataGenerator.IsNullableType(info.PropertyType)
            Dim type = If(nullable, TreeListDemo.DataGenerator.GetNullableType(info.PropertyType), info.PropertyType)
            Return Object.Equals(type, targetType)
        End Function

        Private Shared Function IsNullableType(ByVal propertyType As System.Type) As Boolean
            Return propertyType.IsGenericType AndAlso propertyType.GetGenericTypeDefinition().Name.Contains("Nullable")
        End Function

        Private Shared Function GetNullableType(ByVal propertyType As System.Type) As Type
            Return propertyType.GetGenericArguments()(0)
        End Function

        Private Shared Function GetDataTypeAttribute(ByVal info As System.Reflection.PropertyInfo) As DataTypeAttribute
            For Each attr As Object In info.GetCustomAttributes(False)
                Dim result As System.ComponentModel.DataAnnotations.DataTypeAttribute = TryCast(attr, System.ComponentModel.DataAnnotations.DataTypeAttribute)
                If result IsNot Nothing Then Return result
            Next

            Return Nothing
        End Function

        Public Shared Function ExtractTitles() As List(Of String)
            Dim titles As System.Collections.Generic.List(Of String) = New System.Collections.Generic.List(Of String)()
            Dim employees = DevExpress.DemoData.NWindDataProvider.Employees
            For Each employee As DevExpress.DemoData.Models.Employee In employees
                Dim title As String = employee.Title
                If Not titles.Contains(title) Then titles.Add(title)
            Next

            Return titles
        End Function

        Public Shared Function ExtractEmployers() As List(Of String)
            Dim employers As System.Collections.Generic.List(Of String) = New System.Collections.Generic.List(Of String)()
            Dim customers = DevExpress.DemoData.Models.NWindContext.Create().Customers.ToList()
            For Each customer As DevExpress.DemoData.Models.Customer In customers
                Dim employer As String = customer.CompanyName
                If Not employers.Contains(employer) Then employers.Add(employer)
            Next

            Return employers
        End Function

        Public Shared Function ExtractAddressAndCity() As List(Of System.Tuple(Of String, String))
            Dim addrList As System.Collections.Generic.List(Of System.Tuple(Of String, String)) = New System.Collections.Generic.List(Of System.Tuple(Of String, String))()
            Dim customers = DevExpress.DemoData.Models.NWindContext.Create().Customers.ToList()
            For Each customer As DevExpress.DemoData.Models.Customer In customers
                Dim addr = System.Tuple.Create(customer.Address, customer.City)
                If Not addrList.Contains(addr) Then addrList.Add(addr)
            Next

            Return addrList
        End Function

        Public Shared Function ExtractCategoryDataList() As List(Of TreeListDemo.CategoryData)
            Dim categoryData As System.Collections.Generic.List(Of TreeListDemo.CategoryData) = New System.Collections.Generic.List(Of TreeListDemo.CategoryData)()
            Dim categories = DevExpress.DemoData.Models.NWindContext.Create().Categories.ToList()
            For Each category As DevExpress.DemoData.Models.Category In categories
                categoryData.Add(New TreeListDemo.CategoryData() With {.Name = category.CategoryName, .Picture = category.Picture})
            Next

            Return categoryData
        End Function

        Public Shared Function ExtractProductDataList(ByVal categoriesList As System.Collections.Generic.List(Of TreeListDemo.CategoryData)) As List(Of TreeListDemo.ProductData)
            Dim productData As System.Collections.Generic.List(Of TreeListDemo.ProductData) = New System.Collections.Generic.List(Of TreeListDemo.ProductData)()
            Dim categoryProducts = DevExpress.DemoData.Models.NWindContext.Create().CategoryProducts.ToList()
            Dim rand As System.Random = New System.Random()
            For Each cp As DevExpress.DemoData.Models.CategoryProduct In categoryProducts
                productData.Add(New TreeListDemo.ProductData() With {.Category = TreeListDemo.DataGenerator.FindCategory(categoriesList, cp.CategoryName), .Name = cp.ProductName, .Price = CDec(rand.[Next](20)) + CDec(rand.[Next](99)) / 100D})
            Next

            Return productData
        End Function

        Private Shared Function FindCategory(ByVal categoriesList As System.Collections.Generic.List(Of TreeListDemo.CategoryData), ByVal name As String) As CategoryData
            For Each category As TreeListDemo.CategoryData In categoriesList
                If Equals(category.Name, name) Then Return category
            Next

            Return Nothing
        End Function
    End Class

    Public Class ProductData

        Public Property Name As String

        Public Property Category As CategoryData

        Public Property Price As Decimal

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Public Class CategoryData
        Implements System.IComparable, System.IComparable(Of TreeListDemo.CategoryData)

        Public Property Name As String

        Public Property Picture As Byte()

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function

#Region "IComparable Members"
        Public Function CompareTo(ByVal obj As Object) As Integer Implements Global.System.IComparable.CompareTo
            If TypeOf obj Is TreeListDemo.CategoryData Then Return Me.CompareTo(CType(obj, TreeListDemo.CategoryData))
            Return -1
        End Function

#End Region
#Region "IComparable<CategoryData> Members"
        Public Function CompareTo(ByVal other As TreeListDemo.CategoryData) As Integer Implements Global.System.IComparable(Of Global.TreeListDemo.CategoryData).CompareTo
            Return System.StringComparer.CurrentCulture.Compare(Me.Name, other.Name)
        End Function
#End Region
    End Class
End Namespace
