using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace BarsDemo {
    public partial class ContainerItems : BarsDemoModule {
        public static readonly DependencyProperty BarLinkContainerItemProperty =
            DependencyPropertyManager.Register("BarLinkContainerItem", typeof(BarLinkContainerItem), typeof(ContainerItems), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ToolbarListItemProperty =
            DependencyPropertyManager.Register("ToolbarListItem", typeof(ToolbarListItem), typeof(ContainerItems), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty NewItemProperty =
            DependencyPropertyManager.Register("NewItem", typeof(BarButtonItem), typeof(ContainerItems), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty OpenItemProperty =
            DependencyPropertyManager.Register("OpenItem", typeof(BarButtonItem), typeof(ContainerItems), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty SaveItemProperty =
            DependencyPropertyManager.Register("SaveItem", typeof(BarButtonItem), typeof(ContainerItems), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty PrintItemProperty =
            DependencyPropertyManager.Register("PrintItem", typeof(BarButtonItem), typeof(ContainerItems), new FrameworkPropertyMetadata(null));
        public BarLinkContainerItem BarLinkContainerItem {
            get { return (BarLinkContainerItem)GetValue(BarLinkContainerItemProperty); }
            set { SetValue(BarLinkContainerItemProperty, value); }
        }
        public ToolbarListItem ToolbarListItem {
            get { return (ToolbarListItem)GetValue(ToolbarListItemProperty); }
            set { SetValue(ToolbarListItemProperty, value); }
        }
        public BarButtonItem NewItem {
            get { return (BarButtonItem)GetValue(NewItemProperty); }
            set { SetValue(NewItemProperty, value); }
        }
        public BarButtonItem OpenItem {
            get { return (BarButtonItem)GetValue(OpenItemProperty); }
            set { SetValue(OpenItemProperty, value); }
        }
        public BarButtonItem SaveItem {
            get { return (BarButtonItem)GetValue(SaveItemProperty); }
            set { SetValue(SaveItemProperty, value); }
        }
        public BarButtonItem PrintItem {
            get { return (BarButtonItem)GetValue(PrintItemProperty); }
            set { SetValue(PrintItemProperty, value); }
        }

        public ContainerItems() {            
            InitializeComponent();
            NewItem = bNew;
            OpenItem = bOpen;
            SaveItem = bSave;
            PrintItem = bPrint;
            BarLinkContainerItem = lcStandard;
            ToolbarListItem = toolbarListItemCore;
            DataContext = this;
        }              
    }
}
