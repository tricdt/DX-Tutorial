Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Controls
Imports DevExpress.Docs.Text
Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Functions

Namespace SpreadsheetDemo

    Public Partial Class CustomFunction
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            RegisterCustomFunction()
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("NumberInWords_template.xlsx"))
            AddHandler spreadsheetControl1.CellValueChanged, AddressOf spreadsheetControl1_CellValueChanged
        End Sub

        Private Sub RegisterCustomFunction()
            Dim customFunction As NumberInWordsFunction = New NumberInWordsFunction()
            If Not spreadsheetControl1.Document.GlobalCustomFunctions.Contains(CType(customFunction, ICustomFunction).Name) Then spreadsheetControl1.Document.GlobalCustomFunctions.Add(customFunction)
        End Sub

        Private Sub spreadsheetControl1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs)
            Dim sheet As Worksheet = spreadsheetControl1.Document.Worksheets(0)
            If Equals(e.SheetName, sheet.Name) AndAlso e.RowIndex = 3 AndAlso e.ColumnIndex = 2 Then
                sheet.Columns(2).AutoFit()
                sheet.Columns(4).AutoFit()
            End If
        End Sub
    End Class
End Namespace

Public Class NumberInWordsFunction
    Inherits Object
    Implements ICustomFunction

    Const functionName As String = "SPELLNUMBER"

    Private ReadOnly functionParameters As ParameterInfo()

    Private Shared cultureInfoParamTable As List(Of CultureInfo) = CreateCultureInfoParamTable()

    Public Sub New()
        functionParameters = New ParameterInfo() {New ParameterInfo(ParameterType.Value), New ParameterInfo(ParameterType.Value), New ParameterInfo(ParameterType.Value, ParameterAttributes.Optional)}
    End Sub

    Private ReadOnly Property Name As String Implements IFunction.Name
        Get
            Return functionName
        End Get
    End Property

    Private ReadOnly Property Parameters As ParameterInfo() Implements IFunction.Parameters
        Get
            Return functionParameters
        End Get
    End Property

    Private ReadOnly Property ReturnType As ParameterType Implements IFunction.ReturnType
        Get
            Return ParameterType.Value
        End Get
    End Property

    Private ReadOnly Property Volatile As Boolean Implements IFunction.Volatile
        Get
            Return True
        End Get
    End Property

    Private Function Evaluate(ByVal parameters As IList(Of ParameterValue), ByVal context As EvaluationContext) As ParameterValue Implements IFunction.Evaluate
        Dim isOrdinal As Boolean = False
        Dim numberValue As ParameterValue = parameters(0)
        If numberValue.IsError Then Return numberValue
        Dim cultureValue As ParameterValue = parameters(1)
        If cultureValue.IsError Then Return cultureValue
        If cultureValue.NumericValue < 1 OrElse cultureValue.NumericValue > cultureInfoParamTable.Count Then Return ParameterValue.ErrorNumber
        If parameters.Count = 3 Then
            Dim ordinalValue As ParameterValue = parameters(1)
            If ordinalValue.IsError Then Return ordinalValue
            isOrdinal = ordinalValue.BooleanValue
        End If

        Dim number As Long = 0
        Try
            number = Convert.ToInt64(numberValue.NumericValue)
        Catch __unusedOverflowException1__ As OverflowException
            Return ParameterValue.ErrorNumber
        End Try

        If number < 0 Then Return ParameterValue.ErrorNumber
        Dim culture As CultureInfo = cultureInfoParamTable(CInt(cultureValue.NumericValue) - 1)
        If isOrdinal Then
            Return NumberInWords.Ordinal.ConvertToText(number, culture)
        Else
            Return NumberInWords.Cardinal.ConvertToText(number, culture)
        End If
    End Function

    Private Function GetName(ByVal culture As CultureInfo) As String Implements IFunction.GetName
        Return functionName
    End Function

    Private Shared Function CreateCultureInfoParamTable() As List(Of CultureInfo)
        Dim result As List(Of CultureInfo) = New List(Of CultureInfo)()
        result.Add(CultureInfo.GetCultureInfo("en-US"))
        result.Add(CultureInfo.GetCultureInfo("en-GB"))
        result.Add(CultureInfo.GetCultureInfo("fr-FR"))
        result.Add(CultureInfo.GetCultureInfo("de-DE"))
        result.Add(CultureInfo.GetCultureInfo("el-GR"))
        result.Add(CultureInfo.GetCultureInfo("hi-IN"))
        result.Add(CultureInfo.GetCultureInfo("it-IT"))
        result.Add(CultureInfo.GetCultureInfo("pt-PT"))
        result.Add(CultureInfo.GetCultureInfo("ru-RU"))
        result.Add(CultureInfo.GetCultureInfo("es-ES"))
        result.Add(CultureInfo.GetCultureInfo("sv-SE"))
        result.Add(CultureInfo.GetCultureInfo("th-TH"))
        result.Add(CultureInfo.GetCultureInfo("tr-TR"))
        result.Add(CultureInfo.GetCultureInfo("uk-UA"))
        Return result
    End Function
End Class
