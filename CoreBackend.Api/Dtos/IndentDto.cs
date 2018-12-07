using System;

namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 订单
    /// </summary>
    public class IndentDto
    {
        /// <summary>
        /// 编号 系统设定 无须设置
        /// </summary>
        public int IndentID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string Account { set; get; }
        /// <summary>
        /// 卖家编号
        /// </summary>
        public int SellerID { set; get; }
        /// <summary>
        /// 种子编号
        /// </summary>
        public int SeedID { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public float Price { set; get; }
        /// <summary>
        /// 总价
        /// </summary>
        public float Amount { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { set; get; }
        /// <summary>
        /// 订单状态
        ///   所有状态 = 0,
        /// 处理中,
        /// 完成,
        /// 关闭,
        /// 异常,
        /// 创建
        /// </summary>
        public IndentStatus Status { set; get; }

        public DateTime CreatTIme { set; get; }
        /// <summary>
        /// 完成时间由系统设置
        /// </summary>
        public DateTime FinishedTime { set; get; }



    }
    public class GerIndentModel
    {

        public int IndentID { set; get; }
        public string Account { set; get; }
        public int SeedID { set; get; }
        public IndentStatus Status { set; get; }
    }
}
