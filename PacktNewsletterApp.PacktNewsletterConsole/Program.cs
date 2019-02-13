using System;
using PacktNewsletterApp.Data;
using PacktNewsletterApp.Data.MailSystem;
using PacktNewsletterApp.Models;
using PacktNewsletterApp.MailSender;
using PacktNewsletterApp.PacktHtmlParser;
namespace PacktNewsletterApp.PacktNewsletterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"{DateTime.Now} Program starts");
            var worker = new Worker();
            worker.Run();
        }
    }
}
