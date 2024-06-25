Imports System.Windows

Namespace MapDemo

    Public Class PuzzleGameInfoControl
        Inherits VisibleControl

        Public Shared ReadOnly SolvedCountriesCountProperty As DependencyProperty = DependencyProperty.Register("SolvedCountriesCount", GetType(Integer), GetType(PuzzleGameInfoControl), New PropertyMetadata(0))

        Public Property SolvedCountriesCount As Integer
            Get
                Return CInt(GetValue(SolvedCountriesCountProperty))
            End Get

            Set(ByVal value As Integer)
                SetValue(SolvedCountriesCountProperty, value)
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(PuzzleGameInfoControl)
        End Sub
    End Class
End Namespace
