Imports System

Namespace PdfViewerDemo.Properties

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")>
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    Friend Class Resources

        Private Shared resourceMan As Global.System.Resources.ResourceManager

        Private Shared resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>
        Friend Sub New()
        End Sub

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared ReadOnly Property ResourceManager As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(PdfViewerDemo.Properties.Resources.resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Resources", GetType(PdfViewerDemo.Properties.Resources).Assembly)
                    PdfViewerDemo.Properties.Resources.resourceMan = temp
                End If

                Return PdfViewerDemo.Properties.Resources.resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared Property Culture As Global.System.Globalization.CultureInfo
            Get
                Return PdfViewerDemo.Properties.Resources.resourceCulture
            End Get

            Set(ByVal value As Global.System.Globalization.CultureInfo)
                PdfViewerDemo.Properties.Resources.resourceCulture = value
            End Set
        End Property
    End Class
End Namespace
