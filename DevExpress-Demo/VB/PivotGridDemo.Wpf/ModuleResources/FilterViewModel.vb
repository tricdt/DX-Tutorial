Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports DevExpress.Data.Filtering
Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.DataAnnotations

Namespace PivotGridDemo

    Public Class FilterViewModel

        Private inFilterSync As Boolean

        Public Sub New()
            Dim dates = SalesPersons.[Select](Function(x) x.OrderDate.Value.Date).OrderBy(Function(x) x)
            SelectionStart = dates.FirstOrDefault()
            StartDate = SelectionStart
            SelectionEnd = dates.LastOrDefault()
            EndDate = SelectionEnd
        End Sub

        Public Overridable Property EndDate As Date

        <BindableProperty(True, OnPropertyChangedMethodName:="UpdateSelection")>
        Public Overridable Property FilterCriteria As CriteriaOperator

        Public ReadOnly Property SalesPersons As IList(Of SalesPerson)
            Get
                Return NWindDataProvider.SalesPersons
            End Get
        End Property

        <BindableProperty(True, OnPropertyChangedMethodName:="UpdateFilter")>
        Public Overridable Property SelectionEnd As Date

        <BindableProperty(True, OnPropertyChangedMethodName:="UpdateFilter")>
        Public Overridable Property SelectionStart As Date

        Public Overridable Property StartDate As Date

        Protected Sub UpdateFilter()
            If inFilterSync Then Return
            Dim str1 As String = GetCriteria(SelectionStart, True)
            Dim str2 As String = GetCriteria(SelectionEnd, False)
            inFilterSync = True
            If Not String.IsNullOrEmpty(str1) AndAlso Not String.IsNullOrEmpty(str2) Then
                FilterCriteria = CriteriaOperator.Parse(str1 & " And " & str2)
            Else
                FilterCriteria = CriteriaOperator.Parse(str1 & str2)
            End If

            inFilterSync = False
        End Sub

        Protected Sub UpdateSelection()
            If inFilterSync Then Return
            inFilterSync = True
            SelectionStart = GetDateTimeValue(GetBinaryOperatorAgrument(FilterCriteria, BinaryOperatorType.GreaterOrEqual), StartDate)
            SelectionEnd = GetDateTimeValue(GetBinaryOperatorAgrument(FilterCriteria, BinaryOperatorType.LessOrEqual), EndDate)
            inFilterSync = False
        End Sub

        Private Function GetBinaryOperatorAgrument(ByVal criteria As CriteriaOperator, ByVal type As BinaryOperatorType) As OperandValue
            Dim group As GroupOperator = TryCast(criteria, GroupOperator)
            If ReferenceEquals(Nothing, group) Then Return Nothing
            Dim op As BinaryOperator = group.Operands.OfType(Of BinaryOperator)().Where(Function(bin) CheckBinaryOperator(bin, type)).FirstOrDefault()
            Return If(ReferenceEquals(Nothing, op), Nothing, TryCast(op.RightOperand, OperandValue))
        End Function

        Private Function CheckBinaryOperator(ByVal binaryOperator As BinaryOperator, ByVal type As BinaryOperatorType) As Boolean
            Return Equals(binaryOperator.OperatorType, type) AndAlso IsOperandProperty(binaryOperator.LeftOperand, "fieldOrderDate")
        End Function

        Private Function GetDateTimeValue(ByVal criteria As OperandValue, ByVal defaultValue As Date) As Date
            Dim dateTime As Date? = If(ReferenceEquals(Nothing, criteria), Nothing, CType(criteria.Value, Date?))
            Return If(dateTime.HasValue, dateTime.Value, defaultValue)
        End Function

        Private Function IsOperandProperty(ByVal criteria As CriteriaOperator, ByVal fieldName As String) As Boolean
            Dim [property] As OperandProperty = TryCast(criteria, OperandProperty)
            Return Not ReferenceEquals(Nothing, [property]) AndAlso Equals([property].PropertyName, fieldName)
        End Function

        Private Function GetCriteria(ByVal [date] As Date, ByVal isGreater As Boolean) As String
            Return String.Format("{0} {1} #{2}#", "fieldOrderDate", If(isGreater, ">=", "<"), Convert.ToString([date], CultureInfo.InvariantCulture))
        End Function
    End Class
End Namespace
