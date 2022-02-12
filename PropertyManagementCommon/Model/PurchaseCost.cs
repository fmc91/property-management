using System;
using System.Collections.Generic;
using System.Text;
using PropertyManagementCommon;

namespace PropertyManagementCommon.Model
{
    public class PurchaseCost
    {
        public int PurchaseCostId { get; set; }

        public int PropertyId { get; set; }

        public PurchaseCostType Type
        {
            get;
            set;
        }

        public double Amount { get; set; }
    }
}
