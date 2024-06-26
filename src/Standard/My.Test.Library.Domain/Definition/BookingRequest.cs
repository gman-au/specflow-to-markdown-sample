﻿using System;

namespace My.Test.Library.Domain.Definition
{
    public class BookingRequest
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Guid EventCode { get; set; }
        
        public int NumberOfTickets { get; set; }
    }
}