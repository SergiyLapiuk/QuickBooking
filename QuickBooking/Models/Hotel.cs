﻿namespace QuickBooking.Models
{
    public class Hotel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Location { get; set; }
        public double? Rating { get; set; } 
        public string Description { get; set; }
    }
}
