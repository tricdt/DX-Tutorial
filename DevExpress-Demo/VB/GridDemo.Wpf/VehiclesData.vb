Imports DevExpress.DemoData.Models.Vehicles
#If DXCORE3
using Microsoft.EntityFrameworkCore;
#Else
Imports System.Data.Entity
#End If
Imports System.Collections.Generic
Imports System.Linq
Imports VehicleModel = DevExpress.DemoData.Models.Vehicles.Model

Namespace DevExpress.DemoData

    Public Class VehiclesData

        Private context As VehiclesContext = New VehiclesContext()

        Private modelsField As IEnumerable(Of VehicleModel)

        Public ReadOnly Property Models As IEnumerable(Of VehicleModel)
            Get
                If modelsField Is Nothing Then
                    context.Models.Load()
                    context.Trademarks.Load()
                    modelsField = context.Models.Local.ToList()
                End If

                Return modelsField
            End Get
        End Property
    End Class
End Namespace
