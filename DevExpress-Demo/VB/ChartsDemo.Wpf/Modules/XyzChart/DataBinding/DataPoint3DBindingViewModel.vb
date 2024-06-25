Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace ChartsDemo

    Public Class DataPoint3DBindingViewModel

        Private _FunctionItemsData As List(Of ChartsDemo.Function3DItemData)

        Private Shared Function CreateFunctionItemsData() As List(Of ChartsDemo.Function3DItemData)
            Dim functions = New System.Func(Of Double, Double, System.Windows.Media.Media3D.Point3D)() {AddressOf ChartsDemo.DataPoint3DBindingViewModel.CalculateFirstValue, AddressOf ChartsDemo.DataPoint3DBindingViewModel.CalculateSecondValue, AddressOf ChartsDemo.DataPoint3DBindingViewModel.CalculateThirdValue, AddressOf ChartsDemo.DataPoint3DBindingViewModel.CalculateFourthValue, AddressOf ChartsDemo.DataPoint3DBindingViewModel.CalculateFifthValue}
            Dim images = System.Linq.Enumerable.Range(1, 5).[Select](Function(x) ChartsDemo.Chart3DUtils.CreateFunctionImage(x))
            Return functions.Zip(images, Function(func, image) New ChartsDemo.Function3DItemData With {.Image = image, .Points = ChartsDemo.DataPoint3DBindingViewModel.CreatePoints(func)}).ToList()
        End Function

        Private Shared Function Sinc(ByVal x As Double) As Double
            Return If(x <> 0, System.Math.Sin(x), 1)
        End Function

        Private Shared Function CalculateFirstValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= ChartsDemo.DataPoint3DBindingViewModel.UnitFactor
            y *= ChartsDemo.DataPoint3DBindingViewModel.UnitFactor
            Dim value As Double = ChartsDemo.DataPoint3DBindingViewModel.Sinc(System.Math.Sin(System.Math.Pow(System.Math.Pow(x, 6) + System.Math.Pow(y, 6), 1.0R / 6.0R))) * 5
            Return New System.Windows.Media.Media3D.Point3D(x, y, value)
        End Function

        Private Shared Function CalculateSecondValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= ChartsDemo.DataPoint3DBindingViewModel.UnitFactor
            y *= ChartsDemo.DataPoint3DBindingViewModel.UnitFactor
            Dim square As Double = System.Math.Sqrt(x * x + y * y)
            Dim value As Double = square * ChartsDemo.DataPoint3DBindingViewModel.Sinc(square) * 0.2
            Return New System.Windows.Media.Media3D.Point3D(x, y, value)
        End Function

        Private Shared Function CalculateThirdValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= ChartsDemo.DataPoint3DBindingViewModel.UnitFactor / 2
            y *= ChartsDemo.DataPoint3DBindingViewModel.UnitFactor / 2
            Dim value As Double = System.Math.Sin(x) * System.Math.Cos(y) * 2
            Return New System.Windows.Media.Media3D.Point3D(x, y, value)
        End Function

        Private Shared Function CalculateFourthValue(ByVal x As Double, ByVal y As Double) As Point3D
            Dim theta As Double = System.Math.Atan2(y, x)
            Dim r As Double = x * x + y * y
            Dim z As Double = System.Math.Exp(-r) * System.Math.Sin(2 * System.Math.PI * System.Math.Sqrt(r)) * System.Math.Cos(3 * theta)
            Return New System.Windows.Media.Media3D.Point3D(x, y, z)
        End Function

        Private Shared Function CalculateFifthValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= 3
            y *= 3
            Dim z As Double = System.Math.Sin(x * y)
            Return New System.Windows.Media.Media3D.Point3D(x, y, z)
        End Function

        Private Shared Function CreatePoints(ByVal valueCalculator As System.Func(Of Double, Double, System.Windows.Media.Media3D.Point3D)) As List(Of System.Windows.Media.Media3D.Point3D)
            Dim points As System.Collections.Generic.List(Of System.Windows.Media.Media3D.Point3D) = New System.Collections.Generic.List(Of System.Windows.Media.Media3D.Point3D)()
            For x As Double = -1 To 1 - 1 Step 0.017R
                For y As Double = -1 To 1 - 1 Step 0.017R
                    points.Add(valueCalculator(x, y))
                Next
            Next

            Return points
        End Function

        Public Shared Function Create() As DataPoint3DBindingViewModel
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New ChartsDemo.DataPoint3DBindingViewModel())
        End Function

        Const UnitFactor As Double = 15

        Public Property FunctionItemsData As List(Of ChartsDemo.Function3DItemData)
            Get
                Return _FunctionItemsData
            End Get

            Private Set(ByVal value As List(Of ChartsDemo.Function3DItemData))
                _FunctionItemsData = value
            End Set
        End Property

        Public Overridable Property [Function] As Function3DItemData

        Protected Sub New()
            Me.FunctionItemsData = ChartsDemo.DataPoint3DBindingViewModel.CreateFunctionItemsData()
            Me.[Function] = Me.FunctionItemsData(3)
        End Sub
    End Class

    Public Class Function3DItemData
        Inherits DevExpress.Mvvm.BindableBase

        Private pointsField As System.Collections.Generic.List(Of System.Windows.Media.Media3D.Point3D)

        Private imageField As System.Windows.Media.Imaging.BitmapImage

        Public Property Points As List(Of System.Windows.Media.Media3D.Point3D)
            Get
                Return Me.pointsField
            End Get

            Set(ByVal value As List(Of System.Windows.Media.Media3D.Point3D))
                SetProperty(Me.pointsField, value, Function() Me.Points)
            End Set
        End Property

        Public Property Image As BitmapImage
            Get
                Return Me.imageField
            End Get

            Set(ByVal value As BitmapImage)
                SetProperty(Me.imageField, value, Function() Me.Image)
            End Set
        End Property
    End Class

    Public Module Chart3DUtils

        Public Function CreateSize3D(ByVal x As Double, ByVal y As Double, ByVal z As Double) As Size3D
            Return New System.Windows.Media.Media3D.Size3D(x, y, z)
        End Function

        Public Function CreateFunctionImage(ByVal index As Integer) As BitmapImage
            Dim uriStr As String = String.Format("/ChartsDemo;component/Images/Functions/{0}.png", index)
            Return New System.Windows.Media.Imaging.BitmapImage(New System.Uri(uriStr, System.UriKind.RelativeOrAbsolute))
        End Function
    End Module
End Namespace
