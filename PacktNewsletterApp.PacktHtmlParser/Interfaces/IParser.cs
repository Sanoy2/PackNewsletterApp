using PacktNewsletterApp.Models.Data;

namespace PacktNewsletterApp.PacktHtmlParser.Interfaces
{
    public interface IParser
    {
        Ebook Parse();
    }
}