Imports DevExpress.Mvvm.DataAnnotations
Imports System.Collections.Generic

Namespace TreeListDemo

    Public Class SmartColumnsGenerationViewModel

        Private _Items As List(Of System.Collections.Generic.List(Of Object))

        Public Sub New()
            Items = New List(Of List(Of Object))()
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement1), 100))
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement1_FluentAPI), 100))
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement2), 100))
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement2_FluentAPI), 100))
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement3), 100))
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement3_FluentAPI), 100))
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement4), 100))
            Items.Add(DataGenerator.GetObjects(GetType(DataAnnotationsElement4_FluentAPI), 100))
            CurrentItemsSource = Items(0)
        End Sub

        Public Property Items As List(Of List(Of Object))
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of List(Of Object)))
                _Items = value
            End Set
        End Property

        <BindableProperty(True, OnPropertyChangedMethodName:="CheckIsSmallObject")>
        Public Overridable Property CurrentItemsSource As List(Of Object)

        Public Overridable Property IsSmallObject As Boolean

        Protected Sub CheckIsSmallObject()
            IsSmallObject = CurrentItemsSource(0).GetType().GetProperties().Length <= 10
        End Sub
    End Class

    Public Enum Gender
        Male
        Female
    End Enum
End Namespace
