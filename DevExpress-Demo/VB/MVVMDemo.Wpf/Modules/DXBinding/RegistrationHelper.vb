Namespace MVVMDemo.DXBindingDemo

    Public Module RegistrationHelper

        Public Function CanRegister(ByVal userName As String, ByVal acceptTerms As Boolean) As Boolean
            Return Not String.IsNullOrEmpty(userName) AndAlso acceptTerms
        End Function
    End Module
End Namespace
