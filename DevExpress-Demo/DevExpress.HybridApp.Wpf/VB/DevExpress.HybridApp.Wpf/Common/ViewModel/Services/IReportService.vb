Imports System
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Common.ViewModel

    Public Interface IReportInfo

        ReadOnly Property ParametersViewModel As Object

        Function CreateReport() As IReport

    End Interface

    Public Class ParameterlessReportInfo
        Implements IReportInfo

        Private report As IReport

        Public Sub New(ByVal report As IReport)
            Me.report = report
        End Sub

        Private ReadOnly Property ParametersViewModel As Object Implements IReportInfo.ParametersViewModel
            Get
                Return Nothing
            End Get
        End Property

        Private Function CreateReport() As IReport Implements IReportInfo.CreateReport
            Return report
        End Function
    End Class

    Public Class ReportInfo(Of TParametersViewModel)
        Implements IReportInfo

        Private parametersViewModelField As TParametersViewModel

        Private reportFactory As Func(Of TParametersViewModel, IReport)

        Public Sub New(ByVal parametersViewModel As TParametersViewModel, ByVal reportFactory As Func(Of TParametersViewModel, IReport))
            parametersViewModelField = parametersViewModel
            Me.reportFactory = reportFactory
        End Sub

        Private ReadOnly Property ParametersViewModel As Object Implements IReportInfo.ParametersViewModel
            Get
                Return parametersViewModelField
            End Get
        End Property

        Private Function CreateReport() As IReport Implements IReportInfo.CreateReport
            Return reportFactory(parametersViewModelField)
        End Function
    End Class

    Public Interface IReportService

        Sub SetDefaultReport(ByVal reportInfo As IReportInfo)

        Sub ShowReport(ByVal reportInfo As IReportInfo)

    End Interface
End Namespace
