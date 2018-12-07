using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    ///  订单信息
    /// </summary>
    [Route("api/[controller]")]
    public class IndentController : Controller
    {
        private readonly ILogger<IndentController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;
        public IndentController(ILogger<IndentController> logger,
                                IMailService mailService,
                                ISeedRepository seedRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _productRepository = seedRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indentID"></param>
        /// <param name="account"></param>
        /// <param name="seedID"></param>
        /// <param name="statu">默认不传  状态值为 0=创建，1=处理中，2=完成，3=关闭，4=异常 </param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIndent")]
        public IActionResult GetIndent(int indentid)
        {
            var model = _productRepository.GetIndentForId(indentid);
            if (model == null)
                return BadRequest();
            IndentDto result = new IndentDto
            {
                Account = model.Account,
                Amount = model.Amount,
                CreatTIme = model.CreatTIme,
                Count = model.Count,
                Price = model.Price,
                FinishedTime = model.FinishedTime,
                IndentID = model.IndentID,
                SeedID = model.SeedID,
                Status = model.Status
            };
            return Ok(result);




        }
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="indentid">1</param>
        /// <param name="account" value="Testone">Testone</param>
        /// <param name="seedid">1</param>
        /// <param name="statu">默认不传  状态值为 0=创建，1=处理中，2=完成，3=关闭，4=异常 </param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIndents/{account}")]
        public IActionResult GetIndents(int indentid, string account, int seedid, IndentStatus status)
        {
            GerIndentModel gerIndent = new GerIndentModel
            {
                IndentID = indentid,
                Account = account,
                SeedID = seedid,
                Status = status,


            };
            if (gerIndent == null)
            {
                return BadRequest();
            }
            var model = _productRepository.GetIndents(gerIndent.Account);
            var result1 = new List<IndentDto>();
            var model2 = _productRepository.GetIndents(gerIndent.Account, gerIndent.Status);
            if (gerIndent.Status >= 0)
            {

                foreach (Indent i in model)
                {
                    result1.Add(new IndentDto
                    {
                        Account = i.Account,
                        Amount = i.Amount,
                        CreatTIme = System.DateTime.Now,
                        Count = i.Count,
                        Price = i.Price,
                        FinishedTime = System.DateTime.Now,
                        IndentID = i.IndentID,
                        SeedID = i.SeedID,
                        Status = i.Status,
                        SellerID = i.SellerID
                    });
                }

            }
            else
            {

                foreach (Indent i in model2)
                {
                    result1.Add(new IndentDto
                    {
                        Account = i.Account,
                        Count = i.Count,
                        Price = i.Price,
                        Amount = i.Amount,
                        CreatTIme = System.DateTime.Now,
                        FinishedTime = System.DateTime.Now,
                        IndentID = i.IndentID,
                        SeedID = i.SeedID,
                        Status = i.Status,
                        SellerID = i.SellerID

                    });
                }


            }
            return Ok(result1);
            // return Ok(_Mapper.Map<IndentDto>(_productRepository.GetIndents(gerIndent.Account, gerIndent.Status)));
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="indentDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostIndent([FromBody] IndentDto indentDto)
        {
            if (indentDto == null)
                return BadRequest();
            Indent model = new Indent();

            model.Account = indentDto.Account;
            model.Amount = indentDto.Amount;
            model.CreatTIme = System.DateTime.Now;
            model.Count = indentDto.Count;
            model.Price = _productRepository.GetSeed(indentDto.SeedID).Price;
            model.FinishedTime = System.DateTime.Now;
            model.SeedID = _productRepository.GetSeed(indentDto.SeedID).SeedID;
            model.Status = IndentStatus.创建;

            { var InventoryModel = _productRepository.GetInventory(model.SellerID, model.SeedID);
                if (InventoryModel.Count - model.Count < 0)
                    return BadRequest("库存不足"); }


            _productRepository.AddIndent(model);
            if (!_productRepository.Save())
            {
                return BadRequest("错误");
            }
            return Ok();
        }
        /// <summary>
        /// 修改订单信息 如状态信息
        /// </summary>
        /// <param name="indentid"></param>
        /// <param name="indentDto"></param>
        /// <returns></returns>
        [HttpPut("{indentid}")]
        public IActionResult PutIndent(int indentid, [FromBody] IndentDto indentDto)
        {

            if (indentDto == null)
                return BadRequest();
            Indent Put = new Indent
            {
                Account = indentDto.Account,
                Amount = indentDto.Amount,
                CreatTIme = indentDto.CreatTIme,
                FinishedTime = indentDto.FinishedTime,
                Count = indentDto.Count,
                Price = _productRepository.GetSeed(indentDto.SeedID).Price,
                SeedID = _productRepository.GetSeed(indentDto.SeedID).SeedID,
                Status = indentDto.Status,
                SellerID = _productRepository.GetSeller(indentDto.SellerID).SellerID,
            };
            var model = _productRepository.GetIndentForId(indentid);
            if (model == null)
            { return NotFound(); }


            var InventoryModel = _productRepository.GetInventory(Put.SellerID, Put.SeedID);
            if(InventoryModel==null)
            {
                return BadRequest("无此库存");
            }
            if (InventoryModel.Count - Put.Count < 0)
                return BadRequest("库存不足");
            model.Account = Put.Account;
            model.Amount = Put.Amount;
            model.CreatTIme = Put.CreatTIme;
            model.FinishedTime = Put.FinishedTime;
            model.SeedID = Put.SeedID;
            model.Status = Put.Status;
            model.SellerID = Put.SellerID;

            if (model.Status == IndentStatus.完成)
                _productRepository.AddSellInformation(new SellInformation
                {

                    Account = model.Account,
                    IndentID = model.IndentID,
                    SeedID = model.SeedID,
                    SellCount = model.Count,
                    SellerID = model.SellerID,
                    SellTime = model.FinishedTime
                });

         
            if (!_productRepository.Save())
            {
                return BadRequest();
            }

            return NoContent();

        }

        /// <summary>
        /// 部分更新操作，例如只需要修改名字，需要用到JsonPatchDocument格式标准
        /// op 表示操作, replace 是指替换; path就是属性名, value就是值.
        /// </summary>
        /// <param name="indentID"></param>
        /// <param name="jsonPatch"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult PatchIndent(int indentID, [FromBody] JsonPatchDocument<IndentDto> jsonPatch)
        {
            if (jsonPatch == null)
            { return BadRequest(); }
            var model = _productRepository.GetIndentForId(indentID);

            if (model == null)
            {
                return NotFound();
            }
            IndentDto patch = new IndentDto
            {
                Account = model.Account,
                Amount = model.Amount,
                CreatTIme = model.CreatTIme,
                Count = model.Count,
                FinishedTime = System.DateTime.Now,
                Status = model.Status,
                SeedID = _productRepository.GetSeed(model.SeedID).SeedID,
                Price = _productRepository.GetSeed(model.SeedID).Price


            };



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TryValidateModel(patch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Account = patch.Account;
            model.Amount = patch.Amount;
            model.CreatTIme = patch.CreatTIme;
            model.FinishedTime = patch.FinishedTime;
            model.SeedID = patch.SeedID;
            model.Status = patch.Status;

            if (!_productRepository.Save())
                return StatusCode(500, "更新出错");
            else
                if (model.Status != IndentStatus.异常)
            {
                _productRepository.AddSellInformation(new SellInformation
                {
                    Account = model.Account,
                    IndentID = model.IndentID,
                    SeedID = model.SeedID,
                    SellCount = model.Count,
                    SellerID = model.SellerID,
                    SellTime = model.FinishedTime

                });
                _productRepository.GetInventory(model.SellerID, model.SeedID).Count -= model.Count;
                if (!_productRepository.Save())
                    return StatusCode(500, "更新出错在向库存更新数目时出错");
            }
            return NoContent();
        }

        /// <summary>
        /// 添加订单信息
        /// </summary>
        /// <param name="indentDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addIndents")]
        public IActionResult addIndents([FromBody] List<IndentDto> indentDtos)
        {
            if (indentDtos == null)
                return BadRequest();

            List<Indent> indents = new List<Indent>();
            try
            {
                foreach (IndentDto indentDto in indentDtos)
                {

                    var InventoryModel = _productRepository.GetInventory(indentDto.SellerID, indentDto.SeedID);
                    if (InventoryModel.Count - indentDto.Count < 0)
                        return BadRequest($"商家编号{indentDto.SellerID}下编号为{indentDto.SeedID}的种子库存不足");
                    
                    indents.Add(new Indent
                    {
                        Account = indentDto.Account,
                        Amount = indentDto.Amount,
                        Count = indentDto.Count,
                        Price = _productRepository.GetSeed(indentDto.SeedID).Price,
                        CreatTIme = indentDto.CreatTIme,
                        FinishedTime = indentDto.FinishedTime,
                        SeedID = _productRepository.GetSeed(indentDto.SeedID).SeedID,
                        Status = IndentStatus.创建
                    });
                    InventoryModel.Count -= indentDto.Count;
                 
                }
                _productRepository.AddIndent(indents);
                if (!_productRepository.Save())
                {
                    return BadRequest("错误");
                }
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("传值错误");
                throw;
            }


        }

        /// <summary>
        ///删除订单
        /// </summary>
        /// <param name="indentID"></param>
        /// <returns></returns>
        [HttpDelete("{indentid}")]
        public IActionResult deleteIndent(int indentID)
        {

           
            var indent = _productRepository.GetIndentForId(indentID);
            if(indent.Status!=IndentStatus.异常)
            {
                var InventoryModel = _productRepository.GetInventory(indent.SellerID, indent.SeedID);
                InventoryModel.Count += indent.Count;
                if (!_productRepository.Save())
                {
                    return BadRequest("错误");
                }

            }
            if (indent != null)
                _productRepository.DeleteIndent(indent);
            if (!_productRepository.Save())
            {
                return BadRequest("错误");
            }
            return Ok("删除成功");


        }
    }
}

