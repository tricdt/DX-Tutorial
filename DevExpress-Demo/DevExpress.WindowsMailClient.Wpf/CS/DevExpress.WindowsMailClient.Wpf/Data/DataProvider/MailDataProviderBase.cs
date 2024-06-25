using System.Collections.Generic;

namespace DevExpress.WindowsMailClient.Wpf.Data {
    public abstract class MailDataProviderBase : IDataProvider<MailMessage> {
        const string DefaultCompany = "DX-mail";
        const string DefaultEmail = "maildemo@dx-mail.com";

        string company = DefaultCompany;
        string email = DefaultEmail;
        IList<MailMessage> messages;

        public string Company { get { return this.company; } set { this.company = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
                
        public IEnumerable<MailMessage> GetItems() {
            return this.messages ?? (this.messages = CreateMessages());
        }

        protected abstract IList<MailMessage> CreateMessages();
    }
}
