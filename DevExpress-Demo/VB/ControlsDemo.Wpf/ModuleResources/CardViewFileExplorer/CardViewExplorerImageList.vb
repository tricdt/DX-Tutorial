Imports System
Imports System.Runtime.InteropServices

Namespace GridDemo.ModuleResources

    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT

        Public left, top, right, bottom As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure POINT

        Private x As Integer

        Private y As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure IMAGELISTDRAWPARAMS

        Public cbSize As Integer

        Public himl As IntPtr

        Public i As Integer

        Public hdcDst As IntPtr

        Public x As Integer

        Public y As Integer

        Public cx As Integer

        Public cy As Integer

        Public xBitmap As Integer

        Public yBitmap As Integer

        Public rgbBk As Integer

        Public rgbFg As Integer

        Public fStyle As Integer

        Public dwRop As Integer

        Public fState As Integer

        Public Frame As Integer

        Public crEffect As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure IMAGEINFO

        Public hbmImage As IntPtr

        Public hbmMask As IntPtr

        Public Unused1 As Integer

        Public Unused2 As Integer

        Public rcImage As RECT
    End Structure

    <ComImportAttribute()>
    <GuidAttribute("46EB5926-582E-4017-9FDF-E8998DAA0950")>
    <InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IImageList

        <PreserveSig>
        Function Add(ByVal hbmImage As IntPtr, ByVal hbmMask As IntPtr, ByRef pi As Integer) As Integer

        <PreserveSig>
        Function ReplaceIcon(ByVal i As Integer, ByVal hicon As IntPtr, ByRef pi As Integer) As Integer

        <PreserveSig>
        Function SetOverlayImage(ByVal iImage As Integer, ByVal iOverlay As Integer) As Integer

        <PreserveSig>
        Function Replace(ByVal i As Integer, ByVal hbmImage As IntPtr, ByVal hbmMask As IntPtr) As Integer

        <PreserveSig>
        Function AddMasked(ByVal hbmImage As IntPtr, ByVal crMask As Integer, ByRef pi As Integer) As Integer

        <PreserveSig>
        Function Draw(ByRef pimldp As IMAGELISTDRAWPARAMS) As Integer

        <PreserveSig>
        Function Remove(ByVal i As Integer) As Integer

        <PreserveSig>
        Function GetIcon(ByVal i As Integer, ByVal flags As Integer, ByRef picon As IntPtr) As Integer

        <PreserveSig>
        Function GetImageInfo(ByVal i As Integer, ByRef pImageInfo As IMAGEINFO) As Integer

        <PreserveSig>
        Function Copy(ByVal iDst As Integer, ByVal punkSrc As IImageList, ByVal iSrc As Integer, ByVal uFlags As Integer) As Integer

        <PreserveSig>
        Function Merge(ByVal i1 As Integer, ByVal punk2 As IImageList, ByVal i2 As Integer, ByVal dx As Integer, ByVal dy As Integer, ByRef riid As Guid, ByRef ppv As IntPtr) As Integer

        <PreserveSig>
        Function Clone(ByRef riid As Guid, ByRef ppv As IntPtr) As Integer

        <PreserveSig>
        Function GetImageRect(ByVal i As Integer, ByRef prc As RECT) As Integer

        <PreserveSig>
        Function GetIconSize(ByRef cx As Integer, ByRef cy As Integer) As Integer

        <PreserveSig>
        Function SetIconSize(ByVal cx As Integer, ByVal cy As Integer) As Integer

        <PreserveSig>
        Function GetImageCount(ByRef pi As Integer) As Integer

        <PreserveSig>
        Function SetImageCount(ByVal uNewCount As Integer) As Integer

        <PreserveSig>
        Function SetBkColor(ByVal clrBk As Integer, ByRef pclr As Integer) As Integer

        <PreserveSig>
        Function GetBkColor(ByRef pclr As Integer) As Integer

        <PreserveSig>
        Function BeginDrag(ByVal iTrack As Integer, ByVal dxHotspot As Integer, ByVal dyHotspot As Integer) As Integer

        <PreserveSig>
        Function EndDrag() As Integer

        <PreserveSig>
        Function DragEnter(ByVal hwndLock As IntPtr, ByVal x As Integer, ByVal y As Integer) As Integer

        <PreserveSig>
        Function DragLeave(ByVal hwndLock As IntPtr) As Integer

        <PreserveSig>
        Function DragMove(ByVal x As Integer, ByVal y As Integer) As Integer

        <PreserveSig>
        Function SetDragCursorImage(ByRef punk As IImageList, ByVal iDrag As Integer, ByVal dxHotspot As Integer, ByVal dyHotspot As Integer) As Integer

        <PreserveSig>
        Function DragShowNolock(ByVal fShow As Integer) As Integer

        <PreserveSig>
        Function GetDragImage(ByRef ppt As POINT, ByRef pptHotspot As POINT, ByRef riid As Guid, ByRef ppv As IntPtr) As Integer

        <PreserveSig>
        Function GetItemFlags(ByVal i As Integer, ByRef dwFlags As Integer) As Integer

        <PreserveSig>
        Function GetOverlayImage(ByVal iOverlay As Integer, ByRef piIndex As Integer) As Integer

    End Interface
End Namespace
