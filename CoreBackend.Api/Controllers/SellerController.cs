using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// 卖家信息
    /// </summary>
    [Route("api/[controller]")]
    public class SellerController : Controller
    {
        private readonly ILogger<SellerController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;


        public SellerController(ILogger<SellerController> logger,
                                     IMailService mailService,
                                     ISeedRepository productRepository)
        {

            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }

        /// <summary>
        /// 卖家信息获取
        /// </summary>
        /// <param name="sellerid"></param>
        /// <returns></returns>
        [HttpGet("{sellerid}")]
        public IActionResult GetSeller(int sellerid)
        {
            var result = _productRepository.GetSeller(sellerid);
            if (result == null)
                return BadRequest();
            return Ok(result);

        }
        /// <summary>
        /// 添加卖家信息
        /// </summary>
        /// <param name="sellerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostSeller([FromBody] SellerDto sellerDto)
        {
            if (sellerDto == null)
                return BadRequest("null");
            var model = new Seller
            {

                IsStatus = sellerDto.IsStatus,
                Detail = sellerDto.Detail,
                MarkerID = sellerDto.MarketID,
                Name = sellerDto.Name,

            };
            _productRepository.AddSeller(model);
            if (!_productRepository.Save())
                return StatusCode(500, "插入失败");
            return NoContent();
        }
        /// <summary>
        /// 修改卖家信息
        /// </summary>
        /// <param name="sellerid"></param>
        /// <param name="sellerDto">参照sellerdto模型</param>
        /// <returns></returns>
        [HttpPut("{sellerid}")]
        public IActionResult PutSeller(int sellerid, [FromBody] SellerDto sellerDto)
        {
            if (sellerDto == null)
            {
                return BadRequest();
            }

            var putmodel = new Seller
            {

                IsStatus = sellerDto.IsStatus,
                Detail = sellerDto.Detail,
                MarkerID = sellerDto.MarketID,
                Name = sellerDto.Name
            };
            var model = _productRepository.GetSeller(sellerid);
            if (model == null)
                return BadRequest();

            model.Detail = putmodel.Detail;
            model.IsStatus = putmodel.IsStatus;
            model.Name = putmodel.Name;
          
            model.MarkerID = putmodel.MarkerID;
            if (!_productRepository.Save())
            {
                return StatusCode(500, "修改错误");

            }
            return Ok();

        }
        /// <summary>
        /// 修改卖家信息 如名称 品牌 营业状态
        /// </summary>
        /// <param name="sellerid"></param>
        /// <param name="jsonPatch"></param>
        /// <returns></returns>
        [HttpPatch("{sellerid}")]
        public IActionResult PatchSeller(int sellerid, [FromBody] JsonPatchDocument<SellerDto> jsonPatch)
        {

            if (jsonPatch == null)
            { return BadRequest(); }
            var model = _productRepository.GetSeller(sellerid);

            if (model == null)
            {
                return NotFound();
            }

            SellerDto patch = new SellerDto
            {
                Detail = model.Detail,
                IsStatus = model.IsStatus,
                MarketID = model.MarkerID,
                Name = model.Name

            };
            jsonPatch.ApplyTo(patch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TryValidateModel(patch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Name = patch.Name;
            model.IsStatus = patch.IsStatus;
            model.Detail = patch.Detail;
            model.MarkerID = patch.MarketID;
  

            if (!_productRepository.Save())
                return StatusCode(500, "更新出错");
            return NoContent();
        }
        /// <summary>
        /// 删除卖家信息
        /// </summary>
        /// <param name="sellerid"></param>
        /// <returns></returns>
        [HttpDelete("{sellerid}")]
        public IActionResult DeleteSeller(int sellerid)
        {
            if (sellerid < 0)
                return BadRequest();
            var model = _productRepository.GetSeller(sellerid);
            if (model == null)
            {
                return StatusCode(500,"无数据");
            }
            _productRepository.DeleteSeller(model);
            if (!_productRepository.Save())
            {
                return StatusCode(500,"删除失败");

            }
            return NoContent(); 
        }

    }
}