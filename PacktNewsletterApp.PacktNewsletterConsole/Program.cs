using System;
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
