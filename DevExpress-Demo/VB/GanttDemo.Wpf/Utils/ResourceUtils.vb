Imports System
Imports System.IO
Imports System.Windows

Namespace GanttDemo

    Public Module ResourceUtils

        Public Function GetResourceStream(ByVal resourceName As String) As Stream
            Return Application.GetResourceStream(New Uri("/GanttDemo;component\" & resourceName, UriKind.RelativeOrAbsolute)).Stream
        End Function
    End Module
End Namespace
