Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports System.Windows

Namespace MVVMDemo.POCOViewModels

    Public Class ChangeNotificationsViewModel

        <BindableProperty(OnPropertyChangedMethodName:="OnNameChanged")>
        Public Overridable Property FirstName As String

        <BindableProperty(OnPropertyChangedMethodName:="OnNameChanged")>
        Public Overridable Property LastName As String

        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property

        Protected Sub OnNameChanged()
            RaisePropertyChanged(Function(x) x.FullName)
            RaiseCanExecuteChanged(Sub(x) x.Register())
        End Sub

        <Command(UseCommandManager:=False)>
        Public Sub Register()
            MessageBox.Show("Registered")
        End Sub

        Public Function CanRegister() As Boolean
            Return Not String.IsNullOrEmpty(FirstName) AndAlso Not String.IsNullOrEmpty(LastName)
        End Function
    End Class
End Namespace
