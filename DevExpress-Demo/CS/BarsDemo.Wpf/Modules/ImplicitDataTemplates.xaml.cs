using DevExpress.Xpf.DemoBase;
using System;
using System.Collections.Generic;

namespace BarsDemo {

    [CodeFile("ViewModels/ImplicitDataTemplatesModel.(cs)")]
    public partial class ImplicitDataTemplates : BarsDemoModule {
        public ImplicitDataTemplates() {
            InitializeComponent();
            DataContext = new List<CommandModel> {
                new GroupModel() {
                    Caption = "File",
                    Glyph = "SvgImages/Icon Builder/Actions_Home.svg",
                    Commands = new List<CommandModel>() {
                        new CommandModel() { Caption = "New", Glyph = "SvgImages/Outlook Inspired/New.svg" },
                        new CommandModel() { Caption = "Open", Glyph = "SvgImages/Outlook Inspired/Open.svg" },
                        new CommandModel() { Caption = "Save", Glyph = "SvgImages/Outlook Inspired/Save.svg" }
                    }
                },
                new CommandModel() { Caption = "Settings", Glyph = "SvgImages/Scheduling/ViewSettings.svg" },
                new EditorModel() { Caption = "Search:", Glyph="SvgImages/Outlook Inspired/Find.svg" },
                new LabelModel() { Content = DateTime.Now }
            };
        }
    }
}
