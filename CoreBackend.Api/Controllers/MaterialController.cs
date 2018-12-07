//using AutoMapper;
//using CoreBackend.Api.Dtos;
//using CoreBackend.Api.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CoreBackend.Api.Services
//{
//   /// <summary>
//   /// 已经废弃
//   /// </summary>
//    [Route("api/product")]
//    public class MaterialController : Controller
//    {
//        private readonly IProductRepository _productRepository;

//        public MaterialController(IProductRepository productRepository)
//        {
//            _productRepository = productRepository;
//        }

//        [HttpGet("{productId}/materials")]
//        public IActionResult GetMaterials(int productId)
//        {

//            var materials = _productRepository.GetMaterialsForProduct(productId);
//            var results = Mapper.Map<IEnumerable<MaterialDto>>(materials);
//            //var results = materials.Select(material => new MaterialDto
//            //{
//            //    Id = material.Id,
//            //    Name = material.Name
//            //}).ToList();
//            return Ok(results);

//            //这样做可能会产生空异常 未在material之前判断production.material是否存在，
//            //虽然设计上并不会不不存
//            //var result = ProductService.Current.Products.SingleOrDefault(x => x.Id == productId).Materials;
//            //var result = ProductService.Current.Products.SingleOrDefault(x => x.Id == productId);
//            //if (result == null)
//            //{
//            //    return NotFound();
//            //}
//            //return Ok(result.Materials);
//        }

//        [HttpGet("{productId}/materials/{id}")]
//        public IActionResult GetMaterial(int productId, int id)
//        {

//            ///注意GetMaterials方法内，
//            ///我们往productRepository的GetMaterialsForProduct传进去一个productId，
//            ///如果repository返回的是空list可能会有两种情况：
//            ///1 product不存在，2 product存在，
//            ///而它没有下属的material。
//            ///如果是第一种情况，那么应该返回的是404 NotFound，
//            ///而第二种action应该返回一个空list。
//            ///所以我们需要一个方法判断product是否存在，所以打开ProductRepository，添加方法：

//            var material = _productRepository.GetMaterialForProduct(productId, id);
//            if (material == null)
//            {
//                return NotFound();
//            }
//            var result = Mapper.Map<MaterialDto>(material);
//            return Ok(result);

//            //var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == productid);
//            //if (product == null)
//            //{
//            //    return NotFound();
//            //}
//            //var result = product.Materials.SingleOrDefault(x => x.Id == id);
//            //if (result == null)
//            //{
//            //    return NotFound();
//            //}
//            //return Ok(result);
//        }

//    }
//}
