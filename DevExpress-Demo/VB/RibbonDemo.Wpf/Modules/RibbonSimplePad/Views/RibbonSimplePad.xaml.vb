Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Ribbon
Imports System.ComponentModel
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Grid

Namespace RibbonDemo

    <CodeFiles("Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml;
                 Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml.(cs);
                 Modules/RibbonSimplePad/ViewModels/SimplePadViewModel.(cs);
                 Modules/RibbonSimplePad/Views/BackstageViewPanes.xaml;
                 Modules/RibbonSimplePad/Views/TemplateSelectors.xaml;
                 Modules/RibbonSimplePad/ViewModels/InlineImageViewModel.(cs);
                 Modules/RibbonSimplePad/ViewModels/RecentItemViewModel.(cs);
                 Modules/RibbonSimplePad/Services/BackstageViewService.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/InlineImageContentTemplateSelector.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/TemplateSelectorDictionary.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/TextMarkerStyleTemplateSelector.(cs);
                 Modules/RibbonSimplePad/Views/BackstageViewCommonStyles.xaml")>
    Public Partial Class RibbonSimplePad
        Inherits RibbonDemoModule

        Shared Sub New()
            DataControlBase.AllowInfiniteGridSize = True
        End Sub

        Public Shared ReadOnly FontEditWidthProperty As DependencyProperty = DependencyProperty.Register("FontEditWidth", GetType(Double?), GetType(RibbonSimplePad), New FrameworkPropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property FontEditWidth As Double?
            Get
                Return CDbl(GetValue(FontEditWidthProperty))
            End Get

            Set(ByVal value As Double?)
                SetValue(FontEditWidthProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            Ribbon = RibbonControl
            richControl.Document.Blocks.Add(New Paragraph(New Run("Select the image below to show a contextual tab.")))
            richControl.Document.Blocks.Add(New Paragraph(New InlineUIContainer() With {.Child = New InlineImage(InlineImageViewModel.Create("/RibbonDemo;component/Images/Clipart/caCompClientEnabled.png"))}))
            AddHandler ModuleLoaded, AddressOf OnModuleLoaded
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Unloaded, AddressOf OnUnloaded
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RibbonControl.SetCurrentValue(RibbonControl.RibbonStyleProperty, GetRibbonStyle())
            RibbonControl.SetCurrentValue(RibbonControl.RibbonTitleBarVisibilityProperty, GetTitleBarVisibility())
            AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
            OnThemeChanged(Nothing, Nothing)
        End Sub

        Protected Overridable Function GetTitleBarVisibility() As RibbonTitleBarVisibility
            Return RibbonTitleBarVisibility.Auto
        End Function

        Protected Overridable Function GetRibbonStyle() As RibbonStyle
            Return DevExpress.Xpf.Ribbon.RibbonStyle.Office2019
        End Function

        Protected Overrides Sub Hide()
            RibbonControl.CloseApplicationMenu()
            MyBase.Hide()
        End Sub

        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not IsInDesignTool() Then
                richControl.SetFocus()
            End If
        End Sub

        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
        End Sub

        Private Overloads Sub OnThemeChanged(ByVal sender As DependencyObject, ByVal e As ThemeChangedRoutedEventArgs)
            If Not Dispatcher.CheckAccess() Then Return
            FontEditWidth = If(ThemeManager.GetIsTouchEnabled(Me), 90, 50)
        End Sub
    End Class
End Namespace
