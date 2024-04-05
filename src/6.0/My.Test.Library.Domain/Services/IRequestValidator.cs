using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public interface IRequestValidator
    {
        ValidationResponse Validate(BookingRequest request);
    }
}