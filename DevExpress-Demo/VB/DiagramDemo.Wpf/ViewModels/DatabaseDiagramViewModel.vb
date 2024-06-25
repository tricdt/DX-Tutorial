Imports DevExpress.Diagram.Demos
Imports DevExpress.Mvvm

Namespace DiagramDemo

    Public Class DatabaseDiagramViewModel
        Inherits ViewModelBase

        Private _Database As DatabaseDefinition

        Public Property Database As DatabaseDefinition
            Get
                Return _Database
            End Get

            Private Set(ByVal value As DatabaseDefinition)
                _Database = value
            End Set
        End Property

        Public Sub New()
            Database = GetDatabaseDefinition()
        End Sub
    End Class
End Namespace
