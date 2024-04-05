using System;
using System.Threading.Tasks;
using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public class MyBookingService : IBookingService
    {
        private readonly IEventService _eventService;
        private readonly IRequestValidator _requestValidator;

        public MyBookingService(
            IEventService eventService,
            IRequestValidator requestValidator
        )
        {
            _eventService = eventService;
            _requestValidator = requestValidator;
        }

        public async Task<BookingResponse> ProcessAsync(BookingRequest request)
        {
            var response = new BookingResponse
            {
                ResponseMessage = "Your booking was successful",
                ConfirmationCode = null,
                TicketsPurchased = 0,
                IsSuccess = true
            };

            var validationResponse =
                _requestValidator
                    .Validate(request);

            if (!validationResponse.IsValid)
            {
                response.ResponseMessage = $"Error - {validationResponse.Message}";
                response.IsSuccess = false;
                return response;
            }

            var requestedEvent = 
                await 
                    _eventService
                        .GetAsync(request.EventCode);

            if (requestedEvent.Status == EventStatusEnum.Cancelled) {
                response.ResponseMessage = $"Error - {MessageConstants.EventIsCancelled}";
                response.IsSuccess = false;
                return response;
            }
            
            if (requestedEvent.TicketsRemaining == 0)
            {
                response.ResponseMessage = $"Error - {MessageConstants.EventIsFullyBooked}";
                response.IsSuccess = false;
                return response;
            }

            if (requestedEvent.TicketsRemaining < request.NumberOfTickets)
            {
                var tickets = 
                    requestedEvent
                        .TicketsRemaining;
                
                response.ResponseMessage = $"Partial Success - {tickets} {MessageConstants.TicketConfirmationSuffix}";
                response.TicketsPurchased = tickets;
                return response;
            }
            
            response.ResponseMessage = $"Success - {request.NumberOfTickets} {MessageConstants.TicketConfirmationSuffix}";
            response.TicketsPurchased = request.NumberOfTickets;

            response.ConfirmationCode = 
                Guid
                    .NewGuid()
                    .ToString();
            
            return response;
        }
    }
}