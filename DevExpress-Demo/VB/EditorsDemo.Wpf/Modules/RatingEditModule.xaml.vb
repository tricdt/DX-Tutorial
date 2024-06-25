Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace EditorsDemo

    Public Partial Class RatingEditModule
        Inherits EditorsDemo.EditorsDemoModule

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class

    Public Class RatingEditViewModel

        Public Overridable Property Background As Color

        Public Overridable Property HoverBackground As Color

        Public Overridable Property SelectedBackground As Color

        Public Overridable Property Cars As List(Of EditorsDemo.CarRating)

        Public Overridable Property SelectedCar As CarRating

        Public Sub New()
            Me.Background = System.Windows.Media.Colors.Transparent
            Me.HoverBackground = System.Windows.Media.Colors.Transparent
            Me.SelectedBackground = System.Windows.Media.Colors.Transparent
            Me.CreateCarsSource()
            Me.SelectedCar = Me.Cars(0)
        End Sub

        Private Sub CreateCarsSource()
            Dim carsSource = DevExpress.Xpf.DemoBase.DataClasses.CarsData.DataSource
            Dim cars As System.Collections.Generic.List(Of EditorsDemo.CarRating) = New System.Collections.Generic.List(Of EditorsDemo.CarRating)()
            Dim rand As System.Random = New System.Random()
            For Each car In carsSource
                cars.Add(New EditorsDemo.CarRating() With {.Car = car, .SpeedRating = rand.[Next](5, 10), .ComfortRating = rand.[Next](5, 10), .QualityRating = rand.[Next](5, 10), .PriceRating = rand.[Next](5, 10)})
            Next

            Me.Cars = cars
        End Sub
    End Class

    Public Class ColorDisplayTextConverter
        Inherits System.Windows.Markup.MarkupExtension
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If CType(value, System.Windows.Media.Color) = System.Windows.Media.Colors.Transparent Then Return "Automatic"
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return New EditorsDemo.ColorDisplayTextConverter()
        End Function
    End Class

    Public Class IsNullColorConverter
        Inherits System.Windows.Markup.MarkupExtension
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Return CType(value, System.Windows.Media.Color) = System.Windows.Media.Colors.Transparent
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return New EditorsDemo.IsNullColorConverter()
        End Function
    End Class

    Public Class ColorToBrushConverter
        Inherits System.Windows.Markup.MarkupExtension
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Return If(value Is Nothing, Nothing, New System.Windows.Media.SolidColorBrush(CType(value, System.Windows.Media.Color)))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return New EditorsDemo.ColorToBrushConverter()
        End Function
    End Class

    Public Class CarRating

        Public Property Car As Cars

        Public Property SpeedRating As Double

        Public Property ComfortRating As Double

        Public Property QualityRating As Double

        Public Property PriceRating As Double
    End Class
End Namespace
