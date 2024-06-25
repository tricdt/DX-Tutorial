using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Commands;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;


namespace TreeListDemo {
    public partial class BandedLayout : TreeListDemoModule {
        public BandedLayout() {
            InitializeComponent();
        }
    }

    public class BandedViewViewModel {
        public IList<SpaceObjects> SpaceObjects { get { return SpaceObjectData.DataSource; } }
    }
}
