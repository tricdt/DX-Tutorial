Imports System.Drawing
Imports DevExpress.Spreadsheet

Namespace DevExpress.XtraSpreadsheet.Demos

    Public Module TopTradingPartners

        Public Sub ApplyTopImportsConditionalFormatting(ByVal sheet As Worksheet)
            Dim conditionalFormattings As ConditionalFormattingCollection = sheet.ConditionalFormattings
            Dim cfRule2 As RankConditionalFormatting = conditionalFormattings.AddRankConditionalFormatting(sheet("Table[Imports]"), ConditionalFormattingRankCondition.TopByRank, 5)
            cfRule2.Formatting.Fill.BackgroundColor = Color.FromArgb(250, 191, 143)
        End Sub

        Public Sub ApplyImportsYearlyChangeConditionalFormatting(ByVal sheet As Worksheet)
            Dim conditionalFormattings As ConditionalFormattingCollection = sheet.ConditionalFormattings
            Dim cfRule As ExpressionConditionalFormatting = conditionalFormattings.AddExpressionConditionalFormatting(sheet("Table[Imports 1Y Chg]"), ConditionalFormattingExpressionCondition.GreaterThan, "0")
            cfRule.Formatting.Font.Color = Color.FromArgb(52, 150, 151)
            Dim cfRule2 As ExpressionConditionalFormatting = conditionalFormattings.AddExpressionConditionalFormatting(sheet("Table[Imports 1Y Chg]"), ConditionalFormattingExpressionCondition.LessThan, "0")
            cfRule2.Formatting.Font.Color = Color.FromArgb(227, 108, 9)
        End Sub

        Public Sub ApplyTopExportsConditionalFormatting(ByVal sheet As Worksheet)
            Dim conditionalFormattings As ConditionalFormattingCollection = sheet.ConditionalFormattings
            Dim cfRule1 As RankConditionalFormatting = conditionalFormattings.AddRankConditionalFormatting(sheet("Table[Exports]"), ConditionalFormattingRankCondition.TopByRank, 5)
            cfRule1.Formatting.Fill.BackgroundColor = Color.FromArgb(141, 215, 217)
        End Sub

        Public Sub ApplyExportsYearlyChangeConditionalFormatting(ByVal sheet As Worksheet)
            Dim conditionalFormattings As ConditionalFormattingCollection = sheet.ConditionalFormattings
            Dim cfRule As ExpressionConditionalFormatting = conditionalFormattings.AddExpressionConditionalFormatting(sheet("Table[Exports 1Y Chg]"), ConditionalFormattingExpressionCondition.GreaterThan, "0")
            cfRule.Formatting.Font.Color = Color.FromArgb(52, 150, 151)
            Dim cfRule2 As ExpressionConditionalFormatting = conditionalFormattings.AddExpressionConditionalFormatting(sheet("Table[Exports 1Y Chg]"), ConditionalFormattingExpressionCondition.LessThan, "0")
            cfRule2.Formatting.Font.Color = Color.FromArgb(227, 108, 9)
        End Sub

        Public Sub ApplyAsiaCountriesConditionalFormatting(ByVal sheet As Worksheet)
            Dim conditionalFormattings As ConditionalFormattingCollection = sheet.ConditionalFormattings
            Dim cfRule As FormulaExpressionConditionalFormatting = conditionalFormattings.AddFormulaExpressionConditionalFormatting(sheet("Table[Country]"), "=$B6=""Asia""")
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 94, 202, 199)
        End Sub

        Public Sub ApplyBalanceChangeConditionalFormatting(ByVal sheet As Worksheet)
            Dim conditionalFormattings As ConditionalFormattingCollection = sheet.ConditionalFormattings
            Dim lowBound1 As ConditionalFormattingValue = sheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto)
            Dim highBound1 As ConditionalFormattingValue = sheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto)
            Dim cfRule1 As DataBarConditionalFormatting = conditionalFormattings.AddDataBarConditionalFormatting(sheet("Table[Balance 1Y Chg]"), lowBound1, highBound1, Color.FromArgb(87, 200, 197))
            cfRule1.GradientFill = False
            cfRule1.NegativeBarColor = Color.FromArgb(247, 150, 70)
            cfRule1.AxisPosition = ConditionalFormattingDataBarAxisPosition.Middle
            cfRule1.AxisColor = Color.Black
        End Sub

        Public Sub ApplyBalanceTrendConditionalFormatting(ByVal sheet As Worksheet)
            Dim conditionalFormattings As ConditionalFormattingCollection = sheet.ConditionalFormattings
            Dim minPoint As ConditionalFormattingIconSetValue = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Formula, "=MIN($F$6:$F$22)", ConditionalFormattingValueOperator.GreaterOrEqual)
            Dim midPoint As ConditionalFormattingIconSetValue = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual)
            Dim maxPoint As ConditionalFormattingIconSetValue = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0.0001", ConditionalFormattingValueOperator.GreaterOrEqual)
            Dim cfRule As IconSetConditionalFormatting = conditionalFormattings.AddIconSetConditionalFormatting(sheet("Table[Balance]"), IconSetType.ArrowsGray3, New ConditionalFormattingIconSetValue() {minPoint, midPoint, maxPoint})
        End Sub
    End Module
End Namespace
