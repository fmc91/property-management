using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class Tenant
    {
        public int TenantId { get; set; }

        public int TenancyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string Email { get; set; }
    }
}
