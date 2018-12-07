using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// 用户状态信息表
    /// </summary>
    [Route("api/[controller]")]
    public class UserStatusController : Controller
    {
        private readonly ILogger<UserStatusController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;
        public UserStatusController(ILogger<UserStatusController> logger,
                                    IMailService mailService,
                                    ISeedRepository productRepository)
        {

            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }

        /// <summary>
        /// 获取用户状态表 如购物车
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet("{account}")]
        public IActionResult GetStatus(string account)
        {
            if (account == "")
                return BadRequest();
            var model = _productRepository.GetUserStatus(_productRepository.GetBuyer(account).Account);
            if (model == null)
            {
                return BadRequest();
            }
            List<UserStatusDto> result = new List<UserStatusDto>();
            foreach (UserStatus i in model)
            {
                result.Add(new UserStatusDto
                {
                    Account = i.Account,
                    Count = i.Count,
                    SeedID = i.SeedID,
                    Price = i.Price,
                    SellerID = i.SellerID
                });
            }
            return Ok(result);
        }
        /// <summary>
        /// 添加购物车记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userStatusDto"></param>
        /// <returns></returns>
        [HttpPost("{account}")]
        public IActionResult AddStatus(string account, [FromBody] UserStatusDto userStatusDto)
        {
            if (account == null)
            {
                return StatusCode(500, "错误信息");
            }
            var model = _productRepository.GetBuyer(account);
            if (model == null)
                return BadRequest();

            var status = new UserStatus
            {
                Account = userStatusDto.Account,
                Count = userStatusDto.Count,
                SeedID = _productRepository.GetSeed(userStatusDto.SeedID).SeedID,
                Price = _productRepository.GetSeed(userStatusDto.SeedID).Price,
                SellerID = _productRepository.GetSeed(userStatusDto.SellerID).SellerID

            };
            _productRepository.AddUserStatus(status);
            if (!_productRepository.Save())
                return StatusCode(500, "添加错误");
            return NoContent();
        }

        /// <summary>
        /// 删除单个请补全3个泛型值
        /// </summary>
        /// <param name="sellerid">泛型</param>
        /// <param name="sellid">泛型</param>
        /// <param name="account">泛型</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteUserStatus(int sellerid,int seedid, string account = "")
        {

            _productRepository.GetUserStatus(account, sellerid, seedid);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "删除错误");
            }
            return NoContent();

        }

        /// <summary>
        /// 修改用户某单个状态信息
        /// </summary>
        /// <param name="userStatusDto"></param>
        /// <param name="sellerid"></param>
        /// <param name="seedid"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut("{account}/{sellerid}/{seedid}")]
        public IActionResult PutUserStatis([FromBody] UserStatusDto userStatusDto, int sellerid, int seedid, string account = "")
        {
            if (userStatusDto == null)
                return BadRequest();
            var model=_productRepository.GetUserStatus(account, sellerid, seedid);
            if(model==null)
            { return BadRequest("无数据"); }
            //var result=new UserStatus
            //{
            //    Count=userStatusDto.Count,
            //    SeedID = _productRepository.GetSeed(userStatusDto.SeedID).SeedID,
            //    Price = _productRepository.GetSeed(userStatusDto.SeedID).Price,
            //    SellerID = _productRepository.GetSeed(userStatusDto.SellerID).SellerID
            //}
            model.Count = userStatusDto.Count;
            if (!_productRepository.Save())
                return StatusCode(500,"失败");
            if (model.Count == 0)
            { _productRepository.DeleteUserStatus(model);
                return Ok("count小于0自动删除");
            }
            return Ok();
        }

    }
}
