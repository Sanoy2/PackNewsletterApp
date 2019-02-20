using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PacktNewsletterApp.Data.MailSystem
{
    public class JsonRecipientsService : IRecipientsService
    {
        private readonly string jsonFile = "recipients.json";

        public JsonRecipientsService()
        {
            if (!File.Exists(jsonFile))
            {
                Console.WriteLine("Json with recipients does not exist.");
                Console.WriteLine("Creating json file with recipients.");
                Save(new RecipientsCollection());
            }
        }

        public RecipientsCollection GetAll()
        {
            var recipients = Load();
            return recipients;
        }

        public void Add(Recipient recipient)
        {
            var recipients = Load();
            if (!recipients.Recipients.Exists(n => n.EmailAddress.Equals(recipient.EmailAddress)))
            {
                recipients.Recipients.Add(recipient);
            }
            Save(recipients);
        }

        public void Enable(Recipient recipient)
        {
            var recipients = Load();
            if (recipients.Recipients.Exists(n => n.EmailAddress.Equals(recipient.EmailAddress)))
            {
                recipients.Recipients.Find(n => n.EmailAddress.Equals(recipient.EmailAddress)).Active = true;
            }
            Save(recipients);
        }

        public void Disable(Recipient recipient)
        {
            var recipients = Load();
            if (recipients.Recipients.Exists(n => n.EmailAddress.Equals(recipient.EmailAddress)))
            {
                recipients.Recipients.Find(n => n.EmailAddress.Equals(recipient.EmailAddress)).Active = false;
            }
            Save(recipients);
        }

        private RecipientsCollection Load()
        {
            var recipients = JsonConvert.DeserializeObject<RecipientsCollection>(File.ReadAllText(jsonFile));
            return recipients;
        }

        private void Save(RecipientsCollection collection)
        {
            File.WriteAllText(jsonFile, JsonConvert.SerializeObject(collection));
        }
    }
}