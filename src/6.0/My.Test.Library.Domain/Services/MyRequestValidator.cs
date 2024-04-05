using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public class MyRequestValidator : IRequestValidator
    {
        public ValidationResponse Validate(BookingRequest request)
        {
            var response = new ValidationResponse
            {
                IsValid = true,
                Message = null
            };

            if (string.IsNullOrEmpty(request.FirstName))
            {
                response.IsValid = false;
                response.Message = MessageConstants.InvalidFirstName;
                return response;
            }
            
            if (request.NumberOfTickets <= 0)
            {
                response.IsValid = false;
                response.Message = MessageConstants.InvalidTicketAmount;
                return response;
            }

            return response;
        }
    }
}