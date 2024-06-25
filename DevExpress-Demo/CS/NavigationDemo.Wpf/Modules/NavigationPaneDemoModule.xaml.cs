using System.Collections;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.DemoBase;

namespace NavigationDemo {
    [CodeFile("ViewModels/NavigationPaneViewModel.(cs)")]
    [CodeFile("ViewModels/NavigationPane/ContactsNavigationViewModel.(cs)")]
    [CodeFile("Views/Contacts.xaml")]
    [CodeFile("Views/Contacts.xaml.(cs)")]
    [CodeFile("ViewModels/NavigationPane/ContactsViewModel.(cs)")]
    [CodeFile("Views/Mail.xaml")]
    [CodeFile("Views/Mail.xaml.(cs)")]
    [CodeFile("ViewModels/NavigationPane/MailViewModel.(cs)")]
    [CodeFile("Views/Journal.xaml")]
    [CodeFile("Views/Journal.xaml.(cs)")]
    [CodeFile("Views/Notes.xaml")]
    [CodeFile("Views/Notes.xaml.(cs)")]
    [CodeFile("Views/Tasks.xaml")]
    [CodeFile("Views/Tasks.xaml.(cs)")]
    public partial class NavigationPaneDemoModule : AccordionDemoModule {
        public NavigationPaneDemoModule() {
            InitializeComponent();
        }
    }
    public class NavigationChildrenSelector : IChildrenSelector {
        IEnumerable IChildrenSelector.SelectChildren(object item) {
            if(item is GroupDescription) {
                return ((GroupDescription)item).Items;
            } else if(item is ItemDescription) {
                return ((ItemDescription)item).Items;
            }
            return null;
        }
    }
}
