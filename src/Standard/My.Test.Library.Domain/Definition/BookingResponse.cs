namespace My.Test.Library.Domain.Definition
{
    public class BookingResponse
    {
        public string ResponseMessage { get; set; }
        
        public string ConfirmationCode { get; set; }
        
        public int TicketsPurchased { get; set; }
        
        public bool IsSuccess { get; set; }
    }
}