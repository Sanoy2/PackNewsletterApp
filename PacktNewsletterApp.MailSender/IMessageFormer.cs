using System.Collections.Generic;
using MimeKit;
using PacktNewsletterApp.Data.Models;
using PacktNewsletterApp.Data.MailSystem;


namespace PacktNewsletterApp.MailSender
{
    public interface IMessageFormer
    {
        MimeMessage FormMessageToMe();
        List<MimeMessage> FormMessages(RecipientsCollection recipients, Ebook ebook);
    }
}