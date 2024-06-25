Imports System
Imports System.Windows.Controls
Imports System.Windows.Data
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Grid

Namespace TreeListDemo

    Public Partial Class MultiSelection
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            treeList.SelectRange(0, 10)
        End Sub

        Protected Overrides Sub Show()
            MyBase.Show()
            treeList.UpdateTotalSummary()
        End Sub
    End Class

#Region "Converters"
    Public Class MultiSelectModeToBoolConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return Not Equals(value, MultiSelectMode.None)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(CBool(value), MultiSelectMode.Row, MultiSelectMode.None)
        End Function
#End Region
    End Class
#End Region
End Namespace
