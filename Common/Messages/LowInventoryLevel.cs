namespace Common.Messages
{
    public class LowInventoryLevel : InventoryLevel
    {
        public LowInventoryLevel(int productId, int stockCount)
            : base(productId, stockCount)
        {
        }
    }
}
