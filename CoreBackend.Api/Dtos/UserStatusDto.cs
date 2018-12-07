namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 用户状态信息 如购物车功能
    /// </summary>
    public class UserStatusDto
    {
        /// <summary>
        /// 用户账户
        /// </summary>
        public string Account { set; get; }

      
        /// <summary>
        /// 种子编号
        /// </summary>
        public int SeedID { get; set; }
        /// <summary>
        /// 卖家编号
        /// </summary>
        public int SellerID { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; }
    }
}
