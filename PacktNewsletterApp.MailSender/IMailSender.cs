using System.Collections.Generic;
using MimeKit;

namespace PacktNewsletterApp.MailSender
{
    public interface IMailSender
    {
        void Send(List<MimeMessage> messages);
    }
}