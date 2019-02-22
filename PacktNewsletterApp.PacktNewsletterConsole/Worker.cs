using System;
using PacktNewsletterApp.Data;
using PacktNewsletterApp.Data.MailSystem;
using PacktNewsletterApp.MailSender;
using PacktNewsletterApp.EbookDataGetter;
using PacktNewsletterApp.EbookDataGetter.FreeEbook;

namespace PacktNewsletterApp.PacktNewsletterConsole
{
    public class Worker
    {
        private readonly IEbookDataGetter freeEbookDataGetter;
        private readonly IMailSender mailSender;
        private readonly IMessageFormer messageFormer;
        private readonly IRecipientsService recipientsService;

        public Worker()
        {
            freeEbookDataGetter = new FreeEbookDataGetter();
            mailSender = new MailSender.MailSender();
            messageFormer = new MessageFormer();
            recipientsService = new JsonRecipientsService();
        }

        public Worker(IEbookDataGetter freeEbookDataGetter, IMailSender mailSender, IMessageFormer messageFormer, IRecipientsService recipientsService)
        {
            this.freeEbookDataGetter = freeEbookDataGetter;
            this.mailSender = mailSender;
            this.messageFormer = messageFormer;
            this.recipientsService = recipientsService;
        }

        public void Run()
        {
            try
            {
                var ebook = freeEbookDataGetter.GetEbook();
                var recipients = recipientsService.GetAll();
                var messages = messageFormer.FormMessages(recipients, ebook);
                mailSender.Send(messages);
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{DateTime.Now} Argh exception!");
                System.Console.WriteLine($"Message: {e.Message}");
                System.Console.WriteLine($"Stack trace: {e.StackTrace}");
                System.Console.WriteLine("**********************************");
                System.Console.WriteLine();
            }
        }
    }
}