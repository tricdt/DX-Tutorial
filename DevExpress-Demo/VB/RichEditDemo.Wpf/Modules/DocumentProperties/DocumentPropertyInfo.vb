Imports DevExpress.Mvvm

Namespace RichEditDemo

    Public Class DocumentPropertyInfo
        Inherits BindableBase

        Private _DisplayName As String, _Name As String

        Public Sub New(ByVal displayName As String, ByVal Optional name As String = Nothing)
            Me.DisplayName = displayName
            Me.Name = If(name, displayName.ToUpperInvariant())
        End Sub

        Public Property DisplayName As String
            Get
                Return _DisplayName
            End Get

            Private Set(ByVal value As String)
                _DisplayName = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property
    End Class
End Namespace
