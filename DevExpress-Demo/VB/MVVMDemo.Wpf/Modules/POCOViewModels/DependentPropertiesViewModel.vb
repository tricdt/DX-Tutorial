Imports DevExpress.Mvvm.DataAnnotations

Namespace MVVMDemo.POCOViewModels

    Public Class DependentPropertiesViewModel

        Public Overridable Property FirstName As String

        Public Overridable Property LastName As String

        <DependsOnProperties("FirstName", "LastName")>
        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
    End Class
End Namespace
