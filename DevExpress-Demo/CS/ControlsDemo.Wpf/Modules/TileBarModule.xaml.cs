using System;
using System.Windows;
using DevExpress.Xpf.DemoBase;
namespace ControlsDemo {
    [CodeFile("ViewModels/TileNavBaseViewModel.(cs)")]
    [CodeFile("ViewModels/TileBarViewModel.(cs)")]
    [CodeFile("Modules/Views/TileNavDetailsView.xaml")]    
    [CodeFile("Modules/Views/TileNavDetailsView.xaml.(cs)")]
    [CodeFile("Modules/Views/TileBarHomeView.xaml")]
    [CodeFile("Modules/Views/TileBarHomeView.xaml.(cs)")]
    [CodeFile("Services/TileBarService.(cs)")]
    public partial class TileBarModule : ControlsDemoModule {
        public TileBarModule() {
            InitializeComponent();
        }
        protected override void HidePopupContent() {
            tileBar.CloseFlyout();
            base.HidePopupContent();
        }
    }
}
