using System;

namespace PropertyManagementService.Model
{
    public class Improvement
    {
        public int ImprovementId { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Cost { get; set; }
    }
}