namespace PacktNewsletterApp.Data.MailSystem
{
    public class Recipient
    {
        private string emailAddress;

        public string EmailAddress
        {
            get => emailAddress;
            set => emailAddress = FormEmailAddress(value);
        }

        public bool Active { get; set; }

        public Recipient()
        {
            Active = true;
        }

        public Recipient(string emailAddress)
        {
            Active = true;
            EmailAddress = emailAddress;
        }

        private string FormEmailAddress(string emailAddress)
        {
            return emailAddress.Trim();
        }

        public override string ToString()
        {
            return $"{nameof(EmailAddress)}: {EmailAddress}, {nameof(Active)}: {Active}";
        }
    }
}