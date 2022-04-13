using System;

namespace PropertyManagementService.Model
{
    public class Rate
    {
        public int RateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Amount { get; set; }
    }
}