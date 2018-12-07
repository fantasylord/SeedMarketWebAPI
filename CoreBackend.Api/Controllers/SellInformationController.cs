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
    /// 销售信息表
    /// </summary>
    [Route("api/[controller]")]
    public class SellInformationController:Controller
    {
        private readonly ILogger<SellInformationController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;


        public SellInformationController(ILogger<SellInformationController> logger,
                                     IMailService mailService,
                                     ISeedRepository productRepository)
        {

            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }
        /// <summary>
        /// 获取用户销售订单记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="indentid">订单编号 不传则返回所有</param>
        /// <returns></returns>
        [HttpGet("{account}")]
        public IActionResult getSellInfo(string account,int indentid)
        {
            
            var model = _productRepository.GetSellInformations(account, indentid);
            if (model == null)
             return    BadRequest("服务器无任何数据");
            List<SellInformationDto> result = new List<SellInformationDto>();
            foreach(SellInformation i in model)
            {
                result.Add(new SellInformationDto
                {
                    IndentID = i.IndentID,
                    SeedID = i.SeedID,
                    SellCount = i.SellCount,
                    SelledID = i.SelledID,
                    SellerID = i.SellerID,
                    SellTime = i.SellTime,
                });
            }
            return Ok(result);
        }
    }
}
