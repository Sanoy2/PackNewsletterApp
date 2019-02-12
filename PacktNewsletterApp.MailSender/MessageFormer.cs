using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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
            builder.HtmlBody =
$@"
<h1><a href=""https://www.packtpub.com/"">PacktPub</a> free learning offer for today:</h1>
<div>
<table>
<tr>
<td rowspan=""3"">
<img src=""{ebook.CoverUrl}"" alt=""Cover of the ebook"" style=""max-width: 15em"">
</td>
<td style=""padding: 5px"">
<h2>{ebook.Title}</h2>
</td>
</tr>
<tr>
<td style=""padding: 5px"">What you will learn:
<ul>{GenerateDescriptionHtmlUlBody(ebook.Description)}</ul>
</td>
</tr>
<tr>
<td style=""padding: 5px"">
<a href=""https://www.packtpub.com/packt/offers/free-learning"" style="" padding: 15px 25px; text-align:center;text-decoration: none;display:inline-block; color: white; background-color:#5594db; font-size:1.2em; min-width:10em"">Claim now</a>
</td>
</tr>
</table>
</div>
<div>
If you have any questions/suggestions or want to turn off this newsletter please contact me: krzysztof.tomkow@gmail.com
</div>
";

            message.Body = builder.ToMessageBody();

            return message;
        }
        private string GenerateDescriptionHtmlUlBody(List<string> description)
        {
            var builder = new StringBuilder();
            foreach (var item in description)
            {
                builder.Append($"<li>{item}</li>");
            }

            return builder.ToString();
        }
    }
}