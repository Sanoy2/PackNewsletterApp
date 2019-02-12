using System;
using PacktNewsletterApp.Data;
using PacktNewsletterApp.Data.MailSystem;
using PacktNewsletterApp.MailSender;
using PacktNewsletterApp.PacktHtmlParser;

namespace PacktNewsletterApp.PacktNewsletterConsole
{
    public class Worker
    {
        private Parser parser;
        private IMailSender mailSender;
        private IMessageFormer messageFormer;
        private IRecipientsService recipientsService;

        public Worker()
        {
            parser = new Parser();
            mailSender = new MailSender.MailSender();
            messageFormer = new MessageFormer();
            recipientsService = new JsonRecipientsService();
        }

        public Worker(Parser parser, IMailSender mailSender, IMessageFormer messageFormer, IRecipientsService recipientsService)
        {
            this.parser = parser;
            this.mailSender = mailSender;
            this.messageFormer = messageFormer;
            this.recipientsService = recipientsService;
        }

        public void Run()
        {
            try
            {
                var ebook = parser.Parse();
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