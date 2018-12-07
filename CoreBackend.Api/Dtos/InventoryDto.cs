using System.Collections.Generic;

namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 库存实体
    /// </summary>
    public class InventoryDto
    {
       
        public int SeedID { set; get; }
    
        public int SellerID { set; get; }
        /// <summary>
        /// 库存当前值
        /// </summary>
        public int Count { set; get; }
        /// <summary>
        /// 库存总量
        /// </summary>
        public int SumCount { set; get; }

       // public ICollection<InventoryDto> inventoryDtos { set; get; }

    }

    public class InventoryPut
    {
       
    }
    public class InventoryDelete
    {
        public int SeedID { set; get; }
        public int SellerID { set; get; }
        public ICollection<InventoryDelete> inventoryDeletes { set; get; }
    }

}
