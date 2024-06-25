Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Markup
Imports DevExpress.Utils
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo

    Public Class GaugeModelsSource
        Inherits MarkupExtension

        Public Property PredefinedModels As IEnumerable(Of PredefinedElementKind)

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return PredefinedModels.[Select](Function(x) New GaugeModelItem(x)).ToList()
        End Function
    End Class

    Public Class GaugeModelItem
        Inherits ImmutableObject

        Private ReadOnly modelKind As PredefinedElementKind

        Public ReadOnly Property Model As Object
            Get
                Return Activator.CreateInstance(modelKind.Type)
            End Get
        End Property

        Public ReadOnly Property Name As String
            Get
                Return modelKind.Name
            End Get
        End Property

        Public Sub New(ByVal modelKind As PredefinedElementKind)
            Me.modelKind = modelKind
        End Sub

        Public Overrides Function ToString() As String
            Return modelKind.Name
        End Function
    End Class
End Namespace
