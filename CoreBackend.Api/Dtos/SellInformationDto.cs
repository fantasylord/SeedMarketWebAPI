using System;

namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 销售信息表
    /// </summary>
    public class SellInformationDto
    {
        public int SelledID { set; get; }

        public int SeedID { set; get; }

        public int SellerID { set; get; }

        public int SellCount { set; get; }

        public DateTime SellTime { set; get; }
        /// <summary>
        /// 外键标识 订单表
        /// </summary>
        public int IndentID { set; get; }

        

    }
}
