Namespace EditorsDemo.ModuleResources

    Public Class CardInfo

        Private _Name As String, _Template As String

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Template As String
            Get
                Return _Template
            End Get

            Private Set(ByVal value As String)
                _Template = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal template As String)
            Me.Name = name
            Me.Template = template
        End Sub
    End Class
End Namespace
