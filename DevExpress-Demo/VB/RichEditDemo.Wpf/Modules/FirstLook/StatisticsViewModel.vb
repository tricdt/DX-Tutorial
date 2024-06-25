Imports DevExpress.Mvvm.POCO
Imports System

Namespace RichEditDemo

    Public Class StatisticsViewModel

        Public Shared Function Create(ByVal includeTextboxes As Boolean, ByVal getsStatisctics As Func(Of Boolean, StatisticsData)) As StatisticsViewModel
            Return ViewModelSource.Create(Function() New StatisticsViewModel(includeTextboxes, getsStatisctics))
        End Function

        Protected Sub New(ByVal includeTextboxes As Boolean, ByVal getsStatisctics As Func(Of Boolean, StatisticsData))
            Me.getsStatisctics = getsStatisctics
            Me.IncludeTextboxes = includeTextboxes
        End Sub

        Private ReadOnly getsStatisctics As Func(Of Boolean, StatisticsData)

        Public Overridable Property Statistics As StatisticsData

        Private includeTextboxesField As Boolean

        Public Overridable Property IncludeTextboxes As Boolean
            Get
                Return includeTextboxesField
            End Get

            Set(ByVal value As Boolean)
                includeTextboxesField = value
                Statistics = getsStatisctics(IncludeTextboxes)
            End Set
        End Property
    End Class

    Public Class StatisticsData

        Private _NoSpacesCharacterCount As Integer, _WithSpacesCharacterCount As Integer, _WordCount As Integer, _ParagraphCount As Integer

        Public Sub New(ByVal noSpacesCharacterCount As Integer, ByVal withSpacesCharacterCount As Integer, ByVal wordCount As Integer, ByVal paragraphCount As Integer)
            Me.NoSpacesCharacterCount = noSpacesCharacterCount
            Me.WithSpacesCharacterCount = withSpacesCharacterCount
            Me.WordCount = wordCount
            Me.ParagraphCount = paragraphCount
        End Sub

        Public Property NoSpacesCharacterCount As Integer
            Get
                Return _NoSpacesCharacterCount
            End Get

            Private Set(ByVal value As Integer)
                _NoSpacesCharacterCount = value
            End Set
        End Property

        Public Property WithSpacesCharacterCount As Integer
            Get
                Return _WithSpacesCharacterCount
            End Get

            Private Set(ByVal value As Integer)
                _WithSpacesCharacterCount = value
            End Set
        End Property

        Public Property WordCount As Integer
            Get
                Return _WordCount
            End Get

            Private Set(ByVal value As Integer)
                _WordCount = value
            End Set
        End Property

        Public Property ParagraphCount As Integer
            Get
                Return _ParagraphCount
            End Get

            Private Set(ByVal value As Integer)
                _ParagraphCount = value
            End Set
        End Property
    End Class
End Namespace
