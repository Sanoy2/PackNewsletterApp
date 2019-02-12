using System.Collections.Generic;
using MimeKit;
using PacktNewsletterApp.Models.Data;

namespace PacktNewsletterApp.MailSender
{
    public interface IMailSender
    {
        void Send(List<MimeMessage> messages);
    }
}