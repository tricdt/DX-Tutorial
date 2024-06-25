using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Grid;
using System.Windows.Data;
using DevExpress.Xpf.Editors;
using System.Xml.Serialization;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.DemoBase.Helpers;
using System.ComponentModel;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace TreeListDemo {
    public partial class Filtering : TreeListDemoModule {
        public Filtering() {
            InitializeComponent();
        }
    }

    public class FiltrationModuleViewModel : BindableBase {
        public FiltrationModuleViewModel() {
            InitData();
            ShowAutoFilterRow = true;
            ExpandNodesOnFiltering = true;
            ShowCriteriaInAutoFilterRow = true;
        }

        private void InitData() {
            Filters = new List<Filter>();
            Filters.Add(new Filter("All", ""));
            Filters.Add(new Filter("Administration", "Contains([JobTitle], 'Administrator')"));
            Filters.Add(new Filter("Older than 35", "[Age] > 35"));
            Filters.Add(new Filter("Male", "[Gender] = 'M'"));
            Filters.Add(new Filter("Female", "[Gender] = 'F'"));
            Filters.Add(new Filter("Upcoming Birthdays", "[BirthdayMarkVisibility] = 'True'"));
            SearchPanelModes = new List<ShowSearchPanelMode>();
            SearchPanelModes.Add(ShowSearchPanelMode.Always);
            SearchPanelModes.Add(ShowSearchPanelMode.Default);
            SearchPanelModes.Add(ShowSearchPanelMode.Never);
            FilterModes = new List<TreeListFilteringMode>();
            FilterModes.Add(TreeListFilteringMode.Nodes);
            FilterModes.Add(TreeListFilteringMode.ParentBranch);
            FilterModes.Add(TreeListFilteringMode.EntireBranch);
            FilterModes.Add(TreeListFilteringMode.Recursive);
        }

        public List<Filter> Filters { get; set; }
        public List<ShowSearchPanelMode> SearchPanelModes { get; set; }
        public List<TreeListFilteringMode> FilterModes { get; set; }

        bool showAutoFilterRowCore;
        public bool ShowAutoFilterRow {
            get { return showAutoFilterRowCore; }
            set { SetProperty(ref showAutoFilterRowCore, value, () => ShowAutoFilterRow); }
        }
        bool showCriteriaInAutoFilterRowCore;
        public bool ShowCriteriaInAutoFilterRow {
            get { return showCriteriaInAutoFilterRowCore; }
            set { SetProperty(ref showCriteriaInAutoFilterRowCore, value, () => ShowCriteriaInAutoFilterRow); }
        }
        bool expandNodesOnFilteringCore;
        public bool ExpandNodesOnFiltering {
            get { return expandNodesOnFilteringCore; }
            set { SetProperty(ref expandNodesOnFilteringCore, value, () => ExpandNodesOnFiltering); }
        }
    }

    public class Filter {
        public Filter(string name, string filterString) {
            Name = name;
            FilterString = filterString;
        }
        public string Name { get; private set; }
        public string FilterString { get; private set; }
    }
}
