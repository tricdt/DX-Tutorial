Imports DevExpress.DemoData
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.PivotGrid
Imports System.Collections.Generic

Namespace PivotGridDemo

    Public Class MVVMPivotViewModel
        Inherits ViewModelBase

        Private _DataSource As Object, _FormatConditionsSource As IEnumerable(Of PivotGridDemo.FormatConditionItem), _GroupsSource As IEnumerable(Of String), _FieldsSource As IEnumerable(Of PivotGridDemo.FieldItem)

        Public Sub New()
            FormatConditionsSource = New List(Of FormatConditionItem) From {New FormatConditionItem With {.Type = FormatConditionType.DataBar, .MeasureName = "fieldModelPrice", .ColumnName = "fieldYear", .RowName = "fieldName", .PredefinedFormatName = "BlueGradientDataBar"}, New FormatConditionItem With {.Type = FormatConditionType.DataBar, .MeasureName = "fieldModelPrice", .ColumnName = "fieldMonth", .RowName = "fieldName", .PredefinedFormatName = "BlueGradientDataBar"}, New FormatConditionItem With {.Type = FormatConditionType.DataBar, .MeasureName = "fieldModelPrice", .ColumnName = "fieldDay", .RowName = "fieldName", .PredefinedFormatName = "BlueGradientDataBar"}, New FormatConditionItem With {.Type = FormatConditionType.DataBar, .MeasureName = "fieldModelPrice", .ColumnName = "fieldYear", .RowName = "fieldTrademark", .PredefinedFormatName = "OrangeGradientDataBar"}, New FormatConditionItem With {.Type = FormatConditionType.DataBar, .MeasureName = "fieldModelPrice", .ColumnName = "fieldMonth", .RowName = "fieldTrademark", .PredefinedFormatName = "OrangeGradientDataBar"}, New FormatConditionItem With {.Type = FormatConditionType.DataBar, .MeasureName = "fieldModelPrice", .ColumnName = "fieldDay", .RowName = "fieldTrademark", .PredefinedFormatName = "OrangeGradientDataBar"}}
            GroupsSource = New List(Of String) From {"PivotGridGroup1"}
            FieldsSource = New List(Of FieldItem) From {New FieldItem With {.Name = "fieldTrademark", .DataBinding = New DataSourceColumnBinding("Trademark"), .Area = FieldArea.RowArea, .Caption = "Trademark", .Width = 140R}, New FieldItem With {.Name = "fieldName", .DataBinding = New DataSourceColumnBinding("Name"), .Area = FieldArea.RowArea, .Caption = "Name", .Width = 140R}, New FieldItem With {.Name = "fieldYear", .DataBinding = New DataSourceColumnBinding("SalesDate", FieldGroupInterval.DateYear), .Area = FieldArea.ColumnArea, .Caption = "Year", .Width = 140R, .GroupName = "PivotGridGroup1"}, New FieldItem With {.Name = "fieldMonth", .DataBinding = New DataSourceColumnBinding("SalesDate", FieldGroupInterval.DateMonth), .Area = FieldArea.ColumnArea, .Caption = "Month", .Width = 140R, .GroupName = "PivotGridGroup1"}, New FieldItem With {.Name = "fieldDay", .DataBinding = New DataSourceColumnBinding("SalesDate", FieldGroupInterval.DateDay), .Area = FieldArea.ColumnArea, .Caption = "Day", .Width = 140R, .GroupName = "PivotGridGroup1"}, New FieldItem With {.Name = "fieldModelPrice", .DataBinding = New DataSourceColumnBinding("ModelPrice"), .Area = FieldArea.DataArea, .Caption = "Extended Price", .Width = 140R}, New FieldItem With {.Name = "fieldModification", .DataBinding = New DataSourceColumnBinding("Modification"), .Area = FieldArea.FilterArea, .Caption = "Modification", .Width = 140R}, New FieldItem With {.Name = "fieldBodyStyle", .DataBinding = New DataSourceColumnBinding("BodyStyle"), .Area = FieldArea.FilterArea, .Caption = "Body Style", .Width = 140R}, New FieldItem With {.Name = "fieldSalesDate", .DataBinding = New DataSourceColumnBinding("SalesDate", FieldGroupInterval.Date), .Area = FieldArea.FilterArea, .Caption = "Sales Date", .Width = 140R}, New FieldItem With {.Name = "fieldModelPrice1", .DataBinding = New DataSourceColumnBinding("ModelPrice"), .Area = FieldArea.FilterArea, .Caption = "Model Price", .Width = 140R}, New FieldItem With {.Name = "fieldMPGCity", .DataBinding = New DataSourceColumnBinding("MPGCity"), .Area = FieldArea.FilterArea, .Caption = "MPG City", .Width = 140R}, New FieldItem With {.Name = "fieldMPGHighway", .DataBinding = New DataSourceColumnBinding("MPGHighway"), .Area = FieldArea.FilterArea, .Caption = "MPG Highway", .Width = 140R}}
            DataSource = VehiclesData.GetMDBData()
        End Sub

        Public Property DataSource As Object
            Get
                Return _DataSource
            End Get

            Private Set(ByVal value As Object)
                _DataSource = value
            End Set
        End Property

        Public Property FormatConditionsSource As IEnumerable(Of FormatConditionItem)
            Get
                Return _FormatConditionsSource
            End Get

            Private Set(ByVal value As IEnumerable(Of FormatConditionItem))
                _FormatConditionsSource = value
            End Set
        End Property

        Public Property GroupsSource As IEnumerable(Of String)
            Get
                Return _GroupsSource
            End Get

            Private Set(ByVal value As IEnumerable(Of String))
                _GroupsSource = value
            End Set
        End Property

        Public Property FieldsSource As IEnumerable(Of FieldItem)
            Get
                Return _FieldsSource
            End Get

            Private Set(ByVal value As IEnumerable(Of FieldItem))
                _FieldsSource = value
            End Set
        End Property
    End Class
End Namespace
