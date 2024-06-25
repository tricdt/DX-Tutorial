using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using DevExpress.WindowsMailClient.Wpf.Internal;

namespace DevExpress.WindowsMailClient.Wpf.Data {
    public class DesignTimeMailDataProvider : MailDataProviderBase {
        protected override IList<MailMessage> CreateMessages() {
            IList<MailMessage> result = new List<MailMessage>();
            MailMessage message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddHours(19).AddMinutes(19);
            message.From = "John Heart";
            message.To = "peter_thorpe@dxmail.com";
            message.Subject = "AsyncMode for Pivot Grid";
            message.Text = DataHelper.GetMHTTextFromHTML("Cool will it work as normal if you use chart integration or need some modification?");
            result.Add(message);

            message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddHours(17).AddMinutes(54);
            message.From = "Kobus Smit";
            message.Subject = "XAF - Model Merge Tool";
            message.Text = DataHelper.GetMHTTextFromHTML("Great! This is an useful enhancement that will save us time. One more step making XAF more RAD.");
            result.Add(message);

            message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddHours(17).AddMinutes(11);
            message.From = "Tony Taylor-Moran";
            message.Subject = "WinForms Tab Control - Custom Buttons";
            message.Text = DataHelper.GetMHTTextFromHTML("Sorry, I don't agree with Chris at all, the assumption that Winforms is legacy doesn't site right with me, " +
                "quite a lot of my users are not interested, so I am more inclined to agree with the others. More attention really" +
                " does need to be given to the Winforms collection of controls and in particular the MDITab control (My personal request!), the recent" +
                " offerings haven't seemed to give me much for the money (although I have a DXperience Enterprise Subscription, I really only use the " +
                "WinForms controls) - &amp;nbsp;my subscription is due for renewal in May, unless I see something that is worth the renewal fee, I will " +
                "give one of the other vendors a try.");
            message.IsUnread = true;
            result.Add(message);

            message = new MailMessage();
            message.Company = Company;
            message.Email = Email;
            message.Date = DateTime.Today.Date.AddDays(-2).AddHours(14).AddMinutes(20);
            message.From = "Mehul Harry (DevExpress)";
            message.Subject = "ASP.NET MVC - Code Usability Improvement";
            message.Text = DataHelper.GetMHTTextFromHTML("Graeme, Thanks! And it's possible so we'll consider it for a future release.");
            message.IsUnread = true;
            result.Add(message);
            return result;
        }
    }
}
