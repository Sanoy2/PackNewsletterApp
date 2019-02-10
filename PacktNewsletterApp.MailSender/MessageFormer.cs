using System.Collections.Generic;
using System.Linq;
using MimeKit;
using PacktNewsletterApp.Data.MailSystem;
using PacktNewsletterApp.Models.Data;

namespace PacktNewsletterApp.MailSender
{
    public class MessageFormer : IMessageFormer
    {
        private readonly string sender = "Krzysztof Tomków";
        private readonly string senderAddress = "krzysztof.tomkow@gmail.com";
        private readonly string messageTitle = "PacktPub free learning newsletter";

        public MimeMessage FormMessageToMe()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(sender, senderAddress));
            message.To.Add(new MailboxAddress(sender, senderAddress));
            message.Subject = "Something is wrong with the newsletter app";
            var builder = new BodyBuilder();
            builder.HtmlBody = "<h1> Something went wrong </h2>";

            message.Body = builder.ToMessageBody();

            return message;
        }

        public List<MimeMessage> FormMessages(RecipientsCollection recipients, Ebook ebook)
        {
            var activeRecipients = recipients.Recipients.Where(n => n.Active);
            return activeRecipients.Select(n => FormSingleMessage(n, ebook)).ToList();
        }

        private MimeMessage FormSingleMessage(Recipient recipient, Ebook ebook)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(sender, senderAddress));
            message.To.Add(new MailboxAddress(recipient.EmailAddress, recipient.EmailAddress));
            message.Subject = messageTitle;

            var builder = new BodyBuilder();
            builder.HtmlBody = $@"<h1> Hello </h2>
<h3>{ebook.Title}</h3>
<p> {ebook.CoverUrl} </p>
<p> {ebook.PublishDate} </p>
<div><ul><li>{ebook.Description[0]}</li><li>{ebook.Description[1]}</li><li>{ebook.Description[2]}</li></ul></div>
";

            message.Body = builder.ToMessageBody();

            return message;
        }
    }
}