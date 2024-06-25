Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace DevExpress.DevAV.MasterDetailReport

    Public Partial Class Report

        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Me.components IsNot Nothing Then
                    Me.components.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DevExpress.DevAV.MasterDetailReport.Report))
            Dim xrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
            Dim xrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
            Dim selectQuery1 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
            Dim column1 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression1 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim table1 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
            Dim column2 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression2 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column3 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression3 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column4 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression4 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column5 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression5 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column6 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression6 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column7 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression7 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column8 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim customExpression1 As DevExpress.DataAccess.Sql.CustomExpression = New DevExpress.DataAccess.Sql.CustomExpression()
            Dim column9 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim customExpression2 As DevExpress.DataAccess.Sql.CustomExpression = New DevExpress.DataAccess.Sql.CustomExpression()
            Dim selectQuery2 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
            Dim column10 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression8 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim table2 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
            Dim column11 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression9 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column12 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression10 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column13 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression11 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column14 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression12 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column15 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression13 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column16 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression14 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim column17 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
            Dim columnExpression15 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
            Dim table3 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
            Dim join1 As DevExpress.DataAccess.Sql.Join = New DevExpress.DataAccess.Sql.Join()
            Dim relationColumnInfo1 As DevExpress.DataAccess.Sql.RelationColumnInfo = New DevExpress.DataAccess.Sql.RelationColumnInfo()
            Dim selectQuery3 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
            Dim allColumns1 As DevExpress.DataAccess.Sql.AllColumns = New DevExpress.DataAccess.Sql.AllColumns()
            Dim table4 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
            Dim masterDetailInfo1 As DevExpress.DataAccess.Sql.MasterDetailInfo = New DevExpress.DataAccess.Sql.MasterDetailInfo()
            Dim relationColumnInfo2 As DevExpress.DataAccess.Sql.RelationColumnInfo = New DevExpress.DataAccess.Sql.RelationColumnInfo()
            Dim masterDetailInfo2 As DevExpress.DataAccess.Sql.MasterDetailInfo = New DevExpress.DataAccess.Sql.MasterDetailInfo()
            Dim relationColumnInfo3 As DevExpress.DataAccess.Sql.RelationColumnInfo = New DevExpress.DataAccess.Sql.RelationColumnInfo()
            Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
            Me.xrTable1 = New DevExpress.XtraReports.UI.XRTable()
            Me.xrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.cellCompanyName = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow5 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow7 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell29 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell30 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell38 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell31 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow10 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell39 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell40 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell43 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell44 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow11 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell45 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell46 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell47 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell48 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow12 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell49 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell50 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell51 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell52 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow13 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell53 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell54 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow16 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell57 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
            Me.xrPictureBox4 = New DevExpress.XtraReports.UI.XRPictureBox()
            Me.xrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
            Me.xrPageInfo3 = New DevExpress.XtraReports.UI.XRPageInfo()
            Me.DetailReportBand = New DevExpress.XtraReports.UI.DetailReportBand()
            Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
            Me.xrTable4 = New DevExpress.XtraReports.UI.XRTable()
            Me.xrTableRow14 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell55 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell13 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableRow6 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell56 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell24 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell25 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell26 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell27 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell28 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrCheckBox1 = New DevExpress.XtraReports.UI.XRCheckBox()
            Me.DetailReport2 = New DevExpress.XtraReports.UI.DetailReportBand()
            Me.Detail4 = New DevExpress.XtraReports.UI.DetailBand()
            Me.xrTable7 = New DevExpress.XtraReports.UI.XRTable()
            Me.xrTableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell35 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell36 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell42 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell37 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.ReportHeader3 = New DevExpress.XtraReports.UI.ReportHeaderBand()
            Me.xrTable6 = New DevExpress.XtraReports.UI.XRTable()
            Me.xrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell32 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell33 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell41 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell34 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.GroupFooter2 = New DevExpress.XtraReports.UI.GroupFooterBand()
            Me.xrTable3 = New DevExpress.XtraReports.UI.XRTable()
            Me.xrTableRow15 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
            Me.xrTable2 = New DevExpress.XtraReports.UI.XRTable()
            Me.xrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
            Me.xrPageBreak1 = New DevExpress.XtraReports.UI.XRPageBreak()
            Me.xrTable5 = New DevExpress.XtraReports.UI.XRTable()
            Me.xrTableRow4 = New DevExpress.XtraReports.UI.XRTableRow()
            Me.xrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.xrTableCell23 = New DevExpress.XtraReports.UI.XRTableCell()
            Me.dsMasterDetail1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
            Me.xrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
            Me.xrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
            Me.ReportHeader1 = New DevExpress.XtraReports.UI.ReportHeaderBand()
            Me.xrLine1 = New DevExpress.XtraReports.UI.XRLine()
            Me.lbTitle = New DevExpress.XtraReports.UI.XRLabel()
            Me.xrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
            Me.topMarginBand1 = New DevExpress.XtraReports.UI.TopMarginBand()
            Me.ProductData = New DevExpress.XtraReports.UI.XRControlStyle()
            Me.OddStyle = New DevExpress.XtraReports.UI.XRControlStyle()
            Me.GroupHeader = New DevExpress.XtraReports.UI.XRControlStyle()
            Me.ProductHeader = New DevExpress.XtraReports.UI.XRControlStyle()
            Me.SupplierTitle = New DevExpress.XtraReports.UI.XRControlStyle()
            Me.EvenStyle = New DevExpress.XtraReports.UI.XRControlStyle()
            Me.SupplierInfo = New DevExpress.XtraReports.UI.XRControlStyle()
            Me.OrderDetailsTotal = New DevExpress.XtraReports.UI.CalculatedField()
            CType((Me.xrTable1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.xrTable4), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.xrTable7), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.xrTable6), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.xrTable3), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.xrTable2), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.xrTable5), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Detail.BackColor = System.Drawing.Color.Transparent
            Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTable1})
            Me.Detail.Font = New System.Drawing.Font("Verdana", 9.75F)
            Me.Detail.HeightF = 210F
            Me.Detail.Name = "Detail"
            Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTable1.BackColor = System.Drawing.Color.Transparent
            Me.xrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
            Me.xrTable1.Name = "xrTable1"
            Me.xrTable1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableRow1, Me.xrTableRow5, Me.xrTableRow7, Me.xrTableRow10, Me.xrTableRow11, Me.xrTableRow12, Me.xrTableRow13, Me.xrTableRow16})
            Me.xrTable1.SizeF = New System.Drawing.SizeF(650F, 186F)
            Me.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell3, Me.cellCompanyName})
            Me.xrTableRow1.Name = "xrTableRow1"
            Me.xrTableRow1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow1.StyleName = "SupplierTitle"
            Me.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow1.Weight = 0.12903225806451613R
            Me.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell3.Font = New System.Drawing.Font("Tahoma", 14.25F)
            Me.xrTableCell3.Name = "xrTableCell3"
            Me.xrTableCell3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell3.Text = "Company"
            Me.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell3.Weight = 0.15384615384615386R
            Me.cellCompanyName.Bookmark = "Norske Meierier"
            Me.cellCompanyName.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.cellCompanyName.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyName]"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Bookmark", "[CompanyName]")})
            Me.cellCompanyName.Font = New System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold)
            Me.cellCompanyName.Name = "cellCompanyName"
            Me.cellCompanyName.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.cellCompanyName.Text = "Norske Meierier"
            Me.cellCompanyName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.cellCompanyName.Weight = 0.84615384615384615R
            Me.xrTableRow5.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell17})
            Me.xrTableRow5.Name = "xrTableRow5"
            Me.xrTableRow5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow5.Weight = 0.086021505376344093R
            Me.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell17.Name = "xrTableCell17"
            Me.xrTableCell17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableCell17.Weight = 1R
            Me.xrTableRow7.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell29, Me.xrTableCell30, Me.xrTableCell38, Me.xrTableCell31})
            Me.xrTableRow7.Name = "xrTableRow7"
            Me.xrTableRow7.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow7.Weight = 0.12903225806451613R
            Me.xrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell29.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell29.Name = "xrTableCell29"
            Me.xrTableCell29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell29.StyleName = "SupplierInfo"
            Me.xrTableCell29.Text = "Contact Name:"
            Me.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell29.Weight = 0.2153846153846154R
            Me.xrTableCell30.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell30.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell30.BorderWidth = 1F
            Me.xrTableCell30.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ContactName]")})
            Me.xrTableCell30.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell30.Name = "xrTableCell30"
            Me.xrTableCell30.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell30.Text = "Beate Vileid"
            Me.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell30.Weight = 0.33692307692307694R
            Me.xrTableCell38.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell38.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell38.ForeColor = System.Drawing.Color.Black
            Me.xrTableCell38.Name = "xrTableCell38"
            Me.xrTableCell38.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell38.StyleName = "SupplierInfo"
            Me.xrTableCell38.Text = "Country:"
            Me.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell38.Weight = 0.17846153846153845R
            Me.xrTableCell31.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell31.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell31.BorderWidth = 1F
            Me.xrTableCell31.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Country]")})
            Me.xrTableCell31.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell31.Name = "xrTableCell31"
            Me.xrTableCell31.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell31.Text = "Norway"
            Me.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell31.Weight = 0.26923076923076922R
            Me.xrTableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell39, Me.xrTableCell40, Me.xrTableCell43, Me.xrTableCell44})
            Me.xrTableRow10.Name = "xrTableRow10"
            Me.xrTableRow10.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow10.Weight = 0.12903225806451613R
            Me.xrTableCell39.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell39.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell39.Name = "xrTableCell39"
            Me.xrTableCell39.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell39.StyleName = "SupplierInfo"
            Me.xrTableCell39.Text = "Contact Title:"
            Me.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell39.Weight = 0.2153846153846154R
            Me.xrTableCell40.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell40.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell40.BorderWidth = 1F
            Me.xrTableCell40.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ContactTitle]")})
            Me.xrTableCell40.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell40.Name = "xrTableCell40"
            Me.xrTableCell40.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell40.Text = "Marketing Manager"
            Me.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell40.Weight = 0.33692307692307694R
            Me.xrTableCell43.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell43.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell43.ForeColor = System.Drawing.Color.Black
            Me.xrTableCell43.Name = "xrTableCell43"
            Me.xrTableCell43.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell43.StyleName = "SupplierInfo"
            Me.xrTableCell43.Text = "Region:"
            Me.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell43.Weight = 0.17846153846153845R
            Me.xrTableCell44.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell44.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell44.BorderWidth = 1F
            Me.xrTableCell44.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Region]")})
            Me.xrTableCell44.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell44.Name = "xrTableCell44"
            Me.xrTableCell44.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell44.Weight = 0.26923076923076922R
            Me.xrTableRow11.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell45, Me.xrTableCell46, Me.xrTableCell47, Me.xrTableCell48})
            Me.xrTableRow11.Name = "xrTableRow11"
            Me.xrTableRow11.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow11.Weight = 0.12903225806451613R
            Me.xrTableCell45.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell45.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell45.Name = "xrTableCell45"
            Me.xrTableCell45.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell45.StyleName = "SupplierInfo"
            Me.xrTableCell45.Text = "Phone:"
            Me.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell45.Weight = 0.2153846153846154R
            Me.xrTableCell46.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell46.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell46.BorderWidth = 1F
            Me.xrTableCell46.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Phone]")})
            Me.xrTableCell46.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell46.Name = "xrTableCell46"
            Me.xrTableCell46.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell46.Text = "(0)2-953010"
            Me.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell46.Weight = 0.33692307692307694R
            Me.xrTableCell47.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell47.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell47.ForeColor = System.Drawing.Color.Black
            Me.xrTableCell47.Name = "xrTableCell47"
            Me.xrTableCell47.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell47.StyleName = "SupplierInfo"
            Me.xrTableCell47.Text = "City:"
            Me.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell47.Weight = 0.17846153846153845R
            Me.xrTableCell48.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell48.BorderWidth = 1F
            Me.xrTableCell48.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[City]")})
            Me.xrTableCell48.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell48.Name = "xrTableCell48"
            Me.xrTableCell48.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell48.Text = "Sandvika"
            Me.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell48.Weight = 0.26923076923076922R
            Me.xrTableRow12.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell49, Me.xrTableCell50, Me.xrTableCell51, Me.xrTableCell52})
            Me.xrTableRow12.Name = "xrTableRow12"
            Me.xrTableRow12.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow12.Weight = 0.12903225806451613R
            Me.xrTableCell49.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell49.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell49.Name = "xrTableCell49"
            Me.xrTableCell49.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell49.StyleName = "SupplierInfo"
            Me.xrTableCell49.Text = "Fax:"
            Me.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell49.Weight = 0.2153846153846154R
            Me.xrTableCell50.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell50.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell50.BorderWidth = 1F
            Me.xrTableCell50.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Fax]")})
            Me.xrTableCell50.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell50.Name = "xrTableCell50"
            Me.xrTableCell50.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell50.Weight = 0.33692307692307694R
            Me.xrTableCell51.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell51.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell51.ForeColor = System.Drawing.Color.Black
            Me.xrTableCell51.Name = "xrTableCell51"
            Me.xrTableCell51.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell51.StyleName = "SupplierInfo"
            Me.xrTableCell51.Text = "Postal Code:"
            Me.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell51.Weight = 0.17846153846153845R
            Me.xrTableCell52.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell52.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell52.BorderWidth = 1F
            Me.xrTableCell52.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PostalCode]")})
            Me.xrTableCell52.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell52.Name = "xrTableCell52"
            Me.xrTableCell52.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell52.Text = "1320"
            Me.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell52.Weight = 0.26923076923076922R
            Me.xrTableRow13.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell53, Me.xrTableCell54})
            Me.xrTableRow13.Name = "xrTableRow13"
            Me.xrTableRow13.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow13.Weight = 0.13440860215053763R
            Me.xrTableCell53.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell53.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell53.Name = "xrTableCell53"
            Me.xrTableCell53.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell53.StyleName = "SupplierInfo"
            Me.xrTableCell53.Text = "Home Page:"
            Me.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell53.Weight = 0.2153846153846154R
            Me.xrTableCell54.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell54.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell54.BorderWidth = 1F
            Me.xrTableCell54.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Replace([HomePage], '#', ' ')")})
            Me.xrTableCell54.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell54.Name = "xrTableCell54"
            Me.xrTableCell54.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell54.Weight = 0.7846153846153846R
            Me.xrTableRow16.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell6, Me.xrTableCell57})
            Me.xrTableRow16.Name = "xrTableRow16"
            Me.xrTableRow16.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow16.Weight = 0.13440860215053763R
            Me.xrTableCell6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
            Me.xrTableCell6.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell6.Name = "xrTableCell6"
            Me.xrTableCell6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell6.StyleName = "SupplierInfo"
            Me.xrTableCell6.Text = "Address:"
            Me.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell6.Weight = 0.2153846153846154R
            Me.xrTableCell57.BorderColor = System.Drawing.Color.Black
            Me.xrTableCell57.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell57.BorderWidth = 1F
            Me.xrTableCell57.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Address]")})
            Me.xrTableCell57.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell57.Name = "xrTableCell57"
            Me.xrTableCell57.Padding = New DevExpress.XtraPrinting.PaddingInfo(12, 2, 0, 0, 100F)
            Me.xrTableCell57.Text = "Hatlevegen 5"
            Me.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell57.Weight = 0.7846153846153846R
            Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPictureBox4, Me.xrPageInfo1, Me.xrPageInfo3, Me.xrLabel1, Me.xrLabel2})
            Me.BottomMargin.HeightF = 142F
            Me.BottomMargin.Name = "BottomMargin"
            Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrPictureBox4.ImageSource = New DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPictureBox4.ImageSource"))
            Me.xrPictureBox4.LocationFloat = New DevExpress.Utils.PointFloat(257F, 63F)
            Me.xrPictureBox4.Name = "xrPictureBox4"
            Me.xrPictureBox4.NavigateUrl = "http://www.devexpress.com/Products/NET/Reporting/"
            Me.xrPictureBox4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrPictureBox4.SizeF = New System.Drawing.SizeF(165.625F, 30.20833F)
            Me.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize
            Me.xrPictureBox4.StylePriority.UseBackColor = False
            Me.xrPictureBox4.UseImageResolution = False
            Me.xrPageInfo1.Font = New System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic)
            Me.xrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(517F, 25F)
            Me.xrPageInfo1.Name = "xrPageInfo1"
            Me.xrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrPageInfo1.SizeF = New System.Drawing.SizeF(151F, 17F)
            Me.xrPageInfo1.StylePriority.UseFont = False
            Me.xrPageInfo1.StylePriority.UseTextAlignment = False
            Me.xrPageInfo1.Text = "Total"
            Me.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            Me.xrPageInfo1.TextFormatString = "{0} of {1} pages"
            Me.xrPageInfo3.Font = New System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic)
            Me.xrPageInfo3.LocationFloat = New DevExpress.Utils.PointFloat(0F, 25F)
            Me.xrPageInfo3.Name = "xrPageInfo3"
            Me.xrPageInfo3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrPageInfo3.RunningBand = Me.DetailReportBand
            Me.xrPageInfo3.SizeF = New System.Drawing.SizeF(150F, 17F)
            Me.xrPageInfo3.StylePriority.UseFont = False
            Me.xrPageInfo3.StylePriority.UseTextAlignment = False
            Me.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrPageInfo3.TextFormatString = "{0} of {1} pages"
            Me.DetailReportBand.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1, Me.DetailReport2})
            Me.DetailReportBand.DataMember = "Suppliers.SuppliersProducts"
            Me.DetailReportBand.DataSource = Me.dsMasterDetail1
            Me.DetailReportBand.Level = 0
            Me.DetailReportBand.Name = "DetailReportBand"
            Me.DetailReportBand.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.DetailReportBand.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
            Me.DetailReportBand.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.Detail1.Borders = CType(((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Right) Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.Detail1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTable4})
            Me.Detail1.HeightF = 50F
            Me.Detail1.Name = "Detail1"
            Me.Detail1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTable4.BackColor = System.Drawing.Color.Transparent
            Me.xrTable4.Bookmark = "Geitost"
            Me.xrTable4.BookmarkParent = Me.cellCompanyName
            Me.xrTable4.Borders = CType(((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Right) Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTable4.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Bookmark", "[ProductName]")})
            Me.xrTable4.Font = New System.Drawing.Font("Times New Roman", 12F)
            Me.xrTable4.KeepTogether = True
            Me.xrTable4.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
            Me.xrTable4.Name = "xrTable4"
            Me.xrTable4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTable4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableRow14, Me.xrTableRow6})
            Me.xrTable4.SizeF = New System.Drawing.SizeF(651F, 50F)
            Me.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow14.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((194)))))), (CInt(((CByte((226)))))), (CInt(((CByte((255)))))))
            Me.xrTableRow14.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell55, Me.xrTableCell11, Me.xrTableCell12, Me.xrTableCell13, Me.xrTableCell14, Me.xrTableCell15})
            Me.xrTableRow14.Font = New System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic)
            Me.xrTableRow14.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((0)))))), (CInt(((CByte((103)))))), (CInt(((CByte((196)))))))
            Me.xrTableRow14.Name = "xrTableRow14"
            Me.xrTableRow14.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow14.Weight = 0.48R
            Me.xrTableCell55.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell55.Name = "xrTableCell55"
            Me.xrTableCell55.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell55.StyleName = "ProductHeader"
            Me.xrTableCell55.Text = "Product Name"
            Me.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell55.Weight = 0.21812596006144394R
            Me.xrTableCell11.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell11.Name = "xrTableCell11"
            Me.xrTableCell11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell11.StyleName = "ProductHeader"
            Me.xrTableCell11.Text = "Product ID"
            Me.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell11.Weight = 0.15360983102918588R
            Me.xrTableCell12.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell12.Name = "xrTableCell12"
            Me.xrTableCell12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell12.StyleName = "ProductHeader"
            Me.xrTableCell12.Text = "Category"
            Me.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell12.Weight = 0.13978494623655913R
            Me.xrTableCell13.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell13.Name = "xrTableCell13"
            Me.xrTableCell13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell13.StyleName = "ProductHeader"
            Me.xrTableCell13.Text = "Quantity per Unit"
            Me.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell13.Weight = 0.17972350230414746R
            Me.xrTableCell14.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell14.Name = "xrTableCell14"
            Me.xrTableCell14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell14.StyleName = "ProductHeader"
            Me.xrTableCell14.Text = "Unit Price"
            Me.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell14.Weight = 0.15360983102918588R
            Me.xrTableCell15.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell15.Name = "xrTableCell15"
            Me.xrTableCell15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell15.StyleName = "ProductHeader"
            Me.xrTableCell15.Text = "Discontinued"
            Me.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell15.Weight = 0.15514592933947774R
            Me.xrTableRow6.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell56, Me.xrTableCell24, Me.xrTableCell25, Me.xrTableCell26, Me.xrTableCell27, Me.xrTableCell28})
            Me.xrTableRow6.Name = "xrTableRow6"
            Me.xrTableRow6.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow6.StyleName = "ProductData"
            Me.xrTableRow6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow6.Weight = 0.52R
            Me.xrTableCell56.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductName]")})
            Me.xrTableCell56.Font = New System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold)
            Me.xrTableCell56.Name = "xrTableCell56"
            Me.xrTableCell56.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell56.Text = "Geitost"
            Me.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell56.Weight = 0.21812596006144394R
            Me.xrTableCell24.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductID]")})
            Me.xrTableCell24.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell24.Name = "xrTableCell24"
            Me.xrTableCell24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell24.Text = "33"
            Me.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell24.Weight = 0.15360983102918588R
            Me.xrTableCell25.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CategoryName]")})
            Me.xrTableCell25.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell25.Name = "xrTableCell25"
            Me.xrTableCell25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell25.Text = "Dairy Products"
            Me.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell25.Weight = 0.13978494623655913R
            Me.xrTableCell26.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[QuantityPerUnit]")})
            Me.xrTableCell26.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell26.Name = "xrTableCell26"
            Me.xrTableCell26.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell26.Text = "500 g"
            Me.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell26.Weight = 0.17972350230414746R
            Me.xrTableCell27.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]")})
            Me.xrTableCell27.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell27.Name = "xrTableCell27"
            Me.xrTableCell27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell27.Text = "2.5"
            Me.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell27.TextFormatString = "{0:$0.00}"
            Me.xrTableCell27.Weight = 0.15360983102918588R
            Me.xrTableCell28.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrCheckBox1})
            Me.xrTableCell28.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrTableCell28.Name = "xrTableCell28"
            Me.xrTableCell28.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell28.Weight = 0.15514592933947774R
            Me.xrCheckBox1.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrCheckBox1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "CheckBoxState", "[Discontinued]")})
            Me.xrCheckBox1.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrCheckBox1.LocationFloat = New DevExpress.Utils.PointFloat(42F, 0F)
            Me.xrCheckBox1.Name = "xrCheckBox1"
            Me.xrCheckBox1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrCheckBox1.SizeF = New System.Drawing.SizeF(20F, 23F)
            Me.xrCheckBox1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.DetailReport2.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail4, Me.ReportHeader3, Me.GroupFooter2, Me.GroupHeader2, Me.ReportFooter})
            Me.DetailReport2.DataMember = "Suppliers.SuppliersProducts.ProductsOrder_x0020_Details"
            Me.DetailReport2.DataSource = Me.dsMasterDetail1
            Me.DetailReport2.Level = 0
            Me.DetailReport2.Name = "DetailReport2"
            Me.DetailReport2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.DetailReport2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.Detail4.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTable7})
            Me.Detail4.Font = New System.Drawing.Font("Verdana", 8F)
            Me.Detail4.HeightF = 20F
            Me.Detail4.Name = "Detail4"
            Me.Detail4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.Detail4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTable7.BackColor = System.Drawing.Color.Transparent
            Me.xrTable7.EvenStyleName = "EvenStyle"
            Me.xrTable7.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
            Me.xrTable7.Name = "xrTable7"
            Me.xrTable7.OddStyleName = "OddStyle"
            Me.xrTable7.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTable7.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableRow9})
            Me.xrTable7.SizeF = New System.Drawing.SizeF(650F, 20F)
            Me.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow9.Borders = DevExpress.XtraPrinting.BorderSide.Right
            Me.xrTableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell18, Me.xrTableCell35, Me.xrTableCell36, Me.xrTableCell42, Me.xrTableCell37})
            Me.xrTableRow9.Name = "xrTableRow9"
            Me.xrTableRow9.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow9.Weight = 1R
            Me.xrTableCell18.BackColor = System.Drawing.Color.Transparent
            Me.xrTableCell18.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell18.Font = New System.Drawing.Font("Times New Roman", 9.75F)
            Me.xrTableCell18.Name = "xrTableCell18"
            Me.xrTableCell18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell18.StylePriority.UseBackColor = False
            Me.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableCell18.Weight = 0.21846153846153846R
            Me.xrTableCell35.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell35.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OrderID]")})
            Me.xrTableCell35.Name = "xrTableCell35"
            Me.xrTableCell35.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell35.Text = "10861"
            Me.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell35.Weight = 0.17846153846153845R
            Me.xrTableCell36.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell36.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Quantity]")})
            Me.xrTableCell36.Name = "xrTableCell36"
            Me.xrTableCell36.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell36.Text = "35"
            Me.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell36.Weight = 0.19230769230769232R
            Me.xrTableCell42.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell42.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Discount]")})
            Me.xrTableCell42.Name = "xrTableCell42"
            Me.xrTableCell42.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell42.Text = "0.00"
            Me.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell42.TextFormatString = "{0:0%}"
            Me.xrTableCell42.Weight = 0.19230769230769232R
            Me.xrTableCell37.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Right)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell37.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OrderDetailsTotal]")})
            Me.xrTableCell37.Name = "xrTableCell37"
            Me.xrTableCell37.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell37.Text = "$87.5"
            Me.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell37.TextFormatString = "{0:$0.00}"
            Me.xrTableCell37.Weight = 0.21846153846153846R
            Me.xrTableCell37.XlsxFormatString = "$0.0"
            Me.ReportHeader3.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((244)))))), (CInt(((CByte((155)))))), (CInt(((CByte((24)))))))
            Me.ReportHeader3.BorderColor = System.Drawing.Color.White
            Me.ReportHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTable6})
            Me.ReportHeader3.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold)
            Me.ReportHeader3.ForeColor = System.Drawing.Color.White
            Me.ReportHeader3.HeightF = 22F
            Me.ReportHeader3.Name = "ReportHeader3"
            Me.ReportHeader3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.ReportHeader3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTable6.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
            Me.xrTable6.Name = "xrTable6"
            Me.xrTable6.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTable6.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableRow8})
            Me.xrTable6.SizeF = New System.Drawing.SizeF(650F, 22F)
            Me.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell16, Me.xrTableCell32, Me.xrTableCell33, Me.xrTableCell41, Me.xrTableCell34})
            Me.xrTableRow8.Name = "xrTableRow8"
            Me.xrTableRow8.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow8.Weight = 1R
            Me.xrTableCell16.BackColor = System.Drawing.Color.Transparent
            Me.xrTableCell16.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableCell16.Name = "xrTableCell16"
            Me.xrTableCell16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell16.StylePriority.UseBackColor = False
            Me.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableCell16.Weight = 0.21846153846153846R
            Me.xrTableCell32.Name = "xrTableCell32"
            Me.xrTableCell32.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell32.Text = "OrderID"
            Me.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell32.Weight = 0.17846153846153845R
            Me.xrTableCell33.Borders = DevExpress.XtraPrinting.BorderSide.Left
            Me.xrTableCell33.Name = "xrTableCell33"
            Me.xrTableCell33.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell33.Text = "Quantity"
            Me.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell33.Weight = 0.19230769230769232R
            Me.xrTableCell41.Borders = DevExpress.XtraPrinting.BorderSide.Left
            Me.xrTableCell41.Name = "xrTableCell41"
            Me.xrTableCell41.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell41.Text = "Discount"
            Me.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell41.Weight = 0.19230769230769232R
            Me.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.Left
            Me.xrTableCell34.Name = "xrTableCell34"
            Me.xrTableCell34.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell34.Text = "Total"
            Me.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell34.Weight = 0.21846153846153846R
            Me.GroupFooter2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTable3})
            Me.GroupFooter2.Font = New System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold)
            Me.GroupFooter2.HeightF = 20F
            Me.GroupFooter2.Name = "GroupFooter2"
            Me.GroupFooter2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.GroupFooter2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTable3.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
            Me.xrTable3.Name = "xrTable3"
            Me.xrTable3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTable3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableRow15})
            Me.xrTable3.SizeF = New System.Drawing.SizeF(650F, 20F)
            Me.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow15.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell20, Me.xrTableCell21, Me.xrTableCell22})
            Me.xrTableRow15.Name = "xrTableRow15"
            Me.xrTableRow15.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow15.Weight = 1R
            Me.xrTableCell20.BackColor = System.Drawing.Color.Transparent
            Me.xrTableCell20.Font = New System.Drawing.Font("Times New Roman", 9.75F)
            Me.xrTableCell20.Name = "xrTableCell20"
            Me.xrTableCell20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell20.StylePriority.UseBackColor = False
            Me.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableCell20.Weight = 0.21846153846153846R
            Me.xrTableCell21.BorderColor = System.Drawing.Color.FromArgb((CInt(((CByte((232)))))), (CInt(((CByte((205)))))), (CInt(((CByte((162)))))))
            Me.xrTableCell21.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell21.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold)
            Me.xrTableCell21.Name = "xrTableCell21"
            Me.xrTableCell21.Padding = New DevExpress.XtraPrinting.PaddingInfo(7, 0, 0, 0, 100F)
            Me.xrTableCell21.Text = "Total by unit price:"
            Me.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell21.Weight = 0.563076923076923R
            Me.xrTableCell22.BorderColor = System.Drawing.Color.FromArgb((CInt(((CByte((232)))))), (CInt(((CByte((205)))))), (CInt(((CByte((162)))))))
            Me.xrTableCell22.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Right) Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell22.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([OrderDetailsTotal])")})
            Me.xrTableCell22.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold)
            Me.xrTableCell22.Name = "xrTableCell22"
            Me.xrTableCell22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
            Me.xrTableCell22.Summary = xrSummary1
            Me.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell22.TextFormatString = "{0:$0.00}"
            Me.xrTableCell22.Weight = 0.21846153846153846R
            Me.xrTableCell22.XlsxFormatString = "$0.0"
            Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTable2})
            Me.GroupHeader2.Font = New System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold)
            Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("UnitPrice", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
            Me.GroupHeader2.HeightF = 22F
            Me.GroupHeader2.Name = "GroupHeader2"
            Me.GroupHeader2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.GroupHeader2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTable2.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
            Me.xrTable2.Name = "xrTable2"
            Me.xrTable2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableRow3})
            Me.xrTable2.SizeF = New System.Drawing.SizeF(650F, 22F)
            Me.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow3.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.xrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell19, Me.xrTableCell5, Me.xrTableCell7})
            Me.xrTableRow3.Name = "xrTableRow3"
            Me.xrTableRow3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow3.StyleName = "GroupHeader"
            Me.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow3.Weight = 1R
            Me.xrTableCell19.BackColor = System.Drawing.Color.Transparent
            Me.xrTableCell19.Font = New System.Drawing.Font("Times New Roman", 9.75F)
            Me.xrTableCell19.Name = "xrTableCell19"
            Me.xrTableCell19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell19.StylePriority.UseBackColor = False
            Me.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableCell19.Weight = 0.21846153846153846R
            Me.xrTableCell5.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell5.Name = "xrTableCell5"
            Me.xrTableCell5.Padding = New DevExpress.XtraPrinting.PaddingInfo(8, 0, 0, 0, 100F)
            Me.xrTableCell5.Text = "Unit price:"
            Me.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell5.Weight = 0.17846153846153845R
            Me.xrTableCell7.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.xrTableCell7.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]")})
            Me.xrTableCell7.Name = "xrTableCell7"
            Me.xrTableCell7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrTableCell7.Text = "$2.5"
            Me.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell7.TextFormatString = "{0:$0.00}"
            Me.xrTableCell7.Weight = 0.60307692307692307R
            Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPageBreak1, Me.xrTable5})
            Me.ReportFooter.Font = New System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold)
            Me.ReportFooter.HeightF = 33.54169F
            Me.ReportFooter.Name = "ReportFooter"
            Me.ReportFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.ReportFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrPageBreak1.LocationFloat = New DevExpress.Utils.PointFloat(0F, 26.5417F)
            Me.xrPageBreak1.Name = "xrPageBreak1"
            Me.xrTable5.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
            Me.xrTable5.Name = "xrTable5"
            Me.xrTable5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTable5.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableRow4})
            Me.xrTable5.SizeF = New System.Drawing.SizeF(650F, 20F)
            Me.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCell9, Me.xrTableCell23})
            Me.xrTableRow4.Name = "xrTableRow4"
            Me.xrTableRow4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrTableRow4.StyleName = "ProductData"
            Me.xrTableRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrTableRow4.Weight = 1R
            Me.xrTableCell9.Font = New System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold)
            Me.xrTableCell9.Name = "xrTableCell9"
            Me.xrTableCell9.Padding = New DevExpress.XtraPrinting.PaddingInfo(7, 0, 2, 0, 100F)
            Me.xrTableCell9.Text = "Grand total:"
            Me.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrTableCell9.Weight = 0.78153846153846152R
            Me.xrTableCell23.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([OrderDetailsTotal])")})
            Me.xrTableCell23.Font = New System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold)
            Me.xrTableCell23.Name = "xrTableCell23"
            Me.xrTableCell23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
            Me.xrTableCell23.Summary = xrSummary2
            Me.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            Me.xrTableCell23.TextFormatString = "{0:$0.00}"
            Me.xrTableCell23.Weight = 0.21846153846153846R
            Me.xrTableCell23.XlsxFormatString = "$0.0"
            Me.dsMasterDetail1.ConnectionName = "NWindConnectionString"
            Me.dsMasterDetail1.Name = "dsMasterDetail1"
            columnExpression1.ColumnName = "OrderID"
            table1.Name = "OrderDetailsExtended"
            columnExpression1.Table = table1
            column1.Expression = columnExpression1
            columnExpression2.ColumnName = "ProductID"
            columnExpression2.Table = table1
            column2.Expression = columnExpression2
            columnExpression3.ColumnName = "ProductName"
            columnExpression3.Table = table1
            column3.Expression = columnExpression3
            columnExpression4.ColumnName = "UnitPrice"
            columnExpression4.Table = table1
            column4.Expression = columnExpression4
            columnExpression5.ColumnName = "Quantity"
            columnExpression5.Table = table1
            column5.Expression = columnExpression5
            columnExpression6.ColumnName = "ExtendedPrice"
            columnExpression6.Table = table1
            column6.Expression = columnExpression6
            columnExpression7.ColumnName = "Supplier"
            columnExpression7.Table = table1
            column7.Expression = columnExpression7
            column8.[Alias] = "Discount"
            customExpression1.Expression = "ToDecimal([OrderDetailsExtended].[Discount])"
            column8.Expression = customExpression1
            column9.[Alias] = "SubTotal"
            customExpression2.Expression = "ToFloat([OrderDetailsExtended].[Quantity] * [OrderDetailsExtended].[UnitPrice])"
            column9.Expression = customExpression2
            selectQuery1.Columns.Add(column1)
            selectQuery1.Columns.Add(column2)
            selectQuery1.Columns.Add(column3)
            selectQuery1.Columns.Add(column4)
            selectQuery1.Columns.Add(column5)
            selectQuery1.Columns.Add(column6)
            selectQuery1.Columns.Add(column7)
            selectQuery1.Columns.Add(column8)
            selectQuery1.Columns.Add(column9)
            selectQuery1.Name = "Order Details"
            selectQuery1.Tables.Add(table1)
            columnExpression8.ColumnName = "ProductID"
            table2.Name = "Products"
            columnExpression8.Table = table2
            column10.Expression = columnExpression8
            columnExpression9.ColumnName = "ProductName"
            columnExpression9.Table = table2
            column11.Expression = columnExpression9
            columnExpression10.ColumnName = "SupplierID"
            columnExpression10.Table = table2
            column12.Expression = columnExpression10
            columnExpression11.ColumnName = "CategoryID"
            columnExpression11.Table = table2
            column13.Expression = columnExpression11
            columnExpression12.ColumnName = "QuantityPerUnit"
            columnExpression12.Table = table2
            column14.Expression = columnExpression12
            columnExpression13.ColumnName = "UnitPrice"
            columnExpression13.Table = table2
            column15.Expression = columnExpression13
            columnExpression14.ColumnName = "Discontinued"
            columnExpression14.Table = table2
            column16.Expression = columnExpression14
            columnExpression15.ColumnName = "CategoryName"
            table3.Name = "Categories"
            columnExpression15.Table = table3
            column17.Expression = columnExpression15
            selectQuery2.Columns.Add(column10)
            selectQuery2.Columns.Add(column11)
            selectQuery2.Columns.Add(column12)
            selectQuery2.Columns.Add(column13)
            selectQuery2.Columns.Add(column14)
            selectQuery2.Columns.Add(column15)
            selectQuery2.Columns.Add(column16)
            selectQuery2.Columns.Add(column17)
            selectQuery2.Name = "Products"
            relationColumnInfo1.NestedKeyColumn = "CategoryID"
            relationColumnInfo1.ParentKeyColumn = "CategoryID"
            join1.KeyColumns.Add(relationColumnInfo1)
            join1.Nested = table3
            join1.Parent = table2
            selectQuery2.Relations.Add(join1)
            selectQuery2.Tables.Add(table2)
            selectQuery2.Tables.Add(table3)
            table4.Name = "Suppliers"
            allColumns1.Table = table4
            selectQuery3.Columns.Add(allColumns1)
            selectQuery3.Name = "Suppliers"
            selectQuery3.Tables.Add(table4)
            Me.dsMasterDetail1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {selectQuery1, selectQuery2, selectQuery3})
            masterDetailInfo1.DetailQueryName = "Order Details"
            relationColumnInfo2.NestedKeyColumn = "ProductID"
            relationColumnInfo2.ParentKeyColumn = "ProductID"
            masterDetailInfo1.KeyColumns.Add(relationColumnInfo2)
            masterDetailInfo1.MasterQueryName = "Products"
            masterDetailInfo1.Name = "ProductsOrder_x0020_Details"
            masterDetailInfo2.DetailQueryName = "Products"
            relationColumnInfo3.NestedKeyColumn = "SupplierID"
            relationColumnInfo3.ParentKeyColumn = "SupplierID"
            masterDetailInfo2.KeyColumns.Add(relationColumnInfo3)
            masterDetailInfo2.MasterQueryName = "Suppliers"
            Me.dsMasterDetail1.Relations.AddRange(New DevExpress.DataAccess.Sql.MasterDetailInfo() {masterDetailInfo1, masterDetailInfo2})
            Me.dsMasterDetail1.ResultSchemaSerializable = resources.GetString("dsMasterDetail1.ResultSchemaSerializable")
            Me.xrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CompanyName]")})
            Me.xrLabel1.Font = New System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold)
            Me.xrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(0F, 8F)
            Me.xrLabel1.Name = "xrLabel1"
            Me.xrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrLabel1.SizeF = New System.Drawing.SizeF(400F, 17F)
            Me.xrLabel1.StylePriority.UseFont = False
            Me.xrLabel1.StylePriority.UseTextAlignment = False
            Me.xrLabel1.Text = "Norske Meierier:"
            Me.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
            Me.xrLabel1.TextFormatString = "{0}:"
            Me.xrLabel2.Font = New System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold)
            Me.xrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(567F, 8F)
            Me.xrLabel2.Multiline = True
            Me.xrLabel2.Name = "xrLabel2"
            Me.xrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrLabel2.SizeF = New System.Drawing.SizeF(100F, 17F)
            Me.xrLabel2.StylePriority.UseFont = False
            Me.xrLabel2.StylePriority.UseTextAlignment = False
            Me.xrLabel2.Text = "Total:"
            Me.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
            Me.ReportHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLine1, Me.lbTitle, Me.xrPageInfo2})
            Me.ReportHeader1.HeightF = 92F
            Me.ReportHeader1.Name = "ReportHeader1"
            Me.ReportHeader1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.ReportHeader1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
            Me.xrLine1.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((84)))))), (CInt(((CByte((132)))))), (CInt(((CByte((213)))))))
            Me.xrLine1.LineWidth = 2F
            Me.xrLine1.LocationFloat = New DevExpress.Utils.PointFloat(0F, 81F)
            Me.xrLine1.Name = "xrLine1"
            Me.xrLine1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
            Me.xrLine1.SizeF = New System.Drawing.SizeF(650F, 9F)
            Me.lbTitle.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.lbTitle.Font = New System.Drawing.Font("Tahoma", 18F)
            Me.lbTitle.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((84)))))), (CInt(((CByte((132)))))), (CInt(((CByte((213)))))))
            Me.lbTitle.LocationFloat = New DevExpress.Utils.PointFloat(0F, 42F)
            Me.lbTitle.Name = "lbTitle"
            Me.lbTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.lbTitle.SizeF = New System.Drawing.SizeF(138F, 38F)
            Me.lbTitle.Text = "Suppliers"
            Me.lbTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft
            Me.xrPageInfo2.Font = New System.Drawing.Font("Tahoma", 8F)
            Me.xrPageInfo2.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((84)))))), (CInt(((CByte((132)))))), (CInt(((CByte((213)))))))
            Me.xrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(358F, 58F)
            Me.xrPageInfo2.Name = "xrPageInfo2"
            Me.xrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F)
            Me.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
            Me.xrPageInfo2.SizeF = New System.Drawing.SizeF(292F, 23F)
            Me.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight
            Me.xrPageInfo2.TextFormatString = "{0:""Current Date: "" dddd, dd MMMM yyyy}"
            Me.topMarginBand1.HeightF = 119F
            Me.topMarginBand1.Name = "topMarginBand1"
            Me.ProductData.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((196)))))), (CInt(((CByte((220)))))), (CInt(((CByte((255)))))))
            Me.ProductData.BorderColor = System.Drawing.Color.White
            Me.ProductData.Borders = CType(((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Right) Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.ProductData.BorderWidth = 1F
            Me.ProductData.Font = New System.Drawing.Font("Tahoma", 8.25F)
            Me.ProductData.ForeColor = System.Drawing.Color.Black
            Me.ProductData.Name = "ProductData"
            Me.OddStyle.BackColor = System.Drawing.Color.Transparent
            Me.OddStyle.BorderColor = System.Drawing.Color.FromArgb((CInt(((CByte((232)))))), (CInt(((CByte((205)))))), (CInt(((CByte((162)))))))
            Me.OddStyle.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top)), DevExpress.XtraPrinting.BorderSide)
            Me.OddStyle.BorderWidth = 1F
            Me.OddStyle.Font = New System.Drawing.Font("Tahoma", 8.25F)
            Me.OddStyle.ForeColor = System.Drawing.Color.Black
            Me.OddStyle.Name = "OddStyle"
            Me.GroupHeader.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((243)))))), (CInt(((CByte((243)))))), (CInt(((CByte((243)))))))
            Me.GroupHeader.BorderColor = System.Drawing.Color.White
            Me.GroupHeader.Borders = CType(((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Right) Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.GroupHeader.BorderWidth = 1F
            Me.GroupHeader.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold)
            Me.GroupHeader.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((139)))))), (CInt(((CByte((139)))))), (CInt(((CByte((139)))))))
            Me.GroupHeader.Name = "GroupHeader"
            Me.ProductHeader.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((84)))))), (CInt(((CByte((132)))))), (CInt(((CByte((213)))))))
            Me.ProductHeader.BorderColor = System.Drawing.Color.White
            Me.ProductHeader.Borders = CType(((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Right) Or DevExpress.XtraPrinting.BorderSide.Bottom)), DevExpress.XtraPrinting.BorderSide)
            Me.ProductHeader.BorderWidth = 1F
            Me.ProductHeader.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold)
            Me.ProductHeader.ForeColor = System.Drawing.Color.White
            Me.ProductHeader.Name = "ProductHeader"
            Me.SupplierTitle.BackColor = System.Drawing.Color.Transparent
            Me.SupplierTitle.Borders = DevExpress.XtraPrinting.BorderSide.None
            Me.SupplierTitle.BorderWidth = 1F
            Me.SupplierTitle.Font = New System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold)
            Me.SupplierTitle.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((243)))))), (CInt(((CByte((120)))))), (CInt(((CByte((0)))))))
            Me.SupplierTitle.Name = "SupplierTitle"
            Me.EvenStyle.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((237)))))), (CInt(((CByte((196)))))))
            Me.EvenStyle.BorderColor = System.Drawing.Color.FromArgb((CInt(((CByte((232)))))), (CInt(((CByte((205)))))), (CInt(((CByte((162)))))))
            Me.EvenStyle.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top)), DevExpress.XtraPrinting.BorderSide)
            Me.EvenStyle.BorderWidth = 1F
            Me.EvenStyle.Font = New System.Drawing.Font("Tahoma", 8.25F)
            Me.EvenStyle.ForeColor = System.Drawing.Color.Black
            Me.EvenStyle.Name = "EvenStyle"
            Me.SupplierInfo.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((84)))))), (CInt(((CByte((132)))))), (CInt(((CByte((213)))))))
            Me.SupplierInfo.BorderColor = System.Drawing.Color.White
            Me.SupplierInfo.BorderWidth = 1F
            Me.SupplierInfo.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold)
            Me.SupplierInfo.ForeColor = System.Drawing.Color.White
            Me.SupplierInfo.Name = "SupplierInfo"
            Me.SupplierInfo.Padding = New DevExpress.XtraPrinting.PaddingInfo(7, 0, 0, 0, 100F)
            Me.OrderDetailsTotal.DataMember = "Suppliers.SuppliersProducts.ProductsOrder_x0020_Details"
            Me.OrderDetailsTotal.Expression = "([UnitPrice] * [Quantity]) * (1- [Discount])"
            Me.OrderDetailsTotal.FieldType = DevExpress.XtraReports.UI.FieldType.[Double]
            Me.OrderDetailsTotal.Name = "OrderDetailsTotal"
            Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.BottomMargin, Me.ReportHeader1, Me.DetailReportBand, Me.topMarginBand1})
            Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.OrderDetailsTotal})
            Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.dsMasterDetail1})
            Me.DataMember = "Suppliers"
            Me.DataSource = Me.dsMasterDetail1
            Me.DisplayName = "Suppliers"
            Me.Margins = New System.Drawing.Printing.Margins(100, 80, 119, 142)
            Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.ProductData, Me.OddStyle, Me.GroupHeader, Me.ProductHeader, Me.SupplierTitle, Me.EvenStyle, Me.SupplierInfo})
            Me.Version = "20.2"
            CType((Me.xrTable1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.xrTable4), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.xrTable7), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.xrTable6), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.xrTable3), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.xrTable2), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.xrTable5), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me), System.ComponentModel.ISupportInitialize).EndInit()
        End Sub

