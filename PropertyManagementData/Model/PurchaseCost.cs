using System;
using System.Collections.Generic;
using System.Text;
using PropertyManagementCommon;

namespace PropertyManagementData.Model
{
    public class PurchaseCost
    {
        public int PurchaseCostId { get; set; }

        public int PropertyId { get; set; }

        public PurchaseCostKind Kind { get; set; }

        public double Amount { get; set; }
    }
}
