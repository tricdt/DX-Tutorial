Imports System
Imports System.Globalization
Imports DevExpress.Xpf.Core

Namespace ChartsDemo

    Friend Class OptionsConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing Then Return Nothing
            Dim tabItem As DXTabItem = CType(value, DXTabItem)
            Dim tabModule As TabItemModule = CType(tabItem.Content, TabItemModule)
            If tabModule.Options Is Nothing Then Return Nothing
            tabModule.Options.DataContext = tabModule.DataContext
            Return tabModule.Options
        End Function
    End Class
End Namespace
