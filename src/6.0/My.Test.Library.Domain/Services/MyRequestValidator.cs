using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public class MyRequestValidator : IRequestValidator
    {
        public Task<ValidationResponse> ValidateAsync(BookingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}