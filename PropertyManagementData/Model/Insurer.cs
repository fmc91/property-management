using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagementData.Model
{
    public class Insurer
    {
        public int InsurerId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }
    }
}
