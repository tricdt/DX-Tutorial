Imports System

Namespace DiagramDemo.Properties

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
                If Object.ReferenceEquals(DiagramDemo.Properties.Resources.resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Resources", GetType(DiagramDemo.Properties.Resources).Assembly)
                    DiagramDemo.Properties.Resources.resourceMan = temp
                End If

                Return DiagramDemo.Properties.Resources.resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared Property Culture As Global.System.Globalization.CultureInfo
            Get
                Return DiagramDemo.Properties.Resources.resourceCulture
            End Get

            Set(ByVal value As Global.System.Globalization.CultureInfo)
                DiagramDemo.Properties.Resources.resourceCulture = value
            End Set
        End Property
    End Class
End Namespace