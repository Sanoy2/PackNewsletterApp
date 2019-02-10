using System.Collections.Generic;
using MimeKit;
using PacktNewsletterApp.Data.MailSystem;
using PacktNewsletterApp.Models.Data;

namespace PacktNewsletterApp.MailSender
{
    public interface IMessageFormer
    {
        MimeMessage FormMessageToMe();
        List<MimeMessage> FormMessages(RecipientsCollection recipients, Ebook ebook);
    }
}