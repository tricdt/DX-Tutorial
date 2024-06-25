Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models.Vehicles

Namespace LayoutControlDemo

    Public Class Brand

        Public Property Name As String

        Public Property Image As Byte()
    End Class

    Public Class Car

        Public Property Name As String

        Public Property Image As Byte()

        Public Property Price As Decimal

        Public Property IsFirstInBrand As Boolean

        Public Property IsLastInBrand As Boolean
    End Class

    Public Class CarsViewModel

        Private _Items As List(Of Object)

        Public Property Items As List(Of Object)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of Object))
                _Items = value
            End Set
        End Property

        Public Sub New()
            Me.Items = New System.Collections.Generic.List(Of Object)()
            Using context = New DevExpress.DemoData.Models.Vehicles.VehiclesContext()
                Dim brands = context.Trademarks.OrderBy(Function(x) x.Name)
                For Each brand In brands
                    Dim cars = context.Models.Where(Function(x) x.TrademarkID = brand.ID).OrderBy(Function(x) x.Name)
                    If cars.Count() > 0 Then
                        Me.Items.Add(New LayoutControlDemo.Brand() With {.Name = brand.Name, .Image = brand.Logo})
                        Dim isFirstInBrand As Boolean = True
                        For Each car In cars
                            Me.Items.Add(New LayoutControlDemo.Car() With {.Image = car.Image, .Name = brand.Name & " " & car.Name, .Price = car.Price, .IsFirstInBrand = isFirstInBrand})
                            isFirstInBrand = False
                        Next

                        TryCast(System.Linq.Enumerable.Last(Of System.[Object])(Me.Items), LayoutControlDemo.Car).IsLastInBrand = True
                    End If
                Next
            End Using
        End Sub
    End Class
End Namespace
