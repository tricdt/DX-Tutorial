Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices

Namespace DevExpress.Xpf.Core.Tests

    Public Module ImageComparer

        <DllImport("msvcrt.dll", CallingConvention:=CallingConvention.Cdecl)>
        Private Function memcmp(ByVal b1 As IntPtr, ByVal b2 As IntPtr, ByVal count As UIntPtr) As Integer
        End Function

        Private Function CompareMemCmp(ByVal b1 As Bitmap, ByVal b2 As Bitmap) As Boolean
            If b1 Is Nothing <> (b2 Is Nothing) Then Return False
            If b1.Size <> b2.Size Then Return False
            Dim bd1 = b1.LockBits(New Rectangle(New System.Drawing.Point(0, 0), b1.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            Dim bd2 = b2.LockBits(New Rectangle(New System.Drawing.Point(0, 0), b2.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            Try
                Dim bd1scan0 As IntPtr = bd1.Scan0
                Dim bd2scan0 As IntPtr = bd2.Scan0
                Dim stride As Integer = bd1.Stride
                Dim len As Integer = stride * b1.Height
                Return memcmp(bd1scan0, bd2scan0, New UIntPtr(CUInt(len))) = 0
            Finally
                b1.UnlockBits(bd1)
                b2.UnlockBits(bd2)
            End Try
        End Function

        Public Function CompareMemCmp(ByVal s1 As Stream, ByVal s2 As Stream, <Out> ByRef width1 As Integer, <Out> ByRef width2 As Integer, <Out> ByRef height1 As Integer, <Out> ByRef height2 As Integer) As Boolean
            Dim b1 As Bitmap = New Bitmap(s1)
            Dim b2 As Bitmap = New Bitmap(s2)
            width1 = b1.Size.Width
            width2 = b2.Size.Width
            height1 = b1.Size.Height
            height2 = b2.Size.Height
            Return CompareMemCmp(b1, b2)
        End Function
    End Module
End Namespace
