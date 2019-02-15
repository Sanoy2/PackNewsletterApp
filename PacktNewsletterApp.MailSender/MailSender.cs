using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MimeKit;

namespace PacktNewsletterApp.MailSender
{
    public class MailSender : IMailSender
    {
        static string[] Scope = { GmailService.Scope.GmailSend };
        static string ApplicationName = "PackNewsletterApp";

        public void Send(List<MimeMessage> messages)
        {
            SendAll(messages);
        }

        private void SendAll(List<MimeMessage> mimeMessages)
        {
            var gmailService = GetGmailService();
            var messages = TransformMimeMessages(mimeMessages);

            foreach (var message in messages)
            {
                gmailService.Users.Messages.Send(message, "me").Execute();
            }

            Console.WriteLine("Done");
        }

        private List<Message> TransformMimeMessages(List<MimeMessage> mimeMessages)
        {
            var messages = mimeMessages
            .Select(n => new Message() { Raw = Base64UrlEncode(n.ToString()) })
            .ToList();
            return messages;  
        }


        private GmailService GetGmailService()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart2.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scope,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

    }
}