Imports System
Imports System.ComponentModel

Namespace DevExpress.DevAV.Common

    ''' <summary>
    '''   A strongly-typed resource class, for looking up localized strings, etc.
    ''' </summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")>
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    Friend Class CommonResources

        Private Shared resourceMan As Global.System.Resources.ResourceManager

        Private Shared resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>
        Friend Sub New()
        End Sub

        ''' <summary>
        '''   Returns the cached ResourceManager instance used by this class.
        ''' </summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared ReadOnly Property ResourceManager As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(DevExpress.DevAV.Common.CommonResources.resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("DevExpress.DevAV.Common.CommonResources", GetType(DevExpress.DevAV.Common.CommonResources).Assembly)
                    DevExpress.DevAV.Common.CommonResources.resourceMan = temp
                End If

                Return DevExpress.DevAV.Common.CommonResources.resourceMan
            End Get
        End Property

        ''' <summary>
        '''   Overrides the current thread's CurrentUICulture property for all
        '''   resource lookups using this strongly typed resource class.
        ''' </summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared Property Culture As Global.System.Globalization.CultureInfo
            Get
                Return DevExpress.DevAV.Common.CommonResources.resourceCulture
            End Get

            Set(ByVal value As Global.System.Globalization.CultureInfo)
                DevExpress.DevAV.Common.CommonResources.resourceCulture = value
            End Set
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Do you want to delete this {0}?.
        ''' </summary>
        Friend Shared ReadOnly Property Confirmation_Delete As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Confirmation_Delete", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Do you want to save changes?.
        ''' </summary>
        Friend Shared ReadOnly Property Confirmation_Save As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Confirmation_Save", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Click OK to reload the entity and lose unsaved changes. Click Cancel to continue editing data..
        ''' </summary>
        Friend Shared ReadOnly Property Confirmation_Reset As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Confirmation_Reset", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Confirmation.
        ''' </summary>
        Friend Shared ReadOnly Property Confirmation_Caption As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Confirmation_Caption", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Warning.
        ''' </summary>
        Friend Shared ReadOnly Property Warning_Caption As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Warning_Caption", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Some fields contain invalid data. Click OK to close the page and lose unsaved changes. Press Cancel to continue editing data..
        ''' </summary>
        Friend Shared ReadOnly Property Warning_SomeFieldsContainInvalidData As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Warning_SomeFieldsContainInvalidData", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Update Error.
        ''' </summary>
        Friend Shared ReadOnly Property Exception_UpdateErrorCaption As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Exception_UpdateErrorCaption", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Validation Error.
        ''' </summary>
        Friend Shared ReadOnly Property Exception_ValidationErrorCaption As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Exception_ValidationErrorCaption", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to DataService Request Error.
        ''' </summary>
        Friend Shared ReadOnly Property Exception_DataServiceRequestErrorCaption As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Exception_DataServiceRequestErrorCaption", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to  *.
        ''' </summary>
        Friend Shared ReadOnly Property Entity_Changed As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Entity_Changed", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to  (New).
        ''' </summary>
        Friend Shared ReadOnly Property Entity_New As String
            Get
                Return DevExpress.DevAV.Common.CommonResources.ResourceManager.GetString("Entity_New", DevExpress.DevAV.Common.CommonResources.resourceCulture)
            End Get
        End Property
    End Class
End Namespace
