//using AutoMapper;
//using CoreBackend.Api.Dtos;
//using CoreBackend.Api.Entity;
//using CoreBackend.Api.Repositories;
//using CoreBackend.Api.Services;
//using CoreBackend.Api.Services.Iservices;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http.ModelBinding;

//namespace CoreBackend.Api.Controllers
//{
//    /// <summary>
//    /// 产品数据返回控制器 已经废弃
//    /// </summary>
//    [Route("api/[controller]")]
//    public class ProductController : Controller
//    {
//        private readonly ILogger<ProductController> _logger;
//        private readonly IMailService _mailService;
//        private readonly IProductRepository _productRepository;
//        private readonly IMapper _Mapper;
//        public ProductController(ILogger<ProductController> logger,
//                                    IMailService mailService,
//                                    IProductRepository productRepository
//                                    )
//        {
//            _logger = logger;
//            _mailService = mailService;
//            _productRepository = productRepository;
       
            
//        }
//        /// <summary>
//        /// 获取所有商品
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet(Name ="GetProducts")]
//        public IActionResult GetJsonResultProducts()
//        {
//            var products = _productRepository.GetProducts();
//            var result = Mapper.Map<IEnumerable<ProductWithoutMaterialDto>>(products);
//            return Ok(result);
//        }

//        /// <summary>
//        /// 获取产品编号的详情
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="includeMaterial">指定是否携带材料信息</param>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("{id}", Name = "GetProduct")]//这里Route参数里面的{id}表示该action有一个参数名字是id.这个action的地址是: "/api/product/{id}"
//        public IActionResult GetJsonResultProduct(int id,
//            bool includeMaterial=false)
//        {
//            var product = _productRepository.GetProduct(id, includeMaterial);
//            if (product == null)
//            {
//                return NotFound();
//            }

//            if (includeMaterial)
//            {
//                var productWithMaterialResult = Mapper.Map<ProductDot>(product);

//                //var productWithMaterialResult = new ProductDot()
//                //{
//                //    Id = product.Id,
//                //    Name = product.Name,
//                //    Price = product.Price,
//                //    Description = product.Description
//                //};

//                //foreach (var item in product.Materials)
//                //{
//                //    productWithMaterialResult.Materials.Add(new MaterialDto
//                //    {
//                //        Id = item.Id,
//                //        Name = item.Name
//                //    });
//                //}
//                return Ok(productWithMaterialResult);
//            }

//            var onlyProductResult = Mapper.Map<ProductWithoutMaterialDto>(product);

//            //var onlyProductResult = new ProductDot
//            //{
//            //    Id = product.Id,
//            //    Name = product.Name,
//            //    Price = product.Price,
//            //    Description = product.Description
//            //};

//            return Ok(onlyProductResult);
            
//            //var result = new JsonResult(ProductService.Current.Products.SingleOrDefault(x => x.Id == id));
//            //if (result == null)
//            //{
//            //    return NotFound();
//            //}
//            //else
//            //    return Ok(result);

//        }

//        /// <summary>
//        /// post测试
//        /// </summary>
//        /// <param name="id">所需查找产品的ID</param>
//        /// <returns></returns> 
//        [HttpPost]
//        [Route("{id}", Name = "GetProduct")]
//        public IActionResult GetProduct(int id)
//        {
//            try
//            {
//                throw new Exception("Error! an Exception");
//                var product = ProductService.Current.Products.SingleOrDefault(x => x.ID == id);
//                if (product == null)
//                {
//                    _logger.LogInformation($"没有找到ID{id}的产品..");
//                    return NotFound();
//                }
//                return Ok(product);
//            }
//            catch (Exception e)
//            {
//                _logger.LogCritical($"查找ID为{id}的产品出现错误");
//                return StatusCode(500, "处理请求的时候发生了错误!");
//            }
//        }

//        /// <summary>
//        /// 提交model数据product
//        /// </summary>
//        /// <param name="product">产品 不含材料清单</param>
//        /// <returns></returns>
//        [HttpPost]
//        public IActionResult Post([FromBody] ProductCreation product)
//        {
//            if (product == null)
//            {
//                return BadRequest();
//            }
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var maxId = ProductService.Current.Products.Max(x => x.ID);

//            var newProduct = Mapper.Map<Product>(product);
//            _productRepository.AddProduct(newProduct);
           
         
//            if (!_productRepository.Save())
//            {
//                return StatusCode(500, "保存产品");
//            }
//            var dto = Mapper.Map<ProductWithoutMaterialDto>(newProduct);
//            return CreatedAtRoute("GetProduct", new { id=dto.ID}, dto);
//            //var newProduct = new ProductDot
//            //{
//            //    Id = ++maxId,
//            //    Name = product.Name,
//            //    Price = product.Price,
//            //    Description = product.Description
//            //};
//            //   ProductService.Current.Products.Add(newProduct);


