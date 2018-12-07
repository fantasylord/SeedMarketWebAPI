using System;

namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 种子实体
    /// </summary>
    public class SeedDto
    {
       /// <summary>
       /// 种子编号无须设置
       /// </summary>
        public int SeedID { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 种子类别
        /// </summary>
        public SeedClass SeedClass { set; get; }

        /// <summary>
        /// 商家编号
        /// </summary>
        public string  MakertID { set; get; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string MakertName { set; get; }

        /// <summary>
        /// 详细信息
        /// </summary>
        public string Details { set; get; }
    
        /// <summary>
        /// 卖家编号
        /// </summary>
        public int  SellerID { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public float Price { set; get; }
        /// <summary>
        /// 自定义展示信息 默认json字符串存取
        /// </summary>
        public string Exhibitions { set; get; }
        /// <summary>
        /// 种子类别
        /// </summary>
        public SpeciesClass Species { set; get; }
        /// <summary>
        /// 产品品牌
        /// </summary>
        public string Brand { get; set; }

    }
    /// <summary>
    /// 带展示图片的种子实体
    /// </summary>
    public class SeedAndImgDto
    {

        public int SeedID { set; get; }
        public string Name { set; get; }
        public SeedClass SeedClass { set; get; }
        public string Details { set; get; }
        public string MakertName { set; get; }
        public int SellerID { set; get; }
        public float Price { set; get; }
   
        public string MakertID { set; get; }

        public string Exhibitions { set; get; }
        public SpeciesClass Species { set; get; }
        public string Brand { get; set; }
        public FileUpDownLoadToByteDto FileUpDownLoadDto { get; set; }
     

    }
}
