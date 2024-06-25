using DevExpress.Xpf.Ribbon;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using DevExpress.Mvvm.POCO;
using System;
using DevExpress.Mvvm.UI;
using System.Windows.Media.Imaging;
using System.Windows.Documents;
using System.Diagnostics;

namespace RibbonDemo {
    public class RibbonSimplePadViewModel {
        #region properties
        public string DXVersion { get { return "Version: " + AssemblyInfo.VersionShort; } }
        public string DXCopyright { get { return AssemblyInfo.AssemblyCopyright; } }
        public virtual InlineImageViewModel SelectedImage { get; set; }
        public bool IsSelectionEmpty { get; set; }
        public TextAlignment TextAlignment { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderline { get; set; }
        public virtual double? FontSize { get; set; }
        public virtual string FontFamily { get; set; }
        public Color Foreground { get; set; }
        public Color Background { get; set; }
        public Color SelectedImageColor { get; set; }
        public InlineImageBorderType[] ShapeTypes { get; protected set; }
        public TextMarkerStyle[] ListMarkerStyles { get; protected set; }
        public RecentItemViewModel[] RecentDocuments { get; protected set; }

        public string[] ClipartImages { get; protected set; }
        public IEnumerable<double?> FontSizes { get; protected set; }
        public string[] FontFamilies { get; protected set; }
        public double[] BorderWeightArray { get; protected set; }
        public string[] ImageScaleValueArray { get; protected set; }
        public Color[] PageCategoryColors { get; protected set; }
        #endregion
        #region services
        public virtual IDemoRichControlService DemoRichControlService { get { return null; } }
        public virtual IBackstageViewService BackstageViewService { get { return null; } }
        #endregion
        #region commands
        public void NavigateToContacts(object parameter) {
            BackstageViewService.Close();
            NavigateTo("http://devexpress.com/Home/ContactUs.xml");
        }
        public void NavigateToHomeSite(object parameter) {
            NavigateTo("http://www.devexpress.com");
        }
        public void NavigateToOnlineHelp() {
            BackstageViewService.Close();
            NavigateTo("http://documentation.devexpress.com/#WPF/CustomDocument7895");
        }
        public void NavigateToCodeCentral() {
            BackstageViewService.Close();
            NavigateTo("http://www.devexpress.com/Support/Center/Example/ChangeFilterSet/1?FavoritesOnly=False&MyItemsOnly=False&MyTeamItemsOnly=False&TechnologyName=.NET&PlatformName=WPF&ProductName=DXRibbon%20for%20WPF&TicketType=Examples");
        }
        public void BackstageOpened() {
        }
        public void ContainerChanged() {
            SelectedImage = DemoRichControlService.GetViewModelFromContainer();
        }
        public void InsertImage(object source) {
            InlineImageViewModel image = InlineImageViewModel.Create(source.ToString());
            DemoRichControlService.InsertImage(image);
        }
        bool OnGrowFontCommandCanExecute() { return FontSize != null; }

        public virtual void GrowFont() { FontSize += 2; }
        public virtual void ShrinkFont() { FontSize = FontSize <= 2 ? FontSize : FontSize - 2; }

        bool OnShrinkFontCommandCanExecute() { return FontSize != null; }
        public void CreateBlankDocument() {
            BackstageViewService.Close();
            DemoRichControlService.Clear();
        }
        #endregion

        public RibbonSimplePadViewModel() {
            Initialize();
        }

        private void Initialize() {
            ClipartImages = new string[] {
                 "/RibbonDemo;component/Images/Clipart/caCompClient.png",
                 "/RibbonDemo;component/Images/Clipart/caCompClientEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caDatabaseBlue.png",
                 "/RibbonDemo;component/Images/Clipart/caDataBaseDisabled.png",
                 "/RibbonDemo;component/Images/Clipart/caDataBaseGreen.png",
                 "/RibbonDemo;component/Images/Clipart/caDataBaseViolet.png",
                 "/RibbonDemo;component/Images/Clipart/caInet.png",
                 "/RibbonDemo;component/Images/Clipart/caInetSearch.png",
                 "/RibbonDemo;component/Images/Clipart/caModem.png",
                 "/RibbonDemo;component/Images/Clipart/caModemEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caNetCard.png",
                 "/RibbonDemo;component/Images/Clipart/caNetwork.png",
                 "/RibbonDemo;component/Images/Clipart/caNetworkEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caServer.png",
                 "/RibbonDemo;component/Images/Clipart/caServerEnabled.png",
                 "/RibbonDemo;component/Images/Clipart/caWebCam.png"
            };
            RecentDocuments = new RecentItemViewModel[] {                
                new RecentItemViewModel("Recent Document 1", @"c:\My Documents\Recent Document 1.rtf"),
                new RecentItemViewModel("Recent Document 2", @"c:\My Documents\Recent Document 2.rtf"),
                new RecentItemViewModel("Recent Document 3", @"c:\My Documents\Recent Document 3.rtf"),
                new RecentItemViewModel("Recent Document 4", @"c:\My Documents\Recent Document 4.rtf")
            };

            ListMarkerStyles = new TextMarkerStyle[] { TextMarkerStyle.None, TextMarkerStyle.Disc, TextMarkerStyle.Circle, TextMarkerStyle.Square, TextMarkerStyle.Box, TextMarkerStyle.LowerRoman, TextMarkerStyle.UpperRoman, TextMarkerStyle.LowerLatin, TextMarkerStyle.UpperLatin, TextMarkerStyle.Decimal };
            FontSizes = new Nullable<double>[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30,
                    32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144
                };
            ShapeTypes = new InlineImageBorderType[] { InlineImageBorderType.None, InlineImageBorderType.Rectangle, InlineImageBorderType.Circle, InlineImageBorderType.Triangle, InlineImageBorderType.Star, InlineImageBorderType.LeftArrow, InlineImageBorderType.RightArrow, InlineImageBorderType.DownArrow, InlineImageBorderType.UpArrow };
            FontFamilies = GetFontFamilies();
            BorderWeightArray = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            ImageScaleValueArray = new string[] { "25%", "50%", "75%", "100%", "125%", "150%", "175%", "200%", "250%", "300%", "400%", "500%" };
            PageCategoryColors = new Color[] { Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Blue, Color.FromArgb(255, 75, 0, 130), Color.FromArgb(255, 238, 130, 238) };
        }
        string[] GetFontFamilies() {
            return Fonts.SystemFontFamilies.Select((f) => f.ToString()).OrderBy(f=>f).ToArray();
        }

        protected virtual void NavigateTo(string url) {
            try {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
            catch {
                
            }
        }
    }
}
