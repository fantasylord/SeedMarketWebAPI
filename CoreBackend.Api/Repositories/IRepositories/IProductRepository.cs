using CoreBackend.Api.Entities;
using CoreBackend.Api.Entity;
using System.Collections.Generic;

namespace CoreBackend.Api.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// 获取某个产品下某个原料数据
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
         Material GetMaterialForProduct(int productId, int materialId);


        /// <summary>
        /// 获取某个产品所有原料数据
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
         IEnumerable<Material> GetMaterialsForProduct(int productId);
       
    
        /// <summary>
        /// 获取某个产品
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="includeMaterials">是否包含原材料</param>
        /// <returns></returns>
         Product GetProduct(int productId, bool includeMaterials=false);


        /// <summary>
        /// 获取所有产品信息
        /// </summary>
        /// <returns></returns>
         IEnumerable<Product> GetProducts();

       /// <summary>
       /// 获取指定条数数据 该方法默认orderby id
       /// </summary>
       /// <param name="page"></param>
       /// <param name="size"></param>
       /// <returns></returns>
        IEnumerable<Product> GetProducts(int page=0,int size=50);

        void AddProduct(Product product);
        /// <summary>
        /// 转存数据库是否成功标识
        /// </summary>
        /// <returns></returns>
        bool Save();

    
        void DeleteProduct(Product product);
    }
}
