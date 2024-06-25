using System;
using System.Windows;
using DevExpress.Xpf.DemoBase;
namespace ControlsDemo {
    [CodeFile("ViewModels/TileNavBaseViewModel.(cs)")]
    [CodeFile("ViewModels/TileNavPaneViewModel.(cs)")]
    [CodeFile("Modules/Views/TileNavDetailsView.xaml")]
    [CodeFile("Modules/Views/TileNavDetailsView.xaml.(cs)")]
    [CodeFile("Modules/Views/TileNavPaneHomeView.xaml")]
    [CodeFile("Modules/Views/TileNavPaneHomeView.xaml.(cs)")]
    public partial class TileNavPaneModule : ControlsDemoModule {        
        public TileNavPaneModule() {
            InitializeComponent();
        }
        protected override void HidePopupContent() {
            tileNavPane.CloseFlyout();
            base.HidePopupContent();
        }
    }
}
