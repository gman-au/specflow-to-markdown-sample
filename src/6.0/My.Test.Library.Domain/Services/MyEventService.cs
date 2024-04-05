using My.Test.Library.Domain.Definition;

namespace My.Test.Library.Domain.Services
{
    public class MyEventService : IEventService
    {
        public Task<Event> GetAsync(string eventCode)
        {
            throw new NotImplementedException();
        }
    }
}