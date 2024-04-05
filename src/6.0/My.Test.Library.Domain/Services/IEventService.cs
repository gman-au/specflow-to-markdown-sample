using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public interface IEventService
    {
        Task<Event> GetAsync(string eventCode);
    }
}