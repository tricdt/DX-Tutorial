Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace EditorsDemo

    <CodeFile("ViewModels/BrowsePathEditModuleViewModel.(cs)")>
    Public Partial Class BrowsePathEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class PngSizeToTextConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim intValue = CInt(value)
            If intValue = 0 Then Return String.Empty
            Return String.Format("PNG ({0} kb.)", intValue)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