#End Region
        Private Detail As DevExpress.XtraReports.UI.DetailBand

        Private xrTable1 As DevExpress.XtraReports.UI.XRTable

        Private xrTableRow1 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell3 As DevExpress.XtraReports.UI.XRTableCell

        Private cellCompanyName As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow5 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell17 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow7 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell29 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell30 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell38 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell31 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow10 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell39 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell40 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell43 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell44 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow11 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell45 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell46 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell47 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell48 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow12 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell49 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell50 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell51 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell52 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow13 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell53 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell54 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow16 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell6 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell57 As DevExpress.XtraReports.UI.XRTableCell

        Private BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand

        Private xrPictureBox4 As DevExpress.XtraReports.UI.XRPictureBox

        Private xrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo

        Private xrPageInfo3 As DevExpress.XtraReports.UI.XRPageInfo

        Private DetailReportBand As DevExpress.XtraReports.UI.DetailReportBand

        Private Detail1 As DevExpress.XtraReports.UI.DetailBand

        Private xrTable4 As DevExpress.XtraReports.UI.XRTable

        Private xrTableRow14 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell55 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell11 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell12 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell13 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell14 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell15 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableRow6 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell56 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell24 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell25 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell26 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell27 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell28 As DevExpress.XtraReports.UI.XRTableCell

        Private xrCheckBox1 As DevExpress.XtraReports.UI.XRCheckBox

        Private DetailReport2 As DevExpress.XtraReports.UI.DetailReportBand

        Private Detail4 As DevExpress.XtraReports.UI.DetailBand

        Private xrTable7 As DevExpress.XtraReports.UI.XRTable

        Private xrTableRow9 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell18 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell35 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell36 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell42 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell37 As DevExpress.XtraReports.UI.XRTableCell

        Private ReportHeader3 As DevExpress.XtraReports.UI.ReportHeaderBand

        Private xrTable6 As DevExpress.XtraReports.UI.XRTable

        Private xrTableRow8 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell16 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell32 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell33 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell41 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell34 As DevExpress.XtraReports.UI.XRTableCell

        Private GroupFooter2 As DevExpress.XtraReports.UI.GroupFooterBand

        Private xrTable3 As DevExpress.XtraReports.UI.XRTable

        Private xrTableRow15 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell20 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell21 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell22 As DevExpress.XtraReports.UI.XRTableCell

        Private GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand

        Private xrTable2 As DevExpress.XtraReports.UI.XRTable

        Private xrTableRow3 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell19 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell5 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell7 As DevExpress.XtraReports.UI.XRTableCell

        Private ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand

        Private xrPageBreak1 As DevExpress.XtraReports.UI.XRPageBreak

        Private xrTable5 As DevExpress.XtraReports.UI.XRTable

        Private xrTableRow4 As DevExpress.XtraReports.UI.XRTableRow

        Private xrTableCell9 As DevExpress.XtraReports.UI.XRTableCell

        Private xrTableCell23 As DevExpress.XtraReports.UI.XRTableCell

        Private dsMasterDetail1 As DevExpress.DataAccess.Sql.SqlDataSource

        Private xrLabel1 As DevExpress.XtraReports.UI.XRLabel

        Private xrLabel2 As DevExpress.XtraReports.UI.XRLabel

        Private ReportHeader1 As DevExpress.XtraReports.UI.ReportHeaderBand

        Private xrLine1 As DevExpress.XtraReports.UI.XRLine

        Private lbTitle As DevExpress.XtraReports.UI.XRLabel

        Private xrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo

        Private topMarginBand1 As DevExpress.XtraReports.UI.TopMarginBand

        Private ProductData As DevExpress.XtraReports.UI.XRControlStyle

        Private OddStyle As DevExpress.XtraReports.UI.XRControlStyle

        Private GroupHeader As DevExpress.XtraReports.UI.XRControlStyle

        Private ProductHeader As DevExpress.XtraReports.UI.XRControlStyle

        Private SupplierTitle As DevExpress.XtraReports.UI.XRControlStyle

        Private EvenStyle As DevExpress.XtraReports.UI.XRControlStyle

        Private SupplierInfo As DevExpress.XtraReports.UI.XRControlStyle

        Private OrderDetailsTotal As DevExpress.XtraReports.UI.CalculatedField
    End Class
End Namespace
