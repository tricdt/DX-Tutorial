using System;

namespace DevExpress.WindowsMailClient.Wpf.Data {
    public class MailMessage {
        public string Company { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool IsUnread { get; set; }
    }
}
