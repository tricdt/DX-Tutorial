Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class ExchangeRatesModel

        Private gbpUsdRateField As List(Of FinancialDataPoint)

        Private eurUsdRateField As List(Of FinancialDataPoint)

        Public ReadOnly Property GbpUsdRate As List(Of FinancialDataPoint)
            Get
                Return If(gbpUsdRateField, Function()
                    gbpUsdRateField = ReadFinancialData("GBPUSDDaily.csv")
                    Return gbpUsdRateField
                End Function())
            End Get
        End Property

        Public ReadOnly Property EurUsdRate As List(Of FinancialDataPoint)
            Get
                Return If(eurUsdRateField, Function()
                    eurUsdRateField = ReadFinancialData("EURUSDDaily.csv")
                    Return eurUsdRateField
                End Function())
            End Get
        End Property
    End Class
End Namespace
