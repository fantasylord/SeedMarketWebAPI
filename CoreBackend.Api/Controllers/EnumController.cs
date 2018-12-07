using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// FileClass  文件类别枚举
    /// BusinessStatus  营业状态枚举
    /// IndentStatus  订单信息枚举
    /// SpeciesClass  农作物分类枚举
    /// SeedClass 种子分类枚举
    /// </summary>
    //[Microsoft.AspNetCore.Authorization.Authorize]//每一次访问接口都必须使用authorization
   // [System.Web.Http.Authorize()]//带有cookie性质的token缓存机制
    [Route("api/[controller]")]
    public class EnumController :Controller
    {
        private readonly ILogger<EnumController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;
        public EnumController(ILogger<EnumController> logger,
                                    IMailService mailService,
                                    ISeedRepository productRepository)
        {

            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet("filesclass")]
        public IActionResult getenumfile()
        {
      
            var claims = User.Claims;
            string s = "";
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
           for(int i = 0; i < Enum.GetValues(typeof(FileClass)).Length; i++)
            {

                keyValuePairs.Add(Enum.GetName(typeof(FileClass), i), i);
            }
            return Ok(keyValuePairs);
        }
        [HttpGet("businessstatus")]
        public IActionResult getenumbusiness()
        {

            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            for (int i = 0; i < Enum.GetValues(typeof(BusinessStatus)).Length; i++)
            {

                keyValuePairs.Add(Enum.GetName(typeof(BusinessStatus), i), i);
            }
            return Ok(keyValuePairs);
      
        }
        [HttpGet("speciesslass")]
        public IActionResult getenumspeciess()
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            for (int i = 0; i < Enum.GetValues(typeof(SpeciesClass)).Length; i++)
            {

                keyValuePairs.Add(Enum.GetName(typeof(SpeciesClass), i), i);
            }
            return Ok(keyValuePairs);

        }
        [HttpGet("indentstatus")]
        public IActionResult getenumindent()
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            for (int i = 0; i < Enum.GetValues(typeof(IndentStatus)).Length; i++)
            {

                keyValuePairs.Add(Enum.GetName(typeof(IndentStatus), i), i);
            }
            return Ok(keyValuePairs);
     
        }
        [HttpGet("seedclass")]
        public IActionResult getenumseed()
        {

            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            for (int i = 0; i < Enum.GetValues(typeof(SeedClass)).Length; i++)
            {

                keyValuePairs.Add(Enum.GetName(typeof(SeedClass), i), i);
            }
            return Ok(keyValuePairs);

        }

    }

}
