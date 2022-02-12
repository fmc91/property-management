using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class Property
    {
        public int PropertyId { get; set; }

        [Required]
        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        [Required]
        public string City { get; set; }

        public string County { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Country { get; set; }

        public int OwnerId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public double PurchasePrice { get; set; }

        public PropertyType PropertyType { get; set; }
    }
}
