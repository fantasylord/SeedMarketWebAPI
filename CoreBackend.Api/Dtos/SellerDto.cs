using CoreBackend.Api.Entities;
using System.Collections.Generic;

namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 卖家实体
    /// </summary>
    public class SellerDto
    {
        public string Name { set; get; }

        public string Detail { set; get; }

        /// <summary>
        /// 卖家
        /// </summary>
        public string MarketID { set; get; }
        /// <summary>
        /// 卖家编号 系统设置 唯一标识
        /// </summary>
        public int SellerID { set; get; }
        /// <summary>
        /// 营业状态
        /// </summary>
        public BusinessStatus IsStatus { set; get; }

        //public ICollection<Seed> Seeds { set; get; }
    }
    public class SellerCreation
    {
        public string Name { set; get; }

        public string Detail { set; get; }


        public string MarketID { set; get; }
        public int SellerID { set; get; }
        public BusinessStatus IsStatus { set; get; }
        public static Dictionary<string, bool> BuyerLogins { set; get; } = new Dictionary<string, bool>();
    }
}
