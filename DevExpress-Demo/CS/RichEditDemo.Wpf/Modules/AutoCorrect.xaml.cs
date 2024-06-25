using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Office.Utils;
using System.Reflection;
using DevExpress.Xpf.DemoBase.Helpers;

namespace RichEditDemo {
    public partial class AutoCorrect : RichEditDemoModule {
        public AutoCorrect() {
            InitializeComponent();
        }

        void RichEditControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            IAutoCorrectService service = richEdit.GetService<IAutoCorrectService>();
            if (service != null) {
                AutoCorrectReplaceInfoCollection replaceTable = new AutoCorrectReplaceInfoCollection();
                replaceTable.Add("(C)", "©");
                Assembly assembly = GetType().Assembly;
                replaceTable.Add(new AutoCorrectReplaceInfo(":)", OfficeImage.CreateImage(assembly.GetManifestResourceStream(DemoHelper.GetPath("RichEditDemo.", assembly) + "smile.png"))));
                replaceTable.Add("pctus", "Please do not hesitate to contact us again in case of any further questions.");
                replaceTable.Add("wnwd", "well-nourished, well-developed");
                service.SetReplaceTable(replaceTable);
            }
        }
    }
}
