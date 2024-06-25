Imports System
Imports System.Windows
Imports System.Windows.Media.Imaging
Imports System.Windows.Threading

Namespace MapDemo

    Public Class CityInformationControl
        Inherits VisibleControl

        Public Shared ReadOnly CityInfoProperty As DependencyProperty = DependencyProperty.Register("CityInfo", GetType(CityInfo), GetType(CityInformationControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf CityInfoPropertyChanged)))

        Public Shared ReadOnly CityImageSourceProperty As DependencyProperty = DependencyProperty.Register("CityImageSource", GetType(BitmapImage), GetType(CityInformationControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property CityInfo As CityInfo
            Get
                Return CType(GetValue(CityInfoProperty), CityInfo)
            End Get

            Set(ByVal value As CityInfo)
                SetValue(CityInfoProperty, value)
            End Set
        End Property

        Public Property CityImageSource As BitmapImage
            Get
                Return CType(GetValue(CityImageSourceProperty), BitmapImage)
            End Get

            Set(ByVal value As BitmapImage)
                SetValue(CityImageSourceProperty, value)
            End Set
        End Property

        Private Shared Sub CityInfoPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim control As CityInformationControl = TryCast(d, CityInformationControl)
            If control IsNot Nothing AndAlso Not Equals(e.NewValue, e.OldValue) Then control.StartImageChanging()
        End Sub

        Private imageChangeIntervalField As TimeSpan

        Private imageChangeTimer As DispatcherTimer

        Private currentPlaceIndex As Integer

        Public Property ImageChangeInterval As TimeSpan
            Get
                Return imageChangeIntervalField
            End Get

            Set(ByVal value As TimeSpan)
                imageChangeIntervalField = value
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(CityInformationControl)
            imageChangeIntervalField = TimeSpan.FromSeconds(2.0)
            imageChangeTimer = New DispatcherTimer() With {.Interval = imageChangeIntervalField}
            AddHandler imageChangeTimer.Tick, New EventHandler(AddressOf ImageChangeTimer_Tick)
            AddHandler Unloaded, AddressOf CityInformationControl_Unloaded
        End Sub

        Private Sub CityInformationControl_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            imageChangeTimer.Stop()
            RemoveHandler imageChangeTimer.Tick, New EventHandler(AddressOf ImageChangeTimer_Tick)
        End Sub

        Private Sub StartImageChanging()
            currentPlaceIndex = 0
            If CityInfo IsNot Nothing AndAlso CityInfo.Places.Count > 0 Then CityImageSource = CityInfo.Places(currentPlaceIndex).ImageSource
            imageChangeTimer.Start()
        End Sub

        Private Sub ImageChangeTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            If CityInfo IsNot Nothing AndAlso CityInfo.Places.Count > 0 Then
                currentPlaceIndex += 1
                If currentPlaceIndex > CityInfo.Places.Count - 1 Then currentPlaceIndex = 0
                CityImageSource = CityInfo.Places(currentPlaceIndex).ImageSource
                imageChangeTimer.Interval = imageChangeIntervalField
            Else
                imageChangeTimer.Stop()
            End If
        End Sub
    End Class
End Namespace
