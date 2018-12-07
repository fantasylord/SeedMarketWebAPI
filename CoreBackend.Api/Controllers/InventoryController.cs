using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// 库存信息
    /// </summary>
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;

        public InventoryController(ILogger<InventoryController> logger,
                                IMailService mailService,
                                ISeedRepository seedRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _productRepository = seedRepository;
        }
        /// <summary>
        /// 取单个数据请主键标识完整
        /// </summary>
        /// <param name="seedId">泛型请传值-1 标识主键</param>
        /// <param name="sellerId">泛型请传值-1 标识主键</param>
        /// <returns></returns>
        [HttpGet("GetInventorys")]
        public IActionResult GetInventory(int seedId = -1, int sellerId = -1)
        {
            var model = _productRepository.GetInventories(sellerId, seedId);
            if (model == null)
                return BadRequest();
            List<InventoryDto> dto = new List<InventoryDto>();
            foreach (Inventory i in model)
            {
                dto.Add(new InventoryDto
                {
                    Count = i.Count,
                    SeedID = i.SeedID,
                    SellerID = i.SellerID,
                    SumCount = i.SumCount
                });
            }
            return Ok(model);

        }

        /// <summary>
        /// 添加库存信息
        /// </summary>
        /// <param name="inventoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddInvertory([FromBody] InventoryDto inventoryDto)
        {
            if (inventoryDto == null)
            {
                return BadRequest();
            }

            var model = new Inventory
            {
                Count = inventoryDto.Count,
                SeedID = inventoryDto.SeedID,
                SellerID = inventoryDto.SellerID,
                SumCount = inventoryDto.SumCount
            };

            _productRepository.AddInventory(model);
            if (!_productRepository.Save())
            {
                return BadRequest();
            }
            return NoContent();
        }
        /// <summary>
        /// 修改库存信息
        /// </summary>
        /// <param name="inventoryDtos"></param>
        /// <returns></returns>
        [HttpPut("inventorydtos/{sellerid}/{seedid}")]
        public IActionResult PutInvertorys(int sellerid,int seedid,[FromBody] InventoryDto inventoryDto)
        {

            if (inventoryDto == null||sellerid<0||seedid<0)
                return BadRequest();
            var model = _productRepository.GetInventory(sellerid, seedid);
            if (model == null)
            {
                return StatusCode(500, "没有该数据");
            }
            Inventory put= new Inventory
            {
                Count = inventoryDto.Count,
                SeedID = inventoryDto.SeedID,
                SellerID = inventoryDto.SellerID,
                SumCount = inventoryDto.SumCount
            };
            model.Count = put.Count;
            model.SeedID = put.SeedID;
            model.SellerID = put.SellerID;
            model.SumCount = put.SumCount;
           
            if (!_productRepository.Save())
                return StatusCode(500, "存储失败");
            return NoContent();

        }

        /// <summary>
        /// 修改库存信息
        /// </summary>
        /// <param name="sellerid"></param>
        /// <param name="seedid"></param>
        /// <param name="jsonPatch"></param>
        /// <returns></returns>
        [HttpPatch("{sellerid}/{seedid}")]
        public IActionResult PatchInvertory(int sellerid, int seedid, [FromBody] JsonPatchDocument<InventoryDto> jsonPatch)
        {
            if (jsonPatch == null)
                return BadRequest();
            var model = _productRepository.GetInventory(sellerid, seedid);
            if (model == null)
            {
                return StatusCode(500, "未找到");
            }
            InventoryDto topatch = new InventoryDto
            {
                Count = model.Count,
                SumCount = model.SumCount
            };
            jsonPatch.ApplyTo(topatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TryValidateModel(topatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.Count = topatch.Count;
   
            model.SumCount = topatch.SumCount;
            if (!_productRepository.Save())
            {
                return StatusCode(500, "更新错误");
            }
            return NoContent();

        }

        /// <summary>
        /// 删除库存信息
        /// </summary>
        /// <param name="sellerid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        [HttpDelete("{sellerid}/{seedid}")]
        public IActionResult DeleteInvertory(int sellerid = -1, int seedid = -1)
        {
            if (sellerid < 0 || seedid < 0)
                return BadRequest();
            var model = _productRepository.GetInventory(sellerid, seedid);
            if (model == null)
            {
                return StatusCode(500, "未找到");
            }
            _productRepository.DeleteInventory(model);
            if (!_productRepository.Save())
                return StatusCode(500, "存储错误");
            return NoContent();
        }

        ///// <summary>
        ///// 删除多条数据
        ///// </summary>
        ///// <param name="inventoryDeletes"></param>
        ///// <returns></returns>
        //[HttpGet("DeleteList")]
        //public IActionResult DeleteInventorys([FromBody] List<InventoryDelete> inventoryDeletes)
        //{
        //    var modellist=_productRepository.DeleteInventory()
        //}

    }
}
