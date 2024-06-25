Imports DevExpress.Internal
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Runtime.InteropServices

#If DXCORE3
using Microsoft.EntityFrameworkCore;
#End If
Namespace DevExpress.DemoData

    Public Class VehiclesData

        Public Shared Function GetMDBData() As List(Of OrderItem)
#If DXCORE3
            return InitOrdersData(null, 10000, 3 * 365);
#Else
            Return InitOrdersData("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataDirectoryHelper.GetFile("Vehicles.mdb", DataDirectoryHelper.DataFolderName), 10000, 3 * 365)
#End If
        End Function

        Public Class Trademark

            Public Property ID As Integer

            Public Property Name As String
        End Class

        Public Class BodyStyle

            Public Property ID As Integer

            Public Property Name As String
        End Class

        Public Class OrderItem

            Friend Model As Model

            Public Sub New(ByVal model As Model, ByVal days As Integer, ByVal rnd As Random, ByVal id As Integer)
                Me.Model = model
                ModelPrice = model.Price
                Trademark = model.Trademark
                Name = model.Name
                Modification = model.Modification
                Category = model.Category
                MPGCity = model.MPGCity
                MPGHighway = model.MPGHighway
                Doors = model.Doors
                BodyStyle = model.BodyStyle.ToString()
                Cylinders = model.Cylinders
                Horsepower = model.Horsepower
                Torque = model.Torque
                TransmissionSpeeds = model.TransmissionSpeeds
                TransmissionType = model.TransmissionType
                InStock = model.InStock
                SalesDate = Date.Now.AddDays(-rnd.Next(days))
                Discount = Math.Round(0.05 * rnd.Next(4), 2)
                OrderID = id
            End Sub

            Public Property OrderID As Integer

            Public Property SalesDate As Date

            Public Property Discount As Double

            Public Property ModelPrice As Decimal

            Public Property Trademark As String

            Public Property Name As String

            Public Property Modification As String

            Public Property Category As Integer

            Public Property MPGCity As Integer?

            Public Property MPGHighway As Integer?

            Public Property Doors As Integer

            Public Property BodyStyle As String

            Public Property Cylinders As Integer

            Public Property Horsepower As String

            Public Property Torque As String

            Public Property TransmissionSpeeds As Integer

            Public Property TransmissionType As Integer

            Public Property InStock As Boolean
        End Class

        Public Class Model

            Public Property ID As Integer

            Public Property Trademark As String

            Public Property Name As String

            Public Property Modification As String

            Public Property Category As Integer

            Public Property Price As Decimal

            Public Property MPGCity As Integer?

            Public Property MPGHighway As Integer?

            Public Property Doors As Integer

            Public Property BodyStyle As String

            Public Property Cylinders As Integer

            Public Property Horsepower As String

            Public Property Torque As String

            Public Property TransmissionSpeeds As Integer

            Public Property TransmissionType As Integer

            Public Property Description As String

            Public Property DeliveryDate As Date

            Public Property InStock As Boolean
        End Class

        Public Shared Function InitOrdersData(ByVal connectionString As String, ByVal itemCount As Integer, ByVal dataInterval As Integer) As List(Of OrderItem)
            Dim rnd As Random = New Random()
#If DXCORE3
            List<Model> listModels = InitDataCore(1);
#Else
            Dim ds As DataSet
            Dim listModels As List(Of Model) = InitMDBDataCore(connectionString, ds, 1)
#End If
            Dim orders As List(Of OrderItem) = New List(Of OrderItem)()
            Dim averagePrice As Decimal = Enumerable.Select(Of Model, Global.System.[Decimal])(listModels, CType(Function(x) CDec(x.Price), Func(Of Model, Decimal))).Average()
            Dim averageOrders As Integer = itemCount \ listModels.Count
            For Each model As Model In listModels
                For i As Integer = 0 To averageOrders * averagePrice / model.Price - 1
                    orders.Add(New OrderItem(model, dataInterval, rnd, i + 1))
                Next
            Next

            Return orders
        End Function

#If DXCORE3
        static List<Model> InitDataCore(int dataInterval) {
            var rnd = new Random();
            var listModels = new List<Model>();
            
            var vehiclesContext = new Models.Vehicles.VehiclesContext();
            vehiclesContext.Models.Load();
            vehiclesContext.BodyStyles.Load();
            vehiclesContext.Categories.Load();
            vehiclesContext.Trademarks.Load();
            vehiclesContext.TransmissionTypes.Load();

            vehiclesContext.Models.Local.ToList().ForEach(source => listModels.Add(new Model() {
                ID = (int)source.ID,
                Trademark = source.TrademarkName,
                Name = source.Name,
                Modification = source.Modification,
                Category = (int)source.CategoryID,
                Price = source.Price,
                MPGCity = source.MPGCity,
                MPGHighway = source.MPGHighway,
                Doors = source.Doors,
                BodyStyle = source.BodyStyleName,
                Cylinders = source.Cylinders,
                Horsepower = source.Horsepower,
                Torque = source.Torque,
                TransmissionSpeeds = Convert.ToInt32(source.TransmissionSpeeds),
                TransmissionType = (int)source.TransmissionTypeID,
                Description = source.Description,
                DeliveryDate = DateTime.Now.AddDays(rnd.Next(dataInterval)),
                InStock = rnd.Next(100) < 95
            }));
            return listModels;
        }
#Else
        Private Shared Function InitMDBDataCore(ByVal connectionString As String, <Out> ByRef ds As DataSet, ByVal dataInterval As Integer) As List(Of Model)
            Dim Model As String = "Model"
            Dim Trademark As String = "Trademark"
            Dim Category As String = "Category"
            Dim BodyStyle As String = "BodyStyle"
            Dim TransmissionType As String = "TransmissionType"
            ds = New DataSet()
            FillTable(Model, Nothing, connectionString, ds)
            FillTable(Category, Nothing, connectionString, ds)
            FillTable(Trademark, Nothing, connectionString, ds)
            FillTable(BodyStyle, Nothing, connectionString, ds)
            FillTable(TransmissionType, Nothing, connectionString, ds)
            Dim listTrademarks As List(Of Trademark) = New List(Of Trademark)()
            For Each row As DataRow In ds.Tables(Trademark).Rows
                listTrademarks.Add(New Trademark With {.ID = CInt(row("ID")), .Name = CStr(row("Name"))})
            Next

            Dim listBodyStyles As List(Of BodyStyle) = New List(Of BodyStyle)()
            For Each row As DataRow In ds.Tables(BodyStyle).Rows
                listBodyStyles.Add(New BodyStyle With {.ID = CInt(row("ID")), .Name = CStr(row("Name"))})
            Next

            Dim listModels As List(Of Model) = New List(Of Model)()
            Dim rnd As Random = New Random()
            For Each row As DataRow In ds.Tables(Model).Rows
                listModels.Add(New Model() With {.ID = CInt(row("ID")), .Name = CStr(row("Name")), .Trademark = Enumerable.First(listTrademarks, Function(x) x.ID = CInt(row("TrademarkID"))).Name, .Modification = CStr(row("Modification")), .Category = CInt(row("CategoryID")), .Price = CDec(row("Price")), .MPGCity = If(DBNull.Value.Equals(row("MPG City")), Nothing, CType(row("MPG City"), Integer?)), .MPGHighway = If(DBNull.Value.Equals(row("MPG City")), Nothing, CType(row("MPG Highway"), Integer?)), .Doors = CInt(row("Doors")), .BodyStyle = Enumerable.First(listBodyStyles, Function(x) x.ID = CInt(row("BodyStyleID"))).Name, .Cylinders = CInt(row("Cylinders")), .Horsepower = CStr(row("Horsepower")), .Torque = CStr(row("Torque")), .TransmissionSpeeds = Convert.ToInt32(row("Transmission Speeds")), .TransmissionType = CInt(row("Transmission Type")), .Description = String.Format("{0}", row("Description")), .DeliveryDate = Date.Now.AddDays(rnd.Next(dataInterval)), .InStock = rnd.Next(100) < 95})
            Next

            Return listModels
        End Function

        Private Shared Sub FillTable(ByVal table As String, ByVal query As String, ByVal connectionString As String, ByVal ds As DataSet)
            If Equals(query, Nothing) Then query = String.Format("SELECT * FROM {0}", table)
            Dim oleDbDataAdapter As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(query, connectionString)
            oleDbDataAdapter.Fill(ds, table)
        End Sub
#End If
    End Class
End Namespace
