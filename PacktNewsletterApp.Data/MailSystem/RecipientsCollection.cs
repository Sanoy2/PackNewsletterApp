using System.Collections.Generic;
using System.Text;
using PacktNewsletterApp.Data.MailSystem;

namespace PacktNewsletterApp.Data.MailSystem
{
    public class RecipientsCollection
    {
        public List<Recipient> Recipients { get; set; }

        public RecipientsCollection()
        {
            Recipients = new List<Recipient>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var recipient in Recipients)
            {
                builder.AppendLine(recipient.ToString());
            }

            return builder.ToString();
        }
    }
}