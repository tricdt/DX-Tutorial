Imports DevExpress.Xpf.Editors
Imports DevExpress.XtraPrinting.BarCode.Native
Imports System
Imports System.Windows.Data

Namespace EditorsDemo

    Public Class BarCodeSymbologyCoverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return BarCodeStyleSettingsStorage.Create(CType(value, BarCodeSymbology))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return CType(value, BarCodeStyleSettings).GeneratorBase.SymbologyCode
        End Function
    End Class
End Namespace
