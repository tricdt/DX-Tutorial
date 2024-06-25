Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo

    <POCOViewModel>
    Public Class SmartColumnsGenerationViewModel

        Private _Items As List(Of GridDemo.CollectionInfo)

        Const RowCount As Integer = 100

        Private rnd As Random = New Random()

        Protected Sub New()
            Items = New List(Of CollectionInfo)()
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement1), RowCount), "Supported data types"))
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement1_FluentAPI), RowCount), "Supported data types (Fluent API)"))
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement2), RowCount, "Department"), "Attribute support"))
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement2_FluentAPI), RowCount, "Department"), "Attribute support (Fluent API)"))
            Items.Add(New CollectionInfo(GetMaskedInputData(), "Masked input"))
            Items.Add(New CollectionInfo(GetMaskedInputData_FluentAPI(), "Masked input (Fluent API)"))
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement3), RowCount), "Bands Layout"))
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement3_FluentAPI), RowCount), "Bands Layout (Fluent API)"))
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement4), RowCount), "Nested Bands Layout"))
            Items.Add(New CollectionInfo(DataGenerator.GetObjects(GetType(DataAnnotationsElement4_FluentAPI), RowCount), "Nested Bands Layout (Fluent API)"))
            SelectedCollectionInfo = Items(0)
        End Sub

        Private Function GetMaskedInputData() As IEnumerable(Of MaskedInputData)
            Dim result As List(Of MaskedInputData) = New List(Of MaskedInputData)()
            For i As Integer = 0 To RowCount - 1
                Dim maskedInput = New MaskedInputData With {.PercentProperty = rnd.NextDouble(), .CurrencyProperty = rnd.Next(1, 20) * 100 - 0.1D, .NumberProperty = rnd.Next(10000000) / 100R, .FixedPointProperty = rnd.Next(10000000) / 100R, .Decimal4DigitsProperty = rnd.Next(10000), .CustomNumericMaskPropery1 = rnd.Next(100000) / 10R, .CustomNumericMaskPropery2 = rnd.Next(100000) / 10R, .PhoneProperty = rnd.Next(100, 1000).ToString() & "-" & rnd.Next(10, 100).ToString() & "-" & rnd.Next(10, 100).ToString(), .ShortZipCodeProperty = rnd.Next(10000, 100000).ToString(), .LongZipCodeProperty = rnd.Next(10000, 100000).ToString() & "-" & rnd.Next(1000, 10000).ToString(), .SocialSequrityProperty = rnd.Next(100, 1000).ToString() & "-" & rnd.Next(10, 100).ToString() & "-" & rnd.Next(1000, 10000).ToString()}
                AssignDateTimeProperties(maskedInput)
                result.Add(maskedInput)
            Next

            Return result
        End Function

        Private Function GetMaskedInputData_FluentAPI() As IEnumerable(Of MaskedInputData_FluentAPI)
            Dim result As List(Of MaskedInputData_FluentAPI) = New List(Of MaskedInputData_FluentAPI)()
            For i As Integer = 0 To RowCount - 1
                Dim maskedInput = New MaskedInputData_FluentAPI With {.PercentProperty = rnd.NextDouble(), .CurrencyProperty = rnd.Next(1, 20) * 100 - 0.1D, .NumberProperty = rnd.Next(10000000) / 100R, .FixedPointProperty = rnd.Next(10000000) / 100R, .Decimal4DigitsProperty = rnd.Next(10000), .CustomNumericMaskPropery1 = rnd.Next(100000) / 10R, .CustomNumericMaskPropery2 = rnd.Next(100000) / 10R, .PhoneProperty = rnd.Next(100, 1000).ToString() & "-" & rnd.Next(10, 100).ToString() & "-" & rnd.Next(10, 100).ToString(), .ShortZipCodeProperty = rnd.Next(10000, 100000).ToString(), .LongZipCodeProperty = rnd.Next(10000, 100000).ToString() & "-" & rnd.Next(1000, 10000).ToString(), .SocialSequrityProperty = rnd.Next(100, 1000).ToString() & "-" & rnd.Next(10, 100).ToString() & "-" & rnd.Next(1000, 10000).ToString()}
                AssignDateTimeProperties(maskedInput)
                result.Add(maskedInput)
            Next

            Return result
        End Function

        Private Sub AssignDateTimeProperties(ByVal obj As Object)
            For Each [property] In obj.GetType().GetProperties().Where(Function(x) x.PropertyType Is GetType(Date))
                [property].SetValue(obj, Date.Now.AddDays(rnd.Next(-1000, 1000)), Nothing)
            Next
        End Sub

        Public Property Items As List(Of CollectionInfo)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of CollectionInfo))
                _Items = value
            End Set
        End Property

        Public Overridable Property SelectedCollectionInfo As CollectionInfo

        Protected Sub OnSelectedCollectionInfoChanged()
            IsSmallObject = Enumerable.First(SelectedCollectionInfo.Collection.Cast(Of Object)()).GetType().GetProperties().Length <= 10
        End Sub

        Public Overridable Property IsSmallObject As Boolean
    End Class

    Public Enum Gender
        Male
        Female
    End Enum
End Namespace
