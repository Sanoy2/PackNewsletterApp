using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PacktNewsletterApp.Models.Data
{
    public class Ebook
    {
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public List<string> Description { get; set; }

        public Ebook()
        {
            Description = new List<string>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("**********");
            builder.AppendLine($"Object of type: {this.GetType().Name}");
            builder.AppendLine($"{nameof(Title)}: {Title}");
            builder.AppendLine($"{nameof(CoverUrl)}: {CoverUrl}");
            builder.AppendLine($"{nameof(Description)}:");
            foreach (var item in Description)
            {
                builder.AppendLine($"    > {item}");
            }

            try
            {
                builder.AppendLine(new string('_', (int) (Description.LastOrDefault().Length * 1.1)));
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException Caught on ToString of {this.GetType().Name} class!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return builder.ToString();
        }
    }
}