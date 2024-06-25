Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Friend Class ResourceUsageData
        Inherits List(Of ResourceUsageDataItem)

        Public Sub New()
            Add(New ResourceUsageDataItem(1, 0.21, 0.22, 0.21, 5, 5, 5))
            Add(New ResourceUsageDataItem(2, 0.31, 0.11, 0.02, 7, 7, 20))
            Add(New ResourceUsageDataItem(3, 0.11, 0.21, 0.35, 2, 12, 18))
            Add(New ResourceUsageDataItem(4, 0.13, 0.25, 0.29, 7, 25, 21))
            Add(New ResourceUsageDataItem(5, 0.02, 0.10, 0.15, 25, 25, 19))
            Add(New ResourceUsageDataItem(6, 0.05, 0.11, 0.21, 27, 20, 10))
            Add(New ResourceUsageDataItem(7, 0.11, 0.15, 0.23, 44, 17, 8))
            Add(New ResourceUsageDataItem(8, 0.15, 0.20, 0.30, 45, 24, 15))
            Add(New ResourceUsageDataItem(9, 0.18, 0.25, 0.36, 50, 29, 17))
            Add(New ResourceUsageDataItem(10, 0.23, 0.12, 0.38, 52, 25, 12))
            Add(New ResourceUsageDataItem(11, 0.21, 0.08, 0.36, 52, 28, 40))
            Add(New ResourceUsageDataItem(12, 0.16, 0.08, 0.37, 55, 29, 47))
            Add(New ResourceUsageDataItem(13, 0.22, 0.27, 0.33, 53, 25, 50))
            Add(New ResourceUsageDataItem(14, 0.25, 0.29, 0.22, 51, 28, 45))
            Add(New ResourceUsageDataItem(15, 0.22, 0.31, 0.19, 49, 30, 50))
            Add(New ResourceUsageDataItem(16, 0.23, 0.34, 0.15, 45, 42, 51))
            Add(New ResourceUsageDataItem(17, 0.25, 0.40, 0.03, 46, 45, 48))
            Add(New ResourceUsageDataItem(18, 0.32, 0.54, 0.04, 42, 40, 43))
            Add(New ResourceUsageDataItem(19, 0.30, 0.51, 0.03, 45, 20, 15))
            Add(New ResourceUsageDataItem(20, 0.31, 0.45, 0.07, 48, 21, 19))
            Add(New ResourceUsageDataItem(21, 0.25, 0.40, 0.05, 48, 35, 25))
            Add(New ResourceUsageDataItem(22, 0.10, 0.43, 0.07, 49, 33, 27))
            Add(New ResourceUsageDataItem(23, 0.05, 0.45, 0.15, 49, 35, 30))
            Add(New ResourceUsageDataItem(24, 0.03, 0.44, 0.21, 51, 37, 32))
            Add(New ResourceUsageDataItem(25, 0.01, 0.42, 0.23, 55, 40, 37))
            Add(New ResourceUsageDataItem(26, 0.01, 0.45, 0.21, 57, 43, 39))
            Add(New ResourceUsageDataItem(27, 0.01, 0.43, 0.22, 59, 50, 43))
            Add(New ResourceUsageDataItem(28, 0.01, 0.39, 0.25, 62, 51, 42))
            Add(New ResourceUsageDataItem(29, 0.03, 0.27, 0.20, 42, 31, 23))
            Add(New ResourceUsageDataItem(30, 0.07, 0.25, 0.14, 25, 20, 17))
            Add(New ResourceUsageDataItem(31, 0.05, 0.12, 0.09, 35, 25, 20))
            Add(New ResourceUsageDataItem(32, 0.03, 0.10, 0.05, 41, 29, 24))
            Add(New ResourceUsageDataItem(33, 0.05, 0.08, 0.06, 48, 32, 26))
            Add(New ResourceUsageDataItem(34, 0.02, 0.09, 0.06, 55, 37, 28))
            Add(New ResourceUsageDataItem(35, 0.05, 0.11, 0.07, 59, 38, 28))
            Add(New ResourceUsageDataItem(36, 0.03, 0.13, 0.05, 63, 39, 30))
            Add(New ResourceUsageDataItem(37, 0.02, 0.15, 0.03, 67, 43, 31))
            Add(New ResourceUsageDataItem(38, 0.05, 0.12, 0.07, 71, 50, 32))
            Add(New ResourceUsageDataItem(39, 0.07, 0.16, 0.12, 65, 43, 31))
            Add(New ResourceUsageDataItem(40, 0.09, 0.25, 0.18, 61, 39, 30))
            Add(New ResourceUsageDataItem(41, 0.09, 0.23, 0.19, 60, 38, 30))
            Add(New ResourceUsageDataItem(42, 0.10, 0.25, 0.20, 63, 37, 31))
            Add(New ResourceUsageDataItem(43, 0.11, 0.22, 0.18, 64, 35, 32))
            Add(New ResourceUsageDataItem(44, 0.13, 0.31, 0.19, 60, 36, 30))
            Add(New ResourceUsageDataItem(45, 0.17, 0.33, 0.22, 58, 35, 31))
            Add(New ResourceUsageDataItem(46, 0.23, 0.30, 0.27, 63, 32, 33))
            Add(New ResourceUsageDataItem(47, 0.20, 0.25, 0.30, 58, 29, 31))
            Add(New ResourceUsageDataItem(48, 0.17, 0.23, 0.35, 62, 28, 32))
            Add(New ResourceUsageDataItem(49, 0.15, 0.25, 0.37, 60, 26, 30))
            Add(New ResourceUsageDataItem(50, 0.12, 0.22, 0.40, 55, 23, 27))
            Add(New ResourceUsageDataItem(51, 0.11, 0.20, 0.42, 57, 21, 31))
            Add(New ResourceUsageDataItem(52, 0.09, 0.18, 0.45, 60, 20, 35))
            Add(New ResourceUsageDataItem(53, 0.08, 0.17, 0.46, 65, 19, 45))
            Add(New ResourceUsageDataItem(54, 0.05, 0.10, 0.52, 77, 17, 43))
            Add(New ResourceUsageDataItem(55, 0.03, 0.12, 0.55, 81, 18, 40))
            Add(New ResourceUsageDataItem(56, 0.05, 0.09, 0.53, 75, 17, 15))
            Add(New ResourceUsageDataItem(57, 0.07, 0.12, 0.47, 67, 18, 16))
            Add(New ResourceUsageDataItem(58, 0.03, 0.09, 0.35, 60, 19, 12))
            Add(New ResourceUsageDataItem(59, 0.05, 0.12, 0.23, 41, 10, 5))
            Add(New ResourceUsageDataItem(60, 0.03, 0.07, 0.10, 33, 5, 3))
        End Sub
    End Class

    Friend Class ResourceUsageDataItem

        Private ReadOnly secondField As TimeSpan

        Private ReadOnly process1CpuUsageField As Double

        Private ReadOnly process2CpuUsageField As Double

        Private ReadOnly process3CpuUsageField As Double

        Private ReadOnly process1MemoryField As Double

        Private ReadOnly process2MemoryField As Double

        Private ReadOnly process3MemoryField As Double

        Public ReadOnly Property Second As TimeSpan
            Get
                Return secondField
            End Get
        End Property

        Public ReadOnly Property Process1CpuUsage As Double
            Get
                Return process1CpuUsageField
            End Get
        End Property

        Public ReadOnly Property Process2CpuUsage As Double
            Get
                Return process2CpuUsageField
            End Get
        End Property

        Public ReadOnly Property Process3CpuUsage As Double
            Get
                Return process3CpuUsageField
            End Get
        End Property

        Public ReadOnly Property Process1Memory As Double
            Get
                Return process1MemoryField
            End Get
        End Property

        Public ReadOnly Property Process2Memory As Double
            Get
                Return process2MemoryField
            End Get
        End Property

        Public ReadOnly Property Process3Memory As Double
            Get
                Return process3MemoryField
            End Get
        End Property

        Public Sub New(ByVal second As Integer, ByVal process1CpuUsage As Double, ByVal process2CpuUsage As Double, ByVal process3CpuUsage As Double, ByVal process1Memory As Double, ByVal process2Memory As Double, ByVal process3Memory As Double)
            secondField = TimeSpan.FromSeconds(second)
            process1CpuUsageField = process1CpuUsage
            process2CpuUsageField = process2CpuUsage
            process3CpuUsageField = process3CpuUsage
            process1MemoryField = process1Memory
            process2MemoryField = process2Memory
            process3MemoryField = process3Memory
        End Sub
    End Class
End Namespace
