Imports System.Linq

Namespace ChartsDemo

    Public Class SelectionViewModel

        Private countriesDataField As CountriesInfo

        Public ReadOnly Property CountriesData As CountriesInfo
            Get
                Return countriesDataField
            End Get
        End Property

        Public Overridable Property SelectedCountry As CountryStatisticInfo

        Public Overridable Property SelectedIndex As Integer

        Public Overridable Property SelectedCountryName As String

        Public Sub New()
            countriesDataField = CountriesInfo.Load()
            SelectedCountry = CountriesData.First()
        End Sub

        Protected Sub OnSelectedCountryChanged()
            SelectedCountryName = If(SelectedCountry IsNot Nothing, SelectedCountry.Name, String.Empty)
            SelectedIndex = CountriesData.IndexOf(SelectedCountry)
        End Sub

        Protected Sub OnSelectedCountryNameChanged()
            SelectedCountry = CountriesData.FirstOrDefault(Function(x) Equals(x.Name, SelectedCountryName))
        End Sub
    End Class
End Namespace
