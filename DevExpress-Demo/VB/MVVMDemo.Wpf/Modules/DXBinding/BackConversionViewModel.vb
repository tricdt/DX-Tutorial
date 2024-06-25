Namespace MVVMDemo.DXBindingDemo

    Public Class BackConversionViewModel

        Public Overridable Property IsEnabled As Boolean

        Public Overridable Property FirstName As String

        Public Overridable Property LastName As String

        Protected Sub New()
            FirstName = "Gregory"
            LastName = "Price"
        End Sub
    End Class
End Namespace