//        }

//        /// <summary>
//        /// 修改(put)测试
//        /// </summary>
//        /// <param name="id">需要修改产品的id</param>
//        /// <param name="product">提交的修改模型</param>
//        /// <param name="includeMaterials">是否修改原材料</param>
//        /// <returns></returns>
//        [HttpPut("{id}/{includeMaterials}")]
//        public IActionResult Put(int id,bool includeMaterials, [FromBody] ProductModification product)
//        {
//            if (product == null)
//            {
//                return BadRequest();
//            }
//            if (product.Name == "产品")
//            {
//                ModelState.AddModelError("Name", "产品名称不可以是\"产品\"二字");
//            }
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            //var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
//            var model = _productRepository.GetProduct(id,includeMaterials);
//           // _productRepository.g
//            if (model == null)
//            {
//                return NotFound();
//            }
//            //这个方法会把第一个对象相应的值赋给第二个对象上
//            Mapper.Map(product, model);
//            if (!_productRepository.Save())
//            {
//                return StatusCode(500, "保存产品出错");
//            } 
//            return NoContent();
//            // 把ProductModification的属性都映射查询找到给Product,
//            //这个以后用AutoMapper来映射.
//            //ProductEntity->ProductDTO
//            //Mapper.Initialize(cfg => cfg.CreateMap<ProductEntity, ProductDTO>());
//            //var productDTO = Mapper.Map<ProductDTO>(productEntity);
//            //Mapper.Initialize(cfg => cfg.CreateMap<Product, Product>());
//            //---0-0-0-0-0-0-0-0-00-
//            //MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<ProductModification, Product>());
//            //var mapper = configuration.CreateMapper();
//            //model = mapper.Map<Product>(product);
//            //--0-0-0-0-0

//            //Nocontent 默认无回传信息的回传通知方式，即不需要向客户端发送请求信息
//            return NoContent();
//        }

//        /// <summary>
//        /// 部分更新操作，例如只需要修改名字，需要用到JsonPatchDocument格式标准
//        /// op 表示操作, replace 是指替换; path就是属性名, value就是值.
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="patchDoc">jsonpatchDocunment</param>
//        /// <returns></returns>
//        [HttpPatch("{id}")]
//        //Http Patch 就是做部分更新的, 它的Request Body应该包含需要更新的属性名 和 值, 甚至也可以包含针对这个属性要进行的相应操作.
//        //针对Request Body这种情况, 有一个标准叫做 Json Patch RFC 6092, 它定义了一种json数据的结构 可以表示上面说的那些东西.
//        // Json Patch定义的操作包含替换, 复制, 移除等操作
//        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<ProductModification> patchDoc)
//        {
//            if (patchDoc == null)
//            {
//                return BadRequest();
//            }
//            var model = _productRepository.GetProduct(id);
//            if (model == null)
//            {
//                return NotFound();
//            }
//            var toPatch = Mapper.Map<ProductModification>(model);
         
//            //var toPatch = new ProductModification
//            //{
//            //    Name = model.Name,
//            //    Description = model.Description,
//            //    Price = model.Price
//            //};

//            //product转化成用于更新的ProductModification这个Dto,
//            //然后应用于Patch Document 就是指为toPatch这个model更新那些需要更新的属性, 
//            //是使用ApplyTo方法实现的.
//            patchDoc.ApplyTo(toPatch, ModelState);
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            //ModelState检查只针对标记有State的模型 而此传入模型不是标记好的model
//            if (toPatch.Name == "产品")
//            {
//                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
//            }
//            //使用TryValidateModel(xxx)对model进行手动验证, 结果也会反应在ModelState里面.
//            TryValidateModel(toPatch);
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            Mapper.Map(toPatch, model);
//            if (!_productRepository.Save())
//                return StatusCode(500, "更新出错！");
//            return NoContent();
//        }

//        /// <summary>
//        /// 删除产品
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            var model = _productRepository.GetProduct(id);
//            if (model == null)
//            {
//                return BadRequest("未查找到该ID");
//            }
//            _productRepository.DeleteProduct(model);
//            if(_productRepository.Save())
//            _mailService.Send("Product Deleted ", $"ID为{id}的产品被删除了");
//            return NoContent();
//        }
       
//    }

//}
