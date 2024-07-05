using System.Threading.Tasks;

namespace Mofleet.Domain.ContactUses
{
    public interface IContactUsManager
    {
        Task<bool> IsAnyCotactUSFound();
        Task<ContactUs> GetContactUs();

    }
}