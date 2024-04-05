namespace My.Test.Library.Domain.Definition
{
    public class Event
    {
        public Guid EventCode { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public int TicketsRemaining { get; set; }
    }
}