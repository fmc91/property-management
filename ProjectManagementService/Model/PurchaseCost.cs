using PropertyManagementCommon;

namespace PropertyManagementService.Model
{
    public class PurchaseCost
    {
        public int PurchaseCostId { get; set; }

        public PurchaseCostKind Kind { get; set; }

        public double Amount { get; set; }
    }
}