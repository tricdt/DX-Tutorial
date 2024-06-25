Imports DevExpress.Mvvm

Namespace MVVMDemo.ViewModelsInteraction

    Public Class DetailPOCOViewModel
        Implements ISupportParameter

        Public Overridable Property DetailInfo As String

        Private _Parameter As Object

        Private Property Parameter As Object Implements ISupportParameter.Parameter
            Get
                Return _Parameter
            End Get

            Set(ByVal value As Object)
                _Parameter = value
                DetailInfo = String.Format("Detail for {0} item", _Parameter)
            End Set
        End Property
    End Class
End Namespace
