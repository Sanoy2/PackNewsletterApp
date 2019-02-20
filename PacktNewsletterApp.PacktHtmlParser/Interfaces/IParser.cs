using PacktNewsletterApp.Data.Models;

namespace PacktNewsletterApp.PacktHtmlParser.Interfaces
{
    public interface IParser
    {
        Ebook Parse();
    }
}