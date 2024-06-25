Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.LayoutControl
Imports System.ComponentModel.DataAnnotations

Namespace LayoutControlDemo

    Public Class DataControlPageViewModel

        Private _ObjectList As IEnumerable(Of LayoutControlDemo.DataControlPageViewModel.ObjectNamePair)

        Public Class ObjectNamePair

            Private _Name As String, _Obj As Object

            Public Property Name As String
                Get
                    Return _Name
                End Get

                Private Set(ByVal value As String)
                    _Name = value
                End Set
            End Property

            Public Property Obj As Object
                Get
                    Return _Obj
                End Get

                Private Set(ByVal value As Object)
                    _Obj = value
                End Set
            End Property

            Public Sub New(ByVal name As String, ByVal obj As Object)
                Me.Name = name
                Me.Obj = obj
            End Sub
        End Class

        Public Property ObjectList As IEnumerable(Of ObjectNamePair)
            Get
                Return _ObjectList
            End Get

            Private Set(ByVal value As IEnumerable(Of ObjectNamePair))
                _ObjectList = value
            End Set
        End Property

        Public Overridable Property SelectedObject As ObjectNamePair

        Public Sub New()
            Dim supportedDataTypes = New SupportedDataTypesData With {.IntProperty = 123, .NullableIntProperty = 4567, .DoubleProperty = 12345.12345, .NullableDoubleProperty = 6789.6789, .BoolProperty = True, .CharProperty = "Y"c, .NullableCharProperty = "N"c, .NullableEnumProperty = Gender.Female, .StringProperty = "text", .DateTimeProperty = Date.Today, .NullableDateTimeProperty = Date.Today.AddDays(1), .DecimalProperty = 12345.12345D, .NullableDecimalProperty = 789.789D, .ComplexTypeProperty = New Point(123.45, 678.9), .CurrencyProperty = 1234567.89D, .MultilineTextProperty = "first line of text" & Microsoft.VisualBasic.Constants.vbCrLf & "second line of text" & Microsoft.VisualBasic.Constants.vbCrLf & "third line of text", .PasswordProperty = "password", .PhoneNumberProperty = "1234567890"}
            Dim supportedDataTypes_FluentAPI = New SupportedDataTypesData_FluentAPI With {.IntProperty = 123, .NullableIntProperty = 4567, .DoubleProperty = 12345.12345, .NullableDoubleProperty = 6789.6789, .BoolProperty = True, .CharProperty = "Y"c, .NullableCharProperty = "N"c, .NullableEnumProperty = Gender.Female, .StringProperty = "text", .DateTimeProperty = Date.Today, .NullableDateTimeProperty = Date.Today.AddDays(1), .DecimalProperty = 12345.12345D, .NullableDecimalProperty = 789.789D, .ComplexTypeProperty = New Point(123.45, 678.9), .CurrencyProperty = 1234567.89D, .MultilineTextProperty = "first line of text" & Microsoft.VisualBasic.Constants.vbCrLf & "second line of text" & Microsoft.VisualBasic.Constants.vbCrLf & "third line of text", .PasswordProperty = "password", .PhoneNumberProperty = "1234567890"}
            Dim attributeSupport = New AttributeSupportData With {.ID = 123456, .FirstName = "Steve", .LastName = "Jobs", .Age = 56, .Gender = Gender.Male, .Employer = "Apple Inc", .SSN = "123-45-6789"}
            Dim attributeSupport_FluentAPI = New AttributeSupportData_FluentAPI With {.ID = 123456, .FirstName = "Steve", .LastName = "Jobs", .Age = 56, .Gender = Gender.Male, .Employer = "Apple Inc", .SSN = "123-45-6789"}
            Dim maskedInput = New MaskedInputData With {.PercentProperty = 0.42, .PercentMultBy100Property = 0.42, .CurrencyProperty = 499.90D, .EuroCurrencyProperty = 399.90D, .NumberProperty = 1234567.89, .FixedPointProperty = 9876543.21, .Decimal4DigitsProperty = 17, .CustomNumericMaskPropery1 = 3080.6, .CustomNumericMaskPropery2 = -3080.6, .PhoneProperty = "555-12-34", .ShortZipCodeProperty = "12345", .LongZipCodeProperty = "12345-6789", .SocialSequrityProperty = "123-45-6789"}
            Call AssignDateTimeProperties(maskedInput)
            Dim maskedInput_FluentAPI = New MaskedInputData_FluentAPI With {.PercentProperty = 0.42, .PercentMultBy100Property = 0.42, .CurrencyProperty = 499.90D, .EuroCurrencyProperty = 399.90D, .NumberProperty = 1234567.89, .FixedPointProperty = 9876543.21, .Decimal4DigitsProperty = 17, .CustomNumericMaskPropery1 = 3080.6, .CustomNumericMaskPropery2 = -3080.6, .PhoneProperty = "555-12-34", .ShortZipCodeProperty = "12345", .LongZipCodeProperty = "12345-6789", .SocialSequrityProperty = "123-45-6789"}
            Call AssignDateTimeProperties(maskedInput_FluentAPI)
            Dim groupedLayout = New GroupedLayoutData With {.FirstName = "Anita", .LastName = "Benson", .Group = "Inventory Management", .Title = "Purchasing Manager", .HireDate = New DateTime(2002, 2, 2), .Salary = 65000D, .Phone = "7138638137", .Email = "Anita_Benson@example.com", .AddressLine1 = "9602 South Main", .AddressLine2 = "Seattle, 77025, USA", .Gender = Gender.Female, .BirthDate = New DateTime(1985, 3, 18)}
            Dim groupedLayout_FluentAPI = New GroupedLayoutData_FluentAPI With {.FirstName = "Anita", .LastName = "Benson", .Group = "Inventory Management", .Title = "Purchasing Manager", .HireDate = New DateTime(2002, 2, 2), .Salary = 65000D, .Phone = "7138638137", .Email = "Anita_Benson@example.com", .AddressLine1 = "9602 South Main", .AddressLine2 = "Seattle, 77025, USA", .Gender = Gender.Female, .BirthDate = New DateTime(1985, 3, 18)}
            Dim advancedGroupedLayout = New AdvancedGroupedLayoutData With {.FirstName = "Anita", .LastName = "Benson", .Group = "Inventory Management", .Title = "Purchasing Manager", .HireDate = New DateTime(2002, 2, 2), .Salary = 65000D, .Phone = "7138638137", .Email = "Anita_Benson@example.com", .AddressLine1 = "9602 South Main", .AddressLine2 = "Seattle, 77025, USA", .Gender = Gender.Female, .BirthDate = New DateTime(1985, 3, 18)}
            Dim advancedGroupedLayout_FluentAPI = New AdvancedGroupedLayoutData_FluentAPI With {.FirstName = "Anita", .LastName = "Benson", .Group = "Inventory Management", .Title = "Purchasing Manager", .HireDate = New DateTime(2002, 2, 2), .Salary = 65000D, .Phone = "7138638137", .Email = "Anita_Benson@example.com", .AddressLine1 = "9602 South Main", .AddressLine2 = "Seattle, 77025, USA", .Gender = Gender.Female, .BirthDate = New DateTime(1985, 3, 18)}
            SelectedObject = New ObjectNamePair("Advanced grouped layout (Fluent API)", advancedGroupedLayout_FluentAPI)
            ObjectList = New ObjectNamePair() {New ObjectNamePair("Supported data types", supportedDataTypes), New ObjectNamePair("Supported data types (Fluent API)", supportedDataTypes_FluentAPI), New ObjectNamePair("Attribute support", attributeSupport), New ObjectNamePair("Attribute support (Fluent API)", attributeSupport_FluentAPI), New ObjectNamePair("Masked input", maskedInput), New ObjectNamePair("Masked input (Fluent API)", maskedInput_FluentAPI), New ObjectNamePair("Grouped layout", groupedLayout), New ObjectNamePair("Grouped layout (Fluent API)", groupedLayout_FluentAPI), New ObjectNamePair("Advanced grouped layout", advancedGroupedLayout), SelectedObject}
        End Sub

        Private Shared Sub AssignDateTimeProperties(ByVal obj As Object)
            For Each [property] In obj.GetType().GetProperties().Where(Function(x) x.PropertyType Is GetType(Date))
                [property].SetValue(obj, Date.Now, Nothing)
            Next
        End Sub
    End Class
End Namespace
