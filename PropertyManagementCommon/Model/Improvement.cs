using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class Improvement
    {
        public int ImprovementId { get; set; }

        public int PropertyId { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Cost { get; set; }
    }
}
