Imports System.Windows

Namespace MVVMDemo.Behaviors

    Public Class KeyToCommandViewModel

        Private _Persons As MVVMDemo.PersonInfo()

        Public Property Persons As PersonInfo()
            Get
                Return _Persons
            End Get

            Private Set(ByVal value As PersonInfo())
                _Persons = value
            End Set
        End Property

        Protected Sub New()
            Persons = PersonInfo.Persons
        End Sub

        Public Sub ShowPersonDetail(ByVal person As PersonInfo)
            If person IsNot Nothing Then MessageBox.Show(person.FullName, "Detail info")
        End Sub
    End Class
End Namespace
