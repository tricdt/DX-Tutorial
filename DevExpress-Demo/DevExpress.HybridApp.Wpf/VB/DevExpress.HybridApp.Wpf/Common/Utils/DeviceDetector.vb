Imports System
Imports System.Linq
#If Not DXCORE3
Imports System.Management
#End If
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace DevExpress.DevAV.Common.Utils

#If Not DXCORE3
    Public Class DeviceDetector

        Public Enum ChassisTypes
            Other = 1
            Unknown
            Desktop
            LowProfileDesktop
            PizzaBox
            MiniTower
            Tower
            Portable
            Laptop
            Notebook
            Handheld
            DockingStation
            AllInOne
            SubNotebook
            SpaceSaving
            LunchBox
            MainSystemChassis
            ExpansionChassis
            SubChassis
            BusExpansionChassis
            PeripheralChassis
            StorageChassis
            RackMountChassis
            SealedCasePC
        End Enum

        Private Shared dellModel As String() = New String() {"Venue 8 Pro 5830"}

        Private Shared dellModelKind As KnonwnHardwareKind() = New KnonwnHardwareKind() {KnonwnHardwareKind.DellPro8}

        Private Shared Sub ParseKindDell(ByVal res As HardwareInfo)
            ParseKindCore(res, dellModel, dellModelKind)
        End Sub

        Private Shared Function ParseKindCore(ByVal res As HardwareInfo, ByVal model As String(), ByVal kind As KnonwnHardwareKind()) As Boolean
            Dim i As Integer = Array.IndexOf(model, res.Model)
            If i < 0 Then Return False
            res.Kind = kind(i)
            Return True
        End Function

        Private Shared msModel As String() = New String() {"Surface with Windows 8 Pro", "Surface Pro 2", "Surface Pro 3"}

        Private Shared msModelKind As KnonwnHardwareKind() = New KnonwnHardwareKind() {KnonwnHardwareKind.SurfacePro, KnonwnHardwareKind.SurfacePro2, KnonwnHardwareKind.SurfacePro3}

        Private Shared Sub ParseKindMicrosoft(ByVal res As HardwareInfo)
            ParseKindCore(res, msModel, msModelKind)
        End Sub

        Public Enum KnonwnHardwareKind
            Unknown
            SurfacePro
            SurfacePro2
            SurfacePro3
            DellPro8
            DellPro10
        End Enum

        Public Class HardwareInfo

            Public Sub New()
                Kind = KnonwnHardwareKind.Unknown
                Manufacturer = ""
                Model = ""
            End Sub

            Public Property Kind As KnonwnHardwareKind

            Public Property Manufacturer As String

            Public Property Model As String

            Public Overrides Function ToString() As String
                If Kind = KnonwnHardwareKind.Unknown Then
                    Return String.Format("Unknown: {0}/{1}", Manufacturer, Model)
                End If

                Return String.Format("{0}: {1}/{2}", Kind, Manufacturer, Model)
            End Function
        End Class

        Private Shared deviceHardwareInfo As HardwareInfo = Nothing

        Private Shared hasBatteryField As Boolean? = Nothing

        Private Shared chassisField As ChassisTypes? = Nothing

        Private Shared hasTouchSupportField As Boolean? = Nothing

        Private Shared isWindows8Field As Boolean? = Nothing

        Public Shared ReadOnly Property IsWindows8 As Boolean
            Get
                If isWindows8Field Is Nothing Then
                    isWindows8Field = DetectWindows8()
                End If

                Return isWindows8Field.Value
            End Get
        End Property

        Public Shared Function DetectHardwareInfo() As HardwareInfo
            If deviceHardwareInfo Is Nothing Then deviceHardwareInfo = ParseHardwareInfo()
            Return deviceHardwareInfo
        End Function

        Private Shared Function DetectWindows8() As Boolean
            Try
                Dim win8version = New Version(6, 2, 9200, 0)
                If Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version >= win8version Then
                    Return True
                End If
            Catch
            End Try

            Return False
        End Function

        Public Shared ReadOnly Property IsTablet As Boolean
            Get
                If Not IsWindows8 Then
                    Return False
                End If

                If Not HasTouchSupport Then
                    Return False
                End If

                Return HasBattery
            End Get
        End Property

        Public Shared ReadOnly Property IsTabletChassis As Boolean
            Get
                If Chassis = ChassisTypes.Handheld OrElse Chassis = ChassisTypes.Portable Then
                    Return True
                End If

                Return False
            End Get
        End Property

        Public Shared ReadOnly Property HasTouchSupport As Boolean
            Get
                If hasTouchSupportField Is Nothing Then
                    hasTouchSupportField = CheckTouch()
                End If

                Return hasTouchSupportField.Value
            End Get
        End Property

        Private Shared Function CheckTouch() As Boolean
            Dim device = Windows.Input.Tablet.TabletDevices.Cast(Of Windows.Input.TabletDevice)().FirstOrDefault(Function(dev) dev.Type = Windows.Input.TabletDeviceType.Touch)
            If device Is Nothing Then
                Return False
            End If

            Return True
        End Function

        Public Shared ReadOnly Property Chassis As ChassisTypes
            Get
                If chassisField Is Nothing Then
                    chassisField = GetCurrentChassisType()
                End If

                Return chassisField.Value
            End Get
        End Property

        Public Shared Function GetCurrentChassisType() As ChassisTypes
            Try
                Dim systemEnclosures = New ManagementClass("Win32_SystemEnclosure")
                For Each obj As ManagementObject In systemEnclosures.GetInstances()
                    For Each i As Integer In CType(obj("ChassisTypes"), UShort())
                        If i > 0 AndAlso i < 25 Then
                            Return CType(i, ChassisTypes)
                        End If
                    Next
                Next
            Catch
            End Try

            Return ChassisTypes.Unknown
        End Function

        Public Shared ReadOnly Property HasBattery As Boolean
            Get
                If hasBatteryField Is Nothing Then
                    hasBatteryField = CheckHasBattery()
                End If

                Return hasBatteryField.Value
            End Get
        End Property

        Private Shared Function CheckHasBattery() As Boolean
            Try
                Dim query = New ObjectQuery("Select * FROM Win32_Battery")
                Dim searcher = New ManagementObjectSearcher(query)
                Dim collection = searcher.Get()
                Return collection.Count > 0
            Catch
            End Try

            Return False
        End Function

        Public Shared Function SuggestHybridDemoParameters(<Out> ByRef touchScale As Single, <Out> ByRef fontSize As Single) As Boolean
            touchScale = 2F
            fontSize = 11F
            Dim h = DetectHardwareInfo()
            Select Case h.Kind
                Case KnonwnHardwareKind.DellPro8
                    touchScale = 1.5F
                    fontSize = 10
                    Return True
                Case KnonwnHardwareKind.DellPro10, KnonwnHardwareKind.SurfacePro, KnonwnHardwareKind.SurfacePro2, KnonwnHardwareKind.SurfacePro3
                    touchScale = 2.5F
                    fontSize = 8.2F
                    Return True
            End Select

            If Screen.PrimaryScreen.WorkingArea.Width < 1500 OrElse Screen.PrimaryScreen.WorkingArea.Height < 800 Then
                touchScale = 1.5F
                fontSize = 10
            End If

            Return True
        End Function

        Private Shared Function ParseHardwareInfo() As HardwareInfo
            Dim res As HardwareInfo = New HardwareInfo()
            ParseHardwareInfoCore(res)
            Return res
        End Function

        Private Shared Function ParseHardwareInfoCore(ByVal res As HardwareInfo) As Boolean
            Try
                Dim query = New ObjectQuery("Select * FROM Win32_ComputerSystem")
                Dim searcher = New ManagementObjectSearcher(query)
                Dim collection = searcher.Get()
                For Each c In collection
                    res.Manufacturer = c("Manufacturer").ToString()
                    res.Model = c("Model").ToString()
                Next
            Catch
                Return False
            End Try

            ParseKind(res)
            Return True
        End Function

        Private Shared Sub ParseKind(ByVal res As HardwareInfo)
            If Equals(res.Manufacturer, "Microsoft Corporation") Then
                ParseKindMicrosoft(res)
            End If

            If Equals(res.Manufacturer, "DellInc.") Then
                ParseKindDell(res)
            End If
        End Sub
    End Class
#End If
End Namespace
