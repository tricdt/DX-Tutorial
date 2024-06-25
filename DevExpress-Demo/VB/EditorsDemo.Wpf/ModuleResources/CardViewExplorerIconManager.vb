Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Media.Imaging

Namespace GridDemo.ModuleResources

    Friend Module CardViewExplorerIconManager

#Region "CommonWinApi"
        <DllImport("shell32.dll", CharSet:=CharSet.Auto)>
        Private Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As Integer, <Out> ByRef psfi As SHFILEINFO, ByVal cbfileInfo As UInteger, ByVal uFlags As SHGFI) As Integer
        End Function

        <DllImport("shell32.dll", EntryPoint:="#727")>
        Private Function SHGetImageList(ByVal iImageList As Integer, ByRef riid As Guid, ByRef ppv As IImageList) As Integer
        End Function

        <DllImport("user32")>
        Private Function DestroyIcon(ByVal hIcon As IntPtr) As Integer
        End Function

        Const MAX_PATH As Integer = 260

        Const MAX_TYPE As Integer = 80

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
        Public Structure SHFILEINFO

            Public Sub New(ByVal b As Boolean)
                hIcon = IntPtr.Zero
                iIcon = 0
                dwAttributes = 0
                szDisplayName = ""
                szTypeName = ""
            End Sub

            Public hIcon As IntPtr

            Public iIcon As Integer

            Public dwAttributes As UInteger

            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)>
            Public szDisplayName As String

            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_TYPE)>
            Public szTypeName As String
        End Structure

        <Flags>
        Public Enum SHGFI As Integer
            Icon = &H000000100
            DisplayName = &H000000200
            TypeName = &H000000400
            Attributes = &H000000800
            IconLocation = &H000001000
            ExeType = &H000002000
            SysIconIndex = &H000004000
            LinkOverlay = &H000008000
            Selected = &H000010000
            Attr_Specified = &H000020000
            LargeIcon = &H000000000
            SmallIcon = &H000000001
            OpenIcon = &H000000002
            ShellIconSize = &H000000004
            PIDL = &H000000008
            UseFileAttributes = &H000000010
            AddOverlays = &H000000020
            OverlayIndex = &H000000040
        End Enum

