Imports System.Collections.Generic
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Class PointSeriesViewViewModel

        Public Shared Function Create() As PointSeriesViewViewModel
            Return ViewModelSource.Create(Function() New PointSeriesViewViewModel())
        End Function

        Private ReadOnly models As List(Of Marker3DPointModel) = New List(Of Marker3DPointModel) From {New Marker3DCubePointModel(), New Marker3DSpherePointModel()}

        Public ReadOnly Property PointModels As List(Of Marker3DPointModel)
            Get
                Return models
            End Get
        End Property

        Public Overridable Property SelectedPointModel As Marker3DPointModel

        Public Overridable Property SelectedDetalizationLevel As DetalizationLevel

        Public Overridable Property MarkerSize As Double

        Public Overridable Property IsDetalizationLevelEnabled As Boolean

        Protected Sub New()
            SelectedPointModel = models(1)
            SelectedDetalizationLevel = DetalizationLevel.Normal
            MarkerSize = 30
        End Sub

        Protected Sub OnSelectedDetalizationLevelChanged()
            Dim sphereModel As Marker3DSpherePointModel = TryCast(SelectedPointModel, Marker3DSpherePointModel)
            If sphereModel IsNot Nothing Then sphereModel.SphereDetalizationLevel = SelectedDetalizationLevel
        End Sub

        Protected Sub OnSelectedPointModelChanged()
            If TypeOf SelectedPointModel Is Marker3DSpherePointModel Then
                IsDetalizationLevelEnabled = True
            Else
                IsDetalizationLevelEnabled = False
            End If
        End Sub
    End Class
End Namespace
