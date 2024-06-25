Namespace DevExpress.DevAV.ViewModels

    Partial Class EvaluationViewModel

        Protected Overrides Function CreateEntity() As Evaluation
            Dim entity As Evaluation = MyBase.CreateEntity()
            entity.CreatedOn = Date.Now
            Return entity
        End Function
    End Class
End Namespace