#End Region
        Private dpi As Integer = CInt((GetType(SystemParameters).GetProperty("DpiX", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Static).GetValue(Nothing, Nothing)))

        Private dpiCoef As Double = CDbl(dpi) / 76

        Private dpiCoef2 As Double = CDbl(dpi) / 96

        Private Function IconSource(ByVal ic As Icon, ByVal size As SizeIcon, ByVal maybeMinIcon As Boolean) As BitmapSource
            Dim src As Bitmap = GetBitmap(ic, size, maybeMinIcon)
            Dim ic2 As BitmapSource = Imaging.CreateBitmapSourceFromHBitmap(src.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            ic2.Freeze()
            Return ic2
        End Function

        Private Function IconByte(ByVal ic As Icon, ByVal size As SizeIcon, ByVal maybeMinIcon As Boolean) As Byte()
            Dim converter As ImageConverter = New ImageConverter()
            Return CType(converter.ConvertTo(GetBitmap(ic, size, maybeMinIcon), GetType(Byte())), Byte())
        End Function

        Private Function GetBitmap(ByVal ic As Icon, ByVal size As SizeIcon, ByVal maybeMinIcon As Boolean) As Bitmap
            Dim src As Bitmap = ic.ToBitmap()
            If maybeMinIcon Then
                Dim sizeReal As Integer = CalcRealSize(src)
                If sizeReal < 200 Then
                    src = src.Clone(New Rectangle(0, 0, CInt(sizeReal * dpiCoef2), CInt(sizeReal * dpiCoef2)), src.PixelFormat)
                    Select Case size
                        Case SizeIcon.ExtraLarge
                            src = MakeBorder(New System.Drawing.Size(CInt(256 * dpiCoef), CInt(256 * dpiCoef)), src, CInt(sizeReal * dpiCoef2))
                        Case SizeIcon.Large
                            src = MakeBorder(New System.Drawing.Size(CInt(128 * dpiCoef), CInt(128 * dpiCoef)), src, CInt(sizeReal * dpiCoef2))
                        Case SizeIcon.Medium
                            src = MakeBorder(New System.Drawing.Size(CInt(65 * dpiCoef), CInt(65 * dpiCoef)), src, CInt(sizeReal * dpiCoef2))
                    End Select
                End If
            End If

            Return src
        End Function

        Private Function MakeBorder(ByVal itemSize As System.Drawing.Size, ByVal bmpSource As Bitmap, ByVal sizeReal As Integer) As Bitmap
            Using bmpSmall As Bitmap = bmpSource.Clone(New Rectangle(0, 0, sizeReal, sizeReal), bmpSource.PixelFormat)
                Dim result As Bitmap = New Bitmap(itemSize.Width, itemSize.Height)
                Using g As Graphics = Graphics.FromImage(result)
                    g.DrawRectangle(Pens.Transparent, 0, 0, itemSize.Width - 1, itemSize.Height - 1)
                    Dim pt As System.Drawing.Point = New System.Drawing.Point(result.Width \ 2 - bmpSmall.Width \ 2, result.Width \ 2 - bmpSmall.Width \ 2)
                    g.DrawImage(bmpSmall, pt)
                End Using

                Return result
            End Using
        End Function

        Private Function CalcRealSize(ByVal bmp As Bitmap) As Integer
            Dim halfW As Integer = bmp.Width \ 2
            Dim halfH As Integer = bmp.Height \ 2
            Dim result As Integer = 0
            For i As Integer = bmp.Width - 1 To 0 + 1 Step -1
                If bmp.GetPixel(i, halfH).A <> 0 Then
                    result = i
                    Exit For
                End If
            Next

            If result <> 0 Then
                For i As Integer = bmp.Height - 1 To halfH + 1 Step -1
                    If bmp.GetPixel(halfW, i).A <> 0 Then
                        result = Math.Max(i, result)
                        Exit For
                    End If
                Next
            Else
                Dim count As Integer = Math.Min(bmp.Width, bmp.Height)
                For i As Integer = 48 To bmp.Width - 1
                    If bmp.GetPixel(i, i).A = 0 Then
                        Dim newI As Integer = i - 1
                        If bmp.GetPixel(newI, i).A <> 0 Then Continue For
                        If bmp.GetPixel(i, newI).A <> 0 Then Continue For
                        Return i
                    End If
                Next
            End If

            Return If(result <> 0, result, Math.Max(bmp.Width, bmp.Height))
        End Function

        Private info As SHFILEINFO = New SHFILEINFO(True)

        Private cbFileInfo As UInteger = CUInt(Marshal.SizeOf(info))

        Private flags As SHGFI = SHGFI.Icon Or SHGFI.LargeIcon

        Private guil As Guid = New Guid("192B9D83-50FC-457B-90A0-2B82A8B5DAE1")

        Private flagListImage As Integer = &H00000001 Or &H00000020

        Private Function GetFileIcon(ByVal path As String, <Out> ByRef hIcon As IntPtr) As Icon
            SHGetFileInfo(path, 256, info, cbFileInfo, flags)
            hIcon = GetJumboIcon(info.iIcon)
            Dim icon As Icon = Icon.FromHandle(hIcon)
            Return icon
        End Function

        Private Function GetJumboIcon(ByVal iImage As Integer) As IntPtr
            Dim spiml As IImageList = Nothing
            SHGetImageList(&H4, guil, spiml)
            Dim hIcon As IntPtr = IntPtr.Zero
            spiml.GetIcon(iImage, flagListImage, hIcon)
            Return hIcon
        End Function

        Public Function GetLargeIcon(ByVal fileName As String, ByVal maybeSmall As Boolean, ByVal Optional sizeIcon As SizeIcon = SizeIcon.ExtraLarge) As BitmapSource
            Dim hIcon As IntPtr
            Dim icon As Icon = GetFileIcon(fileName, hIcon)
            Dim bs As BitmapSource = IconSource(icon, sizeIcon, maybeSmall)
            icon.Dispose()
            DestroyIcon(hIcon)
            Return bs
        End Function

        Public Function GetLargeIconByte(ByVal fileName As String, ByVal maybeSmall As Boolean, ByVal Optional sizeIcon As SizeIcon = SizeIcon.ExtraLarge) As Byte()
            Try
                Dim hIcon As IntPtr
                Dim icon As Icon = GetFileIcon(fileName, hIcon)
                Dim byteIcon As Byte() = IconByte(icon, sizeIcon, maybeSmall)
                icon.Dispose()
                DestroyIcon(hIcon)
                Return byteIcon
            Catch
                Return New Byte(-1) {}
            End Try
        End Function
    End Module
End Namespace
