Imports System
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace EditorsDemo

    Friend Class PinnedWindowBehaviorNativeMethods

        <DllImport("user32.dll", EntryPoint:="GetWindowLong")>
        Private Shared Function GetWindowLong32(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
        End Function

        <DllImport("user32.dll", EntryPoint:="GetWindowLongPtr")>
        Private Shared Function GetWindowLongPtr64(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
        End Function

        Public Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Long
            If IntPtr.Size = 4 Then Return GetWindowLong32(hWnd, nIndex)
            Return GetWindowLongPtr64(hWnd, nIndex).ToInt64()
        End Function

        Public Shared Function SetWindowLongPtr(ByVal hWnd As HandleRef, ByVal nIndex As Integer, ByVal dwNewLong As Long) As IntPtr
            If IntPtr.Size = 8 Then Return SetWindowLongPtr64(hWnd, nIndex, New IntPtr(dwNewLong))
            Return New IntPtr(SetWindowLong32(hWnd, nIndex, CInt(dwNewLong)))
        End Function

        <DllImport("user32.dll", EntryPoint:="SetWindowLong")>
        Private Shared Function SetWindowLong32(ByVal hWnd As HandleRef, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
        End Function

        <DllImport("user32.dll", EntryPoint:="SetWindowLongPtr")>
        Private Shared Function SetWindowLongPtr64(ByVal hWnd As HandleRef, ByVal nIndex As Integer, ByVal dwNewLong As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
        End Function
    End Class
End Namespace
