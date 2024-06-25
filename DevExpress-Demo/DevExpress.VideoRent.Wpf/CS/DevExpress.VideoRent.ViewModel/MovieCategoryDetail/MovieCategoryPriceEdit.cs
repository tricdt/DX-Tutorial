using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoryPriceEdit : ModuleObjectEdit {
        public MovieCategoryPriceEdit(MovieCategoryPriceEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            RetrieveFormatIcon();
        }
        public MovieCategoryPriceEditObject MovieCategoryEditObject { get { return (MovieCategoryPriceEditObject)EditObject; } }
        public string FormatText { get { return EnumTitlesKeeper<MovieItemFormat>.GetTitle(MovieCategoryEditObject.CategoryPrice.Format); } }
        public ImageSource FormatIcon { get; set; }
        void RetrieveFormatIcon() {
#if SL
            ItemsSourceHelper.RequestFormatItems((result) =>
            {
                if(MovieCategoryEditObject.CategoryPrice == null) return;
#else
            List<FormatItem> result = ItemsSourceHelper.FormatsList;
#endif
                foreach(FormatItem item in result) {
                    if(item.Value == MovieCategoryEditObject.CategoryPrice.Format) {
                        FormatIcon = item.Icon;
                        break;
                    }
                }
#if SL
            });
#endif
        }
    }
}
