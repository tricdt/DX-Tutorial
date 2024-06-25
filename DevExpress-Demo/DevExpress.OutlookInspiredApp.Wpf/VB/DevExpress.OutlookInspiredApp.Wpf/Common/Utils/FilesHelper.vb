Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace DevExpress.DevAV.Common.Utils

    Public Module FilesHelper

#Region "CommonWinApi"
        <System.FlagsAttribute>
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

        <System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)>
        Friend Structure SHFILEINFO

            Public hIcon As System.IntPtr

            Public iIcon As System.IntPtr

            Public dwAttributes As UInteger

            <System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=260)>
            Public szDisplayName As String

            <System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=80)>
            Public szTypeName As String
        End Structure

        Private Class Shell32

            <System.Runtime.InteropServices.DllImportAttribute("shell32.dll")>
            Public Shared Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As UInteger, ByRef psfi As DevExpress.DevAV.Common.Utils.FilesHelper.SHFILEINFO, ByVal cbSizeFileInfo As UInteger, ByVal uFlags As UInteger) As IntPtr
            End Function

            <System.Runtime.InteropServices.DllImportAttribute("User32.dll")>
            Public Shared Function DestroyIcon(ByVal hIcon As System.IntPtr) As Integer
            End Function
        End Class

#End Region
        Public MaxAttachedFileSize As Long = 6

        Public Function GetAttachedFileInfo(ByVal name As String, ByVal Optional directoryName As String = "", ByVal Optional id As Long = -1) As AttachedFileInfo
            Return New DevExpress.DevAV.Common.Utils.AttachedFileInfo() With {.Name = name, .DisplayName = System.IO.Path.GetFileNameWithoutExtension(name), .Extension = System.IO.Path.GetExtension(name), .Icon = DevExpress.DevAV.Common.Utils.FilesHelper.IconToImageSourceConverter(DevExpress.DevAV.Common.Utils.FilesHelper.IconFromExtension(System.IO.Path.GetExtension(name))), .FullPath = System.IO.Path.Combine(directoryName, name), .Id = id}
        End Function

        Public Function OpenFileFromDb(ByVal name As String, ByVal content As Byte()) As String
            Dim tempFileName As String = System.IO.Path.GetTempFileName()
            Dim newFileName As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(tempFileName), System.IO.Path.GetFileNameWithoutExtension(name) & "_" & System.IO.Path.GetFileNameWithoutExtension(tempFileName) & System.IO.Path.GetExtension(name))
            Call System.IO.File.Delete(tempFileName)
            Call System.IO.File.WriteAllBytes(newFileName, content)
            Call System.IO.File.SetAttributes(newFileName, System.IO.FileAttributes.[ReadOnly])
            Call System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo With {.FileName = newFileName, .UseShellExecute = True})
            Return newFileName
        End Function

        Public Sub OpenFileFromDisc(ByVal fullPath As String)
            Call System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo With {.FileName = fullPath, .UseShellExecute = True})
        End Sub

        Public Sub DeleteTempFiles(ByRef deletedFilesPath As System.Collections.Generic.List(Of String))
            If deletedFilesPath Is Nothing Then Return
            For Each path As String In deletedFilesPath
                Try
                    Call System.IO.File.SetAttributes(path, System.IO.FileAttributes.Normal)
                    Call System.IO.File.Delete(path)
                Catch __unusedException1__ As System.Exception
                End Try
            Next

            deletedFilesPath.Clear()
        End Sub

#If DXCORE3
        public static string GetDatabaseFilePath() {
            var filePath = Internal.DataDirectoryHelper.GetFile("devav.sqlite3", Internal.DataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if (attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            }
            catch { }
            return filePath;
        }
#End If
        Private Function IconFromExtension(ByVal extension As String) As Icon
            Dim shFileInfo As DevExpress.DevAV.Common.Utils.FilesHelper.SHFILEINFO = New DevExpress.DevAV.Common.Utils.FilesHelper.SHFILEINFO()
            Call DevExpress.DevAV.Common.Utils.FilesHelper.Shell32.SHGetFileInfo(extension, &H80, shFileInfo, CUInt(System.Runtime.InteropServices.Marshal.SizeOf(shFileInfo)), CUInt((DevExpress.DevAV.Common.Utils.FilesHelper.SHGFI.Icon Or DevExpress.DevAV.Common.Utils.FilesHelper.SHGFI.LargeIcon Or DevExpress.DevAV.Common.Utils.FilesHelper.SHGFI.UseFileAttributes)))
            Dim icon As System.Drawing.Icon = CType(System.Drawing.Icon.FromHandle(CType((shFileInfo.hIcon), System.IntPtr)).Clone(), System.Drawing.Icon)
            Call DevExpress.DevAV.Common.Utils.FilesHelper.Shell32.DestroyIcon(shFileInfo.hIcon)
            Return icon
        End Function

        Private Function IconToImageSourceConverter(ByVal icon As System.Drawing.Icon) As BitmapSource
            Return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, System.Windows.Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions())
        End Function
    End Module

    Public Class AttachedFileInfo
        Inherits DevExpress.Mvvm.ViewModelBase

        Public Property DisplayName As String

        Public Property Name As String

        Public Property Extension As String

        Public Property Icon As ImageSource

        Public Property FullPath As String

        Public Property Id As Long
    End Class
End Namespace
