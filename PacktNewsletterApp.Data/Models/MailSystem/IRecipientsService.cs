using System.Collections.Generic;

namespace PacktNewsletterApp.Data.MailSystem
{
    public interface IRecipientsService
    {
        RecipientsCollection GetAll();
        void Add(Recipient recipient);
        void Enable(Recipient recipient);
        void Disable(Recipient recipient);
    }
}