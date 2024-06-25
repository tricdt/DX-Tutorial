using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using DevExpress.WindowsMailClient.Wpf.Internal;

namespace DevExpress.WindowsMailClient.Wpf.Data {
    public class XmlMailDataProvider : MailDataProviderBase {
        DataSet messagesDataSet;

        public XmlMailDataProvider(string mailPath = "MailDevAv.xml") {
            this.messagesDataSet = InitDataSet(mailPath);
        }

        protected override IList<MailMessage> CreateMessages() {
            List<MailMessage> result = new List<MailMessage>();
            DataTable messagesTable = messagesDataSet.Tables["Messages"];
            if (messagesTable == null) {
                ReleaseDataSet();
                return result;
            }
            DataRowCollection rows = messagesTable.Rows;
            FillInboxAndArchiveMessages(rows, result, 5);
            FillSentMessages(result);
            FillDraftMessages(result);
            FillReplyMessages(rows, result, 4, MailFolders.Sent);
            FillForwardMessages(rows, result, 3, MailFolders.Drafts);
            ReleaseDataSet();
            return result;
        }
        
        DataSet InitDataSet(string path) {
            var result = new DataSet();
            string fullPath = FilePathHelper.GetFullPath(path);
            if (fullPath != string.Empty) 
                result.ReadXml(new FileInfo(fullPath).FullName);
            return result;
        }
        void ReleaseDataSet() {
            messagesDataSet.Dispose();
            messagesDataSet = null;
        }

        void FillInboxAndArchiveMessages(DataRowCollection rows, List<MailMessage> messages, int archiveCount) {
            int rowsCount = rows.Count;
            if (rowsCount <= 0)
                return;
            if (archiveCount < rowsCount) {
                for (int i = 0; i < archiveCount; i++) {
                    MailMessage message = CreateMessage(rows[i]);
                    message.Type = MailFolders.Archive;
                    messages.Add(message);
                }
                for (int i = archiveCount; i < rowsCount; i++) {
                    MailMessage message = CreateMessage(rows[i]);
                    message.Type = MailFolders.Inbox;
                    messages.Add(message);
                }
            } else {
                for (int i = 0; i < rowsCount; i++) {
                    MailMessage message = CreateMessage(rows[i]);
                    message.Type = MailFolders.Inbox;
                    messages.Add(message);
                }
            }
        }
        void FillSentMessages(List<MailMessage> messages) {
            MailMessage message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddHours(-17).AddMinutes(11);
            message.From = Email;
            message.To = "peter_thorpe@dxmail.com";
            message.Subject = "WinForms Tab Control - Custom Buttons";
            message.Text = DataHelper.GetMHTTextFromHTML("Sorry, I don't agree with Chris at all, the assumption that Winforms is legacy doesn't site right with me, " +
                "quite a lot of my users are not interested, so I am more inclined to agree with the others. More attention really" +
                " does need to be given to the Winforms collection of controls and in particular the MDITab control (My personal request!), the recent" +
                " offerings haven't seemed to give me much for the money (although I have a DXperience Enterprise Subscription, I really only use the " +
                "WinForms controls) - &amp;nbsp;my subscription is due for renewal in May, unless I see something that is worth the renewal fee, I will " +
                "give one of the other vendors a try.");
            message.Type = MailFolders.Sent;
            messages.Add(message);

            message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddDays(-2).AddHours(14).AddMinutes(20);
            message.From = Email;
            message.To = "peter_thorpe@dxmail.com";
            message.Subject = "ASP.NET MVC - Code Usability Improvement";
            message.Text = DataHelper.GetMHTTextFromHTML("Thanks! And it's possible so we'll consider it for a future release.");
            message.Type = MailFolders.Sent;
            messages.Add(message);
        }
        void FillReplyMessages(DataRowCollection rows, List<MailMessage> messages, int replyCount, string type) {
            if (rows.Count > replyCount) {
                for (int i = 0; i < replyCount; i++) {
                    MailMessage message = CreateReplyMessage(CreateMessage(rows[i]));
                    message.Type = type;
                    messages.Add(message);
                }
            }
        }
        void FillDraftMessages(List<MailMessage> messages) {
            MailMessage message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddHours(-19).AddMinutes(19);
            message.From = Email;
            message.To = "peter_thorpe@dxmail.com";
            message.Subject = "AsyncMode for Pivot Grid";
            message.Text = DataHelper.GetMHTTextFromHTML("Cool will it work as normal if you use chart integration or need some modification?");
            message.Type = MailFolders.Drafts;
            messages.Add(message);

            message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddHours(-17).AddMinutes(54);
            message.From = Email;
            message.To = "peter_thorpe@dxmail.com";
            message.Subject = "XAF - Model Merge Tool";
            message.Text = DataHelper.GetMHTTextFromHTML("Great! This is an useful enhancement that will save us time. One more step making XAF more RAD.");
            message.Type = MailFolders.Drafts;
            messages.Add(message);
        }
        void FillForwardMessages(DataRowCollection rows, List<MailMessage> messages, int forwardCount, string type) {
            if (rows.Count > forwardCount) {
                for (int i = 0; i < forwardCount; i++) {
                    MailMessage message = CreateForwardMessage(CreateMessage(rows[i]));
                    message.Type = type;
                    messages.Add(message);
                }
            }
        }

        MailMessage CreateMessage(DataRow row) {
            MailMessage message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Now.AddDays((int)row["Day"]).AddSeconds(-new Random().Next(10000));
            message.From = string.Format("{0}", row["From"]);
            message.To = Email;
            message.Subject = string.Format("{0}", row["Subject"]);
            message.Text = string.Format("{0}", row["Text"]);
            message.IsUnread = (DateTime.Now - message.Date) < TimeSpan.FromHours(200);
            return message;
        }
        MailMessage CreateReplyMessage(MailMessage originalMessage) {
            MailMessage result = new MailMessage();
            result.Company = Company;
            result.Email = Email;
            result.Subject = MailMessageHelper.CreateReplySubject(originalMessage.Subject);
            result.To = MailMessageHelper.CreateReplyAddress(originalMessage.From);
            result.From = Email;
            result.Date = originalMessage.Date;
            result.Text = MailMessageHelper.CreateReplyText(originalMessage.From, originalMessage.To, originalMessage.Subject, originalMessage.Text, originalMessage.Date);
            return result;
        }
        MailMessage CreateForwardMessage(MailMessage originalMessage) {
            MailMessage result = new MailMessage();
            result.Company = Company;
            result.Email = Email;
            result.Subject = MailMessageHelper.CreateForwardSubject(originalMessage.Subject);
            result.From = Email;
            result.Date = originalMessage.Date;
            result.Text = MailMessageHelper.CreateForwardText(originalMessage.From, originalMessage.To, originalMessage.Subject, originalMessage.Text);
            return result;
        }
    }
}
