using DevExpress.Xpf.Ribbon;
using System;
using System.Collections.Generic;
using System.Windows.Media;
namespace RibbonDemo {
    public class PainterWindowViewModel {        
        #region properties        
        public virtual Color PageCategoryColor { get; set; }
        public virtual RibbonPageCategoryCaptionAlignment PageCategoryAlignment { get; set; }
        public virtual RibbonQuickAccessToolbarShowMode ToolbarShowMode { get; set; }
        public virtual RibbonStyle RibbonStyle { get; set; }
        public IEnumerable<double?> FontSizes { get; protected set; }
        #endregion
        #region commands
        public void CloseWindow() {
            CloseWindowService.Close();
        }
        #endregion
        #region services
        public virtual ICloseWindowService CloseWindowService { get { return null; } }
        #endregion
        public PainterWindowViewModel() {
            RibbonStyle = DevExpress.Xpf.Ribbon.RibbonStyle.Office2010;
            PageCategoryColor = Colors.Orange;
            PageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Right;
            FontSizes = new Nullable<double>[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30,
                    32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144
                };
        }
    }
}
