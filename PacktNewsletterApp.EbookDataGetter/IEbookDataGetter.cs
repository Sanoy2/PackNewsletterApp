using System.Threading.Tasks;
using PacktNewsletterApp.Data.Models;

namespace PacktNewsletterApp.EbookDataGetter
{
    public interface IEbookDataGetter
    {
        Ebook GetEbook();
        Task<Ebook> GetEbookAsync();
    }
}