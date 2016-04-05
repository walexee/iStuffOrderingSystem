namespace Common.Messages
{
    public class InventoryReplenished : InventoryLevel
    {
        public InventoryReplenished(int productId, int stockCount) 
            : base(productId, stockCount)
        {
        }

    }
}
