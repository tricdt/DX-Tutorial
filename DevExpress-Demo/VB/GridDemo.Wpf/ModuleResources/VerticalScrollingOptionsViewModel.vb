Imports DevExpress.Mvvm

Namespace GridDemo

    Public Class VerticalScrollingOptionsViewModel
        Inherits BindableBase

        Private selectedDefinitionField As GridControlDefinition

        Public Property SelectedDefinition As GridControlDefinition
            Get
                Return selectedDefinitionField
            End Get

            Set(ByVal value As GridControlDefinition)
                SetProperty(selectedDefinitionField, value, Function() SelectedDefinition)
            End Set
        End Property

        Private controlDefinitionsField As GridControlDefinitionCollection

        Public Property ControlDefinitions As GridControlDefinitionCollection
            Get
                Return controlDefinitionsField
            End Get

            Set(ByVal value As GridControlDefinitionCollection)
                controlDefinitionsField = value
                If ControlDefinitions IsNot Nothing AndAlso ControlDefinitions.Count > 0 Then SelectedDefinition = ControlDefinitions(0)
            End Set
        End Property
    End Class
End Namespace
