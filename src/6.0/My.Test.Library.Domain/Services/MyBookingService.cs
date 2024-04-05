using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public class MyBookingService : IBookingService
    {
        private readonly IEventService _eventService;

        public MyBookingService(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<BookingResponse> ProcessAsync(BookingRequest request)
        {
            return new BookingResponse
            {

            };
        }
    }
}