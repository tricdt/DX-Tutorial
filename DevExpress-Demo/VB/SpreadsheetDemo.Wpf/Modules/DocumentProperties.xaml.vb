Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Functions
Imports DevExpress.XtraSpreadsheet

Namespace SpreadsheetDemo

    Public Partial Class DocumentProperties
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            RegisterCustomFunctions()
            InitializeWorkbook()
            GenerateCustomPropertiesWorksheet()
            AddHandler spreadsheetControl1.DocumentPropertiesChanged, New DocumentPropertiesChangedEventHandler(AddressOf spreadsheetControl1_DocumentPropertiesChanged)
        End Sub

        Private Sub spreadsheetControl1_DocumentPropertiesChanged(ByVal sender As Object, ByVal e As DocumentPropertiesChangedEventArgs)
            If e.BuiltInPropertiesChanged Then spreadsheetControl1.Document.CalculateFull()
            If e.CustomPropertiesChanged Then GenerateCustomPropertiesWorksheet()
        End Sub

        Private Sub InitializeWorkbook()
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("DocumentProperties_template.xlsx"))
            spreadsheetControl1.Document.Unit = DevExpress.Office.DocumentUnit.Point
        End Sub

        Private Sub RegisterCustomFunctions()
            Dim workbook As IWorkbook = spreadsheetControl1.Document
            Dim customFunction As DocumentPropertyFunction = New DocumentPropertyFunction()
            If Not workbook.GlobalCustomFunctions.Contains(CType(customFunction, ICustomFunction).Name) Then workbook.GlobalCustomFunctions.Add(customFunction)
        End Sub

        Private Sub GenerateCustomPropertiesWorksheet()
            If Not ShouldGenerateCustomPropertiesWorksheet() Then Return
            Dim workbook As IWorkbook = spreadsheetControl1.Document
            Dim worksheet As Worksheet = workbook.Worksheets(1)
            Dim properties As DevExpress.Spreadsheet.DocumentProperties = workbook.DocumentProperties
            workbook.BeginUpdate()
            Try
                Dim rowIndex As Integer = 3
                Cleanup(worksheet, rowIndex)
                For Each propertyName As String In properties.Custom.Names
                    Dim propertyValue As CellValue = properties.Custom(propertyName)
                    worksheet(rowIndex, 1).Value = propertyName
                    worksheet(rowIndex, 2).Formula = String.Format("=DOCPROP(""{0}"")", propertyName)
                    If rowIndex Mod 2 <> 0 Then
                        worksheet(rowIndex, 1).Style = workbook.Styles("PropName1")
                        worksheet(rowIndex, 2).Style = workbook.Styles("PropValue1")
                    Else
                        worksheet(rowIndex, 1).Style = workbook.Styles("PropName2")
                        worksheet(rowIndex, 2).Style = workbook.Styles("PropValue2")
                    End If

                    worksheet(rowIndex, 2).NumberFormat = If(propertyValue.IsDateTime, "m/d/yyyy", String.Empty)
                    worksheet.Rows(rowIndex).Height = 21
                    rowIndex += 1
                Next

                SetLastRowBorder(worksheet, rowIndex)
            Finally
                workbook.EndUpdate()
            End Try
        End Sub

        Private Function ShouldGenerateCustomPropertiesWorksheet() As Boolean
            Return spreadsheetControl1.Document.Worksheets.Contains("Custom")
        End Function

        Private Sub Cleanup(ByVal worksheet As Worksheet, ByVal rowIndex As Integer)
            Dim usedRange = worksheet.GetUsedRange()
            If usedRange.BottomRowIndex >= rowIndex Then
                Dim range = worksheet.Range.FromLTRB(1, rowIndex, 2, usedRange.BottomRowIndex)
                range.ClearContents()
                range.ClearFormats()
                range.Fill.BackgroundColor = Color.White
            End If
        End Sub

        Private Sub SetLastRowBorder(ByVal worksheet As Worksheet, ByVal rowIndex As Integer)
            Dim range = worksheet.Range.FromLTRB(1, rowIndex - 1, 2, rowIndex - 1)
            range.Borders.BottomBorder.LineStyle = BorderLineStyle.Medium
            range.Borders.BottomBorder.Color = Color.Gray
        End Sub
    End Class

#Region "DocumentPropertyFunction"
    Public Class DocumentPropertyFunction
        Inherits Object
        Implements ICustomFunction

        Const functionName As String = "DOCPROP"

        Private ReadOnly functionParameters As ParameterInfo()

        Public Sub New()
            functionParameters = New ParameterInfo() {New ParameterInfo(ParameterType.Value)}
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
            Dim propertyNameParam As ParameterValue = parameters(0)
            If propertyNameParam.IsError Then Return propertyNameParam
            If propertyNameParam.IsText Then
                Dim propertyName As String = propertyNameParam.TextValue
                Dim properties As DevExpress.Spreadsheet.DocumentProperties = context.Sheet.Workbook.DocumentProperties
                If propertyName.Equals("Application", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Application
                If propertyName.Equals("Manager", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Manager
                If propertyName.Equals("Company", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Company
                If propertyName.Equals("Version", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Version
                If propertyName.Equals("Security", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Security.ToString()
                If propertyName.Equals("Title", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Title
                If propertyName.Equals("Subject", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Subject
                If propertyName.Equals("Author", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Author
                If propertyName.Equals("Keywords", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Keywords
                If propertyName.Equals("Description", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Description
                If propertyName.Equals("LastModifiedBy", StringComparison.InvariantCultureIgnoreCase) Then Return properties.LastModifiedBy
                If propertyName.Equals("Category", StringComparison.InvariantCultureIgnoreCase) Then Return properties.Category
                If propertyName.Equals("Created", StringComparison.InvariantCultureIgnoreCase) Then
                    If properties.Created <> Date.MinValue Then Return properties.Created
                    Return String.Empty
                End If

                If propertyName.Equals("Modified", StringComparison.InvariantCultureIgnoreCase) Then
                    If properties.Modified <> Date.MinValue Then Return properties.Modified
                    Return String.Empty
                End If

                If propertyName.Equals("Printed", StringComparison.InvariantCultureIgnoreCase) Then
                    If properties.Printed <> Date.MinValue Then Return properties.Printed
                    Return String.Empty
                End If

                Return properties.Custom(propertyName)
            End If

            Return ParameterValue.ErrorInvalidValueInFunction
        End Function

        Private Function GetName(ByVal culture As CultureInfo) As String Implements IFunction.GetName
            Return functionName
        End Function
    End Class
#End Region
End Namespace
