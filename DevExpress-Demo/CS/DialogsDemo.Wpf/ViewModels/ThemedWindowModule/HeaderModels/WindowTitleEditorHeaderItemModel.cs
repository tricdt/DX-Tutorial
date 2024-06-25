using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogsDemo {
    public class WindowTitleEditorHeaderItemModel : BaseHeaderItemModel {
        public virtual string Title { get; set; }

        public WindowTitleEditorHeaderItemModel() { ResourceKey = "WindowTitleEditor"; }

        protected void OnTitleChanged() { BaseModel.Title = Title; }
    }
}
