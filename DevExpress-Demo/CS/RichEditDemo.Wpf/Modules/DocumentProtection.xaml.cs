using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.XtraRichEdit.API.Native;
using System.Linq;

namespace RichEditDemo {
    public partial class DocumentProtection : RichEditDemoModule {
        readonly UserService userService = new UserService();

        public DocumentProtection() {
            InitializeComponent();
            richEdit.ReplaceService(this.userService);
        }

        void RichEditControl_Loaded(object sender, RoutedEventArgs e) {
            richEdit.DocumentLoaded += RichEditControl_DocumentLoaded;
            richEdit.DocumentSource = DemoUtils.GetRelativePath("DocumentProtection.docx");
        }
        void RichEditControl_DocumentLoaded(object sender, EventArgs e) {
            RangePermissionCollection rangePermissions = richEdit.Document.BeginUpdateRangePermissions();
            richEdit.Document.CancelUpdateRangePermissions(rangePermissions);
            List<String> users = rangePermissions
                .Select(rangePermission => rangePermission.UserName)
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToList();
            userService.Update(users);
            cbUserName.ItemsSource = users;
            if (users.Count > 0)
                cbUserName.EditValue = users[0];
        }
    }
}
