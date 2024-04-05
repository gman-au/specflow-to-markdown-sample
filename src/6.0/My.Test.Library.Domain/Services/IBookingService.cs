using System.Threading.Tasks;
using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public interface IBookingService
    {
        Task<BookingResponse> ProcessAsync(BookingRequest request);
    }
}