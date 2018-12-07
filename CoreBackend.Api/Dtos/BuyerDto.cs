using System;
using System.Collections.Generic;

namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 买家信息 
    /// </summary>
    public class BuyerDto
    {
     
        public string Name { set; get; }

        /// <summary>
        /// 积分信息
        /// </summary>
        public int Integral { set; get; }
        public string Account { set; get; }
        public string PW { set; get; }
        public string Tell { set; get; }
        public string Mail { set; get; }
        /// <summary>
        /// 自定义信息
        /// </summary>
        public string BusinessKey { set; get; }
        public DateTime LastTime { set; get; }
       public static  Dictionary<string, bool> BuyerLogins { set; get; } = new Dictionary<string, bool>();
    }
    /// <summary>
    /// 创建卖家信息
    /// </summary>
    public class BuyerCreation
    {

        /// <summary>
        /// 积分信息
        /// </summary>
        public int Integral { set; get; }
        public string Name { set; get; }
        public string Account { set; get; }
        public string PW { set; get; }
        public string Tell { set; get; }
        public string Mail { set; get; }
        public string BusinessKey { set; get; }
        public DateTime LastTime { set; get; }
    }


}
