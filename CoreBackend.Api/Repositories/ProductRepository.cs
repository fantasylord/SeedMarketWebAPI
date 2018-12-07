using CoreBackend.Api.Entities;
using CoreBackend.Api.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreBackend.Api.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly MyContext _myContext;
        public ProductRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

    
        /// <summary>
        /// 获取某个产品下某个原料数据
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public Material GetMaterialForProduct(int productId, int materialId)
        {
            return _myContext.Materials.FirstOrDefault(x => x.ProductId == productId&&x.ID == materialId);
        }

        /// <summary>
        /// 获取某个产品所有原料数据
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IEnumerable<Material> GetMaterialsForProduct(int productId)
        {
            return _myContext.Materials.Where(x => x.ProductId == productId).ToList<Material>();
        }

        /// <summary>
        /// 获取某个产品
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="includeMaterials"></param>
        /// <returns></returns>
        public Product GetProduct(int productId, bool includeMaterials=false)
        {
            if (includeMaterials)
            {
                return _myContext.Products
                    .Include(x => x.Materials).FirstOrDefault(x => x.ID == productId);
            }
            return _myContext.Products.Find(productId);
        }

        /// <summary>
        /// 获取所有产品信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            return _myContext.Products.OrderBy(x => x.ID).ToList();
        }
        /// <summary>
        /// 按照Id排号获取指定条数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts(int page = 0, int size = 50)
        {

            return _myContext.Products.OrderBy(x => x.ID).ToList();
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {


            try
            {
                _myContext.Products.Add(product);
            }
            catch (Exception e)
            {
                string s = e.ToString();
                throw;
            } 
        }

        /// <summary>
        /// 转存数据库是否成功标识
        /// </summary>
        /// <returns></returns>
        public  bool Save()
        {
             return  _myContext.SaveChanges() >= 0;
        }

        public void DeleteProduct(Product product)
        {
            _myContext.Products.Remove(product);
        }
    }
}
