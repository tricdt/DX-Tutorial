Namespace MVVMDemo.POCOViewModels

    Public Class PropertiesViewModel

        Public Overridable Property UserName As String

        Protected Sub OnUserNameChanged(ByVal oldValue As String)
            ChangedStatus = String.Format("Old value: '{0}' New value: '{1}'", oldValue, UserName)
        End Sub

        Public Overridable Property ChangedStatus As String
    End Class
End Namespace
