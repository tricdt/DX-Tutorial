Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Runtime.CompilerServices

Namespace MVVMDemo.Services

    Public Class MessageBoxButtonToMessageBoxResultsConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is MessageButton) Then Return Nothing
            Dim buttons = CType(value, MessageButton)
            Return buttons.ToMessageResults()
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class TimeSpanToStringConverter
        Implements IValueConverter

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim seconds As Integer
            If TypeOf value Is String AndAlso Integer.TryParse(CStr(value), seconds) Then Return TimeSpan.FromSeconds(seconds)
            Return Nothing
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is TimeSpan Then Return $"{value:%s}"
            Return Nothing
        End Function
    End Class

    Public Module Helpers

        <Extension()>
        Public Function ToMessageResults(ByVal buttons As MessageButton) As List(Of MessageResult)
            Dim resultButtons = New List(Of MessageResult)()
            Select Case buttons
                Case MessageButton.OK
                    resultButtons.Add(MessageResult.OK)
                Case MessageButton.OKCancel
                    resultButtons.Add(MessageResult.OK)
                    resultButtons.Add(MessageResult.Cancel)
                Case MessageButton.YesNoCancel
                    resultButtons.Add(MessageResult.Yes)
                    resultButtons.Add(MessageResult.No)
                    resultButtons.Add(MessageResult.Cancel)
                Case MessageButton.YesNo
                    resultButtons.Add(MessageResult.Yes)
                    resultButtons.Add(MessageResult.No)
            End Select

            Return resultButtons
        End Function

        Public Function GeneratePredefinedFormat(ByVal defaultButton As MessageResult, ByVal timeout As TimeSpan) As List(Of PredefinedFormat)
            Dim formats = New List(Of PredefinedFormat)()
            formats.Add(New PredefinedFormat($"{defaultButton} ({timeout:%s} sec.)", "{0} ({1:%s} sec.)"))
            formats.Add(New PredefinedFormat($"{defaultButton} {timeout:%s} sec.", "{0} {1:%s} sec."))
            formats.Add(New PredefinedFormat($"{defaultButton} {timeout:%s} secons to close", "{0} {1:%s} secons to close"))
            formats.Add(New PredefinedFormat($"{defaultButton} close after {timeout:%s} sec.", "{0} close after {1:%s} sec."))
            Return formats
        End Function
    End Module
End Namespace
