using System;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using DevExpress.WindowsMailClient.Wpf.Internal;
using System.Windows.Media;
using System.Globalization;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class MailMessageViewModel {
        public static MailMessageViewModel Create() {
            return ViewModelSource.Create(() => new MailMessageViewModel());
        }
        public static MailMessageViewModel Create(string type, string from, string email, string company) {
            MailMessageViewModel result = Create();
            result.Email = email;
            result.Company = company;
            result.From = from;
            result.Type = type;
            return result;
        }
        
        public virtual string Type { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string From { get; set; }
        [EmailAddressValidation(true)]
        public virtual string To { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Text { get; set; }
        public virtual bool IsUnread { get; set; }
        public virtual bool IsFocused { get; set; }

        public string Email { get; set; }
        public string Company { get; set; }
        [DependsOnProperties("Text")]
        public string PlainText { get; private set; }
        public string SenderName { get; private set; }
        public bool IsDraft { get; private set; }
        public ImageSource SenderPhoto { get; private set; }
        public bool EmptySenderPhoto { get; private set; }

        public string FullDateString { get { return Date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern); } }
        public string SenderInitials {
            get {
                if (string.IsNullOrEmpty(SenderName))
                    return SenderName;
                string[] names = SenderName.Split(' ');
                if (names == null || names.Length <= 1)
                    return SenderName.Substring(0, 1);
                return string.Format("{0}{1}", names[0][0], names[1][0]);
            }
        }
        public string FullName {
            get {
                if (this.IsInDesignMode())
                    return From;
                if (string.IsNullOrEmpty(From))
                    return SenderName;
                return string.Format("{0} ({1})", SenderName, From);
            }
        }
        
        protected virtual void OnFromChanged() {
            SenderName = From == Email ? Company : DataHelper.GetNameByEmail(From, this.IsInDesignMode());
            SenderPhoto = DataHelper.GetPictureByEmail(From, this.IsInDesignMode());
            EmptySenderPhoto = SenderPhoto == DataHelper.UnknownUserPicture;
        }
        protected virtual void OnTypeChanged() {
            if (Type != Data.MailFolders.Deleted)
                IsDraft = Type == Data.MailFolders.Drafts;
        }
        protected virtual void OnToChanged() {
            To = EmailValidationHelper.NormalizeEmailsString(To);
        }
        protected virtual void OnTextChanged() {
            if(string.IsNullOrEmpty(Text)) {
                PlainText = null;
                return;
            }
            var plainText = DataHelper.GetPlainTextFromMHT(Text).Replace("\r\n", " ");
            PlainText = plainText.Length > 200 ? string.Format("{0}...", plainText.Remove(197)) : plainText;
        }
    }
    class EmailAddressValidationAttribute : ValidationAttribute {
        public bool MultipleEmails { get; private set; }

        public EmailAddressValidationAttribute(bool multipleEmails) {
            MultipleEmails = multipleEmails;
        }

        public override bool IsValid(object value) {
            string mails = (string)value;
            if(string.IsNullOrWhiteSpace(mails))
                return false;

            if(!MultipleEmails)
                return EmailValidationHelper.IsEmailValid(mails);

            bool result = true;
            bool hasValues = false;

            foreach(string email in EmailValidationHelper.ExtractEmails(mails)) {
                result &= EmailValidationHelper.IsEmailValid(email);
                hasValues = true;
            }
            return result && hasValues;
        }
    }
}
