using System;

namespace Common.Messages
{
    public class InventoryLevel
    {
        public InventoryLevel(int productId, int stockCount)
        {
            ProductId = productId;
            StockCount = stockCount;
            TimeStamp = DateTime.Now;
        }

        public int StockCount { get; private set; }
        public int ProductId { get; private set; }
        public DateTime TimeStamp { get; private set; }
    }
}
