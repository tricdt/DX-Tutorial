Imports System.Linq
Imports System.IO

Namespace DevExpress.DevAV.ViewModels

    Partial Class ProductViewModel

        Private Shared ZoomFactors As Double() = New Double() {0.5, 0.6, 0.7, 0.8, 0.9, 1, 2, 3, 4, 5}

        Private zoomFactorIndex As Integer = 5

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            ZoomFactor = 1
        End Sub

        Public Overridable Property PdfDocument As Stream

        Public Overridable Property ZoomFactor As Double

        Public Overridable Sub ZoomIn()
            If zoomFactorIndex <> ZoomFactors.Count() - 1 Then zoomFactorIndex += 1
            ZoomFactor = ZoomFactors(zoomFactorIndex)
        End Sub

        Public Overridable Sub ZoomOut()
            If zoomFactorIndex <> 0 Then zoomFactorIndex -= 1
            ZoomFactor = ZoomFactors(zoomFactorIndex)
        End Sub

        Protected Overrides Function CreateEntity() As Product
            Dim entity As Product = MyBase.CreateEntity()
            entity.ProductionStart = Date.Now
            entity.CurrentInventory = 1
            Return entity
        End Function

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            PdfDocument = If(Entity IsNot Nothing AndAlso Entity.Catalog IsNot Nothing AndAlso Entity.Catalog.Count <> 0, Entity.Catalog(0).PdfStream, Nothing)
            Log("HybridApp: View Product")
        End Sub
    End Class
End Namespace
