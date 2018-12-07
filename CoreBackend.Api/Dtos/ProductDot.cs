using System.Collections.Generic;

/// <summary>
/// Dto模块 (Data Transfer Object)
/// 提供服务器接收的数据转换为交互用Model
/// </summary>
namespace CoreBackend.Api.Dtos
{

    /// <summary>
    /// 产品模型原型
    /// </summary>
    public class ProductDot
    {
       

       /// <summary>
       /// 编号
       /// </summary>
        public int ID { get; set; } =-1;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } ="Null";

        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; } = 0;

        /// <summary>
        /// 获取产品材料清单
        /// </summary>
        public ICollection<MaterialDto> Materials { get; set; }

        public int MaterialCount => Materials.Count;

        public string Description { get; set; } = "value";

        public ProductDot()
        {
            Materials = new List<MaterialDto>();
        }
    }

}
